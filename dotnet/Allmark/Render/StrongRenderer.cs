namespace Allmark.Render;

using Allmark.Types;

public static class StrongRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "strong",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderTag.Execute(node, state, "strong");
	}
}
