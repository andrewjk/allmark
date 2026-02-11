namespace Allmark.Parse;

using Allmark.Types;

public static class ParseInline
{
	public static void Execute(InlineParserState state, MarkdownNode parent)
	{
		while (state.I < state.Src.Length)
		{
			char c = Utils.GetChar(state.Src, state.I);
			if (c == '\r' || c == '\n')
			{
				// Treat Windows \r\n as \n
				if (c == '\r' && state.I + 1 < state.Src.Length && state.Src[state.I + 1] == '\n')
				{
					state.I++;
				}

				state.Line += 1;
				state.LineStart = state.I;
			}

			foreach (var rule in state.Rules.Values)
			{
				bool handled = rule.Test(state, parent);
				// Console.WriteLine("Rule:", rule.Name, handled);
				if (handled)
				{
					// TODO: Make sure that state.I has been incremented to prevent infinite loops
					// Console.WriteLine($"Found {rule.Name}");
					break;
				}
			}
		}
	}
}
