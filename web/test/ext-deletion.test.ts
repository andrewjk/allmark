import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import extended from "../src/rules/extended";

describe("deletion", () => {
	test("deletion single", () => {
		const input = `
This text was {-deleted-} recently.
 `;
		const expected = `
<p>This text was <del class="markdown-deletion">deleted</del> recently.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion double", () => {
		const input = `
This text was {--deleted--} recently.
 `;
		const expected = `
<p>This text was <del class="markdown-deletion">deleted</del> recently.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion triple", () => {
		const input = `
This text was {---deleted---} recently.
 `;
		const expected = `
<p>This text was {---deleted---} recently.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion single character", () => {
		const input = `text {-a-} more`;
		const expected = `<p>text <del class="markdown-deletion">a</del> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion with spaces", () => {
		const input = `text {-with spaces-} more`;
		const expected = `<p>text <del class="markdown-deletion">with spaces</del> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion at start of paragraph", () => {
		const input = `{-deleted-} This is new.`;
		const expected = `<p><del class="markdown-deletion">deleted</del> This is new.</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion at end of paragraph", () => {
		const input = `This is {-deleted-}`;
		const expected = `<p>This is <del class="markdown-deletion">deleted</del></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion with punctuation", () => {
		const input = `text {-word!-} more`;
		const expected = `<p>text <del class="markdown-deletion">word!</del> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion with special characters", () => {
		const input = `text {-a-b-} more`;
		const expected = `<p>text <del class="markdown-deletion">a-b</del> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion adjacent to text", () => {
		const input = `test{-ing-}test`;
		const expected = `<p>test<del class="markdown-deletion">ing</del>test</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("empty deletion", () => {
		const input = `text{--}text`;
		const expected = `<p>text{--}text</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion with markdown inside", () => {
		const input = `text {-**bold**-}`;
		const expected = `<p>text <del class="markdown-deletion"><strong>bold</strong></del></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion with code inside", () => {
		const input = `text {-\`code\`-}`;
		const expected = `<p>text <del class="markdown-deletion"><code>code</code></del></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("escaped braces should not be deletion", () => {
		const input = `text \\{-not deletion\\-}`;
		const expected = `<p>text {-not deletion-}</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched opening deletion", () => {
		const input = `text {-not closed`;
		const expected = `<p>text {-not closed</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched closing deletion", () => {
		const input = `text not opened-}`;
		const expected = `<p>text not opened-}</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion in list item", () => {
		const input = `- Item with {-deletion-}`;
		const expected = `<ul>
<li>Item with <del class="markdown-deletion">deletion</del></li>
</ul>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion in blockquote", () => {
		const input = `> Quote with {-deletion-}`;
		const expected = `<blockquote>
<p>Quote with <del class="markdown-deletion">deletion</del></p>
</blockquote>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion with plus inside", () => {
		const input = `text {-plus - inside-}`;
		const expected = `<p>text <del class="markdown-deletion">plus - inside</del></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion at beginning of document", () => {
		const input = `{-Start-} of document.`;
		const expected = `<p><del class="markdown-deletion">Start</del> of document.</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion at end of document", () => {
		const input = `End of {-document-}`;
		const expected = `<p>End of <del class="markdown-deletion">document</del></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("multiple deletions in one line", () => {
		const input = `{-first-} and {-second-} and {-third-}`;
		const expected = `<p><del class="markdown-deletion">first</del> and <del class="markdown-deletion">second</del> and <del class="markdown-deletion">third</del></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion with starting emphasis", () => {
		const input = `{-deleted *text-} that shouldn't be bold*`;
		const expected = `<p><del class="markdown-deletion">deleted *text</del> that shouldn't be bold*</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("deletion with ending emphasis", () => {
		const input = `*this text should be {-deleted but not bold*-}`;
		const expected = `<p>*this text should be <del class="markdown-deletion">deleted but not bold*</del></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});
});
