namespace Allmark.Render;

using Allmark.Types;

public static class RenderUtils
{
    public static void StartNewLine(MarkdownNode node, RendererState state)
    {
        if (state.Output.Length > 0 && node.Block)
        {
            if (state.Output.ToString().EndsWith('\n'))
            {
                state.Output.Length -= 1;
            }
            if (state.Output.ToString().EndsWith('\r'))
            {
                state.Output.Length -= 1;
            }
            state.Output.Append("\n");
        }
    }

    public static void InnerNewLine(MarkdownNode node, RendererState state)
    {
        if (node.Block && node.Children!.Count > 0 && node.Children?[0]?.Block == true)
        {
            state.Output.Append("\n");
        }
    }

    public static void EndNewLine(MarkdownNode node, RendererState state)
    {
        if (node.Block)
        {
            state.Output.Append("\n");
        }
    }
}
