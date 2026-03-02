namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleBlockQuoteRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "block_quote",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        var style = Ansi.Dim;
        state.QuoteDepth++;
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }

        // Render content if it exists
        if (!string.IsNullOrEmpty(node.Content))
        {
            foreach (var line in node.Content.Split('\n'))
            {
                if (!string.IsNullOrEmpty(line))
                {
                    state.Output.Append($"{style}┃{Ansi.Reset} {line}\n");
                }
            }
        }

        // Render children
        if (node.Children != null)
        {
            foreach (var child in node.Children)
            {
                var childOutput = RenderNodeToString(child, state);
                foreach (var line in childOutput.Split('\n'))
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        state.Output.Append($"{style}┃{Ansi.Reset} {line}\n");
                    }
                }
            }
        }

        state.QuoteDepth--;
    }

    private static string RenderNodeToString(MarkdownNode node, RendererState state)
    {
        var originalOutput = state.Output.ToString();
        state.Output.Clear();

        if (state.Renderers != null && state.Renderers.TryGetValue(node.Type, out var renderer))
        {
            renderer.Render(node, state, false, false, true);
        }

        var result = state.Output.ToString();
        state.Output.Clear();
        state.Output.Append(originalOutput);
        return result;
    }
}
