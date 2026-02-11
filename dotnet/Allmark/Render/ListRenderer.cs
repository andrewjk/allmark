namespace Allmark.Render;

using Allmark.Types;

public static class ListRenderer
{
	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		bool ordered = node.Type == "list_ordered";
		string startAttr = "";
		if (ordered)
		{
			if (int.TryParse(node.Markup.Substring(0, node.Markup.Length - 1), out int startNumber) && startNumber != 1)
			{
				startAttr = $" start=\"{startNumber}\"";
			}
		}

		RenderUtils.StartNewLine(node, state);
		state.Output.Append($"<{(ordered ? $"ol{startAttr}" : "ul")}>");
		RenderUtils.InnerNewLine(node, state);

		// "A list is loose if any of its constituent list items are separated by
		// blank lines, or if any of its constituent list items directly contain two
		// block-level elements with a blank line between them. Otherwise a list is
		// tight."
		bool loose = false;
		for (int i = 0; i < node.Children!.Count - 1; i++)
		{
			var child = node.Children[i];

			// A list item has a blank line after if its last child has a blank line after
			var grandchild = child.Children!.Count > 0 ? child.Children[child.Children.Count - 1] : null;
			if (grandchild?.BlankAfter == true)
			{
				child.BlankAfter = true;
			}

			if (child.BlankAfter)
			{
				loose = true;
				break;
			}
		}
		for (int i = 0; i < node.Children!.Count; i++)
		{
			var child = node.Children[i];
			for (int j = 0; j < child.Children!.Count - 1; j++)
			{
				var firstChild = child.Children[j];
				var secondChild = child.Children[j + 1];
				if (firstChild.Block && firstChild.BlankAfter && secondChild.Block)
				{
					loose = true;
					break;
				}
			}
		}

		foreach (var item in node.Children!)
		{
			state.Output.Append("<li>");
			for (int i = 0; i < item.Children!.Count; i++)
			{
				var child = item.Children[i];
				if (!loose && child.Type == "paragraph")
				{
					// Skip paragraphs under list items to make the list tight
					RenderChildren.Execute(child, state, decode);
				}
				else
				{
					if (i == 0)
					{
						RenderUtils.InnerNewLine(item, state);
					}
					RenderNode.Execute(child, state, i == item.Children!.Count - 1, decode: decode);
				}
			}
			state.Output.Append("</li>");
			RenderUtils.EndNewLine(node, state);
		}

		state.Output.Append($"</{(ordered ? "ol" : "ul")}>");
		RenderUtils.EndNewLine(node, state);
	}
}
