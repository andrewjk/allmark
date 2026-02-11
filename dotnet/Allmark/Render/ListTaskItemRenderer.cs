namespace Allmark.Render;

using Allmark.Types;

public static class ListTaskItemRenderer
{
	public static Renderer Create()
	{
		return new Renderer
		{
			Name = "list_task_item",
			Render = Render,
		};
	}

	public static void Render(MarkdownNode node, RendererState state, bool? first = null, bool? last = null, bool? decode = true)
	{
		bool isChecked = node.Markup.Length > 1 && !Utils.IsSpace(node.Markup[1]);
		string checkedAttr = isChecked ? " checked=\"\"" : "";
		state.Output.Append($"<input type=\"checkbox\"{checkedAttr} disabled=\"\" /> ");
	}
}
