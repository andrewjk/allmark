namespace Allmark.Inline;

using Allmark.Types;

public static class LinkRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "link",
			Test = TestLink,
		};
	}

	private static bool TestLink(InlineParserState state, MarkdownNode parent)
	{
		var ch = Utils.GetChar(state.Src, state.I);

		if (!Utils.IsEscaped(state.Src, state.I))
		{
			if (ch == '[')
			{
				return TestLinkOpen(state, parent);
			}

			if (ch == '!' && state.I + 1 < state.Src.Length && state.Src[state.I + 1] == '[')
			{
				return TestImageOpen(state, parent);
			}

			if (ch == ']')
			{
				return TestLinkClose(state, parent);
			}
		}

		return false;
	}

	private static bool TestLinkOpen(InlineParserState state, MarkdownNode parent)
	{
		var start = state.I;
		var markup = "[";

		// Add a new text node which may turn into a link
		var text = Utils.NewNode("text", false, start, state.Line, 1, markup, 0);
		parent.Children!.Add(text);

		state.I++;
		state.Delimiters.Add(new Delimiter { Markup = markup, Start = start, Length = 1 });

		return true;
	}

	private static bool TestImageOpen(InlineParserState state, MarkdownNode parent)
	{
		var start = state.I;
		var markup = "![";

		// Add a new text node which may turn into an image
		var text = Utils.NewNode("text", false, start, state.Line, 1, markup, 0);
		parent.Children!.Add(text);

		state.I += markup.Length;
		state.Delimiters.Add(new Delimiter { Markup = markup, Start = start, Length = 1 });

		return true;
	}

	private static bool TestLinkClose(InlineParserState state, MarkdownNode parent)
	{
		var markup = "]";

		// TODO: Standardize precedence
		Delimiter? startDelimiter = null;
		var i = state.Delimiters.Count;
		while (i-- > 0)
		{
			var prevDelimiter = state.Delimiters[i];
			if (!prevDelimiter.Handled)
			{
				if (prevDelimiter.Markup == "[" || prevDelimiter.Markup == "![")
				{
					startDelimiter = prevDelimiter;
					break;
				}
				else if (prevDelimiter.Markup == "*" || prevDelimiter.Markup == "_")
				{
					continue;
				}
				else
				{
					break;
				}
			}
		}

		if (startDelimiter != null)
		{
			// Convert the text node into a link node with a new text child
			// followed by the other children of the parent (if any)
			var j = parent.Children?.Count ?? 0;
			while (j-- > 0)
			{
				var lastNode = parent.Children?[j];
				if (lastNode?.Index == startDelimiter.Start)
				{
					var start = state.I + 1;
					var label = state.Src.Substring(
						startDelimiter.Start + startDelimiter.Markup.Length,
						state.I - (startDelimiter.Start + startDelimiter.Markup.Length));

					// "The link text may contain balanced brackets, but not
					// unbalanced ones, unless they are escaped"
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

					var isLink = startDelimiter.Markup == "[";

					var hasInfo = state.I + 1 < state.Src.Length && state.Src[state.I + 1] == '(';
					var hasRef = state.I + 1 < state.Src.Length && state.Src[state.I + 1] == '[';

					// "Full and compact references take precedence over shortcut references"
					// "Inline links also take precedence"
					LinkReference? link = null;
					if (hasInfo)
					{
						start++;
						link = Utils.ParseLinkInline(state, start, ")");
					}
					else if (hasRef)
					{
						start++;
						for (var k = start; k < state.Src.Length; k++)
						{
							if (state.Src[k] == ']')
							{
								// Lookup using the text between the [], or if there
								// is no text, use the label
								label = k - start > 0 ? state.Src.Substring(start, k - start) : label;
								label = Utils.NormalizeLabel(label);
								if (state.Refs.TryGetValue(label, out link))
								{
									state.I = k + 1;
								}
								break;
							}
						}
					}

					if (link == null)
					{
						label = Utils.NormalizeLabel(label);
						state.Refs.TryGetValue(label, out link);
						if (link != null)
						{
							state.I++;
						}
					}

					if (link != null)
					{
						var text = Utils.NewNode("text", false, lastNode.Index, lastNode.Line, 1, markup, 0);
						text.Markup = lastNode.Markup.Substring(startDelimiter.Markup.Length) ?? "";

						lastNode.Type = isLink ? "link" : "image";
						lastNode.Info = link.Url;
						lastNode.Title = link.Title;
						var movedNodes = parent.Children!.Skip(j + 1).ToList() ?? [];
						parent.Children!.RemoveRange(j + 1, movedNodes.Count);
						lastNode.Children = [text, .. movedNodes];

						// "[L]inks may not contain other links, at any level of nesting"
						if (isLink)
						{
							// Remove all the opening delimiters so they won't be picked up in future
							var d = state.Delimiters.Count;
							while (d-- > 0)
							{
								var prevDelimiter = state.Delimiters[d];
								if (prevDelimiter.Markup == "[" || prevDelimiter.Markup == "]")
								{
									prevDelimiter.Handled = true;
								}
							}
						}

						startDelimiter.Handled = true;
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
