namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleDeletionRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "deletion",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = RenderToConsole.Styles["deletion"];
        state.Output.Append($"{style}--");
        RenderChildren.Execute(node, state);
        state.Output.Append($"--{RenderToConsole.AnsiReset}");
    }
}
