using System;
using System.Threading;
using Org.W3C;
using Org.W3C.DOM;
using Net.Bindows;

namespace n4w.ExampleProject
{
	public class MainClass
	{
		public MainClass()
		{
			Window.Alert("jajaja");
		}

		public static void Main ()
		{
			new MainClass();

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

			b.SetHtml("<b>Estoy en negrita</b>");

			w.ContentPane.Add(b);

			Application.GetWindow().Add(w);

			Window.Alert(xhr.ResponseText);



		}
	}
}
