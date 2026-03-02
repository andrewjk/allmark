namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleImageRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "image",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Gray;
        var alt = "";
        if (node.Children != null)
        {
            foreach (var child in node.Children)
            {
                if (child.Type == "text")
                {
                    alt += child.Markup;
                }
            }
        }
        var altText = string.IsNullOrEmpty(alt) ? node.Info ?? "" : alt;
        state.Output.Append($"{style}[Image: {altText}]{Ansi.Reset}");
    }
}
