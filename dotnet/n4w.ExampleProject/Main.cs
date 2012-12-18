using System;
using System.Threading;
using Org.W3C;
using Org.W3C.DOM;

namespace n4w.ExampleProject
{
	public class MainClass
	{
		public static void Main ()
		{
			var button = Window.Document.CreateElement("button");

			button.AppendChild(Window.Document.CreateTextNode("Clickeame"));

			button.AddEventListener("click", CuandoClickee, false);

			Window.Document.Body.AppendChild(button);

			Thread.Sleep(500);

			Window.Alert("Hola mundo!!!");
		}

		public static void CuandoClickee(object param) 
		{
			Window.Alert("Clickeaste");
		}
	}
}
