using Allmark.Rulesets;
using Allmark.Types;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Allmark.Tests;

[TestClass]
public class SourceMappingTests
{
    [TestMethod]
    public void HeadingATX()
    {
        var input = "# Heading 1";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var heading = doc.Children![0];
        Assert.AreEqual("heading", heading.Type);
        Assert.AreEqual(0, heading.Index);
        Assert.AreEqual(11, heading.Length);
    }

    [TestMethod]
    public void HeadingATXWithMultipleHashes()
    {
        var input = "### Heading 3";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var heading = doc.Children![0];
        Assert.AreEqual("heading", heading.Type);
        Assert.AreEqual(0, heading.Index);
        Assert.AreEqual(13, heading.Length);
    }

    [TestMethod]
    public void HeadingWithEmphasis()
    {
        var input = "# Heading *bold* 1";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var heading = doc.Children![0];
        Assert.AreEqual("heading", heading.Type);
        Assert.AreEqual(0, heading.Index);
        Assert.AreEqual(18, heading.Length);
        var emphasis = heading.Children![0].Children![1];
        Assert.AreEqual("emphasis", emphasis.Type);
        Assert.AreEqual(10, emphasis.Index);
        Assert.AreEqual(6, emphasis.Length);
    }

    [TestMethod]
    public void HeadingUnderline()
    {
        var input = "Heading\n=====";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var heading = doc.Children![0];
        Assert.AreEqual("heading_underline", heading.Type);
        Assert.AreEqual(0, heading.Index);
        Assert.AreEqual(13, heading.Length);
    }

    [TestMethod]
    public void ThematicBreak()
    {
        var input = "---";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var thematicBreak = doc.Children![0];
        Assert.AreEqual("thematic_break", thematicBreak.Type);
        Assert.AreEqual(0, thematicBreak.Index);
        Assert.AreEqual(3, thematicBreak.Length);
    }

    [TestMethod]
    public void Alert()
    {
        var input = "> [!NOTE]\n> Alert content";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var alert = doc.Children![0];
        Assert.AreEqual(0, alert.Index);
        Assert.AreEqual(25, alert.Length);
    }

    [TestMethod]
    public void BlockQuote()
    {
        var input = "> Quote content";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var blockQuote = doc.Children![0];
        Assert.AreEqual("block_quote", blockQuote.Type);
        Assert.AreEqual(0, blockQuote.Index);
        Assert.AreEqual(15, blockQuote.Length);
    }

    [TestMethod]
    public void BlockQuoteWithEmphasis()
    {
        var input = "> Quote *content*";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var blockQuote = doc.Children![0];
        Assert.AreEqual("block_quote", blockQuote.Type);
        Assert.AreEqual(0, blockQuote.Index);
        Assert.AreEqual(17, blockQuote.Length);
        var emphasis = blockQuote.Children![0].Children![1];
        Assert.AreEqual("emphasis", emphasis.Type);
        Assert.AreEqual(8, emphasis.Index);
        Assert.AreEqual(9, emphasis.Length);
    }

    [TestMethod]
    public void CodeBlockIndented()
    {
        var input = "\n    code\n    here";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var codeBlock = doc.Children![0];
        Assert.AreEqual("code_block", codeBlock.Type);
        Assert.AreEqual(1, codeBlock.Index);
        Assert.AreEqual(17, codeBlock.Length);
    }

    [TestMethod]
    public void CodeFenceBackticks()
    {
        var input = "```\ncode\n```";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var codeFence = doc.Children![0];
        Assert.AreEqual("code_fence", codeFence.Type);
        Assert.AreEqual(0, codeFence.Index);
        Assert.AreEqual(12, codeFence.Length);
    }

    [TestMethod]
    public void CodeFenceTildes()
    {
        var input = "~~~\ncode\n~~~";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var codeFence = doc.Children![0];
        Assert.AreEqual("code_fence", codeFence.Type);
        Assert.AreEqual(0, codeFence.Index);
        Assert.AreEqual(12, codeFence.Length);
    }

    [TestMethod]
    public void CodeFenceWithLanguage()
    {
        var input = "```javascript\ncode\n```";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var codeFence = doc.Children![0];
        Assert.AreEqual("code_fence", codeFence.Type);
        Assert.AreEqual(0, codeFence.Index);
        Assert.AreEqual(22, codeFence.Length);
    }

    [TestMethod]
    public void HtmlBlock()
    {
        var input = "<div>content</div>";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var htmlBlock = doc.Children![0];
        Assert.AreEqual("html_block", htmlBlock.Type);
        Assert.AreEqual(0, htmlBlock.Index);
        Assert.AreEqual(18, htmlBlock.Length);
    }

    [TestMethod]
    public void HtmlBlockMultiline()
    {
        var input = "<div>\ncontent\n</div>";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var htmlBlock = doc.Children![0];
        Assert.AreEqual("html_block", htmlBlock.Type);
        Assert.AreEqual(0, htmlBlock.Index);
        Assert.AreEqual(20, htmlBlock.Length);
    }

    [TestMethod]
    public void LinkReferenceDefinition()
    {
        var input = "[link]: url";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var linkReference = doc.Children![0];
        Assert.AreEqual("link_ref", linkReference.Type);
        Assert.AreEqual(0, linkReference.Index);
        Assert.AreEqual(11, linkReference.Length);
    }

    [TestMethod]
    public void ListOrdered()
    {
        var input = "1. Item one";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var list = doc.Children![0];
        Assert.AreEqual("list_ordered", list.Type);
        Assert.AreEqual(0, list.Index);
        Assert.AreEqual(11, list.Length);
    }

    [TestMethod]
    public void ListBulleted()
    {
        var input = "- Item one";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var list = doc.Children![0];
        Assert.AreEqual("list_bulleted", list.Type);
        Assert.AreEqual(0, list.Index);
        Assert.AreEqual(10, list.Length);
    }

    [TestMethod]
    public void ListItem()
    {
        var input = "1. Item one";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var list = doc.Children![0];
        var listItem = list.Children![0];
        Assert.AreEqual("list_item", listItem.Type);
        Assert.AreEqual(0, listItem.Index);
        Assert.AreEqual(11, listItem.Length);
    }

    [TestMethod]
    public void ListItemWithEmphasis()
    {
        var input = "1. Item *one*";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var list = doc.Children![0];
        var listItem = list.Children![0];
        Assert.AreEqual("list_item", listItem.Type);
        Assert.AreEqual(0, listItem.Index);
        Assert.AreEqual(13, listItem.Length);
        var emphasis = listItem.Children![0].Children![1];
        Assert.AreEqual("emphasis", emphasis.Type);
        Assert.AreEqual(8, emphasis.Index);
        Assert.AreEqual(5, emphasis.Length);
    }

    [TestMethod]
    public void ListTaskItemChecked()
    {
        var input = "- [x] Done task";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var list = doc.Children![0];
        var taskItem = list.Children![0];
        Assert.AreEqual("list_item", taskItem.Type);
        Assert.AreEqual(0, taskItem.Index);
        Assert.AreEqual(15, taskItem.Length);
    }

    [TestMethod]
    public void ListTaskItemUnchecked()
    {
        var input = "- [ ] Todo task";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var list = doc.Children![0];
        var taskItem = list.Children![0];
        Assert.AreEqual("list_item", taskItem.Type);
        Assert.AreEqual(0, taskItem.Index);
        Assert.AreEqual(15, taskItem.Length);
    }

    [TestMethod]
    public void ListTaskItemWithEmphasis()
    {
        var input = "- [ ] very *quick* task";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var list = doc.Children![0];

        var listItem = list.Children![0];
        Assert.AreEqual("list_item", listItem.Type);
        Assert.AreEqual(0, listItem.Index);
        Assert.AreEqual(23, listItem.Length);

        var taskItem = listItem.Children![0];
        Assert.AreEqual("list_task_item", taskItem.Type);
        Assert.AreEqual(2, taskItem.Index);
        Assert.AreEqual(3, taskItem.Length);

        var emphasis = listItem.Children![1].Children![1];
        Assert.AreEqual("emphasis", emphasis.Type);
        Assert.AreEqual(11, emphasis.Index);
        Assert.AreEqual(7, emphasis.Length);
    }

    [TestMethod]
    public void LinkWithEmphasis()
    {
        var input = "# Test\n\n[link *text*](url)";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];

        var link = paragraph.Children![0];
        Assert.AreEqual("link", link.Type);
        Assert.AreEqual(8, link.Index);
        Assert.AreEqual(18, link.Length);

        var emphasis = link.Children![1];
        Assert.AreEqual("emphasis", emphasis.Type);
        Assert.AreEqual(14, emphasis.Index);
        Assert.AreEqual(6, emphasis.Length);
    }

    [TestMethod]
    public void FootnoteReference()
    {
        var input = "[^1]: Footnote content";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var footnoteReference = doc.Children![0];
        Assert.AreEqual("footnote_ref", footnoteReference.Type);
        Assert.AreEqual(0, footnoteReference.Index);
        Assert.AreEqual(22, footnoteReference.Length);
    }

    [TestMethod]
    public void Table()
    {
        var input = "| A | B |\n|---|---|\n| 1 | 2 |";
        var doc = Parser.Execute(input, Extended.RuleSet);

        var table = doc.Children![0];
        Assert.AreEqual("table", table.Type);
        Assert.AreEqual(0, table.Index);
        Assert.AreEqual(29, table.Length);

        var header = table.Children![0];
        Assert.AreEqual("table_header", header.Type);
        Assert.AreEqual(0, header.Index);
        Assert.AreEqual(9, header.Length);

        var hc1 = header.Children![0];
        Assert.AreEqual("table_cell", hc1.Type);
        Assert.AreEqual(0, hc1.Index);
        Assert.AreEqual(5, hc1.Length);

        var hc2 = header.Children![1];
        Assert.AreEqual("table_cell", hc2.Type);
        Assert.AreEqual(4, hc2.Index);
        Assert.AreEqual(5, hc2.Length);

        var row = table.Children![1];
        Assert.AreEqual("table_row", row.Type);
        Assert.AreEqual(20, row.Index);
        Assert.AreEqual(9, row.Length);

        var rc1 = row.Children![0];
        Assert.AreEqual("table_cell", rc1.Type);
        Assert.AreEqual(20, rc1.Index);
        Assert.AreEqual(5, rc1.Length);

        var rc2 = row.Children![1];
        Assert.AreEqual("table_cell", rc2.Type);
        Assert.AreEqual(24, rc2.Index);
        Assert.AreEqual(5, rc2.Length);
    }

    [TestMethod]
    public void TableWithEmphasis()
    {
        var input = "| A | B |\n|---|---|\n| item *one* | 2 |";
        var doc = Parser.Execute(input, Extended.RuleSet);

        var table = doc.Children![0];
        Assert.AreEqual("table", table.Type);
        Assert.AreEqual(0, table.Index);
        Assert.AreEqual(38, table.Length);

        var row = table.Children![1];
        Assert.AreEqual("table_row", row.Type);

        var cell = row.Children![0];
        Assert.AreEqual("table_cell", cell.Type);

        var emphasis = cell.Children![0].Children![1];
        Assert.AreEqual("emphasis", emphasis.Type);
        Assert.AreEqual(27, emphasis.Index);
        Assert.AreEqual(5, emphasis.Length);
    }

    [TestMethod]
    public void Paragraph()
    {
        var input = "A paragraph.";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![0];
        Assert.AreEqual("paragraph", paragraph.Type);
        Assert.AreEqual(0, paragraph.Index);
        Assert.AreEqual(12, paragraph.Length);
    }

    [TestMethod]
    public void Indent()
    {
        var input = "  indented paragraph";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var indent = doc.Children![0];
        Assert.AreEqual("paragraph", indent.Type);
        Assert.AreEqual(2, indent.Index);
        Assert.AreEqual(18, indent.Length);
    }

    [TestMethod]
    public void EscapedBlock()
    {
        var input = "\\# Not a heading";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var escaped = doc.Children![0];
        Assert.AreEqual("paragraph", escaped.Type);
        Assert.AreEqual(0, escaped.Index);
        Assert.AreEqual(16, escaped.Length);
    }

    // Inline tests
    [TestMethod]
    public void AutolinkURL()
    {
        var input = "# Test\n\n<https://example.com>";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var autolink = paragraph.Children![0];
        Assert.AreEqual("link", autolink.Type);
        Assert.AreEqual(8, autolink.Index);
        Assert.AreEqual(21, autolink.Length);
    }

    [TestMethod]
    public void AutolinkEmail()
    {
        var input = "# Test\n\n<user@example.com>";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var autolink = paragraph.Children![0];
        Assert.AreEqual("link", autolink.Type);
        Assert.AreEqual(8, autolink.Index);
        Assert.AreEqual(18, autolink.Length);
    }

    [TestMethod]
    public void ExtendedAutolinkWww()
    {
        var input = "# Test\n\nwww.example.com";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var extendedAutolink = paragraph.Children![0];
        Assert.AreEqual("link", extendedAutolink.Type);
        Assert.AreEqual(8, extendedAutolink.Index);
        Assert.AreEqual(15, extendedAutolink.Length);
    }

    [TestMethod]
    public void CodeSpan()
    {
        var input = "# Test\n\n`code`";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var codeSpan = paragraph.Children![0];
        Assert.AreEqual("code_span", codeSpan.Type);
        Assert.AreEqual(8, codeSpan.Index);
        Assert.AreEqual(6, codeSpan.Length);
    }

    [TestMethod]
    public void EmphasisAsterisk()
    {
        var input = "# Test\n\n*emphasis*";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var emphasis = paragraph.Children![0];
        Assert.AreEqual("emphasis", emphasis.Type);
        Assert.AreEqual(8, emphasis.Index);
        Assert.AreEqual(10, emphasis.Length);
    }

    [TestMethod]
    public void EmphasisUnderscore()
    {
        var input = "# Test\n\nhere: _emphasis_";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var emphasis = paragraph.Children![1];
        Assert.AreEqual("emphasis", emphasis.Type);
        Assert.AreEqual(14, emphasis.Index);
        Assert.AreEqual(10, emphasis.Length);
    }

    [TestMethod]
    public void Strong()
    {
        var input = "# Test\n\n**strong**";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var strong = paragraph.Children![0];
        Assert.AreEqual("strong", strong.Type);
        Assert.AreEqual(8, strong.Index);
        Assert.AreEqual(10, strong.Length);
    }

    [TestMethod]
    public void Link()
    {
        var input = "# Test\n\n[link](url)";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var link = paragraph.Children![0];
        Assert.AreEqual("link", link.Type);
        Assert.AreEqual(8, link.Index);
        Assert.AreEqual(11, link.Length);
    }

    [TestMethod]
    public void LinkWithTitle()
    {
        var input = "# Test\n\n[link](url \"title\")";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var link = paragraph.Children![0];
        Assert.AreEqual("link", link.Type);
        Assert.AreEqual(8, link.Index);
        Assert.AreEqual(19, link.Length);
    }

    [TestMethod]
    public void Footnote()
    {
        var input = "# Test\n\n[^1]";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var footnote = paragraph.Children![0];
        Assert.AreEqual(8, footnote.Index);
        Assert.AreEqual(4, footnote.Length);
    }

    [TestMethod]
    public void HardBreak()
    {
        var input = "# Test\n\nline  \nbreak";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var hardBreak = paragraph.Children![1];
        Assert.AreEqual(12, hardBreak.Index);
        Assert.AreEqual(2, hardBreak.Length);
    }

    [TestMethod]
    public void Strikethrough()
    {
        var input = "# Test\n\n~~strikethrough~~";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var strikethrough = paragraph.Children![0];
        Assert.AreEqual("strikethrough", strikethrough.Type);
        Assert.AreEqual(8, strikethrough.Index);
        Assert.AreEqual(17, strikethrough.Length);
    }

    [TestMethod]
    public void Highlight()
    {
        var input = "# Test\n\n==highlight==";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var highlight = paragraph.Children![0];
        Assert.AreEqual("highlight", highlight.Type);
        Assert.AreEqual(8, highlight.Index);
        Assert.AreEqual(13, highlight.Length);
    }

    [TestMethod]
    public void Subscript()
    {
        var input = "# Test\n\n~subscript~";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var subscript = paragraph.Children![0];
        Assert.AreEqual("subscript", subscript.Type);
        Assert.AreEqual(8, subscript.Index);
        Assert.AreEqual(11, subscript.Length);
    }

    [TestMethod]
    public void Superscript()
    {
        var input = "# Test\n\n^superscript^";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var superscript = paragraph.Children![0];
        Assert.AreEqual("superscript", superscript.Type);
        Assert.AreEqual(8, superscript.Index);
        Assert.AreEqual(13, superscript.Length);
    }

    [TestMethod]
    public void Insertion()
    {
        var input = "# Test\n\n{++inserted++}";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var insertion = paragraph.Children![0];
        Assert.AreEqual("insertion", insertion.Type);
        Assert.AreEqual(8, insertion.Index);
        Assert.AreEqual(14, insertion.Length);
    }

    [TestMethod]
    public void Deletion()
    {
        var input = "# Test\n\ndel: {--deleted--}";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var deletion = paragraph.Children![1];
        Assert.AreEqual("deletion", deletion.Type);
        Assert.AreEqual(13, deletion.Index);
        Assert.AreEqual(13, deletion.Length);
    }

    [TestMethod]
    public void HtmlSpan()
    {
        var input = "# Test\n\n<span>content</span>";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var htmlStart = paragraph.Children![0];
        var htmlEnd = paragraph.Children![2];
        Assert.AreEqual("html_span", htmlStart.Type);
        Assert.AreEqual(8, htmlStart.Index);
        Assert.AreEqual(6, htmlStart.Length);
        Assert.AreEqual("html_span", htmlEnd.Type);
        Assert.AreEqual(21, htmlEnd.Index);
        Assert.AreEqual(7, htmlEnd.Length);
    }

    [TestMethod]
    public void Comment()
    {
        var input = "# Test\n\n<!-- comment -->";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var comment = doc.Children![1];
        Assert.AreEqual(8, comment.Index);
        Assert.AreEqual(16, comment.Length);
    }

    [TestMethod]
    public void Text()
    {
        var input = "# Test\n\nplain text";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var text = paragraph.Children![0];
        Assert.AreEqual("text", text.Type);
        Assert.AreEqual(8, text.Index);
        Assert.AreEqual(10, text.Length);
    }

    [TestMethod]
    public void TextWithSpecialChars()
    {
        var input = "# Test\n\ntext with & chars";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![1];
        var text = paragraph.Children![0];
        Assert.AreEqual("text", text.Type);
        Assert.AreEqual(8, text.Index);
        Assert.AreEqual(17, text.Length);
    }

    [TestMethod]
    public void VariousFormattings()
    {
        var input = "# Heading 1\n\nSome **bold** text, I'm ~~deleted~~, really {+gone+}";
        var doc = Parser.Execute(input, Extended.RuleSet);

        var heading = doc.Children![0];
        Assert.AreEqual("heading", heading.Type);
        Assert.AreEqual(0, heading.Index);
        Assert.AreEqual(11, heading.Length);

        var paragraph = doc.Children![1];
        Assert.AreEqual("paragraph", paragraph.Type);
        Assert.AreEqual(13, paragraph.Index);
        Assert.AreEqual(52, paragraph.Length);

        var strong = paragraph.Children![1];
        Assert.AreEqual("strong", strong.Type);
        Assert.AreEqual(18, strong.Index);
        Assert.AreEqual(8, strong.Length);

        var strikethrough = paragraph.Children![3];
        Assert.AreEqual("strikethrough", strikethrough.Type);
        Assert.AreEqual(37, strikethrough.Index);
        Assert.AreEqual(11, strikethrough.Length);

        var deletion = paragraph.Children![5];
        Assert.AreEqual("insertion", deletion.Type);
        Assert.AreEqual(57, deletion.Index);
        Assert.AreEqual(8, deletion.Length);
    }

    [TestMethod]
    public void ParagraphWithSpacesAfterNewline()
    {
        var input = "Test\n  text with `spaces`";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![0];
        Assert.AreEqual(3, paragraph.Children!.Count);
        var text = paragraph.Children[0];
        Assert.AreEqual("text", text.Type);
        Assert.AreEqual(0, text.Index);
        Assert.AreEqual(5, text.Length);
        var text2 = paragraph.Children[1];
        Assert.AreEqual("text", text2.Type);
        Assert.AreEqual(7, text2.Index);
        Assert.AreEqual(10, text2.Length);
        var code = paragraph.Children[2];
        Assert.AreEqual("code_span", code.Type);
        Assert.AreEqual(17, code.Index);
        Assert.AreEqual(8, code.Length);
    }

    [TestMethod]
    public void ParagraphWithSpacesAfterHardBreak()
    {
        var input = "Test  \n  text with `spaces`";
        var doc = Parser.Execute(input, Extended.RuleSet);
        var paragraph = doc.Children![0];
        Assert.AreEqual(4, paragraph.Children!.Count);
        var text = paragraph.Children[0];
        Assert.AreEqual("text", text.Type);
        Assert.AreEqual(0, text.Index);
        Assert.AreEqual(4, text.Length);
        var br = paragraph.Children[1];
        Assert.AreEqual("hard_break", br.Type);
        Assert.AreEqual(4, br.Index);
        Assert.AreEqual(2, br.Length);
        var text2 = paragraph.Children[2];
        Assert.AreEqual("text", text2.Type);
        Assert.AreEqual(9, text2.Index);
        Assert.AreEqual(10, text2.Length);
        var code = paragraph.Children[3];
        Assert.AreEqual("code_span", code.Type);
        Assert.AreEqual(19, code.Index);
        Assert.AreEqual(8, code.Length);
    }
}
