using System;


namespace System.InteropBrowser
{
	public sealed class NativeMethod : Attribute { public NativeMethod(string code = "") { } }

	public sealed class Javascript
	{
		//El constructor es privado apra que no se puedan crear instancias
		private Javascript()
		{

		}

		[NativeMethod("Aca va el codigo nativo")]
		//Obtiene una instancia del objeto superglobal
		public static Javascript Global { get { return default(Javascript); } }

		[NativeMethod("Aca va el codigo nativo")]
		//Llama a un metodo de determinado objeto
		public Javascript Call (string method, params Javascript[] arguments) { return default(Javascript);	}

		[NativeMethod("Aca va el codigo nativo")]
		//Crea un string javascript a partir de uno de .net
		public static explicit operator Javascript(string source) { return default(Javascript);	}
	}
}