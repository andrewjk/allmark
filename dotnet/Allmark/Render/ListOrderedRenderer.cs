namespace Allmark.Render;

using Allmark.Types;

public static class ListOrderedRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "list_ordered",
            Render = ListRenderer.Render,
        };
    }
}
