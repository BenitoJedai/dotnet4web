using System;
using System.Threading;

namespace Gtk
{
	public class Application
	{
		private static AutoResetEvent mutex;
		public static void Init()
		{
			mutex = new AutoResetEvent(false);
			Net.Bindows.Application.Initialize(onBindowsLoaded,"/bindows/html","BindowsApp.xml");
			mutex.WaitOne();
		}

		private static void onBindowsLoaded(object o)
		{
			mutex.Set();
		}

		public static void Run()
		{
		}

		public static void Quit()
		{
		}
	}
}

