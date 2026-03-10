namespace Allmark.Render;

using Allmark.Types;

public static class ListBulletedRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "list_bulleted",
            Render = ListRenderer.Render,
        };
    }
}
