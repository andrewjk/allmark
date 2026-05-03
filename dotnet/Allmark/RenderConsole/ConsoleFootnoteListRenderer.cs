namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleFootnoteListRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "footnote_list",
            Render = (node, state, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        if (state.Footnotes.Count == 0)
        {
            return;
        }
        state.Output.Append($"\n{Ansi.Dim}---{Ansi.Reset}\n");
        var number = 1;
        foreach (var footnote in state.Footnotes)
        {
            var label = number++;
            state.Output.Append($"{Ansi.Dim}[{label}]{Ansi.Reset} ");
            if (footnote.Info != null && state.FootnoteRefs.TryGetValue(footnote.Info, out var refNode))
            {
                RenderChildren.Execute(refNode, state);
            }
            var output = state.Output.ToString();
            if (output.EndsWith("\n"))
            {
                state.Output.Length -= 1;
            }
            state.Output.Append("\n");
        }
    }
}
