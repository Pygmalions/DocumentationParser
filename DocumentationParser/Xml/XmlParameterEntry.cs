using System.Xml;

namespace DocumentationParser.Xml;

/// <summary>
/// Documentation entry for parameters and type parameters.
/// </summary>
public class XmlParameterEntry(XmlElement element) : IParameterEntry
{
    public string ParameterName { get; } = element.GetAttribute("name");

    public string Description { get; } = element.InnerText;
}