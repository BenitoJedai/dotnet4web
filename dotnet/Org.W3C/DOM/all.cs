

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
    
    
    /*
    ????????????
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node GetNamedItem(  Node oldChild);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node getNamedItem(  Node arg);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node RemoveNamedItem(  Node oldChild);
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node Item(  Node oldChild);*/
    
    [MethodImpl(MethodImplOptions.InternalCall)]
    public extern Node RemoveChild(  Node oldChild);
    
    
    
    public extern uint Length
    {
      [MethodImpl(MethodImplOptions.InternalCall)]
      get;
    }
    /*
    ??????????
    // Introduced in DOM Level 2:
    Node               getNamedItemNS(in DOMString namespaceURI, 
                                      in DOMString localName);
    // Introduced in DOM Level 2:
    Node               setNamedItemNS(in Node arg)
                                        raises(DOMException);
    // Introduced in DOM Level 2:
    Node               removeNamedItemNS(in DOMString namespaceURI, 
                                         in DOMString localName)
                                        raises(DOMException);*/
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
	public extern Node SubstringData(uint offset, uint count);
    
    /*DOMString          substringData(in unsigned long offset, 
                                     in unsigned long count)
                                        raises(DOMException);
    void               appendData(in DOMString arg)
                                        raises(DOMException);
    void               insertData(in unsigned long offset, 
                                  in DOMString arg)
                                        raises(DOMException);
    void               deleteData(in unsigned long offset, 
                                  in unsigned long count)
                                        raises(DOMException);
    void               replaceData(in unsigned long offset, 
                                   in unsigned long count, 
                                   in DOMString arg)
                                        raises(DOMException);*/
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
    
    
    /*DOMString          getAttribute(in DOMString name);
    void               setAttribute(in DOMString name, 
                                    in DOMString value)
                                        raises(DOMException);
    void               removeAttribute(in DOMString name)
                                        raises(DOMException);
    Attr               getAttributeNode(in DOMString name);
    Attr               setAttributeNode(in Attr newAttr)
                                        raises(DOMException);
    Attr               removeAttributeNode(in Attr oldAttr)
                                        raises(DOMException);
    NodeList           getElementsByTagName(in DOMString name);
    // Introduced in DOM Level 2:
    DOMString          getAttributeNS(in DOMString namespaceURI, 
                                      in DOMString localName);
    // Introduced in DOM Level 2:
    void               setAttributeNS(in DOMString namespaceURI, 
                                      in DOMString qualifiedName, 
                                      in DOMString value)
                                        raises(DOMException);
    // Introduced in DOM Level 2:
    void               removeAttributeNS(in DOMString namespaceURI, 
                                         in DOMString localName)
                                        raises(DOMException);
    // Introduced in DOM Level 2:
    Attr               getAttributeNodeNS(in DOMString namespaceURI, 
                                          in DOMString localName);
    // Introduced in DOM Level 2:
    Attr               setAttributeNodeNS(in Attr newAttr)
                                        raises(DOMException);
    // Introduced in DOM Level 2:
    NodeList           getElementsByTagNameNS(in DOMString namespaceURI, 
                                              in DOMString localName);
    // Introduced in DOM Level 2:
    boolean            hasAttribute(in DOMString name);
    // Introduced in DOM Level 2:
    boolean            hasAttributeNS(in DOMString namespaceURI, 
                                      in DOMString localName);*/
  };

  public class Text : CharacterData {
    [MethodImpl(MethodImplOptions.InternalCall)]
    internal extern Text();
    /*Text               splitText(in unsigned long offset)
                                        raises(DOMException);*/
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
                                        // raises(DOMException) on setting

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
    
    /*Element            createElement(in DOMString tagName)
                                        raises(DOMException);
    DocumentFragment   createDocumentFragment();*/

	[MethodImpl(MethodImplOptions.InternalCall)]
    public extern Text CreateTextNode(string data);


		/*
    Comment            createComment(in DOMString data);
    CDATASection       createCDATASection(in DOMString data)
                                        raises(DOMException);
    ProcessingInstruction createProcessingInstruction(in DOMString target, 
                                                      in DOMString data)
                                        raises(DOMException);
    Attr               createAttribute(in DOMString name)
                                        raises(DOMException);
    EntityReference    createEntityReference(in DOMString name)
                                        raises(DOMException);
    NodeList           getElementsByTagName(in DOMString tagname);
    // Introduced in DOM Level 2:
    Node               importNode(in Node importedNode, 
                                  in boolean deep)
                                        raises(DOMException);
    // Introduced in DOM Level 2:
    Element            createElementNS(in DOMString namespaceURI, 
                                       in DOMString qualifiedName)
                                        raises(DOMException);
    // Introduced in DOM Level 2:
    Attr               createAttributeNS(in DOMString namespaceURI, 
                                         in DOMString qualifiedName)
                                        raises(DOMException);
    // Introduced in DOM Level 2:
    NodeList           getElementsByTagNameNS(in DOMString namespaceURI, 
                                              in DOMString localName);
    // Introduced in DOM Level 2:
    Element            getElementById(in DOMString elementId);*/
  };
};

