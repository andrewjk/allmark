namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleListOrderedRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "list_ordered",
            Render = (node, state, decode) => ConsoleListRenderer.Render(node, state, true),
        };
    }
}
