using System;
using System.Runtime.CompilerServices;

namespace Org.W3C.DOM
{
	public class NodeList 
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern NodeList();

		public extern int Length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Node this[int index]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}