using System;

namespace Org.W3C.HTML
{
	public class HTMLHeadElement : HTMLElement
	{
		/// <summary>
		/// Initializes a new instance of the HTMLHeadElement class.
		/// </summary>
		internal extern HTMLHeadElement ();

		/// <summary>
		/// URI [IETF RFC 2396] designating a metadata profile. See the profile attribute definition in HTML 4.01.
		/// </summary>
		public extern string Profile { get; set; }
	}
}