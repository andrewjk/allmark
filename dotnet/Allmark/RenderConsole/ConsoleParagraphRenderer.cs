namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleParagraphRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "paragraph",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }
        if (state.Output.Length > 0 && !state.Output.ToString().EndsWith("\n\n"))
        {
            state.Output.Append('\n');
        }
        RenderChildren.Execute(node, state);
        state.Output.Append("\n\n");
    }
}
