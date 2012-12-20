using System;
using System.Threading;
using Org.W3C;
using Org.W3C.DOM;
using Net.Bindows;

namespace n4w.ExampleProject
{
	public class MainClass
	{
		public string mensajito;
		public Element button;
		public BiTextField t;
		public BiWindow w;

		public MainClass()
		{
			this.mensajito = "Con un sleep me retraze";
			var biscript = Window.Document.CreateElement("script");
			biscript.SetAttribute("src","/bindows/html/js/application.js");
			biscript.SetAttribute("type","application/javascript");
			Window.Document.Head.AppendChild(biscript);

			var bistyle = Window.Document.CreateElement("link");
			bistyle.SetAttribute("rel","StyleSheet");
			bistyle.SetAttribute("type","text/css");
			bistyle.SetAttribute("href","/bindows/html/css/bimain.css");
			Window.Document.Head.AppendChild(bistyle);

			biscript.AddEventListener("load", this.BindowsLoaded,false);
		}


		public static void Main ()
		{
			new MainClass();
		}

		public void BindowsLoaded (object param)
		{
			Application.Start("/bindows/html", "BindowsApp.xml");

			button = Window.Document.CreateElement("button");

			button.AppendChild(Window.Document.CreateTextNode("Clickeame"));

			button.AddEventListener("click", CuandoClickee, false);

			Window.Document.Body.AppendChild(button);

			Thread.Sleep(1000);

			Window.Alert(this.mensajito);


		}

		public void CuandoClickee(object param) 
		{
			Window.Document.Body.RemoveChild(this.button);
			var xhr = new XMLHttpRequest();
			xhr.Open("GET", "test.txt",false, null, null);
			xhr.Send(null);

			t = new BiTextField();

			var b = new BiButton();
			b.HTML = "<b>Estoy en negrita</b>";
			b.AddEventListener(new BiEventListener("click", CuandoClickeeElBotonDeBindows));
			b.Left = 120;

			w = new BiWindow();
			w.ContentPane.Add(b);
			w.ContentPane.Add(t);
			w.ContentPane.BackColor = "red";
			w.Caption = "Escribime pescado";
			w.Icon = BiImage.FromUri("/bindows/html/themes/Default/images/default.16.gif");

			Application.Window.Add(w);

			Window.Alert(xhr.ResponseText);
		}

		public void CuandoClickeeElBotonDeBindows (object args)
		{
		
			if (t.Text == "pescado") {
				Window.Alert ("Bien chabon, sos mas inteligente que un mono");
				Window.SetTimeout(this.CuandoTermineElTimeout, 1000);
				w.Enabled = false;
			} else {
				Window.Alert ("Dale escribi pescado man!!!");
			}
		}

		public void CuandoTermineElTimeout (object a)
		{
			w.Close();
			Window.Alert("Me cerre con el timeout nativo");
		}
	}
}
