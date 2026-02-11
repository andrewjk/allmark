namespace Allmark.Inline;

using Allmark.Types;

public static class FootnoteRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "footnote",
			Test = TestFootnote,
		};
	}

	private static bool TestFootnote(InlineParserState state, MarkdownNode parent)
	{
		var ch = Utils.GetChar(state.Src, state.I);

		if (!Utils.IsEscaped(state.Src, state.I))
		{
			if (ch == '[')
			{
				return TestFootnoteOpen(state, parent);
			}

			if (ch == ']')
			{
				return TestFootnoteClose(state, parent);
			}
		}

		return false;
	}

	private static bool TestFootnoteOpen(InlineParserState state, MarkdownNode parent)
	{
		var start = state.I;

		// Check for [^ pattern which indicates a footnote reference
		if (Utils.GetChar(state.Src, start + 1) != '^')
		{
			return false;
		}

		var markup = "[^";

		// Add a new text node which may turn into a footnote
		var text = Utils.NewNode("text", false, start, state.Line, 1, markup, 0);
		parent.Children!.Add(text);

		state.I += 2;
		state.Delimiters.Add(new Delimiter { Markup = markup, Start = start, Length = 2 });

		return true;
	}

	private static bool TestFootnoteClose(InlineParserState state, MarkdownNode parent)
	{
		// Find the matching footnote delimiter
		Delimiter? startDelimiter = null;
		var i = state.Delimiters.Count;
		while (i-- > 0)
		{
			var prevDelimiter = state.Delimiters[i];
			if (!prevDelimiter.Handled)
			{
				if (prevDelimiter.Markup == "[^")
				{
					startDelimiter = prevDelimiter;
					break;
				}
			}
		}

		if (startDelimiter != null)
		{
			// Convert the text node into a footnote node
			var j = parent.Children?.Count ?? 0;
			while (j-- > 0)
			{
				var lastNode = parent.Children?[j];
				if (lastNode?.Index == startDelimiter.Start)
				{
					var label = state.Src.Substring(
						startDelimiter.Start + startDelimiter.Markup.Length,
						state.I - (startDelimiter.Start + startDelimiter.Markup.Length));

					// No special characters
					if (System.Text.RegularExpressions.Regex.IsMatch(label, @"[^a-zA-Z0-9]"))
					{
						return false;
					}

					// Check for balanced brackets
					var level = 0;
					for (var k = 0; k < label.Length; k++)
					{
						if (label[k] == '\\')
						{
							k++;
						}
						else if (label[k] == '[')
						{
							level++;
						}
						else if (label[k] == ']')
						{
							level--;
						}
					}
					if (level != 0)
					{
						return false;
					}

					// Swallow anything in brackets afterwards
					// Unless it's a link reference, in which case it should be treated as a link instead
					if (Utils.GetChar(state.Src, state.I + 1) == '[')
					{
						var start = state.I + 2;
						for (var k = start; k < state.Src.Length; k++)
						{
							if (state.Src[k] == ']')
							{
								var linkRef = state.Src.Substring(start, k - start);
								linkRef = Utils.NormalizeLabel(linkRef);
								if (state.Refs.ContainsKey(linkRef))
								{
									startDelimiter.Markup = "[";
									return false;
								}
								state.I = k;
								break;
							}
						}
					}

					// Normalize the label and look it up
					label = Utils.NormalizeLabel(label);
					if (state.Footnotes.TryGetValue(label, out var footnote))
					{
						state.I++;
						startDelimiter.Handled = true;

						// Create the footnote reference node with parsed children
						lastNode.Type = "footnote";
						lastNode.Info = label;
						lastNode.Markup = $"[^{label}]";
						lastNode.Children = footnote.Content.Children;

						// Parse the footnote content for inline elements
						var tempState = new InlineParserState
						{
							Rules = state.Rules,
							Src = lastNode.Content?.TrimEnd() ?? "",
							I = 0,
							Line = lastNode.Line,
							LineStart = 0,
							Indent = 0,
							Delimiters = [],
							Refs = state.Refs,
							Footnotes = state.Footnotes,
						};
						Parse.ParseInline.Execute(tempState, lastNode);

						return true;
					}

					startDelimiter.Handled = true;
					break;
				}
			}
		}

		return false;
	}
}
