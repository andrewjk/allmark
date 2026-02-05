import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import core from "../src/rules/core";

describe("spec-cm", () => {
	test("Example 1, line 355: '→foo→baz→→bim'", () => {
		const input = `
	foo	baz		bim
`;
		const expected = `
<pre><code>foo	baz		bim
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 2, line 362: '  →foo→baz→→bim'", () => {
		const input = `
  	foo	baz		bim
`;
		const expected = `
<pre><code>foo	baz		bim
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 3, line 369: '    a→a\\n    ὐ→a'", () => {
		const input = `
    a	a
    ὐ	a
`;
		const expected = `
<pre><code>a	a
ὐ	a
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 4, line 382: '  - foo\\n\\n→bar'", () => {
		const input = `
  - foo

	bar
`;
		const expected = `
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 5, line 395: '- foo\\n\\n→→bar'", () => {
		const input = `
- foo

		bar
`;
		const expected = `
<ul>
<li>
<p>foo</p>
<pre><code>  bar
</code></pre>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 6, line 418: '>→→foo'", () => {
		const input = `
>		foo
`;
		const expected = `
<blockquote>
<pre><code>  foo
</code></pre>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 7, line 427: '-→→foo'", () => {
		const input = `
-		foo
`;
		const expected = `
<ul>
<li>
<pre><code>  foo
</code></pre>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 8, line 439: '    foo\\n→bar'", () => {
		const input = `
    foo
	bar
`;
		const expected = `
<pre><code>foo
bar
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 9, line 448: ' - foo\\n   - bar\\n→ - baz'", () => {
		const input = `
 - foo
   - bar
	 - baz
`;
		const expected = `
<ul>
<li>foo
<ul>
<li>bar
<ul>
<li>baz</li>
</ul>
</li>
</ul>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 10, line 466: '#→Foo'", () => {
		const input = `
#	Foo
`;
		const expected = `
<h1>Foo</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 11, line 472: '*→*→*→'", () => {
		const input = `
*	*	*	
`;
		const expected = `
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 12, line 489: '\\!\\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\-\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\\\\\]\\^\\_\\`\\{\\|\\}\\~'", () => {
		const input = `
\\!\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\-\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\\\\\]\\^\\_\\\`\\{\\|\\}\\~
`;
		const expected = `
<p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\\]^_\`{|}~</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 13, line 499: '\\→\\A\\a\\ \\3\\φ\\«'", () => {
		const input = `
\\	\\A\\a\\ \\3\\φ\\«
`;
		const expected = `
<p>\\	\\A\\a\\ \\3\\φ\\«</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 14, line 509: '\\*not emphasized*\\n\\<br/> not a tag\\n\\[not a link](/foo)\\n\\`not code`\\n1\\. not a list\\n\\* not a list\\n\\# not a heading\\n\\[foo]: /url \"not a reference\"\\n\\&ouml; not a character entity'", () => {
		const input = `
\\*not emphasized*
\\<br/> not a tag
\\[not a link](/foo)
\\\`not code\`
1\\. not a list
\\* not a list
\\# not a heading
\\[foo]: /url "not a reference"
\\&ouml; not a character entity
`;
		const expected = `
<p>*not emphasized*
&lt;br/&gt; not a tag
[not a link](/foo)
\`not code\`
1. not a list
* not a list
# not a heading
[foo]: /url &quot;not a reference&quot;
&amp;ouml; not a character entity</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 15, line 534: '\\\\*emphasis*'", () => {
		const input = `
\\\\*emphasis*
`;
		const expected = `
<p>\\<em>emphasis</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 16, line 543: 'foo\\\\nbar'", () => {
		const input = `
foo\\
bar
`;
		const expected = `
<p>foo<br />
bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 17, line 555: '`` \\[\\` ``'", () => {
		const input = `
\`\` \\[\\\` \`\`
`;
		const expected = `
<p><code>\\[\\\`</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 18, line 562: '    \\[\\]'", () => {
		const input = `
    \\[\\]
`;
		const expected = `
<pre><code>\\[\\]
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 19, line 570: '~~~\\n\\[\\]\\n~~~'", () => {
		const input = `
~~~
\\[\\]
~~~
`;
		const expected = `
<pre><code>\\[\\]
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 20, line 580: '<https://example.com?find=\\*>'", () => {
		const input = `
<https://example.com?find=\\*>
`;
		const expected = `
<p><a href="https://example.com?find=%5C*">https://example.com?find=\\*</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 21, line 587: '<a href=\"/bar\\/)\">'", () => {
		const input = `
<a href="/bar\\/)">
`;
		const expected = `
<a href="/bar\\/)">
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 22, line 597: '[foo](/bar\\* \"ti\\*tle\")'", () => {
		const input = `
[foo](/bar\\* "ti\\*tle")
`;
		const expected = `
<p><a href="/bar*" title="ti*tle">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 23, line 604: '[foo]\\n\\n[foo]: /bar\\* \"ti\\*tle\"'", () => {
		const input = `
[foo]

[foo]: /bar\\* "ti\\*tle"
`;
		const expected = `
<p><a href="/bar*" title="ti*tle">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 24, line 613: '``` foo\\+bar\\nfoo\\n```'", () => {
		const input = `
\`\`\` foo\\+bar
foo
\`\`\`
`;
		const expected = `
<pre><code class="language-foo+bar">foo
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 25, line 649: '&nbsp; &amp; &copy; &AElig; &Dcaron;\\n&frac34; &HilbertSpace; &DifferentialD;\\n&ClockwiseContourIntegral; &ngE;'", () => {
		const input = `
&nbsp; &amp; &copy; &AElig; &Dcaron;
&frac34; &HilbertSpace; &DifferentialD;
&ClockwiseContourIntegral; &ngE;
`;
		const expected = `
<p>  &amp; © Æ Ď
¾ ℋ ⅆ
∲ ≧̸</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 26, line 668: '&#35; &#1234; &#992; &#0;'", () => {
		const input = `
&#35; &#1234; &#992; &#0;
`;
		const expected = `
<p># Ӓ Ϡ �</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 27, line 681: '&#X22; &#XD06; &#xcab;'", () => {
		const input = `
&#X22; &#XD06; &#xcab;
`;
		const expected = `
<p>&quot; ആ ಫ</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 28, line 690: '&nbsp &x; &#; &#x;\\n&#87654321;\\n&#abcdef0;\\n&ThisIsNotDefined; &hi?;'", () => {
		const input = `
&nbsp &x; &#; &#x;
&#87654321;
&#abcdef0;
&ThisIsNotDefined; &hi?;
`;
		const expected = `
<p>&amp;nbsp &amp;x; &amp;#; &amp;#x;
&amp;#87654321;
&amp;#abcdef0;
&amp;ThisIsNotDefined; &amp;hi?;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 29, line 707: '&copy'", () => {
		const input = `
&copy
`;
		const expected = `
<p>&amp;copy</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 30, line 717: '&MadeUpEntity;'", () => {
		const input = `
&MadeUpEntity;
`;
		const expected = `
<p>&amp;MadeUpEntity;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 31, line 728: '<a href=\"&ouml;&ouml;.html\">'", () => {
		const input = `
<a href="&ouml;&ouml;.html">
`;
		const expected = `
<a href="&ouml;&ouml;.html">
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 32, line 735: '[foo](/f&ouml;&ouml; \"f&ouml;&ouml;\")'", () => {
		const input = `
[foo](/f&ouml;&ouml; "f&ouml;&ouml;")
`;
		const expected = `
<p><a href="/f%C3%B6%C3%B6" title="föö">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 33, line 742: '[foo]\\n\\n[foo]: /f&ouml;&ouml; \"f&ouml;&ouml;\"'", () => {
		const input = `
[foo]

[foo]: /f&ouml;&ouml; "f&ouml;&ouml;"
`;
		const expected = `
<p><a href="/f%C3%B6%C3%B6" title="föö">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 34, line 751: '``` f&ouml;&ouml;\\nfoo\\n```'", () => {
		const input = `
\`\`\` f&ouml;&ouml;
foo
\`\`\`
`;
		const expected = `
<pre><code class="language-föö">foo
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 35, line 764: '`f&ouml;&ouml;`'", () => {
		const input = `
\`f&ouml;&ouml;\`
`;
		const expected = `
<p><code>f&amp;ouml;&amp;ouml;</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 36, line 771: '    f&ouml;f&ouml;'", () => {
		const input = `
    f&ouml;f&ouml;
`;
		const expected = `
<pre><code>f&amp;ouml;f&amp;ouml;
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 37, line 783: '&#42;foo&#42;\\n*foo*'", () => {
		const input = `
&#42;foo&#42;
*foo*
`;
		const expected = `
<p>*foo*
<em>foo</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 38, line 791: '&#42; foo\\n\\n* foo'", () => {
		const input = `
&#42; foo

* foo
`;
		const expected = `
<p>* foo</p>
<ul>
<li>foo</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 39, line 802: 'foo&#10;&#10;bar'", () => {
		const input = `
foo&#10;&#10;bar
`;
		const expected = `
<p>foo

bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 40, line 810: '&#9;foo'", () => {
		const input = `
&#9;foo
`;
		const expected = `
<p>	foo</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 41, line 817: '[a](url &quot;tit&quot;)'", () => {
		const input = `
[a](url &quot;tit&quot;)
`;
		const expected = `
<p>[a](url &quot;tit&quot;)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 42, line 840: '- `one\\n- two`'", () => {
		const input = `
- \`one
- two\`
`;
		const expected = `
<ul>
<li>\`one</li>
<li>two\`</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 43, line 879: '***\\n---\\n___'", () => {
		const input = `
***
---
___
`;
		const expected = `
<hr />
<hr />
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 44, line 892: '+++'", () => {
		const input = `
+++
`;
		const expected = `
<p>+++</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 45, line 899: '==='", () => {
		const input = `
===
`;
		const expected = `
<p>===</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 46, line 908: '--\\n**\\n__'", () => {
		const input = `
--
**
__
`;
		const expected = `
<p>--
**
__</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 47, line 921: ' ***\\n  ***\\n   ***'", () => {
		const input = `
 ***
  ***
   ***
`;
		const expected = `
<hr />
<hr />
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 48, line 934: '    ***'", () => {
		const input = `
    ***
`;
		const expected = `
<pre><code>***
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 49, line 942: 'Foo\\n    ***'", () => {
		const input = `
Foo
    ***
`;
		const expected = `
<p>Foo
***</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 50, line 953: '_____________________________________'", () => {
		const input = `
_____________________________________
`;
		const expected = `
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 51, line 962: ' - - -'", () => {
		const input = `
 - - -
`;
		const expected = `
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 52, line 969: ' **  * ** * ** * **'", () => {
		const input = `
 **  * ** * ** * **
`;
		const expected = `
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 53, line 976: '-     -      -      -'", () => {
		const input = `
-     -      -      -
`;
		const expected = `
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 54, line 985: '- - - -    '", () => {
		const input = `
- - - -    
`;
		const expected = `
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 55, line 994: '_ _ _ _ a\\n\\na------\\n\\n---a---'", () => {
		const input = `
_ _ _ _ a

a------

---a---
`;
		const expected = `
<p>_ _ _ _ a</p>
<p>a------</p>
<p>---a---</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 56, line 1010: ' *-*'", () => {
		const input = `
 *-*
`;
		const expected = `
<p><em>-</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 57, line 1019: '- foo\\n***\\n- bar'", () => {
		const input = `
- foo
***
- bar
`;
		const expected = `
<ul>
<li>foo</li>
</ul>
<hr />
<ul>
<li>bar</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 58, line 1036: 'Foo\\n***\\nbar'", () => {
		const input = `
Foo
***
bar
`;
		const expected = `
<p>Foo</p>
<hr />
<p>bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 59, line 1053: 'Foo\\n---\\nbar'", () => {
		const input = `
Foo
---
bar
`;
		const expected = `
<h2>Foo</h2>
<p>bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 60, line 1066: '* Foo\\n* * *\\n* Bar'", () => {
		const input = `
* Foo
* * *
* Bar
`;
		const expected = `
<ul>
<li>Foo</li>
</ul>
<hr />
<ul>
<li>Bar</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 61, line 1083: '- Foo\\n- * * *'", () => {
		const input = `
- Foo
- * * *
`;
		const expected = `
<ul>
<li>Foo</li>
<li>
<hr />
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 62, line 1112: '# foo\\n## foo\\n### foo\\n#### foo\\n##### foo\\n###### foo'", () => {
		const input = `
# foo
## foo
### foo
#### foo
##### foo
###### foo
`;
		const expected = `
<h1>foo</h1>
<h2>foo</h2>
<h3>foo</h3>
<h4>foo</h4>
<h5>foo</h5>
<h6>foo</h6>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 63, line 1131: '####### foo'", () => {
		const input = `
####### foo
`;
		const expected = `
<p>####### foo</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 64, line 1146: '#5 bolt\\n\\n#hashtag'", () => {
		const input = `
#5 bolt

#hashtag
`;
		const expected = `
<p>#5 bolt</p>
<p>#hashtag</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 65, line 1158: '\\## foo'", () => {
		const input = `
\\## foo
`;
		const expected = `
<p>## foo</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 66, line 1167: '# foo *bar* \\*baz\\*'", () => {
		const input = `
# foo *bar* \\*baz\\*
`;
		const expected = `
<h1>foo <em>bar</em> *baz*</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 67, line 1176: '#                  foo                     '", () => {
		const input = `
#                  foo                     
`;
		const expected = `
<h1>foo</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 68, line 1185: ' ### foo\\n  ## foo\\n   # foo'", () => {
		const input = `
 ### foo
  ## foo
   # foo
`;
		const expected = `
<h3>foo</h3>
<h2>foo</h2>
<h1>foo</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 69, line 1198: '    # foo'", () => {
		const input = `
    # foo
`;
		const expected = `
<pre><code># foo
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 70, line 1206: 'foo\\n    # bar'", () => {
		const input = `
foo
    # bar
`;
		const expected = `
<p>foo
# bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 71, line 1217: '## foo ##\\n  ###   bar    ###'", () => {
		const input = `
## foo ##
  ###   bar    ###
`;
		const expected = `
<h2>foo</h2>
<h3>bar</h3>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 72, line 1228: '# foo ##################################\\n##### foo ##'", () => {
		const input = `
# foo ##################################
##### foo ##
`;
		const expected = `
<h1>foo</h1>
<h5>foo</h5>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 73, line 1239: '### foo ###     '", () => {
		const input = `
### foo ###     
`;
		const expected = `
<h3>foo</h3>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 74, line 1250: '### foo ### b'", () => {
		const input = `
### foo ### b
`;
		const expected = `
<h3>foo ### b</h3>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 75, line 1259: '# foo#'", () => {
		const input = `
# foo#
`;
		const expected = `
<h1>foo#</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 76, line 1269: '### foo \\###\\n## foo #\\##\\n# foo \\#'", () => {
		const input = `
### foo \\###
## foo #\\##
# foo \\#
`;
		const expected = `
<h3>foo ###</h3>
<h2>foo ###</h2>
<h1>foo #</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 77, line 1283: '****\\n## foo\\n****'", () => {
		const input = `
****
## foo
****
`;
		const expected = `
<hr />
<h2>foo</h2>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 78, line 1294: 'Foo bar\\n# baz\\nBar foo'", () => {
		const input = `
Foo bar
# baz
Bar foo
`;
		const expected = `
<p>Foo bar</p>
<h1>baz</h1>
<p>Bar foo</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 79, line 1307: '## \\n#\\n### ###'", () => {
		const input = `
## 
#
### ###
`;
		const expected = `
<h2></h2>
<h1></h1>
<h3></h3>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 80, line 1347: 'Foo *bar*\\n=========\\n\\nFoo *bar*\\n---------'", () => {
		const input = `
Foo *bar*
=========

Foo *bar*
---------
`;
		const expected = `
<h1>Foo <em>bar</em></h1>
<h2>Foo <em>bar</em></h2>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 81, line 1361: 'Foo *bar\\nbaz*\\n===='", () => {
		const input = `
Foo *bar
baz*
====
`;
		const expected = `
<h1>Foo <em>bar
baz</em></h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 82, line 1375: '  Foo *bar\\nbaz*→\\n===='", () => {
		const input = `
  Foo *bar
baz*	
====
`;
		const expected = `
<h1>Foo <em>bar
baz</em></h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 83, line 1387: 'Foo\\n-------------------------\\n\\nFoo\\n='", () => {
		const input = `
Foo
-------------------------

Foo
=
`;
		const expected = `
<h2>Foo</h2>
<h1>Foo</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 84, line 1402: '   Foo\\n---\\n\\n  Foo\\n-----\\n\\n  Foo\\n  ==='", () => {
		const input = `
   Foo
---

  Foo
-----

  Foo
  ===
`;
		const expected = `
<h2>Foo</h2>
<h2>Foo</h2>
<h1>Foo</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 85, line 1420: '    Foo\\n    ---\\n\\n    Foo\\n---'", () => {
		const input = `
    Foo
    ---

    Foo
---
`;
		const expected = `
<pre><code>Foo
---

Foo
</code></pre>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 86, line 1439: 'Foo\\n   ----      '", () => {
		const input = `
Foo
   ----      
`;
		const expected = `
<h2>Foo</h2>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 87, line 1449: 'Foo\\n    ---'", () => {
		const input = `
Foo
    ---
`;
		const expected = `
<p>Foo
---</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 88, line 1460: 'Foo\\n= =\\n\\nFoo\\n--- -'", () => {
		const input = `
Foo
= =

Foo
--- -
`;
		const expected = `
<p>Foo
= =</p>
<p>Foo</p>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 89, line 1476: 'Foo  \\n-----'", () => {
		const input = `
Foo  
-----
`;
		const expected = `
<h2>Foo</h2>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 90, line 1486: 'Foo\\\\n----'", () => {
		const input = `
Foo\\
----
`;
		const expected = `
<h2>Foo\\</h2>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 91, line 1497: '`Foo\\n----\\n`\\n\\n<a title=\"a lot\\n---\\nof dashes\"/>'", () => {
		const input = `
\`Foo
----
\`

<a title="a lot
---
of dashes"/>
`;
		const expected = `
<h2>\`Foo</h2>
<p>\`</p>
<h2>&lt;a title=&quot;a lot</h2>
<p>of dashes&quot;/&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 92, line 1516: '> Foo\\n---'", () => {
		const input = `
> Foo
---
`;
		const expected = `
<blockquote>
<p>Foo</p>
</blockquote>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 93, line 1527: '> foo\\nbar\\n==='", () => {
		const input = `
> foo
bar
===
`;
		const expected = `
<blockquote>
<p>foo
bar
===</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 94, line 1540: '- Foo\\n---'", () => {
		const input = `
- Foo
---
`;
		const expected = `
<ul>
<li>Foo</li>
</ul>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 95, line 1555: 'Foo\\nBar\\n---'", () => {
		const input = `
Foo
Bar
---
`;
		const expected = `
<h2>Foo
Bar</h2>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 96, line 1568: '---\\nFoo\\n---\\nBar\\n---\\nBaz'", () => {
		const input = `
---
Foo
---
Bar
---
Baz
`;
		const expected = `
<hr />
<h2>Foo</h2>
<h2>Bar</h2>
<p>Baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 97, line 1585: '\\n===='", () => {
		const input = `

====
`;
		const expected = `
<p>====</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 98, line 1597: '---\\n---'", () => {
		const input = `
---
---
`;
		const expected = `
<hr />
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 99, line 1606: '- foo\\n-----'", () => {
		const input = `
- foo
-----
`;
		const expected = `
<ul>
<li>foo</li>
</ul>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 100, line 1617: '    foo\\n---'", () => {
		const input = `
    foo
---
`;
		const expected = `
<pre><code>foo
</code></pre>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 101, line 1627: '> foo\\n-----'", () => {
		const input = `
> foo
-----
`;
		const expected = `
<blockquote>
<p>foo</p>
</blockquote>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 102, line 1641: '\\> foo\\n------'", () => {
		const input = `
\\> foo
------
`;
		const expected = `
<h2>&gt; foo</h2>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 103, line 1672: 'Foo\\n\\nbar\\n---\\nbaz'", () => {
		const input = `
Foo

bar
---
baz
`;
		const expected = `
<p>Foo</p>
<h2>bar</h2>
<p>baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 104, line 1688: 'Foo\\nbar\\n\\n---\\n\\nbaz'", () => {
		const input = `
Foo
bar

---

baz
`;
		const expected = `
<p>Foo
bar</p>
<hr />
<p>baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 105, line 1706: 'Foo\\nbar\\n* * *\\nbaz'", () => {
		const input = `
Foo
bar
* * *
baz
`;
		const expected = `
<p>Foo
bar</p>
<hr />
<p>baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 106, line 1721: 'Foo\\nbar\\n\\---\\nbaz'", () => {
		const input = `
Foo
bar
\\---
baz
`;
		const expected = `
<p>Foo
bar
---
baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 107, line 1749: '    a simple\\n      indented code block'", () => {
		const input = `
    a simple
      indented code block
`;
		const expected = `
<pre><code>a simple
  indented code block
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 108, line 1763: '  - foo\\n\\n    bar'", () => {
		const input = `
  - foo

    bar
`;
		const expected = `
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 109, line 1777: '1.  foo\\n\\n    - bar'", () => {
		const input = `
1.  foo

    - bar
`;
		const expected = `
<ol>
<li>
<p>foo</p>
<ul>
<li>bar</li>
</ul>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 110, line 1797: '    <a/>\\n    *hi*\\n\\n    - one'", () => {
		const input = `
    <a/>
    *hi*

    - one
`;
		const expected = `
<pre><code>&lt;a/&gt;
*hi*

- one
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 111, line 1813: '    chunk1\\n\\n    chunk2\\n  \\n \\n \\n    chunk3'", () => {
		const input = `
    chunk1

    chunk2
  
 
 
    chunk3
`;
		const expected = `
<pre><code>chunk1

chunk2



chunk3
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 112, line 1836: '    chunk1\\n      \\n      chunk2'", () => {
		const input = `
    chunk1
      
      chunk2
`;
		const expected = `
<pre><code>chunk1
  
  chunk2
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 113, line 1851: 'Foo\\n    bar\\n'", () => {
		const input = `
Foo
    bar

`;
		const expected = `
<p>Foo
bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 114, line 1865: '    foo\\nbar'", () => {
		const input = `
    foo
bar
`;
		const expected = `
<pre><code>foo
</code></pre>
<p>bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 115, line 1878: '# Heading\\n    foo\\nHeading\\n------\\n    foo\\n----'", () => {
		const input = `
# Heading
    foo
Heading
------
    foo
----
`;
		const expected = `
<h1>Heading</h1>
<pre><code>foo
</code></pre>
<h2>Heading</h2>
<pre><code>foo
</code></pre>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 116, line 1898: '        foo\\n    bar'", () => {
		const input = `
        foo
    bar
`;
		const expected = `
<pre><code>    foo
bar
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 117, line 1911: '\\n    \\n    foo\\n    \\n'", () => {
		const input = `

    
    foo
    

`;
		const expected = `
<pre><code>foo
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 118, line 1925: '    foo  '", () => {
		const input = `
    foo  
`;
		const expected = `
<pre><code>foo  
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 119, line 1980: '```\\n<\\n >\\n```'", () => {
		const input = `
\`\`\`
<
 >
\`\`\`
`;
		const expected = `
<pre><code>&lt;
 &gt;
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 120, line 1994: '~~~\\n<\\n >\\n~~~'", () => {
		const input = `
~~~
<
 >
~~~
`;
		const expected = `
<pre><code>&lt;
 &gt;
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 121, line 2007: '``\\nfoo\\n``'", () => {
		const input = `
\`\`
foo
\`\`
`;
		const expected = `
<p><code>foo</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 122, line 2018: '```\\naaa\\n~~~\\n```'", () => {
		const input = `
\`\`\`
aaa
~~~
\`\`\`
`;
		const expected = `
<pre><code>aaa
~~~
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 123, line 2030: '~~~\\naaa\\n```\\n~~~'", () => {
		const input = `
~~~
aaa
\`\`\`
~~~
`;
		const expected = `
<pre><code>aaa
\`\`\`
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 124, line 2044: '````\\naaa\\n```\\n``````'", () => {
		const input = `
\`\`\`\`
aaa
\`\`\`
\`\`\`\`\`\`
`;
		const expected = `
<pre><code>aaa
\`\`\`
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 125, line 2056: '~~~~\\naaa\\n~~~\\n~~~~'", () => {
		const input = `
~~~~
aaa
~~~
~~~~
`;
		const expected = `
<pre><code>aaa
~~~
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 126, line 2071: '```'", () => {
		const input = `
\`\`\`
`;
		const expected = `
<pre><code></code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 127, line 2078: '`````\\n\\n```\\naaa'", () => {
		const input = `
\`\`\`\`\`

\`\`\`
aaa
`;
		const expected = `
<pre><code>
\`\`\`
aaa
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 128, line 2091: '> ```\\n> aaa\\n\\nbbb'", () => {
		const input = `
> \`\`\`
> aaa

bbb
`;
		const expected = `
<blockquote>
<pre><code>aaa
</code></pre>
</blockquote>
<p>bbb</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 129, line 2107: '```\\n\\n  \\n```'", () => {
		const input = `
\`\`\`

  
\`\`\`
`;
		const expected = `
<pre><code>
  
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 130, line 2121: '```\\n```'", () => {
		const input = `
\`\`\`
\`\`\`
`;
		const expected = `
<pre><code></code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 131, line 2133: ' ```\\n aaa\\naaa\\n```'", () => {
		const input = `
 \`\`\`
 aaa
aaa
\`\`\`
`;
		const expected = `
<pre><code>aaa
aaa
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 132, line 2145: '  ```\\naaa\\n  aaa\\naaa\\n  ```'", () => {
		const input = `
  \`\`\`
aaa
  aaa
aaa
  \`\`\`
`;
		const expected = `
<pre><code>aaa
aaa
aaa
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 133, line 2159: '   ```\\n   aaa\\n    aaa\\n  aaa\\n   ```'", () => {
		const input = `
   \`\`\`
   aaa
    aaa
  aaa
   \`\`\`
`;
		const expected = `
<pre><code>aaa
 aaa
aaa
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 134, line 2175: '    ```\\n    aaa\\n    ```'", () => {
		const input = `
    \`\`\`
    aaa
    \`\`\`
`;
		const expected = `
<pre><code>\`\`\`
aaa
\`\`\`
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 135, line 2190: '```\\naaa\\n  ```'", () => {
		const input = `
\`\`\`
aaa
  \`\`\`
`;
		const expected = `
<pre><code>aaa
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 136, line 2200: '   ```\\naaa\\n  ```'", () => {
		const input = `
   \`\`\`
aaa
  \`\`\`
`;
		const expected = `
<pre><code>aaa
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 137, line 2212: '```\\naaa\\n    ```'", () => {
		const input = `
\`\`\`
aaa
    \`\`\`
`;
		const expected = `
<pre><code>aaa
    \`\`\`
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 138, line 2226: '``` ```\\naaa'", () => {
		const input = `
\`\`\` \`\`\`
aaa
`;
		const expected = `
<p><code> </code>
aaa</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 139, line 2235: '~~~~~~\\naaa\\n~~~ ~~'", () => {
		const input = `
~~~~~~
aaa
~~~ ~~
`;
		const expected = `
<pre><code>aaa
~~~ ~~
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 140, line 2249: 'foo\\n```\\nbar\\n```\\nbaz'", () => {
		const input = `
foo
\`\`\`
bar
\`\`\`
baz
`;
		const expected = `
<p>foo</p>
<pre><code>bar
</code></pre>
<p>baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 141, line 2266: 'foo\\n---\\n~~~\\nbar\\n~~~\\n# baz'", () => {
		const input = `
foo
---
~~~
bar
~~~
# baz
`;
		const expected = `
<h2>foo</h2>
<pre><code>bar
</code></pre>
<h1>baz</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 142, line 2288: '```ruby\\ndef foo(x)\\n  return 3\\nend\\n```'", () => {
		const input = `
\`\`\`ruby
def foo(x)
  return 3
end
\`\`\`
`;
		const expected = `
<pre><code class="language-ruby">def foo(x)
  return 3
end
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 143, line 2302: '~~~~    ruby startline=3 $%@#$\\ndef foo(x)\\n  return 3\\nend\\n~~~~~~~'", () => {
		const input = `
~~~~    ruby startline=3 $%@#$
def foo(x)
  return 3
end
~~~~~~~
`;
		const expected = `
<pre><code class="language-ruby">def foo(x)
  return 3
end
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 144, line 2316: '````;\\n````'", () => {
		const input = `
\`\`\`\`;
\`\`\`\`
`;
		const expected = `
<pre><code class="language-;"></code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 145, line 2326: '``` aa ```\\nfoo'", () => {
		const input = `
\`\`\` aa \`\`\`
foo
`;
		const expected = `
<p><code>aa</code>
foo</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 146, line 2337: '~~~ aa ``` ~~~\\nfoo\\n~~~'", () => {
		const input = `
~~~ aa \`\`\` ~~~
foo
~~~
`;
		const expected = `
<pre><code class="language-aa">foo
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 147, line 2349: '```\\n``` aaa\\n```'", () => {
		const input = `
\`\`\`
\`\`\` aaa
\`\`\`
`;
		const expected = `
<pre><code>\`\`\` aaa
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 148, line 2428: '<table><tr><td>\\n<pre>\\n**Hello**,\\n\\n_world_.\\n</pre>\\n</td></tr></table>'", () => {
		const input = `
<table><tr><td>
<pre>
**Hello**,

_world_.
</pre>
</td></tr></table>
`;
		const expected = `
<table><tr><td>
<pre>
**Hello**,
<p><em>world</em>.
</pre></p>
</td></tr></table>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 149, line 2457: '<table>\\n  <tr>\\n    <td>\\n           hi\\n    </td>\\n  </tr>\\n</table>\\n\\nokay.'", () => {
		const input = `
<table>
  <tr>
    <td>
           hi
    </td>
  </tr>
</table>

okay.
`;
		const expected = `
<table>
  <tr>
    <td>
           hi
    </td>
  </tr>
</table>
<p>okay.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 150, line 2479: ' <div>\\n  *hello*\\n         <foo><a>'", () => {
		const input = `
 <div>
  *hello*
         <foo><a>
`;
		const expected = `
 <div>
  *hello*
         <foo><a>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 151, line 2492: '</div>\\n*foo*'", () => {
		const input = `
</div>
*foo*
`;
		const expected = `
</div>
*foo*
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 152, line 2503: '<DIV CLASS=\"foo\">\\n\\n*Markdown*\\n\\n</DIV>'", () => {
		const input = `
<DIV CLASS="foo">

*Markdown*

</DIV>
`;
		const expected = `
<DIV CLASS="foo">
<p><em>Markdown</em></p>
</DIV>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test('Example 153, line 2519: \'<div id="foo"\\n  class="bar">\\n</div>\'', () => {
		const input = `
<div id="foo"
  class="bar">
</div>
`;
		const expected = `
<div id="foo"
  class="bar">
</div>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test('Example 154, line 2530: \'<div id="foo" class="bar\\n  baz">\\n</div>\'', () => {
		const input = `
<div id="foo" class="bar
  baz">
</div>
`;
		const expected = `
<div id="foo" class="bar
  baz">
</div>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 155, line 2542: '<div>\\n*foo*\\n\\n*bar*'", () => {
		const input = `
<div>
*foo*

*bar*
`;
		const expected = `
<div>
*foo*
<p><em>bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 156, line 2558: '<div id=\"foo\"\\n*hi*'", () => {
		const input = `
<div id="foo"
*hi*
`;
		const expected = `
<div id="foo"
*hi*
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 157, line 2567: '<div class\\nfoo'", () => {
		const input = `
<div class
foo
`;
		const expected = `
<div class
foo
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 158, line 2579: '<div *???-&&&-<---\\n*foo*'", () => {
		const input = `
<div *???-&&&-<---
*foo*
`;
		const expected = `
<div *???-&&&-<---
*foo*
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 159, line 2591: '<div><a href=\"bar\">*foo*</a></div>'", () => {
		const input = `
<div><a href="bar">*foo*</a></div>
`;
		const expected = `
<div><a href="bar">*foo*</a></div>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 160, line 2598: '<table><tr><td>\\nfoo\\n</td></tr></table>'", () => {
		const input = `
<table><tr><td>
foo
</td></tr></table>
`;
		const expected = `
<table><tr><td>
foo
</td></tr></table>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 161, line 2615: '<div></div>\\n``` c\\nint x = 33;\\n```'", () => {
		const input = `
<div></div>
\`\`\` c
int x = 33;
\`\`\`
`;
		const expected = `
<div></div>
\`\`\` c
int x = 33;
\`\`\`
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 162, line 2632: '<a href=\"foo\">\\n*bar*\\n</a>'", () => {
		const input = `
<a href="foo">
*bar*
</a>
`;
		const expected = `
<a href="foo">
*bar*
</a>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 163, line 2645: '<Warning>\\n*bar*\\n</Warning>'", () => {
		const input = `
<Warning>
*bar*
</Warning>
`;
		const expected = `
<Warning>
*bar*
</Warning>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 164, line 2656: '<i class=\"foo\">\\n*bar*\\n</i>'", () => {
		const input = `
<i class="foo">
*bar*
</i>
`;
		const expected = `
<i class="foo">
*bar*
</i>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 165, line 2667: '</ins>\\n*bar*'", () => {
		const input = `
</ins>
*bar*
`;
		const expected = `
</ins>
*bar*
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 166, line 2682: '<del>\\n*foo*\\n</del>'", () => {
		const input = `
<del>
*foo*
</del>
`;
		const expected = `
<del>
*foo*
</del>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 167, line 2697: '<del>\\n\\n*foo*\\n\\n</del>'", () => {
		const input = `
<del>

*foo*

</del>
`;
		const expected = `
<del>
<p><em>foo</em></p>
</del>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 168, line 2715: '<del>*foo*</del>'", () => {
		const input = `
<del>*foo*</del>
`;
		const expected = `
<p><del><em>foo</em></del></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 169, line 2731: '<pre language=\"haskell\"><code>\\nimport Text.HTML.TagSoup\\n\\nmain :: IO ()\\nmain = print $ parseTags tags\\n</code></pre>\\nokay'", () => {
		const input = `
<pre language="haskell"><code>
import Text.HTML.TagSoup

main :: IO ()
main = print $ parseTags tags
</code></pre>
okay
`;
		const expected = `
<pre language="haskell"><code>
import Text.HTML.TagSoup

main :: IO ()
main = print $ parseTags tags
</code></pre>
<p>okay</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test('Example 170, line 2752: \'<script type="text/javascript">\\n// JavaScript example\\n\\ndocument.getElementById("demo").innerHTML = "Hello JavaScript!";\\n</script>\\nokay\'', () => {
		const input = `
<script type="text/javascript">
// JavaScript example

document.getElementById("demo").innerHTML = "Hello JavaScript!";
</script>
okay
`;
		const expected = `
<script type="text/javascript">
// JavaScript example

document.getElementById("demo").innerHTML = "Hello JavaScript!";
</script>
<p>okay</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 171, line 2771: '<textarea>\\n\\n*foo*\\n\\n_bar_\\n\\n</textarea>'", () => {
		const input = `
<textarea>

*foo*

_bar_

</textarea>
`;
		const expected = `
<textarea>

*foo*

_bar_

</textarea>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 172, line 2791: '<style\\n  type=\"text/css\">\\nh1 {color:red;}\\n\\np {color:blue;}\\n</style>\\nokay'", () => {
		const input = `
<style
  type="text/css">
h1 {color:red;}

p {color:blue;}
</style>
okay
`;
		const expected = `
<style
  type="text/css">
h1 {color:red;}

p {color:blue;}
</style>
<p>okay</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 173, line 2814: '<style\\n  type=\"text/css\">\\n\\nfoo'", () => {
		const input = `
<style
  type="text/css">

foo
`;
		const expected = `
<style
  type="text/css">

foo
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 174, line 2827: '> <div>\\n> foo\\n\\nbar'", () => {
		const input = `
> <div>
> foo

bar
`;
		const expected = `
<blockquote>
<div>
foo
</blockquote>
<p>bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 175, line 2841: '- <div>\\n- foo'", () => {
		const input = `
- <div>
- foo
`;
		const expected = `
<ul>
<li>
<div>
</li>
<li>foo</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 176, line 2856: '<style>p{color:red;}</style>\\n*foo*'", () => {
		const input = `
<style>p{color:red;}</style>
*foo*
`;
		const expected = `
<style>p{color:red;}</style>
<p><em>foo</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 177, line 2865: '<!-- foo -->*bar*\\n*baz*'", () => {
		const input = `
<!-- foo -->*bar*
*baz*
`;
		const expected = `
<!-- foo -->*bar*
<p><em>baz</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 178, line 2877: '<script>\\nfoo\\n</script>1. *bar*'", () => {
		const input = `
<script>
foo
</script>1. *bar*
`;
		const expected = `
<script>
foo
</script>1. *bar*
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 179, line 2890: '<!-- Foo\\n\\nbar\\n   baz -->\\nokay'", () => {
		const input = `
<!-- Foo

bar
   baz -->
okay
`;
		const expected = `
<!-- Foo

bar
   baz -->
<p>okay</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 180, line 2908: '<?php\\n\\n  echo '>';\\n\\n?>\\nokay'", () => {
		const input = `
<?php

  echo '>';

?>
okay
`;
		const expected = `
<?php

  echo '>';

?>
<p>okay</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 181, line 2927: '<!DOCTYPE html>'", () => {
		const input = `
<!DOCTYPE html>
`;
		const expected = `
<!DOCTYPE html>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 182, line 2936: '<![CDATA[\\nfunction matchwo(a,b)\\n{\\n  if (a < b && a < 0) then {\\n    return 1;\\n\\n  } else {\\n\\n    return 0;\\n  }\\n}\\n]]>\\nokay'", () => {
		const input = `
<![CDATA[
function matchwo(a,b)
{
  if (a < b && a < 0) then {
    return 1;

  } else {

    return 0;
  }
}
]]>
okay
`;
		const expected = `
<![CDATA[
function matchwo(a,b)
{
  if (a < b && a < 0) then {
    return 1;

  } else {

    return 0;
  }
}
]]>
<p>okay</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 183, line 2970: '  <!-- foo -->\\n\\n    <!-- foo -->'", () => {
		const input = `
  <!-- foo -->

    <!-- foo -->
`;
		const expected = `
  <!-- foo -->
<pre><code>&lt;!-- foo --&gt;
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 184, line 2981: '  <div>\\n\\n    <div>'", () => {
		const input = `
  <div>

    <div>
`;
		const expected = `
  <div>
<pre><code>&lt;div&gt;
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 185, line 2995: 'Foo\\n<div>\\nbar\\n</div>'", () => {
		const input = `
Foo
<div>
bar
</div>
`;
		const expected = `
<p>Foo</p>
<div>
bar
</div>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 186, line 3012: '<div>\\nbar\\n</div>\\n*foo*'", () => {
		const input = `
<div>
bar
</div>
*foo*
`;
		const expected = `
<div>
bar
</div>
*foo*
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 187, line 3027: 'Foo\\n<a href=\"bar\">\\nbaz'", () => {
		const input = `
Foo
<a href="bar">
baz
`;
		const expected = `
<p>Foo
<a href="bar">
baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 188, line 3068: '<div>\\n\\n*Emphasized* text.\\n\\n</div>'", () => {
		const input = `
<div>

*Emphasized* text.

</div>
`;
		const expected = `
<div>
<p><em>Emphasized</em> text.</p>
</div>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 189, line 3081: '<div>\\n*Emphasized* text.\\n</div>'", () => {
		const input = `
<div>
*Emphasized* text.
</div>
`;
		const expected = `
<div>
*Emphasized* text.
</div>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 190, line 3103: '<table>\\n\\n<tr>\\n\\n<td>\\nHi\\n</td>\\n\\n</tr>\\n\\n</table>'", () => {
		const input = `
<table>

<tr>

<td>
Hi
</td>

</tr>

</table>
`;
		const expected = `
<table>
<tr>
<td>
Hi
</td>
</tr>
</table>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 191, line 3130: '<table>\\n\\n  <tr>\\n\\n    <td>\\n      Hi\\n    </td>\\n\\n  </tr>\\n\\n</table>'", () => {
		const input = `
<table>

  <tr>

    <td>
      Hi
    </td>

  </tr>

</table>
`;
		const expected = `
<table>
  <tr>
<pre><code>&lt;td&gt;
  Hi
&lt;/td&gt;
</code></pre>
  </tr>
</table>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 192, line 3179: '[foo]: /url \"title\"\\n\\n[foo]'", () => {
		const input = `
[foo]: /url "title"

[foo]
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 193, line 3188: '   [foo]: \\n      /url  \\n           'the title'  \\n\\n[foo]'", () => {
		const input = `
   [foo]: 
      /url  
           'the title'  

[foo]
`;
		const expected = `
<p><a href="/url" title="the title">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 194, line 3199: '[Foo*bar\\]]:my_(url) 'title (with parens)'\\n\\n[Foo*bar\\]]'", () => {
		const input = `
[Foo*bar\\]]:my_(url) 'title (with parens)'

[Foo*bar\\]]
`;
		const expected = `
<p><a href="my_(url)" title="title (with parens)">Foo*bar]</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 195, line 3208: '[Foo bar]:\\n<my url>\\n'title'\\n\\n[Foo bar]'", () => {
		const input = `
[Foo bar]:
<my url>
'title'

[Foo bar]
`;
		const expected = `
<p><a href="my%20url" title="title">Foo bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 196, line 3221: '[foo]: /url '\\ntitle\\nline1\\nline2\\n'\\n\\n[foo]'", () => {
		const input = `
[foo]: /url '
title
line1
line2
'

[foo]
`;
		const expected = `
<p><a href="/url" title="
title
line1
line2
">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 197, line 3240: '[foo]: /url 'title\\n\\nwith blank line'\\n\\n[foo]'", () => {
		const input = `
[foo]: /url 'title

with blank line'

[foo]
`;
		const expected = `
<p>[foo]: /url 'title</p>
<p>with blank line'</p>
<p>[foo]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 198, line 3255: '[foo]:\\n/url\\n\\n[foo]'", () => {
		const input = `
[foo]:
/url

[foo]
`;
		const expected = `
<p><a href="/url">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 199, line 3267: '[foo]:\\n\\n[foo]'", () => {
		const input = `
[foo]:

[foo]
`;
		const expected = `
<p>[foo]:</p>
<p>[foo]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 200, line 3279: '[foo]: <>\\n\\n[foo]'", () => {
		const input = `
[foo]: <>

[foo]
`;
		const expected = `
<p><a href="">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 201, line 3290: '[foo]: <bar>(baz)\\n\\n[foo]'", () => {
		const input = `
[foo]: <bar>(baz)

[foo]
`;
		const expected = `
<p>[foo]: <bar>(baz)</p>
<p>[foo]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test('Example 202, line 3303: \'[foo]: /url\\bar\\*baz "foo\\"bar\\baz"\\n\\n[foo]\'', () => {
		const input = `
[foo]: /url\\bar\\*baz "foo\\"bar\\baz"

[foo]
`;
		const expected = `
<p><a href="/url%5Cbar*baz" title="foo&quot;bar\\baz">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 203, line 3314: '[foo]\\n\\n[foo]: url'", () => {
		const input = `
[foo]

[foo]: url
`;
		const expected = `
<p><a href="url">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 204, line 3326: '[foo]\\n\\n[foo]: first\\n[foo]: second'", () => {
		const input = `
[foo]

[foo]: first
[foo]: second
`;
		const expected = `
<p><a href="first">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 205, line 3339: '[FOO]: /url\\n\\n[Foo]'", () => {
		const input = `
[FOO]: /url

[Foo]
`;
		const expected = `
<p><a href="/url">Foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 206, line 3348: '[ΑΓΩ]: /φου\\n\\n[αγω]'", () => {
		const input = `
[ΑΓΩ]: /φου

[αγω]
`;
		const expected = `
<p><a href="/%CF%86%CE%BF%CF%85">αγω</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 207, line 3363: '[foo]: /url'", () => {
		const input = `
[foo]: /url
`;
		const expected = `

`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 208, line 3371: '[\\nfoo\\n]: /url\\nbar'", () => {
		const input = `
[
foo
]: /url
bar
`;
		const expected = `
<p>bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 209, line 3384: '[foo]: /url \"title\" ok'", () => {
		const input = `
[foo]: /url "title" ok
`;
		const expected = `
<p>[foo]: /url &quot;title&quot; ok</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 210, line 3393: '[foo]: /url\\n\"title\" ok'", () => {
		const input = `
[foo]: /url
"title" ok
`;
		const expected = `
<p>&quot;title&quot; ok</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 211, line 3404: '    [foo]: /url \"title\"\\n\\n[foo]'", () => {
		const input = `
    [foo]: /url "title"

[foo]
`;
		const expected = `
<pre><code>[foo]: /url &quot;title&quot;
</code></pre>
<p>[foo]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 212, line 3418: '```\\n[foo]: /url\\n```\\n\\n[foo]'", () => {
		const input = `
\`\`\`
[foo]: /url
\`\`\`

[foo]
`;
		const expected = `
<pre><code>[foo]: /url
</code></pre>
<p>[foo]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 213, line 3433: 'Foo\\n[bar]: /baz\\n\\n[bar]'", () => {
		const input = `
Foo
[bar]: /baz

[bar]
`;
		const expected = `
<p>Foo
[bar]: /baz</p>
<p>[bar]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 214, line 3448: '# [Foo]\\n[foo]: /url\\n> bar'", () => {
		const input = `
# [Foo]
[foo]: /url
> bar
`;
		const expected = `
<h1><a href="/url">Foo</a></h1>
<blockquote>
<p>bar</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 215, line 3459: '[foo]: /url\\nbar\\n===\\n[foo]'", () => {
		const input = `
[foo]: /url
bar
===
[foo]
`;
		const expected = `
<h1>bar</h1>
<p><a href="/url">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 216, line 3469: '[foo]: /url\\n===\\n[foo]'", () => {
		const input = `
[foo]: /url
===
[foo]
`;
		const expected = `
<p>===
<a href="/url">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test('Example 217, line 3482: \'[foo]: /foo-url "foo"\\n[bar]: /bar-url\\n  "bar"\\n[baz]: /baz-url\\n\\n[foo],\\n[bar],\\n[baz]\'', () => {
		const input = `
[foo]: /foo-url "foo"
[bar]: /bar-url
  "bar"
[baz]: /baz-url

[foo],
[bar],
[baz]
`;
		const expected = `
<p><a href="/foo-url" title="foo">foo</a>,
<a href="/bar-url" title="bar">bar</a>,
<a href="/baz-url">baz</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 218, line 3503: '[foo]\\n\\n> [foo]: /url'", () => {
		const input = `
[foo]

> [foo]: /url
`;
		const expected = `
<p><a href="/url">foo</a></p>
<blockquote>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 219, line 3525: 'aaa\\n\\nbbb'", () => {
		const input = `
aaa

bbb
`;
		const expected = `
<p>aaa</p>
<p>bbb</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 220, line 3537: 'aaa\\nbbb\\n\\nccc\\nddd'", () => {
		const input = `
aaa
bbb

ccc
ddd
`;
		const expected = `
<p>aaa
bbb</p>
<p>ccc
ddd</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 221, line 3553: 'aaa\\n\\n\\nbbb'", () => {
		const input = `
aaa


bbb
`;
		const expected = `
<p>aaa</p>
<p>bbb</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 222, line 3566: '  aaa\\n bbb'", () => {
		const input = `
  aaa
 bbb
`;
		const expected = `
<p>aaa
bbb</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 223, line 3578: 'aaa\\n             bbb\\n                                       ccc'", () => {
		const input = `
aaa
             bbb
                                       ccc
`;
		const expected = `
<p>aaa
bbb
ccc</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 224, line 3592: '   aaa\\nbbb'", () => {
		const input = `
   aaa
bbb
`;
		const expected = `
<p>aaa
bbb</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 225, line 3601: '    aaa\\nbbb'", () => {
		const input = `
    aaa
bbb
`;
		const expected = `
<pre><code>aaa
</code></pre>
<p>bbb</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 226, line 3615: 'aaa     \\nbbb     '", () => {
		const input = `
aaa     
bbb     
`;
		const expected = `
<p>aaa<br />
bbb</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 227, line 3632: '  \\n\\naaa\\n  \\n\\n# aaa\\n\\n  '", () => {
		const input = `
  

aaa
  

# aaa

  
`;
		const expected = `
<p>aaa</p>
<h1>aaa</h1>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 228, line 3700: '> # Foo\\n> bar\\n> baz'", () => {
		const input = `
> # Foo
> bar
> baz
`;
		const expected = `
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 229, line 3715: '># Foo\\n>bar\\n> baz'", () => {
		const input = `
># Foo
>bar
> baz
`;
		const expected = `
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 230, line 3730: '   > # Foo\\n   > bar\\n > baz'", () => {
		const input = `
   > # Foo
   > bar
 > baz
`;
		const expected = `
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 231, line 3745: '    > # Foo\\n    > bar\\n    > baz'", () => {
		const input = `
    > # Foo
    > bar
    > baz
`;
		const expected = `
<pre><code>&gt; # Foo
&gt; bar
&gt; baz
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 232, line 3760: '> # Foo\\n> bar\\nbaz'", () => {
		const input = `
> # Foo
> bar
baz
`;
		const expected = `
<blockquote>
<h1>Foo</h1>
<p>bar
baz</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 233, line 3776: '> bar\\nbaz\\n> foo'", () => {
		const input = `
> bar
baz
> foo
`;
		const expected = `
<blockquote>
<p>bar
baz
foo</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 234, line 3800: '> foo\\n---'", () => {
		const input = `
> foo
---
`;
		const expected = `
<blockquote>
<p>foo</p>
</blockquote>
<hr />
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 235, line 3820: '> - foo\\n- bar'", () => {
		const input = `
> - foo
- bar
`;
		const expected = `
<blockquote>
<ul>
<li>foo</li>
</ul>
</blockquote>
<ul>
<li>bar</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 236, line 3838: '>     foo\\n    bar'", () => {
		const input = `
>     foo
    bar
`;
		const expected = `
<blockquote>
<pre><code>foo
</code></pre>
</blockquote>
<pre><code>bar
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 237, line 3851: '> ```\\nfoo\\n```'", () => {
		const input = `
> \`\`\`
foo
\`\`\`
`;
		const expected = `
<blockquote>
<pre><code></code></pre>
</blockquote>
<p>foo</p>
<pre><code></code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 238, line 3867: '> foo\\n    - bar'", () => {
		const input = `
> foo
    - bar
`;
		const expected = `
<blockquote>
<p>foo
- bar</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 239, line 3891: '>'", () => {
		const input = `
>
`;
		const expected = `
<blockquote>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 240, line 3899: '>\\n>  \\n> '", () => {
		const input = `
>
>  
> 
`;
		const expected = `
<blockquote>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 241, line 3911: '>\\n> foo\\n>  '", () => {
		const input = `
>
> foo
>  
`;
		const expected = `
<blockquote>
<p>foo</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 242, line 3924: '> foo\\n\\n> bar'", () => {
		const input = `
> foo

> bar
`;
		const expected = `
<blockquote>
<p>foo</p>
</blockquote>
<blockquote>
<p>bar</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 243, line 3946: '> foo\\n> bar'", () => {
		const input = `
> foo
> bar
`;
		const expected = `
<blockquote>
<p>foo
bar</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 244, line 3959: '> foo\\n>\\n> bar'", () => {
		const input = `
> foo
>
> bar
`;
		const expected = `
<blockquote>
<p>foo</p>
<p>bar</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 245, line 3973: 'foo\\n> bar'", () => {
		const input = `
foo
> bar
`;
		const expected = `
<p>foo</p>
<blockquote>
<p>bar</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 246, line 3987: '> aaa\\n***\\n> bbb'", () => {
		const input = `
> aaa
***
> bbb
`;
		const expected = `
<blockquote>
<p>aaa</p>
</blockquote>
<hr />
<blockquote>
<p>bbb</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 247, line 4005: '> bar\\nbaz'", () => {
		const input = `
> bar
baz
`;
		const expected = `
<blockquote>
<p>bar
baz</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 248, line 4016: '> bar\\n\\nbaz'", () => {
		const input = `
> bar

baz
`;
		const expected = `
<blockquote>
<p>bar</p>
</blockquote>
<p>baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 249, line 4028: '> bar\\n>\\nbaz'", () => {
		const input = `
> bar
>
baz
`;
		const expected = `
<blockquote>
<p>bar</p>
</blockquote>
<p>baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 250, line 4044: '> > > foo\\nbar'", () => {
		const input = `
> > > foo
bar
`;
		const expected = `
<blockquote>
<blockquote>
<blockquote>
<p>foo
bar</p>
</blockquote>
</blockquote>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 251, line 4059: '>>> foo\\n> bar\\n>>baz'", () => {
		const input = `
>>> foo
> bar
>>baz
`;
		const expected = `
<blockquote>
<blockquote>
<blockquote>
<p>foo
bar
baz</p>
</blockquote>
</blockquote>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 252, line 4081: '>     code\\n\\n>    not code'", () => {
		const input = `
>     code

>    not code
`;
		const expected = `
<blockquote>
<pre><code>code
</code></pre>
</blockquote>
<blockquote>
<p>not code</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 253, line 4135: 'A paragraph\\nwith two lines.\\n\\n    indented code\\n\\n> A block quote.'", () => {
		const input = `
A paragraph
with two lines.

    indented code

> A block quote.
`;
		const expected = `
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 254, line 4157: '1.  A paragraph\\n    with two lines.\\n\\n        indented code\\n\\n    > A block quote.'", () => {
		const input = `
1.  A paragraph
    with two lines.

        indented code

    > A block quote.
`;
		const expected = `
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 255, line 4190: '- one\\n\\n two'", () => {
		const input = `
- one

 two
`;
		const expected = `
<ul>
<li>one</li>
</ul>
<p>two</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 256, line 4202: '- one\\n\\n  two'", () => {
		const input = `
- one

  two
`;
		const expected = `
<ul>
<li>
<p>one</p>
<p>two</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 257, line 4216: ' -    one\\n\\n     two'", () => {
		const input = `
 -    one

     two
`;
		const expected = `
<ul>
<li>one</li>
</ul>
<pre><code> two
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 258, line 4229: ' -    one\\n\\n      two'", () => {
		const input = `
 -    one

      two
`;
		const expected = `
<ul>
<li>
<p>one</p>
<p>two</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 259, line 4251: '   > > 1.  one\\n>>\\n>>     two'", () => {
		const input = `
   > > 1.  one
>>
>>     two
`;
		const expected = `
<blockquote>
<blockquote>
<ol>
<li>
<p>one</p>
<p>two</p>
</li>
</ol>
</blockquote>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 260, line 4278: '>>- one\\n>>\\n  >  > two'", () => {
		const input = `
>>- one
>>
  >  > two
`;
		const expected = `
<blockquote>
<blockquote>
<ul>
<li>one</li>
</ul>
<p>two</p>
</blockquote>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 261, line 4297: '-one\\n\\n2.two'", () => {
		const input = `
-one

2.two
`;
		const expected = `
<p>-one</p>
<p>2.two</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 262, line 4310: '- foo\\n\\n\\n  bar'", () => {
		const input = `
- foo


  bar
`;
		const expected = `
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 263, line 4327: '1.  foo\\n\\n    ```\\n    bar\\n    ```\\n\\n    baz\\n\\n    > bam'", () => {
		const input = `
1.  foo

    \`\`\`
    bar
    \`\`\`

    baz

    > bam
`;
		const expected = `
<ol>
<li>
<p>foo</p>
<pre><code>bar
</code></pre>
<p>baz</p>
<blockquote>
<p>bam</p>
</blockquote>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 264, line 4355: '- Foo\\n\\n      bar\\n\\n\\n      baz'", () => {
		const input = `
- Foo

      bar


      baz
`;
		const expected = `
<ul>
<li>
<p>Foo</p>
<pre><code>bar


baz
</code></pre>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 265, line 4377: '123456789. ok'", () => {
		const input = `
123456789. ok
`;
		const expected = `
<ol start="123456789">
<li>ok</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 266, line 4386: '1234567890. not ok'", () => {
		const input = `
1234567890. not ok
`;
		const expected = `
<p>1234567890. not ok</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 267, line 4395: '0. ok'", () => {
		const input = `
0. ok
`;
		const expected = `
<ol start="0">
<li>ok</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 268, line 4404: '003. ok'", () => {
		const input = `
003. ok
`;
		const expected = `
<ol start="3">
<li>ok</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 269, line 4415: '-1. not ok'", () => {
		const input = `
-1. not ok
`;
		const expected = `
<p>-1. not ok</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 270, line 4438: '- foo\\n\\n      bar'", () => {
		const input = `
- foo

      bar
`;
		const expected = `
<ul>
<li>
<p>foo</p>
<pre><code>bar
</code></pre>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 271, line 4455: '  10.  foo\\n\\n           bar'", () => {
		const input = `
  10.  foo

           bar
`;
		const expected = `
<ol start="10">
<li>
<p>foo</p>
<pre><code>bar
</code></pre>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 272, line 4474: '    indented code\\n\\nparagraph\\n\\n    more code'", () => {
		const input = `
    indented code

paragraph

    more code
`;
		const expected = `
<pre><code>indented code
</code></pre>
<p>paragraph</p>
<pre><code>more code
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 273, line 4489: '1.     indented code\\n\\n   paragraph\\n\\n       more code'", () => {
		const input = `
1.     indented code

   paragraph

       more code
`;
		const expected = `
<ol>
<li>
<pre><code>indented code
</code></pre>
<p>paragraph</p>
<pre><code>more code
</code></pre>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 274, line 4511: '1.      indented code\\n\\n   paragraph\\n\\n       more code'", () => {
		const input = `
1.      indented code

   paragraph

       more code
`;
		const expected = `
<ol>
<li>
<pre><code> indented code
</code></pre>
<p>paragraph</p>
<pre><code>more code
</code></pre>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 275, line 4538: '   foo\\n\\nbar'", () => {
		const input = `
   foo

bar
`;
		const expected = `
<p>foo</p>
<p>bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 276, line 4548: '-    foo\\n\\n  bar'", () => {
		const input = `
-    foo

  bar
`;
		const expected = `
<ul>
<li>foo</li>
</ul>
<p>bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 277, line 4565: '-  foo\\n\\n   bar'", () => {
		const input = `
-  foo

   bar
`;
		const expected = `
<ul>
<li>
<p>foo</p>
<p>bar</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 278, line 4592: '-\\n  foo\\n-\\n  ```\\n  bar\\n  ```\\n-\\n      baz'", () => {
		const input = `
-
  foo
-
  \`\`\`
  bar
  \`\`\`
-
      baz
`;
		const expected = `
<ul>
<li>foo</li>
<li>
<pre><code>bar
</code></pre>
</li>
<li>
<pre><code>baz
</code></pre>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 279, line 4618: '-   \\n  foo'", () => {
		const input = `
-   
  foo
`;
		const expected = `
<ul>
<li>foo</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 280, line 4632: '-\\n\\n  foo'", () => {
		const input = `
-

  foo
`;
		const expected = `
<ul>
<li></li>
</ul>
<p>foo</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 281, line 4646: '- foo\\n-\\n- bar'", () => {
		const input = `
- foo
-
- bar
`;
		const expected = `
<ul>
<li>foo</li>
<li></li>
<li>bar</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 282, line 4661: '- foo\\n-   \\n- bar'", () => {
		const input = `
- foo
-   
- bar
`;
		const expected = `
<ul>
<li>foo</li>
<li></li>
<li>bar</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 283, line 4676: '1. foo\\n2.\\n3. bar'", () => {
		const input = `
1. foo
2.
3. bar
`;
		const expected = `
<ol>
<li>foo</li>
<li></li>
<li>bar</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 284, line 4691: '*'", () => {
		const input = `
*
`;
		const expected = `
<ul>
<li></li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 285, line 4701: 'foo\\n*\\n\\nfoo\\n1.'", () => {
		const input = `
foo
*

foo
1.
`;
		const expected = `
<p>foo
*</p>
<p>foo
1.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 286, line 4723: ' 1.  A paragraph\\n     with two lines.\\n\\n         indented code\\n\\n     > A block quote.'", () => {
		const input = `
 1.  A paragraph
     with two lines.

         indented code

     > A block quote.
`;
		const expected = `
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 287, line 4747: '  1.  A paragraph\\n      with two lines.\\n\\n          indented code\\n\\n      > A block quote.'", () => {
		const input = `
  1.  A paragraph
      with two lines.

          indented code

      > A block quote.
`;
		const expected = `
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 288, line 4771: '   1.  A paragraph\\n       with two lines.\\n\\n           indented code\\n\\n       > A block quote.'", () => {
		const input = `
   1.  A paragraph
       with two lines.

           indented code

       > A block quote.
`;
		const expected = `
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 289, line 4795: '    1.  A paragraph\\n        with two lines.\\n\\n            indented code\\n\\n        > A block quote.'", () => {
		const input = `
    1.  A paragraph
        with two lines.

            indented code

        > A block quote.
`;
		const expected = `
<pre><code>1.  A paragraph
    with two lines.

        indented code

    &gt; A block quote.
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 290, line 4825: '  1.  A paragraph\\nwith two lines.\\n\\n          indented code\\n\\n      > A block quote.'", () => {
		const input = `
  1.  A paragraph
with two lines.

          indented code

      > A block quote.
`;
		const expected = `
<ol>
<li>
<p>A paragraph
with two lines.</p>
<pre><code>indented code
</code></pre>
<blockquote>
<p>A block quote.</p>
</blockquote>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 291, line 4849: '  1.  A paragraph\\n    with two lines.'", () => {
		const input = `
  1.  A paragraph
    with two lines.
`;
		const expected = `
<ol>
<li>A paragraph
with two lines.</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 292, line 4862: '> 1. > Blockquote\\ncontinued here.'", () => {
		const input = `
> 1. > Blockquote
continued here.
`;
		const expected = `
<blockquote>
<ol>
<li>
<blockquote>
<p>Blockquote
continued here.</p>
</blockquote>
</li>
</ol>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 293, line 4879: '> 1. > Blockquote\\n> continued here.'", () => {
		const input = `
> 1. > Blockquote
> continued here.
`;
		const expected = `
<blockquote>
<ol>
<li>
<blockquote>
<p>Blockquote
continued here.</p>
</blockquote>
</li>
</ol>
</blockquote>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 294, line 4907: '- foo\\n  - bar\\n    - baz\\n      - boo'", () => {
		const input = `
- foo
  - bar
    - baz
      - boo
`;
		const expected = `
<ul>
<li>foo
<ul>
<li>bar
<ul>
<li>baz
<ul>
<li>boo</li>
</ul>
</li>
</ul>
</li>
</ul>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 295, line 4933: '- foo\\n - bar\\n  - baz\\n   - boo'", () => {
		const input = `
- foo
 - bar
  - baz
   - boo
`;
		const expected = `
<ul>
<li>foo</li>
<li>bar</li>
<li>baz</li>
<li>boo</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 296, line 4950: '10) foo\\n    - bar'", () => {
		const input = `
10) foo
    - bar
`;
		const expected = `
<ol start="10">
<li>foo
<ul>
<li>bar</li>
</ul>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 297, line 4966: '10) foo\\n   - bar'", () => {
		const input = `
10) foo
   - bar
`;
		const expected = `
<ol start="10">
<li>foo</li>
</ol>
<ul>
<li>bar</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 298, line 4981: '- - foo'", () => {
		const input = `
- - foo
`;
		const expected = `
<ul>
<li>
<ul>
<li>foo</li>
</ul>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 299, line 4994: '1. - 2. foo'", () => {
		const input = `
1. - 2. foo
`;
		const expected = `
<ol>
<li>
<ul>
<li>
<ol start="2">
<li>foo</li>
</ol>
</li>
</ul>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 300, line 5013: '- # Foo\\n- Bar\\n  ---\\n  baz'", () => {
		const input = `
- # Foo
- Bar
  ---
  baz
`;
		const expected = `
<ul>
<li>
<h1>Foo</h1>
</li>
<li>
<h2>Bar</h2>
baz</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 301, line 5249: '- foo\\n- bar\\n+ baz'", () => {
		const input = `
- foo
- bar
+ baz
`;
		const expected = `
<ul>
<li>foo</li>
<li>bar</li>
</ul>
<ul>
<li>baz</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 302, line 5264: '1. foo\\n2. bar\\n3) baz'", () => {
		const input = `
1. foo
2. bar
3) baz
`;
		const expected = `
<ol>
<li>foo</li>
<li>bar</li>
</ol>
<ol start="3">
<li>baz</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 303, line 5283: 'Foo\\n- bar\\n- baz'", () => {
		const input = `
Foo
- bar
- baz
`;
		const expected = `
<p>Foo</p>
<ul>
<li>bar</li>
<li>baz</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 304, line 5360: 'The number of windows in my house is\\n14.  The number of doors is 6.'", () => {
		const input = `
The number of windows in my house is
14.  The number of doors is 6.
`;
		const expected = `
<p>The number of windows in my house is
14.  The number of doors is 6.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 305, line 5370: 'The number of windows in my house is\\n1.  The number of doors is 6.'", () => {
		const input = `
The number of windows in my house is
1.  The number of doors is 6.
`;
		const expected = `
<p>The number of windows in my house is</p>
<ol>
<li>The number of doors is 6.</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 306, line 5384: '- foo\\n\\n- bar\\n\\n\\n- baz'", () => {
		const input = `
- foo

- bar


- baz
`;
		const expected = `
<ul>
<li>
<p>foo</p>
</li>
<li>
<p>bar</p>
</li>
<li>
<p>baz</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 307, line 5405: '- foo\\n  - bar\\n    - baz\\n\\n\\n      bim'", () => {
		const input = `
- foo
  - bar
    - baz


      bim
`;
		const expected = `
<ul>
<li>foo
<ul>
<li>bar
<ul>
<li>
<p>baz</p>
<p>bim</p>
</li>
</ul>
</li>
</ul>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 308, line 5435: '- foo\\n- bar\\n\\n<!-- -->\\n\\n- baz\\n- bim'", () => {
		const input = `
- foo
- bar

<!-- -->

- baz
- bim
`;
		const expected = `
<ul>
<li>foo</li>
<li>bar</li>
</ul>
<!-- -->
<ul>
<li>baz</li>
<li>bim</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 309, line 5456: '-   foo\\n\\n    notcode\\n\\n-   foo\\n\\n<!-- -->\\n\\n    code'", () => {
		const input = `
-   foo

    notcode

-   foo

<!-- -->

    code
`;
		const expected = `
<ul>
<li>
<p>foo</p>
<p>notcode</p>
</li>
<li>
<p>foo</p>
</li>
</ul>
<!-- -->
<pre><code>code
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 310, line 5487: '- a\\n - b\\n  - c\\n   - d\\n  - e\\n - f\\n- g'", () => {
		const input = `
- a
 - b
  - c
   - d
  - e
 - f
- g
`;
		const expected = `
<ul>
<li>a</li>
<li>b</li>
<li>c</li>
<li>d</li>
<li>e</li>
<li>f</li>
<li>g</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 311, line 5508: '1. a\\n\\n  2. b\\n\\n   3. c'", () => {
		const input = `
1. a

  2. b

   3. c
`;
		const expected = `
<ol>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
<li>
<p>c</p>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 312, line 5532: '- a\\n - b\\n  - c\\n   - d\\n    - e'", () => {
		const input = `
- a
 - b
  - c
   - d
    - e
`;
		const expected = `
<ul>
<li>a</li>
<li>b</li>
<li>c</li>
<li>d
- e</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 313, line 5552: '1. a\\n\\n  2. b\\n\\n    3. c'", () => {
		const input = `
1. a

  2. b

    3. c
`;
		const expected = `
<ol>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
</ol>
<pre><code>3. c
</code></pre>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 314, line 5575: '- a\\n- b\\n\\n- c'", () => {
		const input = `
- a
- b

- c
`;
		const expected = `
<ul>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
<li>
<p>c</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 315, line 5597: '* a\\n*\\n\\n* c'", () => {
		const input = `
* a
*

* c
`;
		const expected = `
<ul>
<li>
<p>a</p>
</li>
<li></li>
<li>
<p>c</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 316, line 5619: '- a\\n- b\\n\\n  c\\n- d'", () => {
		const input = `
- a
- b

  c
- d
`;
		const expected = `
<ul>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
<p>c</p>
</li>
<li>
<p>d</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 317, line 5641: '- a\\n- b\\n\\n  [ref]: /url\\n- d'", () => {
		const input = `
- a
- b

  [ref]: /url
- d
`;
		const expected = `
<ul>
<li>
<p>a</p>
</li>
<li>
<p>b</p>
</li>
<li>
<p>d</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 318, line 5664: '- a\\n- ```\\n  b\\n\\n\\n  ```\\n- c'", () => {
		const input = `
- a
- \`\`\`
  b


  \`\`\`
- c
`;
		const expected = `
<ul>
<li>a</li>
<li>
<pre><code>b


</code></pre>
</li>
<li>c</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 319, line 5690: '- a\\n  - b\\n\\n    c\\n- d'", () => {
		const input = `
- a
  - b

    c
- d
`;
		const expected = `
<ul>
<li>a
<ul>
<li>
<p>b</p>
<p>c</p>
</li>
</ul>
</li>
<li>d</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 320, line 5714: '* a\\n  > b\\n  >\\n* c'", () => {
		const input = `
* a
  > b
  >
* c
`;
		const expected = `
<ul>
<li>a
<blockquote>
<p>b</p>
</blockquote>
</li>
<li>c</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 321, line 5734: '- a\\n  > b\\n  ```\\n  c\\n  ```\\n- d'", () => {
		const input = `
- a
  > b
  \`\`\`
  c
  \`\`\`
- d
`;
		const expected = `
<ul>
<li>a
<blockquote>
<p>b</p>
</blockquote>
<pre><code>c
</code></pre>
</li>
<li>d</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 322, line 5757: '- a'", () => {
		const input = `
- a
`;
		const expected = `
<ul>
<li>a</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 323, line 5766: '- a\\n  - b'", () => {
		const input = `
- a
  - b
`;
		const expected = `
<ul>
<li>a
<ul>
<li>b</li>
</ul>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 324, line 5783: '1. ```\\n   foo\\n   ```\\n\\n   bar'", () => {
		const input = `
1. \`\`\`
   foo
   \`\`\`

   bar
`;
		const expected = `
<ol>
<li>
<pre><code>foo
</code></pre>
<p>bar</p>
</li>
</ol>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 325, line 5802: '* foo\\n  * bar\\n\\n  baz'", () => {
		const input = `
* foo
  * bar

  baz
`;
		const expected = `
<ul>
<li>
<p>foo</p>
<ul>
<li>bar</li>
</ul>
<p>baz</p>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 326, line 5820: '- a\\n  - b\\n  - c\\n\\n- d\\n  - e\\n  - f'", () => {
		const input = `
- a
  - b
  - c

- d
  - e
  - f
`;
		const expected = `
<ul>
<li>
<p>a</p>
<ul>
<li>b</li>
<li>c</li>
</ul>
</li>
<li>
<p>d</p>
<ul>
<li>e</li>
<li>f</li>
</ul>
</li>
</ul>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 327, line 5854: '`hi`lo`'", () => {
		const input = `
\`hi\`lo\`
`;
		const expected = `
<p><code>hi</code>lo\`</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 328, line 5886: '`foo`'", () => {
		const input = `
\`foo\`
`;
		const expected = `
<p><code>foo</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 329, line 5897: '`` foo ` bar ``'", () => {
		const input = `
\`\` foo \` bar \`\`
`;
		const expected = `
<p><code>foo \` bar</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 330, line 5907: '` `` `'", () => {
		const input = `
\` \`\` \`
`;
		const expected = `
<p><code>\`\`</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 331, line 5915: '`  ``  `'", () => {
		const input = `
\`  \`\`  \`
`;
		const expected = `
<p><code> \`\` </code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 332, line 5924: '` a`'", () => {
		const input = `
\` a\`
`;
		const expected = `
<p><code> a</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 333, line 5933: '` b `'", () => {
		const input = `
\` b \`
`;
		const expected = `
<p><code> b </code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 334, line 5941: '` `\\n`  `'", () => {
		const input = `
\` \`
\`  \`
`;
		const expected = `
<p><code> </code>
<code>  </code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 335, line 5952: '``\\nfoo\\nbar  \\nbaz\\n``'", () => {
		const input = `
\`\`
foo
bar  
baz
\`\`
`;
		const expected = `
<p><code>foo bar   baz</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 336, line 5962: '``\\nfoo \\n``'", () => {
		const input = `
\`\`
foo 
\`\`
`;
		const expected = `
<p><code>foo </code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 337, line 5973: '`foo   bar \\nbaz`'", () => {
		const input = `
\`foo   bar 
baz\`
`;
		const expected = `
<p><code>foo   bar  baz</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 338, line 5990: '`foo\\`bar`'", () => {
		const input = `
\`foo\\\`bar\`
`;
		const expected = `
<p><code>foo\\</code>bar\`</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 339, line 6001: '``foo`bar``'", () => {
		const input = `
\`\`foo\`bar\`\`
`;
		const expected = `
<p><code>foo\`bar</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 340, line 6007: '` foo `` bar `'", () => {
		const input = `
\` foo \`\` bar \`
`;
		const expected = `
<p><code>foo \`\` bar</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 341, line 6019: '*foo`*`'", () => {
		const input = `
*foo\`*\`
`;
		const expected = `
<p>*foo<code>*</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 342, line 6028: '[not a `link](/foo`)'", () => {
		const input = `
[not a \`link](/foo\`)
`;
		const expected = `
<p>[not a <code>link](/foo</code>)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 343, line 6038: '`<a href=\"`\">`'", () => {
		const input = `
\`<a href="\`">\`
`;
		const expected = `
<p><code>&lt;a href=&quot;</code>&quot;&gt;\`</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 344, line 6047: '<a href=\"`\">`'", () => {
		const input = `
<a href="\`">\`
`;
		const expected = `
<p><a href="\`">\`</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 345, line 6056: '`<https://foo.bar.`baz>`'", () => {
		const input = `
\`<https://foo.bar.\`baz>\`
`;
		const expected = `
<p><code>&lt;https://foo.bar.</code>baz&gt;\`</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 346, line 6065: '<https://foo.bar.`baz>`'", () => {
		const input = `
<https://foo.bar.\`baz>\`
`;
		const expected = `
<p><a href="https://foo.bar.%60baz">https://foo.bar.\`baz</a>\`</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 347, line 6075: '```foo``'", () => {
		const input = `
\`\`\`foo\`\`
`;
		const expected = `
<p>\`\`\`foo\`\`</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 348, line 6082: '`foo'", () => {
		const input = `
\`foo
`;
		const expected = `
<p>\`foo</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 349, line 6091: '`foo``bar``'", () => {
		const input = `
\`foo\`\`bar\`\`
`;
		const expected = `
<p>\`foo<code>bar</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 350, line 6308: '*foo bar*'", () => {
		const input = `
*foo bar*
`;
		const expected = `
<p><em>foo bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 351, line 6318: 'a * foo bar*'", () => {
		const input = `
a * foo bar*
`;
		const expected = `
<p>a * foo bar*</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 352, line 6329: 'a*\"foo\"*'", () => {
		const input = `
a*"foo"*
`;
		const expected = `
<p>a*&quot;foo&quot;*</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 353, line 6338: '* a *'", () => {
		const input = `
* a *
`;
		const expected = `
<p>* a *</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 354, line 6347: '*$*alpha.\\n\\n*£*bravo.\\n\\n*€*charlie.'", () => {
		const input = `
*$*alpha.

*£*bravo.

*€*charlie.
`;
		const expected = `
<p>*$*alpha.</p>
<p>*£*bravo.</p>
<p>*€*charlie.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 355, line 6362: 'foo*bar*'", () => {
		const input = `
foo*bar*
`;
		const expected = `
<p>foo<em>bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 356, line 6369: '5*6*78'", () => {
		const input = `
5*6*78
`;
		const expected = `
<p>5<em>6</em>78</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 357, line 6378: '_foo bar_'", () => {
		const input = `
_foo bar_
`;
		const expected = `
<p><em>foo bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 358, line 6388: '_ foo bar_'", () => {
		const input = `
_ foo bar_
`;
		const expected = `
<p>_ foo bar_</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 359, line 6398: 'a_\"foo\"_'", () => {
		const input = `
a_"foo"_
`;
		const expected = `
<p>a_&quot;foo&quot;_</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 360, line 6407: 'foo_bar_'", () => {
		const input = `
foo_bar_
`;
		const expected = `
<p>foo_bar_</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 361, line 6414: '5_6_78'", () => {
		const input = `
5_6_78
`;
		const expected = `
<p>5_6_78</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 362, line 6421: 'пристаням_стремятся_'", () => {
		const input = `
пристаням_стремятся_
`;
		const expected = `
<p>пристаням_стремятся_</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 363, line 6431: 'aa_\"bb\"_cc'", () => {
		const input = `
aa_"bb"_cc
`;
		const expected = `
<p>aa_&quot;bb&quot;_cc</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 364, line 6442: 'foo-_(bar)_'", () => {
		const input = `
foo-_(bar)_
`;
		const expected = `
<p>foo-<em>(bar)</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 365, line 6454: '_foo*'", () => {
		const input = `
_foo*
`;
		const expected = `
<p>_foo*</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 366, line 6464: '*foo bar *'", () => {
		const input = `
*foo bar *
`;
		const expected = `
<p>*foo bar *</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 367, line 6473: '*foo bar\\n*'", () => {
		const input = `
*foo bar
*
`;
		const expected = `
<p>*foo bar
*</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 368, line 6486: '*(*foo)'", () => {
		const input = `
*(*foo)
`;
		const expected = `
<p>*(*foo)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 369, line 6496: '*(*foo*)*'", () => {
		const input = `
*(*foo*)*
`;
		const expected = `
<p><em>(<em>foo</em>)</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 370, line 6505: '*foo*bar'", () => {
		const input = `
*foo*bar
`;
		const expected = `
<p><em>foo</em>bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 371, line 6518: '_foo bar _'", () => {
		const input = `
_foo bar _
`;
		const expected = `
<p>_foo bar _</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 372, line 6528: '_(_foo)'", () => {
		const input = `
_(_foo)
`;
		const expected = `
<p>_(_foo)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 373, line 6537: '_(_foo_)_'", () => {
		const input = `
_(_foo_)_
`;
		const expected = `
<p><em>(<em>foo</em>)</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 374, line 6546: '_foo_bar'", () => {
		const input = `
_foo_bar
`;
		const expected = `
<p>_foo_bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 375, line 6553: '_пристаням_стремятся'", () => {
		const input = `
_пристаням_стремятся
`;
		const expected = `
<p>_пристаням_стремятся</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 376, line 6560: '_foo_bar_baz_'", () => {
		const input = `
_foo_bar_baz_
`;
		const expected = `
<p><em>foo_bar_baz</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 377, line 6571: '_(bar)_.'", () => {
		const input = `
_(bar)_.
`;
		const expected = `
<p><em>(bar)</em>.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 378, line 6580: '**foo bar**'", () => {
		const input = `
**foo bar**
`;
		const expected = `
<p><strong>foo bar</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 379, line 6590: '** foo bar**'", () => {
		const input = `
** foo bar**
`;
		const expected = `
<p>** foo bar**</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 380, line 6601: 'a**\"foo\"**'", () => {
		const input = `
a**"foo"**
`;
		const expected = `
<p>a**&quot;foo&quot;**</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 381, line 6610: 'foo**bar**'", () => {
		const input = `
foo**bar**
`;
		const expected = `
<p>foo<strong>bar</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 382, line 6619: '__foo bar__'", () => {
		const input = `
__foo bar__
`;
		const expected = `
<p><strong>foo bar</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 383, line 6629: '__ foo bar__'", () => {
		const input = `
__ foo bar__
`;
		const expected = `
<p>__ foo bar__</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 384, line 6637: '__\\nfoo bar__'", () => {
		const input = `
__
foo bar__
`;
		const expected = `
<p>__
foo bar__</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 385, line 6649: 'a__\"foo\"__'", () => {
		const input = `
a__"foo"__
`;
		const expected = `
<p>a__&quot;foo&quot;__</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 386, line 6658: 'foo__bar__'", () => {
		const input = `
foo__bar__
`;
		const expected = `
<p>foo__bar__</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 387, line 6665: '5__6__78'", () => {
		const input = `
5__6__78
`;
		const expected = `
<p>5__6__78</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 388, line 6672: 'пристаням__стремятся__'", () => {
		const input = `
пристаням__стремятся__
`;
		const expected = `
<p>пристаням__стремятся__</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 389, line 6679: '__foo, __bar__, baz__'", () => {
		const input = `
__foo, __bar__, baz__
`;
		const expected = `
<p><strong>foo, <strong>bar</strong>, baz</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 390, line 6690: 'foo-__(bar)__'", () => {
		const input = `
foo-__(bar)__
`;
		const expected = `
<p>foo-<strong>(bar)</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 391, line 6703: '**foo bar **'", () => {
		const input = `
**foo bar **
`;
		const expected = `
<p>**foo bar **</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 392, line 6716: '**(**foo)'", () => {
		const input = `
**(**foo)
`;
		const expected = `
<p>**(**foo)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 393, line 6726: '*(**foo**)*'", () => {
		const input = `
*(**foo**)*
`;
		const expected = `
<p><em>(<strong>foo</strong>)</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 394, line 6733: '**Gomphocarpus (*Gomphocarpus physocarpus*, syn.\\n*Asclepias physocarpa*)**'", () => {
		const input = `
**Gomphocarpus (*Gomphocarpus physocarpus*, syn.
*Asclepias physocarpa*)**
`;
		const expected = `
<p><strong>Gomphocarpus (<em>Gomphocarpus physocarpus</em>, syn.
<em>Asclepias physocarpa</em>)</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 395, line 6742: '**foo \"*bar*\" foo**'", () => {
		const input = `
**foo "*bar*" foo**
`;
		const expected = `
<p><strong>foo &quot;<em>bar</em>&quot; foo</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 396, line 6751: '**foo**bar'", () => {
		const input = `
**foo**bar
`;
		const expected = `
<p><strong>foo</strong>bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 397, line 6763: '__foo bar __'", () => {
		const input = `
__foo bar __
`;
		const expected = `
<p>__foo bar __</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 398, line 6773: '__(__foo)'", () => {
		const input = `
__(__foo)
`;
		const expected = `
<p>__(__foo)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 399, line 6783: '_(__foo__)_'", () => {
		const input = `
_(__foo__)_
`;
		const expected = `
<p><em>(<strong>foo</strong>)</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 400, line 6792: '__foo__bar'", () => {
		const input = `
__foo__bar
`;
		const expected = `
<p>__foo__bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 401, line 6799: '__пристаням__стремятся'", () => {
		const input = `
__пристаням__стремятся
`;
		const expected = `
<p>__пристаням__стремятся</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 402, line 6806: '__foo__bar__baz__'", () => {
		const input = `
__foo__bar__baz__
`;
		const expected = `
<p><strong>foo__bar__baz</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 403, line 6817: '__(bar)__.'", () => {
		const input = `
__(bar)__.
`;
		const expected = `
<p><strong>(bar)</strong>.</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 404, line 6829: '*foo [bar](/url)*'", () => {
		const input = `
*foo [bar](/url)*
`;
		const expected = `
<p><em>foo <a href="/url">bar</a></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 405, line 6836: '*foo\\nbar*'", () => {
		const input = `
*foo
bar*
`;
		const expected = `
<p><em>foo
bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 406, line 6848: '_foo __bar__ baz_'", () => {
		const input = `
_foo __bar__ baz_
`;
		const expected = `
<p><em>foo <strong>bar</strong> baz</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 407, line 6855: '_foo _bar_ baz_'", () => {
		const input = `
_foo _bar_ baz_
`;
		const expected = `
<p><em>foo <em>bar</em> baz</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 408, line 6862: '__foo_ bar_'", () => {
		const input = `
__foo_ bar_
`;
		const expected = `
<p><em><em>foo</em> bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 409, line 6869: '*foo *bar**'", () => {
		const input = `
*foo *bar**
`;
		const expected = `
<p><em>foo <em>bar</em></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 410, line 6876: '*foo **bar** baz*'", () => {
		const input = `
*foo **bar** baz*
`;
		const expected = `
<p><em>foo <strong>bar</strong> baz</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 411, line 6882: '*foo**bar**baz*'", () => {
		const input = `
*foo**bar**baz*
`;
		const expected = `
<p><em>foo<strong>bar</strong>baz</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 412, line 6906: '*foo**bar*'", () => {
		const input = `
*foo**bar*
`;
		const expected = `
<p><em>foo**bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 413, line 6919: '***foo** bar*'", () => {
		const input = `
***foo** bar*
`;
		const expected = `
<p><em><strong>foo</strong> bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 414, line 6926: '*foo **bar***'", () => {
		const input = `
*foo **bar***
`;
		const expected = `
<p><em>foo <strong>bar</strong></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 415, line 6933: '*foo**bar***'", () => {
		const input = `
*foo**bar***
`;
		const expected = `
<p><em>foo<strong>bar</strong></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 416, line 6944: 'foo***bar***baz'", () => {
		const input = `
foo***bar***baz
`;
		const expected = `
<p>foo<em><strong>bar</strong></em>baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 417, line 6950: 'foo******bar*********baz'", () => {
		const input = `
foo******bar*********baz
`;
		const expected = `
<p>foo<strong><strong><strong>bar</strong></strong></strong>***baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 418, line 6959: '*foo **bar *baz* bim** bop*'", () => {
		const input = `
*foo **bar *baz* bim** bop*
`;
		const expected = `
<p><em>foo <strong>bar <em>baz</em> bim</strong> bop</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 419, line 6966: '*foo [*bar*](/url)*'", () => {
		const input = `
*foo [*bar*](/url)*
`;
		const expected = `
<p><em>foo <a href="/url"><em>bar</em></a></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 420, line 6975: '** is not an empty emphasis'", () => {
		const input = `
** is not an empty emphasis
`;
		const expected = `
<p>** is not an empty emphasis</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 421, line 6982: '**** is not an empty strong emphasis'", () => {
		const input = `
**** is not an empty strong emphasis
`;
		const expected = `
<p>**** is not an empty strong emphasis</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 422, line 6995: '**foo [bar](/url)**'", () => {
		const input = `
**foo [bar](/url)**
`;
		const expected = `
<p><strong>foo <a href="/url">bar</a></strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 423, line 7002: '**foo\\nbar**'", () => {
		const input = `
**foo
bar**
`;
		const expected = `
<p><strong>foo
bar</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 424, line 7014: '__foo _bar_ baz__'", () => {
		const input = `
__foo _bar_ baz__
`;
		const expected = `
<p><strong>foo <em>bar</em> baz</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 425, line 7021: '__foo __bar__ baz__'", () => {
		const input = `
__foo __bar__ baz__
`;
		const expected = `
<p><strong>foo <strong>bar</strong> baz</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 426, line 7028: '____foo__ bar__'", () => {
		const input = `
____foo__ bar__
`;
		const expected = `
<p><strong><strong>foo</strong> bar</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 427, line 7035: '**foo **bar****'", () => {
		const input = `
**foo **bar****
`;
		const expected = `
<p><strong>foo <strong>bar</strong></strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 428, line 7042: '**foo *bar* baz**'", () => {
		const input = `
**foo *bar* baz**
`;
		const expected = `
<p><strong>foo <em>bar</em> baz</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 429, line 7049: '**foo*bar*baz**'", () => {
		const input = `
**foo*bar*baz**
`;
		const expected = `
<p><strong>foo<em>bar</em>baz</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 430, line 7056: '***foo* bar**'", () => {
		const input = `
***foo* bar**
`;
		const expected = `
<p><strong><em>foo</em> bar</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 431, line 7063: '**foo *bar***'", () => {
		const input = `
**foo *bar***
`;
		const expected = `
<p><strong>foo <em>bar</em></strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 432, line 7072: '**foo *bar **baz**\\nbim* bop**'", () => {
		const input = `
**foo *bar **baz**
bim* bop**
`;
		const expected = `
<p><strong>foo <em>bar <strong>baz</strong>
bim</em> bop</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 433, line 7081: '**foo [*bar*](/url)**'", () => {
		const input = `
**foo [*bar*](/url)**
`;
		const expected = `
<p><strong>foo <a href="/url"><em>bar</em></a></strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 434, line 7090: '__ is not an empty emphasis'", () => {
		const input = `
__ is not an empty emphasis
`;
		const expected = `
<p>__ is not an empty emphasis</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 435, line 7097: '____ is not an empty strong emphasis'", () => {
		const input = `
____ is not an empty strong emphasis
`;
		const expected = `
<p>____ is not an empty strong emphasis</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 436, line 7107: 'foo ***'", () => {
		const input = `
foo ***
`;
		const expected = `
<p>foo ***</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 437, line 7114: 'foo *\\**'", () => {
		const input = `
foo *\\**
`;
		const expected = `
<p>foo <em>*</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 438, line 7121: 'foo *_*'", () => {
		const input = `
foo *_*
`;
		const expected = `
<p>foo <em>_</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 439, line 7128: 'foo *****'", () => {
		const input = `
foo *****
`;
		const expected = `
<p>foo *****</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 440, line 7135: 'foo **\\***'", () => {
		const input = `
foo **\\***
`;
		const expected = `
<p>foo <strong>*</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 441, line 7142: 'foo **_**'", () => {
		const input = `
foo **_**
`;
		const expected = `
<p>foo <strong>_</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 442, line 7153: '**foo*'", () => {
		const input = `
**foo*
`;
		const expected = `
<p>*<em>foo</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 443, line 7160: '*foo**'", () => {
		const input = `
*foo**
`;
		const expected = `
<p><em>foo</em>*</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 444, line 7167: '***foo**'", () => {
		const input = `
***foo**
`;
		const expected = `
<p>*<strong>foo</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 445, line 7174: '****foo*'", () => {
		const input = `
****foo*
`;
		const expected = `
<p>***<em>foo</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 446, line 7181: '**foo***'", () => {
		const input = `
**foo***
`;
		const expected = `
<p><strong>foo</strong>*</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 447, line 7188: '*foo****'", () => {
		const input = `
*foo****
`;
		const expected = `
<p><em>foo</em>***</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 448, line 7198: 'foo ___'", () => {
		const input = `
foo ___
`;
		const expected = `
<p>foo ___</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 449, line 7205: 'foo _\\__'", () => {
		const input = `
foo _\\__
`;
		const expected = `
<p>foo <em>_</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 450, line 7212: 'foo _*_'", () => {
		const input = `
foo _*_
`;
		const expected = `
<p>foo <em>*</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 451, line 7219: 'foo _____'", () => {
		const input = `
foo _____
`;
		const expected = `
<p>foo _____</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 452, line 7226: 'foo __\\___'", () => {
		const input = `
foo __\\___
`;
		const expected = `
<p>foo <strong>_</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 453, line 7233: 'foo __*__'", () => {
		const input = `
foo __*__
`;
		const expected = `
<p>foo <strong>*</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 454, line 7240: '__foo_'", () => {
		const input = `
__foo_
`;
		const expected = `
<p>_<em>foo</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 455, line 7251: '_foo__'", () => {
		const input = `
_foo__
`;
		const expected = `
<p><em>foo</em>_</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 456, line 7258: '___foo__'", () => {
		const input = `
___foo__
`;
		const expected = `
<p>_<strong>foo</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 457, line 7265: '____foo_'", () => {
		const input = `
____foo_
`;
		const expected = `
<p>___<em>foo</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 458, line 7272: '__foo___'", () => {
		const input = `
__foo___
`;
		const expected = `
<p><strong>foo</strong>_</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 459, line 7279: '_foo____'", () => {
		const input = `
_foo____
`;
		const expected = `
<p><em>foo</em>___</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 460, line 7289: '**foo**'", () => {
		const input = `
**foo**
`;
		const expected = `
<p><strong>foo</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 461, line 7296: '*_foo_*'", () => {
		const input = `
*_foo_*
`;
		const expected = `
<p><em><em>foo</em></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 462, line 7303: '__foo__'", () => {
		const input = `
__foo__
`;
		const expected = `
<p><strong>foo</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 463, line 7310: '_*foo*_'", () => {
		const input = `
_*foo*_
`;
		const expected = `
<p><em><em>foo</em></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 464, line 7320: '****foo****'", () => {
		const input = `
****foo****
`;
		const expected = `
<p><strong><strong>foo</strong></strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 465, line 7327: '____foo____'", () => {
		const input = `
____foo____
`;
		const expected = `
<p><strong><strong>foo</strong></strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 466, line 7338: '******foo******'", () => {
		const input = `
******foo******
`;
		const expected = `
<p><strong><strong><strong>foo</strong></strong></strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 467, line 7347: '***foo***'", () => {
		const input = `
***foo***
`;
		const expected = `
<p><em><strong>foo</strong></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 468, line 7354: '_____foo_____'", () => {
		const input = `
_____foo_____
`;
		const expected = `
<p><em><strong><strong>foo</strong></strong></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 469, line 7363: '*foo _bar* baz_'", () => {
		const input = `
*foo _bar* baz_
`;
		const expected = `
<p><em>foo _bar</em> baz_</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 470, line 7370: '*foo __bar *baz bim__ bam*'", () => {
		const input = `
*foo __bar *baz bim__ bam*
`;
		const expected = `
<p><em>foo <strong>bar *baz bim</strong> bam</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 471, line 7379: '**foo **bar baz**'", () => {
		const input = `
**foo **bar baz**
`;
		const expected = `
<p>**foo <strong>bar baz</strong></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 472, line 7386: '*foo *bar baz*'", () => {
		const input = `
*foo *bar baz*
`;
		const expected = `
<p>*foo <em>bar baz</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 473, line 7395: '*[bar*](/url)'", () => {
		const input = `
*[bar*](/url)
`;
		const expected = `
<p>*<a href="/url">bar*</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 474, line 7402: '_foo [bar_](/url)'", () => {
		const input = `
_foo [bar_](/url)
`;
		const expected = `
<p>_foo <a href="/url">bar_</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test('Example 475, line 7409: \'*<img src="foo" title="*"/>\'', () => {
		const input = `
*<img src="foo" title="*"/>
`;
		const expected = `
<p>*<img src="foo" title="*"/></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 476, line 7416: '**<a href=\"**\">'", () => {
		const input = `
**<a href="**">
`;
		const expected = `
<p>**<a href="**"></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 477, line 7423: '__<a href=\"__\">'", () => {
		const input = `
__<a href="__">
`;
		const expected = `
<p>__<a href="__"></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 478, line 7430: '*a `*`*'", () => {
		const input = `
*a \`*\`*
`;
		const expected = `
<p><em>a <code>*</code></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 479, line 7437: '_a `_`_'", () => {
		const input = `
_a \`_\`_
`;
		const expected = `
<p><em>a <code>_</code></em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 480, line 7444: '**a<https://foo.bar/?q=**>'", () => {
		const input = `
**a<https://foo.bar/?q=**>
`;
		const expected = `
<p>**a<a href="https://foo.bar/?q=**">https://foo.bar/?q=**</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 481, line 7451: '__a<https://foo.bar/?q=__>'", () => {
		const input = `
__a<https://foo.bar/?q=__>
`;
		const expected = `
<p>__a<a href="https://foo.bar/?q=__">https://foo.bar/?q=__</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 482, line 7539: '[link](/uri \"title\")'", () => {
		const input = `
[link](/uri "title")
`;
		const expected = `
<p><a href="/uri" title="title">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 483, line 7549: '[link](/uri)'", () => {
		const input = `
[link](/uri)
`;
		const expected = `
<p><a href="/uri">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 484, line 7555: '[](./target.md)'", () => {
		const input = `
[](./target.md)
`;
		const expected = `
<p><a href="./target.md"></a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 485, line 7562: '[link]()'", () => {
		const input = `
[link]()
`;
		const expected = `
<p><a href="">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 486, line 7569: '[link](<>)'", () => {
		const input = `
[link](<>)
`;
		const expected = `
<p><a href="">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 487, line 7576: '[]()'", () => {
		const input = `
[]()
`;
		const expected = `
<p><a href=""></a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 488, line 7585: '[link](/my uri)'", () => {
		const input = `
[link](/my uri)
`;
		const expected = `
<p>[link](/my uri)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 489, line 7591: '[link](</my uri>)'", () => {
		const input = `
[link](</my uri>)
`;
		const expected = `
<p><a href="/my%20uri">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 490, line 7600: '[link](foo\\nbar)'", () => {
		const input = `
[link](foo
bar)
`;
		const expected = `
<p>[link](foo
bar)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 491, line 7608: '[link](<foo\\nbar>)'", () => {
		const input = `
[link](<foo
bar>)
`;
		const expected = `
<p>[link](<foo
bar>)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 492, line 7619: '[a](<b)c>)'", () => {
		const input = `
[a](<b)c>)
`;
		const expected = `
<p><a href="b)c">a</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 493, line 7627: '[link](<foo\\>)'", () => {
		const input = `
[link](<foo\\>)
`;
		const expected = `
<p>[link](&lt;foo&gt;)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 494, line 7636: '[a](<b)c\\n[a](<b)c>\\n[a](<b>c)'", () => {
		const input = `
[a](<b)c
[a](<b)c>
[a](<b>c)
`;
		const expected = `
<p>[a](&lt;b)c
[a](&lt;b)c&gt;
[a](<b>c)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 495, line 7648: '[link](\\(foo\\))'", () => {
		const input = `
[link](\\(foo\\))
`;
		const expected = `
<p><a href="(foo)">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 496, line 7657: '[link](foo(and(bar)))'", () => {
		const input = `
[link](foo(and(bar)))
`;
		const expected = `
<p><a href="foo(and(bar))">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 497, line 7666: '[link](foo(and(bar))'", () => {
		const input = `
[link](foo(and(bar))
`;
		const expected = `
<p>[link](foo(and(bar))</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 498, line 7673: '[link](foo\\(and\\(bar\\))'", () => {
		const input = `
[link](foo\\(and\\(bar\\))
`;
		const expected = `
<p><a href="foo(and(bar)">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 499, line 7680: '[link](<foo(and(bar)>)'", () => {
		const input = `
[link](<foo(and(bar)>)
`;
		const expected = `
<p><a href="foo(and(bar)">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 500, line 7690: '[link](foo\\)\\:)'", () => {
		const input = `
[link](foo\\)\\:)
`;
		const expected = `
<p><a href="foo):">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 501, line 7699: '[link](#fragment)\\n\\n[link](https://example.com#fragment)\\n\\n[link](https://example.com?foo=3#frag)'", () => {
		const input = `
[link](#fragment)

[link](https://example.com#fragment)

[link](https://example.com?foo=3#frag)
`;
		const expected = `
<p><a href="#fragment">link</a></p>
<p><a href="https://example.com#fragment">link</a></p>
<p><a href="https://example.com?foo=3#frag">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 502, line 7715: '[link](foo\\bar)'", () => {
		const input = `
[link](foo\\bar)
`;
		const expected = `
<p><a href="foo%5Cbar">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 503, line 7731: '[link](foo%20b&auml;)'", () => {
		const input = `
[link](foo%20b&auml;)
`;
		const expected = `
<p><a href="foo%20b%C3%A4">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 504, line 7742: '[link](\"title\")'", () => {
		const input = `
[link]("title")
`;
		const expected = `
<p><a href="%22title%22">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 505, line 7751: '[link](/url \"title\")\\n[link](/url 'title')\\n[link](/url (title))'", () => {
		const input = `
[link](/url "title")
[link](/url 'title')
[link](/url (title))
`;
		const expected = `
<p><a href="/url" title="title">link</a>
<a href="/url" title="title">link</a>
<a href="/url" title="title">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test('Example 506, line 7765: \'[link](/url "title \\"&quot;")\'', () => {
		const input = `
[link](/url "title \\"&quot;")
`;
		const expected = `
<p><a href="/url" title="title &quot;&quot;">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 507, line 7776: '[link](/url \"title\")'", () => {
		const input = `
[link](/url "title")
`;
		const expected = `
<p><a href="/url%C2%A0%22title%22">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test('Example 508, line 7785: \'[link](/url "title "and" title")\'', () => {
		const input = `
[link](/url "title "and" title")
`;
		const expected = `
<p>[link](/url &quot;title &quot;and&quot; title&quot;)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 509, line 7794: '[link](/url 'title \"and\" title')'", () => {
		const input = `
[link](/url 'title "and" title')
`;
		const expected = `
<p><a href="/url" title="title &quot;and&quot; title">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 510, line 7819: '[link](   /uri\\n  \"title\"  )'", () => {
		const input = `
[link](   /uri
  "title"  )
`;
		const expected = `
<p><a href="/uri" title="title">link</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 511, line 7830: '[link] (/uri)'", () => {
		const input = `
[link] (/uri)
`;
		const expected = `
<p>[link] (/uri)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 512, line 7840: '[link [foo [bar]]](/uri)'", () => {
		const input = `
[link [foo [bar]]](/uri)
`;
		const expected = `
<p><a href="/uri">link [foo [bar]]</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 513, line 7847: '[link] bar](/uri)'", () => {
		const input = `
[link] bar](/uri)
`;
		const expected = `
<p>[link] bar](/uri)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 514, line 7854: '[link [bar](/uri)'", () => {
		const input = `
[link [bar](/uri)
`;
		const expected = `
<p>[link <a href="/uri">bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 515, line 7861: '[link \\[bar](/uri)'", () => {
		const input = `
[link \\[bar](/uri)
`;
		const expected = `
<p><a href="/uri">link [bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 516, line 7870: '[link *foo **bar** `#`*](/uri)'", () => {
		const input = `
[link *foo **bar** \`#\`*](/uri)
`;
		const expected = `
<p><a href="/uri">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 517, line 7877: '[![moon](moon.jpg)](/uri)'", () => {
		const input = `
[![moon](moon.jpg)](/uri)
`;
		const expected = `
<p><a href="/uri"><img src="moon.jpg" alt="moon" /></a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 518, line 7886: '[foo [bar](/uri)](/uri)'", () => {
		const input = `
[foo [bar](/uri)](/uri)
`;
		const expected = `
<p>[foo <a href="/uri">bar</a>](/uri)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 519, line 7893: '[foo *[bar [baz](/uri)](/uri)*](/uri)'", () => {
		const input = `
[foo *[bar [baz](/uri)](/uri)*](/uri)
`;
		const expected = `
<p>[foo <em>[bar <a href="/uri">baz</a>](/uri)</em>](/uri)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 520, line 7900: '![[[foo](uri1)](uri2)](uri3)'", () => {
		const input = `
![[[foo](uri1)](uri2)](uri3)
`;
		const expected = `
<p><img src="uri3" alt="[foo](uri2)" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 521, line 7910: '*[foo*](/uri)'", () => {
		const input = `
*[foo*](/uri)
`;
		const expected = `
<p>*<a href="/uri">foo*</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 522, line 7917: '[foo *bar](baz*)'", () => {
		const input = `
[foo *bar](baz*)
`;
		const expected = `
<p><a href="baz*">foo *bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 523, line 7927: '*foo [bar* baz]'", () => {
		const input = `
*foo [bar* baz]
`;
		const expected = `
<p><em>foo [bar</em> baz]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 524, line 7937: '[foo <bar attr=\"](baz)\">'", () => {
		const input = `
[foo <bar attr="](baz)">
`;
		const expected = `
<p>[foo <bar attr="](baz)"></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 525, line 7944: '[foo`](/uri)`'", () => {
		const input = `
[foo\`](/uri)\`
`;
		const expected = `
<p>[foo<code>](/uri)</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 526, line 7951: '[foo<https://example.com/?search=](uri)>'", () => {
		const input = `
[foo<https://example.com/?search=](uri)>
`;
		const expected = `
<p>[foo<a href="https://example.com/?search=%5D(uri)">https://example.com/?search=](uri)</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 527, line 7989: '[foo][bar]\\n\\n[bar]: /url \"title\"'", () => {
		const input = `
[foo][bar]

[bar]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 528, line 8004: '[link [foo [bar]]][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[link [foo [bar]]][ref]

[ref]: /uri
`;
		const expected = `
<p><a href="/uri">link [foo [bar]]</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 529, line 8013: '[link \\[bar][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[link \\[bar][ref]

[ref]: /uri
`;
		const expected = `
<p><a href="/uri">link [bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 530, line 8024: '[link *foo **bar** `#`*][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[link *foo **bar** \`#\`*][ref]

[ref]: /uri
`;
		const expected = `
<p><a href="/uri">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 531, line 8033: '[![moon](moon.jpg)][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[![moon](moon.jpg)][ref]

[ref]: /uri
`;
		const expected = `
<p><a href="/uri"><img src="moon.jpg" alt="moon" /></a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 532, line 8044: '[foo [bar](/uri)][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[foo [bar](/uri)][ref]

[ref]: /uri
`;
		const expected = `
<p>[foo <a href="/uri">bar</a>]<a href="/uri">ref</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 533, line 8053: '[foo *bar [baz][ref]*][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[foo *bar [baz][ref]*][ref]

[ref]: /uri
`;
		const expected = `
<p>[foo <em>bar <a href="/uri">baz</a></em>]<a href="/uri">ref</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 534, line 8068: '*[foo*][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
*[foo*][ref]

[ref]: /uri
`;
		const expected = `
<p>*<a href="/uri">foo*</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 535, line 8077: '[foo *bar][ref]*\\n\\n[ref]: /uri'", () => {
		const input = `
[foo *bar][ref]*

[ref]: /uri
`;
		const expected = `
<p><a href="/uri">foo *bar</a>*</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 536, line 8089: '[foo <bar attr=\"][ref]\">\\n\\n[ref]: /uri'", () => {
		const input = `
[foo <bar attr="][ref]">

[ref]: /uri
`;
		const expected = `
<p>[foo <bar attr="][ref]"></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 537, line 8098: '[foo`][ref]`\\n\\n[ref]: /uri'", () => {
		const input = `
[foo\`][ref]\`

[ref]: /uri
`;
		const expected = `
<p>[foo<code>][ref]</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 538, line 8107: '[foo<https://example.com/?search=][ref]>\\n\\n[ref]: /uri'", () => {
		const input = `
[foo<https://example.com/?search=][ref]>

[ref]: /uri
`;
		const expected = `
<p>[foo<a href="https://example.com/?search=%5D%5Bref%5D">https://example.com/?search=][ref]</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 539, line 8118: '[foo][BaR]\\n\\n[bar]: /url \"title\"'", () => {
		const input = `
[foo][BaR]

[bar]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 540, line 8129: '[ẞ]\\n\\n[SS]: /url'", () => {
		const input = `
[ẞ]

[SS]: /url
`;
		const expected = `
<p><a href="/url">ẞ</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 541, line 8141: '[Foo\\n  bar]: /url\\n\\n[Baz][Foo bar]'", () => {
		const input = `
[Foo
  bar]: /url

[Baz][Foo bar]
`;
		const expected = `
<p><a href="/url">Baz</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 542, line 8154: '[foo] [bar]\\n\\n[bar]: /url \"title\"'", () => {
		const input = `
[foo] [bar]

[bar]: /url "title"
`;
		const expected = `
<p>[foo] <a href="/url" title="title">bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 543, line 8163: '[foo]\\n[bar]\\n\\n[bar]: /url \"title\"'", () => {
		const input = `
[foo]
[bar]

[bar]: /url "title"
`;
		const expected = `
<p>[foo]
<a href="/url" title="title">bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 544, line 8204: '[foo]: /url1\\n\\n[foo]: /url2\\n\\n[bar][foo]'", () => {
		const input = `
[foo]: /url1

[foo]: /url2

[bar][foo]
`;
		const expected = `
<p><a href="/url1">bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 545, line 8219: '[bar][foo\\!]\\n\\n[foo!]: /url'", () => {
		const input = `
[bar][foo\\!]

[foo!]: /url
`;
		const expected = `
<p>[bar][foo!]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 546, line 8231: '[foo][ref[]\\n\\n[ref[]: /uri'", () => {
		const input = `
[foo][ref[]

[ref[]: /uri
`;
		const expected = `
<p>[foo][ref[]</p>
<p>[ref[]: /uri</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 547, line 8241: '[foo][ref[bar]]\\n\\n[ref[bar]]: /uri'", () => {
		const input = `
[foo][ref[bar]]

[ref[bar]]: /uri
`;
		const expected = `
<p>[foo][ref[bar]]</p>
<p>[ref[bar]]: /uri</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 548, line 8251: '[[[foo]]]\\n\\n[[[foo]]]: /url'", () => {
		const input = `
[[[foo]]]

[[[foo]]]: /url
`;
		const expected = `
<p>[[[foo]]]</p>
<p>[[[foo]]]: /url</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 549, line 8261: '[foo][ref\\[]\\n\\n[ref\\[]: /uri'", () => {
		const input = `
[foo][ref\\[]

[ref\\[]: /uri
`;
		const expected = `
<p><a href="/uri">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 550, line 8272: '[bar\\\\]: /uri\\n\\n[bar\\\\]'", () => {
		const input = `
[bar\\\\]: /uri

[bar\\\\]
`;
		const expected = `
<p><a href="/uri">bar\\</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 551, line 8284: '[]\\n\\n[]: /uri'", () => {
		const input = `
[]

[]: /uri
`;
		const expected = `
<p>[]</p>
<p>[]: /uri</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 552, line 8294: '[\\n ]\\n\\n[\\n ]: /uri'", () => {
		const input = `
[
 ]

[
 ]: /uri
`;
		const expected = `
<p>[
]</p>
<p>[
]: /uri</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 553, line 8317: '[foo][]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[foo][]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 554, line 8326: '[*foo* bar][]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
[*foo* bar][]

[*foo* bar]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title"><em>foo</em> bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 555, line 8337: '[Foo][]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[Foo][]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">Foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 556, line 8350: '[foo] \\n[]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[foo] 
[]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a>
[]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 557, line 8370: '[foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[foo]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 558, line 8379: '[*foo* bar]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
[*foo* bar]

[*foo* bar]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title"><em>foo</em> bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 559, line 8388: '[[*foo* bar]]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
[[*foo* bar]]

[*foo* bar]: /url "title"
`;
		const expected = `
<p>[<a href="/url" title="title"><em>foo</em> bar</a>]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 560, line 8397: '[[bar [foo]\\n\\n[foo]: /url'", () => {
		const input = `
[[bar [foo]

[foo]: /url
`;
		const expected = `
<p>[[bar <a href="/url">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 561, line 8408: '[Foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[Foo]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">Foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 562, line 8419: '[foo] bar\\n\\n[foo]: /url'", () => {
		const input = `
[foo] bar

[foo]: /url
`;
		const expected = `
<p><a href="/url">foo</a> bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 563, line 8431: '\\[foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
\\[foo]

[foo]: /url "title"
`;
		const expected = `
<p>[foo]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 564, line 8443: '[foo*]: /url\\n\\n*[foo*]'", () => {
		const input = `
[foo*]: /url

*[foo*]
`;
		const expected = `
<p>*<a href="/url">foo*</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 565, line 8455: '[foo][bar]\\n\\n[foo]: /url1\\n[bar]: /url2'", () => {
		const input = `
[foo][bar]

[foo]: /url1
[bar]: /url2
`;
		const expected = `
<p><a href="/url2">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 566, line 8464: '[foo][]\\n\\n[foo]: /url1'", () => {
		const input = `
[foo][]

[foo]: /url1
`;
		const expected = `
<p><a href="/url1">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 567, line 8474: '[foo]()\\n\\n[foo]: /url1'", () => {
		const input = `
[foo]()

[foo]: /url1
`;
		const expected = `
<p><a href="">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 568, line 8482: '[foo](not a link)\\n\\n[foo]: /url1'", () => {
		const input = `
[foo](not a link)

[foo]: /url1
`;
		const expected = `
<p><a href="/url1">foo</a>(not a link)</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 569, line 8493: '[foo][bar][baz]\\n\\n[baz]: /url'", () => {
		const input = `
[foo][bar][baz]

[baz]: /url
`;
		const expected = `
<p>[foo]<a href="/url">bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 570, line 8505: '[foo][bar][baz]\\n\\n[baz]: /url1\\n[bar]: /url2'", () => {
		const input = `
[foo][bar][baz]

[baz]: /url1
[bar]: /url2
`;
		const expected = `
<p><a href="/url2">foo</a><a href="/url1">baz</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 571, line 8518: '[foo][bar][baz]\\n\\n[baz]: /url1\\n[foo]: /url2'", () => {
		const input = `
[foo][bar][baz]

[baz]: /url1
[foo]: /url2
`;
		const expected = `
<p>[foo]<a href="/url1">bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 572, line 8541: '![foo](/url \"title\")'", () => {
		const input = `
![foo](/url "title")
`;
		const expected = `
<p><img src="/url" alt="foo" title="title" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 573, line 8548: '![foo *bar*]\\n\\n[foo *bar*]: train.jpg \"train & tracks\"'", () => {
		const input = `
![foo *bar*]

[foo *bar*]: train.jpg "train & tracks"
`;
		const expected = `
<p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 574, line 8557: '![foo ![bar](/url)](/url2)'", () => {
		const input = `
![foo ![bar](/url)](/url2)
`;
		const expected = `
<p><img src="/url2" alt="foo bar" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 575, line 8564: '![foo [bar](/url)](/url2)'", () => {
		const input = `
![foo [bar](/url)](/url2)
`;
		const expected = `
<p><img src="/url2" alt="foo bar" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 576, line 8578: '![foo *bar*][]\\n\\n[foo *bar*]: train.jpg \"train & tracks\"'", () => {
		const input = `
![foo *bar*][]

[foo *bar*]: train.jpg "train & tracks"
`;
		const expected = `
<p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 577, line 8587: '![foo *bar*][foobar]\\n\\n[FOOBAR]: train.jpg \"train & tracks\"'", () => {
		const input = `
![foo *bar*][foobar]

[FOOBAR]: train.jpg "train & tracks"
`;
		const expected = `
<p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 578, line 8596: '![foo](train.jpg)'", () => {
		const input = `
![foo](train.jpg)
`;
		const expected = `
<p><img src="train.jpg" alt="foo" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 579, line 8603: 'My ![foo bar](/path/to/train.jpg  \"title\"   )'", () => {
		const input = `
My ![foo bar](/path/to/train.jpg  "title"   )
`;
		const expected = `
<p>My <img src="/path/to/train.jpg" alt="foo bar" title="title" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 580, line 8610: '![foo](<url>)'", () => {
		const input = `
![foo](<url>)
`;
		const expected = `
<p><img src="url" alt="foo" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 581, line 8617: '![](/url)'", () => {
		const input = `
![](/url)
`;
		const expected = `
<p><img src="/url" alt="" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 582, line 8626: '![foo][bar]\\n\\n[bar]: /url'", () => {
		const input = `
![foo][bar]

[bar]: /url
`;
		const expected = `
<p><img src="/url" alt="foo" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 583, line 8635: '![foo][bar]\\n\\n[BAR]: /url'", () => {
		const input = `
![foo][bar]

[BAR]: /url
`;
		const expected = `
<p><img src="/url" alt="foo" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 584, line 8646: '![foo][]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![foo][]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo" title="title" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 585, line 8655: '![*foo* bar][]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
![*foo* bar][]

[*foo* bar]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo bar" title="title" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 586, line 8666: '![Foo][]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![Foo][]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="Foo" title="title" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 587, line 8678: '![foo] \\n[]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![foo] 
[]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo" title="title" />
[]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 588, line 8691: '![foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![foo]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo" title="title" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 589, line 8700: '![*foo* bar]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
![*foo* bar]

[*foo* bar]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo bar" title="title" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 590, line 8711: '![[foo]]\\n\\n[[foo]]: /url \"title\"'", () => {
		const input = `
![[foo]]

[[foo]]: /url "title"
`;
		const expected = `
<p>![[foo]]</p>
<p>[[foo]]: /url &quot;title&quot;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 591, line 8723: '![Foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![Foo]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="Foo" title="title" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 592, line 8735: '!\\[foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
!\\[foo]

[foo]: /url "title"
`;
		const expected = `
<p>![foo]</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 593, line 8747: '\\![foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
\\![foo]

[foo]: /url "title"
`;
		const expected = `
<p>!<a href="/url" title="title">foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 594, line 8780: '<http://foo.bar.baz>'", () => {
		const input = `
<http://foo.bar.baz>
`;
		const expected = `
<p><a href="http://foo.bar.baz">http://foo.bar.baz</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 595, line 8787: '<https://foo.bar.baz/test?q=hello&id=22&boolean>'", () => {
		const input = `
<https://foo.bar.baz/test?q=hello&id=22&boolean>
`;
		const expected = `
<p><a href="https://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean">https://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 596, line 8794: '<irc://foo.bar:2233/baz>'", () => {
		const input = `
<irc://foo.bar:2233/baz>
`;
		const expected = `
<p><a href="irc://foo.bar:2233/baz">irc://foo.bar:2233/baz</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 597, line 8803: '<MAILTO:FOO@BAR.BAZ>'", () => {
		const input = `
<MAILTO:FOO@BAR.BAZ>
`;
		const expected = `
<p><a href="MAILTO:FOO@BAR.BAZ">MAILTO:FOO@BAR.BAZ</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 598, line 8815: '<a+b+c:d>'", () => {
		const input = `
<a+b+c:d>
`;
		const expected = `
<p><a href="a+b+c:d">a+b+c:d</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 599, line 8822: '<made-up-scheme://foo,bar>'", () => {
		const input = `
<made-up-scheme://foo,bar>
`;
		const expected = `
<p><a href="made-up-scheme://foo,bar">made-up-scheme://foo,bar</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 600, line 8829: '<https://../>'", () => {
		const input = `
<https://../>
`;
		const expected = `
<p><a href="https://../">https://../</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 601, line 8836: '<localhost:5001/foo>'", () => {
		const input = `
<localhost:5001/foo>
`;
		const expected = `
<p><a href="localhost:5001/foo">localhost:5001/foo</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 602, line 8845: '<https://foo.bar/baz bim>'", () => {
		const input = `
<https://foo.bar/baz bim>
`;
		const expected = `
<p>&lt;https://foo.bar/baz bim&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 603, line 8854: '<https://example.com/\\[\\>'", () => {
		const input = `
<https://example.com/\\[\\>
`;
		const expected = `
<p><a href="https://example.com/%5C%5B%5C">https://example.com/\\[\\</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 604, line 8876: '<foo@bar.example.com>'", () => {
		const input = `
<foo@bar.example.com>
`;
		const expected = `
<p><a href="mailto:foo@bar.example.com">foo@bar.example.com</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 605, line 8883: '<foo+special@Bar.baz-bar0.com>'", () => {
		const input = `
<foo+special@Bar.baz-bar0.com>
`;
		const expected = `
<p><a href="mailto:foo+special@Bar.baz-bar0.com">foo+special@Bar.baz-bar0.com</a></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 606, line 8892: '<foo\\+@bar.example.com>'", () => {
		const input = `
<foo\\+@bar.example.com>
`;
		const expected = `
<p>&lt;foo+@bar.example.com&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 607, line 8901: '<>'", () => {
		const input = `
<>
`;
		const expected = `
<p>&lt;&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 608, line 8908: '< https://foo.bar >'", () => {
		const input = `
< https://foo.bar >
`;
		const expected = `
<p>&lt; https://foo.bar &gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 609, line 8915: '<m:abc>'", () => {
		const input = `
<m:abc>
`;
		const expected = `
<p>&lt;m:abc&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 610, line 8922: '<foo.bar.baz>'", () => {
		const input = `
<foo.bar.baz>
`;
		const expected = `
<p>&lt;foo.bar.baz&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 611, line 8929: 'https://example.com'", () => {
		const input = `
https://example.com
`;
		const expected = `
<p>https://example.com</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 612, line 8936: 'foo@bar.example.com'", () => {
		const input = `
foo@bar.example.com
`;
		const expected = `
<p>foo@bar.example.com</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 613, line 9016: '<a><bab><c2c>'", () => {
		const input = `
<a><bab><c2c>
`;
		const expected = `
<p><a><bab><c2c></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 614, line 9025: '<a/><b2/>'", () => {
		const input = `
<a/><b2/>
`;
		const expected = `
<p><a/><b2/></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 615, line 9034: '<a  /><b2\\ndata=\"foo\" >'", () => {
		const input = `
<a  /><b2
data="foo" >
`;
		const expected = `
<p><a  /><b2
data="foo" ></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 616, line 9045: '<a foo=\"bar\" bam = 'baz <em>\"</em>'\\n_boolean zoop:33=zoop:33 />'", () => {
		const input = `
<a foo="bar" bam = 'baz <em>"</em>'
_boolean zoop:33=zoop:33 />
`;
		const expected = `
<p><a foo="bar" bam = 'baz <em>"</em>'
_boolean zoop:33=zoop:33 /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 617, line 9056: 'Foo <responsive-image src=\"foo.jpg\" />'", () => {
		const input = `
Foo <responsive-image src="foo.jpg" />
`;
		const expected = `
<p>Foo <responsive-image src="foo.jpg" /></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 618, line 9065: '<33> <__>'", () => {
		const input = `
<33> <__>
`;
		const expected = `
<p>&lt;33&gt; &lt;__&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 619, line 9074: '<a h*#ref=\"hi\">'", () => {
		const input = `
<a h*#ref="hi">
`;
		const expected = `
<p>&lt;a h*#ref=&quot;hi&quot;&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 620, line 9083: '<a href=\"hi'> <a href=hi'>'", () => {
		const input = `
<a href="hi'> <a href=hi'>
`;
		const expected = `
<p>&lt;a href=&quot;hi'&gt; &lt;a href=hi'&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 621, line 9092: '< a><\\nfoo><bar/ >\\n<foo bar=baz\\nbim!bop />'", () => {
		const input = `
< a><
foo><bar/ >
<foo bar=baz
bim!bop />
`;
		const expected = `
<p>&lt; a&gt;&lt;
foo&gt;&lt;bar/ &gt;
&lt;foo bar=baz
bim!bop /&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 622, line 9107: '<a href='bar'title=title>'", () => {
		const input = `
<a href='bar'title=title>
`;
		const expected = `
<p>&lt;a href='bar'title=title&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 623, line 9116: '</a></foo >'", () => {
		const input = `
</a></foo >
`;
		const expected = `
<p></a></foo ></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 624, line 9125: '</a href=\"foo\">'", () => {
		const input = `
</a href="foo">
`;
		const expected = `
<p>&lt;/a href=&quot;foo&quot;&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 625, line 9134: 'foo <!-- this is a --\\ncomment - with hyphens -->'", () => {
		const input = `
foo <!-- this is a --
comment - with hyphens -->
`;
		const expected = `
<p>foo <!-- this is a --
comment - with hyphens --></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 626, line 9142: 'foo <!--> foo -->\\n\\nfoo <!---> foo -->'", () => {
		const input = `
foo <!--> foo -->

foo <!---> foo -->
`;
		const expected = `
<p>foo <!--> foo --&gt;</p>
<p>foo <!---> foo --&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 627, line 9154: 'foo <?php echo $a; ?>'", () => {
		const input = `
foo <?php echo $a; ?>
`;
		const expected = `
<p>foo <?php echo $a; ?></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 628, line 9163: 'foo <!ELEMENT br EMPTY>'", () => {
		const input = `
foo <!ELEMENT br EMPTY>
`;
		const expected = `
<p>foo <!ELEMENT br EMPTY></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 629, line 9172: 'foo <![CDATA[>&<]]>'", () => {
		const input = `
foo <![CDATA[>&<]]>
`;
		const expected = `
<p>foo <![CDATA[>&<]]></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 630, line 9182: 'foo <a href=\"&ouml;\">'", () => {
		const input = `
foo <a href="&ouml;">
`;
		const expected = `
<p>foo <a href="&ouml;"></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 631, line 9191: 'foo <a href=\"\\*\">'", () => {
		const input = `
foo <a href="\\*">
`;
		const expected = `
<p>foo <a href="\\*"></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test('Example 632, line 9198: \'<a href="\\"">\'', () => {
		const input = `
<a href="\\"">
`;
		const expected = `
<p>&lt;a href=&quot;&quot;&quot;&gt;</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 633, line 9212: 'foo  \\nbaz'", () => {
		const input = `
foo  
baz
`;
		const expected = `
<p>foo<br />
baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 634, line 9224: 'foo\\\\nbaz'", () => {
		const input = `
foo\\
baz
`;
		const expected = `
<p>foo<br />
baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 635, line 9235: 'foo       \\nbaz'", () => {
		const input = `
foo       
baz
`;
		const expected = `
<p>foo<br />
baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 636, line 9246: 'foo  \\n     bar'", () => {
		const input = `
foo  
     bar
`;
		const expected = `
<p>foo<br />
bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 637, line 9255: 'foo\\\\n     bar'", () => {
		const input = `
foo\\
     bar
`;
		const expected = `
<p>foo<br />
bar</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 638, line 9267: '*foo  \\nbar*'", () => {
		const input = `
*foo  
bar*
`;
		const expected = `
<p><em>foo<br />
bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 639, line 9276: '*foo\\\\nbar*'", () => {
		const input = `
*foo\\
bar*
`;
		const expected = `
<p><em>foo<br />
bar</em></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 640, line 9287: '`code  \\nspan`'", () => {
		const input = `
\`code  
span\`
`;
		const expected = `
<p><code>code   span</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 641, line 9295: '`code\\\\nspan`'", () => {
		const input = `
\`code\\
span\`
`;
		const expected = `
<p><code>code\\ span</code></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 642, line 9305: '<a href=\"foo  \\nbar\">'", () => {
		const input = `
<a href="foo  
bar">
`;
		const expected = `
<p><a href="foo  
bar"></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 643, line 9314: '<a href=\"foo\\\\nbar\">'", () => {
		const input = `
<a href="foo\\
bar">
`;
		const expected = `
<p><a href="foo\\
bar"></p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 644, line 9327: 'foo\\'", () => {
		const input = `
foo\\
`;
		const expected = `
<p>foo\\</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 645, line 9334: 'foo  '", () => {
		const input = `
foo  
`;
		const expected = `
<p>foo</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 646, line 9341: '### foo\\'", () => {
		const input = `
### foo\\
`;
		const expected = `
<h3>foo\\</h3>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 647, line 9348: '### foo  '", () => {
		const input = `
### foo  
`;
		const expected = `
<h3>foo</h3>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 648, line 9363: 'foo\\nbaz'", () => {
		const input = `
foo
baz
`;
		const expected = `
<p>foo
baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 649, line 9375: 'foo \\n baz'", () => {
		const input = `
foo 
 baz
`;
		const expected = `
<p>foo
baz</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 650, line 9395: 'hello $.;'there'", () => {
		const input = `
hello $.;'there
`;
		const expected = `
<p>hello $.;'there</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 651, line 9402: 'Foo χρῆν'", () => {
		const input = `
Foo χρῆν
`;
		const expected = `
<p>Foo χρῆν</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});

	test("Example 652, line 9411: 'Multiple     spaces'", () => {
		const input = `
Multiple     spaces
`;
		const expected = `
<p>Multiple     spaces</p>
`;
		const doc = parse(input.substring(1, input.length - 1), core);
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});
});
