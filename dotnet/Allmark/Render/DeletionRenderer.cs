namespace Allmark.Render;

using Allmark.Types;

public static class DeletionRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "deletion",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);
		state.Output.Append("<del class=\"markdown-deletion\">");
		RenderChildren.Execute(node, state, decode);
		state.Output.Append("</del>");
		RenderUtils.EndNewLine(node, state);
	}
}
