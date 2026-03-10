namespace Allmark;

using System.Text;
using Allmark.Render;
using Allmark.Rulesets;
using Allmark.Types;

public static class Renderer
{
    public static string Execute(MarkdownNode doc, Dictionary<string, OutputRenderer> renderers)
    {
        var state = new RendererState
        {
            Renderers = renderers,
            Output = new StringBuilder(),
            Footnotes = new List<MarkdownNode>(),
            ListDepth = 0
        };

        RenderChildren.Execute(doc, state);

        if (state.Footnotes.Count > 0 && renderers.TryGetValue("footnote_list", out var footnoteListRenderer))
        {
            footnoteListRenderer.Render(doc, state, null, null, true);
        }

        if (state.Output.Length > 0)
        {
            //output = System.Text.RegularExpressions.Regex.Replace(output, @"\n*$", "\n");
            var output = state.Output.ToString().TrimEnd("\n").ToString() + "\n";
            state.Output.Clear();
            state.Output.Append(output);
        }

        return state.Output.ToString();
    }
}
