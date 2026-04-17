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
        state.Output.Append($"{style}┌─{Ansi.Reset}\n");
        var lines = (node.Content ?? "").Split('\n');
        // Remove last line if empty
        if (lines.Length > 0 && string.IsNullOrEmpty(lines[^1]))
        {
            lines = lines.Take(lines.Length - 1).ToArray();
        }
        foreach (var line in lines)
        {
            state.Output.Append($"{style}│{Ansi.Reset} {line}\n");
        }
        state.Output.Append($"{style}└─{Ansi.Reset}\n\n");
    }
}
