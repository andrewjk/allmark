import { renderHtmlSync } from "cmark-gfm";
import { describe, expect, test } from "vite-plus/test";

import gfm from "../src/rulesets/gfm";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

const options = {
	footnotes: true,
	extensions: {
		strikethrough: true,
		table: true,
		tasklist: true,
		autolink: true,
	},
};

describe("strikethrough", () => {
	test("spec strikethrough", () => {
		const input = `
~~Hi~~ Hello, world!
`;
		const expected = `
<p><del>Hi</del> Hello, world!</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough single word", () => {
		const input = `
~~deleted~~
`;
		const expected = `
<p><del>deleted</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough multiple words", () => {
		const input = `
~~this is deleted~~
`;
		const expected = `
<p><del>this is deleted</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough with spaces inside", () => {
		const input = `
~~  spaces  ~~
`;
		const expected = `
<p>~~  spaces  ~~</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough with emphasis", () => {
		const input = `
~~*bold and deleted*~~
`;
		const expected = `
<p><del><em>bold and deleted</em></del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough inside emphasis", () => {
		const input = `
*~~deleted in italic~~*
`;
		const expected = `
<p><em><del>deleted in italic</del></em></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough with code", () => {
		const input = `
~~code: \`var x\` here~~
`;
		const expected = `
<p><del>code: <code>var x</code> here</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough with link", () => {
		const input = `
~~[link text](http://example.com)~~
`;
		const expected = `
<p><del><a href="http://example.com">link text</a></del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("multiple strikethroughs in one line", () => {
		const input = `
~~first~~ and ~~second~~ and ~~third~~
`;
		const expected = `
<p><del>first</del> and <del>second</del> and <del>third</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough at start of paragraph", () => {
		const input = `
~~deleted~~ followed by normal text.
`;
		const expected = `
<p><del>deleted</del> followed by normal text.</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough at end of paragraph", () => {
		const input = `
Normal text followed by ~~deleted~~
`;
		const expected = `
<p>Normal text followed by <del>deleted</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough in list item", () => {
		const input = `
- ~~deleted item~~
- normal item
`;
		const expected = `
<ul>
<li><del>deleted item</del></li>
<li>normal item</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough with tildes inside", () => {
		const input = `
~~text with ~ tilde~~
`;
		const expected = `
<p><del>text with ~ tilde</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough with multiple tildes", () => {
		const input = `
~~~~double~~~~
`;
		const expected = `
<pre><code class="language-double~~~~"></code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough across lines", () => {
		const input = `
~~line one
line two~~
`;
		const expected = `
<p><del>line one
line two</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough with punctuation", () => {
		const input = `
~~Hello, world!~~
`;
		const expected = `
<p><del>Hello, world!</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough with numbers", () => {
		const input = `
~~12345~~
`;
		const expected = `
<p><del>12345</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough in table cell", () => {
		const input = `
| col1 | col2 |
| ---- | ---- |
| ~~deleted~~ | normal 
`;
		const expected = `
<table>
<thead>
<tr>
<th>col1</th>
<th>col2</th>
</tr>
</thead>
<tbody>
<tr>
<td><del>deleted</del></td>
<td>normal</td>
</tr>
</tbody>
</table>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough adjacent to regular text", () => {
		const input = `
normal~~deleted~~normal
`;
		const expected = `
<p>normal<del>deleted</del>normal</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("strikethrough with escaped characters", () => {
		const input = `
~~text with \\*asterisk\\*~~
`;
		const expected = `
<p><del>text with *asterisk*</del></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});
});
