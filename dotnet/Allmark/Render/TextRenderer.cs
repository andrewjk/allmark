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
        var scanDecode = decode == true;

        // Fast path: if none of the special characters are present, output as-is
        var needsProcessing = scanDecode
            ? content.AsSpan().IndexOfAny("&<>\"\\") >= 0
            : content.AsSpan().IndexOfAny("&<>\"") >= 0;
        if (!needsProcessing)
        {
            state.Output.Append(content);
            return;
        }

        if (scanDecode)
        {
            content = Utils.DecodeEntities(content);
            content = Utils.EscapePunctuation(content);
        }
        content = Utils.EscapeHtml(content);

        state.Output.Append(content);
    }
}
