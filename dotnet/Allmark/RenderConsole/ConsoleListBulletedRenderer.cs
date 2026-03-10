namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleListBulletedRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "list_bulleted",
            Render = (node, state, first, last, decode) => ConsoleListRenderer.Render(node, state, false),
        };
    }
}
