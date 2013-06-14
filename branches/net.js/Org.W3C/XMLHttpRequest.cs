using System;

namespace Org.W3C
{
	/**
	 * @see http://www.w3.org/TR/2012/WD-XMLHttpRequest-20121206/
	 */
	public class XMLHttpRequest
	{
		public extern XMLHttpRequest();

		public extern void Open(string method, string url, bool async = true, string username = null, string password = null);
		public extern void Send(string data =  null);
		public extern void Send(Blob data);
		public extern void Send(ArrayBufferView data);
		public extern void Send(Document data);
		public extern void Send(FormData data);
		public extern void SetRequestHeader(string header, string value);
		public extern void Abort();
		public extern string GetResponseHeader(string header);
		public extern string GetAllResponseHeaders();
		public extern void OverrideMimeType(string mime);

		public extern string ResponseText { get; }
		public extern ReadyState ReadyState { get; }
		public extern uint Timeout { get; }
		public extern bool WithCredentials { get; }
		public extern XMLHttpRequestUpload Upload { get; }
		public extern ushort Status { get; }
		public extern string StatusText { get; }
		public extern string ResponseType { get; set; }
		public extern object Response { get; }
		public extern Document ResponseXML { get; }

		public extern event Action OnLoadStart;
		public extern event Action OnProgress;
		public extern event Action OnAbort;
		public extern event Action OnError;
		public extern event Action OnLoad;
		public extern event Action OnTimeout;
		public extern event Action OnLoadEnd;
		public extern event Action OnReadyStateChange;
	}
}