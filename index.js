(function(){ "use strict";
  
var PseudoOpcodes = {
  Const:0,
  Store:1,
  Load:2,
  Call:3,
  Return:4,
  NativeCall:5,
  NewArray:6
}

var BuiltinTypes = {
  String:[],
  Int32:[],
  Array:[]
}

var JavascriptInteropType = [];
  
//Metodos javascript nativos
var JavascriptInterop = {
  get_Window:function()
  {
    this.stack.push([JavascriptInteropType, window]);
  }
};


//Metodo alert()
var other = [
  [PseudoOpcodes.NativeCall, JavascriptInterop.get_Window],
  [PseudoOpcodes.Const, BuiltinTypes.String, "alert"],
  [PseudoOpcodes.Const, BuiltinTypes.Int32, 1],
  [PseudoOpcodes.NewArray, BuiltinTypes.JavascriptInteropType],
  [PseudoOpcodes.Return, false]
];

//Metodo main
var code = [
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

Exec.prototype.run = function()
{
  while(true)
  {
    var current = this.code[this.ip];
    switch(current[0])
    {
      //Load constant(1:type, 2:value)
      case 0x00:
        this.stack.push([current[1], current[2]]);
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
        debugger;
        var toremove = this.stack.pop();
        this.stack.push([BuiltinTypes.Array, current[1], this.stack.splice(this.stack.length - toremove, toremove)]);
        break;
      default:
        throw new Error("Invalid pseudo instruction code: " + current[0]);
    }
    this.ip++;
  }
}


var x = new Exec(code, function() {  console.info("termino"); });
x.run();










  
  
}());