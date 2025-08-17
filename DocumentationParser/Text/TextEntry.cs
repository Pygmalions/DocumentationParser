namespace DocumentationParser.Text;

public class TextEntry : IEntry
{
    public required string EntryName { get; init; }

    public required string MemberName { get; init; }

    public string? Summary { get; init; }

    public string? Remarks { get; init; }

    public string? Example { get; init; }

    public string? Code { get; init; }

    public string? Value { get; init; }

    public IReadOnlyList<string>? SeeAlsoEntryNames { get; init; }
}