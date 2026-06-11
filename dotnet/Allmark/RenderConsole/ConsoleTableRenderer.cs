namespace Allmark.Render;

using Allmark.Types;
using System.Text;

public static class ConsoleTableRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "table",
            Render = (node, state, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Dim;

        if (node.Children == null || node.Children.Count == 0)
        {
            return;
        }

        var headerRow = node.Children[0];
        var dataRows = node.Children.Skip(1).ToList();

        var headerCells = headerRow.Children ?? new List<MarkdownNode>();
        var cellTexts = new List<List<string>>();

        var maxColumns = Math.Max(headerCells.Count, dataRows.Max(r => r.Children?.Count ?? 0));
        var columnWidths = Enumerable.Repeat(0, maxColumns).ToList();
        var alignments = Enumerable.Repeat("", maxColumns).ToList();

        for (int i = 0; i < headerCells.Count; i++)
        {
            var text = GetTextFromNode(headerCells[i]);
            if (cellTexts.Count == 0)
            {
                cellTexts.Add(new List<string>());
            }
            cellTexts[0].Add(text);
            columnWidths[i] = Math.Max(columnWidths[i], text.Length + 2);
            alignments[i] = headerCells[i].Info ?? "";
        }

        for (int r = 0; r < dataRows.Count; r++)
        {
            var row = dataRows[r];
            var rowCells = row.Children ?? new List<MarkdownNode>();
            if (cellTexts.Count <= r + 1)
            {
                cellTexts.Add(new List<string>());
            }
            for (int c = 0; c < rowCells.Count; c++)
            {
                var text = GetTextFromNode(rowCells[c]);
                while (cellTexts[r + 1].Count <= c)
                {
                    cellTexts[r + 1].Add("");
                }
                cellTexts[r + 1][c] = text;
                if (c < columnWidths.Count)
                {
                    columnWidths[c] = Math.Max(columnWidths[c], text.Length + 2);
                }
            }
        }

        string MakeLine(string left, string mid, string right, string sep, int[] widths)
        {
            var line = new StringBuilder();
            line.Append(left);
            for (int i = 0; i < widths.Length; i++)
            {
                line.Append(new string('─', widths[i]));
                if (i < widths.Length - 1)
                {
                    line.Append(i == 0 ? mid : sep);
                }
            }
            line.Append(right);
            return $"{style}{line}{Ansi.Reset}\n";
        }

        string[] alignmentsArray = alignments.ToArray();

        string[][] cellTextsArray = cellTexts.Select(r => r.ToArray()).ToArray();
        int[] targetWidths = columnWidths.ToArray();
        string[][][]? wrappedCells = null;

        int totalWidth = targetWidths.Sum() + maxColumns + 1;
        if (state.LineWidth.HasValue && totalWidth > state.LineWidth.Value)
        {
            targetWidths = FitColumns(targetWidths, state.LineWidth.Value, maxColumns, cellTextsArray);
            wrappedCells = WrapAllCells(cellTextsArray, targetWidths);
            targetWidths = Enumerable.Repeat(2, maxColumns).ToArray();
            for (int r = 0; r < cellTextsArray.Length; r++)
            {
                for (int c = 0; c < maxColumns; c++)
                {
                    var lines = wrappedCells[r][c];
                    foreach (var line in lines)
                    {
                        targetWidths[c] = Math.Max(targetWidths[c], line.Length + 2);
                    }
                }
            }
        }

        state.Output.Append(MakeLine("┌", "┬", "┐", "┬", targetWidths));

        if (headerCells.Count > 0)
        {
            RenderRow(state, style, cellTextsArray, alignmentsArray, targetWidths, 0, null, wrappedCells);
        }

        state.Output.Append(MakeLine("├", "┼", "┤", "┼", targetWidths));

        for (int r = 0; r < dataRows.Count; r++)
        {
            var row = dataRows[r];
            var rowCells = row.Children ?? new List<MarkdownNode>();
            var rowAlignments = Enumerable.Range(0, maxColumns)
                .Select(c => c < rowCells.Count ? (rowCells[c].Info ?? "") : "")
                .ToArray();
            RenderRow(state, style, cellTextsArray, rowAlignments, targetWidths, r + 1, rowCells, wrappedCells);
        }

        state.Output.Append(MakeLine("└", "┴", "┘", "┴", targetWidths));
        state.Output.AppendLine();
    }

    private static void RenderRow(RendererState state, string style, string[][] cellTexts, string[] alignments, int[] targetWidths, int rowIdx, List<MarkdownNode>? rowCells, string[][][]? wrappedCells)
    {
        int maxColumns = targetWidths.Length;
        int maxLines = 1;
        string[][] rowLines = new string[maxColumns][];

        for (int c = 0; c < maxColumns; c++)
        {
            if (wrappedCells != null)
            {
                rowLines[c] = wrappedCells[rowIdx][c];
            }
            else
            {
                string text = rowIdx < cellTexts.Length && c < cellTexts[rowIdx].Length ? cellTexts[rowIdx][c] : "";
                rowLines[c] = [text];
            }
            maxLines = Math.Max(maxLines, rowLines[c].Length);
        }

        string align = "";
        if (rowCells != null && rowCells.Count > 0)
        {
            align = rowCells[0].Info ?? "";
        }
        else if (rowIdx == 0 && alignments.Length > 0)
        {
            align = alignments[0];
        }

        for (int lineIdx = 0; lineIdx < maxLines; lineIdx++)
        {
            state.Output.Append($"{style}│{Ansi.Reset}");
            for (int c = 0; c < maxColumns; c++)
            {
                string lineText = lineIdx < rowLines[c].Length ? rowLines[c][lineIdx] : "";
                string colAlign = c < alignments.Length ? alignments[c] : "";
                state.Output.Append($" {PadText(lineText, targetWidths[c] - 2, colAlign)}{style}│{Ansi.Reset}");
            }
            state.Output.Append('\n');
        }
    }

    private static int[] FitColumns(int[] columnWidths, int lineWidth, int numColumns, string[][] cellTexts)
    {
        int available = lineWidth - 1 - numColumns;
        int[] targetWidths = (int[])columnWidths.Clone();

        int[] minWidths = columnWidths.Select((_, colIdx) =>
        {
            int maxWordLen = 1;
            foreach (var row in cellTexts)
            {
                string text = colIdx < row.Length ? row[colIdx] : "";
                foreach (var word in text.Split(' '))
                {
                    maxWordLen = Math.Max(maxWordLen, word.Length);
                }
            }
            return maxWordLen + 2;
        }).ToArray();

        while (targetWidths.Sum() > available)
        {
            int maxIdx = 0;
            for (int i = 1; i < targetWidths.Length; i++)
            {
                if (targetWidths[i] > targetWidths[maxIdx]) maxIdx = i;
            }
            if (targetWidths[maxIdx] <= minWidths[maxIdx]) break;
            targetWidths[maxIdx]--;
        }

        return targetWidths;
    }

    private static string[][][] WrapAllCells(string[][] cellTexts, int[] targetWidths)
    {
        var result = new string[cellTexts.Length][][];
        for (int r = 0; r < cellTexts.Length; r++)
        {
            result[r] = new string[targetWidths.Length][];
            for (int c = 0; c < targetWidths.Length; c++)
            {
                string text = (r < cellTexts.Length && c < cellTexts[r].Length) ? cellTexts[r][c] : "";
                result[r][c] = WrapText(text, targetWidths[c] - 2);
            }
        }
        return result;
    }

    private static string[] WrapText(string text, int maxWidth)
    {
        if (text.Length <= maxWidth) return [text];
        var words = text.Split(' ');
        var lines = new List<string>();
        string currentLine = "";
        foreach (var word in words)
        {
            if (currentLine.Length == 0)
            {
                currentLine = word;
            }
            else if (currentLine.Length + 1 + word.Length <= maxWidth)
            {
                currentLine += " " + word;
            }
            else
            {
                lines.Add(currentLine);
                currentLine = word;
            }
        }
        if (currentLine.Length > 0) lines.Add(currentLine);
        return lines.ToArray();
    }

    private static string PadText(string text, int width, string align)
    {
        if (align == "right")
        {
            return new string(' ', width - text.Length) + text + " ";
        }
        if (align == "center")
        {
            var leftPad = (width - text.Length) / 2;
            var rightPad = width - text.Length - leftPad + 1;
            return new string(' ', leftPad) + text + new string(' ', rightPad);
        }
        return text + new string(' ', width - text.Length) + " ";
    }

    private static string GetTextFromNode(MarkdownNode node)
    {
        if (node.Type == "text")
        {
            return node.Content;
        }
        if (node.Children != null)
        {
            return string.Join("", node.Children.Select(GetTextFromNode));
        }
        return "";
    }
}
