namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleAlertRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "alert",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var type = node.Markup.ToLower();
        var styles = new Dictionary<string, string>
        {
            ["note"] = Ansi.Blue,
            ["tip"] = Ansi.Green,
            ["important"] = Ansi.Magenta,
            ["warning"] = Ansi.Yellow,
            ["caution"] = Ansi.Red,
        };
        var style = styles.TryGetValue(type, out var s) ? s : styles["note"];
        var icons = new Dictionary<string, string>
        {
            ["note"] = "📝",
            ["tip"] = "💡",
            ["important"] = "❗",
            ["warning"] = "⚠️",
            ["caution"] = "🚨",
        };
        var icon = icons.TryGetValue(type, out var i) ? i : icons["note"];
        if (state.Output.Length > 0 && state.Output[^1] != '\n')
        {
            state.Output.Append('\n');
        }
        state.Output.Append($"{style}{icon} {char.ToUpper(type[0])}{type[1..]}:{Ansi.Reset}\n");
        RenderChildren.Execute(node, state);
    }
}
