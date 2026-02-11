namespace Allmark.Parse;

using Allmark.Types;

public static class ParseLine
{
	public static void Execute(BlockParserState state)
	{
		state.Indent = 0;
		state.Line++;
		state.LineStart = state.I;
		state.MaybeContinue = false;

		// if (state.Debug)
		// {
		// 	Console.WriteLine(
		// 		$"Parsing line {state.Line} at {state.I} with open nodes [{string.Join(", ", state.OpenNodes.Select(n => n.Type))}]"
		// 	);
		// }
		ParseIndent.Execute(state);

		// Skip document -- it's always going to continue
		for (int i = state.OpenNodes.Count - 2; i >= 0; i--)
		{
			var node = state.OpenNodes.ElementAt(i);
			// TODO: Fallback rule??
			var rule = state.Rules[node.Type];
			// if (state.Debug && rule == null)
			// {
			// 	Console.WriteLine("RULE NOT FOUND:", node.Type);
			// }
			if (rule.TestContinue(state, node))
			{
				// TODO: Is there a rule that shouldn't do this?
				ParseIndent.Execute(state);
			}
			else
			{
				Utils.CloseNode(state, node);
				var newLength = state.OpenNodes.Count - i - 1;
				while (state.OpenNodes.Count > newLength)
				{
					state.OpenNodes.Pop();
				}
				break;
			}
		}

		var parent = state.OpenNodes.Peek();
		ParseBlock.Execute(state, parent);
	}
}
