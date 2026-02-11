namespace Allmark;

using System.Text.RegularExpressions;
using Allmark.Types;

public static partial class Utils
{
	private static readonly Regex BlankLineRegex = new(@"\n[ \t]*\n");
	private static readonly Regex LineBreakRegex = new(@"[\r\n]");

	public static LinkReference? ParseLinkBlock(BlockParserState state, int start, string end)
	{
		// Consume spaces
		var spaces = ConsumeSpaces(state.Src, start);
		if (BlankLineRegex.IsMatch(spaces))
		{
			return null;
		}
		start += spaces.Length;

		// Get the url
		var url = "";
		if (GetChar(state.Src, start) == '<')
		{
			start++;
			for (int i = start; i < state.Src.Length; i++)
			{
				if (state.Src[i] == '>' && !IsEscaped(state.Src, i))
				{
					url = state.Src[start..i];
					start = i + 1;
					break;
				}
			}
		}
		else
		{
			for (int i = start; i <= state.Src.Length; i++)
			{
				if (i == state.Src.Length || IsSpace((int)state.Src[i]))
				{
					url = state.Src[start..i];
					start = i;
					break;
				}
			}
		}

		if (!string.IsNullOrEmpty(url))
		{
			// "The destination cannot contain line breaks, even if enclosed in pointy brackets"
			if (LineBreakRegex.IsMatch(url))
			{
				return null;
			}

			// "Both title and destination can contain backslash escapes and literal backslashes"
			url = DecodeEntities(url);
			url = EscapeBackslashes(url);
			url = EscapeUriString(Uri.UnescapeDataString(url));
		}

		// We may need to backtrack to here if there is an invalid title
		int urlEnd = start;

		// Consume spaces
		spaces = ConsumeSpaces(state.Src, start);
		start += spaces.Length;

		// Get the title
		var title = "";
		char delimiter = GetChar(state.Src, start);;
		if (delimiter == '\'' || delimiter == '"')
		{
			start++;
			for (int i = start; i < state.Src.Length; i++)
			{
				if (state.Src[i] == delimiter && !IsEscaped(state.Src, i))
				{
					title = state.Src[start..i];
					start = i + 1;
					break;
				}
			}
		}
		else if (delimiter == '(')
		{
			start++;
			int level = 1;
			for (int i = start; i < state.Src.Length; i++)
			{
				if (!IsEscaped(state.Src, i))
				{
					if (state.Src[i] == ')')
					{
						level--;
						if (level == 0)
						{
							title = state.Src[start..i];
							start = i + 1;
							break;
						}
					}
					else if (state.Src[i] == '(')
					{
						level++;
					}
				}
			}
		}

		// "The title may be omitted"
		// "The title may extend over multiple lines"
		if (!string.IsNullOrEmpty(title))
		{
			// "The title must be separated from the link destination by whitespace"
			if (spaces.Length == 0)
			{
				return null;
			}

			// "[The title] may not contain a blank line"
			if (BlankLineRegex.IsMatch(title))
			{
				return null;
			}

			// "Both title and destination can contain backslash escapes and literal backslashes"
			title = DecodeEntities(title);
			title = EscapeBackslashes(title);
			title = EscapeHtml(title);
		}

		// "[There may not be] non-whitespace characters after the title"
		if (!IsNewLine(state.Src[start - 1]))
		{
			for (; start < state.Src.Length; start++)
			{
				if (IsNewLine(state.Src[start]))
				{
					start++;
					break;
				}
				else if (IsSpace((int)state.Src[start]))
				{
					continue;
				}
				else
				{
					// If the title is on a new line, only it is ignored,
					// otherwise the whole link is ignored
					if (spaces.Contains('\n'))
					{
						title = "";
						start = urlEnd;
						break;
					}
					else
					{
						return null;
					}
				}
			}
		}

		state.I = start;

		return new LinkReference { Url = url, Title = title };
	}
}
