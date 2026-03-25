namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleCodeSpanRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "code_span",
            Render = (node, state, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Green;
        state.Output.Append(style);
        RenderChildren.Execute(node, state);
        state.Output.Append(Ansi.Reset);
    }
}
