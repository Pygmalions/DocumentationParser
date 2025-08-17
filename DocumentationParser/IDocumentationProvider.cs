using System.Reflection;

namespace DocumentationParser;

/// <summary>
/// Documentation is a collection of documentation entries.
/// </summary>
public interface IDocumentationProvider
{
    /// <summary>
    /// Get the documentation entry for the specified entry name.
    /// Use <see cref="EntryName"/> to get the entry name according to reflection information.
    /// </summary>
    /// <param name="entryName">Formatted name of the entry.</param>
    /// <returns>Entry with the specified entry name, or null if not found.</returns>
    IEntry? GetEntry(string entryName);
}

public static class DocumentationEntryNameExtensions
{
    public static ITypeEntry? GetEntry(this IDocumentationProvider fileDocumentation, Type type)
        => (ITypeEntry?)fileDocumentation.GetEntry(EntryName.Of(type));

    public static IMemberEntry? GetEntry(this IDocumentationProvider fileDocumentation, MemberInfo member)
        => (IMemberEntry?)fileDocumentation.GetEntry(EntryName.Of(member));

    public static IMethodEntry? GetEntry(this IDocumentationProvider fileDocumentation, MethodInfo method)
        => (IMethodEntry?)fileDocumentation.GetEntry(EntryName.Of(method));

    public static IMethodEntry? GetEntry(this IDocumentationProvider fileDocumentation, ConstructorInfo constructor)
        => (IMethodEntry?)fileDocumentation.GetEntry(EntryName.Of(constructor));

    public static IMemberEntry? GetEntry(this IDocumentationProvider fileDocumentation, FieldInfo field)
        => (IMemberEntry?)fileDocumentation.GetEntry(EntryName.Of(field));

    public static IMemberEntry? GetEntry(this IDocumentationProvider fileDocumentation, PropertyInfo property)
        => (IMemberEntry?)fileDocumentation.GetEntry(EntryName.Of(property));

    public static IMemberEntry? GetEntry(this IDocumentationProvider fileDocumentation, EventInfo @event)
        => (IMemberEntry?)fileDocumentation.GetEntry(EntryName.Of(@event));
}