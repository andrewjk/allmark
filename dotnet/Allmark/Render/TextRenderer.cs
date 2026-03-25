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
        var markup = node.Markup;
        if (decode == true)
        {
            markup = Utils.DecodeEntities(markup);
            markup = Utils.EscapePunctuation(markup);
        }
        markup = Utils.EscapeHtml(markup);

        state.Output.Append(markup);
    }
}
