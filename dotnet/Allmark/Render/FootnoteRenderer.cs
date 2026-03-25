namespace Allmark.Render;

using Allmark.Types;

public static class FootnoteRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "footnote",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        if (!state.Footnotes.Any((f) => f.Info == node.Info))
        {
            state.Footnotes.Add(node);
        }
        var label = state.Footnotes.Count;
        var id = $"fnref{label}";
        var href = $"#fn{label}";
        state.Output.Append($"<sup class=\"footnote-ref\"><a href=\"{href}\" id=\"{id}\">{label}</a></sup>");
    }
}
