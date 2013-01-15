using System;
using Org.W3C;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Net.Sencha.ExtJS
{
	public class Ext
	{
		private static AutoResetEvent mutex;
		public static void Initialize ()
		{
			mutex = new AutoResetEvent(false);

			var link = Window.Document.CreateElement("link");
			link.SetAttribute("rel", "stylesheet");
			link.SetAttribute("type", "text/css");
			link.SetAttribute("href", "/extjs/resources/css/ext-all.css");
			Window.Document.Head.AppendChild(link);

			var script = Window.Document.CreateElement("script");
			script.SetAttribute("src","/extjs/ext-all.js");
			script.SetAttribute("type","text/javascript");
			script.AddEventListener("load", kk, false);
			Window.Document.Head.AppendChild(script);

			mutex.WaitOne();

		}

		private static void kk(object o)
		{
			OnReady(OnExtJsReady);
		}

		private static void OnExtJsReady(object o)
		{
			mutex.Set();
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		public static extern void OnReady(Action<object> cb);
	}
}