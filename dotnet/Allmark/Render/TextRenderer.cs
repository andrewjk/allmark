namespace Allmark.Render;

using Allmark.Types;

public static class TextRenderer
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
        var content = node.Content;
        if (decode == true)
        {
            content = Utils.DecodeEntities(content);
            content = Utils.EscapePunctuation(content);
        }
        content = Utils.EscapeHtml(content);

        state.Output.Append(content);
    }
}
