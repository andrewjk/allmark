namespace Allmark.Render;

using Allmark.Types;

public static class InsertionRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "insertion",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);
		state.Output.Append("<ins class=\"markdown-insertion\">");
		RenderChildren.Execute(node, state, decode);
		state.Output.Append("</ins>");
		RenderUtils.EndNewLine(node, state);
	}
}
