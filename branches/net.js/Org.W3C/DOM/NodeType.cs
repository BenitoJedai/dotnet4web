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

//http://www.w3.org/TR/2004/REC-DOM-Level-3-Core-20040407/
namespace Org.W3C.DOM
{
	/// <summary>
	/// Numeric codes up to 200 are reserved to W3C for possible future use.
	/// </summary>
	public enum NodeType
	{
		Element                = 1,
		Attribute                 = 2,
		Text                      = 3,
		CDataSection            = 4,
		EntityReference         = 5,
		Entity                    = 6,
		ProcessingInstruction    = 7,
		Comment                  = 8,
		Document                  = 9,
		DocumentType             = 10,
		DocumentFragment         = 11,
		Notation                  = 12
	}
}

