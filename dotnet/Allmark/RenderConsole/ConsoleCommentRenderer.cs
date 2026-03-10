namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleCommentRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "comment",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Blue;
        state.Output.Append($"{style}>>");
        RenderChildren.Execute(node, state);
        state.Output.Append($">>{Ansi.Reset}");
    }
}
