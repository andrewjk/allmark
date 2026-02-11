namespace Allmark.Block;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class HeadingUnderlineRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "heading_underline",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		if (state.MaybeContinue)
		{
			for (var i = 0; i < state.OpenNodes.Count - 1; i++)
			{
				var node = state.OpenNodes.ElementAt(i);
				if (node.MaybeContinuing)
				{
					return false;
				}
			}
		}

		var ch = Utils.GetChar(state.Src, state.I);
		if (state.Indent <= 3 && (ch == '=' || ch == '-'))
		{
			var matched = 1;
			var end = state.I + 1;
			for (; end < state.Src.Length; end++)
			{
				var nextChar = state.Src[end];
				if (nextChar == ch)
				{
					// "The setext heading underline cannot contain internal spaces"
					if (matched > 0 && Utils.IsSpace(state.Src[end - 1]))
					{
						return false;
					}
					matched++;
				}
				else if (Utils.IsNewLine(nextChar))
				{
					// TODO: Handle windows crlf
					end++;
					break;
				}
				else if (Utils.IsSpace(nextChar))
				{
					continue;
				}
				else
				{
					return false;
				}
			}

			// HACK: Special case for an underlined heading in a list
			// Maybe do this with interrupts?
			if (parent.Type == "list_item" && !parent.BlankAfter && state.Indent == parent.Indent)
			{
				state.OpenNodes.Pop();
				state.OpenNodes.Pop();
				parent = state.OpenNodes.Peek();
			}

			var haveParagraph =
				parent.Type == "paragraph" && !parent.BlankAfter && Regex.IsMatch(parent.Content ?? "", @"[^\s]");
			if (haveParagraph)
			{
				parent.Type = "heading";
				parent.Markup = state.Src.Substring(state.I, end - state.I);
				state.I = end;
				return true;
			}
		}

		return false;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		return false;
	}
}
