using System;

namespace System.Runtime.InteropServices
{
	public sealed class GuidAttribute : Attribute
	{
		public extern GuidAttribute (string guid);
	}
}