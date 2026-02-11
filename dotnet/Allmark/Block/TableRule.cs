namespace Allmark.Block;

using System.Text.RegularExpressions;
using Allmark.Types;

public static class TableRule
{
	private static readonly Regex PipeRegex = new(@"(?<!\\)\|", RegexOptions.Compiled);
	private static readonly Regex TrimPipesRegex = new(@"(^\||\|$)", RegexOptions.Compiled);
	private static readonly Regex NonSpaceRegex = new(@"[^\s]", RegexOptions.Compiled);

	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "table",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		if (parent.AcceptsContent)
		{
			return false;
		}

		// We may already have a table
		var lastNode = parent.Children!.LastOrDefault();
		if (!state.HasBlankLine && lastNode?.Type == "table")
		{
			var endOfLine = Utils.GetEndOfLine(state);

			var headers = lastNode.Children?[0].Children!.Select((c) => c.Info ?? "").ToList() ?? [];

			var row = Utils.NewNode("table_row", true, state.I, state.Line, 1, "", 0, []);
			lastNode.Children!.Add(row);

			var rowContent = TrimPipesRegex.Replace(state.Src.Substring(state.I, endOfLine - state.I).Trim(), "");
			var rowParts = PipeRegex.Split(rowContent).ToList();
			while (rowParts.Count < headers.Count)
			{
				rowParts.Add("");
			}

			var ri = 0;
			foreach (var text in rowParts.Take(headers.Count))
			{
				var cell = Utils.NewNode("table_cell", true, state.I, state.Line, 1, "", 0, []);
				cell.Content = (text ?? "").Trim().Replace("\\|", "|");
				cell.Info = headers[ri++];
				row.Children!.Add(cell);
			}

			state.I = endOfLine;
			return true;
		}

		// "The delimiter row consists of cells whose only content are hyphens (-),
		// and optionally, a leading or trailing colon (:), or both, to indicate
		// left, right, or center alignment respectively"
		var ch = Utils.GetChar(state.Src, state.I);
		if (state.Indent <= 3 && (ch == '|' || ch == '-' || ch == ':'))
		{
			var cells = new List<string> { ch == ':' ? "left" : "" };
			var end = state.I + 1;
			var lastChar = ch;
			for (; end < state.Src.Length; end++)
			{
				var nextChar = state.Src[end];
				if (nextChar == '|')
				{
					cells.Add("");
					lastChar = nextChar;
				}
				else if (nextChar == '-')
				{
					lastChar = nextChar;
				}
				else if (nextChar == ':')
				{
					var x = cells.Count - 1;
					if (lastChar == '|')
					{
						cells[x] = "left";
					}
					else
					{
						cells[x] = cells[x] == "left" ? "center" : "right";
					}
					lastChar = nextChar;
				}
				else if (Utils.IsNewLine(nextChar))
				{
					// TODO: Handle windows crlf
					end++;
					break;
				}
				else if (Utils.IsSpace(nextChar))
				{
					continue;
				}
				else
				{
					return false;
				}
			}
			if (lastChar == '|')
			{
				cells.RemoveAt(cells.Count - 1);
			}

			var haveParagraph =
				parent.Type == "paragraph" && !parent.BlankAfter && NonSpaceRegex.IsMatch(parent.Content ?? "");
			if (haveParagraph)
			{
				// "The header row must match the delimiter row in the number of
				// cells. If not, a table will not be recognized"
				var headerCellCount = 1;
				var headerContent = TrimPipesRegex.Replace((parent.Content ?? "").Trim(), "");
				for (var i = 0; i < headerContent.Length; i++)
				{
					if (headerContent[i] == '|' && !Utils.IsEscaped(headerContent, i))
					{
						headerCellCount++;
					}
				}
				if (cells.Count != headerCellCount)
				{
					return false;
				}

				MarkdownNode? closedNode = null;

				if (state.MaybeContinue)
				{
					state.MaybeContinue = false;
					for (var i = 0; i < state.OpenNodes.Count - 1; i++)
					{
						var node = state.OpenNodes.ElementAt(i);
						if (node.MaybeContinuing)
						{
							node.MaybeContinuing = false;
							closedNode = node;
							// Pop until we reach this node
							var newLength = state.OpenNodes.Count - i - 1;
							while (state.OpenNodes.Count > newLength)
							{
								state.OpenNodes.Pop();
							}
							break;
						}
					}
					parent = state.OpenNodes.Peek();
				}

				if (closedNode != null)
				{
					Utils.CloseNode(state, closedNode);
				}

				var header = Utils.NewNode("table_header", true, state.I, state.Line, 1, "", 0, []);
				parent.Children!.Add(header);

				var headerParts = PipeRegex.Split(headerContent);
				var hi = 0;
				foreach (var text in headerParts)
				{
					var cell = Utils.NewNode("table_cell", true, state.I, state.Line, 1, "", 0, []);
					cell.Content = text.Trim().Replace("\\|", "|");
					cell.Info = cells[hi++];
					header.Children!.Add(cell);
				}

				parent.Type = "table";
				parent.Content = "";
				parent.Markup = state.Src.Substring(state.I, end - state.I);
				state.I = end;
				return true;
			}
		}

		return false;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		// Just close the table every time, and check whether the last node was a
		// table in testStart. That way we can interrupt tables with e.g.
		// blockquotes, even if the blockquote contains a pipe
		return false;
	}
}
