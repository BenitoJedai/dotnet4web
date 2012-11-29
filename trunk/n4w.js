(function ()
{

    "use strict";

    
    
    var natives = {
      'System.Runtime.InteropServices.WebIntegration':{
        "JavascriptObject":{
          get_Global:function() {
            return {type:this.method.DeclaringType, value:window}
          },
          Call:function(instance, methodname, args) {
            return {
              type:this.method.DeclaringType,
              value:instance.value[methodname.value].apply(instance.value,args.value)
            }
         },
          op_Implicit:function(boxvalue)
          {
            return boxvalue.value;
          }
        }
      }
    }

    
    function readMetadata(buffer)
    {
        var dataview = new DataView(buffer);
        var offset = 0;

        function dword()
        {
            var rv = dataview.getUint32(offset, true);
            offset += 4;
            return rv;
        }

        function sdword()
        {
            var rv = dataview.getInt32(offset, true);
            offset += 4;
            return rv;
        }


        function decr(type)
        {
            return function ()
            {
                return type() - 1;
            }
        }


        function word()
        {
            var rv = dataview.getUint16(offset, true);
            offset += 2;
            return rv;
        }

        function byte()
        {
            return dataview.getUint8(offset++, true);
        }

        function reserved(type, value)
        {
            return function ()
            {
                var rv = type();
                console.assert(rv == value, "Invalid reserved value",
                 rv.toString(16), value.toString(16));
            };
        }

        function sstring(lreader, creader, plus)
        {
            if (!plus)
                plus = 0;
            return function ()
            {
                var len = lreader();
                var rv = new Array();
                for (var i = 0; i < len; i++)
                {
                    var c = creader();
                    rv.push(c == 0 ? "" : String.fromCharCode(c));
                    i += plus;
                }
                return rv.join("");
            };
        }



        function dstring(creader, filter)
        {
            return function ()
            {
                var rv = new Array();
                while (true)
                {
                    var c = creader();

                    if (c == 0)
                    {

                        break;
                    }

                    rv.push(String.fromCharCode(c));

                }
                return filter ? filter(rv.join("")) : rv.join("");
            };
        }

        function sarray(lreader, type)
        {
            return function ()
            {
                var rv = Array();
                var len = lreader();
                for (var i = 0; i < len; i++)
                    rv.push(type());
                return rv;
            };
        }


        function offround(type, x)
        {
            return function ()
            {
                var rv = type();
                var n = x - (offset % x);
                offset += n == 4 ? 0 : n;
                return rv;
            };
        }

        function struct()
        {
            var fields = Array.prototype.slice.call(arguments, 0);
            return function ()
            {
                var rv = new Object();
                for (var i = 0; i < fields.length; i++)
                {
                    var val = fields[i][1]();
                    if (typeof val != "undefined")
                        rv[fields[i][0]] = val;
                }
                return rv;
            };
        }

        function groupby(name, arrtype)
        {
            return function ()
            {
                var list = arrtype();
                var rv = {};
                for (var i = 0; i < list.length; i++)
                {
                    var aux = list[i][name];
                    delete list[i][name];
                    rv[aux] = list[i];
                }
                return rv;
            };
        }

        function flags(len, fields)
        {
            return function ()
            {
                var bits = bitmap(len);
                var rv = {};
                for (var i in fields)
                {
                    rv[i] = bits[fields[i]];
                }
                return rv;
            }
        }

        function bitmap(len)
        {
            var bits = new Array();
            for (var i = 0; i < len; i++)
            {
                var b = byte();

                for (var j = 0; j < 8; j++)
                {
                    bits.push((b & 1 << j) > 0);
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
          ["Flags", flags(4, {})],
          ["EntryPointTojen", dword],
          ["ResourcesRVA", dword],
          ["ResourcesSize", dword],
          ["StrongNameSignatureRVA", dword],
          ["StrongNameSignatureSize", dword]
        )();

        var RVA = -0x1e00;

        var metadataoffset = RVA + netdir.MetadataRVA;

        offset = metadataoffset;


        var substringone = function (s) { return s.substring(1); };

        var root = struct(
        		["Signature", reserved(dword, 0x424A5342)],
        		["MajorVersion", word],
        		["MinorVersion", word],
        		["Reserved", reserved(dword, 0)],
        		["Version", sstring(dword, byte)],
        		["Flags", word],
        		["Streams", groupby("Name", sarray(word, struct(
        				["Offset", dword],
                		["Size", dword],
                		["Name", offround(dstring(byte, substringone), 4)]
        		)))]
        )();

        var ids = { Module: 0, TypeRef: 1, TypeDef: 2, Field: 4, MethodDef: 6, Param: 8,
            InterfaceImpl: 9, MemberRef: 10, Constant: 11, CustomAttribute: 12,
            FieldMarshal: 13, DeclSecurity: 14, ClassLayout: 15, FieldLayout: 16,
            StandAloneSig: 17, EventMap: 18, Event: 20, PropertyMap: 21, Property: 23,
            MethodSemantics: 24, MethodImpl: 25, ModuleRef: 26, TypeSpec: 27,
            ImplMap: 28, FieldRVA: 29, Assembly: 32, AssemblyProcessor: 33,
            AssemblyOS: 34, AssemblyRef: 35, AssemblyRefProcessor: 36,
            AssemblyRefOS: 37, File: 38, ExportedType: 39, ManifestResource: 40,
            NestedClass: 41, GenericParam: 42, GenericParamConstraint: 44
        };

        var invertids = [];
        for (var i in ids)
            invertids[ids[i]] = i;

        offset = root.Streams["~"].Offset + metadataoffset - 1;

        function nstring()
        {
            var isbig = root.Streams["~"].HeapOffsetSizes.BigString;
            var index = isbig ? dword() : word();
            var backup = offset;
            offset = root.Streams.Strings.Offset + metadataoffset + index;

            var rv = dstring(byte)();

            offset = backup;
            return rv;
        }

        function nguidomit()
        {
            var isbig = root.Streams["~"].HeapOffsetSizes.BigGUID;
            var index = isbig ? dword() : word();
        }

        function nblob()
        {
            return root.Streams["~"].HeapOffsetSizes.BigBlob ? dword() : word();

        }

        function nguid()
        {
            var isbig = root.Streams["~"].HeapOffsetSizes.BigGUID;
            var index = isbig ? dword() : word();
            var backup = offset;
            offset = root.Streams.Strings.Offset + metadataoffset + index + 1;

            var rv = new Array();
            for (var i = 0; i < 16; i++)
            {
                var c = byte().toString(16);

                if (c.length == 1)
                    c = "0" + c;

                rv.push(c);
            }
            offset = backup;
            return rv.join("");
        }

        root.Streams["~"] = struct(
            ["Reserved", reserved(dword, 0)],
            ["MajorVersion", byte],
            ["MinorVersion", byte],
            ["HeapOffsetSizes", flags(2, { BigString: 1, BigGUID: 2, BigBlob: 3 })],
            ["Reserved", reserved(byte, 1)],
            ["Valid", flags(8, ids)],
            ["Sorted", flags(8, ids)]
        )();

        var counts = {}
        for (var i = 0; i < invertids.length; i++)
        {
            if (typeof invertids[i] == "undefined")
            {
                continue;
            }

            if (root.Streams["~"].Valid[invertids[i]])
                counts[invertids[i]] = dword();
        }

        function ind(bits, mask)
        {
            var size = word;
            for (var i = 0; i < mask.length; i++)
            {
                if (counts[mask[i]] >= Math.pow(2, 16 - bits))
                {
                    size = dword;
                    break;
                }
            }
            return function ()
            {
                var rv = new Array();
                var value = size();
                if (value == 0)
                    return null;
                rv[0] = mask[value & (Math.pow(2, bits) - 1)];
                rv[1] = (value >> bits) - 1;

                return rv;
            }
        }

        var Index = {
            ResolutionScope: ind(2, ["Module", "ModuleRef", "AssemblyRef", "TypeRef"]),
            TypeDefOrRef: ind(2, ["TypeDef", "TypeRef", "TypeSpec"]),
            MemberRefParent: ind(3, ["TypeDef", "TypeRef", "ModuleRef", "MethodDef",
                "TypeSpec"]),
            CustomAttributeType: ind(3, [null, null, "MethodDef", "MemberRef"]),
            HasCustomAttribute: ind(5, ["MethodDef", "FieldDef", "TypeRef",
                "TypeDef", "ParamDef", "InterfaceImpl", "MemberRef", "Module",
                "Permission", "Property", "Event", "StandAloneSig", "ModuleRef",
                "TypeSpec", "Assembly", "AssemblyRef", "File", "ExportedType",
                "ManifestResource"]),
            HasSemantics: ind(1, ["Property", "Event"]),
            HasConstant: ind(2, ["FieldDef", "ParamDef", "Property"])
        };


        var Tables = {
            "Module": struct(
                ["Generation", reserved(word, 0)],
                ["Name", nstring],
                ["Mvid", nguid],
                ["EncId", nguidomit],
                ["EncBaseId", nguidomit]
            ),
            "TypeRef": struct(
                ["ResolutionScope", Index.ResolutionScope],
                ["TypeName", nstring],
                ["TypeNamespace", nstring]
            ),
            "TypeDef": struct(
                ["Flags", flags(4, {})],
                ["TypeName", nstring],
                ["TypeNamespace", nstring],
                ["Extends", Index.TypeDefOrRef],
                ["FieldList", decr(word)],
                ["MethodList", decr(word)]
            ),
            "MethodDef": struct(
                ["RVA", dword],
                ["ImplFlags", flags(2, {
                    InternalCall: 12,
                    Syncronized: 4,
                    Managed: 2,
                    NoInlining: 3,
                    PreserveSig: 6

                })],
                ["Flags", flags(2,{IsStatic:4})],
                ["Name", nstring],
                ["Signature", nblob],
                ["ParamList", decr(word)]
            ),
            "MemberRef": struct(
                ["Class", Index.MemberRefParent],
                ["Name", nstring],
                ["Signature", nblob]
            ),
            "CustomAttribute": struct(
                ["Parent", Index.HasCustomAttribute],
                ["Type", Index.CustomAttributeType],
                ["Value", nblob]
            ),
            "Assembly": struct(
                ["HashAlgId", dword],
                ["MajorVersion", word],
                ["MinorVersion", word],
                ["BuildNumber", word],
                ["RevisionNumber", word],
                ["Flags", dword],
                ["Value", nblob],
                ["Name", nstring],
                ["Culture", nstring]
            ),
            "AssemblyRef": struct(
                ["MajorVersion", word],
                ["MinorVersion", word],
                ["BuildNumber", word],
                ["RevisionNumber", word],
                ["Flags", flags(4, {})],
                ["PublicKeyOrToken", nblob],
                ["Name", nstring],
                ["Culture", nstring],
                ["HashValue", nblob]
            ),
            "Param": struct(
                ["Flags", flags(2, {})],
                ["Sequence", decr(word)],
                ["Name", nstring]
            ),
            "Property": struct(
                ["Flags", flags(2, {})],
                ["Name", nstring],
                ["Type", nblob]
            ),
            "PropertyMap": struct(
                ["Parent", decr(word)],
                ["PropertyList", decr(word)]
            ),
            "MethodSemantics": struct(
                ["Semantics", flags(2, {})],
                ["Method", decr(word)],
                ["Association", Index.HasSemantics]
            ),
            "Field": struct(
              ["Flags", flags(2, {})],
              ["Name", nstring],
              ["Signature", nblob]
            ),
            "Constant": struct(
                ["Type", byte],
                ["Reserved", reserved(byte, 0)],
                ["Parent", Index.HasConstant],
                ["Value", nblob]
            ),
            "StandAloneSig": struct(
                ["Signature", nblob]
            )


        };


        /*
        Flags (a 2-byte bit mask of type FieldAttributes).
        Name (index into String heap).
        Signature (index into Blob heap).
        */

        var tables = {};
        for (var i = 0; i < invertids.length; i++)
        {
            var current = counts[invertids[i]];
            if (current)
            {
                var aux = [];
                for (var j = 0; j < current; j++)
                {

                    try
                    {
                        aux.push(Tables[invertids[i]]());
                    }
                    catch (e)
                    {
                        throw new Error(i);
                    }
                }
                tables[invertids[i]] = aux;
            }
        }

        offset = root.Streams.US.Offset + metadataoffset;
        var us = [];

        while (offset < root.Streams.US.Offset + metadataoffset + root.Streams.US.Size)
        {

            us.push(sstring(byte, word, 1)());
        }

        for (var i = 0; i < tables.MethodDef.length; i++)
        {
            offset = tables.MethodDef[i] + metadataoffset;
            var isFat = byte() == 2;
        }

        var blob = buffer.slice(metadataoffset + root.Streams.Blob.Offset,
            metadataoffset + root.Streams.Blob.Offset + root.Streams.Blob.Size);


        function noparam(opcode)
        {
            return [opcode];
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

        function TreeAndOneByte(opcode)
        {
            var data = dword();
            return [opcode, data >> 24, data & 0xFFFFFF];

        }




        if (tables.MethodDef)
        {
            for (var i = 0; i < tables.MethodDef.length; i++)
            {
                var current = tables.MethodDef[i];
                var backup = offset;

                if (current.RVA == 0)
                {
                    delete current.RVA;
                    continue;
                }


                offset = RVA + current.RVA;



                var code = [];
                var limit;

                var b = byte();



                if (b & 3 == 3)
                {
                    throw new Error("Metodos gordos no implementados");
                } else
                {

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
                params[0x72] = function (b) { return [b, us[dword() & 0xFFFFFF]]; };
                params[0x73] = TreeAndOneByte;

                params[0x7B] = TreeAndOneByte;

                params[0x7D] = TreeAndOneByte;
                params[0x8D] = TreeAndOneByte;
                params[0xA2] = noparam;

                //0x2a

                while (offset < limit)
                {

                    var opcode = byte();



                    if (!params[opcode])
                        throw new Error("0x" + opcode.toString(16));
                    else
                        code.push(params[opcode](opcode));
                }

                delete current.RVA;
                current.Code = code;

                offset = backup;
            }
        }

        return { Version: root.Version, Tables: tables, Blob: blob };

    }

    function realLoad(path, name, callback)
    {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", path + name, true);
        xhr.responseType = "arraybuffer";
        xhr.send();
        xhr.onreadystatechange = function ()
        {
            if (xhr.readyState == 4)
            {
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
    for (var i = 0; i < modules.length; i++)
    {
        realLoad(path, modules[i], function (name, data)
        {
            final[name] = data;
            j--;
            if (j == 0)
               main(startTypesResolutions(final));
        });
    }


    function Domain()
    {
        this.Assemblies = {};
        this.Modules = {};
    }

    function Module(tok, name, domain)
    {
        this.meta = tok;
        this.Domain = domain;
        this.Name = name;
    }

    function Assembly(tok, module)
    {
        this.Module = module;
        for (var i in tok)
            this[i] = tok[i];
        this.Types = new Array();
    }

    Assembly.prototype.getType = function (namespace, name)
    {
        for (var i = 0; i < this.Types.length; i++)
        {
            if (this.Types[i].TypeName == name && this.Types[i].TypeNamespace == namespace)
            {
                return this.Types[i];
            }
        }
        throw new Error("Type " + name + " of " + namespace + " not found in " + this.Name);
    };

    function Type(tok, module)
    {
        this.Module = module;
        for (var i in tok)
            this[i] = tok[i];
    }
    
    Type.prototype.getMethod = function(name, signature)
    {
      var match = [];
      for(var i = 0; i < this.MethodList.length; i++)
      {
        if(this.MethodList[i].Name == name)
          match.push(this.MethodList[i]);
      }
      
      if(match.length > 1)
      {
        console.warn("Sobrecarga no implementada en el metodo '" + name+ "' del tipo '" + this.TypeNamespace +"." + this.TypeName + "'");
      }
      
      if(match.length == 0)
        throw new Error("Metodo '" + name+ "' no encontrado en el tipo '" + this.TypeName + "'");
      
      return match[0];
    }

    function Method(tok, module)
    {
        this.Module = module;
        for (var i in tok)
            this[i] = tok[i];
    }


    function startTypesResolutions(meta)
    {
        var domain = new Domain();
        var assemblies = {};

        //Resuelve todas las definiciones de modulos
        for (var i in meta)
        {
            var mod = new Module(meta[i], i, domain);
            domain.Modules[i] = mod;

            var tables = { "Assembly": Assembly, "TypeDef": Type, "MethodDef": Method };

            for (var l in tables)
            {
                if (meta[i].Tables[l])
                {
                    for (var j = 0; j < meta[i].Tables[l].length; j++)
                    {
                        meta[i].Tables[l][j] = new tables[l](meta[i].Tables[l][j], mod);
                        switch (l)
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
        for (var i in domain.Modules)
        {
            var mod = domain.Modules[i];

            for (var j = 0; mod.meta.Tables.AssemblyRef && j < mod.meta.Tables.AssemblyRef.length; j++)
            {
                mod.meta.Tables.AssemblyRef[j] = assemblies[mod.meta.Tables.AssemblyRef[j].Name];
            }
        }

        //Lleno la lista de tipos de cada ensamblado
        for (var i in assemblies)
        {
            var assembly = assemblies[i];

            if (assembly.Module.meta.Tables.TypeDef)
            {
                for (var j = 0; j < assembly.Module.meta.Tables.TypeDef.length; j++)
                {
                    assembly.Types.push(assembly.Module.meta.Tables.TypeDef[j]);
                }
            }
        }

        //lleno todas los typeref con sus correspondientes typedef
        //FIXME: No se que mierda para con los tipos dentro de tipos
        for (var i in assemblies)
        {
            var assembly = assemblies[i];

            if (assembly.Module.meta.Tables.TypeRef)
            {
                for (var j = 0; j < assembly.Module.meta.Tables.TypeRef.length; j++)
                {
                    var ref = assembly.Module.meta.Tables.TypeRef[j];
                    var module = assembly.Module.meta.Tables[ref.ResolutionScope[0]][ref.ResolutionScope[1]];
                    assembly.Module.meta.Tables.TypeRef[j] = module.getType(ref.TypeNamespace, ref.TypeName);
                }
            }
        }

        
        //Lleno las referencias a las clases padre
        //FIXME: falta instanciar TypeSpec y TypeDef
        for (var i in assemblies)
        {
            var assembly = assemblies[i];

            if (assembly.Module.meta.Tables.TypeDef)
            {
                for (var j = 0; j < assembly.Module.meta.Tables.TypeDef.length; j++)
                {
                    var type = assembly.Module.meta.Tables.TypeDef[j];

                    if (type.Extends != null)
                    {
                        type.Extends = assembly.Module.meta.Tables[type.Extends[0]][type.Extends[1]];
                    }
                }
            }
        }
        
        function replacelist(master, slave, reason, reverse)  {
        
          for (var i in assemblies)
          {
              var assembly = assemblies[i];

              if (assembly.Module.meta.Tables[master] && assembly.Module.meta.Tables[slave])
              {
                  var methods = assembly.Module.meta.Tables[slave];
                  var lastMethod = methods.length;
                  for (var j = assembly.Module.meta.Tables[master].length - 1; j >= 0; j--)
                  {
                      var type = assembly.Module.meta.Tables[master][j];

                      var newml = [];
                      if (type[reason] != lastMethod)
                      {
                          newml = methods.slice(type[reason], lastMethod);
                          lastMethod = type[reason];
                      }

                      type[reason] = newml;
                      
                      for(var k = 0; k < newml.length; k++)
                        newml[k][reverse] = type;
                  }
              }
          }
        }
        
        replacelist("TypeDef","MethodDef","MethodList", "DeclaringType");
        replacelist("MethodDef","Param","ParamList", "DeclaringMethod");
        replacelist("TypeDef","Field","FieldList", "DeclaringType");
        

       for (var i in assemblies)
        {
            var assembly = assemblies[i];

            if (assembly.Module.meta.Tables.MemberRef)
            {
                var refs = assembly.Module.meta.Tables.MemberRef;
                
                
                for (var j = 0; j < refs.length; j++)
                {
                    refs[j] = assembly.Module.meta.Tables[ refs[j].Class[0] ][ refs[j].Class[1] ].getMethod(  refs[j].Name );
                }
            }
        }

        var toresolve = [0x28,0x8D,0x6f];
        
        //Resuelvo el bytecode
        for (var i in assemblies)
        {
            var assembly = assemblies[i];

            if (assembly.Module.meta.Tables.MethodDef)
            {
                var methods = assembly.Module.meta.Tables.MethodDef;

                for(var j = 0; j < methods.length; j++)
                {
                  
                  var method = methods[j];
                  
                  if(method.ImplFlags.InternalCall)
                  {
                    method.Code = natives
                      [method.DeclaringType.TypeNamespace]
                      [method.DeclaringType.TypeName]
                      [method.Name];
                  }
                  else
                  {
                    for(var k = 0; k < method.Code.length; k++)
                    {
                      var line = method.Code[k];
                      
                      if(line.length == 3 && toresolve.indexOf(line[0]) != -1)
                      {
                        var index = line.pop();
                        var table = line.pop();

                        if(table == 6) {
                          table = "MethodDef";
                        } else if(table == 10) {
                          table = "MemberRef";
                        } else if(table == 1) {
                          table = "TypeDef";
                          index++;
                        } else {
                          debugger;
                        }
                  
                        
                        try
                        {
                        
                        
                        method.Code[k] = [line[0], assembly.Module.meta.Tables[table][index-1]];
                        }
                        catch(ex)
                        
                        {
                          debugger;
                          console.debug("a");
                        }

                      }
                    }
                  }
                  
                }
                
            }
        }
        console.warn("ESTO ESTA CHANCHO");
        domain.CoreTypes = {
          array:domain.Modules["mscorlib.dll"].meta.Tables.TypeDef[1],
          string:domain.Modules["mscorlib.dll"].meta.Tables.TypeDef[23],
          int:domain.Modules["mscorlib.dll"].meta.Tables.TypeDef[13]
        }
        return domain;
    }

    
    function Thread(method)
    {
      this.stack = [];
      this.domain = method.Module.Domain;
      this.locals = [];
      this.arguments = [];
      this.method = method;
      this.ip = 0;
      this.sleeped = false;
      this.started = false;
      this.calls = [];
    }
    
    Thread.prototype.start = function(parameter)
    {
      if(this.started)
        throw new Error("El hilo ya se encuentra iniciado");
      
      if(parameter)
        this.arguments.push(parameter);
  
      this._exec();
    }
    
    Thread.prototype.suspend = function()
    {
    }
    
    
    Thread.prototype.resume = function()
    {
      
    }
    
    Thread.prototype._throwoperr = function(opcode, operand)
    {
      //Formateo el codigo de la instruccion
      opcode = opcode.toString(16);
      if(opcode.length == 1) {
        opcode = "0" + opcode;
      }
      var message = "El codigo de instruccion '" + opcode + "' es invalido o aun no esta implementado";
      
      
      console.debug(this);
      //En caso de que no halla operando
      if(typeof operand != "undefined") {
        console.error("Operand:", operand );
      } 
      
      //Lanzo la excepcion
      throw new Error(message);
    }
    
    Thread.prototype._call = function(operand)
    {
      var argc = operand.ParamList.length;
      if(!operand.Flags.IsStatic)
        argc++;
      
      var args = this.stack.splice(-argc, argc);
      
      if(operand.ImplFlags.InternalCall)
      {
        var backup = this.method;
        this.method = operand;
        this.stack.push(operand.Code.apply(this, args));
        this.method = backup;
        this.ip++;
      }
      else
      {
        this.calls.push({
          stack:this.stack,
          locals:this.locals,
          arguments:this.arguments,
          method:this.method,
          ip:this.ip
        });
        this.arguments = args;
        this.stack = [];
        this.ip = 0;
        this.locals = [];
        this.method = operand;
      }
    }
    
    Thread.prototype._ret = function()
    {
      var backup = this.calls.pop();
      if(this.stack.length > 0)
        backup.stack.push(this.stack.pop());
      this.stack = backup.stack;
      this.ip = backup.ip + 1;
      this.arguments = backup.arguments;
      this.locals = backup.locals;
      this.method = backup.method;
    }
    
    Thread.prototype._int = function(number)
    {
      return {type:this.domain.CoreTypes.int,value:number};
    }
    
    Thread.prototype._string = function(number)
    {
      return {type:this.domain.CoreTypes.string,value:number};
    }
    
    Thread.prototype._exec = function()
    {
      while(true) {
        
        //Comprueba que no se alla exedido el limite del sector de codigo
        if(this.ip >= this.method.Code.length) {
          throw new Error("Se supero el limite de las instrucciones");
        }
        
        //Obtiene la instruccion actual a correr y su parametro
        var opcode = this.method.Code[this.ip][0];
        var operand = this.method.Code[this.ip][1];
        
        //Realiza la operacion necesaria dependiendo del operador
        switch(opcode) {
          
          //ldarg.0: Carga el primer argumento en la pila
          case 0x02:
            this.stack.push(this.arguments[0]);
            this.ip++;
            break;
          
          //ldc.i4.1: Carga un Int32 de valor 0 en la pila
          case 0x16:
            this.stack.push(this._int(0));
            this.ip++;
            break;
          
          //ldc.i4.1: Carga un Int32 de valor 1 en la pila
          case 0x17:
            this.stack.push(this._int(1));
            this.ip++;
            break;
            
          //dup: Duplica el elemento de la cima de la pila
          case 0x25:
            this.stack.push(this.stack[this.stack.length -1]);
            this.ip++;
            break;
            
          //pop: Quita el elemento de la cima de la pila
          case 0x26:
            this.stack.pop();
            this.ip++;
            break;
            
          //call <method>: Llama a un metodo por su referencia. como es largo
          //lo implemento en un metodo de thread
          case 0x28:
            this._call(operand);
            break;

          //ret: Termina le ejecuciion de un metodo posiblemente con un valor de retorno
          //FIXME: Como no tengo la firma de los metodos asumo que si la pila
          //tiene elementos es que tiene valor de retorno OJO
          case 0x2A:
            if(this.calls.length == 0) {
              if(this.stack.length == 0) {
                console.info("Hilo finalizado sin valor de retorno");
              } else {
                console.info("Hilo finalizado con valor de retorno", this.stack.pop());
              }
              return;
            }
            
            this._ret(operand);
            break;
          
          //callvit <method>: Llama a un metodo asociado a un determinada instancia
          case 0x6F:
            console.warn("Polimorfismo no implementado, llamando al metodo sin comprobaciones");
            this._call(operand);
            break;
            
          //ldstr: Carga una cadena literal en la cima de la pila
          case 0x72:
            this.stack.push(this._string(operand));
            this.ip++;
            break;
            
          //newarr <type>: Crea un array del tipo especificado sacando el tamaño de la pila
          case 0x8D:
            this.stack.push({type:this.domain.CoreTypes.array,elemtype:operand, value:new Array(this.stack.pop().value)});
            this.ip++;
            break;
            
          //stelem.ref:
          case 0xA2:
            var value = this.stack.pop(), index = this.stack.pop().value, array = this.stack.pop().value;
            array[index] = value;
            value = null; array = null; index = null;
            this.ip++;
            break;
            
          //En caso de no reconocer el operador
          default:
            this._throwoperr(opcode, operand);
        }
        
        operand = null;
        //si se detuvo el hilo se termina la ejecucion
        if(this.sleeped) {
          return;
        }
      }
    }
    
    
    Thread.prototype.sleep = function()
    {
    }
    

    function main(domain)
    {
      var entrythread = new Thread(domain.Modules["n4w.ExampleProject.exe"].meta.Tables.MethodDef[1]);
      entrythread.start();
    }
    
    
})();