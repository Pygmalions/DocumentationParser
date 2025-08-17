namespace DocumentationParser;

/// <summary>
/// Documentation entry for methods.
/// </summary>
public interface IMethodEntry : IEntry
{
    /// <summary>
    /// Documentation about the return value of the method.
    /// </summary>
    public string? Returns { get; }

    /// <summary>
    /// Documentation about the type parameters of the method.
    /// </summary>
    public IReadOnlyList<IParameterEntry>? TypeParameters { get; }

    /// <summary>
    /// Documentation about the parameters of the method.
    /// </summary>
    public IReadOnlyList<IParameterEntry>? Parameters { get; }

    /// <summary>
    /// Documentation about the exceptions that may be thrown by the method.
    /// </summary>
    public IReadOnlyList<IExceptionEntry>? Exceptions { get; }
}