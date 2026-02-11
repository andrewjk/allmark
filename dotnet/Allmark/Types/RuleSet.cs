namespace Allmark.Types;

/// <summary>
/// A complete set of rules for parsing and rendering markdown.
/// </summary>
public record RuleSet
{
	public required Dictionary<string, BlockRule> Blocks { get; init; }
	public required Dictionary<string, InlineRule> Inlines { get; init; }
	public required Dictionary<string, Renderer> Renderers { get; init; }
}
