using System;

namespace Org.W3C.HTML
{
	public class HTMLHtmlElement : HTMLElement
	{
		/// <summary>
		/// Initializes a new instance of the HTMLHtmlElement class.
		/// </summary>
		internal extern HTMLHtmlElement ();

		/// <summary>
		/// Version information about the document's DTD. See the version attribute definition in HTML 4.01. This attribute is deprecated in HTML 4.01.
		/// </summary>
		public extern string Version { get; set; }
	}
}