using System;

namespace System.Runtime.InteropServices.WebIntegration
{
	public class JavascriptObject
	{
		private JavascriptObject ()
		{
		}

		public static JavascriptObject Global {
			get {
				return default(JavascriptObject);
			}
		}

		public JavascriptObject Call(string name, params JavascriptObject[] arguments)
		{
			return default(JavascriptObject);
		}

		public static implicit operator JavascriptObject (string value)
		{
			return default(JavascriptObject);
		}
	}
}

