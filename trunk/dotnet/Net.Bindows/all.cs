

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
}