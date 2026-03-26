namespace Allmark.Render.Console;

using Allmark.Types;
using System.Text.RegularExpressions;

public static class ConsoleHeadingUnderlineRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "heading_underline",
            Render = (node, state, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var level = 0;
        if (node.Markup.Contains("="))
        {
            level = 1;
        }
        else if (node.Markup.Contains("-"))
        {
            level = 2;
        }

        var style = Ansi.Bold + Ansi.Magenta;

        // Build heading text by manually rendering children
        var headingTextBuilder = new System.Text.StringBuilder();
        if (node.Children != null)
        {
            foreach (var child in node.Children)
            {
                if (child.Type == "text")
                {
                    headingTextBuilder.Append(child.Content);
                }
                else if (state.Renderers != null && state.Renderers.TryGetValue(child.Type, out var renderer))
                {
                    // Temporarily capture current output and render child to a new buffer
                    var childOutput = RenderNodeToString(child, state);
                    headingTextBuilder.Append(childOutput);
                }
            }
        }

        var headingText = headingTextBuilder.ToString();
        var plainText = StripAnsiCodes(headingText);
        var plainTextLength = plainText.Length;
        var underlineChar = level == 1 ? '=' : '-';

        state.Output.Append(style);
        state.Output.Append(headingText);
        state.Output.Append($"{Ansi.Reset}\n");
        state.Output.Append($"{Ansi.Dim}{new string(underlineChar, plainTextLength)}{Ansi.Reset}\n");
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
        return Regex.Replace(input, @"\x1b\\[[0-9;]*m", "");
    }
}
