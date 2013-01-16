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
		Window.Alert("Confirmo");
	}
}