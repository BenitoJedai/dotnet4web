using System;
using Net.Bindows;

namespace Gtk
{
	public class Window : Bin
	{
		public Window (WindowType type)
		{
			var w  = new BiWindow();
			Net.Bindows.Application.Window.Add(w);
			this.bi = w;
			this.rc = w.ContentPane;
		}

		public string Name {
			set {

			}
		}

		public string Title {
			set {
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

