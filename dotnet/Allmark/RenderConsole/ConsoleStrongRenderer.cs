namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleStrongRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "strong",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
    {
        state.Output.Append($"{RenderToConsole.Styles["strong"]}");
        RenderChildren.Execute(node, state);
        state.Output.Append($"{RenderToConsole.AnsiReset}");
    }
}
