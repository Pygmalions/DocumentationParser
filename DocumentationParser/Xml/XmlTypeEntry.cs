using System.Xml;

namespace DocumentationParser.Xml;

/// <summary>
/// Documentation entry for types.
/// A delegate type may have documentation for parameters and return value,
/// so this class inherits from <see cref="XmlMethodEntry"/> class.
/// </summary>
public class XmlTypeEntry(XmlElement element) : XmlMethodEntry(element), ITypeEntry
{
}