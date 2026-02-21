namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleHeadingRenderer
{
    public static Renderer CreateConsole(Dictionary<string, string> styles)
    {
        return new Renderer
        {
            Name = "heading",
            Render = (node, state, first, last, decode) => Render(node, state, styles),
        };
    }

    public static void Render(MarkdownNode node, RendererState state, Dictionary<string, string> styles)
    {
        var level = 0;
        var isSetext = node.Markup.Contains("=") || node.Markup.Contains("-");
        if (node.Markup.StartsWith("#"))
        {
            level = node.Markup.Length;
        }
        else if (isSetext)
        {
            if (node.Markup.Contains("="))
            {
                level = 1;
            }
            else
            {
                level = 2;
            }
        }

        var style = styles.TryGetValue($"heading{level}", out var s) ? s : "";
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }

        if (isSetext)
        {
            var plainTextLength = GetPlainTextLength(node);
            var underlineChar = level == 1 ? '=' : '-';
            state.Output.Append(style);
            RenderChildren.Execute(node, state);
            state.Output.Append($"\n{new string(underlineChar, plainTextLength)}{RenderToConsole.AnsiReset}\n");
        }
        else
        {
            state.Output.Append($"{style}{new string('#', level)} ");
            RenderChildren.Execute(node, state);
            state.Output.Append($"{RenderToConsole.AnsiReset}\n");
        }
    }

    private static int GetPlainTextLength(MarkdownNode node)
    {
        if (node.Type == "text")
        {
            return node.Markup?.Length ?? 0;
        }
        if (node.Children != null)
        {
            return node.Children.Sum(child => GetPlainTextLength(child));
        }
        return 0;
    }
}
