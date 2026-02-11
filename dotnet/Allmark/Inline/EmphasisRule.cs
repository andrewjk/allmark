namespace Allmark.Inline;

using Allmark.Types;

public static class EmphasisRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "emphasis",
			Test = TestEmphasis,
		};
	}

	private static bool TestEmphasis(InlineParserState state, MarkdownNode parent)
	{
		var ch = Utils.GetChar(state.Src, state.I);
		if ((ch == '*' || ch == '_') && !Utils.IsEscaped(state.Src, state.I))
		{
			var start = state.I;
			var end = state.I;

			// Get the markup
			var markup = ch.ToString();
			for (var j = start + 1; j < state.Src.Length; j++)
			{
				if (state.Src[j] == ch)
				{
					markup += ch;
					end++;
				}
				else
				{
					break;
				}
			}

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

			// TODO: Precedence
			// Loop backwards through delimiters to find a matching one that does
			// not take precedence, and ideally has the same length
			Delimiter? startDelimiter = null;
			var startIndex = -1;
			var i = state.Delimiters.Count;
			while (i-- > 0)
			{
				var prevDelimiter = state.Delimiters[i];
				if (!prevDelimiter.Handled)
				{
					if (prevDelimiter.Markup == ch.ToString())
					{
						if (prevDelimiter.Length == markup.Length)
						{
							startDelimiter = prevDelimiter;
							startIndex = i;
							break;
						}
						else if (startDelimiter == null)
						{
							startDelimiter = prevDelimiter;
							startIndex = i;
						}
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

			// Check if it's a closing emphasis
			if (startDelimiter != null)
			{
				var canClose =
					(rightFlanking ||
						// Check if it's a continuing part of a three-run delimiter
						(state.I > 0 && state.Src[state.I - 1] == ch)) &&
					startDelimiter.Markup == ch.ToString() &&
					// "Emphasis with _ is not allowed inside words"
					(ch != '_' || spaceAfter || punctuationAfter) &&
					// "[A] delimiter that can both open and close ... cannot form
					// emphasis if the sum of the lengths of the delimiter runs
					// containing the opening and closing delimiters is a multiple
					// of 3 unless both lengths are multiples of 3."
					(!leftFlanking ||
						(markup.Length + startDelimiter.Length) % 3 != 0 ||
						(markup.Length % 3 == 0 && startDelimiter.Length % 3 == 0));
				if (canClose)
				{
					// Convert the text node into an emphasis node with a new text child
					// followed by the other children of the parent (if any)
					var j = parent.Children?.Count ?? 0;
					while (j-- > 0)
					{
						var lastNode = parent.Children?[j];
						if (lastNode?.Index == startDelimiter.Start)
						{
							// If it's longer than the last delimiter, or longer
							// than two, save some for the next go-round
							markup = markup.Substring(0, Math.Min(markup.Length, Math.Min(startDelimiter.Length, 2)));

							var text = Utils.NewNode("text", false, lastNode.Index, lastNode.Line, 1, ch.ToString(), 0);
							text.Markup = lastNode.Markup.Substring(startDelimiter.Length) ?? "";

							var movedNodes = parent.Children!.Skip(j + 1).ToList() ?? [];
							parent.Children!.RemoveRange(j + 1, movedNodes.Count);

							if (markup.Length < startDelimiter.Length)
							{
								lastNode.Markup = lastNode.Markup.Substring(0, startDelimiter.Length - markup.Length);
								var emphasis = Utils.NewNode(
									markup.Length == 2 ? "strong" : "emphasis",
									false,
									lastNode.Index + markup.Length,
									lastNode.Line,
									1,
									markup,
									0,
									[text, .. movedNodes]);
								parent.Children!.Add(emphasis);
							}
							else
							{
								lastNode.Type = markup.Length == 2 ? "strong" : "emphasis";
								lastNode.Markup = markup;
								lastNode.Children = [text, .. movedNodes];
							}

							state.I += markup.Length;

							// Mark delimiters between the start and end as handled,
							// as they can't start anything anymore
							var d = state.Delimiters.Count;
							while (d-- > 0)
							{
								if (d == startIndex)
								{
									break;
								}
								var prevDelimiter = state.Delimiters[d];
								prevDelimiter.Handled = true;
							}

							// Mark the start delimiter handled if all its chars are used up
							startDelimiter.Length -= markup.Length;
							if (startDelimiter.Length == 0)
							{
								startDelimiter.Handled = true;
							}

							return true;
						}
					}
				}
			}

			// Check if it's an opening emphasis
			var canOpen =
				leftFlanking &&
				// "Emphasis with _ is not allowed inside words"
				(ch != '_' || spaceBefore || punctuationBefore);
			if (canOpen)
			{
				// Add a new text node which may turn into emphasis
				var text = Utils.NewNode("text", false, start, state.Line, 1, markup, 0);
				parent.Children!.Add(text);

				state.I += markup.Length;
				state.Delimiters.Add(new Delimiter { Markup = ch.ToString(), Start = start, Length = markup.Length });

				return true;
			}

			Utils.AddMarkupAsText(markup, state, parent);

			return true;
		}

		return false;
	}
}
