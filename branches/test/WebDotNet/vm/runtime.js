
function Call(args, code, assembly)
{
	var locals = [];
	var stack = [];
	var ip = 0;
	var exit = false;
	var ip = 0;
	var push = function(value) { stack.push(value); };
	var pop = function() { return stack.pop(); };
	var goto = function(index) { ip = index - 1; };
	var ret = function() { exit = true; };
	
	var debug = function()
	{
		console.debug({"stack":stack.slice(), "locals": locals.slice(), ip:ip});
	}
	
	this.run = function()
	{
		for(; !exit; ip++)
		{
			debug();
			cil[code[ip][0]](push,pop, goto,locals, args,ret,assembly, code[ip][1]);
		}
		debug();
	}
}
 
var f = [ alert, ["message(future may be most)"], 0 /*zero type is void*/]


var c = new Call([1,[[12],0]], [[1,0],[114,0],[2, 0],[15,1],[114,0],[42,0]], ["constante", f]);

c.run();

//Reference = [ context_array, array_index  ]
//Method = [  real_function, parameters_array, return_type ]