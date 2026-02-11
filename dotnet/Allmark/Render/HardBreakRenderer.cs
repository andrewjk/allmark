namespace Allmark.Render;

using Allmark.Types;

public static class HardBreakRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "hard_break",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		state.Output.AppendLine(@"<br />");
	}
}
