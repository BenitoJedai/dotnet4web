using System;
using System.Runtime.InteropServices.WebIntegration;

//http://www.w3.org/TR/2000/WD-DOM-Level-1-20000929/java-language-binding.html

namespace Org.W3C
{
	public static class Window
	{
		public static void Alert(string message)
		{
			JavascriptObject.Global.Call("alert", message);
		}
	}
}

