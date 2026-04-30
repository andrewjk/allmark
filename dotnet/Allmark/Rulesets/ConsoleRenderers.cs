namespace Allmark.Rulesets;

using Allmark.Render;
using Allmark.Render.Console;
using Allmark.Types;

public static class ConsoleRenderers
{
    public static OutputRenderer[] Renderers => new OutputRenderer[]
    {
        ConsoleAlertRenderer.Create(),
        ConsoleBlockQuoteRenderer.Create(),
        ConsoleCodeBlockRenderer.Create(),
        ConsoleCodeFenceRenderer.Create(),
        ConsoleCodeSpanRenderer.Create(),
        ConsoleCommentRenderer.Create(),
        ConsoleDeletionRenderer.Create(),
        ConsoleEmphasisRenderer.Create(),
        ConsoleFootnoteRenderer.Create(),
        ConsoleHardBreakRenderer.Create(),
        ConsoleHeadingRenderer.Create(),
        ConsoleHeadingUnderlineRenderer.Create(),
        ConsoleHighlightRenderer.Create(),
        ConsoleHtmlBlockRenderer.Create(),
        ConsoleHtmlSpanRenderer.Create(),
        ConsoleImageRenderer.Create(),
        ConsoleInsertionRenderer.Create(),
        ConsoleLinkRenderer.Create(),
        ConsoleListBulletedRenderer.Create(),
        ConsoleListOrderedRenderer.Create(),
        ConsoleListTaskItemRenderer.Create(),
        ConsoleParagraphRenderer.Create(),
        ConsoleStrikethroughRenderer.Create(),
        ConsoleStrongRenderer.Create(),
        ConsoleTableRenderer.Create(),
        ConsoleTextRenderer.Create(),
        ConsoleThematicBreakRenderer.Create(),
    };
}
