namespace Allmark.Render;

using Allmark.Types;

public static class RenderChildren
{
	public static void Execute(MarkdownNode node, RendererState state, bool? decode = true)
	{
		var children = node.Children;
		if (children != null && children.Count > 0)
		{
			var trim =
				node.Type != "code_block" &&
				node.Type != "code_fence" &&
				node.Type != "code_span";

			var i = 0;
			foreach (var child in children)
			{
				var first = i == 0;
				var last = i == children.Count - 1;
				RenderNode.Execute(child, state, trim && first, trim && last, decode);
				i++;
			}
		}
	}
}
