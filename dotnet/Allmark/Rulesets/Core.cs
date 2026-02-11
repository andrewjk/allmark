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
		Blocks = new Dictionary<string, BlockRule>
		{
			[IndentRule.Create().Name] = IndentRule.Create(),
			[HeadingRule.Create().Name] = HeadingRule.Create(),
			[HeadingUnderlineRule.Create().Name] = HeadingUnderlineRule.Create(),
			[ThematicBreakRule.Create().Name] = ThematicBreakRule.Create(),
			[BlockQuoteRule.Create().Name] = BlockQuoteRule.Create(),
			[ListOrderedRule.Create().Name] = ListOrderedRule.Create(),
			[ListBulletedRule.Create().Name] = ListBulletedRule.Create(),
			[ListItemRule.Create().Name] = ListItemRule.Create(),
			[CodeBlockRule.Create().Name] = CodeBlockRule.Create(),
			[CodeFenceRule.Create().Name] = CodeFenceRule.Create(),
			[HtmlBlockRule.Create().Name] = HtmlBlockRule.Create(),
			[LinkReferenceRule.Create().Name] = LinkReferenceRule.Create(),
			[ParagraphRule.Create().Name] = ParagraphRule.Create(),
			[ContentRule.Create().Name] = ContentRule.Create(),
		},
		Inlines = new Dictionary<string, InlineRule>
		{
			[AutolinkRule.Create().Name] = AutolinkRule.Create(),
			[HtmlSpanRule.Create().Name] = HtmlSpanRule.Create(),
			[CodeSpanRule.Create().Name] = CodeSpanRule.Create(),
			[EmphasisRule.Create().Name] = EmphasisRule.Create(),
			[LinkRule.Create().Name] = LinkRule.Create(),
			[HardBreakRule.Create().Name] = HardBreakRule.Create(),
			[LineBreakRule.Create().Name] = LineBreakRule.Create(),
			[TextRule.Create().Name] = TextRule.Create(),
		},
		Renderers = new Dictionary<string, Renderer>
		{
			[BlockQuoteRenderer.Create().Name] = BlockQuoteRenderer.Create(),
			[CodeBlockRenderer.Create().Name] = CodeBlockRenderer.Create(),
			[CodeFenceRenderer.Create().Name] = CodeFenceRenderer.Create(),
			[CodeSpanRenderer.Create().Name] = CodeSpanRenderer.Create(),
			[EmphasisRenderer.Create().Name] = EmphasisRenderer.Create(),
			[HardBreakRenderer.Create().Name] = HardBreakRenderer.Create(),
			[HeadingRenderer.Create().Name] = HeadingRenderer.Create(),
			[HeadingUnderlineRenderer.Create().Name] = HeadingUnderlineRenderer.Create(),
			[HtmlBlockRenderer.Create().Name] = HtmlBlockRenderer.Create(),
			[HtmlSpanRenderer.Create().Name] = HtmlSpanRenderer.Create(),
			[ImageRenderer.Create().Name] = ImageRenderer.Create(),
			[LinkRenderer.Create().Name] = LinkRenderer.Create(),
			[ListBulletedRenderer.Create().Name] = ListBulletedRenderer.Create(),
			[ListOrderedRenderer.Create().Name] = ListOrderedRenderer.Create(),
			[ParagraphRenderer.Create().Name] = ParagraphRenderer.Create(),
			[StrongRenderer.Create().Name] = StrongRenderer.Create(),
			[TextRenderer.Create().Name] = TextRenderer.Create(),
			[ThematicBreakRenderer.Create().Name] = ThematicBreakRenderer.Create(),
		},
	};
}
