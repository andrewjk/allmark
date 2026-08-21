namespace Allmark;

using System.Text;
using Allmark.Render;
using Allmark.Rulesets;
using Allmark.Types;

public static class Renderer
{
    public static string Execute(MarkdownNode doc, OutputRenderer[] renderers, RenderOptions? options = null)
    {
        var state = new RendererState
        {
            RenderersMap = renderers.ToDictionary(r => r.Name),
            Output = new StringBuilder(),
            Footnotes = new List<MarkdownNode>(),
            FootnoteRefs = new Dictionary<string, MarkdownNode>(),
            ListDepth = 0,
            LineWidth = options?.LineWidth
        };

        RenderChildren.Execute(doc, state);

        if (state.Footnotes.Count > 0 && state.RenderersMap.TryGetValue("footnote_list", out var footnoteListRenderer))
        {
            footnoteListRenderer.Render(doc, state, true);
        }

        if (state.Output.Length > 0)
        {
            //output = System.Text.RegularExpressions.Regex.Replace(output, @"\n*$", "\n");
            var output = state.Output.ToString().TrimEnd('\n', '\r') + "\n";
            state.Output.Clear();
            state.Output.Append(output);
        }

        return state.Output.ToString();
    }
}
