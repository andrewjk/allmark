namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleThematicBreakRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "thematic_break",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Dim;
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }
        if (state.Output.Length > 0 && state.Output.Length >= 2 && state.Output.ToString(state.Output.Length - 2, 2) != "\n\n")
        {
            state.Output.Append("\n");
        }
        var count = Math.Max(3, node.Markup.Length);
        state.Output.Append($"{style}{new string('─', count)}{Ansi.Reset}\n");
    }
}
