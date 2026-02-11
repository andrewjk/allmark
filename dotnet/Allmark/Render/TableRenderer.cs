namespace Allmark.Render;

using Allmark.Types;

public static class TableRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "table",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);
		state.Output.Append("<table>\n<thead>\n<tr>\n");
		foreach (var cell in node.Children![0].Children!)
		{
			RenderTableCell(cell, state, "th", decode);
		}
		state.Output.Append("</tr>\n</thead>\n");
		if (node.Children!.Count > 1)
		{
			state.Output.Append("<tbody>\n");
			for (int i = 1; i < node.Children.Count; i++)
			{
				var row = node.Children[i];
				state.Output.Append("<tr>\n");
				foreach (var cell in row.Children!)
				{
					RenderTableCell(cell, state, "td", decode);
				}
				state.Output.Append("</tr>\n");
			}
			state.Output.Append("</tbody>\n");
		}
		state.Output.Append("</table>");
		RenderUtils.EndNewLine(node, state);
	}

	private static void RenderTableCell(MarkdownNode node, RendererState state, string tag, bool? decode = true)
	{
		RenderUtils.StartNewLine(node, state);
		string alignAttr = !string.IsNullOrEmpty(node.Info) ? $" align=\"{node.Info}\"" : "";
		state.Output.Append($"<{tag}{alignAttr}>");
		RenderUtils.InnerNewLine(node, state);
		RenderChildren.Execute(node, state, decode);
		state.Output.Append($"</{tag}>");
		RenderUtils.EndNewLine(node, state);
	}
}
