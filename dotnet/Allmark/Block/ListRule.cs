namespace Allmark.Block;

using Allmark.Types;

public record ListInfo
{
	public required string Delimiter { get; set; }
	public required string Markup { get; set; }
	public required bool IsBlank { get; set; }
	public required string Type { get; set; }
}

public static class ListRule
{
	public static bool TestListStart(
		BlockParserState state,
		MarkdownNode parent,
		ListInfo? info)
	{
		if (info == null)
		{
			return false;
		}

		MarkdownNode? closedNode = null;

		// TODO: Move the things that refer to "list_[un]ordered" into their rules

		// "When the first list item in a list interrupts a paragraph—that is, when
		// it starts on a line that would otherwise count as paragraph continuation
		// text—then (a) the lines Ls must not begin with a blank line, and (b) if
		// the list item is ordered, the start number must be 1."
		// HACK: Only at the top level??
		if (parent.Type == "paragraph" && state.OpenNodes.Count == 2)
		{
			// "[A]n empty list item cannot interrupt a paragraph"
			if (info.IsBlank)
			{
				return false;
			}

			// "[W]e allow only lists starting with 1 to interrupt paragraphs"
			if (info.Type == "list_ordered" && info.Markup != "1" + info.Delimiter)
			{
				return false;
			}
		}

		// "[L]ist items may not be indented more than three spaces"
		var openIndent = state.Indent;
		for (var i = 0; i < state.OpenNodes.Count - 1; i++)
		{
			if (IsList(state.OpenNodes.ElementAt(i).Type))
			{
				openIndent -= state.OpenNodes.ElementAt(i).Indent;
				break;
			}
		}
		if (openIndent >= 4)
		{
			return false;
		}

		// If this was possibly a continuation, it no longer is
		// If there was a blank line after the paragraph, move it to the continued node
		if (state.MaybeContinue)
		{
			state.MaybeContinue = false;
			for (var j = 0; j < state.OpenNodes.Count - 1; j++)
			{
				var node = state.OpenNodes.ElementAt(j);
				if (node.MaybeContinuing)
				{
					node.MaybeContinuing = false;
					closedNode = node;
					// Pop until we reach this node
					var newLength = state.OpenNodes.Count - j - 1;
					while (state.OpenNodes.Count > newLength)
					{
						state.OpenNodes.Pop();
					}
				}
			}
			parent = state.OpenNodes.Peek();
		}

		// If there's an open paragraph, close it
		// TODO: Is there a better way to do this??
		if (parent.Type == "paragraph")
		{
			closedNode = state.OpenNodes.Pop();
			parent = state.OpenNodes.Peek();
		}

		// If there's an open list of a different type, and this node is not nested, close it
		// TODO: Is there a better way to do this??
		if (IsList(parent.Type) && parent.Delimiter != info.Delimiter)
		{
			var lastItem = parent.Children!.Last();
			if (lastItem?.Type == "list_item" && state.Indent < lastItem.Subindent)
			{
				closedNode = state.OpenNodes.Pop();
				parent = state.OpenNodes.Peek();
			}
		}

		if (closedNode != null)
		{
			Utils.CloseNode(state, closedNode);
		}

		// Count spaces after the marker
		var spaces = 0;
		var blank = true;
		for (var j = state.I + info.Markup.Length; j < state.Src.Length; j++)
		{
			var nextChar = state.Src[j];
			if (Utils.IsNewLine(nextChar))
			{
				break;
			}
			else if (Utils.IsSpace(nextChar))
			{
				spaces++;
			}
			else
			{
				blank = false;
				break;
			}
		}

		// If there's a newline after the marker, treat it as one space
		if (blank)
		{
			spaces = 1;
		}

		// "If the first block in the list item is an indented code block, then
		// by rule #2, the contents must be indented one space after the list
		// marker:"
		if (spaces > 4)
		{
			spaces = 1;
		}

		var haveList = parent.Type == info.Type;
		var list = haveList
			? parent
			: Utils.NewNode(info.Type, true, state.I, state.Line, 1, info.Markup, state.Indent, []);
		list.Delimiter = info.Delimiter;
		var item = Utils.NewNode("list_item", true, state.I, state.Line, 1, info.Markup, state.Indent, []);
		item.Delimiter = info.Delimiter;
		item.Subindent = state.Indent + info.Markup.Length + spaces;

		if (!haveList)
		{
			if (state.HasBlankLine && parent.Children!.Count > 0)
			{
				parent.Children[^1].BlankAfter = true;
				state.HasBlankLine = false;
			}

			parent.Children!.Add(list);
			state.OpenNodes.Push(list);
		}

		if (state.HasBlankLine && parent.Children!.Count > 0)
		{
			parent.Children[^1].BlankAfter = true;
			state.HasBlankLine = false;
		}

		list.Children!.Add(item);
		state.OpenNodes.Push(item);

		Utils.MovePastMarker(info.Markup.Length, state);

		state.HasBlankLine = false;
		Parse.ParseBlock.Execute(state, item);

		return true;
	}

	public static bool TestListContinue(
		BlockParserState state,
		MarkdownNode node,
		ListInfo? info)
	{
		var ch = Utils.GetChar(state.Src, state.I);

		// If there's the same list marker and the indent is not too far, we can continue
		if (info != null)
		{
			if (state.HasBlankLine && state.Indent >= 4)
			{
				return false;
			}
			if (info.Delimiter == node.Delimiter)
			{
				return true;
			}
		}

		// Can't continue if there's only one item, it's blank and there's a blank line after the list
		// HACK: This is messy
		if (state.HasBlankLine && node.Children!.Count == 1 && node.Children[0].Children!.Count == 0)
		{
			return false;
		}

		// TODO: Not sure about this one
		// Also, do the state.isEmpty check with indent like on the other branch
		if (Utils.IsNewLine(ch))
		{
			return true;
		}

		var openNode = state.OpenNodes.Peek();
		if (openNode.Type == "paragraph")
		{
			// We won't know until we try more things
			return true;
		}

		var lastItem = node.Children!.Last();
		if (lastItem != null && lastItem.Type == "list_item" && state.Indent >= lastItem.Subindent)
		{
			return true;
		}

		return false;
	}

	// HACK: Not great
	private static bool IsList(string nodeType)
	{
		return nodeType.StartsWith("list_") && nodeType != "list_item";
	}
}
