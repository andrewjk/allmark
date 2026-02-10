import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import extended from "../src/rules/extended";

describe("superscript", () => {
	test("superscript single", () => {
		const input = `
This should be ^up^ above everything else.
 `;
		const expected = `
<p>This should be <sup>up</sup> above everything else.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript double", () => {
		const input = `
This should be ^^up^^ above everything else.
 `;
		const expected = `
<p>This should be <sup>up</sup> above everything else.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript triple", () => {
		const input = `
This should be ^^^up^^^ above everything else.
 `;
		const expected = `
<p>This should be ^^^up^^^ above everything else.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript single character", () => {
		const input = `x^2^`;
		const expected = `<p>x<sup>2</sup></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript with numbers", () => {
		const input = `E=mc^2^`;
		const expected = `<p>E=mc<sup>2</sup></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("multiple superscripts in one line", () => {
		const input = `x^2^ + y^2^ = z^2^`;
		const expected = `<p>x<sup>2</sup> + y<sup>2</sup> = z<sup>2</sup></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript at start of paragraph", () => {
		const input = `^note^ This is important.`;
		const expected = `<p><sup>note</sup> This is important.</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript at end of paragraph", () => {
		const input = `See footnote^1^`;
		const expected = `<p>See footnote<sup>1</sup></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript with punctuation", () => {
		const input = `Hello^world!^`;
		const expected = `<p>Hello<sup>world!</sup></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript with spaces", () => {
		const input = `text ^with spaces^ more`;
		const expected = `<p>text <sup>with spaces</sup> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript with special characters", () => {
		const input = `math^2+3^`;
		const expected = `<p>math<sup>2+3</sup></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript adjacent to text", () => {
		const input = `test^ing^test`;
		const expected = `<p>test<sup>ing</sup>test</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("empty superscript", () => {
		const input = `text^^text`;
		const expected = `<p>text^^text</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript with markdown inside", () => {
		const input = `text ^**bold**^`;
		const expected = `<p>text <sup><strong>bold</strong></sup></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript with code inside", () => {
		const input = `text ^\`code\`^`;
		const expected = `<p>text <sup><code>code</code></sup></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("escaped caret should not be superscript", () => {
		const input = `text \\^not superscript\\^`;
		const expected = `<p>text ^not superscript^</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched opening caret", () => {
		const input = `text ^not closed`;
		const expected = `<p>text ^not closed</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched closing caret", () => {
		const input = `text not opened^`;
		const expected = `<p>text not opened^</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript in list item", () => {
		const input = `- Item with ^superscript^`;
		const expected = `<ul>
<li>Item with <sup>superscript</sup></li>
</ul>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript in blockquote", () => {
		const input = `> Quote with ^superscript^`;
		const expected = `<blockquote>
<p>Quote with <sup>superscript</sup></p>
</blockquote>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("nested superscript", () => {
		const input = `x^y^z^`;
		// The first pair of carets creates a superscript, leaving ^z^ as text
		const expected = `<p>x<sup>y</sup>z^</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("superscript with caret inside", () => {
		const input = `text ^caret ^ inside^`;
		const expected = `<p>text <sup>caret ^ inside</sup></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});
});
