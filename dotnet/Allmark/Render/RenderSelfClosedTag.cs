namespace Allmark.Render;

using Allmark.Types;

public static class RenderSelfClosedTag
{
	public static void Execute(MarkdownNode node, RendererState state, string tag)
	{
		RenderUtils.StartNewLine(node, state);
		state.Output.Append($"<{tag} />");
		RenderUtils.EndNewLine(node, state);
	}
}
