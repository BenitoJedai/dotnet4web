using System;
using Net.Sencha.ExtJS;

namespace Gtk
{
	public class Widget
	{
		internal Ext.AbstractComponent ext_component;
		public void ShowAll()
		{
		}

		public bool CanFocus {
			set {
			}
		}

		public void Show() 
		{
			this.ext_component.Show();
		}
		public string Name
		{
			set
			{

			}
		}
	
		public void add_DeleteEvent(DeleteEventHandler handler)
		{
		}
	}
}

