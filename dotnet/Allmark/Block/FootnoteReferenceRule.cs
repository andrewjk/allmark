namespace Allmark.Block;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class FootnoteReferenceRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "footnote_ref",
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

		var ch = Utils.GetChar(state.Src, state.I);
		if (state.Indent <= 3 && ch == '[' && !Utils.IsEscaped(state.Src, state.I))
		{
			// "A footnote definition cannot interrupt a paragraph"
			if (parent.Type == "paragraph" && !parent.BlankAfter)
			{
				return false;
			}

			var start = state.I + 1;

			// Check for ^ that indicates a footnote (not a regular link reference)
			if (Utils.GetChar(state.Src, start) != '^')
			{
				return false;
			}
			start++;

			// Get the label
			var label = "";
			for (var i = start; i < state.Src.Length; i++)
			{
				if (!Utils.IsEscaped(state.Src, i))
				{
					if (Utils.GetChar(state.Src, i) == ']')
					{
						label = state.Src.Substring(start, i - start);
						start = i + 1;
						break;
					}

					// "Labels cannot contain brackets, unless they are
					// backslash-escaped"
					if (Utils.GetChar(state.Src, i) == '[')
					{
						return false;
					}
				}
			}
			// "A label must contain at least one non-whitespace character"
			if (string.IsNullOrEmpty(label) || !Regex.IsMatch(label, @"[^\s]"))
			{
				return false;
			}

			if (Utils.GetChar(state.Src, start) != ':')
			{
				return false;
			}
			start++;

			// Skip whitespace after colon
			while (start < state.Src.Length && IsSpace(Utils.GetChar(state.Src, start)))
			{
				start++;
			}

			state.I = start;

			// "Matching of labels is case-insensitive"
			label = Utils.NormalizeLabel(label);

			// "If there are several matching definitions, the first one takes
			// precedence"
			if (state.Footnotes.ContainsKey(label))
			{
				return true;
			}

			var refNode = Utils.NewNode("footnote_ref", true, state.I, state.Line, 1, "", 0, []);
			state.Footnotes[label] = new FootnoteReference
			{
				Label = label,
				Content = refNode
			};

			if (state.HasBlankLine && parent.Children!.Count > 0)
			{
				parent.Children[^1].BlankAfter = true;
				state.HasBlankLine = false;
			}

			parent.Children!.Add(refNode);
			state.OpenNodes.Push(refNode);

			state.HasBlankLine = false;
			Parse.ParseBlock.Execute(state, refNode);

			return true;
		}

		// Add another paragraph if there is an indent of at least 4 characters
		if (state.HasBlankLine && state.Indent >= 4 && parent.Children!.LastOrDefault()?.Type == "footnote_ref")
		{
			state.Indent = 0;
			Parse.ParseBlock.Execute(state, parent.Children!.Last());
			return true;
		}

		return false;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		if (state.HasBlankLine)
		{
			return false;
		}

		var openNode = state.OpenNodes.Peek();
		if (openNode.Type == "paragraph")
		{
			if (
				state.Indent >= 4 ||
				openNode.Content.EndsWith("  \n") ||
				// GitHub swallows link references after footnote references
				(Utils.GetChar(state.Src, state.I) == '[' && Utils.GetChar(state.Src, state.I + 1) != '^'))
			{
				// We won't know until we try more things
				state.MaybeContinue = true;
				node.MaybeContinuing = true;
				return true;
			}
		}

		return false;
	}

	private static bool IsSpace(char c)
	{
		return c == '\u0020' || c == '\u0009';
	}
}
