namespace Allmark.Render;

using Allmark.Types;

public static class ListBulletedRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "list_bulleted",
			Render = ListRenderer.Render,
		};
	}
}
