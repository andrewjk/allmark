namespace Allmark.Render;

using Allmark.Types;

public static class HtmlSpanRenderer
{
	public static Renderer Create()
	{
		return new Renderer
			{
			Name = "html_span",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		state.Output.Append(node.Content);
	}
}
