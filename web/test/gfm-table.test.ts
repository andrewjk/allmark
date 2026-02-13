import { renderHtmlSync } from "cmark-gfm";
import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import gfm from "../src/rulesets/gfm";

const options = {
	footnotes: true,
	extensions: {
		strikethrough: true,
		table: true,
		tasklist: true,
		autolink: true,
	},
};

describe("table", () => {
	test("spec table", () => {
		const input = `
| foo | bar |
| --- | --- |
| baz | bim |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with alignment", () => {
		const input = `
| Left | Center | Right |
| :--- | :----: | ----: |
| foo  |  bar   |   baz |
| a    |   b    |     c |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with inline formatting", () => {
		const input = `
| Text | Code |
| ---- | ---- |
| **bold** | \`code\` |
| *italic* | [link](url) |
| ~~strike~~ | \`multi\` |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with missing cells", () => {
		const input = `
| a | b | c |
| - | - | - |
| 1 | 2 |
| 1 |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with extra cells", () => {
		const input = `
| a | b |
| - | - |
| 1 | 2 | 3 | 4 |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with only header", () => {
		const input = `
| foo | bar |
| --- | --- |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with empty cells", () => {
		const input = `
| a | b | c |
| - | - | - |
|   | 2 |   |
| 1 |   | 3 |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table without outer pipes", () => {
		const input = `
a | b | c
- | - | -
1 | 2 | 3
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with whitespace variations", () => {
		const input = `
|  a  |  b  |  c  |
| --- | --- | --- |
| 1   |   2 |3    |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with mixed content types", () => {
		const input = `
| Type | Example |
| ---- | ------- |
| Text | plain text |
| Code | \`inline\` |
| Bold | **strong** |
| Link | [text](http://example.com) |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with single column", () => {
		const input = `
| Column |
| ------ |
| data   |
| more   |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("table with many columns", () => {
		const input = `
| A | B | C | D | E | F |
| - | - | - | - | - | - |
| 1 | 2 | 3 | 4 | 5 | 6 |
| a | b | c | d | e | f |
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});
});
