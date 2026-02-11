import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import extended from "../src/rulesets/extended";

describe("insertion", () => {
	test("insertion single", () => {
		const input = `
This text was {+inserted+} recently.
 `;
		const expected = `
<p>This text was <ins class="markdown-insertion">inserted</ins> recently.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion double", () => {
		const input = `
This text was {++inserted++} recently.
 `;
		const expected = `
<p>This text was <ins class="markdown-insertion">inserted</ins> recently.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion triple", () => {
		const input = `
This text was {+++inserted+++} recently.
 `;
		const expected = `
<p>This text was {+++inserted+++} recently.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion single character", () => {
		const input = `text {+a+} more`;
		const expected = `<p>text <ins class="markdown-insertion">a</ins> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion with spaces", () => {
		const input = `text {+with spaces+} more`;
		const expected = `<p>text <ins class="markdown-insertion">with spaces</ins> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion at start of paragraph", () => {
		const input = `{+inserted+} This is new.`;
		const expected = `<p><ins class="markdown-insertion">inserted</ins> This is new.</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion at end of paragraph", () => {
		const input = `This is {+inserted+}`;
		const expected = `<p>This is <ins class="markdown-insertion">inserted</ins></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion with punctuation", () => {
		const input = `text {+word!+} more`;
		const expected = `<p>text <ins class="markdown-insertion">word!</ins> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion with special characters", () => {
		const input = `text {+a+b+} more`;
		const expected = `<p>text <ins class="markdown-insertion">a+b</ins> more</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion adjacent to text", () => {
		const input = `test{+ing+}test`;
		const expected = `<p>test<ins class="markdown-insertion">ing</ins>test</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("empty insertion", () => {
		const input = `text{++}text`;
		const expected = `<p>text{++}text</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion with markdown inside", () => {
		const input = `text {+**bold**+}`;
		const expected = `<p>text <ins class="markdown-insertion"><strong>bold</strong></ins></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion with code inside", () => {
		const input = `text {+\`code\`+}`;
		const expected = `<p>text <ins class="markdown-insertion"><code>code</code></ins></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("escaped braces should not be insertion", () => {
		const input = `text \\{+not insertion\\+}`;
		const expected = `<p>text {+not insertion+}</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched opening insertion", () => {
		const input = `text {+not closed`;
		const expected = `<p>text {+not closed</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("unmatched closing insertion", () => {
		const input = `text not opened+}`;
		const expected = `<p>text not opened+}</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion in list item", () => {
		const input = `- Item with {+insertion+}`;
		const expected = `<ul>
<li>Item with <ins class="markdown-insertion">insertion</ins></li>
</ul>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion in blockquote", () => {
		const input = `> Quote with {+insertion+}`;
		const expected = `<blockquote>
<p>Quote with <ins class="markdown-insertion">insertion</ins></p>
</blockquote>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion with plus inside", () => {
		const input = `text {+plus + inside+}`;
		const expected = `<p>text <ins class="markdown-insertion">plus + inside</ins></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion at beginning of document", () => {
		const input = `{+Start+} of document.`;
		const expected = `<p><ins class="markdown-insertion">Start</ins> of document.</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion at end of document", () => {
		const input = `End of {+document+}`;
		const expected = `<p>End of <ins class="markdown-insertion">document</ins></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("multiple insertions in one line", () => {
		const input = `{+first+} and {+second+} and {+third+}`;
		const expected = `<p><ins class="markdown-insertion">first</ins> and <ins class="markdown-insertion">second</ins> and <ins class="markdown-insertion">third</ins></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion with starting emphasis", () => {
		const input = `{+inserted *text+} that shouldn't be bold*`;
		const expected = `<p><ins class="markdown-insertion">inserted *text</ins> that shouldn't be bold*</p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("insertion with ending emphasis", () => {
		const input = `*this text should be {+inserted but not bold*+}`;
		const expected = `<p>*this text should be <ins class="markdown-insertion">inserted but not bold*</ins></p>`;
		const doc = parse(input, extended);
		const html = renderHtml(doc, extended.renderers);
		expect(html.trim()).toBe(expected.trim());
	});
});
