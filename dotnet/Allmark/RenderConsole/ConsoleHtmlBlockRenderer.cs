namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleHtmlBlockRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "html_block",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        state.Output.Append(node.Content);
    }
}
