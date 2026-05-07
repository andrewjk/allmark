import { renderHtmlSync } from "cmark-gfm";
import { describe, expect, test } from "vite-plus/test";

import core from "../src/rulesets/core";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

const options = { unsafe: true };

describe("html blocks", () => {
	test("HTML script tag - single line", () => {
		const input = `
<script>alert('hi');</script>
`;
		const expected = `
<script>alert('hi');</script>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML script tag - multi-line", () => {
		const input = `
<script>
alert('hi');
alert('bye');
</script>
`;
		const expected = `
<script>
alert('hi');
alert('bye');
</script>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML pre tag", () => {
		const input = `
<pre>code here</pre>
`;
		const expected = `
<pre>code here</pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML style tag", () => {
		const input = `
<style>body { color: red; }</style>
`;
		const expected = `
<style>body { color: red; }</style>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	// TODO:
	test.skip("HTML textarea tag", () => {
		const input = `
<textarea>Type here</textarea>
`;
		const expected = `
<p><textarea>Type here</textarea></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML comment - single line", () => {
		const input = `
<!-- This is a comment -->
`;
		const expected = `
<!-- This is a comment -->
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML comment - multi-line", () => {
		const input = `
<!--
This is a
multi-line comment
-->
`;
		const expected = `
<!--
This is a
multi-line comment
-->
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML processing instruction", () => {
		const input = `
<?php echo 'hello'; ?>
`;
		const expected = `
<?php echo 'hello'; ?>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML declaration - DOCTYPE", () => {
		const input = `
<!DOCTYPE html>
`;
		const expected = `
<!DOCTYPE html>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML CDATA section", () => {
		const input = `
<![CDATA[<greeting>Hello</greeting>]]>
`;
		const expected = `
<![CDATA[<greeting>Hello</greeting>]]>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block-level div tag", () => {
		const input = `
<div>Content</div>
`;
		const expected = `
<div>Content</div>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML div with blank line after", () => {
		const input = `
<div>Content</div>

Next paragraph
`;
		const expected = `
<div>Content</div>
<p>Next paragraph</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML paragraph tag", () => {
		const input = `
<p>HTML paragraph</p>
`;
		const expected = `
<p>HTML paragraph</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML heading tags", () => {
		const input = `
<h1>Heading</h1>
`;
		const expected = `
<h1>Heading</h1>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML list tags", () => {
		const input = `
<ul>
<li>Item</li>
</ul>
`;
		const expected = `
<ul>
<li>Item</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML table tag", () => {
		const input = `
<table><tr><td>Cell</td></tr></table>
`;
		const expected = `
<table><tr><td>Cell</td></tr></table>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block with indentation (less than 4 spaces)", () => {
		const input = `
   <div>Indented</div>
`;
		const expected = `
   <div>Indented</div>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block with 4 space indent should be code", () => {
		const input = `
    <div>Code</div>
`;
		const expected = `
<pre><code>&lt;div&gt;Code&lt;/div&gt;
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML closing tag alone", () => {
		const input = `
</div>
`;
		const expected = `
</div>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML self-closing tag", () => {
		const input = `
<br />
`;
		const expected = `
<br />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML img tag", () => {
		const input = `
<img src="image.jpg" alt="Image">
`;
		const expected = `
<img src="image.jpg" alt="Image">
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML hr tag", () => {
		const input = `
<hr />
`;
		const expected = `
<hr />
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block followed by Markdown", () => {
		const input = `
<div>HTML</div>

# Markdown Heading
`;
		const expected = `
<div>HTML</div>
<h1>Markdown Heading</h1>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("Paragraph before HTML block type 7 should not interrupt", () => {
		const input = `
Paragraph text
<span>inline</span>
`;
		const expected = `
<p>Paragraph text
<span>inline</span></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML comment in paragraph", () => {
		const input = `
Text <!-- comment --> more text
`;
		const expected = `
<p>Text <!-- comment --> more text</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("Multiple HTML blocks", () => {
		const input = `
<div>First</div>

<div>Second</div>
`;
		const expected = `
<div>First</div>
<div>Second</div>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block with attributes", () => {
		const input = `
<div class="container" id="main">Content</div>
`;
		const expected = `
<div class="container" id="main">Content</div>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML script tag with attributes", () => {
		const input = `
<script src="script.js" async></script>
`;
		const expected = `
<script src="script.js" async></script>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML form tag", () => {
		const input = `
<form action="/submit"> <input type="text"> </form>
`;
		const expected = `
<form action="/submit"> <input type="text"> </form>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML blockquote tag", () => {
		const input = `
<blockquote>Quote</blockquote>
`;
		const expected = `
<blockquote>Quote</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML address tag", () => {
		const input = `
<address>123 Main St</address>
`;
		const expected = `
<address>123 Main St</address>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML article tag", () => {
		const input = `
<article>Content</article>
`;
		const expected = `
<article>Content</article>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML aside tag", () => {
		const input = `
<aside>Sidebar</aside>
`;
		const expected = `
<aside>Sidebar</aside>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML section tag", () => {
		const input = `
<section>Section</section>
`;
		const expected = `
<section>Section</section>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML nav tag", () => {
		const input = `
<nav>Menu</nav>
`;
		const expected = `
<nav>Menu</nav>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML footer tag", () => {
		const input = `
<footer>Copyright</footer>
`;
		const expected = `
<footer>Copyright</footer>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML header tag", () => {
		const input = `
<header>Header</header>
`;
		const expected = `
<header>Header</header>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML main tag", () => {
		const input = `
<main>Main</main>
`;
		const expected = `
<main>Main</main>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML figure tag", () => {
		const input = `
<figure><img src="img.jpg"></figure>
`;
		const expected = `
<figure><img src="img.jpg"></figure>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML figcaption tag", () => {
		const input = `
<figcaption>Caption</figcaption>
`;
		const expected = `
<figcaption>Caption</figcaption>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML details and summary tags", () => {
		const input = `
<details><summary>Click</summary>Content</details>
`;
		const expected = `
<details><summary>Click</summary>Content</details>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML dialog tag", () => {
		const input = `
<dialog>Dialog content</dialog>
`;
		const expected = `
<dialog>Dialog content</dialog>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML fieldset tag", () => {
		const input = `
<fieldset>Field</fieldset>
`;
		const expected = `
<fieldset>Field</fieldset>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML legend tag", () => {
		const input = `
<legend>Legend</legend>
`;
		const expected = `
<legend>Legend</legend>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML dl, dt, dd tags", () => {
		const input = `
<dl><dt>Term</dt><dd>Definition</dd></dl>
`;
		const expected = `
<dl><dt>Term</dt><dd>Definition</dd></dl>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML link tag (empty)", () => {
		const input = `
<link rel="stylesheet" href="style.css">
`;
		const expected = `
<link rel="stylesheet" href="style.css">
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML base tag", () => {
		const input = `
<base href="https://example.com/">
`;
		const expected = `
<base href="https://example.com/">
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML basefont tag", () => {
		const input = `
<basefont face="Arial">
`;
		const expected = `
<basefont face="Arial">
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML center tag", () => {
		const input = `
<center>Centered</center>
`;
		const expected = `
<center>Centered</center>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML col and colgroup tags", () => {
		const input = `
<colgroup><col span="2"></colgroup>
`;
		const expected = `
<colgroup><col span="2"></colgroup>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML tbody, thead, tfoot, tr, th, td tags", () => {
		const input = `
<table><thead><tr><th>Header</th></tr></thead><tbody><tr><td>Data</td></tr></tbody></table>
`;
		const expected = `
<table><thead><tr><th>Header</th></tr></thead><tbody><tr><td>Data</td></tr></tbody></table>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML caption tag", () => {
		const input = `
<caption>Table caption</caption>
`;
		const expected = `
<caption>Table caption</caption>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML source tag", () => {
		const input = `
<source src="video.mp4" type="video/mp4">
`;
		const expected = `
<source src="video.mp4" type="video/mp4">
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML track tag", () => {
		const input = `
<track src="captions.vtt" kind="captions">
`;
		const expected = `
<track src="captions.vtt" kind="captions">
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML frameset, frame, noframes tags", () => {
		const input = `
<frameset><frame src="frame.html"></frameset>
`;
		const expected = `
<frameset><frame src="frame.html"></frameset>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML noframes tag", () => {
		const input = `
<noframes>No frames</noframes>
`;
		const expected = `
<noframes>No frames</noframes>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML iframe tag", () => {
		const input = `
<iframe src="page.html"></iframe>
`;
		const expected = `
<iframe src="page.html"></iframe>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML param tag", () => {
		const input = `
<param name="autoplay" value="true">
`;
		const expected = `
<param name="autoplay" value="true">
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML dir tag", () => {
		const input = `
<dir><li>Item</li></dir>
`;
		const expected = `
<dir><li>Item</li></dir>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML menu and menuitem tags", () => {
		const input = `
<menu><menuitem>Item</menuitem></menu>
`;
		const expected = `
<menu><menuitem>Item</menuitem></menu>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML optgroup and option tags", () => {
		const input = `
<select><optgroup label="Group"><option>Item</option></optgroup></select>
`;
		const expected = `
<p><select><optgroup label="Group"><option>Item</option></optgroup></select></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML title tag in body (not head)", () => {
		const input = `
<title>Page Title</title>
`;
		const expected = `
<title>Page Title</title>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block with line breaks inside", () => {
		const input = `
<div>
Line 1
Line 2
Line 3
</div>
`;
		const expected = `
<div>
Line 1
Line 2
Line 3
</div>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block inside list", () => {
		const input = `
- Item

  <div>HTML</div>
`;
		const expected = `
<ul>
<li>
<p>Item</p>
<div>HTML</div>
</li>
</ul>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block inside blockquote", () => {
		const input = `
> <div>HTML</div>
`;
		const expected = `
<blockquote>
<div>HTML</div>
</blockquote>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML case insensitive - uppercase", () => {
		const input = `
<DIV>Content</DIV>
`;
		const expected = `
<DIV>Content</DIV>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML script tag with mixed case", () => {
		const input = `
<SCRIPT>alert('hi');</SCRIPT>
`;
		const expected = `
<SCRIPT>alert('hi');</SCRIPT>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block with no content", () => {
		const input = `
<div></div>
`;
		const expected = `
<div></div>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML custom tag (non-block-level)", () => {
		const input = `
<custom-tag>Content</custom-tag>
`;
		const expected = `
<p><custom-tag>Content</custom-tag></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML comment ending on same line", () => {
		const input = `
<!-- comment -->
`;
		const expected = `
<!-- comment -->
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML comment with multiple dashes", () => {
		const input = `
<!-- --- comment --- -->
`;
		const expected = `
<!-- --- comment --- -->
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML CDATA with embedded brackets", () => {
		const input = `
<![CDATA[<test>data</test>]]>
`;
		const expected = `
<![CDATA[<test>data</test>]]>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML DOCTYPE with public identifier", () => {
		const input = `
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
`;
		const expected = `
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/html4/strict.dtd">
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("HTML block continues until blank line (type 6)", () => {
		const input = `
<div>Line 1
Line 2
Line 3

Next
`;
		const expected = `
<div>Line 1
Line 2
Line 3
<p>Next</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input, options));

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});
});
