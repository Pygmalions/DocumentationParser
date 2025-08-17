using System.Xml;

namespace DocumentationParser.Xml;

/// <summary>
/// Documentation entry for exceptions.
/// </summary>
public class XmlExceptionEntry(XmlElement element) : IExceptionEntry
{
    public string TypeEntryName { get; } = element.GetAttribute("cref");

    public string Description { get; } = element.GetTrimmedInnerText();
}