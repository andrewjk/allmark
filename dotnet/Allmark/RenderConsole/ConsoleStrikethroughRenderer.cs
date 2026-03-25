namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleStrikethroughRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "strikethrough",
            Render = (node, state, decode) => Render(node, state),
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
