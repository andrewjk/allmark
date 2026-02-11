namespace Allmark.Inline;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class CodeSpanRule
{
	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "code_span",
			Test = TestCodeSpan,
		};
	}

	private static bool TestCodeSpan(InlineParserState state, MarkdownNode parent)
	{
		var ch = Utils.GetChar(state.Src, state.I);
		if (ch == '`' && !Utils.IsEscaped(state.Src, state.I))
		{
			var openMatched = 1;
			var openEnd = state.I + 1;
			for (; openEnd < state.Src.Length; openEnd++)
			{
				var nextChar = state.Src[openEnd];
				if (nextChar == ch)
				{
					openMatched++;
				}
				else
				{
					break;
				}
			}

			var markup = new string('`', openMatched);

			// "The contents of a code block are literal text, and do not get parsed as Markdown"
			var closeEnd = state.I + openMatched;
			closeEnd = Utils.SkipSpaces(state.Src, closeEnd);
			var closeMatched = 0;
			for (; closeEnd < state.Src.Length; closeEnd++)
			{
				if (state.Src[closeEnd] == ch)
				{
					for (; closeEnd < state.Src.Length; closeEnd++)
					{
						var nextChar = state.Src[closeEnd];
						if (nextChar == ch)
						{
							closeMatched++;
						}
						else
						{
							break;
						}
					}
					if (closeMatched == openMatched)
					{
						break;
					}
					closeMatched = 0;
				}
			}

			if (closeMatched == openMatched)
			{
				state.I += openMatched;

				var content = state.Src.Substring(state.I, closeEnd - closeMatched - state.I);

				// "[L]ine endings are converted to spaces"
				content = Regex.Replace(content, @"[\r\n]", " ");

				// "If the resulting string both begins and ends with a space
				// character, but does not consist entirely of space characters, a
				// single space character is removed from the front and back. This
				// allows you to include code that begins or ends with backtick
				// characters, which must be separated by whitespace from the
				// opening or closing backtick strings"
				//
				// "Only spaces, and not unicode whitespace in general, are stripped
				// in this way"
				if (
					Regex.IsMatch(content, @"[^\s]") &&
					Utils.IsSpace(content[0]) &&
					Utils.IsSpace(content[content.Length - 1]))
				{
					content = content.Substring(1, content.Length - 2);
				}

				var text = Utils.NewNode("text", false, state.I, state.Line, 1, content, 0);
				var code = Utils.NewNode("code_span", false, state.I, state.Line, 1, markup, 0, [text]);
				parent.Children!.Add(code);

				state.I = closeEnd;

				return true;
			}

			Utils.AddMarkupAsText(markup, state, parent);

			return true;
		}

		return false;
	}
}
