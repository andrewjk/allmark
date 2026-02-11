namespace Allmark.Types;

/// <summary>
/// Represents the state of the inline parser during markdown parsing.
/// </summary>
public record InlineParserState
{
	public required Dictionary<string, InlineRule> Rules { get; set; }

	public required string Src { get; set; }
	public required int I { get; set; }
	public required int Line { get; set; }
	public required int LineStart { get; set; }
	public required int Indent { get; set; }
	public required List<Delimiter> Delimiters { get; set; }
	public required Dictionary<string, LinkReference> Refs { get; set; }
	public required Dictionary<string, FootnoteReference> Footnotes { get; set; }

	// HACK:
	public bool? Debug { get; set; }
}
