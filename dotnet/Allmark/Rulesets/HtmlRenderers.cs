namespace Allmark.Rulesets;

using Allmark.Render;
using Allmark.Types;

public static class HtmlRenderers
{
    public static Dictionary<string, OutputRenderer> Renderers => new Dictionary<string, OutputRenderer>
    {
        [AlertRenderer.Create().Name] = AlertRenderer.Create(),
        [BlockQuoteRenderer.Create().Name] = BlockQuoteRenderer.Create(),
        [CodeBlockRenderer.Create().Name] = CodeBlockRenderer.Create(),
        [CodeFenceRenderer.Create().Name] = CodeFenceRenderer.Create(),
        [CodeSpanRenderer.Create().Name] = CodeSpanRenderer.Create(),
        [CommentRenderer.Create().Name] = CommentRenderer.Create(),
        [DeletionRenderer.Create().Name] = DeletionRenderer.Create(),
        [EmphasisRenderer.Create().Name] = EmphasisRenderer.Create(),
        [FootnoteRenderer.Create().Name] = FootnoteRenderer.Create(),
        [FootnoteListRenderer.Create().Name] = FootnoteListRenderer.Create(),
        [HardBreakRenderer.Create().Name] = HardBreakRenderer.Create(),
        [HeadingRenderer.Create().Name] = HeadingRenderer.Create(),
        [HeadingUnderlineRenderer.Create().Name] = HeadingUnderlineRenderer.Create(),
        [HighlightRenderer.Create().Name] = HighlightRenderer.Create(),
        [HtmlBlockRenderer.Create().Name] = HtmlBlockRenderer.Create(),
        [HtmlSpanRenderer.Create().Name] = HtmlSpanRenderer.Create(),
        [ImageRenderer.Create().Name] = ImageRenderer.Create(),
        [InsertionRenderer.Create().Name] = InsertionRenderer.Create(),
        [LinkRenderer.Create().Name] = LinkRenderer.Create(),
        [ListBulletedRenderer.Create().Name] = ListBulletedRenderer.Create(),
        [ListOrderedRenderer.Create().Name] = ListOrderedRenderer.Create(),
        [ListTaskItemRenderer.Create().Name] = ListTaskItemRenderer.Create(),
        [ParagraphRenderer.Create().Name] = ParagraphRenderer.Create(),
        [StrikethroughRenderer.Create().Name] = StrikethroughRenderer.Create(),
        [StrongRenderer.Create().Name] = StrongRenderer.Create(),
        [SubscriptRenderer.Create().Name] = SubscriptRenderer.Create(),
        [SuperscriptRenderer.Create().Name] = SuperscriptRenderer.Create(),
        [TableRenderer.Create().Name] = TableRenderer.Create(),
        [TextRenderer.Create().Name] = TextRenderer.Create(),
        [ThematicBreakRenderer.Create().Name] = ThematicBreakRenderer.Create(),
    };
}
