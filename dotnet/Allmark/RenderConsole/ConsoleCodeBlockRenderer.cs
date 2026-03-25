namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleCodeBlockRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "code_block",
            Render = (node, state, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var style = Ansi.Dim;
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }
        state.Output.Append($"{style}┌─{Ansi.Reset}\n");
        foreach (var line in node.Content.Split('\n'))
        {
            state.Output.Append($"{style}│{Ansi.Reset} {line}\n");
        }
        state.Output.Append($"{style}└─{Ansi.Reset}\n");
    }
}
