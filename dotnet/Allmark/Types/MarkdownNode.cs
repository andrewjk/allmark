namespace Allmark.Types;

/// <summary>
/// Represents a node in the markdown abstract syntax tree (AST).
/// </summary>
public record MarkdownNode
{
	public required string Type { get; set; }
	public required bool Block { get; set; }
	public required int Index { get; set; }

	/// <summary>
	/// The line number.
	/// </summary>
	public required int Line { get; set; }

	/// <summary>
	/// The column number.
	/// </summary>
	public required int Column { get; set; }

	/// <summary>
	/// The markdown-specific markup for this node as it has been entered by the user.
	/// </summary>
	public required string Markup { get; set; }

	/// <summary>
	/// The delimiter that has determined this node's type.
	/// </summary>
	public required string Delimiter { get; set; }

	/// <summary>
	/// The text content for this node.
	/// </summary>
	public required string Content { get; set; }

	/// <summary>
	/// The number of (logical, not physical) spaces this node starts after.
	/// </summary>
	public required int Indent { get; set; }

	/// <summary>
	/// For list item nodes, the number of (logical, not physical) spaces its content starts after.
	/// </summary>
	public required int Subindent { get; set; }

	/// <summary>
	/// Whether this node is followed by a blank line.
	/// </summary>
	public required bool BlankAfter { get; set; }

	/// <summary>
	/// Whether this node contains plain text content, rather than parsed Markdown.
	/// </summary>
	public required bool AcceptsContent { get; set; }

	/// <summary>
	/// Whether this node ends with a paragraph that may lazily continue.
	/// </summary>
	public required bool MaybeContinuing { get; set; }

	/// <summary>
	/// Info for a fenced code block, or the URL for a link.
	/// </summary>
	public string? Info { get; set; }

	/// <summary>
	/// The title for a link.
	/// </summary>
	public string? Title { get; set; }

	public List<MarkdownNode>? Children { get; set; }
}
