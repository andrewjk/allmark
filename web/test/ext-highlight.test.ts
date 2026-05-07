import { describe, expect, test } from "vite-plus/test";

import extended from "../src/rulesets/extended";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

describe("highlight", () => {
	test("highlight single", () => {
		const input = `
This should be =highlighted= as it is important.
`;
		const expected = `
<p>This should be <mark>highlighted</mark> as it is important.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight double", () => {
		const input = `
This should be ==highlighted== as it is important.
`;
		const expected = `
<p>This should be <mark>highlighted</mark> as it is important.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight triple", () => {
		const input = `
This should be ===highlighted=== as it is important.
`;
		const expected = `
<p>This should be ===highlighted=== as it is important.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight single character", () => {
		const input = `
text =a= more
`;
		const expected = `
<p>text <mark>a</mark> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("multiple highlights in one line", () => {
		const input = `
=first= and =second= and =third=
`;
		const expected = `
<p><mark>first</mark> and <mark>second</mark> and <mark>third</mark></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight at start of paragraph", () => {
		const input = `
=highlighted= This is important.
`;
		const expected = `
<p><mark>highlighted</mark> This is important.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight at end of paragraph", () => {
		const input = `
This is =highlighted=
`;
		const expected = `
<p>This is <mark>highlighted</mark></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight with punctuation", () => {
		const input = `
text =word!= more
`;
		const expected = `
<p>text <mark>word!</mark> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight with spaces", () => {
		const input = `
text =with spaces= more
`;
		const expected = `
<p>text <mark>with spaces</mark> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight with special characters", () => {
		const input = `
text =a+b= more
`;
		const expected = `
<p>text <mark>a+b</mark> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight adjacent to text", () => {
		const input = `
test=ing=test
`;
		const expected = `
<p>test<mark>ing</mark>test</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("empty highlight", () => {
		const input = `
text==text
`;
		const expected = `
<p>text==text</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight with markdown inside", () => {
		const input = `
text =**bold**=
`;
		const expected = `
<p>text <mark><strong>bold</strong></mark></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight with code inside", () => {
		const input = `
text =\`code\`=
`;
		const expected = `
<p>text <mark><code>code</code></mark></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("escaped equals should not be highlight", () => {
		const input = `
text \\=not highlight\\=
`;
		const expected = `
<p>text =not highlight=</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("unmatched opening equals", () => {
		const input = `
text =not closed
`;
		const expected = `
<p>text =not closed</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("unmatched closing equals", () => {
		const input = `
text not opened=
`;
		const expected = `
<p>text not opened=</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight in list item", () => {
		const input = `
- Item with =highlight=
`;
		const expected = `
<ul>
<li>Item with <mark>highlight</mark></li>
</ul>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight in blockquote", () => {
		const input = `
> Quote with =highlight=
`;
		const expected = `
<blockquote>
<p>Quote with <mark>highlight</mark></p>
</blockquote>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight with equals inside", () => {
		const input = `
text =equals = inside=
`;
		const expected = `
<p>text <mark>equals = inside</mark></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight at beginning of document", () => {
		const input = `
=Start= of document.
`;
		const expected = `
<p><mark>Start</mark> of document.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("highlight at end of document", () => {
		const input = `
End of =document=
`;
		const expected = `
<p>End of <mark>document</mark></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});
});
