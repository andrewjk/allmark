namespace Allmark.Render;

using Allmark.Types;

public static class AlertRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "alert",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);
		state.Output.Append(@$"<div class=""markdown-alert markdown-alert-{node.Markup}"">
<p class=""markdown-alert-title"">{node.Markup.Substring(0, 1).ToUpper() + node.Markup.Substring(1)}</p>");
		RenderChildren.Execute(node, state, decode);
		state.Output.Append("</div>");
		RenderUtils.EndNewLine(node, state);
	}
}
