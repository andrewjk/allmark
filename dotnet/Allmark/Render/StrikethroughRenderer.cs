namespace Allmark.Render;

using Allmark.Types;

public static class StrikethroughRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "strikethrough",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderTag.Execute(node, state, "del");
	}
}
