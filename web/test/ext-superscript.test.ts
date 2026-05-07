import { describe, expect, test } from "vite-plus/test";

import extended from "../src/rulesets/extended";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

describe("superscript", () => {
	test("superscript single", () => {
		const input = `
This should be ^up^ above everything else.
`;
		const expected = `
<p>This should be <sup>up</sup> above everything else.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript double", () => {
		const input = `
This should be ^^up^^ above everything else.
`;
		const expected = `
<p>This should be <sup>up</sup> above everything else.</p>
`.substring(1);
		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript triple", () => {
		const input = `
This should be ^^^up^^^ above everything else.
`;
		const expected = `
<p>This should be ^^^up^^^ above everything else.</p>
`.substring(1);
		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript single character", () => {
		const input = `
x^2^
`;
		const expected = `
<p>x<sup>2</sup></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript with numbers", () => {
		const input = `
E=mc^2^
`;
		const expected = `
<p>E=mc<sup>2</sup></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("multiple superscripts in one line", () => {
		const input = `
x^2^ + y^2^ = z^2^
`;
		const expected = `
<p>x<sup>2</sup> + y<sup>2</sup> = z<sup>2</sup></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript at start of paragraph", () => {
		const input = `
^note^ This is important.
`;
		const expected = `
<p><sup>note</sup> This is important.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript at end of paragraph", () => {
		const input = `
See footnote^1^
`;
		const expected = `
<p>See footnote<sup>1</sup></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript with punctuation", () => {
		const input = `
Hello^world!^
`;
		const expected = `
<p>Hello<sup>world!</sup></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript with spaces", () => {
		const input = `
text ^with spaces^ more
`;
		const expected = `
<p>text <sup>with spaces</sup> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript with special characters", () => {
		const input = `
math^2+3^
`;
		const expected = `
<p>math<sup>2+3</sup></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript adjacent to text", () => {
		const input = `
test^ing^test
`;
		const expected = `
<p>test<sup>ing</sup>test</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("empty superscript", () => {
		const input = `
text^^text
`;
		const expected = `
<p>text^^text</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript with markdown inside", () => {
		const input = `
text ^**bold**^
`;
		const expected = `
<p>text <sup><strong>bold</strong></sup></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript with code inside", () => {
		const input = `
text ^\`code\`^
`;
		const expected = `
<p>text <sup><code>code</code></sup></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("escaped caret should not be superscript", () => {
		const input = `
text \\^not superscript\\^
`;
		const expected = `
<p>text ^not superscript^</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("unmatched opening caret", () => {
		const input = `
text ^not closed
`;
		const expected = `
<p>text ^not closed</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("unmatched closing caret", () => {
		const input = `
text not opened^
`;
		const expected = `
<p>text not opened^</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript in list item", () => {
		const input = `
- Item with ^superscript^
`;
		const expected = `
<ul>
<li>Item with <sup>superscript</sup></li>
</ul>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript in blockquote", () => {
		const input = `
> Quote with ^superscript^
`;
		const expected = `
<blockquote>
<p>Quote with <sup>superscript</sup></p>
</blockquote>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("nested superscript", () => {
		const input = `
x^y^z^
`;
		// The first pair of carets creates a superscript, leaving ^z^ as text
		const expected = `
<p>x<sup>y</sup>z^</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("superscript with caret inside", () => {
		const input = `
text ^caret ^ inside^
`;
		const expected = `
<p>text <sup>caret ^ inside</sup></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});
});
