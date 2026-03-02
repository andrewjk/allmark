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
        var style = Ansi.Dim;
        state.Output.Append($"{style}{Ansi.Strikethrough}");
        RenderChildren.Execute(node, state);
        state.Output.Append($"{Ansi.StrikethroughReset}{Ansi.Reset}");
    }
}
