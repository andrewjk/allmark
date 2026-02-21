namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleThematicBreakRenderer
{
    public static Renderer Create(string style)
    {
        return new Renderer
        {
            Name = "thematic_break",
            Render = (node, state, first, last, decode) => Render(node, state, style),
        };
    }

    public static void Render(MarkdownNode node, RendererState state, string style)
    {
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }
        if (state.Output.Length > 0 && state.Output.Length >= 2 && state.Output.ToString(state.Output.Length - 2, 2) != "\n\n")
        {
            state.Output.Append("\n");
        }
        state.Output.Append($"{style}───{RenderToConsole.AnsiReset}\n\n");
    }
}
