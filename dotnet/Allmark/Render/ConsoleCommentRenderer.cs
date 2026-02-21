namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleCommentRenderer
{
    public static Renderer Create(string style, string reset)
    {
        return new Renderer
        {
            Name = "comment",
            Render = (node, state, first, last, decode) => Render(node, state, style, reset),
        };
    }

    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "comment",
            Render = (node, state, first, last, decode) => Render(node, state, RenderToConsole.AnsiBlue, RenderToConsole.AnsiReset),
        };
    }

	public static void Render(MarkdownNode node, RendererState state, string style, string reset)
	{
		state.Output.Append($"{style}>>");
		RenderChildren.Execute(node, state);
		state.Output.Append($">>{reset}");
	}
}
