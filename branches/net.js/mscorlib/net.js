(function () {
	"use strict";

    function Reader(uri, callback) {
        var xhr = new XMLHttpRequest();
        xhr.open("GET", uri, true);
        xhr.responseType = "arraybuffer";
        xhr.send(null);
        xhr.onreadystatechange = (function () {
            if (xhr.readyState == 4) {
                this.view = new DataView(xhr.response);
                this.ip = 0;
                callback(this);
            }
        }).bind(this);
    }

    Reader.prototype.readByte = function (endian) {
        var rv = this.view.getUint8(this.ip, !endian);
        this.foward(1);
        return rv;
    };

    Reader.prototype.readWord = function (endian) {
        var rv = this.view.getUint16(this.ip, !endian);
        this.foward(2);
        return rv;
    };

    Reader.prototype.readDoubleWord = function (endian) {
        var rv = this.view.getUint32(this.ip, !endian);
        this.foward(4);
        return rv;
    };

    Reader.prototype.move = function (offset) {
        this.ip = offset;
    };

    Reader.prototype.foward = function (count) {
        this.ip += count;
    };


    function Parser(uri, callback) {
        new Reader(uri, function (reader) {
            var data = {};

            //Me muevo a la cabecera .NET
            reader.move(0x168);
            reader.move((reader.readDoubleWord() % 0x2000) + 520);

            //Me muevo a los metadatos .NET
            reader.move((reader.readDoubleWord() % 0x2000) + 524);


            //Indice base de todas las cabeceras .NET
            var base = reader.ip - 12;


            //Obtengo la version de .NET del ensamblado
            data.version = (function () {

                var vlength = reader.readDoubleWord();
                var auxarray = [];
                for (var i = 0; i < vlength; i++) {
                    var code = reader.readByte();

                    if (code == 0)
                        continue;

                    auxarray.push(String.fromCharCode(code));
                }
                reader.foward(2);
                return auxarray.join("");
            }());


            (function () {

                var streams = {};
                var scount = reader.readWord();

                for (var i = 0; i < scount; i++) {
                    (function () {

                        var offset = reader.readDoubleWord();
                        reader.foward(4);

                        var aux = [];

                        while (true) {
                            var current = reader.readByte();
                            if (current == 0) {
                                break;
                            } else {
                                aux.push(String.fromCharCode(current));
                            }
                        }

                        streams[aux.join("").substring(1)] = offset + base;
                        reader.ip = Math.ceil(reader.ip / 4) * 4;

                    }());
                }

                reader.streams = streams;

            }());


            reader.move(reader.streams["~"] + 6);

            (function () {

                var heapSizes = reader.readByte();
                reader.heapSizes = {
                    string: heapSizes & 0x01,
                    guid: heapSizes & 0x02,
                    blob: heapSizes & 0x04,
                }


            }());


            reader.foward(1);

            var valid = [];

            (function () {
                for (var i = 0; i < 8; i++) {
                    var b = reader.readByte();
                    for (var j = 0; j < 8; j++) {
                        valid.push(((b >> j) & 1) == 1);
                    }
                }

                reader.foward(8);

                for (var i = 0; i < valid.length; i++) {
                    if (valid[i]) {
                        valid[i] = reader.readDoubleWord();
                    } else {
                        valid[i] = 0;
                    }
                }

                reader.valid = valid;

                for (var i = 0; i < valid.length; i++) {


                    var target = Reader.relations[i];

                    if (!target || valid[i] == 0)
                        continue;

                    var mname = "read" + target;
                    var target = target[0].toLowerCase() + target.substring(1);


                    if (!data[target]) {
                        data[target] = [];
                    }

                    target = data[target];



                    for (var j = 0; j < valid[i]; j++) {
                        target.push(reader[mname]());
                    }

                };
            }());


            callback(data);
        });
    };



    Reader.prototype.readModule = function () {
        this.foward(2);
        return {
            name: this.readNetString(),
            mvid: this.readNetGuid(),
            encId: this.readNetGuid(),
            baseEncId: this.readNetGuid()
        };
    };

    Reader.prototype.readTypeRef = function () {
        return {
            resolutionScope: this.readNetIndex("Module", "ModuleRed", "AssemblyRef", "TypeRef"),
            typeName: this.readNetString(),
            typeNamespace: this.readNetString()
        };
    };

    Reader.prototype.readField = function () {
        return {
            flags: this.readWord(),
            name: this.readNetString(),
            signature: this.readFieldSignature()
        };
    };

    Reader.prototype.readFieldSignature = function () {

        var offset = this.readHeapOffset("blob");
        offset += this.streams.Blob;
        var backup = this.ip;
        this.move(offset);

        var size = this.readCompressedNumber();

        var start = this.ip;

        var prolog = this.readCompressedNumber();




        var rv = {
            fieldType: this.readSignatureConstant()
        };


        if (this.ip < size + start) {
            throw new Error("Los atributos personalizados de los campos no estan implementados aun");
        }



        this.ip = backup;
        return rv;

    };


    Reader.prototype.readConstant = function () {
        return {
            type: this.readWord(),
            parent: this.readNetIndex("Param", "Field", "Property"),
            value: this.readHeapOffset("blob")
        };
    };

    Reader.prototype.readEventMap = function () {
        return {
            parent: this.readNetIndex("TypeDef"),
            eventList: this.readWord()
        };
    };

    Reader.prototype.readEvent = function () {
        return {
            flags: this.readWord(),
            name: this.readNetString(),
            eventType: this.readNetIndex("TypeDef", "TypeRef", "TypeSpec")
        };
    };

    Reader.prototype.readPropertyMap = function () {
        return {
            parent: this.readNetIndex("TypeDef"),
            propertyList: this.readWord()
        };
    };

    Reader.prototype.readProperty = function () {
        return {
            flags: this.readWord(),
            name: this.readNetString(),
            type: this.readHeapOffset("blob")
        };
    };


    Reader.prototype.readMethodSemantics = function () {
        return {
            semantics: this.readWord(),
            method: this.readNetIndex("MethodDef"),
            association: this.readNetIndex("Event", "Property")
        };
    };

    Reader.prototype.readInterfaceImpl = function () {
        return {
            "class": this.readNetIndex("TypeDef"),
            "interface": this.readNetIndex("TypeDef", "TypeRef", "TypeSpec")
        };
    };

    Reader.prototype.readGenericParam = function () {
        return {
            number: this.readWord(),
            flags: this.readWord(),
            owner: this.readNetIndex("TypeDef", "MethodDef"),
            name: this.readNetString()
        };
    };


    Reader.prototype.readTypeDef = function () {
        return {
            flags: this.readDoubleWord(),
            name: this.readNetString(),
            namespace: this.readNetString(),
            extends: this.readNetIndex("TypeDef", "TypeRef", "TypeSpec"),
            fieldList: this.readNetIndex("Field"),
            methodList: this.readNetIndex("MethodDef")
        };
    }

    Reader.prototype.readAssemblyRef = function () {
        return {
            majorVersion: this.readWord(),
            minorVersion: this.readWord(),
            buildNumber: this.readWord(),
            revisionNumber: this.readWord(),
            flags: this.readDoubleWord(),
            publicKeyOrToken: this.readHeapOffset("blob"),
            name: this.readNetString(),
            culture: this.readNetString(),
            hashValue: this.readHeapOffset("blob")

        };
    };



    Reader.prototype.readParam = function () {
        return {
            flags: this.readWord(),
            sequence: this.readWord(),
            name: this.readNetString()
        };
    };

    Reader.prototype.readAssembly = function () {
        return {
            hashAlgId: this.readDoubleWord(),
            majorVersion: this.readWord(),
            minorVersion: this.readWord(),
            buildNumber: this.readWord(),
            revisionNumber: this.readWord(),
            flags: this.readDoubleWord(),
            publicKey: this.readHeapOffset("blob"),
            name: this.readNetString(),
            culture: this.readNetString()
        };
    };

    Reader.prototype.readClassLayout = function () {
        return {
            packingSize: this.readWord(),
            classSize: this.readDoubleWord(),
            parent: this.readNetIndex("TypeDef")
        };
    };
    
    Reader.prototype.readMethodBody = function() {
    
    	var rv = this.readDoubleWord();
    	
    	
    	return rv == 0 ? null : rv;
    
    };

    Reader.prototype.readMethodDef = function () {
        var rv = {
            rva: this.readMethodBody(),
            implFlags: this.readWord(),
            flags: this.readWord(),
            name: this.readNetString(),
            signature: this.readMethodDefSignature(),
            paramList: this.readNetIndex("Param")
        };
        
        return rv;
    };

    Reader.prototype.readSignatureConstant = function () {

        var constant = this.readCompressedNumber();

        if ((constant > 0 && constant < 0x0F) || constant == 0x1C || constant == 0x18 || constant == 0x19) {
            return {
                table: "coreTypes",
                index: constant
            };
        } else if (constant == 0x13) {
            return {
                table: "OwnerTypeGeneric",
                index: this.readCompressedNumber()
            }
        } else if (constant == 0x12 || constant == 0x11) {

            var dato = this.readCompressedNumber();

            return {
                table: ["TypeDef", "TypeRef"][dato & 1],
                index: (dato >> 2) - 1
            };



        } else {

            throw new Error("La constante " + constant.toString(16) + " no esta implementada");
        }


    };

    Reader.prototype.readMethodDefSignature = function () {


        var offset = this.readHeapOffset("blob");
        offset += this.streams.Blob;
        var backup = this.ip;
        this.move(offset);

        var size = this.readCompressedNumber();

        var current = this.readCompressedNumber();

        var rv = {
            hasThis: (current & 0x20) != 0,
            explicitThis: (current & 0x40) != 0,
            varargs: (current & 0x05) != 0,
            isGeneric: (current & 0x10) != 0
        }

        if (rv.isGeneric) {
            rv.genericCount = this.readCompressedNumber();
        }

        rv.paramCount = this.readCompressedNumber();

        rv.returnType = this.readSignatureConstant();

        rv.parametersTypes = [];

        for (var i = 0; i < rv.paramCount; i++) {
            rv.parametersTypes.push(this.readSignatureConstant());
        }

        this.ip = backup;
        return rv;

    };

    Reader.prototype.readMemberRef = function () {
        return {
            "class": this.readNetIndex("TypeRef", "ModuleRef", "MethodDef", "TypeSpec", "TypeDef"),
            name: this.readNetString(),
            signature: this.readHeapOffset("blob")
        };
    };


    Reader.prototype.readStandAloneSig = function () {
        return {
            signature: this.readHeapOffset("blob")
        };
    };

    Reader.prototype.readNestedClass = function () {
        return {
            nestedClass: this.readNetIndex("TypeDef") - 1,
            enclosingClass: this.readNetIndex("TypeDef") - 1
        };
    };

    Reader.prototype.readNetString = function () {
        var start = this.readHeapOffset("string");
        var backup = this.ip;
        this.ip = this.streams.Strings + start;

        var aux = [];

        while (true) {
            var char = this.readByte();
            if (char == 0) {
                break;
            } else {
                aux.push(String.fromCharCode(char));
            }
        }

        this.ip = backup;
        return aux.join("");
    };


    Reader.prototype.readNetGuid = function () {
        var start = this.readHeapOffset("guid");
        if (start == 0) {
            return null;
        };

        var backup = this.ip;
        this.ip = this.streams.GUID + start - 1;

        var aux = "";
        for (var i = 0; i < 16; i++) {
            var hex = this.readByte().toString(16);
            aux += (hex.length == 1 ? "0" : "") + hex;
        }

        this.ip = backup;
        return aux;
    };


    Reader.prototype.readCompressedNumber = function () {

        var current = this.view.getUint8(this.ip);

        if (current == 0xFF) {
            this.ip++;
            return null;
        };

        if ((current >> 7) == 0) {
            this.ip++;
            return current;
        }

        current = this.view.getUint16(this.ip, false);

        if ((current >> 14) == 2) {
            this.ip += 2;
            return current & 0x3FFF;
        }

        current = this.view.getUint32(this.ip, false);
        this.ip += 4;
        return current & 0x1FFFFFFF;

    };



    Reader.prototype.readNetIndex = function () {


        var names = Array.prototype.slice.call(arguments, 0);
        var id = names.join("Or");

        if (!this.cache)
            this.cache = {};

        if (!this.cache[id]) {
            var indexSize = 0;
            while (Math.pow(2, indexSize) < arguments.length)
                indexSize++;

            var maxvalue = 0xFFFF >> indexSize

            var isBig = false;

            for (var i = 0; i < arguments.length; i++) {

                if (this.valid[Reader.relations.indexOf(arguments[i])] > maxvalue) {
                    isBig = true;
                    break;
                }
            }


            this.cache[id] = function () {

                var all = isBig ? this.readDoubleWord() : this.readWord();

                if (names.length == 1)
                    return all == 0 ? null : all;

                var table = names[((1 << indexSize) - 1) & all];
                table = table[0].toLowerCase() + table.substring(1);

                var aux = {
                    index: (all >> indexSize) - 1,
                    table: table
                }

                return aux.index == -1 ? null : aux;

            }
        }

        return this.cache[id].call(this);
    }

    Reader.prototype.readHeapOffset = function (type) {
        return this[this.heapSizes[type] == 1 ? "readDoubleWord" : "readWord"]();
    };

    Reader.prototype.readCustomAttribute = function () {
        return {
            parent: this.readNetIndex("MethodDef", "FieldDef", "TypeRef", "TypeDef",
                "paramDef", "InterfaceImpl", "MemberRef", "Module", "Permission",
                "Property", "Event", "StandAloneSig", "ModuleRef", "TypeSpec",
                "Assembly", "AssemblyRef", "File", "ExportedType"),
            type: this.readNetIndex("MethodDef", "MethodRef"),
            value: this.readHeapOffset("blob")
        };
    };


    Reader.relations = [
        "Module", "TypeRef", "TypeDef", null, "Field", null, "MethodDef", null, "Param", "InterfaceImpl", "MemberRef",
        "Constant", "CustomAttribute", "FieldMarshal", "DeclSecurity", "ClassLayout", "FieldLayout", "StandAloneSig", "EventMap", null, "Event", "PropertyMap",
        null, "Property", "MethodSemantics", "MethodImpl", "ModuleRef", "TypeSpec", "ImplMap", "FieldRVA", null, null, "Assembly",
        "AssemblyProcessor", "AssemblyOS", "AssemblyRef", "AssemblyRefProcessor", "AssemblyRefOS", "File", "ExportedType", "ManifestResource",
        "NestedClass", "GenericParam", null, "GenericParamConstraint"
    ]



    function Domain(path, callback) {
        this.assemblies = {};
        this.load(path, callback);

    }


    Domain.prototype.load = function (path, callback) {
        new Parser(path, function (data) {
            
            new Assembly(this, data, function (assembly) {
				console.debug("Parseado el ensamblado " + path);
                callback(this);

            }.bind(this));

        }.bind(this));
    };

    function Assembly(domain, metadata, callback) {
        this.name = metadata.assembly[0].name;
        this.types = {};
        this.domain = domain;
        var self = this;

        this.domain.assemblies[this.name] = this;

        //Cuando se cargaron todas las dependencias
        var finalize = function () {

            //Reemplazo el indice de ensamblado por la instancia real
            metadata.assembly[0] = self;

            //Si tiene referencias a ensamblados
            if ("assemblyRef" in metadata) {
                //Cambio todos los assemblyref por ensamblados reales
                for (var i = 0; i < metadata.assemblyRef.length; i++) {
                    metadata.assemblyRef[i] = self.domain.assemblies[metadata.assemblyRef[i].name];
                }
            }

            //Si se crearon tipos (?? igual pasa siempe)
            if ("typeDef" in metadata) {
                //Cambios todos los typedef por tipos reales

                for (var i = 0; i < metadata.typeDef.length; i++) {
                    var orig = metadata.typeDef[i];

                    var declaringType = null;

                    //Busco si es un nesetd type
                    if ("nestedClass" in metadata) {
                        for (var j = 0; j < metadata.nestedClass.length; j++) {
                            if (metadata.nestedClass[j].nestedClass == i) {
                                declaringType = metadata.nestedClass[j].enclosingClass;
                            }
                        }
                    }

                    metadata.typeDef[i] = new Type(self, orig.namespace, orig.name, declaringType, orig["extends"], orig.methodList);
                    declaringType = null;
                    orig = null;
                }

                //Agrego cado uno de los tipos a su declaringType si tiene, sino a su ensamblado
                for (var i = 0; i < metadata.typeDef.length; i++) {
                    var t = metadata.typeDef[i];
                    if (t.declaringType == null) {
                        if (!(t.namespace in self.types)) {
                            self.types[t.namespace] = new(function Namespace() {})();
                        }

                        self.types[t.namespace][t.name] = t;
                    } else {
                        t.declaringType = metadata.typeDef[t.declaringType];
                        t.declaringType.types[t.name] = t;
                    }
                    t = null;
                }
            }


			//Cargando los tipos del nucleo de .net
			if(self.name == "mscorlib") {
				
				domain.coreTypes = [
					self.getType("System", "Void"),
					self.getType("System", "Boolean"),
					self.getType("System", "Char"),
					self.getType("System", "SByte"),
					self.getType("System", "Byte"),
					self.getType("System", "Int16"),
					self.getType("System", "UInt16"),
					self.getType("System", "Int32"),
					self.getType("System", "UInt32"),
					self.getType("System", "Int64"),
					self.getType("System", "UInt64"),
					self.getType("System", "Single"),
					self.getType("System", "Double"),
					self.getType("System", "String"),
					self.getType("System", "Boolean")
				];
			}
			

            //Cambio todos los typeref por los typeref de los otros modulos

            if ("typeRef" in metadata) {
                for (var i = 0; i < metadata.typeRef.length; i++) {
                    var ref = metadata.typeRef[i];
                    metadata.typeRef[i] = metadata[ref.resolutionScope.table]
                    [ref.resolutionScope.index].getType(ref.typeNamespace, ref.typeName);
                }
            }

            //Cambio todos los basetype por lo tipos reales

            if ("typeDef" in metadata) {
                for (var i = 0; i < metadata.typeDef.length; i++) {
                    var def = metadata.typeDef[i];
                    if (def.baseType == null)
                        continue;
                    def.baseType = metadata[def.baseType.table][def.baseType.index];
                    def = null;
                }
            }
//
            //Reemplazo los metodDef por instancias de metodo reales
            if ("methodDef" in metadata) {
                for (var i = 0; i < metadata.methodDef.length; i++) {
                    var mdef = metadata.methodDef[i];
                    metadata.methodDef[i] = new Method(mdef.name, mdef.signature, mdef.paramList, mdef.rva);
                    mdef = null;
                }
            }
            
            //Reemplazo los param por instancias de parametros reales
            if ("param" in metadata) {
                for (var i = 0; i < metadata.param.length; i++) {
                    var par = metadata.param[i];
                    metadata.param[i] = new Parameter(par.name);
                    par = null;
                }
            }

			//Metodo magico de marcelo que seleciona las listas por indices
            var replacelist = function (master, slave, reason, reverse) {

                if (metadata[master] && metadata[slave]) {
                    var methods = metadata[slave];
                    var lastMethod = methods.length;

                    for (var j = metadata[master].length - 1; j >= 0; j--) {
                        var type = metadata[master][j];

                        var newml = [];
                        if (type[reason] - 1 != lastMethod) {
                            newml = methods.slice(type[reason] - 1, lastMethod);
                            lastMethod = type[reason] - 1;
                        }

                        type[reason] = newml;

                        for (var k = 0; k < newml.length; k++)
                            newml[k][reverse] = type;
                    }
                } else if (metadata[master]) {
                    for (var i = 0; i < metadata[master].length; i++) {
                        metadata[master][i][reason] = [];
                    }
                }

            }

            replacelist("typeDef", "methodDef", "methods", "declaringType");
            replacelist("methodDef", "param", "parameters", "declaringMethod");

			//Reemplazo las firmas de los metodos por los datos utiles

			if("methodDef" in metadata) {
				for(var i = 0; i < metadata.methodDef.length; i++) {
				
					//Sus tipos de retornos
					var mdef = metadata.methodDef[i];
					var rdef = mdef.signature.returnType;
					rdef = { table : rdef.table[0].toLowerCase() + rdef.table.substring(1), index: rdef.index };
					if(rdef.table == "coreTypes") {
						mdef.returnType = domain.coreTypes[rdef.index - 1];					
					} else { 
						mdef.returnType = metadata[rdef.table][rdef.index];
					}
					
					//Sus tipos de parametros
					for(var j = 0; j < mdef.signature.parametersTypes.length; j++) {
						var partype = mdef.signature.parametersTypes[j];
						partype = { table : partype.table[0].toLowerCase() + partype.table.substring(1), index: partype.index };
						if(partype.table == "coreTypes") {
							mdef.parameters[j].parameterType = domain.coreTypes[partype.index - 1];					
						} else { 
							mdef.parameters[j].parameterType = metadata[partype.table][partype.index];
						}
					}
					
					
					delete mdef.signature;
					mdef = null;
				}
			}
			
			//Creo los cuerpos de los metodos

			if("methodDef" in metadata) {
				for(var i = 0; i < metadata.methodDef.length; i++) {
				
					var methodDef = metadata.methodDef[i];
					var body = methodDef.body;
					
					//Si no tiene cuerpo se continua sin parsear
					if(body == null)
						continue;
					
					

					console.debug(methodDef);
				}
			}

            metadata = null;
            callback(self);
        };


        var loader, i = 0;

        loader = function () {

            //Ya estan todas sus referencias
            if (i == metadata.assemblyRef.length) {
                finalize();
                return;
            }

            var name = metadata.assemblyRef[i].name;


            i++;
            //Si el modulo no esta cargado, lo carga
            if (name in self.domain.assemblies) {
                //Incrementa para el llamado al proximo modulo
                loader();
            } else {
                self.domain.load(name + ".dll", function () {
                    loader()
                });
            }
        };


        if ("assemblyRef" in metadata)
            loader();
        else
            finalize();
    }

    Assembly.prototype.getType = function (namespace, name) {
        return this.types[namespace][name];
    };
    
    function Type(assembly, namespace, name, declaringType, baseType, methods) {
        this.assembly = assembly;
        this.name = name;
        this.namespace = namespace;
        this.declaringType = declaringType;
        this.types = {};
        this.baseType = baseType;
        this.methods = methods;
        this.genericArguments = [];
    }

    function Method(name, signature, parameters, body) {
        this.name = name;
        this.signature = signature;
        this.parameters = parameters;
        this.genericArguments = [];
        this.body = body;
    };

	function Parameter(name) {
		this.name = name;
		this.parameterType = null;
	};

	function Generic(name) {
		this.name;
	};


    window.onload = function () {
        var uri = document.getElementById("3436ff6a-c82c-4e0a-b7fa-12d104074de3").getAttribute("main");
        new Domain(uri, function (domain) {
            console.debug(domain);
        });
    };

}());