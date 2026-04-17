namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleThematicBreakRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "thematic_break",
            Render = (node, state, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Dim;
        state.Output.Append($"{style}───{Ansi.Reset}\n\n");
    }
}
