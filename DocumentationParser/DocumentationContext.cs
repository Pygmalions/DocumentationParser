using System.Collections.Concurrent;

namespace DocumentationParser;

public class DocumentationContext : IDocumentationProvider
{
    private readonly ConcurrentDictionary<string, IEntry> _entries = new();

    /// <summary>
    /// Registered providers that can provide documentation entries.
    /// </summary>
    public LinkedList<IDocumentationProvider> Providers { get; } = [];

    /// <inheritdoc cref="IDocumentationProvider.GetEntry(string)"/>
    public IEntry? GetEntry(string entryName)
    {
        if (_entries.TryGetValue(entryName, out var entry))
            return entry;
        return Providers.Select(provider => provider.GetEntry(entryName))
            .FirstOrDefault(value => value is not null);
    }

    /// <summary>
    /// Set the entry for the specified entry name.
    /// </summary>
    /// <param name="entryName">Entry name.</param>
    /// <param name="entry">Entry instance.</param>
    public void SetEntry(string entryName, IEntry? entry)
    {
        if (entry == null)
        {
            _entries.TryRemove(entryName, out _);
            return;
        }

        _entries[entryName] = entry;
    }
}