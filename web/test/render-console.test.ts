import { expect, test } from "vitest";
import parse from "../src/parse";
import renderToConsole from "../src/renderToConsole";
import core from "../src/rulesets/core";
import extended from "../src/rulesets/extended";
import gfm from "../src/rulesets/gfm";

test("renders paragraph to console", () => {
	const input = "Hello, world!";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("Hello, world!");
});

test("renders heading to console with color", () => {
	const input = "# Heading 1\n## Heading 2";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[1m\x1b[36m# Heading 1\x1b[0m\n\x1b[1m\x1b[34m## Heading 2\x1b[0m");
});

test("renders bulleted list with Unicode bullets", () => {
	const input = "- Item 1\n- Item 2";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("• Item 1\n• Item 2");
});

test("renders ordered list", () => {
	const input = "1. First\n2. Second";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("1. First\n2. Second");
});

test("renders code fence with box drawing", () => {
	const input = "```\ncode\n```";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[2m┌─\x1b[0m\n\x1b[2m│\x1b[0m code\n\x1b[2m└─\x1b[0m");
});

test("renders inline code", () => {
	const input = "`code`";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[32m`code`\x1b[0m");
});

test("renders block quote with vertical line", () => {
	const input = "> Quote text";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	const stripped = output.replace(/\x1b\[[0-9;]*m/g, "");
	expect(stripped).toBe("┃ Quote text");
});

test("renders thematic break", () => {
	const input = "---";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[2m───\x1b[0m");
});

test("renders task list with emojis", () => {
	const input = "- [x] Done\n- [ ] Todo";
	const root = parse(input, gfm, false);
	const output = renderToConsole(root);
	expect(output).toBe("• [✓] Done\n• [ ] Todo");
});

test("renders table with Unicode borders", () => {
	const input = "| A | B |\n|---|---|\n| 1 | 2 |";
	const root = parse(input, gfm, false);
	const output = renderToConsole(root);
	expect(output).toBe(
		"\x1b[2m┌───┬───┐\x1b[0m\n\x1b[2m│\x1b[0m A \x1b[2m│\x1b[0m B \x1b[2m│\x1b[0m\n\x1b[2m├───┼───┤\x1b[0m\n\x1b[2m│\x1b[0m 1 \x1b[2m│\x1b[0m 2 \x1b[2m│\x1b[0m\n\x1b[2m└───┴───┘\x1b[0m",
	);
});

test("renders strong text", () => {
	const input = "**bold**";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[1m\x1b[33mbold\x1b[0m");
});

test("renders emphasis text", () => {
	const input = "*italic*";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[3m\x1b[33mitalic\x1b[0m");
});

test("renders link", () => {
	const input = "[text](url)";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[34m\x1b[4mtext\x1b[0m (url)");
});

test("renders image", () => {
	const input = "![alt](url)";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[2m[Image: alt]\x1b[0m");
});

test("renders strikethrough", () => {
	const input = "~~deleted~~";
	const root = parse(input, gfm, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m");
});

test("renders alert with emoji", () => {
	const input = "> [!NOTE]\n> Note content";
	const root = parse(input, gfm, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[34m📝 Note:\x1b[0m\n\nNote content");
});

test("renders nested list with different bullets", () => {
	const input = "- Level 1\n  - Level 2\n    - Level 3";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("• Level 1\n  ◦ Level 2\n    ▪ Level 3");
});

test("renders hard break", () => {
	const input = "Line 1\n\nLine 2";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("Line 1\n\nLine 2");
});

test("renders heading with underline Setext style", () => {
	const input = "Heading\n=======\n\nSubheading\n-------";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe(
		"\x1b[1m\x1b[36mHeading\n=======\x1b[0m\n\x1b[1m\x1b[34mSubheading\n----------\x1b[0m",
	);
});

test("renders HTML block", () => {
	const input = "<div>html</div>";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("<div>html</div>");
});

test("renders HTML span inline", () => {
	const input = "test <span>html</span> test";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toBe("test <span>html</span> test");
});

test("renders comment", () => {
	const input = "<!-- comment -->";
	const root = parse(input, extended, false);
	const output = renderToConsole(root);
	expect(output).toBe("<!-- comment -->");
});

test("renders deletion (strikethrough alternative)", () => {
	const input = "~~deleted~~";
	const root = parse(input, extended, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[2m\x1b[9mdeleted\x1b[29m\x1b[0m");
});

test("renders footnote", () => {
	const input = "Text [^1]\n\n[^1]: http://example.com";
	const root = parse(input, gfm, false);
	const output = renderToConsole(root);
	expect(output).toBe("Text \x1b[2m[1]\x1b[0m");
});

test("renders highlight", () => {
	const input = "==highlighted==";
	const root = parse(input, extended, false);
	const output = renderToConsole(root);
	expect(output).toBe("\x1b[43m\x1b[30mhighlighted\x1b[0m");
});

test("renders insertion", () => {
	const input = "++inserted++";
	const root = parse(input, extended, false);
	const output = renderToConsole(root);
	expect(output).toBe("++inserted++");
});
