namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleAlertRenderer
{
    public static Renderer Create()
    {
        return new Renderer
        {
            Name = "alert",
            Render = (node, state, first, last, decode) => Render(node, state),
        };
    }

    public static void Render(MarkdownNode node, RendererState state)
    {
        var styles = RenderToConsole.Styles;
        var type = node.Markup.ToLower();
        var style = styles.TryGetValue($"alert{char.ToUpper(type[0])}{type[1..]}", out var s) ? s : styles["alertNote"]!;
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
        state.Output.Append($"{style}{icon} {char.ToUpper(type[0])}{type[1..]}:{RenderToConsole.AnsiReset}\n");
        RenderChildren.Execute(node, state);
    }
}
