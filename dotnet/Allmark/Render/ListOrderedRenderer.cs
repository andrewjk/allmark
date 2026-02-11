namespace Allmark.Render;

using Allmark.Types;

public static class ListOrderedRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "list_ordered",
			Render = ListRenderer.Render,
		};
	}
}
