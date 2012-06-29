
var f = assembly[operand];

var a = [];
for(var i  = 0; i < f[1].length; i++)
	a.push(pop());
	
var rv = f[0](a);

if(f[2] != 0)
	push(rv);
	
