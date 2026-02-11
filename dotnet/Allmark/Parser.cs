namespace Allmark;

using Allmark.Parse;
using Allmark.Types;

public static class Parser
{
	public static MarkdownNode Execute(string src, RuleSet rules, bool debug = false)
	{
		var document = Utils.NewNode("document", true, 0, 1, 1, "", 0, []);

		var state = new BlockParserState
		{
			Rules = rules.Blocks,
			Src = src,
			I = 0,
			Line = 0,
			LineStart = 0,
			Indent = 0,
			MaybeContinue = false,
			HasBlankLine = false,
			OpenNodes = new Stack<MarkdownNode>(new[] { document }),
			Refs = new Dictionary<string, LinkReference>(),
			Footnotes = new Dictionary<string, FootnoteReference>(),
			Debug = debug
		};

		// Stage 1 -- parse each line into blocks
		while (state.I < state.Src.Length)
		{
			ParseLine.Execute(state);
		}

		// TODO: Close the open nodes?

		// Stage 2 -- parse the inlines for each block
		ParseBlockInlines.Execute(document, rules.Inlines, state.Refs, state.Footnotes);

		return document;
	}
}
