namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleTextRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "text",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        var text = node.Markup;
        if (first == true)
        {
            text = text.TrimStart();
        }
        if (last == true)
        {
            text = text.TrimEnd();
        }
        state.Output.Append(text);
    }
}
