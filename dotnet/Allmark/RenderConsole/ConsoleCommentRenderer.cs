namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleCommentRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "comment",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

	public static void Render(MarkdownNode node, RendererState state)
	{
        var style = RenderToConsole.Styles["comment"];
		state.Output.Append($"{style}>>");
		RenderChildren.Execute(node, state);
		state.Output.Append($">>{RenderToConsole.AnsiReset}");
	}
}
