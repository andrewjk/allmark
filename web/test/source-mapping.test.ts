import { describe, expect, test } from "vite-plus/test";

import parse from "../src/parse";
import extended from "../src/rulesets/extended";

describe("source mapping - block rules", () => {
	test("heading - ATX", () => {
		const input = "# Heading 1";
		const doc = parse(input, extended);
		const heading = doc.children![0];
		expect(heading.type).toBe("heading");
		expect(heading.index).toBe(0);
		expect(heading.length).toBe(11);
	});

	test("heading - ATX with multiple hashes", () => {
		const input = "### Heading 3";
		const doc = parse(input, extended);
		const heading = doc.children![0];
		expect(heading.type).toBe("heading");
		expect(heading.index).toBe(0);
		expect(heading.length).toBe(13);
	});

	test("heading - underline", () => {
		const input = "Heading\n=====";
		const doc = parse(input, extended);
		const heading = doc.children![0];
		expect(heading.type).toBe("heading_underline");
		expect(heading.index).toBe(0);
		expect(heading.length).toBe(13);
	});

	test("heading with emphasis", () => {
		const input = "# Heading *bold* 1";
		const doc = parse(input, extended);

		const heading = doc.children![0];
		expect(heading.type).toBe("heading");
		expect(heading.index).toBe(0);
		expect(heading.length).toBe(18);

		const emphasis = heading.children![0].children![1];
		expect(emphasis.type).toBe("emphasis");
		expect(emphasis.index).toBe(10);
		expect(emphasis.length).toBe(6);
	});

	test("thematic break", () => {
		const input = "---";
		const doc = parse(input, extended);
		const thematicBreak = doc.children![0];
		expect(thematicBreak.type).toBe("thematic_break");
		expect(thematicBreak.index).toBe(0);
		expect(thematicBreak.length).toBe(3);
	});

	test("alert", () => {
		const input = "> [!NOTE]\n> Alert content";
		const doc = parse(input, extended);
		const alert = doc.children![0];
		// TODO:
		expect(alert.index).toBe(0);
		expect(alert.length).toBe(25);
	});

	test("block quote", () => {
		const input = "> Quote content";
		const doc = parse(input, extended);
		const blockQuote = doc.children![0];
		expect(blockQuote.type).toBe("block_quote");
		expect(blockQuote.index).toBe(0);
		expect(blockQuote.length).toBe(15);
	});

	test("block quote with emphasis", () => {
		const input = "> Quote *content*";
		const doc = parse(input, extended);

		const blockQuote = doc.children![0];
		expect(blockQuote.type).toBe("block_quote");
		expect(blockQuote.index).toBe(0);
		expect(blockQuote.length).toBe(17);

		const emphasis = blockQuote.children![0].children![1];
		expect(emphasis.type).toBe("emphasis");
		expect(emphasis.index).toBe(8);
		expect(emphasis.length).toBe(9);
	});

	test("code block - indented", () => {
		const input = "\n    code\n    here";
		const doc = parse(input, extended);
		const codeBlock = doc.children![0];
		expect(codeBlock.type).toBe("code_block");
		expect(codeBlock.index).toBe(1);
		expect(codeBlock.length).toBe(17);
	});

	test("code fence - backticks", () => {
		const input = "```\ncode\n```";
		const doc = parse(input, extended);
		const codeFence = doc.children![0];
		expect(codeFence.type).toBe("code_fence");
		expect(codeFence.index).toBe(0);
		expect(codeFence.length).toBe(12);
	});

	test("code fence - tildes", () => {
		const input = "~~~\ncode\n~~~";
		const doc = parse(input, extended);
		const codeFence = doc.children![0];
		expect(codeFence.type).toBe("code_fence");
		expect(codeFence.index).toBe(0);
		expect(codeFence.length).toBe(12);
	});

	test("code fence with language", () => {
		const input = "```javascript\ncode\n```";
		const doc = parse(input, extended);
		const codeFence = doc.children![0];
		expect(codeFence.type).toBe("code_fence");
		expect(codeFence.index).toBe(0);
		expect(codeFence.length).toBe(22);
	});

	test("html block", () => {
		const input = "<div>content</div>";
		const doc = parse(input, extended);
		const htmlBlock = doc.children![0];
		expect(htmlBlock.type).toBe("html_block");
		expect(htmlBlock.index).toBe(0);
		expect(htmlBlock.length).toBe(18);
	});

	test("html block multiline", () => {
		const input = "<div>\ncontent\n</div>";
		const doc = parse(input, extended);
		const htmlBlock = doc.children![0];
		expect(htmlBlock.type).toBe("html_block");
		expect(htmlBlock.index).toBe(0);
		expect(htmlBlock.length).toBe(20);
	});

	test("link reference definition", () => {
		const input = "[link]: url";
		const doc = parse(input, extended);
		const linkReference = doc.children![0];
		expect(linkReference.type).toBe("link_ref");
		expect(linkReference.index).toBe(0);
		expect(linkReference.length).toBe(11);
	});

	test("list - ordered", () => {
		const input = "1. Item one";
		const doc = parse(input, extended);
		const list = doc.children![0];
		expect(list.type).toBe("list_ordered");
		expect(list.index).toBe(0);
		expect(list.length).toBe(11);
	});

	test("list - bulleted", () => {
		const input = "- Item one";
		const doc = parse(input, extended);
		const list = doc.children![0];
		expect(list.type).toBe("list_bulleted");
		expect(list.index).toBe(0);
		expect(list.length).toBe(10);
	});

	test("list item", () => {
		const input = "1. Item one";
		const doc = parse(input, extended);
		const list = doc.children![0];
		const listItem = list.children![0];
		expect(listItem.type).toBe("list_item");
		expect(listItem.index).toBe(0);
		expect(listItem.length).toBe(11);
	});

	test("list item with emphasis", () => {
		const input = "1. Item *one*";
		const doc = parse(input, extended);
		const list = doc.children![0];

		const listItem = list.children![0];
		expect(listItem.type).toBe("list_item");
		expect(listItem.index).toBe(0);
		expect(listItem.length).toBe(13);

		const emphasis = listItem.children![0].children![1];
		expect(emphasis.type).toBe("emphasis");
		expect(emphasis.index).toBe(8);
		expect(emphasis.length).toBe(5);
	});

	test("list task item - checked", () => {
		const input = "- [x] Done task";
		const doc = parse(input, extended);
		const list = doc.children![0];

		const listItem = list.children![0];
		expect(listItem.type).toBe("list_item");
		expect(listItem.index).toBe(0);
		expect(listItem.length).toBe(15);

		const taskItem = listItem.children![0];
		expect(taskItem.type).toBe("list_task_item");
		expect(taskItem.index).toBe(2);
		expect(taskItem.length).toBe(3);
	});

	test("list task item - unchecked", () => {
		const input = "- [ ] Todo task";
		const doc = parse(input, extended);
		const list = doc.children![0];

		const listItem = list.children![0];
		expect(listItem.type).toBe("list_item");
		expect(listItem.index).toBe(0);
		expect(listItem.length).toBe(15);

		const taskItem = listItem.children![0];
		expect(taskItem.type).toBe("list_task_item");
		expect(taskItem.index).toBe(2);
		expect(taskItem.length).toBe(3);
	});

	test("list task item with emphasis", () => {
		const input = "- [ ] very *quick* task";
		const doc = parse(input, extended);
		const list = doc.children![0];

		const listItem = list.children![0];
		expect(listItem.type).toBe("list_item");
		expect(listItem.index).toBe(0);
		expect(listItem.length).toBe(23);

		const taskItem = listItem.children![0];
		expect(taskItem.type).toBe("list_task_item");
		expect(taskItem.index).toBe(2);
		expect(taskItem.length).toBe(3);

		const emphasis = listItem.children![1].children![1];
		expect(emphasis.type).toBe("emphasis");
		expect(emphasis.index).toBe(11);
		expect(emphasis.length).toBe(7);
	});

	test("footnote reference", () => {
		const input = "[^1]: Footnote content";
		const doc = parse(input, extended);
		const footnoteReference = doc.children![0];
		expect(footnoteReference.type).toBe("footnote_ref");
		expect(footnoteReference.index).toBe(0);
		expect(footnoteReference.length).toBe(22);
	});

	test("table", () => {
		const input = "| A | B |\n|---|---|\n| 1 | 2 |";
		const doc = parse(input, extended);

		const table = doc.children![0];
		expect(table.type).toBe("table");
		expect(table.index).toBe(0);
		expect(table.length).toBe(29);

		const header = table.children![0];
		expect(header.type).toBe("table_header");
		expect(header.index).toBe(0);
		expect(header.length).toBe(9);

		const hc1 = header.children![0];
		expect(hc1.type).toBe("table_cell");
		expect(hc1.index).toBe(0);
		expect(hc1.length).toBe(5);

		const hc2 = header.children![1];
		expect(hc2.type).toBe("table_cell");
		expect(hc2.index).toBe(4);
		expect(hc2.length).toBe(5);

		const row = table.children![1];
		expect(row.type).toBe("table_row");
		expect(row.index).toBe(20);
		expect(row.length).toBe(9);

		const rc1 = row.children![0];
		expect(rc1.type).toBe("table_cell");
		expect(rc1.index).toBe(20);
		expect(rc1.length).toBe(5);

		const rc2 = row.children![1];
		expect(rc2.type).toBe("table_cell");
		expect(rc2.index).toBe(24);
		expect(rc2.length).toBe(5);
	});

	test("table with emphasis", () => {
		const input = "| A | B |\n|---|---|\n| item *one* | 2 |";
		const doc = parse(input, extended);

		const table = doc.children![0];
		expect(table.type).toBe("table");
		expect(table.index).toBe(0);
		expect(table.length).toBe(38);

		const row = table.children![1];
		expect(row.type).toBe("table_row");

		const cell = row.children![0];
		expect(cell.type).toBe("table_cell");

		const emphasis = cell.children![0].children![1];
		expect(emphasis.type).toBe("emphasis");
		expect(emphasis.index).toBe(27);
		expect(emphasis.length).toBe(5);
	});

	test("paragraph", () => {
		const input = "A paragraph.";
		const doc = parse(input, extended);
		const paragraph = doc.children![0];
		expect(paragraph.type).toBe("paragraph");
		expect(paragraph.index).toBe(0);
		expect(paragraph.length).toBe(12);
	});

	test("indent", () => {
		const input = "  indented paragraph";
		const doc = parse(input, extended);
		const indent = doc.children![0];
		expect(indent.type).toBe("paragraph");
		expect(indent.index).toBe(2);
		expect(indent.length).toBe(18);
	});

	test("escaped block", () => {
		const input = "\\# Not a heading";
		const doc = parse(input, extended);
		const escaped = doc.children![0];
		expect(escaped.type).toBe("paragraph");
		expect(escaped.index).toBe(0);
		expect(escaped.length).toBe(16);
	});
});

describe("source mapping - inline rules", () => {
	test("autolink - URL", () => {
		const input = "# Test\n\n<https://example.com>";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const autolink = paragraph.children![0];
		expect(autolink.type).toBe("link");
		expect(autolink.index).toBe(8);
		expect(autolink.length).toBe(21);
	});

	test("autolink - email", () => {
		const input = "# Test\n\n<user@example.com>";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const autolink = paragraph.children![0];
		expect(autolink.type).toBe("link");
		expect(autolink.index).toBe(8);
		expect(autolink.length).toBe(18);
	});

	test("extended autolink - www", () => {
		const input = "# Test\n\nwww.example.com";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const extendedAutolink = paragraph.children![0];
		expect(extendedAutolink.type).toBe("link");
		expect(extendedAutolink.index).toBe(8);
		expect(extendedAutolink.length).toBe(15);
	});

	test("code span", () => {
		const input = "# Test\n\n`code`";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const codeSpan = paragraph.children![0];
		expect(codeSpan.type).toBe("code_span");
		expect(codeSpan.index).toBe(8);
		expect(codeSpan.length).toBe(6);
	});

	test("emphasis - asterisk", () => {
		const input = "# Test\n\n*emphasis*";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const emphasis = paragraph.children![0];
		expect(emphasis.type).toBe("emphasis");
		expect(emphasis.index).toBe(8);
		expect(emphasis.length).toBe(10);
	});

	test("emphasis - underscore", () => {
		const input = "# Test\n\nhere: _emphasis_";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const emphasis = paragraph.children![1];
		expect(emphasis.type).toBe("emphasis");
		expect(emphasis.index).toBe(14);
		expect(emphasis.length).toBe(10);
	});

	test("strong", () => {
		const input = "# Test\n\n**strong**";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const strong = paragraph.children![0];
		expect(strong.type).toBe("strong");
		expect(strong.index).toBe(8);
		expect(strong.length).toBe(10);
	});

	test("link", () => {
		const input = "# Test\n\n[link](url)";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const link = paragraph.children![0];
		expect(link.type).toBe("link");
		expect(link.index).toBe(8);
		expect(link.length).toBe(11);
	});

	test("link with title", () => {
		const input = '# Test\n\n[link](url "title")';
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const link = paragraph.children![0];
		expect(link.type).toBe("link");
		expect(link.index).toBe(8);
		expect(link.length).toBe(19);
	});

	test("link with emphasis", () => {
		const input = "# Test\n\n[link *text*](url)";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];

		const link = paragraph.children![0];
		expect(link.type).toBe("link");
		expect(link.index).toBe(8);
		expect(link.length).toBe(18);

		const emphasis = link.children![1];
		expect(emphasis.type).toBe("emphasis");
		expect(emphasis.index).toBe(14);
		expect(emphasis.length).toBe(6);
	});

	test("footnote", () => {
		const input = "# Test\n\n[^1]";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const footnote = paragraph.children![0];
		expect(footnote.index).toBe(8);
		expect(footnote.length).toBe(4);
	});

	test("hard break", () => {
		const input = "# Test\n\nline  \nbreak";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const hardBreak = paragraph.children![1];
		expect(hardBreak.index).toBe(12);
		expect(hardBreak.length).toBe(2);
	});

	test("strikethrough", () => {
		const input = "# Test\n\n~~strikethrough~~";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const strikethrough = paragraph.children![0];
		expect(strikethrough.type).toBe("strikethrough");
		expect(strikethrough.index).toBe(8);
		expect(strikethrough.length).toBe(17);
	});

	test("highlight", () => {
		const input = "# Test\n\n==highlight==";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const highlight = paragraph.children![0];
		expect(highlight.type).toBe("highlight");
		expect(highlight.index).toBe(8);
		expect(highlight.length).toBe(13);
	});

	test("subscript", () => {
		const input = "# Test\n\n~subscript~";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const subscript = paragraph.children![0];
		expect(subscript.type).toBe("subscript");
		expect(subscript.index).toBe(8);
		expect(subscript.length).toBe(11);
	});

	test("superscript", () => {
		const input = "# Test\n\n^superscript^";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const superscript = paragraph.children![0];
		expect(superscript.type).toBe("superscript");
		expect(superscript.index).toBe(8);
		expect(superscript.length).toBe(13);
	});

	test("insertion", () => {
		const input = "# Test\n\n{++inserted++}";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const insertion = paragraph.children![0];
		expect(insertion.type).toBe("insertion");
		expect(insertion.index).toBe(8);
		expect(insertion.length).toBe(14);
	});

	test("deletion", () => {
		const input = "# Test\n\ndel: {--deleted--}";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const deletion = paragraph.children![1];
		expect(deletion.type).toBe("deletion");
		expect(deletion.index).toBe(13);
		expect(deletion.length).toBe(13);
	});

	test("html span", () => {
		const input = "# Test\n\n<span>content</span>";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const htmlStart = paragraph.children![0];
		const htmlEnd = paragraph.children![2];
		expect(htmlStart.type).toBe("html_span");
		expect(htmlStart.index).toBe(8);
		expect(htmlStart.length).toBe(6);
		expect(htmlEnd.type).toBe("html_span");
		expect(htmlEnd.index).toBe(21);
		expect(htmlEnd.length).toBe(7);
	});

	test("comment", () => {
		const input = "# Test\n\n<!-- comment -->";
		const doc = parse(input, extended);
		const comment = doc.children![1];
		expect(comment.index).toBe(8);
		expect(comment.length).toBe(16);
	});

	test("text", () => {
		const input = "# Test\n\nplain text";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const text = paragraph.children![0];
		expect(text.type).toBe("text");
		expect(text.index).toBe(8);
		expect(text.length).toBe(10);
	});

	test("text with special chars", () => {
		const input = "# Test\n\ntext with & chars";
		const doc = parse(input, extended);
		const paragraph = doc.children![1];
		const text = paragraph.children![0];
		expect(text.type).toBe("text");
		expect(text.index).toBe(8);
		expect(text.length).toBe(17);
	});

	test("paragraph with spaces after newline", () => {
		const input = "Test\n  text with `spaces`";
		const doc = parse(input, extended);
		const paragraph = doc.children![0];
		expect(paragraph.children!.length).toBe(3);
		const text = paragraph.children![0];
		expect(text.type).toBe("text");
		expect(text.index).toBe(0);
		expect(text.length).toBe(5);
		const text2 = paragraph.children![1];
		expect(text2.type).toBe("text");
		expect(text2.index).toBe(7);
		expect(text2.length).toBe(10);
		const code = paragraph.children![2];
		expect(code.type).toBe("code_span");
		expect(code.index).toBe(17);
		expect(code.length).toBe(8);
	});

	test("paragraph with spaces after hard break", () => {
		const input = "Test  \n  text with `spaces`";
		const doc = parse(input, extended);
		const paragraph = doc.children![0];
		expect(paragraph.children!.length).toBe(4);
		const text = paragraph.children![0];
		expect(text.type).toBe("text");
		expect(text.index).toBe(0);
		expect(text.length).toBe(4);
		const br = paragraph.children![1];
		expect(br.type).toBe("hard_break");
		expect(br.index).toBe(4);
		expect(br.length).toBe(2);
		const text2 = paragraph.children![2];
		expect(text2.type).toBe("text");
		expect(text2.index).toBe(9);
		expect(text2.length).toBe(10);
		const code = paragraph.children![3];
		expect(code.type).toBe("code_span");
		expect(code.index).toBe(19);
		expect(code.length).toBe(8);
	});
});

describe("source mapping - block and inline rules", () => {
	test("various formattings", () => {
		const input = "# Heading 1\n\nSome **bold** text, I'm ~~deleted~~, really {+gone+}";
		const doc = parse(input, extended);

		const heading = doc.children![0];
		expect(heading.type).toBe("heading");
		expect(heading.index).toBe(0);
		expect(heading.length).toBe(12);

		const paragraph = doc.children![1];
		expect(paragraph.type).toBe("paragraph");
		expect(paragraph.index).toBe(13);
		expect(paragraph.length).toBe(52);

		const strong = paragraph.children![1];
		expect(strong.type).toBe("strong");
		expect(strong.index).toBe(18);
		expect(strong.length).toBe(8);

		const strikethrough = paragraph.children![3];
		expect(strikethrough.type).toBe("strikethrough");
		expect(strikethrough.index).toBe(37);
		expect(strikethrough.length).toBe(11);

		const deletion = paragraph.children![5];
		expect(deletion.type).toBe("insertion");
		expect(deletion.index).toBe(57);
		expect(deletion.length).toBe(8);
	});
});
