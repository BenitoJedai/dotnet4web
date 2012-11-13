(function(){ "use strict";
  
var PseudoOpcodes = {
  Const:0,
  Store:1,
  Load:2,
  Call:3,
  Return:4,
  NativeCall:5,
  NewArray:6,
  Duplicate:7,
  SetIndex:8,
  Pop:9
}

var typeType = [undefined, "Type"];
typeType[0] = typeType;

var BuiltinTypes = {
  String:[typeType,"String"],
  Int32:[typeType, "Int32"],
  Array:[typeType, "Array"],
  Type:typeType
}

//Type
//0: Referencia al tipo typeType
//1: Nombre del tipo
var JavascriptInteropType = [BuiltinTypes.Type, "Javascript"];
  
//Metodos javascript nativos
var JavascriptInterop = {
  get_Window:function()
  {
    this.stack.push([JavascriptInteropType, window]);
  },
  Javascript_from_string:function()
  {
    this.stack.push(this.stack.pop()[1].toString());
  },
  Call:function()
  {
    var params = this.stack.pop()[2];
    var mname = this.stack.pop()[1];
    var instance = this.stack.pop()[1];
    this.stack.push([JavascriptInteropType, instance[mname].apply(instance, params)]);
  }
};

//Metodos nativos de la corelib
var CorelibMethod = {
  Thread_Sleep:function()
  {
    this.pause();

    window.setTimeout((function(){ 
      
      this.run(); 
      
      
    }).bind(this),this.stack.pop()[1]);
  }
};

//Metodo alert()
var other = [
  [PseudoOpcodes.NativeCall, JavascriptInterop.get_Window],
  [PseudoOpcodes.Const, BuiltinTypes.String, "alert"],
  [PseudoOpcodes.Const, BuiltinTypes.Int32, 1],
  [PseudoOpcodes.NewArray, BuiltinTypes.JavascriptInteropType],
  [PseudoOpcodes.Duplicate],
  [PseudoOpcodes.Const, BuiltinTypes.Int32, 0],
  [PseudoOpcodes.Load, 0],
  [PseudoOpcodes.NativeCall, JavascriptInterop.Javascript_from_string],
  [PseudoOpcodes.SetIndex],
  [PseudoOpcodes.NativeCall, JavascriptInterop.Call],
  [PseudoOpcodes.Pop],
  [PseudoOpcodes.Return, false]
];

//Metodo main
var code = [
  [PseudoOpcodes.Const, BuiltinTypes.Int32, 1000],
  [PseudoOpcodes.NativeCall, CorelibMethod.Thread_Sleep],
  [PseudoOpcodes.Const, BuiltinTypes.String, "Hello world!!!"],
  [PseudoOpcodes.Call, other, 1],
  [PseudoOpcodes.Return, false]
];




function Exec(code, cb)
{
  this.stack = [];
  this.vars = [];
  this.code = code;
  this.ip = 0;
  this.calls = [];
  this.cb = cb;
}

Exec.prototype.save = function()
{
  this.calls.push({
    stack:this.stack,
    vars:this.vars,
    code:this.code,
    ip:this.ip
  });
}

Exec.prototype.restore = function()
{
  var prev = this.calls.pop();
  this.stack = prev.stack;
  this.vars = prev.vars;
  this.code = prev.code;
  this.ip = prev.ip;
}

Exec.prototype.pause = function()
{
  this.stoped = true;
}

Exec.prototype.run = function()
{
  this.stoped = false;
  while(true)
  {
    var current = this.code[this.ip];
    switch(current[0])
    {
      //Load constant(1:type, 2:value)
      case 0x00:
        this.stack.push([current[1], current[2]]);
        break;
      case 0x02:
        this.stack.push(this.vars[current[1]]);
        break;
      //Call(1:code, 2:argcount)
      case 0x03:
        this.save();
        this.vars = this.stack.splice(this.stack.length - current[2], current[2]);
        this.stack = [];
        this.ip = -1;
        this.code = current[1];
        break;
      //Return(1:hasreturn)
      case 0x04:
        //Finaliza la ejecucion
        if(this.calls.length == 0)
        {
          this.cb(current[1] ? this.stack.pop() : null);
          return;
        }
        //Si tiene que devolver parametros lo pone en la pila de afuera        
        if(current[1])
          this.calls[this.calls.length - 1].stack.push(this.stack.pop());
        //Reestablece el contexto anterior
        this.restore();
        break;
      //NativeCall(1:function)
      case 0x05:
        current[1].call(this);
        break;
      case 0x06:
        this.stack.push([BuiltinTypes.Array, current[1], new Array(this.stack.pop()[1])]);
        break;
      case 0x07:
        this.stack.push(this.stack[this.stack.length - 1]);
        break;
      case 0x08:
        var value = this.stack.pop();
        var index = this.stack.pop();
        var array = this.stack.pop();
        array[2][index[1]] = value;
        break;
      case 0x09:
        this.stack.pop();
        break;
      default:
        throw new Error("Invalid pseudo instruction code: " + current[0]);
    }
    this.ip++;
    if(this.stoped)
      return;
  }
}


var x = new Exec(code, function() {  console.info("termino"); });
x.run();










  
  
}());