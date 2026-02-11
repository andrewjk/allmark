namespace Allmark.Render;

using Allmark.Types;

public static class RenderTag
{
	public static void Execute(MarkdownNode node, RendererState state, string tag, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);
		state.Output.Append($"<{tag}>");

		// Block nodes with no children still need a newline
		if (node.Block && (node.Children?.Count ?? 0) == 0)
		{
			state.Output.Append("\n");
		}
		else
		{
			RenderUtils.InnerNewLine(node, state);
		}

		RenderChildren.Execute(node, state, decode);
		state.Output.Append($"</{tag}>");
		RenderUtils.EndNewLine(node, state);
	}
}
