namespace Allmark.Render;

using Allmark.Types;

public static class SuperscriptRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "superscript",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderTag.Execute(node, state, "sup");
	}
}
