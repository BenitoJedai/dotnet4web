using System;
using System.Runtime.CompilerServices;

namespace Org.W3C.DOM
{
	public class DOMImplementation
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern DOMImplementation ();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasFeature (string feature, string version);
	}
}

