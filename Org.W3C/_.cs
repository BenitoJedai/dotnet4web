using System;
using Org.W3C.DOM;
using Org.W3C.HTML;

namespace Org.W3C
{
	public class Window
	{
		protected extern Window();
		public extern void Alert(string message);
		public void Alert(object message)
		{
			Alert (message.ToString());
		}
		public extern HTMLDocument Document { get; }
	}

	public class NodeList
	{
		internal extern NodeList();
	}

	public class NamedNodeMap
	{
		internal extern NamedNodeMap();
	}

	public class UserData
	{
	}

	public class UserDataHandler
	{
	}

	public class ArrayBuffer
	{
		internal extern ArrayBuffer();
	}

	public class Blob
	{
		internal extern Blob();
	}

	public class Document : Node
	{
		internal extern Document();
	}

	public class Element : Node
	{
	}

	public class HTMLFormElement : HTMLElement
	{
	}


	public class ExampleEnclosing
	{
		public class ExampleNested
		{
		}
	}

}

