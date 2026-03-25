namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleHeadingRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "heading",
            Render = (node, state, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
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

        var style = Ansi.Bold + Ansi.Magenta;
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }

        if (isSetext)
        {
            var headingText = GetHeadingText(node, state);
            var plainText = StripAnsiCodes(headingText);
            var plainTextLength = plainText.Length;
            var underlineChar = level == 1 ? '=' : '-';
            state.Output.Append(style);
            state.Output.Append(headingText);
            state.Output.Append($"\n{Ansi.Reset}{Ansi.Dim}{new string(underlineChar, plainTextLength)}{Ansi.Reset}\n");
        }
        else
        {
            state.Output.Append($"{Ansi.Dim}{new string('#', level)}{Ansi.Reset} {style}");
            RenderChildren.Execute(node, state);
            state.Output.Append($"{Ansi.Reset}\n");
        }
    }

    private static string GetHeadingText(MarkdownNode node, RendererState state)
    {
        var text = new System.Text.StringBuilder();
        foreach (var child in node.Children ?? [])
        {
            if (child.Type == "text")
            {
                text.Append(child.Content);
            }
            else
            {
                text.Append(RenderNodeToString(child, state));
            }
        }
        return text.ToString();
    }

    private static string RenderNodeToString(MarkdownNode node, RendererState state)
    {
        var originalOutput = state.Output.ToString();
        state.Output.Clear();

        if (state.Renderers != null && state.Renderers.TryGetValue(node.Type, out var renderer))
        {
            renderer.Render(node, state, true);
        }

        var result = state.Output.ToString();
        state.Output.Clear();
        state.Output.Append(originalOutput);
        return result;
    }

    private static string StripAnsiCodes(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, @"\x1b\\[[0-9;]*m", "");
    }
}
