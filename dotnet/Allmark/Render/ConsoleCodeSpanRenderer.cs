namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleCodeSpanRenderer
{
    public static Renderer Create(string style)
    {
        return new Renderer
        {
            Name = "code_span",
            Render = (node, state, first, last, decode) => Render(node, state, style),
        };
    }

    public static void Render(MarkdownNode node, RendererState state, string style)
    {
        state.Output.Append(style);
        state.Output.Append('`');
        RenderChildren.Execute(node, state);
        state.Output.Append('`');
        state.Output.Append(RenderToConsole.AnsiReset);
    }
}
