using System;
using Net.Sencha.ExtJS;
using Org.W3C;


public class MainClass
{
	public static void Main (string[] args)
	{
		Ext.Initialize();
		Ext.MessageBox.Confirm("Titulo", "Contenido", OnConfirmed);
	}

	public static void OnConfirmed(object o)
	{
		var win = new Ext.Window(new Ext.WindowOptions { Layout = "fit" });
		var box = new Ext.Container(new Ext.ContainerOptions { Layout = "hbox" });
		var b1 = new Ext.Button();

		box.Add(new Ext.Button());
		win.Add(box);
		box.Add(b1);
		box.Add(new Ext.Button());


		win.Show();
	}
}