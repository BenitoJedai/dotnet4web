using System;


namespace System.Runtime.InteropServices.WebBrowser
{
	public sealed class Object
	{
		//El constructor es privado apra que no se puedan crear instancias
		private Object()
		{

		}

		//Obtiene una instancia del objeto superglobal
		public static Object Global { get { return default(Object); } }

		//Llama a un metodo de determinado objeto
		public Object Call (string method, params Object[] arguments) { return default(Object);	}

		//Crea un string javascript a partir de uno de .net
		public static explicit operator Object(string source) { return default(Object);	}
	}
}