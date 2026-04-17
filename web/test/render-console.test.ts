import { stripVTControlCharacters } from "node:util";

import { expect, test } from "vite-plus/test";

import parse from "../src/parse";
import render from "../src/render";
import consoleRenderers from "../src/rulesets/consoleRenderers";
import core from "../src/rulesets/core";
import extended from "../src/rulesets/extended";
import gfm from "../src/rulesets/gfm";

test("renders paragraph to console", () => {
	const input = "Hello, world!";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("Hello, world!\n");
});

test("renders paragraph then paragraph to console", () => {
	const input = "Hello, world!\n\nHello again";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("Hello, world!\n\nHello again\n");
});

test("renders heading to console with color", () => {
	const input = "# Heading 1\n## Heading 2";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe(
		"\x1b[2m#\x1b[0m \x1b[1m\x1b[35mHeading 1\x1b[0m\n\n" +
			"\x1b[2m##\x1b[0m \x1b[1m\x1b[35mHeading 2\x1b[0m\n",
	);
});

test("renders heading then heading to console", () => {
	const input = "# Heading 1\n## Heading 2";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("# Heading 1\n\n## Heading 2\n");
});

test("renders paragraph x 3 to console", () => {
	const input = "First\n\nSecond\n\nThird";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("First\n\nSecond\n\nThird\n");
});

test("renders heading then paragraph", () => {
	const input = "# Heading\n\nParagraph text";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("# Heading\n\nParagraph text\n");
});

test("renders paragraph then heading", () => {
	const input = "Paragraph text\n\n# Heading";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("Paragraph text\n\n# Heading\n");
});

test("renders heading then list", () => {
	const input = "# Heading\n\n- Item 1\n- Item 2";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("# Heading\n\n• Item 1\n• Item 2\n");
});

test("renders list then heading", () => {
	const input = "- Item 1\n- Item 2\n\n# Heading";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("• Item 1\n• Item 2\n\n# Heading\n");
});

test("renders paragraph then list", () => {
	const input = "Paragraph\n\n- Item 1\n- Item 2";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("Paragraph\n\n• Item 1\n• Item 2\n");
});

test("renders list then paragraph", () => {
	const input = "- Item 1\n- Item 2\n\nParagraph";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("• Item 1\n• Item 2\n\nParagraph\n");
});

test("renders heading then code block", () => {
	const input = "# Heading\n\n```\ncode\n```";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("# Heading\n\n┌─\n│ code\n└─\n");
});

test("renders code block then heading", () => {
	const input = "```\ncode\n```\n\n# Heading";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("┌─\n│ code\n└─\n\n# Heading\n");
});

test("renders heading then block quote", () => {
	const input = "# Heading\n\n> Quote text";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("# Heading\n\n┃ Quote text\n");
});

test("renders block quote then heading", () => {
	const input = "> Quote text\n\n# Heading";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("┃ Quote text\n\n# Heading\n");
});

test("renders heading then thematic break", () => {
	const input = "# Heading\n\n---";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("# Heading\n\n───\n");
});

test("renders thematic break then heading", () => {
	const input = "---\n\n# Heading";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("───\n\n# Heading\n");
});

test("renders multiple block types", () => {
	const input = "# Heading 1\n\nParagraph 1\n\n---\n\n## Heading 2\n\nParagraph 2";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("# Heading 1\n\nParagraph 1\n\n───\n\n## Heading 2\n\nParagraph 2\n");
});

test("renders alert then paragraph", () => {
	const input = "> [!NOTE]\n> Note\n\nParagraph";
	const doc = parse(input, gfm);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("📝 Note:\n\nNote\n\nParagraph\n");
});

test("renders paragraph then alert", () => {
	const input = "Paragraph\n\n> [!NOTE]\n> Note";
	const doc = parse(input, gfm);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("Paragraph\n\n📝 Note:\n\nNote\n");
});

test("renders bulleted list with Unicode bullets", () => {
	const input = "- Item 1\n- Item 2";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[2m•\x1b[0m Item 1\n\x1b[2m•\x1b[0m Item 2\n");
});

test("renders ordered list", () => {
	const input = "1. First\n2. Second";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[2m1.\x1b[0m First\n\x1b[2m2.\x1b[0m Second\n");
});

test("renders tight bulleted list", () => {
	const input = "- Item 1\n- Item 2\n- Item 3";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("• Item 1\n• Item 2\n• Item 3\n");
});

test("renders loose bulleted list", () => {
	const input = "- Item 1\n\n- Item 2\n\n- Item 3";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("• Item 1\n\n• Item 2\n\n• Item 3\n");
});

test("renders tight ordered list", () => {
	const input = "1. First\n2. Second\n3. Third";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("1. First\n2. Second\n3. Third\n");
});

test("renders loose ordered list", () => {
	const input = "1. First\n\n2. Second\n\n3. Third";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("1. First\n\n2. Second\n\n3. Third\n");
});

test("renders ordered list with nested bulleted list", () => {
	const input = "1. First\n   - Nested A\n   - Nested B\n2. Second";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("1. First\n  ◦ Nested A\n  ◦ Nested B\n2. Second\n");
});

test("renders bulleted list with nested ordered list", () => {
	const input = "- First\n  1. Nested A\n  2. Nested B\n- Second";
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("• First\n  1. Nested A\n  2. Nested B\n• Second\n");
});

test("renders code fence with box drawing", () => {
	const input = "```\ncode\n```";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[2m┌─\x1b[0m\n\x1b[2m│\x1b[0m code\n\x1b[2m└─\x1b[0m\n");
});

test("renders inline code", () => {
	const input = "`code`";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[32mcode\x1b[0m\n");
});

test("renders block quote with vertical line", () => {
	const input = "> Quote text";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	const stripped = output.replace(/\x1b\[[0-9;]*m/g, "");
	expect(stripped).toBe("┃ Quote text\n");
});

test("renders thematic break", () => {
	const input = "---";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[2m───\x1b[0m\n");
});

test("renders task list with emojis", () => {
	const input = "- [x] Done\n- [ ] Todo";
	const doc = parse(input, gfm);
	const output = render(doc, consoleRenderers);
	expect(output).toBe(
		"\x1b[2m•\x1b[0m \x1b[2m[\x1b[0m✓\x1b[2m]\x1b[0m Done\n\x1b[2m•\x1b[0m \x1b[2m[\x1b[0m \x1b[2m]\x1b[0m Todo\n",
	);
});

test("renders table with Unicode borders", () => {
	const input = "| A | B |\n|---|---|\n| 1 | 2 |";
	const doc = parse(input, gfm);
	const output = render(doc, consoleRenderers);
	expect(output).toBe(
		"\x1b[2m┌───┬───┐\x1b[0m\n\x1b[2m│\x1b[0m A \x1b[2m│\x1b[0m B \x1b[2m│\x1b[0m\n\x1b[2m├───┼───┤\x1b[0m\n\x1b[2m│\x1b[0m 1 \x1b[2m│\x1b[0m 2 \x1b[2m│\x1b[0m\n\x1b[2m└───┴───┘\x1b[0m\n",
	);
});

test("renders table then paragraph", () => {
	const input = "| A |\n|---|\n| 1 |\n\nParagraph";
	const doc = parse(input, gfm);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("┌───┐\n│ A │\n├───┤\n│ 1 │\n└───┘\nParagraph\n");
});

test("renders paragraph then table", () => {
	const input = "Paragraph\n\n| A |\n|---|\n| 1 |";
	const doc = parse(input, gfm);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe("Paragraph\n\n┌───┐\n│ A │\n├───┤\n│ 1 │\n└───┘\n");
});

test("renders table with padding", () => {
	const input = "| A | B |\n| - | - |\n| 1 | hello |";
	const doc = parse(input, gfm);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe(
		`
┌───┬───────┐
│ A │ B     │
├───┼───────┤
│ 1 │ hello │
└───┴───────┘
`.trimStart(),
	);
});

test("renders table with correctly aligned padding", () => {
	const input = "| A | B |\n| - | -: |\n| x | 1 |\n| y | 200 |";
	const doc = parse(input, gfm);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe(
		`
┌───┬─────┐
│ A │   B │
├───┼─────┤
│ x │   1 │
│ y │ 200 │
└───┴─────┘
`.trimStart(),
	);
});

test("renders strong text", () => {
	const input = "**bold**";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[1m\x1b[33mbold\x1b[0m\n");
});

test("renders emphasis text", () => {
	const input = "*italic*";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[3m\x1b[33mitalic\x1b[0m\n");
});

test("renders link", () => {
	const input = "[text](url)";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[4m\x1b[34mtext\x1b[0m \x1b[2m(url)\x1b[0m\n");
});

test("renders image", () => {
	const input = "![alt](url)";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[90m[Image: alt]\x1b[0m\n");
});

test("renders strikethrough", () => {
	const input = "~~deleted~~";
	const doc = parse(input, gfm);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m\n");
});

test("renders alert with emoji", () => {
	const input = "> [!NOTE]\n> Note content";
	const doc = parse(input, gfm);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[34m📝 Note:\x1b[0m\n\nNote content\n");
});

test("renders nested list with different bullets", () => {
	const input = "- Level 1\n  - Level 2\n    - Level 3";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe(
		"\x1b[2m•\x1b[0m Level 1\n  \x1b[2m◦\x1b[0m Level 2\n    \x1b[2m▪\x1b[0m Level 3\n",
	);
});

test("renders hard break", () => {
	const input = "Line 1\n\nLine 2";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("Line 1\n\nLine 2\n");
});

test("renders heading with underline Setext style", () => {
	const input = "Heading\n=======\n\nSubheading\n-------";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe(
		"\x1b[1m\x1b[35mHeading\x1b[0m\n\x1b[2m=======\x1b[0m\n\n" +
			"\x1b[1m\x1b[35mSubheading\x1b[0m\n\x1b[2m----------\x1b[0m\n",
	);
});

test("renders HTML block", () => {
	const input = "<div>html</div>";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("<div>html</div>\n");
});

test("renders HTML span inline", () => {
	const input = "test <span>html</span> test";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("test <span>html</span> test\n");
});

test("renders comment", () => {
	const input = "<!-- comment -->";
	const doc = parse(input, extended);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("<!-- comment -->\n");
});

test("renders deletion (strikethrough alternative)", () => {
	const input = "~~deleted~~";
	const doc = parse(input, extended);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m\n");
});

test("renders footnote", () => {
	const input = "Text [^1]\n\n[^1]: http://example.com";
	const doc = parse(input, gfm);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("Text \x1b[2m[1]\x1b[0m\n");
});

test("renders highlight", () => {
	const input = "==highlighted==";
	const doc = parse(input, extended);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("\x1b[43m\x1b[30mhighlighted\x1b[0m\n");
});

test("renders insertion", () => {
	const input = "++inserted++";
	const doc = parse(input, extended);
	const output = render(doc, consoleRenderers);
	expect(output).toBe("++inserted++\n");
});

test("basic parse and render", () => {
	const input = `
# Test

Here is some text

* Tight item 1
  * Nested item 1
* Tight item 2

- Loose item 1

- Loose item 2

## Subtest

Here is some more text
`;
	const expected = `
# Test

Here is some text

• Tight item 1
  ◦ Nested item 1
• Tight item 2

• Loose item 1

• Loose item 2

## Subtest

Here is some more text
`.trimStart();
	const doc = parse(input, core);
	const output = stripVTControlCharacters(render(doc, consoleRenderers));
	expect(output).toBe(expected);
});
