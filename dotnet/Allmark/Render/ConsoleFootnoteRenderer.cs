namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleFootnoteRenderer
{
    public static Renderer Create(string style)
    {
        return new Renderer
        {
            Name = "footnote",
            Render = (node, state, first, last, decode) => Render(node, state, style),
        };
    }

    public static void Render(MarkdownNode node, RendererState state, string style)
    {
        if (state.Footnotes.FirstOrDefault(f => f.Info == node.Info) == null)
        {
            state.Footnotes.Add(node);
        }
        var label = state.Footnotes.Count;
        state.Output.Append($"{style}[{label}]{RenderToConsole.AnsiReset}");
    }
}
