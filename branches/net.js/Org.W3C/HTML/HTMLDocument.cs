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

namespace Org.W3C.HTML
{
	//http://www.w3.org/TR/2003/REC-DOM-Level-2-HTML-20030109/html.html

	/// <summary>
	/// An HTMLDocument is the root of the HTML hierarchy and holds the entire content. Besides providing access to the hierarchy, it also provides some convenience methods for accessing certain sets of information from the document.
	/// </summary>
	public class HTMLDocument : Document
	{
		#region constructors

		/// <summary>
		/// Initializes a new instance of the HTMLDocument class.
		/// </summary>
		internal extern HTMLDocument ();

		#endregion

		#region properties

		#endregion


	}
}

/*


Attributes
URL of type DOMString, readonly
The absolute URI [IETF RFC 2396] of the document.
anchors of type HTMLCollection, readonly
A collection of all the anchor (A) elements in a document with a value for the name attribute.
Note: For reasons of backward compatibility, the returned set of anchors only contains those anchors created with the name attribute, not those created with the id attribute. Note that in [XHTML 1.0], the name attribute (see section 4.10) has no semantics and is only present for legacy user agents: the id attribute is used instead. Users should prefer the iterator mechanisms provided by [DOM Level 2 Traversal] instead.

applets of type HTMLCollection, readonly
A collection of all the OBJECT elements that include applets and APPLET (deprecated) elements in a document.
body of type HTMLElement
The element that contains the content for the document. In documents with BODY contents, returns the BODY element. In frameset documents, this returns the outermost FRAMESET element.
cookie of type DOMString
This mutable string attribute denotes persistent state information that (1) is associated with the current frame or document and (2) is composed of information described by the cookies non-terminal of [IETF RFC 2965], Section 4.2.2.
If no persistent state information is available for the current frame or document document, then this property's value is an empty string.
When this attribute is read, all cookies are returned as a single string, with each cookie's name-value pair concatenated into a list of name-value pairs, each list item being separated by a ';' (semicolon).
When this attribute is set, the value it is set to should be a string that adheres to the cookie non-terminal of [IETF RFC 2965]; that is, it should be a single name-value pair followed by zero or more cookie attribute values. If no domain attribute is specified, then the domain attribute for the new value defaults to the host portion of an absolute URI [IETF RFC 2396] of the current frame or document. If no path attribute is specified, then the path attribute for the new value defaults to the absolute path portion of the URI [IETF RFC 2396] of the current frame or document. If no max-age attribute is specified, then the max-age attribute for the new value defaults to a user agent defined value. If a cookie with the specified name is already associated with the current frame or document, then the new value as well as the new attributes replace the old value and attributes. If a max-age attribute of 0 is specified for the new value, then any existing cookies of the specified name are removed from the cookie storage.
Note: See [IETF RFC 2965] for the semantics of persistent state item attribute value pairs.

Note: The precise nature of a user agent session is not defined by this specification.

Exceptions on setting
DOMException

SYNTAX_ERR: If the new value does not adhere to the cookie syntax specified by [IETF RFC 2965].

domain of type DOMString, readonly
The domain name of the server that served the document, or null if the server cannot be identified by a domain name.
forms of type HTMLCollection, readonly
A collection of all the forms of a document.
images of type HTMLCollection, readonly
A collection of all the IMG elements in a document. The behavior is limited to IMG elements for backwards compatibility.
Note: As suggested by [HTML 4.01], to include images, authors may use the OBJECT element or the IMG element. Therefore, it is recommended not to use this attribute to find the images in the document but getElementsByTagName with HTML 4.01 or getElementsByTagNameNS with XHTML 1.0.

links of type HTMLCollection, readonly
A collection of all AREA elements and anchor (A) elements in a document with a value for the href attribute.
referrer of type DOMString, readonly
Returns the URI [IETF RFC 2396] of the page that linked to this page. The value is an empty string if the user navigated to the page directly (not through a link, but, for example, via a bookmark).
title of type DOMString
The title of a document as specified by the TITLE element in the head of the document.
Methods
close
Closes a document stream opened by open() and forces rendering.
No Parameters
No Return Value
No Exceptions
getElementsByName
With [HTML 4.01] documents, this method returns the (possibly empty) collection of elements whose name value is given by elementName. In [XHTML 1.0] documents, this methods only return the (possibly empty) collection of form controls with matching name. This method is case sensitive.
Parameters
elementName of type DOMString
The name attribute value for an element.
Return Value
NodeList

The matching elements.

No Exceptions
open
Open a document stream for writing. If a document exists in the target, this method clears it.
Note: This method and the ones following allow a user to add to or replace the structure model of a document using strings of unparsed HTML. At the time of writing alternate methods for providing similar functionality for both HTML and XML documents were being considered (see [DOM Level 3 Load and Save]).

No Parameters
No Return Value
No Exceptions
write
Write a string of text to a document stream opened by open(). Note that the function will produce a document which is not necessarily driven by a DTD and therefore might be produce an invalid result in the context of the document.
Parameters
text of type DOMString
The string to be parsed into some structure in the document structure model.
No Return Value
No Exceptions
writeln
Write a string of text followed by a newline character to a document stream opened by open(). Note that the function will produce a document which is not necessarily driven by a DTD and therefore might be produce an invalid result in the context of the document
Parameters
text of type DOMString
The string to be parsed into some structure in the document structure model.
No Return Value
No Exceptions*/
