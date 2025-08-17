namespace DocumentationParser.Text;

public class TextExceptionEntry : IExceptionEntry
{
    public required string TypeEntryName { get; init; }

    public required string Description { get; init; }
}