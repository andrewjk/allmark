namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleStrikethroughRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "strikethrough",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = RenderToConsole.Styles["strikethrough"];
        state.Output.Append($"{style}{RenderToConsole.AnsiStrikethrough}");
        RenderChildren.Execute(node, state);
        state.Output.Append($"{RenderToConsole.AnsiStrikethroughReset}{RenderToConsole.AnsiReset}");
    }
}
