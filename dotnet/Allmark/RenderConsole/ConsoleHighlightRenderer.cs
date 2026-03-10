namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleHighlightRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "highlight",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.YellowBg + Ansi.Black;
        state.Output.Append(style);
        RenderChildren.Execute(node, state);
        state.Output.Append(Ansi.Reset);
    }
}
