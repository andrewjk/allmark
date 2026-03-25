namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleTextRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "text",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        var text = node.Content;
        state.Output.Append(text);
    }
}
