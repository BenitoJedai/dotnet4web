using System;

namespace Gtk
{
	public class Button : Widget
	{
		public Button ()
		{
			var b = new Net.Bindows.BiButton();

			this.bi = b;
		}

		public bool UseUnderline {
			set {
			}
		}

		public bool Label {
			set {
			}
		}
	}
}



/*this.button1 = new global::Gtk.Button ();
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");*/