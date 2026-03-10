namespace Allmark.Render;

using Allmark.Types;

public static class ThematicBreakRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "thematic_break",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        RenderUtils.StartNewLine(node, state);
        state.Output.Append("<hr />");
        RenderUtils.EndNewLine(node, state);
    }
}
