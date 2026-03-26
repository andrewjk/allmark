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

test("renders heading to console with color", () => {
	const input = "# Heading 1\n## Heading 2";
	const doc = parse(input, core);
	const output = render(doc, consoleRenderers);
	expect(output).toBe(
		"\x1b[2m#\x1b[0m \x1b[1m\x1b[35mHeading 1\x1b[0m\n" +
			"\x1b[2m##\x1b[0m \x1b[1m\x1b[35mHeading 2\x1b[0m\n",
	);
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
		"\x1b[1m\x1b[35mHeading\x1b[0m\n\x1b[2m=======\x1b[0m\n" +
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
