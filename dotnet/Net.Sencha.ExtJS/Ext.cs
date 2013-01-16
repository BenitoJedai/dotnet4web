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

			var link = Org.W3C.Window.Document.CreateElement("link");
			link.SetAttribute("rel", "stylesheet");
			link.SetAttribute("type", "text/css");
			link.SetAttribute("href", "/extjs/resources/css/ext-all.css");
			Org.W3C.Window.Document.Head.AppendChild(link);

			var script = Org.W3C.Window.Document.CreateElement("script");
			script.SetAttribute("src","/extjs/ext-all.js");
			script.SetAttribute("type","text/javascript");
			script.AddEventListener("load", kk, false);
			Org.W3C.Window.Document.Head.AppendChild(script);

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


		public static class MessageBox
		{
			[MethodImplAttribute(MethodImplOptions.InternalCall)]
			public static extern void Confirm(string title, string message, Action<object> cb);
		}

		public class Base
		{
		}


		public class AbstractComponent
		{
			[MethodImplAttribute(MethodImplOptions.InternalCall)]
			public extern AbstractComponent();

			[MethodImplAttribute(MethodImplOptions.InternalCall)]
			public extern void Show();
		}

		public class Window : container.Container
		{
			[MethodImplAttribute(MethodImplOptions.InternalCall)]
			public extern Window(WindowOptions options);

		}

		public class Button : AbstractComponent
		{
			[MethodImplAttribute(MethodImplOptions.InternalCall)]
			public extern Button();
		}

		public static class container
		{
			public class AbstractContainer : AbstractComponent
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				public extern layout.Layout GetLayout();
			}

			public class Container : AbstractContainer
			{
				[MethodImplAttribute(MethodImplOptions.InternalCall)]
				public extern void Add(AbstractComponent w);
			}
		}

		public static class layout {
			public class Layout {
				internal Layout() {
				}

				[MethodImpl(MethodImplOptions.InternalCall)]
				public extern void SetConfig(string val);
			}
		}

		public struct WindowOptions {
			private string layout;
			public string Layout {
				set {
					this.layout = value;
				}
			}
		}

	}
}