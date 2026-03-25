namespace Allmark.Render;

using Allmark.Types;

public static class RenderChildren
{
    public static void Execute(MarkdownNode node, RendererState state, bool? decode = true)
    {
        var children = node.Children;
        if (children != null && children.Count > 0)
        {
            foreach (var child in children)
            {
                RenderNode.Execute(child, state, decode);
            }
        }
    }
}
