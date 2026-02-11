namespace Allmark.Render;

using Allmark.Types;

public static class CodeBlockRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "code_block",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);
		var lang = !string.IsNullOrEmpty(node.Info) ? $" class=\"language-{node.Info.Trim().Split(' ')[0]}\"" : "";
		state.Output.Append($"<pre><code{lang}>");
		RenderChildren.Execute(node, state, false);
		state.Output.Append("</code></pre>");
		RenderUtils.EndNewLine(node, state);
	}
}
