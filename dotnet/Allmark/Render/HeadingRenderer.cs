namespace Allmark.Render;

using Allmark.Types;

public static class HeadingRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "heading",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);

		var level = 0;
		if (node.Markup.StartsWith("#"))
		{
			level = node.Markup.Length;
		}
		else if (node.Markup.Contains("="))
		{
			level = 1;
		}
		else if (node.Markup.Contains("-"))
		{
			level = 2;
		}

		state.Output.Append($"<h{level}>");
		RenderUtils.InnerNewLine(node, state);
		RenderChildren.Execute(node, state, decode);
		state.Output.Append($"</h{level}>");
		RenderUtils.EndNewLine(node, state);
	}
}
