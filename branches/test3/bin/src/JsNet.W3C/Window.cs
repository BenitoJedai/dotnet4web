using System;
using System.InteropBrowser;

namespace JsNet.W3C
{
	public static class Window
	{
		public static void Alert(string message)
		{
			Javascript.Global.Call("alert", (Javascript) message);
		}
	}
}