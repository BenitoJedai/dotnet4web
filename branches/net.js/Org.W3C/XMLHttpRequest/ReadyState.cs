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

namespace Org.W3C.XMLHttpRequest
{
	//http://www.w3.org/TR/2012/WD-XMLHttpRequest-20121206/

	public enum ReadyState
	{
		Unsent = 0,
		Opened = 1,
		HeadersReceived = 2,
		Loading = 3,
		Done = 4
	}
}