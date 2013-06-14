using System;

namespace Org.W3C
{
	/**
	 * @see http://www.w3.org/TR/2012/WD-XMLHttpRequest-20121206/
	 */
	public class XMLHttpRequestUpload
	{
		internal extern XMLHttpRequestUpload();
		public extern event Action OnLoadStart;
		public extern event Action OnProgress;
		public extern event Action OnAbort;
		public extern event Action OnError;
		public extern event Action OnLoad;
		public extern event Action OnTimeout;
		public extern event Action OnLoadEnd;
	}
}