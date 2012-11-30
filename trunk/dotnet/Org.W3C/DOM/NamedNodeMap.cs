using System;
using System.Runtime.CompilerServices;

namespace Org.W3C.DOM
{
	public class NamedNodeMap 
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern NamedNodeMap();

		public extern int Length
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		public extern Node this[string name]
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
		/* PEDORRO
		 *  public Node removeNamedItem(String name)
                                throws DOMException;

    public Node item(int index);
*/
	}
}