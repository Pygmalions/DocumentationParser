namespace DocumentationParser;

/// <summary>
/// Documentation entry for exceptions.
/// </summary>
public interface IExceptionEntry
{
    /// <summary>
    /// Formatted entry name defined in the "cref" attribute,
    /// points to the documentation for the exception type.
    /// </summary>
    public string TypeEntryName { get; }

    /// <summary>
    /// Documentation for the exception, such as when and why the exception is thrown.
    /// </summary>
    public string Description { get; }
}