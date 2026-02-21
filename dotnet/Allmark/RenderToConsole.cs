namespace Allmark;

using System.Text;
using Allmark.Types;

public static class RenderToConsole
{
    public const string AnsiReset = "\x1b[0m";
    public const string AnsiBold = "\x1b[1m";
    public const string AnsiDim = "\x1b[2m";
    public const string AnsiGray = "\x1b[90m";
    public const string AnsiRed = "\x1b[31m";
    public const string AnsiGreen = "\x1b[32m";
    public const string AnsiYellow = "\x1b[33m";
    public const string AnsiBlue = "\x1b[34m";
    public const string AnsiMagenta = "\x1b[35m";
    public const string AnsiCyan = "\x1b[36m";
    public const string AnsiOrange = "\x1b[38;5;208m";
    public const string AnsiUnderline = "\x1b[4m";
    public const string AnsiBlack = "\x1b[30m";
    public const string AnsiYellowBg = "\x1b[43m";
    public const string AnsiStrikethrough = "\x1b[9m";
    public const string AnsiStrikethroughReset = "\x1b[29m";

    public static readonly string[] ConsoleBullets = ["•", "◦", "▪", "‣"];

    public static string Execute(MarkdownNode doc, RuleSet rules)
    {
        var consoleRenderers = GetConsoleRenderers(rules.Renderers);

        var state = new RendererState
        {
            Renderers = consoleRenderers,
            Output = new StringBuilder(),
            Footnotes = new List<MarkdownNode>(),
            Depth = 0,
            QuoteDepth = 0
        };

        Render.RenderChildren.Execute(doc, state);

        var output = state.Output.ToString();
        output = System.Text.RegularExpressions.Regex.Replace(output, @"\n+$", "");

        return output;
    }

    private static Dictionary<string, Renderer> GetConsoleRenderers(Dictionary<string, Renderer> htmlRenderers)
    {
        var consoleRenderers = new Dictionary<string, Renderer>();

        var styles = new Dictionary<string, string>
        {
            ["heading1"] = $"{AnsiBold}{AnsiCyan}",
            ["heading2"] = $"{AnsiBold}{AnsiBlue}",
            ["heading3"] = $"{AnsiBold}{AnsiMagenta}",
            ["heading4"] = AnsiBold,
            ["heading5"] = $"{AnsiDim}{AnsiBold}",
            ["heading6"] = $"{AnsiDim}{AnsiBold}",
            ["strong"] = $"{AnsiBold}{AnsiOrange}",
            ["emphasis"] = AnsiYellow,
            ["code"] = AnsiGreen,
            ["link"] = $"{AnsiBlue}{AnsiUnderline}",
            ["blockQuote"] = AnsiGray,
            ["codeBlock"] = AnsiDim,
            ["thematicBreak"] = AnsiDim,
            ["alertNote"] = AnsiBlue,
            ["alertTip"] = AnsiGreen,
            ["alertImportant"] = AnsiMagenta,
            ["alertWarning"] = AnsiYellow,
            ["alertCaution"] = AnsiRed,
            ["highlight"] = $"{AnsiYellowBg}{AnsiBlack}",
        };

        consoleRenderers["paragraph"] = Render.ConsoleParagraphRenderer.CreateConsole();
        consoleRenderers["heading"] = Render.ConsoleHeadingRenderer.CreateConsole(styles);
        consoleRenderers["heading_underline"] = Render.ConsoleHeadingRenderer.CreateConsole(styles);
        consoleRenderers["thematic_break"] = Render.ConsoleThematicBreakRenderer.Create(styles["thematicBreak"]!);
        consoleRenderers["block_quote"] = Render.ConsoleBlockQuoteRenderer.Create(styles["blockQuote"]!);
        consoleRenderers["list_bulleted"] = Render.ConsoleListRenderer.Create(false);
        consoleRenderers["list_ordered"] = Render.ConsoleListRenderer.Create(true);
        consoleRenderers["list_task_item"] = Render.ConsoleListTaskItemRenderer.Create();
        consoleRenderers["code_block"] = Render.ConsoleCodeBlockRenderer.Create(styles["codeBlock"]!);
        consoleRenderers["code_fence"] = Render.ConsoleCodeBlockRenderer.Create(styles["codeBlock"]!);
        consoleRenderers["code_span"] = Render.ConsoleCodeSpanRenderer.Create(styles["code"]!);
        consoleRenderers["strong"] = Render.ConsoleInlineRenderer.Create(styles["strong"]!);
        consoleRenderers["emphasis"] = Render.ConsoleInlineRenderer.Create(styles["emphasis"]!);
        consoleRenderers["strikethrough"] = Render.ConsoleStrikethroughRenderer.Create(AnsiDim);
        consoleRenderers["link"] = Render.ConsoleLinkRenderer.Create(styles["link"]!);
        consoleRenderers["image"] = Render.ConsoleImageRenderer.Create(AnsiDim);
        consoleRenderers["text"] = Render.ConsoleTextRenderer.Create();
        consoleRenderers["hard_break"] = Render.ConsoleHardBreakRenderer.Create();
        consoleRenderers["alert"] = Render.ConsoleAlertRenderer.Create(styles);
        consoleRenderers["footnote"] = Render.ConsoleFootnoteRenderer.Create(AnsiDim);
        consoleRenderers["table"] = Render.ConsoleTableRenderer.Create(AnsiDim);
        consoleRenderers["html_block"] = Render.ConsoleHtmlRenderer.Create();
        consoleRenderers["html_span"] = Render.ConsoleHtmlRenderer.Create();
        consoleRenderers["highlight"] = Render.ConsoleHighlightRenderer.Create(styles["highlight"]!, AnsiReset);
        consoleRenderers["insertion"] = Render.ConsoleInsertionRenderer.Create();
        consoleRenderers["deletion"] = Render.ConsoleDeletionRenderer.Create();
        consoleRenderers["comment"] = Render.ConsoleCommentRenderer.Create();

        return consoleRenderers;
    }
}
