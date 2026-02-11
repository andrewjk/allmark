namespace Allmark.Block;

using Allmark.Types;

public static class ListTaskItemRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "list_task_item",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		if (parent.Type == "list_item")
		{
			var start = state.I;
			if (
				Utils.GetChar(state.Src, start) == '[' &&
				Utils.GetChar(state.Src, start + 2) == ']' &&
				Utils.IsSpace(Utils.GetChar(state.Src, start + 3)) &&
				// GitHub doesn't support task lists in block quotes
				!state.OpenNodes.Any((n) => n.Type == "block_quote"))
			{
				var markup = $"[{Utils.GetChar(state.Src, start + 1)}]";
				// HACK: It should be a block, but it's not for output reasons
				var task = Utils.NewNode("list_task_item", false, state.I, state.Line, 1, markup, 0, []);
				parent.Children!.Add(task);
				state.I = start + 3;
			}
		}

		return false;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		return false;
	}
}
