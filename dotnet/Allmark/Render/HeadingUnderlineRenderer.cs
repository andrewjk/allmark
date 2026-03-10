namespace Allmark.Render;

using Allmark.Types;

public static class HeadingUnderlineRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "heading_underline",
            Render = HeadingRenderer.Render,
        };
    }
}
