namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleListOrderedRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "list_ordered",
            Render = (node, state, first, last, decode) => ConsoleListRenderer.Render(node, state, true),
        };
    }
}
