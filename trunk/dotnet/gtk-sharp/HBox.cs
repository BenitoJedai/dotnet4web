using System;
using Net.Sencha.ExtJS;

namespace Gtk
{
	public class HBox : Box
	{
		public HBox ()
		{
			this.ext_container = new Ext.Container(new Ext.ContainerOptions { Layout = "hbox" } );
			this.ext_component = this.ext_component;
		}
	}
}