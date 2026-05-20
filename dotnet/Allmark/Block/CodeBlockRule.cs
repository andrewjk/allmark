namespace Allmark.Block;

using Allmark.Types;

public static class CodeBlockRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "code_block",
            TestStart = TestStart,
            TestContinue = TestContinue,
        };
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent)
    {
        if (parent.AcceptsContent)
        {
            return false;
        }

        // "An indented code block cannot interrupt a paragraph, so there must be a
        // blank line between a paragraph and a following indented code block"
        if (parent.Type == "paragraph" && !parent.BlankAfter)
        {
            return false;
        }

        if (state.Indent >= 4 && state.I < state.Src.Length && !Utils.IsNewLine(state.Src[state.I]))
        {
            var codeIndent = state.Indent - 4;

            var code = Utils.NewBlock("code_block", state.I - state.Indent, state.Line, "    ", codeIndent);
            code.AcceptsContent = true;
            code.Content += new string(' ', codeIndent);

            if (state.HasBlankLine && parent.Children!.Count > 0)
            {
                parent.Children[^1].BlankAfter = true;
                state.HasBlankLine = false;
            }

            parent.Children!.Add(code);
            state.OpenNodes.Push(code);

            state.Indent = 0;
            state.HasBlankLine = false;
            Parse.ParseBlock.Execute(state, code);

            return true;
        }

        return false;
    }

    private static bool TestContinue(BlockParserState state, MarkdownNode node)
    {
        if (state.HasBlankLine && state.Indent >= 4)
        {
            // "Any initial spaces beyond four will be included in the content,
            // even in interior blank lines"
            node.Content += new string(' ', state.Indent - 4);
        }

        if (state.Indent >= 4)
        {
            state.Indent -= 4;
            return true;
        }

        if (state.HasBlankLine)
        {
            return true;
        }

        return false;
    }
}
