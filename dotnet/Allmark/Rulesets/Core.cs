namespace Allmark.Rulesets;

using Allmark.Block;
using Allmark.Inline;
using Allmark.Render;
using Allmark.Types;

/// <summary>
/// The core rules from [CommonMark](https://spec.commonmark.org/).
/// </summary>
public static class Core
{
    public static RuleSet RuleSet => new RuleSet
    {
        Blocks = new BlockRule[]
        {
            IndentRule.Create(),
            HeadingRule.Create(),
            HeadingUnderlineRule.Create(),
            ThematicBreakRule.Create(),
            BlockQuoteRule.Create(),
            ListOrderedRule.Create(),
            ListBulletedRule.Create(),
            ListItemRule.Create(),
            CodeBlockRule.Create(),
            CodeFenceRule.Create(),
            HtmlBlockRule.Create(),
            LinkReferenceRule.Create(),
            ParagraphRule.Create(),
            ContentRule.Create(),
        },
        Inlines = new InlineRule[]
        {
            AutolinkRule.Create(),
            HtmlSpanRule.Create(),
            CodeSpanRule.Create(),
            EmphasisRule.Create(),
            LinkRule.Create(),
            HardBreakRule.Create(),
            TextRule.Create(),
        },
    };
}
