namespace Allmark.Types;

/// <summary>
/// Defines a rule for parsing block-level markdown elements.
/// </summary>
public record BlockRule
{
	/// <summary>
	/// The name for this rule, which must also be used for nodes created by this rule.
	/// </summary>
	public required string Name { get; init; }

	/// <summary>
	/// Tests whether a node should start e.g. a block quote should start when we find a '>'.
	/// </summary>
	public required Func<BlockParserState, MarkdownNode, bool> TestStart { get; init; }

	/// <summary>
	/// Tests whether a node should continue after being started e.g. a block quote should continue if we find a '>'.
	/// </summary>
	public required Func<BlockParserState, MarkdownNode, bool> TestContinue { get; init; }

	/// <summary>
	/// Does any cleanup for this rule's node when it is closed.
	/// </summary>
	public Action<BlockParserState, MarkdownNode>? CloseNode { get; init; }
}
