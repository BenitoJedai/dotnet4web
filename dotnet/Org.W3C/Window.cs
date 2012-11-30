using System;

//http://www.w3.org/TR/2000/WD-DOM-Level-1-20000929/java-language-binding.html
using System.Runtime.CompilerServices;

namespace Org.W3C
{
	public static class Window
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern static void Alert(string message);

		public extern static DOM.Document Document
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}

