namespace Allmark.Render;

using Allmark.Types;

public static class SubscriptRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "subscript",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderTag.Execute(node, state, "sub");
    }
}
