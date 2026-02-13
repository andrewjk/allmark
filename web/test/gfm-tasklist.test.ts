import { renderHtmlSync } from "cmark-gfm";
import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import gfm from "../src/rulesets/gfm";

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
		const doc = parse(input.substring(1, input.length - 1), gfm);
		const html = renderHtml(doc, gfm.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("tasklist with asterisk marker", () => {
		const input = `

* [ ] unchecked
* [x] checked

`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with plus marker", () => {
		const input = `

+ [ ] unchecked
+ [x] checked

`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist in ordered list", () => {
		const input = `
1. [ ] unchecked item
2. [x] checked item
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with inline formatting", () => {
		const input = `
- [ ] **bold** task
- [x] *italic* task
- [ ] ~~strikethrough~~ task
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with code", () => {
		const input = `
- [ ] task with \`code\`
- [x] another \`code\` task
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with links", () => {
		const input = `
- [ ] task with [link](http://example.com)
- [x] checked [link](http://example.com) task
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("nested tasklist", () => {
		const input = `
- [ ] parent task
  - [ ] child task 1
  - [x] child task 2
- [x] another parent
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("mixed tasks and regular items", () => {
		const input = `
- [ ] task item
- regular item
- [x] checked task
- another regular item
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with single character", () => {
		const input = `
- [ ] a
- [x] b
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with empty brackets", () => {
		const input = `
- [ ] 
- [x] 
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with uppercase X", () => {
		const input = `
- [ ] unchecked
- [X] checked with uppercase
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist in blockquote", () => {
		const input = `
> - [ ] quoted task
> - [x] checked quoted task
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with multiple paragraphs", () => {
		const input = `
- [ ] task with paragraph

  continuation paragraph
- [x] another task
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with sublist", () => {
		const input = `
- [ ] task with sublist
  - subitem 1
  - subitem 2
- [x] checked task
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with html entities", () => {
		const input = `
- [ ] task with &amp; entity
- [x] task with &lt;HTML&gt;
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("tasklist with various whitespace", () => {
		const input = `
- [ ]one
- [  ] two
- [ x] three
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});
});
