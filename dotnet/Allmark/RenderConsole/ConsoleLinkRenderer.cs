namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleLinkRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "link",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Blue + Ansi.Underline;
        state.Output.Append(style);
        RenderChildren.Execute(node, state);
        if (!string.IsNullOrEmpty(node.Info))
        {
            state.Output.Append($"{Ansi.Reset} {Ansi.Dim}({node.Info}){Ansi.Reset}");
        }
        else
        {
            state.Output.Append(Ansi.Reset);
        }
    }
}
