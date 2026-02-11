namespace Allmark.Render;

using Allmark.Types;

public static class TextRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "text",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		var markup = node.Markup;
		if (first == true)
		{
			markup = markup.TrimStart();
		}
		if (last == true)
		{
			markup = markup.TrimEnd();
		}
		if (decode == true)
		{
			markup = Utils.DecodeEntities(markup);
			markup = Utils.EscapePunctuation(markup);
		}
		markup = Utils.EscapeHtml(markup);

		state.Output.Append(markup);
	}
}
