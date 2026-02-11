namespace Allmark.Types;

/// <summary>
/// Defines a rule for parsing inline markdown elements.
/// </summary>
public record InlineRule
{
	public required string Name { get; init; }
	public required Func<InlineParserState, MarkdownNode, bool> Test { get; init; }
}
