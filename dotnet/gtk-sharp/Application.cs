using System;
using System.Threading;

namespace Gtk
{
	public class Application
	{
		public static void Init()
		{
			Net.Bindows.Application.Initialize("/bindows/html","BindowsApp.xml");
		}


		public static void Run()
		{
		}

		public static void Quit()
		{
		}
	}
}

