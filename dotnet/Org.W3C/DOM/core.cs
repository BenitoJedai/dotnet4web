

using System.Runtime.CompilerServices;

namespace Org.W3C.DOM
{

  public class DOMImplementation
  {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern DOMImplementation();
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern bool HasFeature(string feature, string version);

    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern DocumentType CreateDocumentType(string qualifiedName, string publicId, string systemId);
    
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Document CreateDocument(string namespaceURI, string qualifiedName, DocumentType doctype);

  };

  public class Node
  {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Node();
    public const ushort ELEMENT_NODE = 1;
    public const ushort ATTRIBUTE_NODE = 2;
    public const ushort TEXT_NODE = 3;
    public const ushort CDATA_SECTION_NODE = 4;
    public const ushort ENTITY_REFERENCE_NODE = 5;
    public const ushort ENTITY_NODE = 6;
    public const ushort PROCESSING_INSTRUCTION_NODE = 7;
    public const ushort COMMENT_NODE = 8;
    public const ushort DOCUMENT_NODE = 9;
    public const ushort DOCUMENT_TYPE_NODE = 10;
    public const ushort DOCUMENT_FRAGMENT_NODE = 11;
    public const ushort NOTATION_NODE = 12;

    public extern string NodeName
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
   public extern string NodeValue
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
      [MethodImpl(MethodImplOptions.InternalCall)]
      set;
    }


    public extern ushort NodeType
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

    public extern Node ParentNode
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

   public extern NodeList ChildNodes
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

    public extern Node FirstChild
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

    public extern Node LastChild
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
    public extern Node PreviousSibling
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

    public extern Node NextSibling
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

    public extern NamedNodeMap Attributes
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
    public extern Document OwnerDocument
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    

    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node InsertBebore( Node newChild,  Node refChild);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node ReplaceChild( Node newChild,  Node oldChild);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node RemoveChild(  Node oldChild);
    

    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node AppendChild(  Node newChild);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern bool HasChildNodes(  );
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node CloneNode(  bool deep);
    
  [MethodImpl(MethodImplOptions.InternalCall)]
    public extern void Normalize(  );
    
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node IsSupported( string feature, string version);
    
   
    
    public extern string NamespaceURI
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
    public extern string Prefix
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
      [MethodImpl(MethodImplOptions.InternalCall)]
      set;
    }

    public extern string LocalName
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }                                    
    
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern bool HasAttributes();
    
  };

  public class NodeList {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern NodeList();
    
  [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node Item(uint index);
    
	
    public extern uint Length
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
  };

  public class NamedNodeMap {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern NamedNodeMap();
    
    

    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node GetNamedItem(string name);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node SetNamedItem(string name, Node arg);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node RemoveNamedItem(string name);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node Item(uint index);
    
    
    public extern uint Length
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node GetNamedItemNS(string namespaceURI, string qualifiedName);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node SetNamedItemNS(Node arg);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node RemoveNamedItem(string namespaceURI,string localName);
    
  };

  public class CharacterData : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern CharacterData();
    
    public extern string Data
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
      [MethodImpl(MethodImplOptions.InternalCall)]
      set;
    }

    public extern uint Length
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

     [MethodImpl(MethodImplOptions.InternalCall)]
	public extern string SubstringData(uint offset, uint count);
    

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void AppendData(string arg);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void InsertData(uint offset, string arg);
	
	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void DeleteData(uint offset, uint count);

	[MethodImpl(MethodImplOptions.InternalCall)]
	public extern void ReplaceData(uint offset, uint count, string arg);
  };

  public class Attr : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Attr();
    public extern string Name
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    public extern bool Specified
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
public extern string Value
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
      [MethodImpl(MethodImplOptions.InternalCall)]
      set;
    }
          
    public extern Element OwnerElement
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
  };

  public class Element : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Element();
    
    public extern string TagName
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern string GetAttribute(string name);


	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern void SetAttribute(string name, string value);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern void RemoveAttribute(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Attr GetAttributeNode(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Attr SetAttributeNode(Attr newAttr);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Attr RemoveAttributeNode(Attr oldAttr);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern NodeList GetElementsByTagName(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern string GetAttributeNS(string namespaceURI, string localName);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern void SetAttributeNS(string namespaceURI,string quialifiedName, string value);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern void RemoveAttributeNS(string namespaceURI, string localName);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Attr GetAttributeNodeNS(string namespaceURI, string localName);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Attr SetAttributeNodeNS(Attr newAttr);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern NodeList GetElementsByTagNameNS(string namespaceURI, string localName);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern bool HasAttribute(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern bool HasAttributeNS(string namespaceURI, string localName);
  };

  public class Text : CharacterData {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Text();

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Text SplitText(uint offset);
  };

  public class Comment : CharacterData {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Comment();
  };

  public class CDATASection : Text {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern CDATASection();
  };

  public class DocumentType : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern DocumentType();
    
    public extern string Name
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
    public extern NamedNodeMap Entities
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
    public extern NamedNodeMap Notations
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
    public extern string PublicId
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
    public extern string SystemId
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
    public extern string InternalSubset
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
   
  };

  public class Notation : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Notation();
    
    public extern string PublicId
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    
    public extern string SystemId
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
  };

  public class Entity : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Entity();
    
    public extern string PublicId
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    public extern string SystemId
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    public extern string NotationName
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

  };

  public class EntityReference : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern EntityReference();
  };

  public class ProcessingInstruction : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern ProcessingInstruction();
    
    public extern string Target
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }

     public extern string data
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
      [MethodImpl(MethodImplOptions.InternalCall)]
      set;
    }
                                      

  };

  public class DocumentFragment : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern DocumentFragment();
  };

  public class Document : Node {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Document();
    
    public extern DocumentType DocType
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    public extern DOMImplementation Implementation
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    public extern Element DocumentElement
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
  
	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Element CreateElement(string tagName);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern DocumentFragment CreateDocumentFragment();


	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Text CreateTextNode(string data);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Comment CreateComment(string data);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern CDATASection CreateCDATASection(string data);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern ProcessingInstruction CreateProcessingInstruction(string target, string data);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern CDATASection CreateAttribute(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern EntityReference CreateEntityReference(string name);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern NodeList GetElementsByTagName(string tagname);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node ImportNode(Node importedNode, bool deep);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Element CreateElementNS(string namespaceURI, string quialifiedName);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Attr CreateAttributeNS(string namespaceURI, string quialifiedName);

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern NodeList GetElementsByTagNameNS(string namespaceURI, string localName);
	
	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Element GetElementById(string elementId);
  };
};

