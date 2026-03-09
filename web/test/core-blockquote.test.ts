import { renderHtmlSync } from "cmark-gfm";
import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import render from "../src/render";
import core from "../src/rulesets/core";
import htmlRenderers from "../src/rulesets/htmlRenderers";

describe("blockquotes", () => {
	test("Simple blockquote", () => {
		const input = "> Simple quote";
		const expected = `
<blockquote>
<p>Simple quote</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with multiple lines", () => {
		const input = `
> Line 1
> Line 2
> Line 3`.substring(1);
		const expected = `
<blockquote>
<p>Line 1
Line 2
Line 3</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with lazy continuation", () => {
		const input = `
> Line 1
Line 2
> Line 3`.substring(1);
		const expected = `
<blockquote>
<p>Line 1
Line 2
Line 3</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with space after >", () => {
		const input = "> With space";
		const expected = `
<blockquote>
<p>With space</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote without space after >", () => {
		const input = ">Without space";
		const expected = `
<blockquote>
<p>Without space</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with multiple paragraphs", () => {
		const input = `
> Paragraph 1
>
> Paragraph 2`.substring(1);
		const expected = `
<blockquote>
<p>Paragraph 1</p>
<p>Paragraph 2</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with code block", () => {
		const input = `
>     code block
>     more code`.substring(1);
		const expected = `
<blockquote>
<pre><code>code block
more code
</code></pre>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with list", () => {
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

	test("Blockquote with nested blockquote", () => {
		const input = `
> Outer
>> Inner
>>> Innerer`.substring(1);
		const expected = `
<blockquote>
<p>Outer</p>
<blockquote>
<p>Inner</p>
<blockquote>
<p>Innerer</p>
</blockquote>
</blockquote>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with heading", () => {
		const input = "> # Heading";
		const expected = `
<blockquote>
<h1>Heading</h1>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with inline emphasis", () => {
		const input = "> *italic* and **bold**";
		const expected = `
<blockquote>
<p><em>italic</em> and <strong>bold</strong></p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with inline code", () => {
		const input = "> \`code\` inside quote";
		const expected = `
<blockquote>
<p><code>code</code> inside quote</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with link", () => {
		const input = "> [link](https://example.com)";
		const expected = `
<blockquote>
<p><a href="https://example.com">link</a></p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with 1 space indent", () => {
		const input = " > Indented quote";
		const expected = `
<blockquote>
<p>Indented quote</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with 3 space indent", () => {
		const input = "   > Indented quote";
		const expected = `
<blockquote>
<p>Indented quote</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with 4 space indent should be code", () => {
		const input = "    > Not a quote";
		const expected = `
<pre><code>&gt; Not a quote
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Multiple consecutive blockquotes", () => {
		const input = `
> Quote 1

> Quote 2`.substring(1);
		const expected = `
<blockquote>
<p>Quote 1</p>
</blockquote>
<blockquote>
<p>Quote 2</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote preceded by paragraph without blank line", () => {
		const input = `
Paragraph
> Quote`.substring(1);
		const expected = `
<p>Paragraph</p>
<blockquote>
<p>Quote</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with thematic break", () => {
		const input = `
> Text
>
> ---`.substring(1);
		const expected = `
<blockquote>
<p>Text</p>
<hr />
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with multiple blocks", () => {
		const input = `
> Paragraph
>
> - List item
>
> Code:
>     code`.substring(1);
		const expected = `
<blockquote>
<p>Paragraph</p>
<ul>
<li>List item</li>
</ul>
<p>Code:
code</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with complex nested content", () => {
		const input = `
> Quote
>
>> Nested quote
>>
>> - List in nested
> Back to outer`.substring(1);
		const expected = `
<blockquote>
<p>Quote</p>
<blockquote>
<p>Nested quote</p>
<ul>
<li>List in nested
Back to outer</li>
</ul>
</blockquote>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Empty blockquote", () => {
		const input = ">";
		const expected = `
<blockquote>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with only space", () => {
		const input = "> ";
		const expected = `
<blockquote>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote at end of document", () => {
		const input = "> Last quote";
		const expected = `
<blockquote>
<p>Last quote</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with fenced code block", () => {
		const input = `
> \`\`\`
> code
> \`\`\``.substring(1);
		const expected = `
<blockquote>
<pre><code>code
</code></pre>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with ordered list", () => {
		const input = `
> 1. First
> 2. Second`.substring(1);
		const expected = `
<blockquote>
<ol>
<li>First</li>
<li>Second</li>
</ol>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with setext heading", () => {
		const input = `
> Heading
> =======`.substring(1);
		const expected = `
<blockquote>
<h1>Heading</h1>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with HTML block", () => {
		const input = "> <div>HTML</div>";
		const expected = `
<blockquote>
<div>HTML</div>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, { unsafe: true }));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with hard line breaks", () => {
		const input = `
> Line 1  
> Line 2`.substring(1);
		const expected = `
<blockquote>
<p>Line 1<br />
Line 2</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with image", () => {
		const input = "> ![alt](image.png)";
		const expected = `
<blockquote>
<p><img src="image.png" alt="alt" /></p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Deeply nested blockquotes", () => {
		const input = `
> Level 1
>> Level 2
>>> Level 3
>>>> Level 4`.substring(1);
		const expected = `
<blockquote>
<p>Level 1</p>
<blockquote>
<p>Level 2</p>
<blockquote>
<p>Level 3</p>
<blockquote>
<p>Level 4</p>
</blockquote>
</blockquote>
</blockquote>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with mixed lazy continuation", () => {
		const input = `
> Line 1
> Line 2
Line 3 (lazy)
> Line 4`.substring(1);
		const expected = `
<blockquote>
<p>Line 1
Line 2
Line 3 (lazy)
Line 4</p>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with loose list", () => {
		const input = `
> - Item 1
>
> - Item 2`.substring(1);
		const expected = `
<blockquote>
<ul>
<li>
<p>Item 1</p>
</li>
<li>
<p>Item 2</p>
</li>
</ul>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = render(doc, htmlRenderers);
		expect(html).toBe(expected);
	});

	test("Blockquote with tight list", () => {
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
});
