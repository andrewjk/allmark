namespace Allmark.Types;

/// <summary>
/// Represents a footnote reference in markdown.
/// </summary>
public record FootnoteReference
{
	public required string Label { get; init; }
	public required MarkdownNode Content { get; init; }
}
