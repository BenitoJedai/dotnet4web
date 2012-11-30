using System;
using System.Runtime.CompilerServices;

namespace Org.W3C.DOM
{
	public class Document : Node
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Document();

		/*public extern DocumentType DocType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}*/

		public extern DOMImplementation Implementation
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		/*public extern Element DocumentElement
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}*/

		/*[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Element CreateElement(string tagName);*/

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern DocumentFragment CreateDocumentFragment();

		/*[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Text CreateTextNode(string data);*/

		/*[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Comment CreateComment();*/

		/*[MethodImpl(MethodImplOptions.InternalCall)]
		public extern CDATASection CreateCDATASection();*/

		/*[MethodImpl(MethodImplOptions.InternalCall)]
		public extern ProcessingInstruction CreateProcessingInstruction(string target, string data);*/

		/*[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Attr CreateAttribute(string name);*/

		/*[MethodImpl(MethodImplOptions.InternalCall)]
		public extern EntityReference CreateEntityReference();*/

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern NodeList GetElementsByTagName(string tagname);
	}
}