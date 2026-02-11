namespace Allmark.Inline;

using Allmark.Types;

public static class TagMarksRule
{
	public static bool Execute(string name, string ch, InlineParserState state, MarkdownNode parent)
	{
		var start = state.I;
		var end = state.I;

		// Get the markup
		var markup = ch;
		for (var i = state.I + 1; i < state.Src.Length; i++)
		{
			if (state.Src[i].ToString() == ch)
			{
				markup += ch;
				end++;
			}
			else
			{
				break;
			}
		}

		// "Three or more tildes do not create a strikethrough"
		if (markup.Length < 3)
		{
			// TODO: Better space checks including start/end of line
			var codeBefore = start > 0 ? state.Src[start - 1] : default;
			var spaceBefore = start == 0 || Utils.IsUnicodeSpace(codeBefore);
			var punctuationBefore = !spaceBefore && Utils.IsUnicodePunctuation(codeBefore);

			var codeAfter = end + 1 < state.Src.Length ? state.Src[end + 1] : default;
			var spaceAfter = end == state.Src.Length - 1 || Utils.IsUnicodeSpace(codeAfter);
			var punctuationAfter = !spaceAfter && Utils.IsUnicodePunctuation(codeAfter);

			// "A left-flanking delimiter run is a delimiter run that is (1) not
			// followed by Unicode whitespace, and either (2a) not followed by a
			// punctuation character, or (2b) followed by a punctuation character
			// and preceded by Unicode whitespace or a punctuation character. For
			// purposes of this definition, the beginning and the end of the line
			// count as Unicode whitespace."
			var leftFlanking =
				!spaceAfter &&
				(!punctuationAfter || (punctuationAfter && (spaceBefore || punctuationBefore)));

			// "A right-flanking delimiter run is a delimiter run that is (1) not
			// preceded by Unicode whitespace, and either (2a) not preceded by a
			// punctuation character, or (2b) preceded by a punctuation character
			// and followed by Unicode whitespace or a punctuation character. For
			// purposes of this definition, the beginning and the end of the line
			// count as Unicode whitespace"
			var rightFlanking =
				!spaceBefore &&
				(!punctuationBefore || (punctuationBefore && (spaceAfter || punctuationAfter)));

			if (rightFlanking)
			{
				// TODO: Precedence
				// Loop backwards through delimiters to find a matching one that does
				// not take precedence
				Delimiter? startDelimiter = null;
				var i = state.Delimiters.Count;
				while (i-- > 0)
				{
					var prevDelimiter = state.Delimiters[i];
					if (!prevDelimiter.Handled)
					{
						if (prevDelimiter.Markup == ch && prevDelimiter.Length == markup.Length)
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

				// Check if it's a closing delimiter
				if (startDelimiter != null)
				{
					// Convert the text node into a delimited node with a new text
					// child followed by the other children of the parent (if any)
					var j = parent.Children?.Count ?? 0;
					while (j-- > 0)
					{
						var lastNode = parent.Children?[j];
						if (lastNode?.Index == startDelimiter.Start)
						{
							var text = Utils.NewNode("text", false, lastNode.Index, lastNode.Line, 1, ch, 0);
							text.Markup = lastNode.Markup.Substring(startDelimiter.Length) ?? "";

							lastNode.Type = name;
							lastNode.Markup = markup;
							var movedNodes = parent.Children!.Skip(j + 1).ToList() ?? [];
							parent.Children!.RemoveRange(j + 1, movedNodes.Count);
							lastNode.Children = [text, .. movedNodes];

							state.I += markup.Length;
							startDelimiter.Handled = true;

							return true;
						}
					}
				}
			}

			if (leftFlanking)
			{
				// Add a new text node which may turn into a delimiter
				var text = Utils.NewNode("text", false, start, state.Line, 1, markup, 0);
				parent.Children!.Add(text);

				state.I += markup.Length;
				state.Delimiters.Add(new Delimiter { Markup = ch, Start = start, Length = markup.Length });

				return true;
			}
		}

		Utils.AddMarkupAsText(markup, state, parent);

		return true;
	}
}
