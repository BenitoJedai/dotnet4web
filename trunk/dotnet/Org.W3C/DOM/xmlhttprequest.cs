using System;
using System.Runtime.CompilerServices;

namespace Org.W3C
{
	public class XMLHttpRequest
	{
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern XMLHttpRequest();

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Open(string method, string url, bool isAsync, string user, string password);

		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Send(string data);

		public extern string ResponseText
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}
	}
}

