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

describe("tasklist", () => {
	test("spec tasklist", () => {
		const input = `
- [ ] foo
- [x] bar
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> foo</li>
<li><input type="checkbox" checked="" disabled="" /> bar</li>
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

	test("tasklist with asterisk marker", () => {
		const input = `
* [ ] unchecked
* [x] checked
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> unchecked</li>
<li><input type="checkbox" checked="" disabled="" /> checked</li>
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

	test("tasklist with plus marker", () => {
		const input = `
+ [ ] unchecked
+ [x] checked
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> unchecked</li>
<li><input type="checkbox" checked="" disabled="" /> checked</li>
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

	test("tasklist in ordered list", () => {
		const input = `
1. [ ] unchecked item
2. [x] checked item
`;
		const expected = `
<ol>
<li><input type="checkbox" disabled="" /> unchecked item</li>
<li><input type="checkbox" checked="" disabled="" /> checked item</li>
</ol>
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

	test("tasklist with inline formatting", () => {
		const input = `
- [ ] **bold** task
- [x] *italic* task
- [ ] ~~strikethrough~~ task
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> <strong>bold</strong> task</li>
<li><input type="checkbox" checked="" disabled="" /> <em>italic</em> task</li>
<li><input type="checkbox" disabled="" /> <del>strikethrough</del> task</li>
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

	test("tasklist with code", () => {
		const input = `
- [ ] task with \`code\`
- [x] another \`code\` task
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> task with <code>code</code></li>
<li><input type="checkbox" checked="" disabled="" /> another <code>code</code> task</li>
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

	test("tasklist with links", () => {
		const input = `
- [ ] task with [link](http://example.com)
- [x] checked [link](http://example.com) task
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> task with <a href="http://example.com">link</a></li>
<li><input type="checkbox" checked="" disabled="" /> checked <a href="http://example.com">link</a> task</li>
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

	test("nested tasklist", () => {
		const input = `
- [ ] parent task
  - [ ] child task 1
  - [x] child task 2
- [x] another parent
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> parent task
<ul>
<li><input type="checkbox" disabled="" /> child task 1</li>
<li><input type="checkbox" checked="" disabled="" /> child task 2</li>
</ul>
</li>
<li><input type="checkbox" checked="" disabled="" /> another parent</li>
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

	test("mixed tasks and regular items", () => {
		const input = `
- [ ] task item
- regular item
- [x] checked task
- another regular item
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> task item</li>
<li>regular item</li>
<li><input type="checkbox" checked="" disabled="" /> checked task</li>
<li>another regular item</li>
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

	test("tasklist with single character", () => {
		const input = `
- [ ] a
- [x] b
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> a</li>
<li><input type="checkbox" checked="" disabled="" /> b</li>
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

	test("tasklist with empty brackets", () => {
		const input = `
- [ ] 
- [x] 
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> </li>
<li><input type="checkbox" checked="" disabled="" /> </li>
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

	test("tasklist with uppercase X", () => {
		const input = `
- [ ] unchecked
- [X] checked with uppercase
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> unchecked</li>
<li><input type="checkbox" checked="" disabled="" /> checked with uppercase</li>
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

	test("tasklist in blockquote", () => {
		const input = `
> - [ ] quoted task
> - [x] checked quoted task
`;
		const expected = `
<blockquote>
<ul>
<li>[ ] quoted task</li>
<li>[x] checked quoted task</li>
</ul>
</blockquote>
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

	test("tasklist with multiple paragraphs", () => {
		const input = `
- [ ] task with paragraph

  continuation paragraph
- [x] another task
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> 
<p>task with paragraph</p>
<p>continuation paragraph</p>
</li>
<li><input type="checkbox" checked="" disabled="" /> 
<p>another task</p>
</li>
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

	test("tasklist with sublist", () => {
		const input = `
- [ ] task with sublist
  - subitem 1
  - subitem 2
- [x] checked task
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> task with sublist
<ul>
<li>subitem 1</li>
<li>subitem 2</li>
</ul>
</li>
<li><input type="checkbox" checked="" disabled="" /> checked task</li>
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

	test("tasklist with html entities", () => {
		const input = `
- [ ] task with &amp; entity
- [x] task with &lt;HTML&gt;
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> task with &amp; entity</li>
<li><input type="checkbox" checked="" disabled="" /> task with &lt;HTML&gt;</li>
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

	test("tasklist with various whitespace", () => {
		const input = `
- [ ]one
- [  ] two
- [ x] three
`;
		const expected = `
<ul>
<li>[ ]one</li>
<li>[  ] two</li>
<li>[ x] three</li>
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
});
