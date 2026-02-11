namespace Allmark.Render;

using Allmark.Types;

public static class RenderFootnoteList
{
	public static void Execute(RendererState state)
	{
		state.Output.Append("<section class=\"footnotes\">\n<ol>\n");
		var number = 1;
		foreach (var node in state.Footnotes)
		{
			var label = number++;
			var id = $"fn{label}";
			var href = $"#fnref{label}";
			state.Output.Append($"<li id=\"{id}\">");
			RenderChildren.Execute(node, state);
			var output = state.Output.ToString();
			if (output.EndsWith("</p>\n"))
			{
				state.Output.Length -= 5;
			}
			state.Output.Append($" <a href=\"{href}\" class=\"footnote-backref\">↩</a></p>\n</li>\n");
		}
		state.Output.Append("</ol>\n</section>");
	}
}
