namespace Allmark.Render;

using Allmark.Types;

public static class RenderTag
{
    public static void Execute(MarkdownNode node, RendererState state, string tag, bool? decode = true)
    {
        RenderUtils.StartNewLine(node, state);
        state.Output.Append($"<{tag}>");

        if (node.Block && (node.Children?.Count ?? 0) == 0)
        {
            state.Output.Append("\n");
        }
        else
        {
            RenderUtils.InnerNewLine(node, state);
            RenderChildren.Execute(node, state, decode);
            if (node.Block)
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

        state.Output.Append($"</{tag}>");
        RenderUtils.EndNewLine(node, state);
    }
}
