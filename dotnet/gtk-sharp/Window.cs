using System;
using Net.Sencha.ExtJS;
using System.Runtime.CompilerServices;

namespace Gtk
{
	public class Window : Bin
	{
		private Ext.Window ext_window;
		public Window (WindowType type)
		{
			this.ext_window = new Ext.Window(new Ext.WindowOptions { Layout = "fit" });

			this.ext_container = this.ext_window;
			this.ext_component = this.ext_window;

		}

		public string Name {
			set {

			}
		}

		public string Title {
			set {
				ext_window.SetTitle(value);
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




	}
}

