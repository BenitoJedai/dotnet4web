(function(){ "use strict";
  
var PseudoOpcodes = {
  Const:0,
  Store:1,
  Load:2,
  Call:3,
  Return:4
}

var BuiltinTypes = {
  String:[]
}
  


//Metodo alert()
var other = [
  [PseudoOpcodes.Return, false]
];

//Metodo main
var code = [
  [PseudoOpcodes.Const, BuiltinTypes.String, "Hello world!!!"],
  [PseudoOpcodes.Call, 1],
  [PseudoOpcodes.Return, false]
];




function Exec(code)
{
  this.stack = [];
  this.vars = [];
  this.code = code;
  this.ip = 0;
  this.calls = [];
}

Exec.prototype.run = function()
{
  var current;
  for(var i = 0; current = this.code[i]; i++)
  {
    switch(current[0])
    {
      //Load constant(1:type, 2:value)
      case 0x00:
        this.stack.push([current[1], current[2]]);
        break;
      case 0x03:
        break;
      case 0x04:
        break;
      default:
        throw new Error("Invalid pseudo instruction code: " + current[0]);
    }
  }
}


var x = new Exec(code);
x.run();










  
  
}());