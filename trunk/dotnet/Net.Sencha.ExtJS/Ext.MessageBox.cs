using System;
using System.Runtime.CompilerServices;


namespace Net.Sencha.ExtJS
{
	public class MessageBox
	{
		internal MessageBox()
		{
		}


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		public static extern void Confirm(string title, string message, Action<object> cb);
	}
}

