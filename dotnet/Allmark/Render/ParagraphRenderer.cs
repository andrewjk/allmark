namespace Allmark.Render;

using Allmark.Types;

public static class ParagraphRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "paragraph",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);
		state.Output.Append("<p>");
		RenderUtils.InnerNewLine(node, state);
		RenderChildren.Execute(node, state);
		state.Output.Append("</p>");
		RenderUtils.EndNewLine(node, state);
	}
}
