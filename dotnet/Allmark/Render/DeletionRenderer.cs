namespace Allmark.Render;

using Allmark.Types;

public static class DeletionRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "deletion",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        RenderUtils.StartNewLine(node, state);
        state.Output.Append("<del class=\"markdown-deletion\">");
        RenderChildren.Execute(node, state, decode);
        state.Output.Append("</del>");
        RenderUtils.EndNewLine(node, state);
    }
}
