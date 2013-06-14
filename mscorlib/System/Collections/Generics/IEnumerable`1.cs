using System;

namespace System.Collections.Generics
{
	public interface IEnumerable<out T> : IEnumerable
	{
		IEnumerator<T> GetEnumerator ();
	}
}