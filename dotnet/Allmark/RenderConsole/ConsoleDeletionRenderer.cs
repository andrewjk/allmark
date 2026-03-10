namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleDeletionRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "deletion",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Dim;
        state.Output.Append($"{style}--");
        RenderChildren.Execute(node, state);
        state.Output.Append($"--{Ansi.Reset}");
    }
}
