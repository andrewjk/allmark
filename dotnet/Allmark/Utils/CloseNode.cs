namespace Allmark;

using Allmark.Types;
using System.Linq;

public static partial class Utils
{
	public static void CloseNode(BlockParserState state, MarkdownNode node)
	{
		// Call the close function of each open child after (and including) this one
		for (int i = 0; i < state.OpenNodes.Count - 1; i++)
		{
			var openNode = state.OpenNodes.ElementAt(i);
			var rule = state.Rules[openNode.Type];
			// TODO: Add close functions for dangling nodes, so they get cleaned up more tidily
			rule.CloseNode?.Invoke(state, openNode);
			if (openNode == node)
			{
				break;
			}
		}
	}
}
