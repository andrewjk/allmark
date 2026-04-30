namespace Allmark.Parse;

using Allmark.Types;

public static class ParseBlock
{
    public static void Execute(BlockParserState state, MarkdownNode parent)
    {
        state.IsEscaped = Utils.IsEscaped(state.Src, state.I);

        foreach (var rule in state.Rules)
        {
            // int start = state.I;
            bool handled = rule.TestStart(state, parent);

            if (handled)
            {
                // if (state.Debug)
                // {
                // 	Console.WriteLine($"Found {rule.Name}, at {start}");
                // }

                // DEBUG: Make sure we are AFTER the line end?

                return;
            }
        }
    }
}
