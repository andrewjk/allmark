namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleStrongRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "strong",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        state.Output.Append($"{Ansi.Bold}{Ansi.Yellow}");
        RenderChildren.Execute(node, state);
        state.Output.Append($"{Ansi.Reset}");
    }
}
