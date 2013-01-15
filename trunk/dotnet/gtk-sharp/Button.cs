using System;

namespace Gtk
{
	public class Button : Widget
	{
		internal Net.Bindows.BiButton _bibutton = new Net.Bindows.BiButton();
		public Button ()
		{
			this._bibutton = new Net.Bindows.BiButton();

			this.bi = this._bibutton;
		}

		public bool UseUnderline {
			set {
			}
		}

		public string Label {
			set {
				this._bibutton.HTML = value;
			}
		}

		public void add_Clicked (EventHandler handler)
		{
			//this.bi.AddEventListener(new Net.Bindows.BiEventListener("click",handler));
		}


	}
}



/*this.button1 = new global::Gtk.Button ();
		this.button1.CanFocus = true;
		this.button1.Name = "button1";
		this.button1.UseUnderline = true;
		this.button1.Label = global::Mono.Unix.Catalog.GetString ("GtkButton");*/