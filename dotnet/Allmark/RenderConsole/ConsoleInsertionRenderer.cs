namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleInsertionRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "insertion",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Green;
        state.Output.Append($"{style}++");
        RenderChildren.Execute(node, state);
        state.Output.Append($"++{Ansi.Reset}");
    }
}
