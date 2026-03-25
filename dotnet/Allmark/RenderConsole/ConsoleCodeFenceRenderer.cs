namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleCodeFenceRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "code_fence",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        var content = (node.Content ?? "").TrimEnd('\n');
        var lines = content.Split('\n');

        if (lines.Length == 0 || (lines.Length == 1 && string.IsNullOrEmpty(lines[0])))
        {
            state.Output.Append($"{Ansi.Dim}┌─{Ansi.Reset}\n{Ansi.Dim}└─{Ansi.Reset}\n");
        }
        else
        {
            state.Output.Append($"{Ansi.Dim}┌─{Ansi.Reset}\n");
            for (int i = 0; i < lines.Length; i++)
            {
                // Skip last empty line
                if (i == lines.Length - 1 && string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }
                state.Output.Append($"{Ansi.Dim}│{Ansi.Reset} {lines[i].TrimEnd('\r')}\n");
            }
            state.Output.Append($"{Ansi.Dim}└─{Ansi.Reset}\n");
        }
    }
}
