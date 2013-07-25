using System;
using Org.W3C.DOM;
using Org.W3C.HTML;

namespace Org.W3C
{
	public class Window
	{
		protected extern Window();
		public extern void Alert(string message);
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
		internal extern UserData();
	}

	public class UserDataHandler
	{
		internal extern UserDataHandler();
	}

	public class ArrayBuffer
	{
		internal extern ArrayBuffer();
	}

	public class Blob
	{
		internal extern Blob();
	}

	public class Text : Node
	{
		internal extern Text();
	}

	public class Document : Node
	{
		internal extern Document();
		public extern Text CreateTextNode (string data);
		public extern Element CreateElement (string tagname);
	}

	public class Element : Node
	{
		internal extern Element();
	}

	public class HTMLFormElement : HTMLElement
	{
		internal extern HTMLFormElement();
	}
}

