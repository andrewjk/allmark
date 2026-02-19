import { renderHtmlSync } from "cmark-gfm";
import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import core from "../src/rulesets/core";

describe("fenced code", () => {
	test("Simple code fence with backticks", () => {
		const input = `
\`\`\`
code
\`\`\``.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Simple code fence with tildes", () => {
		const input = `
~~~
code
~~~`.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with 4 backticks", () => {
		const input = `
\`\`\`\`
code
\`\`\`\``.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with 5 tildes", () => {
		const input = `
~~~~~
code
~~~~~`.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with language specifier", () => {
		const input = `
\`\`\`javascript
const x = 1;
\`\`\``.substring(1);
		const expected = `
<pre><code class="language-javascript">const x = 1;
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with language specifier and extra text", () => {
		const input = `
\`\`\`javascript extra
const x = 1;
\`\`\``.substring(1);
		const expected = `
<pre><code class="language-javascript">const x = 1;
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with empty content", () => {
		const input = `
\`\`\`
\`\`\``.substring(1);
		const expected = `
<pre><code></code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with multi-line content", () => {
		const input = `
\`\`\`
line 1
line 2
line 3
\`\`\``.substring(1);
		const expected = `
<pre><code>line 1
line 2
line 3
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with 1 space indent", () => {
		const input = `
 \`\`\`
code
\`\`\``.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with 3 space indent", () => {
		const input = `
   \`\`\`
code
\`\`\``.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with 4 space indent should be code", () => {
		const input = `
    \`\`\`
code
\`\`\``.substring(1);
		const expected = `
<pre><code>\`\`\`
</code></pre>
<p>code</p>
<pre><code></code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence interrupts paragraph", () => {
		const input = `
Paragraph
\`\`\`
code
\`\`\``.substring(1);
		const expected = `
<p>Paragraph</p>
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence without space after opening", () => {
		const input = `
\`\`\`code
\`\`\``.substring(1);
		const expected = `
<pre><code class="language-code"></code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with blank line in content", () => {
		const input = `
\`\`\`
line 1

line 2
\`\`\``.substring(1);
		const expected = `
<pre><code>line 1

line 2
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence not valid - only 2 backticks", () => {
		const input = `
\`\`
code
\`\``.substring(1);
		const expected = `
<p><code>code</code></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence not valid - only 2 tildes", () => {
		const input = `
~~
code
~~`.substring(1);
		const expected = `
<p>~~
code
~~</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence not valid - mixed backticks and tildes", () => {
		const input = `
\`~\`
code
\`~\``.substring(1);
		const expected = `
<p><code>~</code>
code
<code>~</code></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence not valid - info string with backticks", () => {
		const input = `
\`\`\`code with backtick\`
code
\`\`\``.substring(1);
		const expected = `
<p>\`\`\`code with backtick\`
code</p>
<pre><code></code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with backticks in content", () => {
		const input = `
\`\`\`
code with \`backticks\`
\`\`\``.substring(1);
		const expected = `
<pre><code>code with \`backticks\`
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with tildes in content", () => {
		const input = `
~~~
code with ~tildes~
~~~`.substring(1);
		const expected = `
<pre><code>code with ~tildes~
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence preceded by paragraph without blank line", () => {
		const input = `
Paragraph
\`\`\`
code
\`\`\``.substring(1);
		const expected = `
<p>Paragraph</p>
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence followed by paragraph without blank line", () => {
		const input = `
\`\`\`
code
\`\`\`
Paragraph`.substring(1);
		const expected = `
<pre><code>code
</code></pre>
<p>Paragraph</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Multiple code fences", () => {
		const input = `
\`\`\`
code1
\`\`\`

\`\`\`
code2
\`\`\``.substring(1);
		const expected = `
<pre><code>code1
</code></pre>
<pre><code>code2
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with inline markdown in content", () => {
		const input = `
\`\`\`
*not italic*
**not bold**
\`\`\``.substring(1);
		const expected = `
<pre><code>*not italic*
**not bold**
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence at end of document", () => {
		const input = `
\`\`\`
code
\`\`\``.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence in blockquote", () => {
		const input = `
> \`\`\`
code
\`\`\``.substring(1);
		const expected = `
<blockquote>
<pre><code></code></pre>
</blockquote>
<p>code</p>
<pre><code></code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence in list item", () => {
		const input = `
- \`\`\`
code
\`\`\``.substring(1);
		const expected = `
<ul>
<li>
<pre><code></code></pre>
</li>
</ul>
<p>code</p>
<pre><code></code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with trailing spaces after opening", () => {
		const input = `
\`\`\`   
code
\`\`\``.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with trailing spaces after closing", () => {
		const input = `
\`\`\`
code
\`\`\`   `.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with very long opening", () => {
		const input = `
\`\`\`\`\`\`\`\`\`\`
code
\`\`\`\`\`\`\`\`\`\``.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with shorter closing", () => {
		const input = `
\`\`\`\`\`
code
\`\`\``.substring(1);
		const expected = `
<pre><code>code
\`\`\`
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence not valid - closing fence shorter than opening", () => {
		const input = `
\`\`\`
code
\`\``.substring(1);
		const expected = `
<pre><code>code
\`\`
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with language containing numbers", () => {
		const input = `
\`\`\`python3
import x
\`\`\``.substring(1);
		const expected = `
<pre><code class="language-python3">import x
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with language containing dashes", () => {
		const input = `
\`\`\`c++
int main() {}
\`\`\``.substring(1);
		const expected = `
<pre><code class="language-c++">int main() {}
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with trailing whitespace on closing fence", () => {
		const input = `
\`\`\`
code
\`\`\`   `.substring(1);
		const expected = `
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence between paragraphs", () => {
		const input = `
Paragraph 1

\`\`\`
code
\`\`\`

Paragraph 2`.substring(1);
		const expected = `
<p>Paragraph 1</p>
<pre><code>code
</code></pre>
<p>Paragraph 2</p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with backslash in info string", () => {
		const input = `
\`\`\`javascript\\test
code
\`\`\``.substring(1);
		const expected = `
<pre><code class="language-javascript\\test">code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with HTML entities in info", () => {
		const input = `
\`\`\`&lt;test&gt;
code
\`\`\``.substring(1);
		const expected = `
<pre><code class="language-&lt;test&gt;">code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with indented content lines", () => {
		const input = `
 \`\`\`
    indented
not indented
\`\`\``.substring(1);
		const expected = `
<pre><code>   indented
not indented
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence not valid - space between fence chars", () => {
		const input = `
\` \` \`
code
\` \` \``.substring(1);
		const expected = `
<p><code> </code> <code>code</code> <code> </code></p>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with only info string", () => {
		const input = `
\`\`\`javascript
\`\`\``.substring(1);
		const expected = `
<pre><code class="language-javascript"></code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with setext heading above", () => {
		const input = `
Heading
=======
\`\`\`
code
\`\`\``.substring(1);
		const expected = `
<h1>Heading</h1>
<pre><code>code
</code></pre>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});

	test("Code fence with ATX heading below", () => {
		const input = `
\`\`\`
code
\`\`\`
# Heading`.substring(1);
		const expected = `
<pre><code>code
</code></pre>
<h1>Heading</h1>
`.substring(1);
		expect(expected).toBe(renderHtmlSync(input));
		const doc = parse(input, core);
		const html = renderHtml(doc, core.renderers);
		expect(html).toBe(expected);
	});
});
