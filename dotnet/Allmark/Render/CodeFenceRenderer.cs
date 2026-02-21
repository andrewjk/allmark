namespace Allmark.Render;

using Allmark.Types;

public static class CodeFenceRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "code_fence",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        RenderUtils.StartNewLine(node, state);
        var lang = "";
        if (!string.IsNullOrEmpty(node.Info))
        {
            var trimmed = node.Info.Trim().Split(' ')[0];
            if (!string.IsNullOrEmpty(trimmed))
            {
                lang = $" class=\"language-{Utils.EscapeHtml(trimmed)}\"";
            }
        }
        state.Output.Append($"<pre><code{lang}>");
        RenderChildren.Execute(node, state, false);
        state.Output.Append("</code></pre>");
        RenderUtils.EndNewLine(node, state);
    }
}
