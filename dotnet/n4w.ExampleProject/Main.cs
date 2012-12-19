using System;
using System.Threading;
using Org.W3C;
using Org.W3C.DOM;
using Net.Bindows;

namespace n4w.ExampleProject
{
	public class MainClass
	{
		public static void Main ()
		{
			var biscript = Window.Document.CreateElement("script");
			biscript.SetAttribute("src","/bindows/html/js/application.js");
			biscript.SetAttribute("type","application/javascript");
			Window.Document.Head.AppendChild(biscript);

			var bistyle = Window.Document.CreateElement("link");
			bistyle.SetAttribute("rel","StyleSheet");
			bistyle.SetAttribute("type","text/css");
			bistyle.SetAttribute("href","/bindows/html/css/bimain.css");
			Window.Document.Head.AppendChild(bistyle);

			biscript.AddEventListener("load", BindowsLoaded,false);
		}

		public static void BindowsLoaded (object param)
		{
			Application.Start("/bindows/html", "BindowsApp.xml");

			var button = Window.Document.CreateElement("button");

			button.AppendChild(Window.Document.CreateTextNode("Clickeame"));

			button.AddEventListener("click", CuandoClickee, false);

			Window.Document.Body.AppendChild(button);

			Thread.Sleep(1000);

			Window.Alert("Hola mundo!!!");


		}

		public static void CuandoClickee(object param) 
		{
			var xhr = new XMLHttpRequest();
			xhr.Open("GET", "test.txt",false, null, null);
			xhr.Send(null);

			var b = new BiButton();
			var w = new BiWindow();

			b.HTML = "<b>Estoy en negrita</b>";

			w.ContentPane.Add(b);
			w.ContentPane.BackColor = "red";
			w.Caption = "Titulo RE PRO";

			b.AddEventListener(new BiEventListener("click", CuandoClickeeElBotonDeBindows));

			Application.Window.Add(w);

			Window.Alert(xhr.ResponseText);


		}

		public static void CuandoClickeeElBotonDeBindows(object args)
		{
			Window.Alert("IM SO PRO!!!");
		}
	}
}
