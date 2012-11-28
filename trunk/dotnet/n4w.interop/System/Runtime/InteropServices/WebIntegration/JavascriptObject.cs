using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.WebIntegration
{
	public class JavascriptObject
	{
		private JavascriptObject ()
		{
		}


		public extern static JavascriptObject Global {
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public JavascriptObject Call(string name, params JavascriptObject[] arguments);

		[MethodImpl(MethodImplOptions.InternalCall)]
		extern public static implicit operator JavascriptObject (string value);
	}
}

