namespace Allmark;

using Allmark.Parse;
using Allmark.Types;

public static class Parser
{
    public static MarkdownNode Execute(string src, RuleSet rules)
    {
        var document = Utils.NewBlock("document", 0, 1, "", 0);

        // Skip empty lines at start
        var start = 0;
        var i = 0;
        var index = 0;
        for (; i < src.Length; i++)
        {
            if (!Utils.IsSpace(src[i]))
            {
                break;
            }
            else if (Utils.IsNewLine(src[i]))
            {
                start = i + 1;
            }
        }
        index = i;

        // Process frontmatter if found
        string? frontmatter = null;
        if (i < src.Length && src[i] == '-')
        {
            frontmatter = Utils.ExtractFrontMatter(document, src, index);
            if (frontmatter != null)
            {
                start = index + frontmatter.Length;
            }
        }

        var rulesMap = rules.Blocks.ToDictionary(r => r.Name);

        var state = new BlockParserState
        {
            Rules = rules.Blocks,
            RulesMap = rulesMap,
            Src = src,
            I = start,
            Line = 0,
            LineStart = 0,
            Indent = 0,
            Spaces = "",
            IsEscaped = false,
            MaybeContinue = false,
            HasBlankLine = false,
            OpenNodes = new Stack<MarkdownNode>(new[] { document }),
            Refs = new Dictionary<string, LinkReference>(),
            Footnotes = new Dictionary<string, FootnoteReference>(),
        };

        // Stage 1 -- parse each line into blocks
        while (state.I < state.Src.Length)
        {
            ParseLine.Execute(state);
        }

        // Close remaining open nodes
        for (int j = state.OpenNodes.Count - 1; j >= 0; j--)
        {
            var openNode = state.OpenNodes.ElementAt(j);
            openNode.Length = state.I - openNode.Index;
            if (state.RulesMap.TryGetValue(openNode.Type, out var rule))
            {
                rule.CloseNode?.Invoke(state, openNode);
            }
        }

        // Stage 2 -- parse inlines for each block
        ParseBlockInlines.Execute(document, rules.Inlines, state.Refs, state.Footnotes);

        if (frontmatter != null)
        {
            document.Info = frontmatter;
        }

        return document;
    }
}
