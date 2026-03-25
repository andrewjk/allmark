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
                    var content = Utils.EscapeHtml(linkMatch.Groups[0].Value);
                    var text = Utils.NewText(state.ParentIndex + state.I, state.Line, content, state.Indent);
                    text.Length = linkMatch.Groups[0].Length;
                    parent.Children!.Add(text);
                    state.I += linkMatch.Groups[0].Length;

                    return true;
                }

                var escapedUrl = url.Replace("\\", "\\\\");
                var decodedUrl = Utils.DecodeEntities(url);
                var encodedUrl = Utils.EscapeUriString(decodedUrl);

                var linkText = Utils.NewText(state.ParentIndex + state.I, state.Line, escapedUrl, state.Indent);

                var link = Utils.NewInline("link", state.ParentIndex + state.I, state.Line, "", state.Indent);
                link.Info = encodedUrl;
                link.Length = linkMatch.Groups[0].Length;
                link.Children = [linkText];
                parent.Children!.Add(link);
                state.I += linkMatch.Groups[0].Length;

                return true;
            }

            var emailMatch = EmailRegex.Match(tail);
            if (emailMatch.Success)
            {
                var url = Utils.EscapeHtml(emailMatch.Groups[1].Value);

                if (SpaceRegex.IsMatch(url))
                {
                    var content = Utils.EscapeHtml(emailMatch.Groups[0].Value);
                    var text = Utils.NewText(state.ParentIndex + state.I, state.Line, content, state.Indent);
                    text.Length = emailMatch.Groups[0].Length;
                    parent.Children!.Add(text);
                    state.I += emailMatch.Groups[0].Length;

                    return true;
                }

                var decodedUrl = Utils.DecodeEntities(url);
                var encodedUrl = Utils.EscapeUriString(decodedUrl);

                var linkText = Utils.NewText(state.ParentIndex + state.I, state.Line, url, state.Indent);

                var link = Utils.NewInline("link", state.ParentIndex + state.I, state.Line, "", state.Indent);
                link.Info = $"mailto:{encodedUrl}";
                link.Length = emailMatch.Groups[0].Length;
                link.Children = [linkText];
                link.Children = [linkText];
                parent.Children!.Add(link);
                state.I += emailMatch.Groups[0].Length;

                return true;
            }
        }

        return false;
    }
}
