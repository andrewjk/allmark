namespace Allmark.Parse;

using Allmark.Types;

public static class ParseInline
{
    public static void Execute(InlineParserState state, MarkdownNode parent)
    {
        while (state.I < state.Src.Length)
        {
            char c = Utils.GetChar(state.Src, state.I);
            if (c == '\n')
            {
                state.Indent = 0;
                state.Line += 1;
                state.LineStart = state.I;
            }

            state.IsEscaped = Utils.IsEscaped(state.Src, state.I);

            foreach (var rule in state.Rules)
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
