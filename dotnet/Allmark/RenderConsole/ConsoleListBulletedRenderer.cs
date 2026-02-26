namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleListBulletedRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "list_bulleted",
            Render = (node, state, first, last, decode) => ConsoleListRenderer.Render(node, state, false),
        };
    }
}
