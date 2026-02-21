namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleLinkRenderer
{
    public static Renderer Create(string style)
    {
        return new Renderer
        {
            Name = "link",
            Render = (node, state, first, last, decode) => Render(node, state, style),
        };
    }

    public static void Render(MarkdownNode node, RendererState state, string style)
    {
        state.Output.Append(style);
        RenderChildren.Execute(node, state);
        if (!string.IsNullOrEmpty(node.Info))
        {
            state.Output.Append($"{RenderToConsole.AnsiReset} ({node.Info})");
        }
        else
        {
            state.Output.Append(RenderToConsole.AnsiReset);
        }
    }
}
