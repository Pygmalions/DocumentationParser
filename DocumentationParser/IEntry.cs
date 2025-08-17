namespace DocumentationParser;

/// <summary>
/// This interface contains the basic and common documentation properties.
/// </summary>
public interface IEntry
{
    /// <summary>
    /// Entry name is the formatted name of the entry,
    /// which contains a symbol such as "T:" for types or "M:" for methods.
    /// </summary>
    public string EntryName { get; }

    /// <summary>
    /// Member name is the name of the corresponding member in the program.
    /// </summary>
    /// <remarks>Signatures of methods are not included in the name.</remarks>
    public string MemberName { get; }

    public string? Summary { get; }

    public string? Remarks { get; }

    public string? Example { get; }

    public string? Code { get; }

    public string? Value { get; }

    /// <summary>
    /// Entry names of the see-also elements contained in this entry.
    /// </summary>
    public IReadOnlyList<string>? SeeAlsoEntryNames { get; }
}