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
	/// The Node interface is the primary datatype for the entire Document Object Model. It represents a single node in the document tree. While all objects implementing the Node interface expose methods for dealing with children, not all objects implementing the Node interface may have children. For example, Text nodes may not have children, and adding children to such nodes results in a DOMException being raised.
	/// The attributes nodeName, nodeValue and attributes are included as a mechanism to get at node information without casting down to the specific derived interface. In cases where there is no obvious mapping of these attributes for a specific nodeType (e.g., nodeValue for an Element or attributes for a Comment), this returns null. Note that the specialized interfaces may contain additional and more convenient mechanisms to get and set the relevant information.
	/// </summary>

	public class Node
	{
		#region constructors

		/// <summary>
		/// Internal initializer for "Org.W3C.Node" class.
		/// </summary>
		internal extern Node ();

		#endregion

		#region properties

		/// <summary>
		/// A NamedNodeMap containing the attributes of this node (if it is an Element) or null otherwise.
		/// </summary>
		public extern NamedNodeMap Attributes { get; }

		/// <summary>
		/// The absolute base URI of this node or null if the implementation wasn't able to obtain an absolute URI. This value is computed as described in Base URIs. However, when the Document supports the feature "HTML" [DOM Level 2 HTML], the base URI is computed using first the value of the href attribute of the HTML BASE element if any, and the value of the documentURI attribute from the Document interface otherwise.
		/// </summary>
		/// <value>The base UR.</value>
		public extern string BaseURI { get; }

		/// <summary>
		/// A NodeList that contains all children of this node. If there are no children, this is a NodeList containing no nodes.
		/// </summary>
		public extern NodeList ChildNodes { get; }

		/// <summary>
		/// The first child of this node. If there is no such node, this returns null.
		/// </summary>
		public extern Node FirstChild { get; }

		/// <summary>
		/// The last child of this node. If there is no such node, this returns null.
		/// </summary>
		public extern Node LastChild { get; }

		/// <summary>
		/// Returns the local part of the qualified name of this node.
		/// For nodes of any type other than ELEMENT_NODE and ATTRIBUTE_NODE and nodes created with a DOM Level 1 method, such as Document.createElement(), this is always null.
		/// </summary>
		public extern string LocalName { get; }

		/// <summary>
		/// The namespace URI of this node, or null if it is unspecified.
		/// This is not a computed value that is the result of a namespace lookup based on an examination of the namespace declarations in scope. It is merely the namespace URI given at creation time.
		/// For nodes of any type other than ELEMENT_NODE and ATTRIBUTE_NODE and nodes created with a DOM Level 1 method, such as createElement from the Document interface, this is always null.
		/// </summary>
		public extern string NamespaceURI { get; }

		/// <summary>
		/// The node immediately following this node. If there is no such node, this returns null.
		/// </summary>
		public extern Node NextSibling { get; }

		/// <summary>
		/// The name of this node, depending on its type; see the table above.
		/// </summary>
		public extern string NodeName { get; }

		/// <summary>
		/// A code representing the type of the underlying object.
		/// </summary>
		public extern NodeType NodeType { get; }

		/// <summary>
		/// The value of this node, depending on its type; see the table above. When it is defined to be null, setting it has no effect, including if the node is read-only.
		/// </summary>
		public extern string NodeValue { get; set; }

		/// <summary>
		/// The Document object associated with this node. This is also the Document object used to create new nodes. When this node is a Document or a DocumentType which is not used with any Document yet, this is null.
		/// </summary>
		public extern Document OwnerDocument { get; }

		/// <summary>
		/// The parent of this node. All nodes, except Attr, Document, DocumentFragment, Entity, and Notation may have a parent. However, if a node has just been created and not yet added to the tree, or if it has been removed from the tree, this is null. 
		/// </summary>
		public extern Node ParentNode { get; }

		/// <summary>
		/// The namespace prefix of this node, or null if it is unspecified. When it is defined to be null, setting it has no effect, including if the node is read-only.
		/// Note that setting this attribute, when permitted, changes the nodeName attribute, which holds the qualified name, as well as the tagName and name attributes of the Element and Attr interfaces, when applicable.
		///	Setting the prefix to null makes it unspecified, setting it to an empty string is implementation dependent.
		/// Note also that changing the prefix of an attribute that is known to have a default value, does not make a new attribute with the default value and the original prefix appear, since the namespaceURI and localName do not change.
		/// For nodes of any type other than ELEMENT_NODE and ATTRIBUTE_NODE and nodes created with a DOM Level 1 method, such as createElement from the Document interface, this is always null.
		/// </summary>
		public extern string Prefix { get; set; }

		/// <summary>
		/// The node immediately preceding this node. If there is no such node, this returns null.
		/// </summary>
		public extern Node PreviousSibling { get; }

		/// <summary>
		/// This attribute returns the text content of this node and its descendants. When it is defined to be null, setting it has no effect. On setting, any possible children this node may have are removed and, if it the new string is not empty or null, replaced by a single Text node containing the string this attribute is set to. 
		/// On getting, no serialization is performed, the returned string does not contain any markup. No whitespace normalization is performed and the returned string does not contain the white spaces in element content (see the attribute Text.isElementContentWhitespace). Similarly, on setting, no parsing is performed either, the input string is taken as pure textual content. 
		/// </summary>
		public extern string TextContent { get; }

		#endregion

		#region methods

		/// <summary>
		/// Adds the node newChild to the end of the list of children of this node. If the newChild is already in the tree, it is first removed.
		/// </summary>
		public extern Node AppendChild (Node newChild);

		/// <summary>
		/// Returns a duplicate of this node, i.e., serves as a generic copy constructor for nodes. The duplicate node has no parent (parentNode is null) and no user data. User data associated to the imported node is not carried over. However, if any UserDataHandlers has been specified along with the associated data these handlers will be called with the appropriate parameters before this method returns.
		/// Cloning an Element copies all attributes and their values, including those generated by the XML processor to represent defaulted attributes, but this method does not copy any children it contains unless it is a deep clone. This includes text contained in an the Element since the text is contained in a child Text node. Cloning an Attr directly, as opposed to be cloned as part of an Element cloning operation, returns a specified attribute (specified is true). Cloning an Attr always clones its children, since they represent its value, no matter whether this is a deep clone or not. Cloning an EntityReference automatically constructs its subtree if a corresponding Entity is available, no matter whether this is a deep clone or not. Cloning any other type of node simply returns a copy of this node.
		///	Note that cloning an immutable subtree results in a mutable copy, but the children of an EntityReference clone are readonly. In addition, clones of unspecified Attr nodes are specified. And, cloning Document, DocumentType, Entity, and Notation nodes is implementation dependent.
		/// </summary>
		public extern Node CloneNode (bool deep);

		/// <summary>
		/// Compares the reference node, i.e. the node on which this method is being called, with a node, i.e. the one passed as a parameter, with regard to their position in the document and according to the document order.
		/// </summary>
		public extern ushort CompareDocumentPosition (Node other);

		/// <summary>
		/// Returns an object which implements the specialized APIs of the specified feature and version, if any, or null if there is no object which implements interfaces associated with that feature. If the DOMObject returned by this method implements the Node interface, it must delegate to the primary core Node and not return results inconsistent with the primary core Node such as attributes, childNodes, etc.
		/// </summary>
		public extern object GetFeature (string name, string version);

		/// <summary>
		/// Retrieves the object associated to a key on a this node. The object must first have been set to this node by calling setUserData with the same key.
		/// </summary>
		public extern UserData GetUserData (string key);

		/// <summary>
		/// Returns whether this node (if it is an element) has any attributes.
		/// </summary>
		public extern bool HasAttributes ();

		/// <summary>
		/// Returns whether this node has any children.
		/// </summary>
		public extern bool HasChildNodes();

		/// <summary>
		/// Inserts the node newChild before the existing child node refChild. If refChild is null, insert newChild at the end of the list of children.
		/// If newChild is a DocumentFragment object, all of its children are inserted, in the same order, before refChild. If the newChild is already in the tree, it is first removed.
		/// </summary>
		public extern Node InsertBefore (Node newChild, Node refChild);

		/// <summary>
		/// This method checks if the specified namespaceURI is the default namespace or not.
		/// </summary>
		public extern bool IsDefaultNamespace (string namespaceURI);

		/// <summary>
		/// Tests whether two nodes are equal.
		/// This method tests for equality of nodes, not sameness (i.e., whether the two nodes are references to the same object) which can be tested with Node.isSameNode(). All nodes that are the same will also be equal, though the reverse may not be true.
		/// </summary>
		public extern bool IsEqualNode (Node arg);

		/// <summary>
		/// Returns whether this node is the same node as the given one.
		/// This method provides a way to determine whether two Node references returned by the implementation reference the same object. When two Node references are references to the same object, even if through a proxy, the references may be used completely interchangeably, such that all attributes have the same values and calling the same DOM method on either reference always has exactly the same effect.
		/// </summary>
		public extern bool IsSameNode (Node other);

		/// <summary>
		/// public extern bool IsSupported (string feature, string version);
		/// </summary>
		public extern bool IsSupported (string feature, string version);

		/// <summary>
		/// Look up the namespace URI associated to the given prefix, starting from this node.
		/// See Namespace URI Lookup for details on the algorithm used by this method.
		/// </summary>
		public extern string LookupNamespaceURI (string prefix);

		/// <summary>
		/// Puts all Text nodes in the full depth of the sub-tree underneath this Node, including attribute nodes, into a "normal" form where only structure (e.g., elements, comments, processing instructions, CDATA sections, and entity references) separates Text nodes, i.e., there are neither adjacent Text nodes nor empty Text nodes. This can be used to ensure that the DOM view of a document is the same as if it were saved and re-loaded, and is useful when operations (such as XPointer [XPointer] lookups) that depend on a particular document tree structure are to be used. If the parameter "normalize-characters" of the DOMConfiguration object attached to the Node.ownerDocument is true, this method will also fully normalize the characters of the Text nodes.
		/// </summary>
		public extern void Normalize();

		/// <summary>
		/// Removes the child node indicated by oldChild from the list of children, and returns it.
		/// </summary>
		public extern Node RemoveChild (Node oldChild);

		/// <summary>
		/// Replaces the child node oldChild with newChild in the list of children, and returns the oldChild node.
		/// If newChild is a DocumentFragment object, oldChild is replaced by all of the DocumentFragment children, which are inserted in the same order. If the newChild is already in the tree, it is first removed.
		/// </summary>
		public extern Node ReplaceChild (Node newChild, Node oldChild);

		/// <summary>
		/// Associate an object to a key on this node. The object can later be retrieved from this node by calling getUserData with the same key.
		/// </summary>
		public extern UserData SetUserData (string key, string data, UserDataHandler handler);

		#endregion
	}
}
