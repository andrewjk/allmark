import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import extended from "../src/rulesets/extended";

describe("subscript", () => {
	test("subscript single", () => {
		const input = `
This should be ~down~ below everything else.
 `;
		const expected = `
<p>This should be <sub>down</sub> below everything else.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	// NOTE: GFM strikethrough must take precedence
	test("subscript double", () => {
		const input = `
This should be ~~down~~ below everything else.
 `;
		const expected = `
<p>This should be <del>down</del> below everything else.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript triple", () => {
		const input = `
This should be ~~~down~~~ below everything else.
 `;
		const expected = `
<p>This should be ~~~down~~~ below everything else.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript single character", () => {
		const input = `H~2~O`;
		const expected = `<p>H<sub>2</sub>O</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript with numbers", () => {
		const input = `x~1~ + x~2~`;
		const expected = `<p>x<sub>1</sub> + x<sub>2</sub></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("multiple subscripts in one line", () => {
		const input = `a~i~ + b~j~ = c~k~`;
		const expected = `<p>a<sub>i</sub> + b<sub>j</sub> = c<sub>k</sub></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript at start of paragraph", () => {
		const input = `~note~ This is important.`;
		const expected = `<p><sub>note</sub> This is important.</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript at end of paragraph", () => {
		const input = `See index~1~`;
		const expected = `<p>See index<sub>1</sub></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript with punctuation", () => {
		const input = `Hello~world!~`;
		const expected = `<p>Hello<sub>world!</sub></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript with spaces", () => {
		const input = `text ~with spaces~ more`;
		const expected = `<p>text <sub>with spaces</sub> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript with special characters", () => {
		const input = `math~i+j~`;
		const expected = `<p>math<sub>i+j</sub></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript adjacent to text", () => {
		const input = `test~ing~test`;
		const expected = `<p>test<sub>ing</sub>test</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("empty subscript", () => {
		const input = `text~~text`;
		const expected = `<p>text~~text</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript with markdown inside", () => {
		const input = `text ~**bold**~`;
		const expected = `<p>text <sub><strong>bold</strong></sub></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript with code inside", () => {
		const input = `text ~\`code\`~`;
		const expected = `<p>text <sub><code>code</code></sub></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("escaped tilde should not be subscript", () => {
		const input = `text \\~not subscript\\~`;
		const expected = `<p>text ~not subscript~</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched opening tilde", () => {
		const input = `text ~not closed`;
		const expected = `<p>text ~not closed</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched closing tilde", () => {
		const input = `text not opened~`;
		const expected = `<p>text not opened~</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript in list item", () => {
		const input = `- Item with ~subscript~`;
		const expected = `<ul>
<li>Item with <sub>subscript</sub></li>
</ul>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript in blockquote", () => {
		const input = `> Quote with ~subscript~`;
		const expected = `<blockquote>
<p>Quote with <sub>subscript</sub></p>
</blockquote>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("strikethrough vs subscript precedence", () => {
		const input = `This is ~~deleted~~ text.`;
		const expected = `<p>This is <del>deleted</del> text.</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("subscript with tilde inside", () => {
		const input = `text ~tilde ~ inside~`;
		const expected = `<p>text <sub>tilde ~ inside</sub></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("strikethrough still works", () => {
		const input = `text ~~struck~~, not subscripted`;
		const expected = `<p>text <del>struck</del>, not subscripted</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});
});
