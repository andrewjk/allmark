namespace Allmark;

using System.Text;
using Allmark.Render;
using Allmark.Types;

public static class RenderHtml
{
	public static string Execute(MarkdownNode doc, Dictionary<string, Renderer> renderers)
	{
		var state = new RendererState
		{
			Renderers = renderers,
			Output = new StringBuilder(),
			Footnotes = new List<MarkdownNode>()
		};

		RenderChildren.Execute(doc, state);

		if (state.Footnotes.Count > 0)
		{
			RenderFootnoteList.Execute(state);
		}

		if (state.Output.Length > 0 && state.Output[^1] != '\n')
		{
			state.Output.Append('\n');
		}

		return state.Output.ToString();
	}
}
