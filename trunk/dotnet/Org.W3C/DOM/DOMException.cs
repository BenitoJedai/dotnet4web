using System;
using System.Runtime.CompilerServices;

namespace Org.W3C.DOM
{
	public class DOMException
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern DOMException();

		public extern short Code
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}