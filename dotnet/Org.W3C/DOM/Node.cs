using System;
using System.Runtime.CompilerServices;

namespace Org.W3C.DOM
{
	public class Node 
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Node();

		public extern string NodeName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern string NodeValue
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		public extern short NodeType
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Node ParentNode
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern NodeList ChildNodes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Node FirstChild
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Node LastChild
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Node PreviousSibling
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Node NextSibling
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern NamedNodeMap Attributes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Node OwnerDocument
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Node InsertBefore(Node newChild, Node oldChild);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Node RemoveChild(Node oldChild);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Node AppendChild(Node newChild);

		public extern bool HasChildNodes
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Node CloneNode(Node newChild);
	}
}