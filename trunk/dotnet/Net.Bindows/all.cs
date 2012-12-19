

using System.Runtime.CompilerServices;
using System;

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

	}

	public class BiButton : BiComponent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern BiButton();

		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetHtml(string html);

		public string HTML { set { this.SetHtml(value); } }

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

	}

	public static class Application
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern static BiApplicationWindow GetWindow();

		public static BiApplicationWindow Window {
			get {
				return GetWindow();
			}
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern static void Start(string root, string adf);
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
}