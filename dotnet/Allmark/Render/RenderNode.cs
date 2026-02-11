namespace Allmark.Render;

using Allmark.Types;

public static class RenderNode
{
	public static void Execute(MarkdownNode node, RendererState state, bool first = false, bool last = false, bool? decode = true)
	{
		if (state.Renderers.TryGetValue(node.Type, out var renderer))
		{
			renderer.Render(node, state, first, last, decode);
		}
	}
}
