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
        RenderChildren.Execute(node, state);
        state.Output.Append("\n\n");
    }
}
