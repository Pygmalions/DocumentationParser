using System.Diagnostics;
using System.Xml;

namespace DocumentationParser.Xml;

/// <summary>
/// This is the base class for documentation entries,
/// which contains common properties.
/// </summary>
/// <param name="element">Xml element.</param>
[DebuggerDisplay("XmlEntry: {MemberName}")]
public class XmlEntry(XmlElement element) : IEntry
{
    /// <summary>
    /// Entry name is the formatted name of the entry,
    /// which contains a symbol such as "T:" for types or "M:" for methods.
    /// </summary>
    public string EntryName { get; } = element.GetAttribute("name");

    /// <summary>
    /// Member name is the name of the corresponding member in the program.
    /// </summary>
    /// <remarks>Signatures of methods are not included in the name.</remarks>
    public string MemberName { get; } = element.GetAttribute("name").Split('(')[0].Split('.')[^1];

    public string? Summary { get; } = element.AggregateContent("./summary");

    public string? Remarks { get; } = element.AggregateContent("./remarks");

    public string? Example { get; } = element.AggregateContent("./example");

    public string? Code { get; } = element.AggregateContent("./code");

    public string? Value { get; } = element.AggregateContent("./value");

    /// <summary>
    /// Entry names of the see-also elements contained in this entry.
    /// </summary>
    public IReadOnlyList<string>? SeeAlsoEntryNames { get; } =
        element.SelectElements("//seealso")?
            .Select(element => element.GetAttribute("cref")).ToList();
}

public static class XmlElementExtensions
{
    public static string? AggregateContent(this XmlElement element, string xpath)
    {
        var nodes = element.SelectNodes(xpath);
        return nodes == null || nodes.Count == 0
            ? null
            : string.Join("\n", nodes.Cast<XmlElement>()
                .Select(node => node.GetTrimmedInnerText()));
    }

    public static IEnumerable<XmlElement>? SelectElements(this XmlElement element, string xpath)
        => element.SelectNodes(xpath)?.Cast<XmlElement>();

    public static string GetTrimmedInnerText(this XmlElement element)
        => element.InnerText.Trim();
}