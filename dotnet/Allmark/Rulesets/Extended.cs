namespace Allmark.Rulesets;

using Allmark.Block;
using Allmark.Inline;
using Allmark.Render;
using Allmark.Types;

/// <summary>
/// The rules from [CommonMark](https://spec.commonmark.org/) and [GitHub
/// Flavored Markdown](https://github.github.com/gfm/) plus some extended rules
/// from various note taking apps.
/// </summary>
public static class Extended
{
    public static RuleSet RuleSet => new RuleSet
    {
        Blocks = new BlockRule[]
        {
            IndentRule.Create(),
            HeadingRule.Create(),
            HeadingUnderlineRule.Create(),
            ThematicBreakRule.Create(),
            AlertRule.Create(),
            BlockQuoteRule.Create(),
            ListOrderedRule.Create(),
            ListBulletedRule.Create(),
            ListItemRule.Create(),
            ListTaskItemRule.Create(),
            FootnoteReferenceRule.Create(),
            CodeBlockRule.Create(),
            CodeFenceRule.Create(),
            HtmlBlockRule.Create(),
            LinkReferenceRule.Create(),
            TableRule.Create(),
            ParagraphRule.Create(),
            ContentRule.Create(),
        },
        Inlines = new InlineRule[]
        {
            AutolinkRule.Create(),
            ExtendedAutolinkRule.Create(),
            HtmlSpanRule.Create(),
            CodeSpanRule.Create(),
            EmphasisRule.Create(),
            SubscriptRule.Create(),
            SuperscriptRule.Create(),
            StrikethroughRule.Create(),
            HighlightRule.Create(),
            FootnoteRule.Create(),
            LinkRule.Create(),
            HardBreakRule.Create(),
            InsertionRule.Create(),
            DeletionRule.Create(),
            CommentRule.Create(),
            TextRule.Create(),
        },
    };
}
