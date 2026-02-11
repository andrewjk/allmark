namespace Allmark.Render;

using Allmark.Types;

public static class FootnoteRenderer
{
	public static Renderer Create()
	{
		return new Renderer
			{
			Name = "footnote",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		if (!state.Footnotes.Any((f) => f.Info == node.Info)) {
			state.Footnotes.Add(node);
		}
		var label = state.Footnotes.Count;
		var id = $"fnref{label}";
		var href = $"#fn{label}";
		state.Output.Append($"<sup class=\"footnote-ref\"><a href=\"{href}\" id=\"{id}\">{label}</a></sup>");
	}
}
