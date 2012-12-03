using System;
using System.Threading;
using Org.W3C;
using Org.W3C.DOM;

namespace n4w.ExampleProject
{
	public class MainClass
	{
		private static Text Create ()
		{
			return Window.Document.CreateTextNode("Hello world!!!");
		}

		public static void Main ()
		{
			Window.Document.Body.AppendChild(Create());
		}
	}
}
