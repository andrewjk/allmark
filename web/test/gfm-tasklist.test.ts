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
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with asterisk marker", () => {
		const input = `
* [ ] unchecked
* [x] checked
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with plus marker", () => {
		const input = `
+ [ ] unchecked
+ [x] checked
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist in ordered list", () => {
		const input = `
1. [ ] unchecked item
2. [x] checked item
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with inline formatting", () => {
		const input = `
- [ ] **bold** task
- [x] *italic* task
- [ ] ~~strikethrough~~ task
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with code", () => {
		const input = `
- [ ] task with \`code\`
- [x] another \`code\` task
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with links", () => {
		const input = `
- [ ] task with [link](http://example.com)
- [x] checked [link](http://example.com) task
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("nested tasklist", () => {
		const input = `
- [ ] parent task
  - [ ] child task 1
  - [x] child task 2
- [x] another parent
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("mixed tasks and regular items", () => {
		const input = `
- [ ] task item
- regular item
- [x] checked task
- another regular item
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with single character", () => {
		const input = `
- [ ] a
- [x] b
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with empty brackets", () => {
		const input = `
- [ ] 
- [x] 
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with uppercase X", () => {
		const input = `
- [ ] unchecked
- [X] checked with uppercase
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist in blockquote", () => {
		const input = `
> - [ ] quoted task
> - [x] checked quoted task
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with multiple paragraphs", () => {
		const input = `
- [ ] task with paragraph

  continuation paragraph
- [x] another task
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with sublist", () => {
		const input = `
- [ ] task with sublist
  - subitem 1
  - subitem 2
- [x] checked task
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with html entities", () => {
		const input = `
- [ ] task with &amp; entity
- [x] task with &lt;HTML&gt;
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("tasklist with various whitespace", () => {
		const input = `
- [ ]one
- [  ] two
- [ x] three
`;
		const expected = renderHtmlSync(input, options);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});
});
