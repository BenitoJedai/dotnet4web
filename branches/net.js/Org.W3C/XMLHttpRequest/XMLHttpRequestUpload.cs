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

	public class XMLHttpRequestUpload
	{
		#region constructors

		internal extern XMLHttpRequestUpload();

		#endregion

		#region events

		public extern event Action OnLoadStart;
		public extern event Action OnProgress;
		public extern event Action OnAbort;
		public extern event Action OnError;
		public extern event Action OnLoad;
		public extern event Action OnTimeout;
		public extern event Action OnLoadEnd;

		#endregion
	}
}