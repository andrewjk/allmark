namespace Allmark.Rulesets;

using Allmark.Render;
using Allmark.Render.Console;
using Allmark.Types;

public static class ConsoleRenderers
{
    public static Dictionary<string, OutputRenderer> Renderers => new Dictionary<string, OutputRenderer>
    {
        [ConsoleAlertRenderer.Create().Name] = ConsoleAlertRenderer.Create(),
        [ConsoleBlockQuoteRenderer.Create().Name] = ConsoleBlockQuoteRenderer.Create(),
        [ConsoleCodeBlockRenderer.Create().Name] = CodeBlockRenderer.Create(),
        [ConsoleCodeFenceRenderer.Create().Name] = ConsoleCodeFenceRenderer.Create(),
        [ConsoleCodeSpanRenderer.Create().Name] = ConsoleCodeSpanRenderer.Create(),
        [ConsoleCommentRenderer.Create().Name] = ConsoleCommentRenderer.Create(),
        [ConsoleDeletionRenderer.Create().Name] = ConsoleDeletionRenderer.Create(),
        [ConsoleEmphasisRenderer.Create().Name] = ConsoleEmphasisRenderer.Create(),
        [ConsoleFootnoteRenderer.Create().Name] = ConsoleFootnoteRenderer.Create(),
        [ConsoleHardBreakRenderer.Create().Name] = ConsoleHardBreakRenderer.Create(),
        [ConsoleHeadingRenderer.Create().Name] = ConsoleHeadingRenderer.Create(),
        [ConsoleHeadingUnderlineRenderer.Create().Name] = ConsoleHeadingUnderlineRenderer.Create(),
        [ConsoleHighlightRenderer.Create().Name] = ConsoleHighlightRenderer.Create(),
        [ConsoleHtmlBlockRenderer.Create().Name] = ConsoleHtmlBlockRenderer.Create(),
        [ConsoleHtmlSpanRenderer.Create().Name] = ConsoleHtmlSpanRenderer.Create(),
        [ConsoleImageRenderer.Create().Name] = ConsoleImageRenderer.Create(),
        [ConsoleInsertionRenderer.Create().Name] = ConsoleInsertionRenderer.Create(),
        [ConsoleLinkRenderer.Create().Name] = ConsoleLinkRenderer.Create(),
        [ConsoleListBulletedRenderer.Create().Name] = ConsoleListBulletedRenderer.Create(),
        [ConsoleListOrderedRenderer.Create().Name] = ConsoleListOrderedRenderer.Create(),
        [ConsoleListTaskItemRenderer.Create().Name] = ConsoleListTaskItemRenderer.Create(),
        [ConsoleParagraphRenderer.Create().Name] = ConsoleParagraphRenderer.Create(),
        [ConsoleStrikethroughRenderer.Create().Name] = ConsoleStrikethroughRenderer.Create(),
        [ConsoleStrongRenderer.Create().Name] = ConsoleStrongRenderer.Create(),
        //[ConsoleSubscriptRenderer.Create().Name] = ConsoleSubscriptRenderer.Create(),
        //[ConsoleSuperscriptRenderer.Create().Name] = ConsoleSuperscriptRenderer.Create(),
        [ConsoleTableRenderer.Create().Name] = ConsoleTableRenderer.Create(),
        [ConsoleTextRenderer.Create().Name] = ConsoleTextRenderer.Create(),
        [ConsoleThematicBreakRenderer.Create().Name] = ConsoleThematicBreakRenderer.Create(),
    };
}
