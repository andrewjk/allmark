namespace Allmark.Render;

using Allmark.Types;

public static class ImageRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "image",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);
		var alt = GetChildText(node);
		var title = !string.IsNullOrEmpty(node.Title) ? $" title=\"{node.Title}\"" : "";
		state.Output.Append($"<img src=\"{node.Info}\" alt=\"{alt}\"{title} />");
		RenderUtils.EndNewLine(node, state);
	}

	static string GetChildText(MarkdownNode node) {
		var text = "";
		if (node.Children != null) {
			foreach (var child in node.Children) {
				if (child.Type == "text") {
					text += child.Markup;
				} else {
					text += GetChildText(child);
				}
			}
		}
		return text;
	}
}
