namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleStrikethroughRenderer
{
    public static Renderer Create(string style)
    {
        return new Renderer
        {
            Name = "strikethrough",
            Render = (node, state, first, last, decode) => Render(node, state, style),
        };
    }

    public static void Render(MarkdownNode node, RendererState state, string style)
    {
        state.Output.Append($"{style}{RenderToConsole.AnsiStrikethrough}");
        RenderChildren.Execute(node, state);
        state.Output.Append($"{RenderToConsole.AnsiStrikethroughReset}{RenderToConsole.AnsiReset}");
    }
}
