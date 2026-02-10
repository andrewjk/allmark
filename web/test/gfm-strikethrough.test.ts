import { renderHtmlSync } from "cmark-gfm";
import { expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import gfm from "../src/rules/gfm";

const options = {
	footnotes: true,
	extensions: {
		strikethrough: true,
		table: true,
		tasklist: true,
		autolink: true,
	},
};

test("spec strikethrough", () => {
	const input = `
~~Hi~~ Hello, world!
`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input.substring(1, input.length - 1), gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough single word", () => {
	const input = `~~deleted~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough multiple words", () => {
	const input = `~~this is deleted~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough with spaces inside", () => {
	const input = `~~  spaces  ~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough with emphasis", () => {
	const input = `~~*bold and deleted*~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough inside emphasis", () => {
	const input = `*~~deleted in italic~~*`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough with code", () => {
	const input = `~~code: \`var x\` here~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough with link", () => {
	const input = `~~[link text](http://example.com)~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("multiple strikethroughs in one line", () => {
	const input = `~~first~~ and ~~second~~ and ~~third~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough at start of paragraph", () => {
	const input = `~~deleted~~ followed by normal text.`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough at end of paragraph", () => {
	const input = `Normal text followed by ~~deleted~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough in list item", () => {
	const input = `- ~~deleted item~~
- normal item`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough with tildes inside", () => {
	const input = `~~text with ~ tilde~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough with multiple tildes", () => {
	const input = `~~~~double~~~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough across lines", () => {
	const input = `~~line one
line two~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough with punctuation", () => {
	const input = `~~Hello, world!~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough with numbers", () => {
	const input = `~~12345~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough in table cell", () => {
	const input = `| col1 | col2 |
| ---- | ---- |
| ~~deleted~~ | normal |`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough adjacent to regular text", () => {
	const input = `normal~~deleted~~normal`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});

test("strikethrough with escaped characters", () => {
	const input = `~~text with \\*asterisk\\*~~`;
	const expected = renderHtmlSync(input, options);
	const doc = parse(input, gfm);
	const html = renderHtml(doc, gfm.renderers);
	expect(html.trim()).toBe(expected.trim());
});
