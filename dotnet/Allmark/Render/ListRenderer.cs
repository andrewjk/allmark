namespace Allmark.Render;

using Allmark.Types;

public static class ListRenderer
{
    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        bool ordered = node.Type == "list_ordered";
        string startAttr = "";
        if (ordered)
        {
            if (int.TryParse(node.Markup.Substring(0, node.Markup.Length - 1), out int startNumber) && startNumber != 1)
            {
                startAttr = $" start=\"{startNumber}\"";
            }
        }

        RenderUtils.StartNewLine(node, state);
        state.Output.Append($"<{(ordered ? $"ol{startAttr}" : "ul")}>");
        RenderUtils.InnerNewLine(node, state);

        foreach (var item in node.Children!)
        {
            state.Output.Append("<li>");
            for (int i = 0; i < item.Children!.Count; i++)
            {
                var child = item.Children[i];
                if (!node.Loose && child.Type == "paragraph")
                {
                    // Skip paragraphs under list items to make the list tight
                    RenderChildren.Execute(child, state, decode);
                }
                else
                {
                    if (i == 0)
                    {
                        RenderUtils.InnerNewLine(item, state);
                    }
                    RenderNode.Execute(child, state, decode);
                    if (i == item.Children!.Count - 1 && child.Block)
                    {
                        if (state.Output.Length > 0 && state.Output[^1] == '\n')
                        {
                            state.Output.Length -= 1;
                        }
                        if (state.Output.Length > 0 && state.Output[^1] == '\r')
                        {
                            state.Output.Length -= 1;
                        }
                        state.Output.Append('\n');
                    }
                }
            }
            state.Output.Append("</li>");
            RenderUtils.EndNewLine(node, state);
        }

        state.Output.Append($"</{(ordered ? "ol" : "ul")}>");
        RenderUtils.EndNewLine(node, state);
    }
}
