using System;
using System.Runtime.InteropServices.WebIntegration;

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

