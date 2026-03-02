namespace Allmark.Render;

using Allmark.Types;
using System.Text;

public static class ConsoleTableRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "table",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Dim;
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }

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

        for (int i = 0; i < headerCells.Count; i++)
        {
            var text = GetTextFromNode(headerCells[i]);
            if (cellTexts.Count == 0)
            {
                cellTexts.Add(new List<string>());
            }
            cellTexts[0].Add(text);
            columnWidths[i] = Math.Max(columnWidths[i], text.Length + 2);
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

        state.Output.Append(MakeLine("┌", "┬", "┐", "┬"));

        if (headerCells.Count > 0)
        {
            state.Output.Append($"{style}│{Ansi.Reset}");
            for (int i = 0; i < headerCells.Count; i++)
            {
                var text = cellTexts.Count > 0 && i < cellTexts[0].Count ? cellTexts[0][i] : "";
                var padding = new string(' ', columnWidths[i] - text.Length - 1);
                state.Output.Append($" {text}{padding}{style}│{Ansi.Reset}");
            }
            state.Output.Append('\n');
        }

        state.Output.Append(MakeLine("├", "┼", "┤", "┼"));

        for (int r = 0; r < dataRows.Count; r++)
        {
            state.Output.Append($"{style}│{Ansi.Reset}");
            for (int c = 0; c < columnWidths.Count; c++)
            {
                var text = (r + 1) < cellTexts.Count && c < cellTexts[r + 1].Count ? cellTexts[r + 1][c] : "";
                var padding = new string(' ', columnWidths[c] - text.Length - 1);
                state.Output.Append($" {text}{padding}{style}│{Ansi.Reset}");
            }
            state.Output.Append('\n');
        }

        state.Output.Append(MakeLine("└", "┴", "┘", "┴"));
    }

    private static string GetTextFromNode(MarkdownNode node)
    {
        if (node.Type == "text")
        {
            return node.Markup;
        }
        if (node.Children != null)
        {
            return string.Join("", node.Children.Select(GetTextFromNode));
        }
        return "";
    }
}
