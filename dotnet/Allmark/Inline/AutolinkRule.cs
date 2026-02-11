namespace Allmark.Inline;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class AutolinkRule
{
	private static readonly Regex SpaceRegex = new(@"\s");
	private static readonly Regex LinkRegex = new(@"^<(\s*[a-z][a-z0-9+.-]{1,31}:[^<>]*)>", RegexOptions.IgnoreCase);
	private static readonly Regex EmailRegex = new(
		@"^<(\s*[a-z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?(?:\.[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?)*\s*)>",
		RegexOptions.IgnoreCase);

	public static InlineRule Create()
	{
		return new InlineRule
		{
			Name = "autolink",
			Test = TestAutolink,
		};
	}

	private static bool TestAutolink(InlineParserState state, MarkdownNode parent)
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

			var linkMatch = LinkRegex.Match(tail);
			if (linkMatch.Success)
			{
				var url = Utils.EscapeHtml(linkMatch.Groups[1].Value);

				if (SpaceRegex.IsMatch(url))
				{
					var text = Utils.NewNode("text", false, state.I, state.Line, 1, "", state.Indent);
					text.Markup = Utils.EscapeHtml(linkMatch.Groups[0].Value);
					parent.Children!.Add(text);
					state.I += linkMatch.Groups[0].Length;

					return true;
				}

				var html = Utils.NewNode("html_span", false, state.I, state.Line, 1, "", state.Indent);
				html.Content = $"<a href=\"{Utils.EscapeUriString(url)}\">{url}</a>";
				parent.Children!.Add(html);
				state.I += linkMatch.Groups[0].Length;

				return true;
			}

			var emailMatch = EmailRegex.Match(tail);
			if (emailMatch.Success)
			{
				var url = Utils.EscapeHtml(emailMatch.Groups[1].Value);

				if (SpaceRegex.IsMatch(url))
				{
					var text = Utils.NewNode("text", false, state.I, state.Line, 1, "", state.Indent);
					text.Markup = Utils.EscapeHtml(emailMatch.Groups[0].Value);
					parent.Children!.Add(text);
					state.I += emailMatch.Groups[0].Length;

					return true;
				}

				var html = Utils.NewNode("html_span", false, state.I, state.Line, 1, "", state.Indent);
				html.Content = $"<a href=\"mailto:{Utils.EscapeUriString(url)}\">{url}</a>";
				parent.Children!.Add(html);
				state.I += emailMatch.Groups[0].Length;

				return true;
			}
		}

		return false;
	}
}
