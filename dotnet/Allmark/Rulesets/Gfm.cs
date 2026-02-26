namespace Allmark.Rulesets;

using Allmark.Block;
using Allmark.Inline;
using Allmark.Render;
using Allmark.Types;

/// <summary>
/// The rules from [CommonMark](https://spec.commonmark.org/) plus [GitHub
/// Flavored Markdown](https://github.github.com/gfm/) rules.
/// </summary>
public static class Gfm
{
    public static RuleSet RuleSet => new RuleSet
    {
        Blocks = new Dictionary<string, BlockRule>
        {
            [IndentRule.Create().Name] = IndentRule.Create(),
            [HeadingRule.Create().Name] = HeadingRule.Create(),
            [HeadingUnderlineRule.Create().Name] = HeadingUnderlineRule.Create(),
            [ThematicBreakRule.Create().Name] = ThematicBreakRule.Create(),
            [AlertRule.Create().Name] = AlertRule.Create(),
            [BlockQuoteRule.Create().Name] = BlockQuoteRule.Create(),
            [ListOrderedRule.Create().Name] = ListOrderedRule.Create(),
            [ListBulletedRule.Create().Name] = ListBulletedRule.Create(),
            [ListItemRule.Create().Name] = ListItemRule.Create(),
            [ListTaskItemRule.Create().Name] = ListTaskItemRule.Create(),
            [FootnoteReferenceRule.Create().Name] = FootnoteReferenceRule.Create(),
            [CodeBlockRule.Create().Name] = CodeBlockRule.Create(),
            [CodeFenceRule.Create().Name] = CodeFenceRule.Create(),
            [HtmlBlockRule.Create().Name] = HtmlBlockRule.Create(),
            [LinkReferenceRule.Create().Name] = LinkReferenceRule.Create(),
            [TableRule.Create().Name] = TableRule.Create(),
            [ParagraphRule.Create().Name] = ParagraphRule.Create(),
            [ContentRule.Create().Name] = ContentRule.Create(),
        },
        Inlines = new Dictionary<string, InlineRule>
        {
            [AutolinkRule.Create().Name] = AutolinkRule.Create(),
            [ExtendedAutolinkRule.Create().Name] = ExtendedAutolinkRule.Create(),
            [HtmlSpanRule.Create().Name] = HtmlSpanRule.Create(),
            [CodeSpanRule.Create().Name] = CodeSpanRule.Create(),
            [EmphasisRule.Create().Name] = EmphasisRule.Create(),
            [StrikethroughRule.Create().Name] = StrikethroughRule.Create(),
            [FootnoteRule.Create().Name] = FootnoteRule.Create(),
            [LinkRule.Create().Name] = LinkRule.Create(),
            [HardBreakRule.Create().Name] = HardBreakRule.Create(),
            [LineBreakRule.Create().Name] = LineBreakRule.Create(),
            [TextRule.Create().Name] = TextRule.Create(),
        },
    };
}
