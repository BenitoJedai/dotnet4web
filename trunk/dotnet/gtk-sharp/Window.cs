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
			Ext.WindowOptions options = new Ext.WindowOptions();
			options.Layout = "fit";

			this.ext_window = new Ext.Window(options);

			this.ext_container = this.ext_window;
			this.ext_component = this.ext_window;

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




	}
}

