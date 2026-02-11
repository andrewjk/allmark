namespace Allmark.Block;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class ParagraphRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "paragraph",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		if (parent.AcceptsContent)
		{
			return false;
		}

		if (parent.Type == "paragraph" && !parent.BlankAfter)
		{
			return false;
		}

		var endOfLine = Utils.GetEndOfLine(state);
		var content = state.Src.Substring(state.I, endOfLine - state.I);

		if (!Regex.IsMatch(content, @"[^\s]"))
		{
			state.I += content.Length;
			return true;
		}

		var paragraph = Utils.NewNode("paragraph", true, state.I, state.Line, 1, "", 0, []);
		paragraph.Content = content;
		state.I = endOfLine;

		if (state.HasBlankLine && parent.Children!.Count > 0)
		{
			parent.Children[^1].BlankAfter = true;
			state.HasBlankLine = false;
		}

		parent.Children!.Add(paragraph);
		state.OpenNodes.Push(paragraph);

		return true;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		if (state.HasBlankLine)
		{
			return false;
		}

		return true;
	}
}
