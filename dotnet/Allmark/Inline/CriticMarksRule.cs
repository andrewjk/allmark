namespace Allmark.Inline;

using Allmark.Types;

public static class CriticMarksRule
{
    public static bool Execute(string name, string delimiter, InlineParserState state, MarkdownNode parent, int precedence, string? closingDelimiter = null)
    {
        var closeDel = closingDelimiter ?? delimiter;
        var ch = Utils.GetChar(state.Src, state.I);
        if (ch == '{' && !Utils.IsEscaped(state.Src, state.I))
        {
            var start = state.I;
            var end = state.I;

            // Get the markup
            var markup = ch.ToString();
            for (var i = start + 1; i < state.Src.Length; i++)
            {
                if (state.Src[i].ToString() == delimiter)
                {
                    markup += delimiter;
                    end++;
                }
                else if (state.Src[i] == '}' || (closeDel != delimiter && state.Src[i].ToString() == closeDel))
                {
                    return false;
                }
                else
                {
                    break;
                }
            }

            if (markup.Length == 2 || markup.Length == 3)
            {
                // Add a new text node which may turn into deletion
                 var text = Utils.NewText(state.ParentIndex + start, state.Line, markup, 0);
                parent.Children!.Add(text);

                // Add the start delimiter
                state.I += markup.Length;
                state.Delimiters.Add(new Delimiter { Markup = markup, Start = start, Length = markup.Length, Precedence = precedence });

                return true;
            }
        }
        else if (ch.ToString() == closeDel && !Utils.IsEscaped(state.Src, state.I))
        {
            // Get the markup
            var markup = "{" + delimiter;
            for (var i = state.I + 1; i < state.Src.Length; i++)
            {
                if (state.Src[i].ToString() == closeDel)
                {
                    markup += delimiter;
                }
                else if (state.Src[i] == '}')
                {
                    break;
                }
                else
                {
                    return false;
                }
            }

            if (markup.Length == 2 || markup.Length == 3)
            {
                // Loop backwards through delimiters to find a matching one that
                // does not take precedence
                Delimiter? startDelimiter = null;
                var i = state.Delimiters.Count;
                while (i-- > 0)
                {
                    var prevDelimiter = state.Delimiters[i];
                    if (!prevDelimiter.Handled && prevDelimiter.Markup == markup)
                    {
                        startDelimiter = prevDelimiter;
                        break;
                    }
                }

                // Check if it's a closing deletion
                if (startDelimiter != null)
                {
                    // Convert the text node into a deletion node with a new text
                    // child followed by the other children of the parent (if any)
                    var j = parent.Children?.Count ?? 0;
                    while (j-- > 0)
                    {
                        var lastNode = parent.Children?[j];
                        if (lastNode?.Index == state.ParentIndex + startDelimiter.Start)
                        {
                            var newText = lastNode.Content.Substring(startDelimiter.Length) ?? "";
                            var text = Utils.NewText(lastNode.Index, lastNode.Line, newText, 0);

                            lastNode.Type = name;
                            lastNode.Markup = markup;
                            lastNode.Length = state.ParentIndex + state.I - lastNode.Index + markup.Length;
                            var movedNodes = parent.Children!.Skip(j + 1).ToList() ?? [];
                            parent.Children!.RemoveRange(j + 1, movedNodes.Count);
                            lastNode.Children = [text, .. movedNodes];

                            state.I += markup.Length;
                            startDelimiter.Handled = true;

                            return true;
                        }
                    }

                    // TODO: Precedence!
                    // TODO: Should mark all delimiters between the tags as handled...
                }
            }
        }

        return false;
    }
}
