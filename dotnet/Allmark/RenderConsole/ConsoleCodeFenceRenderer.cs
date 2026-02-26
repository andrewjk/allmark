namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleCodeFenceRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "code_fence",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        var content = (node.Content ?? "").TrimEnd('\n');
        var lines = content.Split('\n');

        if (lines.Length == 0 || (lines.Length == 1 && string.IsNullOrEmpty(lines[0])))
        {
            state.Output.Append($"{RenderToConsole.AnsiDim}┌─{RenderToConsole.AnsiReset}\n{RenderToConsole.AnsiDim}└─{RenderToConsole.AnsiReset}");
        }
        else
        {
            state.Output.Append($"{RenderToConsole.AnsiDim}┌─{RenderToConsole.AnsiReset}\n");
            for (int i = 0; i < lines.Length; i++)
            {
                // Skip the last empty line
                if (i == lines.Length - 1 && string.IsNullOrEmpty(lines[i]))
                {
                    continue;
                }
                state.Output.Append($"{RenderToConsole.AnsiDim}│{RenderToConsole.AnsiReset} {lines[i].TrimEnd('\r')}\n");
            }
            state.Output.Append($"{RenderToConsole.AnsiDim}└─{RenderToConsole.AnsiReset}");
        }
    }
}
