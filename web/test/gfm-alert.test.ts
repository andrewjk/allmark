import { describe, expect, test } from "vite-plus/test";

import gfm from "../src/rulesets/gfm";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

describe("alert", () => {
	test("spec alert", () => {
		const input = `
> [!NOTE]
> Useful information that users should know, even when skimming content.
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>Useful information that users should know, even when skimming content.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert tip", () => {
		const input = `
> [!TIP]
> Helpful advice for doing things better or more easily.
`;
		const expected = `
<div class="markdown-alert markdown-alert-tip">
<p class="markdown-alert-title">Tip</p>
<p>Helpful advice for doing things better or more easily.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert important", () => {
		const input = `
> [!IMPORTANT]
> Key information users need to know to achieve their goal.
`;
		const expected = `
<div class="markdown-alert markdown-alert-important">
<p class="markdown-alert-title">Important</p>
<p>Key information users need to know to achieve their goal.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert warning", () => {
		const input = `
> [!WARNING]
> Urgent info that needs immediate user attention to avoid problems.
`;
		const expected = `
<div class="markdown-alert markdown-alert-warning">
<p class="markdown-alert-title">Warning</p>
<p>Urgent info that needs immediate user attention to avoid problems.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert caution", () => {
		const input = `
> [!CAUTION]
> Advises about risks or negative outcomes of certain actions.
`;
		const expected = `
<div class="markdown-alert markdown-alert-caution">
<p class="markdown-alert-title">Caution</p>
<p>Advises about risks or negative outcomes of certain actions.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert with multiple paragraphs", () => {
		const input = `
> [!NOTE]
> First paragraph of the note.
>
> Second paragraph of the note.
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>First paragraph of the note.</p>
<p>Second paragraph of the note.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert with inline formatting", () => {
		const input = `
> [!NOTE]
> This is **bold** and this is *italic* and this is \`code\`.
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>This is <strong>bold</strong> and this is <em>italic</em> and this is <code>code</code>.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert with list", () => {
		const input = `
> [!NOTE]
> Some important points:
> - First point
> - Second point
> - Third point
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>Some important points:</p>
<ul>
<li>First point</li>
<li>Second point</li>
<li>Third point</li>
</ul>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert with code block", () => {
		const input = `
> [!NOTE]
> Example code:
>
> \`\`\`
> console.log("Hello World");
> \`\`\`
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>Example code:</p>
<pre><code>console.log(&quot;Hello World&quot;);
</code></pre>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert with link", () => {
		const input = `
> [!NOTE]
> Check out the [documentation](https://example.com) for more info.
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>Check out the <a href="https://example.com">documentation</a> for more info.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert case insensitive", () => {
		const input = `
> [!note]
> This should work with lowercase.
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>This should work with lowercase.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("non alert blockquote", () => {
		const input = `
> This is just a regular blockquote.
> It should not be treated as an alert.
`;
		const expected = `
<blockquote>
<p>This is just a regular blockquote.
It should not be treated as an alert.</p>
</blockquote>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("blockquote with brackets but not alert", () => {
		const input = `
> [NOTE] This is not an alert syntax.
> It should be a regular blockquote.
`;
		const expected = `
<blockquote>
<p>[NOTE] This is not an alert syntax.
It should be a regular blockquote.</p>
</blockquote>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert with nested blockquote", () => {
		const input = `
> [!NOTE]
> Outer alert content.
>
> > Nested blockquote inside alert.
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>Outer alert content.</p>
<blockquote>
<p>Nested blockquote inside alert.</p>
</blockquote>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("consecutive alerts", () => {
		const input = `
> [!NOTE]
> First alert.

> [!WARNING]
> Second alert.
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>First alert.</p>
</div>
<div class="markdown-alert markdown-alert-warning">
<p class="markdown-alert-title">Warning</p>
<p>Second alert.</p>
</div>
`.substring(1);

		const htmlSpaced = transform(input, gfm, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), gfm, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), gfm, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);

		const htmlCr = transform(input.replaceAll("\n", "\r"), gfm, htmlRenderers);
		expect(htmlCr.replaceAll("\r", "\n")).toBe(expected);
	});

	test("alert with empty content", () => {
		const input = `
> [!NOTE]
>
> Content after empty line.
`;
		const expected = `
<div class="markdown-alert markdown-alert-note">
<p class="markdown-alert-title">Note</p>
<p>Content after empty line.</p>
</div>
`.substring(1);

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
