(function () {

	"use strict";

    function readMetadata(buffer) {
        var dataview = new DataView(buffer);
        var offset = 0;

        function dword() {
            var rv = dataview.getUint32(offset, true);
            offset += 4;
            return rv;
        }
        
        function sdword() {
            var rv = dataview.getInt32(offset, true);
            offset += 4;
            return rv;
        }
        
        
        function decr(type) {
            return function() {
                return type() - 1; 
            }
        }

        
        function word() {
            var rv = dataview.getUint16(offset, true);
            offset += 2;
            return rv;
        }

        function byte() {
            return dataview.getUint8(offset++, true);
        }

        function reserved(type, value) {
            return function () {
                var rv = type();
                console.assert(rv == value, "Invalid reserved value",
                 rv.toString(16), value.toString(16));
            };
        }

        function sstring(lreader, creader, plus) {
            if(!plus)
                plus = 0;
            return function () {
                var len = lreader();
                var rv = new Array();
                for (var i = 0; i < len; i++) {
                    var c = creader();
                    rv.push(c == 0 ? "" : String.fromCharCode(c));
                    i += plus;
                }
                return rv.join("");
            };
        }
        
        

        function dstring(creader, filter) {
            return function () {
                var rv = new Array();
                while (true) {
                    var c = creader();

                    if (c == 0) {

                        break;
                    }

                    rv.push(String.fromCharCode(c));

                }
                return filter? filter(rv.join("")) : rv.join("");
            };
        }

        function sarray(lreader, type) {
            return function () {
                var rv = Array();
                var len = lreader();
                for (var i = 0; i < len; i++)
                rv.push(type());
                return rv;
            };
        }

        
        function offround(type, x) {
        	return function() {
        		var rv = type();
        		var n =  x -(offset % x);
            	offset += n == 4 ? 0 : n;
            	return rv;
        	};
        }

        function struct() {
            var fields = Array.prototype.slice.call(arguments, 0);
            return function () {
                var rv = new Object();
                for (var i = 0; i < fields.length; i++) {
                	var val = fields[i][1]();
                	if(typeof val != "undefined")
                		rv[fields[i][0]] = val;
                }
                return rv;
            };
        }
        
        function groupby(name, arrtype) {
        	return function() {
        		var list = arrtype();
        		var rv = {};
        		for(var i = 0; i < list.length; i++) {
        			var aux = list[i][name];
        			delete list[i][name];
        			rv[aux] = list[i];
        		}
        		return rv;
        	};
        }
        
        function flags(len, fields) {
            return function() {
                var bits = bitmap(len);
                var rv = {};
            	for(var i in fields){
            		rv[i] = bits[fields[i]];
            	}
            	return rv;
        	}
        }

        function bitmap(len) {
        	var bits = new Array();
        	for(var i = 0; i < len; i++) {
        		var b = byte();
        		
        		for(var j = 0; j < 8; j++) {
        			bits.push((b &   1 << j) > 0);
        		}
        	}
        	return bits;
        }
        
        offset = 0x264;
       
        var netdir = struct(
          ["cb", dword],
          ["MajorRuntimeVersion", word],
          ["MinorRuntimeVersion", word],
          ["MetadataRVA", dword],
          ["MetadataSize", dword],
          ["Flags", flags(4,{})],
          ["EntryPointTojen", dword],
          ["ResourcesRVA", dword],
          ["ResourcesSize", dword],
          ["StrongNameSignatureRVA", dword],
          ["StrongNameSignatureSize", dword]
        )();
        
        var RVA = -0x1e00;
        
        var metadataoffset = RVA + netdir.MetadataRVA;
       
        offset = metadataoffset;

        
        var substringone = function(s) { return s.substring(1); };
        
        var root = struct(
        		["Signature", reserved(dword, 0x424A5342)],
        		["MajorVersion", word],
        		["MinorVersion", word],
        		["Reserved", reserved(dword, 0)],
        		["Version", sstring(dword, byte)],
        		["Flags", word],
        		["Streams", groupby("Name",sarray(word, struct(
        				["Offset", dword],
                		["Size", dword],
                		["Name", offround(dstring(byte,substringone),4)]
        		)))]
        )();

        var ids = {Module:0,TypeRef:1,TypeDef:2,Field:4,MethodDef:6,Param:8,
            InterfaceImpl:9,MemberRef:10,Constant:11,CustomAttribute:12,
            FieldMarshal:13,DeclSecurity:14,ClassLayout:15,FieldLayout:16,
            StandAloneSig:17,EventMap:18,Event:20,PropertyMap:21,Property:23,
            MethodSemantics:24,MethodImpl:25,ModuleRef:26,TypeSpec:27,
            ImplMap:28,FieldRVA:29,Assembly:32,AssemblyProcessor:33,
            AssemblyOS:34,AssemblyRef:35,AssemblyRefProcessor:36,
            AssemblyRefOS:37,File:38,ExportedType:39,ManifestResource:40,
            NestedClass:41,GenericParam:42,GenericParamConstraint:44
        };
        
        var invertids = [];
        for(var i in ids)
            invertids[ids[i]] = i;
        
        offset = root.Streams["~"].Offset + metadataoffset - 1;

        function nstring() {
            var isbig = root.Streams["~"].HeapOffsetSizes.BigString;
            var index = isbig ? dword() : word();
            var backup = offset;
            offset = root.Streams.Strings.Offset + metadataoffset + index;
            
            var rv = dstring(byte)();
            
            offset = backup;
            return rv;
        }
        
        function nguidomit() {
            var isbig = root.Streams["~"].HeapOffsetSizes.BigGUID;
            var index = isbig ? dword() : word();
        }
        
        function nblob() {
            return root.Streams["~"].HeapOffsetSizes.BigBlob ? dword() : word();
            
        }
        
        function nguid() {
            var isbig = root.Streams["~"].HeapOffsetSizes.BigGUID;
            var index = isbig ? dword() : word();
            var backup = offset;
            offset = root.Streams.Strings.Offset + metadataoffset + index + 1;
            
            var rv = new Array();
            for(var i = 0; i < 16; i++) { 
                var c = byte().toString(16);
                
                if(c.length == 1)
                    c = "0" + c;
                    
                rv.push(c);
            }
            offset = backup;
            return rv.join("");
        }

        root.Streams["~"] = struct(
            ["Reserved", reserved(dword, 0)],
            ["MajorVersion", byte],
            ["MinorVersion",byte],
            ["HeapOffsetSizes", flags(2,{BigString:1,BigGUID:2,BigBlob:3})],
            ["Reserved",reserved(byte,1)],
            ["Valid", flags(8, ids)],
            ["Sorted", flags(8, ids)]
        )();
        
        var counts = {}
        for(var i = 0; i < invertids.length; i++) {
            if(typeof invertids[i] == "undefined") {
                continue;
            }

            if(root.Streams["~"].Valid[invertids[i]])
                counts[invertids[i]] = dword();
        }
        
        function ind(bits, mask) {
            var size = word;
            for(var i = 0; i < mask.length; i++) {
                if(counts[mask[i]] >= Math.pow(2,16 - bits)) {
                    size = dword;
                    break;
                }
            }
            return function() {
                var rv = new Array();
                var value = size();
                if(value == 0)
                    return null;
                rv[0] = mask[value & (Math.pow(2, bits) - 1)];
                rv[1] = (value >> bits) - 1;
                
                return rv;
            }
        }
        
        var Index = {
            ResolutionScope:ind(2,["Module","ModuleRef","AssemblyRef","TypeRef"]),
            TypeDefOrRef:ind(2,["TypeDef","TypeRef","TypeSpec"]),
            MemberRefParent:ind(3,["TypeDef","TypeRef","ModuleRef","MethodDef",
                "TypeSpec"]),
            CustomAttributeType:ind(3,[null,null,"MethodDef","MemberRef"]),
            HasCustomAttribute:ind(5,["MethodDef","FieldDef","TypeRef",
                "TypeDef","ParamDef","InterfaceImpl","MemberRef","Module",
                "Permission","Property","Event","StandAloneSig","ModuleRef",
                "TypeSpec","Assembly","AssemblyRef","File","ExportedType",
                "ManifestResource"]),
            HasSemantics:ind(1, ["Property","Event"])
        };
        
        
        var Tables = {
            "Module":struct(
                ["Generation",reserved(word,0)],
                ["Name",nstring],
                ["Mvid", nguid],
                ["EncId", nguidomit],
                ["EncBaseId", nguidomit]
            ),
            "TypeRef":struct(
                ["ResolutionScope",Index.ResolutionScope],
                ["TypeName",nstring],
                ["TypeNamespace",nstring]
            ),
            "TypeDef":struct(
                ["Flags",flags(4,{})],
                ["TypeName",nstring],
                ["TypeNamespace", nstring],
                ["Extends", Index.TypeDefOrRef],
                ["FieldList", decr(word)],
                ["MethodList", decr(word)]
            ),
            "MethodDef":struct(
                ["RVA",dword],
                ["ImplFlags", word],
                ["Flags", word],
                ["Name", nstring],
                ["Signature", nblob],
                ["ParamList", decr(word)]
            ),
            "MemberRef":struct(
                ["Class",Index.MemberRefParent],
                ["Name", nstring],
                ["Signature", nblob]
            ),
            "CustomAttribute":struct(
                ["Parent",Index.HasCustomAttribute],
                ["Type",Index.CustomAttributeType],
                ["Value", nblob]
            ),
            "Assembly":struct(
                ["HashAlgId", dword],
                ["MajorVersion", word],
                ["MinorVersion",word],
                ["BuildNumber",word],
                ["RevisionNumber", word],
                ["Flags", dword],
                ["Value", nblob],
                ["Name", nstring],
                ["Culture", nstring]
            ),
            "AssemblyRef":struct(
                ["MajorVersion", word],
                ["MinorVersion",word],
                ["BuildNumber",word],
                ["RevisionNumber", word],
                ["Flags",flags(4,{})],
                ["PublicKeyOrToken", nblob],
                ["Name", nstring],
                ["Culture", nstring],
                ["HashValue", nblob]
            ),
            "Param":struct(
                ["Flags",flags(2,{})],
                ["Sequence", decr(word)],
                ["Name", nstring]
            ),
            "Property":struct(
                ["Flags", flags(2, {})],
                ["Name", nstring],
                ["Type", nblob]
            ),
            "PropertyMap":struct(
                ["Parent", decr(word)],
                ["PropertyList", decr(word)]
            ),
            "MethodSemantics":struct(
                ["Semantics", flags(2,{})],
                ["Method", decr(word)],
                ["Association", Index.HasSemantics]
            ),
            "Field":struct(
              ["Flags",flags(2, {})],
              ["Name", nstring],
              ["Signature", nblob]
            )
            
 
        };
        
        
        /*
    Flags (a 2-byte bit mask of type FieldAttributes).
    Name (index into String heap).
    Signature (index into Blob heap).
*/
        
        var tables = {};
        for(var i = 0; i < invertids.length; i++) {
            var current = counts[invertids[i]];
            if(current) {
                var aux = [];
                for(var j = 0; j < current; j++) {

                    try{
                    aux.push(Tables[invertids[i]]());
                    }
                    catch(e)
                    {
                        throw new Error(i);
                    }
                }
                tables[invertids[i]] = aux;
            }
        }
        
        offset = root.Streams.US.Offset + metadataoffset;
        var us = [];

        while(offset < root.Streams.US.Offset + metadataoffset + root.Streams.US.Size) {
            
            us.push(sstring(byte, word,1)());
        }
        
        for(var i = 0; i < tables.MethodDef.length; i++) {
            offset = tables.MethodDef[i] + metadataoffset;
            var isFat = byte() == 2;
        }
        
        var blob = buffer.slice(metadataoffset + root.Streams.Blob.Offset,
            metadataoffset + root.Streams.Blob.Offset+root.Streams.Blob.Size);
        
        
        function noparam(opcode)
        {
          return opcode;
        }
        
        function wordparam(opcode)
        {
          return [opcode, word()];
        }
        
        
        function sdwordparam(opcode)
        {
          return [opcode, sdword()];
        }
        
         function dwordparam(opcode)
        {
          return [opcode, dword()];
        } 
        
        function tribyteparam(opcode)
        {
          offset--;
          var x = dword();
          return [opcode, x >> 8]
        }
        
        function TreeAndOneByte(opcode) {
           var data =  dword();
           return [opcode, invertids[data >> 24], data & 0xFFFFFF];
           
        }
        
        
            
            
        if(tables.MethodDef) {
          for(var i = 0; i < tables.MethodDef.length; i++) {
            var current = tables.MethodDef[i];
            var backup = offset;
            offset = RVA + current.RVA;
            
            var code = [];
            var limit;
            var b = byte();
            
  
            if(b & 3 == 3) {
              throw new Error("Metodos gordos no implementados");
            } else {
              
              var size = b >> 2;
              limit = offset + (b >> 2);
            }
            
            
            

            var params = [];
            params[0] = noparam;
            params[0x02] = noparam;
            params[0x03] = noparam;
            params[0x06] = noparam;
            params[0x0A] = noparam;
            params[0x14] = noparam;
            params[0x16] = noparam;
            params[0x17] = noparam;
            params[0x20] = sdwordparam;
            params[0x25] = noparam;
            params[0x26] = noparam;
            params[0x28] = TreeAndOneByte;
            params[0x2A] = noparam;
            params[0x6F] = TreeAndOneByte;
            params[0x72] = function(b) { return [b, us[dword() & 0xFFFFFF ]]; };
            params[0x7B] = TreeAndOneByte;
            
            params[0x7D] = TreeAndOneByte;
            params[0x8D] = TreeAndOneByte;
            params[0xA2] = noparam;
            
            //0x2a
            
            while(offset < limit) {
              
              var opcode = byte();    
              
             
              
              if(!params[opcode])
                throw new Error("0x" + opcode.toString(16));
              else
                code.push(params[opcode](opcode));
            }

            delete current.RVA;
            current.Code = code;
            
            offset = backup;
          }
        }
        
        return {Version:root.Version,Tables:tables,Blob:blob};
        
    }
    
    function realLoad(path,name, callback)
    {
      var xhr = new XMLHttpRequest();
      xhr.open("GET", path + name, true);
      xhr.responseType = "arraybuffer";
      xhr.send();
      xhr.onreadystatechange = function () {
          if (xhr.readyState == 4) {
              callback(name, readMetadata(xhr.response));
          }
      };
    }
    
    
    

    //Se obtiene la lista de modulos que se van a cargar
    var modules = document.getElementsByTagName("script");
    modules = modules[modules.length - 1];
    var path = modules.getAttribute("path");
    modules = modules.getAttribute("load").split(";");

    var final = {};
    var j = modules.length;
    for(var i = 0; i < modules.length; i++) {
      realLoad(path, modules[i], function(name, data) {
        final[name] = data;
        j--;
        if(j == 0)
          startTypesResolutions(final);
      });
    }

    
    function Domain()
    {
      this.Assemblies = {};
      this.Modules = {};
    }

    function Module(tok, name, domain)
    {
      this.meta= tok;
      this.Domain = domain;
      this.Name = name;
    }

    function Assembly(tok, module)
    {
      this.Module = module;
      for(var i in tok)
        this[i] = tok[i];
      this.Types = new Array();
    }

    Assembly.prototype.getType = function(namespace, name) {
      for(var i = 0; i < this.Types.length; i++)
      {
        if(this.Types[i].TypeName == name && this.Types[i].TypeNamespace == namespace)
        {
          return this.Types[i];
        }
      }
      throw new Error("Type " + name + " of " + namespace + " not found in " + this.Name);
    };
    
    function Type(tok, module)
    {
      this.Module = module;
      for(var i in tok)
        this[i] = tok[i];
    }

    function Method(tok, module)
    {
      this.Module = module;
      for(var i in tok)
        this[i] = tok[i];
    }

    
    function startTypesResolutions(meta)
    {
      var domain = new Domain();
      var assemblies = {};
     
      //Resuelve todas las definiciones de modulos
      for(var i in meta)
      {
        var mod = new Module(meta[i], i, domain);
        domain.Modules[i] = mod;
        
        var tables = {"Assembly":Assembly,"TypeDef":Type,"MethodDef":Method};
        
        for(var l in tables)
        {
          if(meta[i].Tables[l]) {
            for(var j = 0; j < meta[i].Tables[l].length; j++)
            {
              meta[i].Tables[l][j] = new tables[l](meta[i].Tables[l][j], mod);
              switch(l)
              {
                case "Assembly":
                  assemblies[meta[i].Tables[l][j].Name] = meta[i].Tables[l][j];
                  break;
              }
            }
          }
        }
      }
      
      //Reemplaza todos los assemblyref por Assembly reales
      for(var i in domain.Modules)
      {
        var mod = domain.Modules[i];
        
        for(var j = 0; mod.meta.Tables.AssemblyRef && j < mod.meta.Tables.AssemblyRef.length; j++) {
          mod.meta.Tables.AssemblyRef[j] = assemblies[mod.meta.Tables.AssemblyRef[j].Name];
        }
      }
      
      //Lleno la lista de tipos de cada ensamblado
      for(var i in assemblies)
      {
        var assembly = assemblies[i];
        
        if(assembly.Module.meta.Tables.TypeDef)
        {
           for(var j = 0; j < assembly.Module.meta.Tables.TypeDef.length; j++)
           {
             assembly.Types.push(assembly.Module.meta.Tables.TypeDef[j]);
           }
        }
      }
      
      //lleno todas los typeref con sus correspondientes typedef
      //FIXME: No se que mierda para con los tipos dentro de tipos
       for(var i in assemblies)
      {
        var assembly = assemblies[i];
        
        if(assembly.Module.meta.Tables.TypeRef)
        {
           for(var j = 0; j < assembly.Module.meta.Tables.TypeRef.length; j++)
           {
             var ref = assembly.Module.meta.Tables.TypeRef[j];
             var module = assembly.Module.meta.Tables[ref.ResolutionScope[0]][ref.ResolutionScope[1]];
             assembly.Module.meta.Tables.TypeRef[j] = module.getType(ref.TypeNamespace, ref.TypeName);
           }
        }
      }
      
      //Lleno las referencias a las clases padre
      //FIXME: falta instanciar TypeSpec y TypeDef
      for(var i in assemblies)
      {
        var assembly = assemblies[i];
        
        if(assembly.Module.meta.Tables.TypeDef)
        {
           for(var j = 0; j < assembly.Module.meta.Tables.TypeDef.length; j++)
           {
             var type = assembly.Module.meta.Tables.TypeDef[j];
             
             if(type.Extends != null)
             {
               type.Extends = assembly.Module.meta.Tables[type.Extends[0]][type.Extends[1]];
             }
           }
        }
      }
      
      console.debug(domain);
    }
    
    
    
})();