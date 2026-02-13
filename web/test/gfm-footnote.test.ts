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

describe("footnote", () => {
	test("spec footnote", () => {
		const input = `
Here is a simple footnote[^1].

A footnote can also have multiple lines[^2].

[^1]: My reference.
[^2]: To add line breaks within a footnote, add 2 spaces to the end of a line.  
This is a second line.

`;
		const expected = renderHtmlSync(input, options);
		const doc = parse(input.substring(1, input.length - 1), gfm);
		const html = renderHtml(doc, gfm.renderers);
		expect(html.trim()).toBe(expected.trim());
	});

	test("simple footnote reference", () => {
		const input = `
Text with a footnote[^1].

[^1]: This is the footnote content.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("multiple footnote references", () => {
		const input = `
First reference[^1] and second[^2].

[^1]: First footnote.
[^2]: Second footnote.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote with inline formatting", () => {
		const input = `
Text[^1].

[^1]: Footnote with **bold** and *italic* text.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote with code", () => {
		const input = `
Code reference[^1].

[^1]: Footnote with \`inline code\`.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote with link", () => {
		const input = `
Link reference[^1].

[^1]: See [example](http://example.com).
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote reference not at definition", () => {
		const input = `
Unknown footnote[^99].
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote with multiline content", () => {
		const input = `
Multiline[^1].

[^1]: First line
    Second line
    Third line
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("repeated footnote reference", () => {
		const input = `
First[^1] and second[^1] use same footnote.

[^1]: Shared footnote content.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote in list", () => {
		const input = `
- Item with footnote[^1]
- Another item[^2]

[^1]: First footnote.
[^2]: Second footnote.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote in blockquote", () => {
		const input = `
> Quoted text with footnote[^1]

[^1]: Footnote for quote.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote with special characters in label", () => {
		const input = `
Special label[^a-b_c].

[^a-b_c]: Footnote with special label.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("case insensitive footnote labels", () => {
		const input = `
Mixed case[^ABC].

[^abc]: Should match.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote then list", () => {
		const input = `
Text[^1]

[^1]: Here is the content  
- and here is a list
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("title after footnote label", () => {
		const input = `
Text[^1]

[^1]: https://example.com test
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("link then footnote", () => {
		const input = `
Text[^1] [foo]

[foo]: https://example.com/foo
[^1]: https://example.com/1 test
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("footnote then link", () => {
		const input = `
Text[^1] [foo]

[^1]: https://example.com/1 test
[foo]: https://example.com/foo
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("swallow following brackets", () => {
		const input = `
[^1][asd]f]

[^1]: /footnote
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	// from https://github.com/commonmark/commonmark-java/issues/273#issuecomment-1292823856
	test("link reference takes precedence", () => {
		const input = `
[^1][foo]

[^1]: /footnote

[foo]: /url

`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.trim(), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("multiple paragraphs", () => {
		const input = `
Footnote 1 link[^first].

[^first]: Footnote **can have markup**

    and multiple paragraphs.
`;
		const expected = renderHtmlSync(input, options);

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});
});
