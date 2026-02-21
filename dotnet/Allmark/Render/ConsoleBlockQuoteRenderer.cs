namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleBlockQuoteRenderer
{
    public static Renderer Create(string style)
    {
        return new Renderer
        {
            Name = "block_quote",
            Render = (node, state, first, last, decode) => Render(node, state, style),
        };
    }

    public static void Render(MarkdownNode node, RendererState state, string style)
    {
        state.QuoteDepth++;
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }
        RenderChildren.Execute(node, state);
        state.QuoteDepth--;
    }
}
