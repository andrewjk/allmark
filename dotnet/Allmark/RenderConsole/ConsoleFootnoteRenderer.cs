namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleFootnoteRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "footnote",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Dim;
        if (state.Footnotes.FirstOrDefault(f => f.Info == node.Info) == null)
        {
            state.Footnotes.Add(node);
        }
        var label = state.Footnotes.Count;
        state.Output.Append($"{style}[{label}]{Ansi.Reset}");
    }
}
