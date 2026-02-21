import { renderHtmlSync } from "cmark-gfm";
import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import core from "../src/rulesets/core";

describe("links", () => {
	test("basic inline link", () => {
		const input = `[Google](https://google.com)`;
		const expected = `<p><a href="https://google.com">Google</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with title", () => {
		const input = `[Google](https://google.com "Search Engine")`;
		const expected = `<p><a href="https://google.com" title="Search Engine">Google</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with single quoted title", () => {
		const input = `[Google](https://google.com 'Search Engine')`;
		const expected = `<p><a href="https://google.com" title="Search Engine">Google</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link in paragraph", () => {
		const input = `Visit [Google](https://google.com) for search.`;
		const expected = `<p>Visit <a href="https://google.com">Google</a> for search.</p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("multiple links in one line", () => {
		const input = `[Google](https://google.com) and [GitHub](https://github.com)`;
		const expected = `<p><a href="https://google.com">Google</a> and <a href="https://github.com">GitHub</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with emphasis", () => {
		const input = `[*Google*](https://google.com)`;
		const expected = `<p><a href="https://google.com"><em>Google</em></a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("emphasis around link", () => {
		const input = `*[Google](https://google.com)*`;
		const expected = `<p><em><a href="https://google.com">Google</a></em></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with code in text", () => {
		const input = `[\`const\`](https://example.com)`;
		const expected = `<p><a href="https://example.com"><code>const</code></a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link in list item", () => {
		const input = `- [Link](https://example.com)`;
		const expected = `<ul>
<li><a href="https://example.com">Link</a></li>
</ul>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link in heading", () => {
		const input = `# See [Google](https://google.com)`;
		const expected = `<h1>See <a href="https://google.com">Google</a></h1>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("reference link definition and usage", () => {
		const input = `[Google][google]

[google]: https://google.com`;
		const expected = `<p><a href="https://google.com">Google</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("reference link with implicit label", () => {
		const input = `[Google][]

[Google]: https://google.com`;
		const expected = `<p><a href="https://google.com">Google</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("reference link with title", () => {
		const input = `[Google][google]

[google]: https://google.com "Search Engine"`;
		const expected = `<p><a href="https://google.com" title="Search Engine">Google</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("multiple reference links", () => {
		const input = `[Google][google] and [GitHub][github]

[google]: https://google.com
[github]: https://github.com`;
		const expected = `<p><a href="https://google.com">Google</a> and <a href="https://github.com">GitHub</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("autolink with http", () => {
		const input = `<http://example.com>`;
		const expected = `<p><a href="http://example.com">http://example.com</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("autolink with https", () => {
		const input = `<https://example.com>`;
		const expected = `<p><a href="https://example.com">https://example.com</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("autolink with ftp", () => {
		const input = `<ftp://example.com>`;
		const expected = `<p><a href="ftp://example.com">ftp://example.com</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("email autolink", () => {
		const input = `<user@example.com>`;
		const expected = `<p><a href="mailto:user@example.com">user@example.com</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with special characters in URL", () => {
		const input = `[Link](https://example.com/path?query=value&other=123#anchor)`;
		const expected = `<p><a href="https://example.com/path?query=value&amp;other=123#anchor">Link</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with parentheses in URL", () => {
		const input = `[Link](https://example.com/path(with)parentheses)`;
		const expected = `<p><a href="https://example.com/path(with)parentheses">Link</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with spaces in title", () => {
		const input = `[Link](https://example.com "This is a title")`;
		const expected = `<p><a href="https://example.com" title="This is a title">Link</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with escaped brackets in text", () => {
		const input = `[\[link\]](https://example.com)`;
		const expected = `<p><a href="https://example.com">[link]</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("empty link text", () => {
		const input = `[](https://example.com)`;
		const expected = `<p><a href="https://example.com"></a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with underscore in URL", () => {
		const input = `[Link](https://example.com/path_with_underscore)`;
		const expected = `<p><a href="https://example.com/path_with_underscore">Link</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("relative URL", () => {
		const input = `[Link](/path/to/page)`;
		const expected = `<p><a href="/path/to/page">Link</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});

	test("link with percent encoding", () => {
		const input = `[Link](https://example.com/path%20with%20spaces)`;
		const expected = `<p><a href="https://example.com/path%20with%20spaces">Link</a></p>
`;
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc);
		expect(html).toBe(expected);
	});
});
