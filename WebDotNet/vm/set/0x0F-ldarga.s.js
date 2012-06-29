//Una referencia es una array donde el elemento 0 es 
//el array donde esta el valor y el segundo el indice 
//donde esta el valor.
//por ejemplo si yo tengo una conjunto=> local = [ 1, 2, 3]
//para tener una referencia al segundo elemento tenemos=>  [local,1]

var ref = args[operand];
push(ref[0][ref[1]]);