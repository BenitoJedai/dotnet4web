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

//http://www.w3.org/TR/2012/WD-XMLHttpRequest-20121206/
namespace Org.W3C
{
	/// <summary>
	/// The FormData object represents an ordered collection of entries. Each entry has a name, a value, a type, and optionally a filename (if type is "file").
	/// </summary>
	public class FormData
	{
		#region constructors

		/// <summary>
		/// Initializes a new instance of the FormData class.
		/// </summary>
		internal extern FormData();

		/// <summary>
		/// Initializes a new instance of the FormData class with the data from form.
		/// </summary>
		/// <param name="form">Form.</param>
		internal extern FormData(HTMLFormElement form);

		#endregion

		#region methods

		/// <summary>
		/// Appends a new entry to the FormData object.
		/// </summary>
		/// <param name="name">Name.</param>
		public extern void Append(string name, string value);

		/// <summary>
		/// Appends a new entry to the FormData object.
		/// </summary>
		/// <param name="name">Name.</param>
		public extern void Append(string name, Blob value);

		/// <summary>
		/// Appends a new entry to the FormData object.
		/// </summary>
		/// <param name="name">Name.</param>
		public extern void Append(string name, Blob value, string filename);

		#endregion
	}
}

