namespace Allmark.Types;

/// <summary>
/// Represents the state of the block parser during markdown parsing.
/// </summary>
public record BlockParserState
{
	public required Dictionary<string, BlockRule> Rules { get; set; }

	public required string Src { get; set; }
	public required int I { get; set; }
	public required int Line { get; set; }
	public required int LineStart { get; set; }
	public required int Indent { get; set; }
	public required Stack<MarkdownNode> OpenNodes { get; set; }
	public required bool MaybeContinue { get; set; }
	public required bool HasBlankLine { get; set; }
	public required Dictionary<string, LinkReference> Refs { get; set; }
	public required Dictionary<string, FootnoteReference> Footnotes { get; set; }

	// HACK:
	public bool? Debug { get; set; }
}
