namespace Allmark.Render;

using Allmark.Types;

public static class FootnoteListRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "footnote_list",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        state.Output.Append("<section class=\"footnotes\">\n<ol>\n");
        var number = 1;
        foreach (var footnote in state.Footnotes)
        {
            var label = number++;
            var id = $"fn{label}";
            var href = $"#fnref{label}";
            state.Output.Append($"<li id=\"{id}\">");
            RenderChildren.Execute(footnote, state);
            var output = state.Output.ToString();
            if (output.EndsWith("</p>\n"))
            {
                state.Output.Length -= 5;
            }
            state.Output.Append($" <a href=\"{href}\" class=\"footnote-backref\">↩</a></p>\n</li>\n");
        }
        state.Output.Append("</ol>\n</section>");
    }
}
