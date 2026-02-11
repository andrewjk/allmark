namespace Allmark.Render;

using Allmark.Types;

public static class HeadingUnderlineRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "heading_underline",
			Render = HeadingRenderer.Render,
		};
	}
}
