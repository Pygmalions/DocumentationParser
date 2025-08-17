namespace DocumentationParser;

/// <summary>
/// Documentation entry for parameters and type parameters.
/// </summary>
public interface IParameterEntry
{
    /// <summary>
    /// Name of the parameter.
    /// </summary>
    public string ParameterName { get; }

    /// <summary>
    /// Description for the parameter.
    /// </summary>
    public string Description { get; }
}