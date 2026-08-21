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
            var rule = state.RulesMap[node.Type];
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
                var newLength = state.OpenNodes.Count - i - 1;
                while (state.OpenNodes.Count > newLength)
                {
                    var openNode = state.OpenNodes.Pop();
                    Utils.CloseNode(state, openNode);
                }
                break;
            }
        }

        var parent = state.OpenNodes.Peek();

        // Get the end of the line
        var endOfLine = state.I;
        var nextIndex = state.Src.Length;
        for (; endOfLine < state.Src.Length; endOfLine++)
        {
            var code = state.Src[endOfLine];
            if (code == '\n')
            {
                nextIndex = endOfLine + 1;
                break;
            }
            else if (code == '\r')
            {
                nextIndex = endOfLine + 1;
                if (Utils.GetChar(state.Src, endOfLine + 1) == '\n')
                {
                    nextIndex++;
                }
                break;
            }
        }

        ParseBlock.Execute(state, parent, endOfLine);

        // NOTE: a rule can move state.I past the next line
        // (e.g. for a HTML block or link reference containing a newline)
        if (state.I < nextIndex)
        {
            state.I = nextIndex;
            state.LineStart = nextIndex;
        }
    }
}
