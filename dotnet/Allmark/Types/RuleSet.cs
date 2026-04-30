namespace Allmark.Types;

/// <summary>
/// A complete set of rules for parsing and rendering markdown.
/// </summary>
public record RuleSet
{
    public required BlockRule[] Blocks { get; init; }
    public required InlineRule[] Inlines { get; init; }
}
