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
			[SubscriptRule.Create().Name] = SubscriptRule.Create(),
			[SuperscriptRule.Create().Name] = SuperscriptRule.Create(),
			[StrikethroughRule.Create().Name] = StrikethroughRule.Create(),
			[HighlightRule.Create().Name] = HighlightRule.Create(),
			[FootnoteRule.Create().Name] = FootnoteRule.Create(),
			[LinkRule.Create().Name] = LinkRule.Create(),
			[HardBreakRule.Create().Name] = HardBreakRule.Create(),
			[LineBreakRule.Create().Name] = LineBreakRule.Create(),
			[InsertionRule.Create().Name] = InsertionRule.Create(),
			[DeletionRule.Create().Name] = DeletionRule.Create(),
			[TextRule.Create().Name] = TextRule.Create(),
		},
		Renderers = new Dictionary<string, Renderer>
		{
			[AlertRenderer.Create().Name] = AlertRenderer.Create(),
			[BlockQuoteRenderer.Create().Name] = BlockQuoteRenderer.Create(),
			[CodeBlockRenderer.Create().Name] = CodeBlockRenderer.Create(),
			[CodeFenceRenderer.Create().Name] = CodeFenceRenderer.Create(),
			[CodeSpanRenderer.Create().Name] = CodeSpanRenderer.Create(),
			[DeletionRenderer.Create().Name] = DeletionRenderer.Create(),
			[EmphasisRenderer.Create().Name] = EmphasisRenderer.Create(),
			[FootnoteRenderer.Create().Name] = FootnoteRenderer.Create(),
			[HardBreakRenderer.Create().Name] = HardBreakRenderer.Create(),
			[HeadingRenderer.Create().Name] = HeadingRenderer.Create(),
			[HeadingUnderlineRenderer.Create().Name] = HeadingUnderlineRenderer.Create(),
			[HighlightRenderer.Create().Name] = HighlightRenderer.Create(),
			[HtmlBlockRenderer.Create().Name] = HtmlBlockRenderer.Create(),
			[HtmlSpanRenderer.Create().Name] = HtmlSpanRenderer.Create(),
			[ImageRenderer.Create().Name] = ImageRenderer.Create(),
			[InsertionRenderer.Create().Name] = InsertionRenderer.Create(),
			[LinkRenderer.Create().Name] = LinkRenderer.Create(),
			[ListBulletedRenderer.Create().Name] = ListBulletedRenderer.Create(),
			[ListOrderedRenderer.Create().Name] = ListOrderedRenderer.Create(),
			[ListTaskItemRenderer.Create().Name] = ListTaskItemRenderer.Create(),
			[ParagraphRenderer.Create().Name] = ParagraphRenderer.Create(),
			[StrikethroughRenderer.Create().Name] = StrikethroughRenderer.Create(),
			[StrongRenderer.Create().Name] = StrongRenderer.Create(),
			[SubscriptRenderer.Create().Name] = SubscriptRenderer.Create(),
			[SuperscriptRenderer.Create().Name] = SuperscriptRenderer.Create(),
			[TableRenderer.Create().Name] = TableRenderer.Create(),
			[TableCellRenderer.Create().Name] = TableCellRenderer.Create(),
			[TableHeaderRenderer.Create().Name] = TableHeaderRenderer.Create(),
			[TableRowRenderer.Create().Name] = TableRowRenderer.Create(),
			[TextRenderer.Create().Name] = TextRenderer.Create(),
			[ThematicBreakRenderer.Create().Name] = ThematicBreakRenderer.Create(),
		},
	};
}
