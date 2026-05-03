namespace Allmark.Rulesets;

using Allmark.Render;
using Allmark.Types;

public static class HtmlRenderers
{
    public static OutputRenderer[] Renderers => new OutputRenderer[]
    {
        AlertRenderer.Create(),
        BlockQuoteRenderer.Create(),
        CodeBlockRenderer.Create(),
        CodeFenceRenderer.Create(),
        CodeSpanRenderer.Create(),
        CommentRenderer.Create(),
        DeletionRenderer.Create(),
        EmphasisRenderer.Create(),
        FootnoteRenderer.Create(),
        FootnoteRefRenderer.Create(),
        FootnoteListRenderer.Create(),
        HardBreakRenderer.Create(),
        HeadingRenderer.Create(),
        HeadingUnderlineRenderer.Create(),
        HighlightRenderer.Create(),
        HtmlBlockRenderer.Create(),
        HtmlSpanRenderer.Create(),
        ImageRenderer.Create(),
        InsertionRenderer.Create(),
        LinkRenderer.Create(),
        ListBulletedRenderer.Create(),
        ListOrderedRenderer.Create(),
        ListTaskItemRenderer.Create(),
        ParagraphRenderer.Create(),
        StrikethroughRenderer.Create(),
        StrongRenderer.Create(),
        SubscriptRenderer.Create(),
        SuperscriptRenderer.Create(),
        TableRenderer.Create(),
        TextRenderer.Create(),
        ThematicBreakRenderer.Create(),
    };
}
