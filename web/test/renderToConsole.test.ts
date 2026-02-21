import { expect, test } from "vitest";
import parse from "../src/parse";
import renderToConsole from "../src/renderToConsole";
import core from "../src/rulesets/core";
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
	expect(output).toContain("# Heading 1");
	expect(output).toContain("## Heading 2");
});

test("renders bulleted list with Unicode bullets", () => {
	const input = "- Item 1\n- Item 2";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("• Item 1");
	expect(output).toContain("• Item 2");
});

test("renders ordered list", () => {
	const input = "1. First\n2. Second";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("1. First");
	expect(output).toContain("2. Second");
});

test("renders code block with box drawing", () => {
	const input = "```\ncode\n```";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("┌─");
	expect(output).toContain("│");
	expect(output).toContain("└─");
});

test("renders inline code", () => {
	const input = "`code`";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("`code`");
});

test("renders block quote with vertical line", () => {
	const input = "> Quote text";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	const stripped = output.replace(/\x1b\[[0-9;]*m/g, "");
	expect(stripped).toContain("┃ Quote text");
});

test("renders thematic break", () => {
	const input = "---";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("─");
});

test("renders task list with emojis", () => {
	const input = "- [x] Done\n- [ ] Todo";
	const root = parse(input, gfm, false);
	const output = renderToConsole(root);
	expect(output).toContain("[✓]");
	expect(output).toContain("[ ]");
});

test("renders table with Unicode borders", () => {
	const input = "| A | B |\n|---|---|\n| 1 | 2 |";
	const root = parse(input, gfm, false);
	const output = renderToConsole(root);
	expect(output).toContain("┌");
	expect(output).toContain("┬");
	expect(output).toContain("┐");
	expect(output).toContain("┼");
	expect(output).toContain("│");
	expect(output).toContain("├");
	expect(output).toContain("┤");
	expect(output).toContain("└");
	expect(output).toContain("┴");
	expect(output).toContain("┘");
});

test("renders strong text", () => {
	const input = "**bold**";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("bold");
});

test("renders emphasis text", () => {
	const input = "*italic*";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("italic");
});

test("renders link", () => {
	const input = "[text](url)";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("text");
	expect(output).toContain("url");
});

test("renders image", () => {
	const input = "![alt](url)";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("Image");
});

test("renders strikethrough", () => {
	const input = "~~deleted~~";
	const root = parse(input, gfm, false);
	const output = renderToConsole(root);
	expect(output).toContain("\x1b[9mdeleted\x1b[29m");
});

test("renders alert with emoji", () => {
	const input = "> [!NOTE]\n> Note content";
	const root = parse(input, gfm, false);
	const output = renderToConsole(root);
	expect(output).toContain("📝");
	expect(output).toContain("Note:");
});

test("renders nested list with different bullets", () => {
	const input = "- Level 1\n  - Level 2\n    - Level 3";
	const root = parse(input, core, false);
	const output = renderToConsole(root);
	expect(output).toContain("• Level 1");
	expect(output).toContain("◦ Level 2");
});
