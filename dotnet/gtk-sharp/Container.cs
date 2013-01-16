using System;
using Net.Sencha.ExtJS;

namespace Gtk
{
	public class Container : Widget
	{
		protected Ext.container.Container ext_container;
		public Container ()
		{
		}

		public void Add(Widget w)
		{
			this.ext_container.Add(w.ext_component);
		}
	}
}

