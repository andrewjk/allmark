namespace Allmark.Render;

using Allmark.Types;

public static class HighlightRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "highlight",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderTag.Execute(node, state, "mark");
	}
}
