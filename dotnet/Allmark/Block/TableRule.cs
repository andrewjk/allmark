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

            var rowLength = endOfLine - state.I;

            var row = Utils.NewBlock("table_row", state.I, state.Line, "", 0);
            row.Length = rowLength;
            lastNode.Children!.Add(row);

            var rowSrc = state.Src.Substring(state.I, rowLength);
            var pipePositions = LoadPipePositions(rowSrc);

            var rowContent = TrimPipesRegex.Replace(rowSrc.Trim(), "");
            var rowParts = PipeRegex.Split(rowContent).ToList();
            while (rowParts.Count < headers.Count)
            {
                rowParts.Add("");
            }

            for (var j = 0; j < Math.Min(rowParts.Count, headers.Count); j++)
            {
                ParseTableCell(row, state, j, rowParts, headers, pipePositions);
            }

            lastNode.Length = endOfLine - lastNode.Index;

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
                else if (nextChar == '\n')
                {
                    end++;
                    break;
                }
                else if (nextChar == '\r')
                {
                    end++;
                    if (Utils.GetChar(state.Src, end) == '\n') end++;
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

                var headerIndex = parent.Index;
                var headerLength = parent.Content?.Length ?? 0;
                if ((parent.Content?.EndsWith("\n") ?? false))
                {
                    headerLength--;
                }
                var header = Utils.NewBlock("table_header", headerIndex, state.Line, "", 0);
                header.Length = headerLength;
                parent.Children!.Add(header);

                var headerSrc = parent.Content?.Substring(0, headerLength) ?? "";
                var pipePositions = LoadPipePositions(headerSrc);

                var headerParts = PipeRegex.Split(headerContent).ToList();
                for (var j = 0; j < headerParts.Count; j++)
                {
                    ParseTableCell(header, state, j, headerParts, cells, pipePositions);
                }

                parent.Type = "table";
                parent.Content = "";
                parent.Markup = state.Src.Substring(state.I, end - state.I);
                parent.Length = end - parent.Index;
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

    private static void ParseTableCell(
        MarkdownNode row,
        BlockParserState state,
        int index,
        List<string> parts,
        List<string> headers,
        List<int> pipePositions)
    {
        var text = parts[index] ?? "";

        var cellStart = index < pipePositions.Count ? pipePositions[index] : 0;
        var cellEnd = index + 1 < pipePositions.Count ? pipePositions[index + 1] : 0;
        var cellLength = cellEnd - cellStart + 1;

        var trimmedText = text.Trim();
        var textLength = text.Length;
        var trimmedLength = trimmedText.Length;
        var contentStartOffset = textLength > 0 && trimmedLength > 0 ? text.IndexOf(trimmedText) + 1 : 0;
        var contentStart = row.Index + cellStart + contentStartOffset;

        var cell = Utils.NewBlock("table_cell", row.Index + cellStart, state.Line, "", 0);
        cell.Length = cellLength;
        cell.Info = headers[index];
        row.Children!.Add(cell);

        var content = Utils.NewBlock("table_cell_content", contentStart, state.Line, "", 0);
        content.Content = trimmedText.Replace("\\|", "|");
        content.AcceptsContent = true;
        cell.Children = [content];
    }

    private static List<int> LoadPipePositions(string line)
    {
        var pipePositions = new List<int>();
        var haveEndPipe = false;
        for (var i = 0; i < line.Length; i++)
        {
            if (line[i] == '|' && !Utils.IsEscaped(line, i))
            {
                pipePositions.Add(i);
                haveEndPipe = true;
            }
            else if (!Utils.IsSpace(line[i]))
            {
                // Make sure there's a start pipe position
                if (pipePositions.Count == 0)
                {
                    pipePositions.Add(0);
                }
                haveEndPipe = false;
            }
        }
        // Make sure there's an end pipe position
        if (!haveEndPipe)
        {
            pipePositions.Add(line.Length - 1);
        }
        return pipePositions;
    }
}
