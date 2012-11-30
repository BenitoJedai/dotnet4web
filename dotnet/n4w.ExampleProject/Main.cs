using System;
using System.Threading;
using Org.W3C;

namespace n4w.ExampleProject
{
	public class MainClass
	{
		public static void Main (string[] args)
		{
			Window.Alert("Hello world!!!");
			Thread.Sleep(1000);
			Window.Alert("Otro alert");
		}
	}
}
