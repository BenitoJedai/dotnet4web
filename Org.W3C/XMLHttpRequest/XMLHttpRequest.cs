/*

This file is part of net.js.

net.js is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as
published by the Free Software Foundation, either version 3 of the
License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see `<http://www.gnu.org/licenses/>`.

*/

using System;

namespace Org.W3C.XMLHttpRequest
{
	//http://www.w3.org/TR/2012/WD-XMLHttpRequest-20121206/

	public class XMLHttpRequest
	{
		#region constructors

		/// <summary>
		/// Initializes a new instance of the XMLHttpRequestclass.
		/// </summary>
		public extern XMLHttpRequest();

		/// <summary>
		/// Initializes a new instance of the XMLHttpRequest class with specific options.
		/// </summary>
		/// <param name="options">Options.</param>
		public extern XMLHttpRequest(XMLHttpRequestOptions options);

		#endregion

		#region propeties

		/// <summary>
		/// Returns the current state.
		/// </summary>
		public extern ReadyState ReadyState { get; }

		/// <summary>
		/// Returns the response entity body.
		/// </summary>
		public extern object Response { get; }

		/// <summary>
		/// Returns the text response entity body.
		/// </summary>
		public extern string ResponseText { get; }

		/// <summary>
		/// Can be set to change the response type. Values are: the empty string (default), "arraybuffer", "blob", "document", "json", and "text".
		/// </summary>
		public extern string ResponseType { get; set; }

		/// <summary>
		/// Returns the document response entity body.
		/// </summary>
		public extern Document ResponseXML { get; }

		/// <summary>
		/// Returns the HTTP status code.
		/// </summary>
		public extern ushort Status { get; }

		/// <summary>
		/// Returns the HTTP status text.
		/// </summary>
		public extern string StatusText { get; }

		/// <summary>
		/// The amount of milliseconds a request can take before being terminated. Initially zero. Zero means there is no timeout.
		/// </summary>
		public extern uint Timeout { get; }

		/// <summary>
		/// Returns the associated XMLHttpRequestUpload object.
		/// </summary>
		public extern XMLHttpRequestUpload Upload { get; }

		/// <summary>
		/// True when user credentials are to be included in a cross-origin request. False when they are to be excluded in a cross-origin request and when cookies are to be ignored in its response. Initially false.
		/// </summary>
		public extern bool WithCredentials { get; }

		#endregion

		#region methods

		/// <summary>
		/// Cancels any network activity.
		/// </summary>
		public extern void Abort();

		/// <summary>
		/// Returns all headers from the response, with the exception of those whose field name is Set-Cookie or Set-Cookie2.
		/// </summary>
		public extern string GetAllResponseHeaders();

		/// <summary>
		/// Returns the header field value from the response of which the field name matches header, unless the field name is Set-Cookie or Set-Cookie2.
		/// </summary>
		public extern string GetResponseHeader(string header);

		/// <summary>
		/// Sets the request method, request URL, synchronous flag, request username, and request password.
		/// </summary>
		public extern void Open(string method, string url, bool async = true, string username = null, string password = null);
	
		/// <summary>
		/// Sets the Content-Type header for the response to mime.
		/// </summary>
		public extern void OverrideMimeType(string mime);

		/// <summary>
		/// Initiates the request. The optional argument provides the request entity body. The argument is ignored if request method is GET or HEAD.
		/// </summary>
		public extern void Send(string data =  null);

		/// <summary>
		/// Initiates the request. The argument provides the request entity body. The argument is ignored if request method is GET or HEAD.
		/// </summary>
		public extern void Send(Blob data);

		/// <summary>
		/// Initiates the request. The argument provides the request entity body. The argument is ignored if request method is GET or HEAD.
		/// </summary>
		public extern void Send(ArrayBuffer data);

		/// <summary>
		/// Initiates the request. The argument provides the request entity body. The argument is ignored if request method is GET or HEAD.
		/// </summary>
		public extern void Send(Document data);

		/// <summary>
		/// Initiates the request. The argument provides the request entity body. The argument is ignored if request method is GET or HEAD.
		/// </summary>
		public extern void Send(FormData data);

		/// <summary>
		/// Appends an header to the list of author request headers, or if header is already in the list of author request headers, combines its value with value.
		/// </summary>
		public extern void SetRequestHeader(string header, string value);

		#endregion

		#region events

		public extern event Action OnLoadStart;
		public extern event Action OnProgress;
		public extern event Action OnAbort;
		public extern event Action OnError;
		public extern event Action OnLoad;
		public extern event Action OnTimeout;
		public extern event Action OnLoadEnd;
		public extern event Action OnReadyStateChange;

		#endregion
	}
}