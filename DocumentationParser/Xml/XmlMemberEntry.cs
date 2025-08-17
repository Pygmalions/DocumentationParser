using System.Xml;

namespace DocumentationParser.Xml;

/// <summary>
/// Documentation entry for members such as fields and properties.
/// </summary>
public class XmlMemberEntry(XmlElement element) : XmlEntry(element), IMemberEntry
{
}