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

		public extern HTMLElement Head
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}

	public class HTMLElement : Element
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern HTMLElement();

		public extern HTMLElement Id
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
		public extern HTMLElement Title
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
		public extern HTMLElement Lang
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
		public extern HTMLElement Dir
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
		public extern HTMLElement ClassName
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}

