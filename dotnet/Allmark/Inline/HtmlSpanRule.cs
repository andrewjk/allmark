namespace Allmark.Inline;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class HtmlSpanRule
{
	private static readonly Regex HtmlTagRegex = new(
		$"^(?:{HtmlPatterns.OpenTag}|{HtmlPatterns.CloseTag}|{HtmlPatterns.Comment}|{HtmlPatterns.Instruction}|{HtmlPatterns.Declaration}|{HtmlPatterns.Cdata})",
		RegexOptions.Compiled);

	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "html_span",
			Test = TestHtmlSpan,
		};
	}

	private static bool TestHtmlSpan(InlineParserState state, MarkdownNode parent)
	{
		// Don't try to extract HTML for HTML blocks
		if (parent.Type == "html_block")
		{
			return false;
		}

		var ch = Utils.GetChar(state.Src, state.I);
		if (ch == '<' && !Utils.IsEscaped(state.Src, state.I))
		{
			var tail = state.Src.Substring(state.I);
			var match = HtmlTagRegex.Match(tail);
			if (match.Success)
			{
				var content = match.Groups[0].Value;
				var html = Utils.NewNode("html_span", false, state.I, state.Line, 1, "", state.Indent);
				html.Content = content;
				parent.Children!.Add(html);
				state.I += match.Groups[0].Length;
				return true;
			}
		}

		return false;
	}
}
