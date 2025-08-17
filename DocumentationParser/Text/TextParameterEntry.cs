namespace DocumentationParser.Text;

public class TextParameterEntry : IParameterEntry
{
    public required string ParameterName { get; init; }

    public required string Description { get; init; }
}