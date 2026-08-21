import { describe, expect, test } from "vite-plus/test";

import extended from "../src/rulesets/extended";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

describe("subscript", () => {
	test("subscript single", () => {
		const input = `
This should be ~down~ below everything else.
`;
		const expected = `
<p>This should be <sub>down</sub> below everything else.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript double", () => {
		const input = `
This should be ~~down~~ below everything else.
`;
		const expected = `
<p>This should be <del>down</del> below everything else.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript triple", () => {
		const input = `
This should be ~~~down~~~ below everything else.
`;
		const expected = `
<p>This should be ~~~down~~~ below everything else.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript single character", () => {
		const input = `
H~2~O
`;
		const expected = `
<p>H<sub>2</sub>O</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript with numbers", () => {
		const input = `
x~1~ + x~2~
`;
		const expected = `
<p>x<sub>1</sub> + x<sub>2</sub></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("multiple subscripts in one line", () => {
		const input = `
a~i~ + b~j~ = c~k~
`;
		const expected = `
<p>a<sub>i</sub> + b<sub>j</sub> = c<sub>k</sub></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript at start of paragraph", () => {
		const input = `
~note~ This is important.
`;
		const expected = `
<p><sub>note</sub> This is important.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript at end of paragraph", () => {
		const input = `
See index~1~
`;
		const expected = `
<p>See index<sub>1</sub></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript with punctuation", () => {
		const input = `
Hello~world!~
`;
		const expected = `
<p>Hello<sub>world!</sub></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript with spaces", () => {
		const input = `
text ~with spaces~ more
`;
		const expected = `
<p>text <sub>with spaces</sub> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript with special characters", () => {
		const input = `
math~i+j~
`;
		const expected = `
<p>math<sub>i+j</sub></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript adjacent to text", () => {
		const input = `
test~ing~test
`;
		const expected = `
<p>test<sub>ing</sub>test</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("empty subscript", () => {
		const input = `
text~~text
`;
		const expected = `
<p>text~~text</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript with markdown inside", () => {
		const input = `
text ~**bold**~
`;
		const expected = `
<p>text <sub><strong>bold</strong></sub></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript with code inside", () => {
		const input = `
text ~\`code\`~
`;
		const expected = `
<p>text <sub><code>code</code></sub></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("escaped tilde should not be subscript", () => {
		const input = `
text \\~not subscript\\~
`;
		const expected = `
<p>text ~not subscript~</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("unmatched opening tilde", () => {
		const input = `
text ~not closed
`;
		const expected = `
<p>text ~not closed</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("unmatched closing tilde", () => {
		const input = `
text not opened~
`;
		const expected = `
<p>text not opened~</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript in list item", () => {
		const input = `
- Item with ~subscript~
`;
		const expected = `
<ul>
<li>Item with <sub>subscript</sub></li>
</ul>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript in blockquote", () => {
		const input = `
> Quote with ~subscript~
`;
		const expected = `
<blockquote>
<p>Quote with <sub>subscript</sub></p>
</blockquote>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough vs subscript precedence", () => {
		const input = `
This is ~~deleted~~ text.
`;
		const expected = `
<p>This is <del>deleted</del> text.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("subscript with tilde inside", () => {
		const input = `
text ~tilde ~ inside~
`;
		const expected = `
<p>text <sub>tilde ~ inside</sub></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough still works", () => {
		const input = `
text ~~struck~~, not subscripted
`;
		const expected = `
<p>text <del>struck</del>, not subscripted</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), extended, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), extended, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});
});
