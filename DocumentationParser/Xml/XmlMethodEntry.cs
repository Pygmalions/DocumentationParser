using System.Xml;

namespace DocumentationParser.Xml;

/// <summary>
/// Documentation entry for methods.
/// </summary>
public class XmlMethodEntry(XmlElement element) : XmlEntry(element), IMethodEntry
{
    public string? Returns { get; } = element.AggregateContent("./returns");

    public IReadOnlyList<IParameterEntry>? TypeParameters { get; } =
        element.SelectElements("./typeparam")?
            .Select(element => new XmlParameterEntry(element)).ToList();

    public IReadOnlyList<IParameterEntry>? Parameters { get; } =
        element.SelectElements("./param")?
            .Select(element => new XmlParameterEntry(element)).ToList();

    public IReadOnlyList<IExceptionEntry>? Exceptions { get; } =
        element.SelectElements("./exception")?
            .Select(element => new XmlExceptionEntry(element)).ToList();
}