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
using Org.W3C.Events;

namespace Org.W3C.HTML
{
	//http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109/html.html

	/// <summary>
	/// All HTML element interfaces derive from this class. Elements that only expose the HTML core attributes are represented by the base HTMLElement interface
	/// </summary>
	public class HTMLElement : Element
	{
		#region constructors

		/// <summary>
		/// Initializes a new instance of the HTMLElement class.
		/// </summary>
		internal extern HTMLElement ();

		#endregion

		#region properties

		/// <summary>
		/// The class attribute of the element. This attribute has been renamed due to conflicts with the "class" keyword exposed by many languages. See the class attribute definition in HTML 4.01.
		/// </summary>
		public extern string ClassName { get; set; }

		/// <summary>
		/// Specifies the base direction of directionally neutral text and the directionality of tables. See the dir attribute definition in HTML 4.01.
		/// </summary>
		public extern string Dir { get; set; }

		/// <summary>
		/// The element's identifier. See the id attribute definition in HTML 4.01.
		/// </summary>
		public extern string Id { get; set; }

		/// <summary>
		/// Language code defined in RFC 1766. See the lang attribute definition in HTML 4.01.
		/// </summary>
		public extern string Lang { get; set; }

		/// <summary>
		/// The element's advisory title. See the title attribute definition in HTML 4.01.
		/// </summary>
		public virtual string Title { get; set; }

		#endregion

		#region events

		public extern event MouseEventHandler OnClick;

		#endregion

	}
}
	