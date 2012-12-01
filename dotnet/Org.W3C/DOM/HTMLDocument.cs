using System;
using System.Runtime.CompilerServices;

namespace Org.W3C.DOM
{
	public class HTMLDocument : Document
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern HTMLDocument();

		public extern HTMLElement Body
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}

