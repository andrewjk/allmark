namespace Allmark;

using System.Text;
using Allmark.Rulesets;
using Allmark.Types;

public static class RenderToConsole
{
    public static readonly string[] ConsoleBullets = ["•", "◦", "▪", "‣"];

    public static readonly Dictionary<string, string> Styles = new Dictionary<string, string>
    {
        ["heading1"] = $"{Ansi.Bold}{Ansi.Blue}",
        ["heading2"] = $"{Ansi.Bold}{Ansi.Blue}",
        ["heading3"] = $"{Ansi.Bold}{Ansi.Blue}",
        ["heading4"] = $"{Ansi.Bold}{Ansi.Blue}",
        ["heading5"] = $"{Ansi.Bold}{Ansi.Blue}",
        ["heading6"] = $"{Ansi.Bold}{Ansi.Blue}",
        ["strong"] = $"{Ansi.Bold}{Ansi.Yellow}",
        ["emphasis"] = Ansi.Italic + Ansi.Yellow,
        ["code"] = Ansi.Green,
        ["link"] = $"{Ansi.Blue}{Ansi.Underline}",
        ["blockQuote"] = Ansi.Gray,
        ["codeBlock"] = Ansi.Dim,
        ["thematicBreak"] = Ansi.Dim,
        ["alertNote"] = Ansi.Blue,
        ["alertTip"] = Ansi.Green,
        ["alertImportant"] = Ansi.Magenta,
        ["alertWarning"] = Ansi.Yellow,
        ["alertCaution"] = Ansi.Red,
        ["highlight"] = $"{Ansi.YellowBg}{Ansi.Black}",
        ["strikethrough"] = Ansi.Dim,
        ["footnote"] = Ansi.Dim,
        ["table"] = Ansi.Dim,
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
