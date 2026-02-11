namespace Allmark;

using System.Text.RegularExpressions;
using Allmark.Types;

public static partial class Utils
{
	public static LinkReference? ParseLinkInline(InlineParserState state, int start, string end)
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
			int level = 1;
			for (int i = start; i < state.Src.Length; i++)
			{
				// "Any number of parentheses are allowed without escaping, as long
				// as they are balanced"
				if (state.Src[i] == ')' && !IsEscaped(state.Src, i))
				{
					level--;
					if (level == 0)
					{
						url = state.Src[start..i];
						start = i;
						break;
					}
				}
				else if (state.Src[i] == '(' && !IsEscaped(state.Src, i))
				{
					level++;
				}

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
			if (url.Contains('\r') || url.Contains('\n'))
			{
				return null;
			}

			// "Both title and destination can contain backslash escapes and literal backslashes"
			url = DecodeEntities(url);
			url = EscapeBackslashes(url);
			url = EscapeUriString(Uri.UnescapeDataString(url));
		}

		// Consume spaces
		spaces = ConsumeSpaces(state.Src, start);
		start += spaces.Length;

		// Get the title
		var title = "";
		char delimiter = GetChar(state.Src, start);;
		if (delimiter == ')')
		{
			// No title
		}
		else if (delimiter == '\'' || delimiter == '"')
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
		else
		{
			// Bad character
			return null;
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

		spaces = ConsumeSpaces(state.Src, start);
		start += spaces.Length;

		// "[There may not be] non-whitespace characters after the title"
		if (state.Src[start] != ')')
		{
			return null;
		}

		state.I = start + 1;

		return new LinkReference { Url = url, Title = title };
	}
}
