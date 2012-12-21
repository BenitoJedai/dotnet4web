using System;
using Net.Bindows;
using Org.W3C;


public class MainClass
{
	public static void Main (string[] args)
	{
		Application.Initialize(OnBindowsLoaded,"/bindows/html","BindowsApp.xml");
	}

	private static void OnBindowsLoaded(object arg)
	{
		var b = new BiButton();
		b.HTML = "<b>Click me</b>";
		b.AddEventListener(new BiEventListener("click", OnButtonClicked));

		var w = new BiWindow();
		w.ContentPane.Add(b);

		Application.Window.Add(w);
	}

	public static void OnButtonClicked(object arg)
	{
		Window.Alert("You clicked me");
	}
}