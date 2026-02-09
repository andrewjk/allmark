import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import extended from "../src/rules/extended";

describe("highlight", () => {
	test("highlight single", () => {
		const input = `
This should be =highlighted= as it is important.
 `;
		const expected = `
<p>This should be <mark>highlighted</mark> as it is important.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight double", () => {
		const input = `
This should be ==highlighted== as it is important.
 `;
		const expected = `
<p>This should be <mark>highlighted</mark> as it is important.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight triple", () => {
		const input = `
This should be ===highlighted=== as it is important.
 `;
		const expected = `
<p>This should be ===highlighted=== as it is important.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight single character", () => {
		const input = `text =a= more`;
		const expected = `<p>text <mark>a</mark> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("multiple highlights in one line", () => {
		const input = `=first= and =second= and =third=`;
		const expected = `<p><mark>first</mark> and <mark>second</mark> and <mark>third</mark></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight at start of paragraph", () => {
		const input = `=highlighted= This is important.`;
		const expected = `<p><mark>highlighted</mark> This is important.</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight at end of paragraph", () => {
		const input = `This is =highlighted=`;
		const expected = `<p>This is <mark>highlighted</mark></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight with punctuation", () => {
		const input = `text =word!= more`;
		const expected = `<p>text <mark>word!</mark> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight with spaces", () => {
		const input = `text =with spaces= more`;
		const expected = `<p>text <mark>with spaces</mark> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight with special characters", () => {
		const input = `text =a+b= more`;
		const expected = `<p>text <mark>a+b</mark> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight adjacent to text", () => {
		const input = `test=ing=test`;
		const expected = `<p>test<mark>ing</mark>test</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("empty highlight", () => {
		const input = `text==text`;
		const expected = `<p>text==text</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight with markdown inside", () => {
		const input = `text =**bold**=`;
		const expected = `<p>text <mark><strong>bold</strong></mark></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight with code inside", () => {
		const input = `text =\`code\`=`;
		const expected = `<p>text <mark><code>code</code></mark></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("escaped equals should not be highlight", () => {
		const input = `text \\=not highlight\\=`;
		const expected = `<p>text =not highlight=</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched opening equals", () => {
		const input = `text =not closed`;
		const expected = `<p>text =not closed</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched closing equals", () => {
		const input = `text not opened=`;
		const expected = `<p>text not opened=</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight in list item", () => {
		const input = `- Item with =highlight=`;
		const expected = `<ul>
<li>Item with <mark>highlight</mark></li>
</ul>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight in blockquote", () => {
		const input = `> Quote with =highlight=`;
		const expected = `<blockquote>
<p>Quote with <mark>highlight</mark></p>
</blockquote>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight with equals inside", () => {
		const input = `text =equals = inside=`;
		const expected = `<p>text <mark>equals = inside</mark></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight at beginning of document", () => {
		const input = `=Start= of document.`;
		const expected = `<p><mark>Start</mark> of document.</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("highlight at end of document", () => {
		const input = `End of =document=`;
		const expected = `<p>End of <mark>document</mark></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});
});
