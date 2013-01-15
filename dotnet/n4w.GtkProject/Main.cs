using System;
using Gtk;


class MainClass
{
	public static void Main (string[] args)
	{
		Net.Bindows.Application.Initialize(GtkMain,"/bindows/html","BindowsApp.xml");
	}

 	private static void GtkMain(object param)
	{
		//Application.Init ();
		MainWindow win = new MainWindow ();
		/*win.Show ();
		Application.Run ();*/
	}
}

