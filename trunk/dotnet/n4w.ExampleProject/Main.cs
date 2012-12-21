using System;
using Org.W3C;
using Org.W3C.DOM;

public class MainClass
{
	public static void Main ()
	{
		new MainClass();
	}

	public MainClass()
	{
		var b = Window.Document.CreateElement("button");
		b.AppendChild(Window.Document.CreateTextNode("Launch AJAX"));
		b.AddEventListener("click",this.OnButtonClicked, false);
		Window.Document.Body.AppendChild(b);
	}

	public void OnButtonClicked(object param)
	{
		var xhr = new XMLHttpRequest();
		xhr.Open("GET","test.txt",false, null, null);
		xhr.Send(null);
		Window.Alert(xhr.ResponseText);
	}

}
