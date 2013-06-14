using System;

namespace Org.W3C
{
	/**
	 * @see http://www.w3.org/TR/2012/WD-XMLHttpRequest-20121206/
	 */
	public enum ReadyState
	{
		Unsent = 0,
		Opened = 1,
		HeadersReceived = 2,
		Loading = 3,
		Done = 4
	}
}