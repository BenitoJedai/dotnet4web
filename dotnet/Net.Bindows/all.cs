

using System.Runtime.CompilerServices;
using System;
using Org.W3C;

namespace Net.Bindows
{
	public class BiWindow : BiComponent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern BiWindow();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern BiComponent GetContentPane();


		public BiComponent ContentPane {
			get {
				return this.GetContentPane();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetCaption(string color);

		public string Caption { set { this.SetCaption(value); } }

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetIcon(BiImage color);

		public BiImage Icon { set { this.SetIcon(value); } }

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Close();

	}

	public class BiButton : BiComponent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern BiButton();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHtml(string html);

		public string HTML { set { this.SetHtml(value); } }

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetLeft(int value);

		public int Left { set { this.SetLeft(value); } }


	}

	public class BiComponent : BiEventTarget
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern BiComponent();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Add(BiComponent newChild);


		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBackColor(string color);

		public string BackColor { set { this.SetBackColor(value); } }

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetEnabled(bool value);
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool GetEnabled();

		public bool Enabled {
			set { this.SetEnabled (value); }
			get { return this.GetEnabled(); } }


	}

	public static class Application
	{
		private static Action<object> cb;
		private static string root;
		private static string adf;
		public static void Initialize(Action<object> cb, string root, string adf)
		{
			System.Threading.Thread.Sleep(1000);
			Application.cb = cb;
			Application.root = root;
			Application.adf = adf;

			var biscript = Org.W3C.Window.Document.CreateElement("script");
			biscript.SetAttribute("src","/bindows/html/js/application.js");
			biscript.SetAttribute("type","application/javascript");
			Org.W3C.Window.Document.Head.AppendChild(biscript);

			var bistyle = Org.W3C.Window.Document.CreateElement("link");
			bistyle.SetAttribute("rel","StyleSheet");
			bistyle.SetAttribute("type","text/css");
			bistyle.SetAttribute("href","/bindows/html/css/bimain.css");
			Org.W3C.Window.Document.Head.AppendChild(bistyle);

			biscript.AddEventListener("load", OnInitialized,false);
		}

		private static void OnInitialized(object arg)
		{
			Start(root, adf);
			System.Threading.Thread.Sleep(500);
			Application.cb(null);
		}


		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern static BiApplicationWindow GetWindow();

		public static BiApplicationWindow Window {
			get {
				return GetWindow();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern static void Start(string root, string adf);
	}

	public class BiApplicationWindow : BiComponent
	{
	}


	public class BiEventTarget
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddEventListener(BiEventListener ev);
	}

	public class BiEventListener
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern BiEventListener(string sType, Action<object> fHandler);



	}

	public class BiImage
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern static BiImage FromUri(string path);
	}

	public class BiTextField : BiComponent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern BiTextField();


		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetText(string value);

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetText();

		public string Text {
			set { this.SetText (value);}
			get { return this.GetText(); } }


	}
}

/*var biscript = Window.Document.CreateElement("script");
			biscript.SetAttribute("src","/bindows/html/js/application.js");
			biscript.SetAttribute("type","application/javascript");
			Window.Document.Head.AppendChild(biscript);

			var bistyle = Window.Document.CreateElement("link");
			bistyle.SetAttribute("rel","StyleSheet");
			bistyle.SetAttribute("type","text/css");
			bistyle.SetAttribute("href","/bindows/html/css/bimain.css");
			Window.Document.Head.AppendChild(bistyle);

			biscript.AddEventListener("load", this.BindowsLoaded,false);*/