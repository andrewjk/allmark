namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleEmphasisRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "emphasis",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        state.Output.Append($"{Ansi.Italic}{Ansi.Yellow}");
        RenderChildren.Execute(node, state);
        state.Output.Append($"{Ansi.Reset}");
    }
}
