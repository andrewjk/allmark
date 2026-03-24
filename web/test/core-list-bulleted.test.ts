import { renderHtmlSync } from "cmark-gfm";
import { describe, expect, test } from "vite-plus/test";

import parse from "../src/parse";
import render from "../src/render";
import core from "../src/rulesets/core";
import htmlRenderers from "../src/rulesets/htmlRenderers";

describe("bulleted lists", () => {
	test("Simple bulleted list with dashes", () => {
		const input = "- Item";
		const expected = `
<ul>
<li>Item</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Simple bulleted list with plus", () => {
		const input = "+ Item";
		const expected = `
<ul>
<li>Item</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Simple bulleted list with asterisks", () => {
		const input = "* Item";
		const expected = `
<ul>
<li>Item</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list with multiple items", () => {
		const input = `
- Item 1
- Item 2
- Item 3`.substring(1);
		const expected = `
<ul>
<li>Item 1</li>
<li>Item 2</li>
<li>Item 3</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Tight bulleted list", () => {
		const input = `
- Item 1
- Item 2`.substring(1);
		const expected = `
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Loose bulleted list with blank lines", () => {
		const input = `
- Item 1

- Item 2`.substring(1);
		const expected = `
<ul>
<li>
<p>Item 1</p>
</li>
<li>
<p>Item 2</p>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Nested bulleted lists", () => {
		const input = `
- Item 1
  - Nested item
- Item 2`.substring(1);
		const expected = `
<ul>
<li>Item 1
<ul>
<li>Nested item</li>
</ul>
</li>
<li>Item 2</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Deep nested bulleted lists", () => {
		const input = `
- Level 1
  - Level 2
    - Level 3`.substring(1);
		const expected = `
<ul>
<li>Level 1
<ul>
<li>Level 2
<ul>
<li>Level 3</li>
</ul>
</li>
</ul>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list in blockquote", () => {
		const input = `
> - Item 1
> - Item 2`.substring(1);
		const expected = `
<blockquote>
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Empty list item", () => {
		const input = "-";
		const expected = `
<ul>
<li></li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list with paragraphs", () => {
		const input = `
- Item 1

  Paragraph in item 1

- Item 2`.substring(1);
		const expected = `
<ul>
<li>
<p>Item 1</p>
<p>Paragraph in item 1</p>
</li>
<li>
<p>Item 2</p>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list preceded by paragraph", () => {
		const input = `
Paragraph

- Item`.substring(1);
		const expected = `
<p>Paragraph</p>
<ul>
<li>Item</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list followed by paragraph", () => {
		const input = `
- Item

Paragraph`.substring(1);
		const expected = `
<ul>
<li>Item</li>
</ul>
<p>Paragraph</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Mixed bullet markers should not be same list", () => {
		const input = `
- Item 1
+ Item 2`.substring(1);
		const expected = `
<ul>
<li>Item 1</li>
</ul>
<ul>
<li>Item 2</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list with code block", () => {
		const input = `
- Item

  \`\`\`
  code
  \`\`\``.substring(1);
		const expected = `
<ul>
<li>
<p>Item</p>
<pre><code>code
</code></pre>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list with inline formatting", () => {
		const input = "- Item with *emphasis*";
		const expected = `
<ul>
<li>Item with <em>emphasis</em></li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list with bold", () => {
		const input = "- Item with **bold**";
		const expected = `
<ul>
<li>Item with <strong>bold</strong></li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list item with multiple paragraphs (loose)", () => {
		const input = `
- Item 1

  Second paragraph

- Item 2`.substring(1);
		const expected = `
<ul>
<li>
<p>Item 1</p>
<p>Second paragraph</p>
</li>
<li>
<p>Item 2</p>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list with links", () => {
		const input = "- [Link](https://example.com)";
		const expected = `
<ul>
<li><a href="https://example.com">Link</a></li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list with code span", () => {
		const input = "- `inline code`";
		const expected = `
<ul>
<li><code>inline code</code></li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list at end of document", () => {
		const input = `
- Item 1
- Item 2`.substring(1);
		const expected = `
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Multiple separate bulleted lists", () => {
		const input = `
- List 1 item 1
- List 1 item 2

- List 2 item 1
- List 2 item 2`.substring(1);
		const expected = `
<ul>
<li>
<p>List 1 item 1</p>
</li>
<li>
<p>List 1 item 2</p>
</li>
<li>
<p>List 2 item 1</p>
</li>
<li>
<p>List 2 item 2</p>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list item with leading spaces (still a list)", () => {
		const input = "   - Item";
		const expected = `
<ul>
<li>Item</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list item with 4 spaces indent should be code", () => {
		const input = "    - Item";
		const expected = `
<pre><code>- Item
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list with only spaces after marker", () => {
		const input = "-    Item";
		const expected = `
<ul>
<li>Item</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Nested lists with different markers", () => {
		const input = `
- Dash
  + Plus
    * Star`.substring(1);
		const expected = `
<ul>
<li>Dash
<ul>
<li>Plus
<ul>
<li>Star</li>
</ul>
</li>
</ul>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list followed immediately by ordered list", () => {
		const input = `
- Item 1
- Item 2
1. Ordered 1
2. Ordered 2`.substring(1);
		const expected = `
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
<ol>
<li>Ordered 1</li>
<li>Ordered 2</li>
</ol>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Bulleted list with thematic break in item", () => {
		const input = `
- Item 1

  ---

- Item 2`.substring(1);
		const expected = `
<ul>
<li>
<p>Item 1</p>
<hr />
</li>
<li>
<p>Item 2</p>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});
});
