import { renderHtmlSync } from "cmark-gfm";
import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import render from "../src/render";
import core from "../src/rulesets/core";
import htmlRenderers from "../src/rulesets/htmlRenderers";

describe("thematic breaks", () => {
	test("Simple thematic break with dashes", () => {
		const input = "---";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Simple thematic break with asterisks", () => {
		const input = "***";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Simple thematic break with underscores", () => {
		const input = "___";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with 4 dashes", () => {
		const input = "----";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with 5 asterisks", () => {
		const input = "*****";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with spaces between characters", () => {
		const input = "- - -";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with tabs between characters", () => {
		const input = "*\t*\t*";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with 1 space indent", () => {
		const input = " ---";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with 3 space indent", () => {
		const input = "   ---";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with 4 space indent should be code", () => {
		const input = "    ---";
		const expected = `
<pre><code>---
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break followed by paragraph without blank line", () => {
		const input = `
---
Paragraph`.substring(1);
		const expected = `
<hr />
<p>Paragraph</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Multiple thematic breaks", () => {
		const input = `
---

***`.substring(1);
		const expected = `
<hr />
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break not valid - only 2 dashes", () => {
		const input = "--";
		const expected = `
<p>--</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break not valid - only 2 asterisks", () => {
		const input = "**";
		const expected = `
<p>**</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break not valid - only 2 underscores", () => {
		const input = "__";
		const expected = `
<p>__</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break not valid - mixed characters", () => {
		const input = "-*-";
		const expected = `
<p>-*-</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break not valid - mixed dashes and asterisks", () => {
		const input = "---***";
		const expected = `
<p>---***</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break in blockquote", () => {
		const input = "> ---";
		const expected = `
<blockquote>
<hr />
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break in list item", () => {
		const input = `
- Item
---`.substring(1);
		const expected = `
<ul>
<li>Item</li>
</ul>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with trailing spaces", () => {
		const input = "---   ";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with trailing tabs", () => {
		const input = "***\t\t";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break after list without blank line", () => {
		const input = `
- Item 1
- Item 2
---`.substring(1);
		const expected = `
<ul>
<li>Item 1</li>
<li>Item 2</li>
</ul>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break before list without blank line", () => {
		const input = `
---
- Item`.substring(1);
		const expected = `
<hr />
<ul>
<li>Item</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break at end of document", () => {
		const input = `
> Quote
---`.substring(1);
		const expected = `
<blockquote>
<p>Quote</p>
</blockquote>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break between paragraphs", () => {
		const input = `
Paragraph 1

---

Paragraph 2`.substring(1);
		const expected = `
<p>Paragraph 1</p>
<hr />
<p>Paragraph 2</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break between paragraphs without blank lines", () => {
		const input = `
Paragraph 1

---
Paragraph 2`.substring(1);
		const expected = `
<p>Paragraph 1</p>
<hr />
<p>Paragraph 2</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break after heading", () => {
		const input = `
# Heading
---`.substring(1);
		const expected = `
<h1>Heading</h1>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break before heading", () => {
		const input = `
---
# Heading`.substring(1);
		const expected = `
<hr />
<h1>Heading</h1>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with code block above", () => {
		const input = `
\`\`\`
code
\`\`\`
---`.substring(1);
		const expected = `
<pre><code>code
</code></pre>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with code block below", () => {
		const input = `
---
\`\`\`
code
\`\`\``.substring(1);
		const expected = `
<hr />
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break in nested blockquote", () => {
		const input = `
> Quote
>
> ---
> More quote`.substring(1);
		const expected = `
<blockquote>
<p>Quote</p>
<hr />
<p>More quote</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with very long sequence", () => {
		const input = "--------------------------------------------------";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break not valid - starts with dash but has spaces", () => {
		const input = "-   -   -";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with inline elements above", () => {
		const input = `
Text with *emphasis*

---`.substring(1);
		const expected = `
<p>Text with <em>emphasis</em></p>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with inline elements below", () => {
		const input = `
---
Text with **bold**`.substring(1);
		const expected = `
<hr />
<p>Text with <strong>bold</strong></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break between blockquotes", () => {
		const input = `
> Quote 1

---

> Quote 2`.substring(1);
		const expected = `
<blockquote>
<p>Quote 1</p>
</blockquote>
<hr />
<blockquote>
<p>Quote 2</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with setext heading", () => {
		const input = `
Heading
=======
---`.substring(1);
		const expected = `
<h1>Heading</h1>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break after ordered list", () => {
		const input = `
1. First
2. Second
---`.substring(1);
		const expected = `
<ol>
<li>First</li>
<li>Second</li>
</ol>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Empty thematic break (should not match)", () => {
		const input = "";
		const expected = "";
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Text that looks like thematic break but has other content", () => {
		const input = "--- text";
		const expected = `
<p>--- text</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break preceded by code fence", () => {
		const input = `
\`\`\`
code
\`\`\`
---`.substring(1);
		const expected = `
<pre><code>code
</code></pre>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break not valid - less than 3 chars with spaces", () => {
		const input = "- -";
		const expected = `
<ul>
<li>
<ul>
<li></li>
</ul>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break after loose list", () => {
		const input = `
- Item 1

- Item 2
---`.substring(1);
		const expected = `
<ul>
<li>
<p>Item 1</p>
</li>
<li>
<p>Item 2</p>
</li>
</ul>
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break in fenced code block (should not be interpreted)", () => {
		const input = `
\`\`\`
---
\`\`\``.substring(1);
		const expected = `
<pre><code>---
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break with mixed spacing", () => {
		const input = "  *  *  *  ";
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Thematic break not valid - text after spaces", () => {
		const input = "---   text";
		const expected = `
<p>---   text</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});
});
