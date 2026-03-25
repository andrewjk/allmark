namespace Allmark;

using Allmark.Types;

public static partial class Utils
{
    public static void AddMarkupAsText(string markup, InlineParserState state, MarkdownNode parent)
    {
        var lastNode = parent.Children!.LastOrDefault();
        var haveText = lastNode != null && lastNode.Type == "text";
        var text = haveText ? lastNode! : NewText(state.I, state.Line, "", 0);
        text.Content += markup;
        if (!haveText)
        {
            parent.Children ??= new List<MarkdownNode>();
            parent.Children.Add(text);
        }
        state.I += markup.Length;
    }
}
