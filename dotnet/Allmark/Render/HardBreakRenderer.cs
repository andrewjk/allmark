namespace Allmark.Render;

using Allmark.Types;

public static class HardBreakRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "hard_break",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        state.Output.AppendLine(@"<br />");
    }
}
