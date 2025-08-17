namespace DocumentationParser.Text;

public class TextMethodEntry : TextMemberEntry, IMethodEntry
{
    public string? Returns { get; init; }

    public IReadOnlyList<IParameterEntry>? TypeParameters { get; init; }

    public IReadOnlyList<IParameterEntry>? Parameters { get; init; }

    public IReadOnlyList<IExceptionEntry>? Exceptions { get; init; }
}