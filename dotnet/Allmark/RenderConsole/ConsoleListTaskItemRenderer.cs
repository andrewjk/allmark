namespace Allmark.Render;

using Allmark.Types;

public static class ConsoleListTaskItemRenderer
{
    public static OutputRenderer Create()
    {
        return new OutputRenderer
        {
            Name = "list_task_item",
            Render = Render,
        };
    }

    public static void Render(MarkdownNode node, RendererState state, bool? decode = true)
    {
        var isChecked = node.Markup.Length > 1 && node.Markup[1] != ' ';
        var emoji = isChecked ? "[✓]" : "[ ]";
        state.Output.Append($"{emoji} ");
    }
}
