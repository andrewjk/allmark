namespace Allmark.Inline;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class ExtendedAutolinkRule
{
    private static readonly Regex SpaceRegex = new(@"\s");
    private static readonly Regex UrlRegex = new(@"^(www\.([a-z0-9_-]\.*)+([a-z0-9-]\.*){0,2}[^\s<]*)", RegexOptions.IgnoreCase);
    private static readonly Regex ExtUrlRegex = new(@"^((https*|ftp):\/\/([a-z0-9_-]\.*)+([a-z0-9-]\.*){0,2}[^\s<]*)", RegexOptions.IgnoreCase);
    private static readonly Regex ExtEmailRegex = new(@"^([a-z0-9._\-+]+@([a-z0-9._\-+]+\.*)+)", RegexOptions.IgnoreCase);
    private static readonly Regex ExtXmppRegex = new(@"^((mailto|xmpp):[a-z0-9._\-+]+@([a-z0-9._\-+]+\.*)+(\/[a-z0-9@.]+){0,1})", RegexOptions.IgnoreCase);
    private static readonly Regex TrailingPunctuation = new(@"[?!.,:*_~]$");
    private static readonly Regex TrailingEntity = new(@"&[a-z0-9]+;$", RegexOptions.IgnoreCase);

    public static InlineRule Create()
    {
        return new InlineRule
        {
            Name = "extended_autolink",
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
        if (!state.IsEscaped)
        {
            if (ch == 'w')
            {
                var tail = state.Src.Substring(state.I);

                var urlMatch = UrlRegex.Match(tail);
                if (urlMatch.Success)
                {
                    var url = urlMatch.Groups[1].Value;

                    if (SpaceRegex.IsMatch(url))
                    {
                        var originalLength = urlMatch.Groups[0].Length;
                        var content = Utils.EscapeHtml(urlMatch.Groups[0].Value);
                        var text = Utils.NewText(state.ParentIndex + state.I, state.Line, content, state.Indent);
                        text.Length = originalLength;
                        parent.Children!.Add(text);
                        state.I += originalLength;

                        return true;
                    }

                    url = ExtendedValidation(url);
                    url = Utils.EscapeHtml(url);

                    var link = NewLink(url, state);
                    link.Info = $"http://{link.Info}";
                    parent.Children!.Add(link);
                    state.I += url.Length;

                    return true;
                }
            }

            if (ch == 'h' || ch == 'f')
            {
                var tail = state.Src.Substring(state.I);

                var urlMatch = ExtUrlRegex.Match(tail);
                if (urlMatch.Success)
                {
                    var url = urlMatch.Groups[1].Value;

                    if (SpaceRegex.IsMatch(url))
                    {
                        var content = Utils.EscapeHtml(urlMatch.Groups[0].Value);
                        var text = Utils.NewText(state.ParentIndex + state.I, state.Line, content, state.Indent);
                        parent.Children!.Add(text);
                        state.I += urlMatch.Groups[0].Length;

                        return true;
                    }

                    url = ExtendedValidation(url);
                    url = Utils.EscapeHtml(url);

                    var link = NewLink(url, state);
                    parent.Children!.Add(link);
                    state.I += url.Length;

                    return true;
                }
            }

            if (ch == '@')
            {
                // Find start of potential email (first space before @ or beginning of string)
                var start = 0;
                for (var i = state.I - 1; i >= 0; i--)
                {
                    var previousChar = Utils.GetChar(state.Src, i);
                    if (Utils.IsSpace(previousChar))
                    {
                        start = i + 1;
                        break;
                    }
                }

                var tail = state.Src.Substring(start);

                var emailMatch = ExtEmailRegex.Match(tail);
                if (emailMatch.Success)
                {
                    var url = emailMatch.Groups[1].Value;

                    var atIdx = url.IndexOf('@');
                    var plusIdx = url.IndexOf('+', atIdx);
                    var hasPlusAfterAt = atIdx != -1 && plusIdx != -1 && plusIdx > atIdx;

                    // "+ can occur before the @, but not after" "., -, and _ can
                    // occur on both sides of the @, but only . may occur at the end
                    // of the email address, in which case it will not be considered
                    // part of the address"
                    if (Regex.IsMatch(url, @"[-_]$") || hasPlusAfterAt)
                    {
                        return false;
                    }

                    url = Regex.Replace(url, @"\.$", "");

                    // Update the last node's content to remove characters already added
                    var lastNode = parent.Children!.Last();
                    lastNode.Content = lastNode.Content.Substring(0, lastNode.Content.Length - (state.I - start));

                    var link = NewLink(url, state);
                    link.Info = $"mailto:{link.Info}";
                    parent.Children!.Add(link);

                    state.I = start + url.Length;

                    return true;
                }
            }

            if (ch == 'm' || ch == 'x')
            {
                var tail = state.Src.Substring(state.I);

                var emailMatch = ExtXmppRegex.Match(tail);
                if (emailMatch.Success)
                {
                    var url = emailMatch.Groups[1].Value;

                    // "+ can occur before the @, but not after" "., -, and _ can
                    // occur on both sides of the @, but only . may occur at the end
                    // of the email address, in which case it will not be considered
                    // part of the address"
                    if (Regex.IsMatch(url, @"[-_]$") || url.IndexOf("+", url.IndexOf("@")) != -1)
                    {
                        var content = Utils.EscapeHtml(emailMatch.Groups[0].Value);
                        var text = Utils.NewText(state.ParentIndex + state.I, state.Line, content, state.Indent);
                        parent.Children!.Add(text);
                        state.I += emailMatch.Groups[0].Length;

                        return true;
                    }

                    url = Regex.Replace(url, @"\.$", "");

                    var link = NewLink(url, state);
                    parent.Children!.Add(link);
                    state.I += url.Length;

                    return true;
                }
            }
        }

        return false;
    }

    private static string ExtendedValidation(string url)
    {
        // "Trailing punctuation (specifically, ?, !, ., ,, :, *, _,
        // and ~) will not be considered part of the autolink,
        // though they may be included in the interior of the link"
        url = TrailingPunctuation.Replace(url, "");

        // "When an autolink ends in ), we scan the entire autolink for the total
        // number of parentheses. If there is a greater number of closing
        // parentheses than opening ones, we don't consider the unmatched trailing
        // parentheses part of the autolink, in order to facilitate including an
        // autolink inside a parenthesis"
        if (url.EndsWith(")"))
        {
            var trimCount = 0;
            var i = url.Length;
            var countingUp = true;
            while (i-- > 0)
            {
                if (countingUp)
                {
                    if (url[i] == ')')
                    {
                        trimCount++;
                    }
                    else
                    {
                        countingUp = false;
                    }
                }
                else
                {
                    if (url[i] == '(')
                    {
                        trimCount--;
                    }
                    if (trimCount == 0)
                    {
                        break;
                    }
                }
            }
            url = url.Substring(0, url.Length - trimCount);
        }

        // "If an autolink ends in a semicolon (;), we check to see if it appears to
        // resemble an entity reference; if the preceding text is & followed by one
        // or more alphanumeric characters. If so, it is excluded from the autolink"
        if (url.EndsWith(";"))
        {
            url = TrailingEntity.Replace(url, "");
        }

        return url;
    }

    private static MarkdownNode NewLink(string url, InlineParserState state)
    {
        var escapedUrl = url.Replace("\\", "\\\\");
        var decodedUrl = Utils.DecodeEntities(url);
        var encodedUrl = Utils.EscapeUriString(decodedUrl);

        var linkText = Utils.NewText(state.ParentIndex + state.I, state.Line, escapedUrl, state.Indent);

        var link = Utils.NewInline("link", state.ParentIndex + state.I, state.Line, "", state.Indent);
        link.Children = [linkText];
        link.Info = encodedUrl;
        link.Length = url.Length;

        return link;
    }
}
