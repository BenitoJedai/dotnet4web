using System;
using Net.Bindows;

namespace Gtk
{
	public class Window : Widget
	{
		private BiWindow bi;
		public Window (WindowType type)
		{
			bi = new BiWindow();
			Net.Bindows.Application.Window.Add(bi);
		}

		public string Name {
			set {

			}
		}

		public string Title {
			set {
			}
		}

		public Widget Child {
			get {
				return null;
			}
		}

		public WindowPosition WindowPosition {
			set {

			}
		}

		public int DefaultWidth {
			set {

			}
		}

		public int DefaultHeight {
			set {

			}
		}

		public void Show() 
		{
		}
	}
}

