namespace Allmark;

using System.Text;
using Allmark.Rulesets;
using Allmark.Types;

public static class RenderToConsole
{
    public const string AnsiReset = "\x1b[0m";
    public const string AnsiBold = "\x1b[1m";
    public const string AnsiItalic = "\x1b[3m";
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

    public static readonly Dictionary<string, string> Styles = new Dictionary<string, string>
    {
        ["heading1"] = $"{AnsiBold}{AnsiCyan}",
        ["heading2"] = $"{AnsiBold}{AnsiBlue}",
        ["heading3"] = $"{AnsiBold}{AnsiMagenta}",
        ["heading4"] = AnsiBold,
        ["heading5"] = $"{AnsiDim}{AnsiBold}",
        ["heading6"] = $"{AnsiDim}{AnsiBold}",
        ["strong"] = $"{AnsiBold}{AnsiYellow}",
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
        ["strikethrough"] = AnsiDim,
        ["footnote"] = AnsiDim,
        ["table"] = AnsiDim,
    };

    public static string Execute(MarkdownNode doc, Dictionary<string, Renderer>? renderers = null)
    {
        renderers ??= ConsoleRenderers.Renderers;

        var state = new RendererState
        {
            Renderers = renderers,
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
}
