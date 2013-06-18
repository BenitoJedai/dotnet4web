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

using Org.W3C.DOM;

namespace Org.W3C.HTML
{
	//http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109/html.html

	/// <summary>
	/// An HTMLOptionsCollection is a list of nodes representing HTML option element. An individual node may be accessed by either ordinal index or the node's name or id attributes.
	/// </summary>
	public class HTMLOptionsCollection
	{
		#region constructors

		/// <summary>
		/// Initializes a new instance of the HTMLCollection"class.
		/// </summary>
		internal extern HTMLOptionsCollection ();

		#endregion

		#region properties

		/// <summary>
		/// This attribute specifies the length or size of the list.
		/// </summary>
		public extern uint Length { get; }

		#endregion

		/// <summary>
		/// This method retrieves a node specified by ordinal index. Nodes are numbered in tree order (depth-first traversal order).
		/// </summary>
		public extern Node this[uint index] { get; }

		/// <summary>
		/// This method retrieves a Node using a name. With [HTML 4.01] documents, it first searches for a Node with a matching id attribute. If it doesn't find one, it then searches for a Node with a matching name attribute, but only on those elements that are allowed a name attribute. With [XHTML 1.0] documents, this method only searches for Nodes with a matching id attribute. This method is case insensitive in HTML documents and case sensitive in XHTML documents.
		/// </summary>
		public extern Node this[string name] { get; }
	}
}
