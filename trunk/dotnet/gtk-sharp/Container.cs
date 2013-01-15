using System;

namespace Gtk
{
	public class Container : Widget
	{
		internal Net.Bindows.BiComponent rc;
		public Container ()
		{
		}

		public void Add(Widget w)
		{
			rc.Add(w.bi);
		}
	}
}

