namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleCodeBlockRenderer
{
    public static Renderer Create(string style)
    {
        return new Renderer
        {
            Name = "code_block",
            Render = (node, state, first, last, decode) => Render(node, state, style),
        };
    }

    public static void Render(MarkdownNode node, RendererState state, string style)
    {
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }
        state.Output.Append($"{style}┌─{RenderToConsole.AnsiReset}\n");
        foreach (var line in node.Content.Split('\n'))
        {
            state.Output.Append($"{style}│{RenderToConsole.AnsiReset} {line}\n");
        }
        state.Output.Append($"{style}└─{RenderToConsole.AnsiReset}\n");
    }
}
