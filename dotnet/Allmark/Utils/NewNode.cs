namespace Allmark;

using Allmark.Types;

public static partial class Utils
{
	// TODO: Move a lot of functionality into here
	// e.g. checking maybeContinue, checking if last node needs closing etc

	public static MarkdownNode NewNode(string type, bool block, int index, int line, int column, string markup, int indent, List<MarkdownNode>? children = null)
	{
		return new MarkdownNode
		{
			Type = type,
			Block = block,
			Index = index,
			Line = line,
			Column = column,
			Markup = markup,
			Delimiter = "",
			Content = "",
			Indent = indent,
			Subindent = 0,
			AcceptsContent = false,
			MaybeContinuing = false,
			BlankAfter = false,
			Children = children
		};
	}
}
