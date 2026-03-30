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
        var level = node.Markup.Length;

        var style = Ansi.Bold + Ansi.Magenta;
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }

        state.Output.Append($"{Ansi.Dim}{new string('#', level)}{Ansi.Reset} {style}");
        // Render the dummy paragraph's children directly (not the paragraph itself)
        if (node.Children != null && node.Children.Count > 0)
        {
            RenderChildren.Execute(node.Children[0], state);
        }
        state.Output.Append($"{Ansi.Reset}\n");
    }
}
