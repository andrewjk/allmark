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

        string MakeLine(string left, string mid, string right, string sep)
        {
            var line = new StringBuilder();
            line.Append(left);
            for (int i = 0; i < columnWidths.Count; i++)
            {
                line.Append(new string('─', columnWidths[i]));
                if (i < columnWidths.Count - 1)
                {
                    line.Append(i == 0 ? mid : sep);
                }
            }
            line.Append(right);
            return $"{style}{line}{Ansi.Reset}\n";
        }

        string PadText(string text, int width, string align)
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

        state.Output.Append(MakeLine("┌", "┬", "┐", "┬"));

        if (headerCells.Count > 0)
        {
            state.Output.Append($"{style}│{Ansi.Reset}");
            for (int i = 0; i < headerCells.Count; i++)
            {
                var text = cellTexts.Count > 0 && i < cellTexts[0].Count ? cellTexts[0][i] : "";
                var align = alignments[i];
                state.Output.Append($" {PadText(text, columnWidths[i] - 2, align)}{style}│{Ansi.Reset}");
            }
            state.Output.Append('\n');
        }

        state.Output.Append(MakeLine("├", "┼", "┤", "┼"));

        for (int r = 0; r < dataRows.Count; r++)
        {
            state.Output.Append($"{style}│{Ansi.Reset}");
            var row = dataRows[r];
            var rowCells = row.Children ?? new List<MarkdownNode>();
            for (int c = 0; c < columnWidths.Count; c++)
            {
                var text = (r + 1) < cellTexts.Count && c < cellTexts[r + 1].Count ? cellTexts[r + 1][c] : "";
                var align = c < rowCells.Count ? (rowCells[c].Info ?? "") : "";
                state.Output.Append($" {PadText(text, columnWidths[c] - 2, align)}{style}│{Ansi.Reset}");
            }
            state.Output.Append('\n');
        }

        state.Output.Append(MakeLine("└", "┴", "┘", "┴"));
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
