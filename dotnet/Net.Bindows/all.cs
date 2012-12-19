

using System.Runtime.CompilerServices;

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

		public string Html { set { this.SetHtml(value); } }

	}

	public class BiComponent
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern BiComponent();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Add(BiComponent newChild);
	}

	public class Application
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern static BiApplicationWindow GetWindow();
	}

	public class BiApplicationWindow : BiComponent
	{
	}

}