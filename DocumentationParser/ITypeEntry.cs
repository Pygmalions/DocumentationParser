namespace DocumentationParser;

/// <summary>
/// Documentation entry for types.
/// A delegate type may have documentation for parameters and return value,
/// so this class inherits from <see cref="IMethodEntry"/> class.
/// </summary>
public interface ITypeEntry : IMethodEntry
{
}