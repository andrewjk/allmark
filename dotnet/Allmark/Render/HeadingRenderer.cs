namespace Allmark.Render;

using Allmark.Types;

public static class HeadingRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "heading",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderUtils.StartNewLine(node, state);

        var level = node.Markup.Length;

        state.Output.Append($"<h{level}>");
        // Render the children of the dummy paragraph directly (not the paragraph itself)
        if (node.Children != null && node.Children.Count > 0)
        {
            RenderChildren.Execute(node.Children[0], state, decode);
        }
        state.Output.Append($"</h{level}>");
        RenderUtils.EndNewLine(node, state);
    }
}
