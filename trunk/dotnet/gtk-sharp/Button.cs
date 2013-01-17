using System;
using Net.Sencha.ExtJS;

namespace Gtk
{
	public class Button : Widget
	{
		private Ext.Button ext_button;
		public Button ()
		{
			this.ext_button = new Ext.Button();
			this.ext_component = this.ext_button;
		}

		public bool UseUnderline {
			set {
			}
		}

		public string Label {
			set {
				ext_button.SetText(value);
			}
		}

		public void add_Clicked (EventHandler handler)
		{

		}


	}
}



/*this.button1 = new global::Gtk.Button ();
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");*/