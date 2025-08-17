using System.Diagnostics;
using System.Reflection;
using System.Xml;

namespace DocumentationParser.Xml;

[DebuggerDisplay("XmlDocumentation: {AssemblyName}")]
public class XmlDocumentation : IDocumentationProvider
{
    /// <summary>
    /// Dictionary of unparsed XML elements.
    /// </summary>
    private readonly Dictionary<string, XmlElement> _elements = new();

    /// <summary>
    /// Dictionary of unparsed XML elements.
    /// </summary>
    private readonly Dictionary<string, XmlEntry> _entries = new();

    private readonly List<string> _names = [];

    /// <summary>
    /// Name of the assembly that this documentation belongs to.
    /// </summary>
    public readonly string AssemblyName;

    public XmlDocumentation(XmlDocument document)
    {
        AssemblyName = document.SelectSingleNode("/doc/assembly/name")?.InnerText ??
                       throw new Exception("The documentation does not contain an assembly name.");
        var members = document.SelectNodes("/doc/members/member");
        if (members == null)
            return;
        foreach (var node in members.Cast<XmlElement>())
        {
            var name = node.GetAttribute("name");
            _names.Add(name);
            _elements[name] = node;
        }
    }

    /// <summary>
    /// List of entry names of this documentation.
    /// </summary>
    public IReadOnlyList<string> EntryNames => _names;

    /// <summary>
    /// Get the documentation entry for the specified entry name.
    /// Use <see cref="EntryName"/> to get the entry name according to reflection information.
    /// </summary>
    /// <param name="entryName">Formatted name of the entry.</param>
    /// <returns>Entry with the specified entry name, or null if not found.</returns>
    /// <exception cref="Exception">
    /// Throws an exception if the entry name is invalid.
    /// </exception>
    public IEntry? GetEntry(string entryName)
    {
        if (_entries.TryGetValue(entryName, out var entry))
            return entry;
        if (!_elements.Remove(entryName, out var element))
            return null;
        var symbol = entryName[0];
        entry = symbol switch
        {
            'T' => new XmlTypeEntry(element),
            'M' => new XmlMethodEntry(element),
            'P' => new XmlMemberEntry(element),
            'F' => new XmlMemberEntry(element),
            'E' => new XmlMemberEntry(element),
            _ => throw new Exception("Invalid entry name with an unknown symbol.")
        };
        _entries[entryName] = entry;
        return entry;
    }

    /// <summary>
    /// Try to load a documentation file with the same name to the assembly
    /// from the directory of the specified assembly.
    /// MSBuild usually copies the generated XML documentation file to the output directory
    /// along with DLL files.
    /// </summary>
    /// <param name="assembly">
    /// Assembly to load documentation for.
    /// </param>
    /// <returns>Loaded documentation, or null if not found.</returns>
    public static XmlDocumentation? LoadForAssembly(Assembly assembly)
    {
        // Try to search under current working directory.
        var file = $"{assembly.GetName().Name}.xml";
        if (!File.Exists(file))
        {
            // Try to search under the directory of the specified assembly.
            file = Path.GetDirectoryName(assembly.Location) + Path.DirectorySeparatorChar + file;
            if (!File.Exists(file))
                return null;
        }

        var document = new XmlDocument();
        document.Load(file);
        return new XmlDocumentation(document);
    }
}

public static class DocumentationFileExtensions
{
    public static DocumentationContext WithXmlDocumentation(this DocumentationContext context, string path)
    {
        var document = new XmlDocument();
        document.Load(path);
        context.Providers.AddLast(new XmlDocumentation(document));
        return context;
    }

    public static DocumentationContext WithXmlDocumentation(this DocumentationContext context, Assembly assembly)
    {
        var documentation = XmlDocumentation.LoadForAssembly(assembly);
        if (documentation != null)
            context.Providers.AddLast(documentation);
        return context;
    }
}