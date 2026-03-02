namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleInlineRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "inline",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        state.Output.Append(Ansi.Reset);
        RenderChildren.Execute(node, state);
        state.Output.Append(Ansi.Reset);
    }
}
