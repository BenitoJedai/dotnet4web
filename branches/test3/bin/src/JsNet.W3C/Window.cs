using System.Runtime.InteropServices.WebBrowser;

namespace JsNet.W3C
{
	public static class Window
	{
		public static void Alert(string message)
		{
			Object.Global.Call("alert", (Object) message);
		}
	}
}