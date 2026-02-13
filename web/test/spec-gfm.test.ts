import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import gfm from "../src/rulesets/gfm";

describe("spec-gfm", () => {
	test("Example 1, line 368: '→foo→baz→→bim'", () => {
		const input = `
	foo	baz		bim
`;
		const expected = `
<pre><code>foo	baz		bim
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 2, line 375: '  →foo→baz→→bim'", () => {
		const input = `
  	foo	baz		bim
`;
		const expected = `
<pre><code>foo	baz		bim
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 3, line 382: '    a→a\\n    ὐ→a'", () => {
		const input = `
    a	a
    ὐ	a
`;
		const expected = `
<pre><code>a	a
ὐ	a
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 4, line 395: '  - foo\\n\\n→bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 5, line 408: '- foo\\n\\n→→bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 6, line 431: '>→→foo'", () => {
		const input = `
>		foo
`;
		const expected = `
<blockquote>
<pre><code>  foo
</code></pre>
</blockquote>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 7, line 440: '-→→foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 8, line 452: '    foo\\n→bar'", () => {
		const input = `
    foo
	bar
`;
		const expected = `
<pre><code>foo
bar
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 9, line 461: ' - foo\\n   - bar\\n→ - baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 10, line 479: '#→Foo'", () => {
		const input = `
#	Foo
`;
		const expected = `
<h1>Foo</h1>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 11, line 485: '*→*→*→'", () => {
		const input = `
*	*	*	
`;
		const expected = `
<hr />
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 12, line 512: '- `one\\n- two`'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 13, line 551: '***\\n---\\n___'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 14, line 564: '+++'", () => {
		const input = `
+++
`;
		const expected = `
<p>+++</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 15, line 571: '==='", () => {
		const input = `
===
`;
		const expected = `
<p>===</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 16, line 580: '--\\n**\\n__'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 17, line 593: ' ***\\n  ***\\n   ***'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 18, line 606: '    ***'", () => {
		const input = `
    ***
`;
		const expected = `
<pre><code>***
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 19, line 614: 'Foo\\n    ***'", () => {
		const input = `
Foo
    ***
`;
		const expected = `
<p>Foo
***</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 20, line 625: '_____________________________________'", () => {
		const input = `
_____________________________________
`;
		const expected = `
<hr />
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 21, line 634: ' - - -'", () => {
		const input = `
 - - -
`;
		const expected = `
<hr />
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 22, line 641: ' **  * ** * ** * **'", () => {
		const input = `
 **  * ** * ** * **
`;
		const expected = `
<hr />
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 23, line 648: '-     -      -      -'", () => {
		const input = `
-     -      -      -
`;
		const expected = `
<hr />
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 24, line 657: '- - - -    '", () => {
		const input = `
- - - -    
`;
		const expected = `
<hr />
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 25, line 666: '_ _ _ _ a\\n\\na------\\n\\n---a---'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 26, line 682: ' *-*'", () => {
		const input = `
 *-*
`;
		const expected = `
<p><em>-</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 27, line 691: '- foo\\n***\\n- bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 28, line 708: 'Foo\\n***\\nbar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 29, line 725: 'Foo\\n---\\nbar'", () => {
		const input = `
Foo
---
bar
`;
		const expected = `
<h2>Foo</h2>
<p>bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 30, line 738: '* Foo\\n* * *\\n* Bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 31, line 755: '- Foo\\n- * * *'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 32, line 784: '# foo\\n## foo\\n### foo\\n#### foo\\n##### foo\\n###### foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 33, line 803: '####### foo'", () => {
		const input = `
####### foo
`;
		const expected = `
<p>####### foo</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 34, line 818: '#5 bolt\\n\\n#hashtag'", () => {
		const input = `
#5 bolt

#hashtag
`;
		const expected = `
<p>#5 bolt</p>
<p>#hashtag</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 35, line 830: '\\## foo'", () => {
		const input = `
\\## foo
`;
		const expected = `
<p>## foo</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 36, line 839: '# foo *bar* \\*baz\\*'", () => {
		const input = `
# foo *bar* \\*baz\\*
`;
		const expected = `
<h1>foo <em>bar</em> *baz*</h1>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 37, line 848: '#                  foo                     '", () => {
		const input = `
#                  foo                     
`;
		const expected = `
<h1>foo</h1>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 38, line 857: ' ### foo\\n  ## foo\\n   # foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 39, line 870: '    # foo'", () => {
		const input = `
    # foo
`;
		const expected = `
<pre><code># foo
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 40, line 878: 'foo\\n    # bar'", () => {
		const input = `
foo
    # bar
`;
		const expected = `
<p>foo
# bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 41, line 889: '## foo ##\\n  ###   bar    ###'", () => {
		const input = `
## foo ##
  ###   bar    ###
`;
		const expected = `
<h2>foo</h2>
<h3>bar</h3>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 42, line 900: '# foo ##################################\\n##### foo ##'", () => {
		const input = `
# foo ##################################
##### foo ##
`;
		const expected = `
<h1>foo</h1>
<h5>foo</h5>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 43, line 911: '### foo ###     '", () => {
		const input = `
### foo ###     
`;
		const expected = `
<h3>foo</h3>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 44, line 922: '### foo ### b'", () => {
		const input = `
### foo ### b
`;
		const expected = `
<h3>foo ### b</h3>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 45, line 931: '# foo#'", () => {
		const input = `
# foo#
`;
		const expected = `
<h1>foo#</h1>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 46, line 941: '### foo \\###\\n## foo #\\##\\n# foo \\#'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 47, line 955: '****\\n## foo\\n****'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 48, line 966: 'Foo bar\\n# baz\\nBar foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 49, line 979: '## \\n#\\n### ###'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 50, line 1019: 'Foo *bar*\\n=========\\n\\nFoo *bar*\\n---------'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 51, line 1033: 'Foo *bar\\nbaz*\\n===='", () => {
		const input = `
Foo *bar
baz*
====
`;
		const expected = `
<h1>Foo <em>bar
baz</em></h1>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 52, line 1047: '  Foo *bar\\nbaz*→\\n===='", () => {
		const input = `
  Foo *bar
baz*	
====
`;
		const expected = `
<h1>Foo <em>bar
baz</em></h1>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 53, line 1059: 'Foo\\n-------------------------\\n\\nFoo\\n='", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 54, line 1074: '   Foo\\n---\\n\\n  Foo\\n-----\\n\\n  Foo\\n  ==='", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 55, line 1092: '    Foo\\n    ---\\n\\n    Foo\\n---'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 56, line 1111: 'Foo\\n   ----      '", () => {
		const input = `
Foo
   ----      
`;
		const expected = `
<h2>Foo</h2>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 57, line 1121: 'Foo\\n    ---'", () => {
		const input = `
Foo
    ---
`;
		const expected = `
<p>Foo
---</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 58, line 1132: 'Foo\\n= =\\n\\nFoo\\n--- -'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 59, line 1148: 'Foo  \\n-----'", () => {
		const input = `
Foo  
-----
`;
		const expected = `
<h2>Foo</h2>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 60, line 1158: 'Foo\\\\n----'", () => {
		const input = `
Foo\\
----
`;
		const expected = `
<h2>Foo\\</h2>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 61, line 1169: '`Foo\\n----\\n`\\n\\n<a title=\"a lot\\n---\\nof dashes\"/>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 62, line 1188: '> Foo\\n---'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 63, line 1199: '> foo\\nbar\\n==='", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 64, line 1212: '- Foo\\n---'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 65, line 1227: 'Foo\\nBar\\n---'", () => {
		const input = `
Foo
Bar
---
`;
		const expected = `
<h2>Foo
Bar</h2>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 66, line 1240: '---\\nFoo\\n---\\nBar\\n---\\nBaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 67, line 1257: '\\n===='", () => {
		const input = `

====
`;
		const expected = `
<p>====</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 68, line 1269: '---\\n---'", () => {
		const input = `
---
---
`;
		const expected = `
<hr />
<hr />
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 69, line 1278: '- foo\\n-----'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 70, line 1289: '    foo\\n---'", () => {
		const input = `
    foo
---
`;
		const expected = `
<pre><code>foo
</code></pre>
<hr />
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 71, line 1299: '> foo\\n-----'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 72, line 1313: '\\> foo\\n------'", () => {
		const input = `
\\> foo
------
`;
		const expected = `
<h2>&gt; foo</h2>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 73, line 1344: 'Foo\\n\\nbar\\n---\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 74, line 1360: 'Foo\\nbar\\n\\n---\\n\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 75, line 1378: 'Foo\\nbar\\n* * *\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 76, line 1393: 'Foo\\nbar\\n\\---\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 77, line 1421: '    a simple\\n      indented code block'", () => {
		const input = `
    a simple
      indented code block
`;
		const expected = `
<pre><code>a simple
  indented code block
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 78, line 1435: '  - foo\\n\\n    bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 79, line 1449: '1.  foo\\n\\n    - bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 80, line 1469: '    <a/>\\n    *hi*\\n\\n    - one'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 81, line 1485: '    chunk1\\n\\n    chunk2\\n  \\n \\n \\n    chunk3'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 82, line 1508: '    chunk1\\n      \\n      chunk2'", () => {
		const input = `
    chunk1
      
      chunk2
`;
		const expected = `
<pre><code>chunk1
  
  chunk2
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 83, line 1523: 'Foo\\n    bar\\n'", () => {
		const input = `
Foo
    bar

`;
		const expected = `
<p>Foo
bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 84, line 1537: '    foo\\nbar'", () => {
		const input = `
    foo
bar
`;
		const expected = `
<pre><code>foo
</code></pre>
<p>bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 85, line 1550: '# Heading\\n    foo\\nHeading\\n------\\n    foo\\n----'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 86, line 1570: '        foo\\n    bar'", () => {
		const input = `
        foo
    bar
`;
		const expected = `
<pre><code>    foo
bar
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 87, line 1583: '\\n    \\n    foo\\n    \\n'", () => {
		const input = `

    
    foo
    

`;
		const expected = `
<pre><code>foo
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 88, line 1597: '    foo  '", () => {
		const input = `
    foo  
`;
		const expected = `
<pre><code>foo  
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 89, line 1652: '```\\n<\\n >\\n```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 90, line 1666: '~~~\\n<\\n >\\n~~~'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 91, line 1679: '``\\nfoo\\n``'", () => {
		const input = `
\`\`
foo
\`\`
`;
		const expected = `
<p><code>foo</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 92, line 1690: '```\\naaa\\n~~~\\n```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 93, line 1702: '~~~\\naaa\\n```\\n~~~'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 94, line 1716: '````\\naaa\\n```\\n``````'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 95, line 1728: '~~~~\\naaa\\n~~~\\n~~~~'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 96, line 1743: '```'", () => {
		const input = `
\`\`\`
`;
		const expected = `
<pre><code></code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 97, line 1750: '`````\\n\\n```\\naaa'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 98, line 1763: '> ```\\n> aaa\\n\\nbbb'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 99, line 1779: '```\\n\\n  \\n```'", () => {
		const input = `
\`\`\`

  
\`\`\`
`;
		const expected = `
<pre><code>
  
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 100, line 1793: '```\\n```'", () => {
		const input = `
\`\`\`
\`\`\`
`;
		const expected = `
<pre><code></code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 101, line 1805: ' ```\\n aaa\\naaa\\n```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 102, line 1817: '  ```\\naaa\\n  aaa\\naaa\\n  ```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 103, line 1831: '   ```\\n   aaa\\n    aaa\\n  aaa\\n   ```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 104, line 1847: '    ```\\n    aaa\\n    ```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 105, line 1862: '```\\naaa\\n  ```'", () => {
		const input = `
\`\`\`
aaa
  \`\`\`
`;
		const expected = `
<pre><code>aaa
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 106, line 1872: '   ```\\naaa\\n  ```'", () => {
		const input = `
   \`\`\`
aaa
  \`\`\`
`;
		const expected = `
<pre><code>aaa
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 107, line 1884: '```\\naaa\\n    ```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 108, line 1898: '``` ```\\naaa'", () => {
		const input = `
\`\`\` \`\`\`
aaa
`;
		const expected = `
<p><code> </code>
aaa</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 109, line 1907: '~~~~~~\\naaa\\n~~~ ~~'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 110, line 1921: 'foo\\n```\\nbar\\n```\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 111, line 1938: 'foo\\n---\\n~~~\\nbar\\n~~~\\n# baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 112, line 1960: '```ruby\\ndef foo(x)\\n  return 3\\nend\\n```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 113, line 1974: '~~~~    ruby startline=3 $%@#$\\ndef foo(x)\\n  return 3\\nend\\n~~~~~~~'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 114, line 1988: '````;\\n````'", () => {
		const input = `
\`\`\`\`;
\`\`\`\`
`;
		const expected = `
<pre><code class="language-;"></code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 115, line 1998: '``` aa ```\\nfoo'", () => {
		const input = `
\`\`\` aa \`\`\`
foo
`;
		const expected = `
<p><code>aa</code>
foo</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 116, line 2009: '~~~ aa ``` ~~~\\nfoo\\n~~~'", () => {
		const input = `
~~~ aa \`\`\` ~~~
foo
~~~
`;
		const expected = `
<pre><code class="language-aa">foo
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 117, line 2021: '```\\n``` aaa\\n```'", () => {
		const input = `
\`\`\`
\`\`\` aaa
\`\`\`
`;
		const expected = `
<pre><code>\`\`\` aaa
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 118, line 2100: '<table><tr><td>\\n<pre>\\n**Hello**,\\n\\n_world_.\\n</pre>\\n</td></tr></table>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 119, line 2129: '<table>\\n  <tr>\\n    <td>\\n           hi\\n    </td>\\n  </tr>\\n</table>\\n\\nokay.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 120, line 2151: ' <div>\\n  *hello*\\n         <foo><a>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 121, line 2164: '</div>\\n*foo*'", () => {
		const input = `
</div>
*foo*
`;
		const expected = `
</div>
*foo*
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 122, line 2175: '<DIV CLASS=\"foo\">\\n\\n*Markdown*\\n\\n</DIV>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test('Example 123, line 2191: \'<div id="foo"\\n  class="bar">\\n</div>\'', () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test('Example 124, line 2202: \'<div id="foo" class="bar\\n  baz">\\n</div>\'', () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 125, line 2214: '<div>\\n*foo*\\n\\n*bar*'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 126, line 2230: '<div id=\"foo\"\\n*hi*'", () => {
		const input = `
<div id="foo"
*hi*
`;
		const expected = `
<div id="foo"
*hi*
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 127, line 2239: '<div class\\nfoo'", () => {
		const input = `
<div class
foo
`;
		const expected = `
<div class
foo
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 128, line 2251: '<div *???-&&&-<---\\n*foo*'", () => {
		const input = `
<div *???-&&&-<---
*foo*
`;
		const expected = `
<div *???-&&&-<---
*foo*
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 129, line 2263: '<div><a href=\"bar\">*foo*</a></div>'", () => {
		const input = `
<div><a href="bar">*foo*</a></div>
`;
		const expected = `
<div><a href="bar">*foo*</a></div>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 130, line 2270: '<table><tr><td>\\nfoo\\n</td></tr></table>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 131, line 2287: '<div></div>\\n``` c\\nint x = 33;\\n```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 132, line 2304: '<a href=\"foo\">\\n*bar*\\n</a>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 133, line 2317: '<Warning>\\n*bar*\\n</Warning>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 134, line 2328: '<i class=\"foo\">\\n*bar*\\n</i>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 135, line 2339: '</ins>\\n*bar*'", () => {
		const input = `
</ins>
*bar*
`;
		const expected = `
</ins>
*bar*
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 136, line 2354: '<del>\\n*foo*\\n</del>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 137, line 2369: '<del>\\n\\n*foo*\\n\\n</del>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 138, line 2387: '<del>*foo*</del>'", () => {
		const input = `
<del>*foo*</del>
`;
		const expected = `
<p><del><em>foo</em></del></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 139, line 2403: '<pre language=\"haskell\"><code>\\nimport Text.HTML.TagSoup\\n\\nmain :: IO ()\\nmain = print $ parseTags tags\\n</code></pre>\\nokay'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test('Example 140, line 2424: \'<script type="text/javascript">\\n// JavaScript example\\n\\ndocument.getElementById("demo").innerHTML = "Hello JavaScript!";\\n</script>\\nokay\'', () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 141, line 2443: '<style\\n  type=\"text/css\">\\nh1 {color:red;}\\n\\np {color:blue;}\\n</style>\\nokay'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 142, line 2466: '<style\\n  type=\"text/css\">\\n\\nfoo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 143, line 2479: '> <div>\\n> foo\\n\\nbar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 144, line 2493: '- <div>\\n- foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 145, line 2508: '<style>p{color:red;}</style>\\n*foo*'", () => {
		const input = `
<style>p{color:red;}</style>
*foo*
`;
		const expected = `
<style>p{color:red;}</style>
<p><em>foo</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 146, line 2517: '<!-- foo -->*bar*\\n*baz*'", () => {
		const input = `
<!-- foo -->*bar*
*baz*
`;
		const expected = `
<!-- foo -->*bar*
<p><em>baz</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 147, line 2529: '<script>\\nfoo\\n</script>1. *bar*'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 148, line 2542: '<!-- Foo\\n\\nbar\\n   baz -->\\nokay'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 149, line 2560: '<?php\\n\\n  echo '>';\\n\\n?>\\nokay'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 150, line 2579: '<!DOCTYPE html>'", () => {
		const input = `
<!DOCTYPE html>
`;
		const expected = `
<!DOCTYPE html>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 151, line 2588: '<![CDATA[\\nfunction matchwo(a,b)\\n{\\n  if (a < b && a < 0) then {\\n    return 1;\\n\\n  } else {\\n\\n    return 0;\\n  }\\n}\\n]]>\\nokay'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 152, line 2621: '  <!-- foo -->\\n\\n    <!-- foo -->'", () => {
		const input = `
  <!-- foo -->

    <!-- foo -->
`;
		const expected = `
  <!-- foo -->
<pre><code>&lt;!-- foo --&gt;
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 153, line 2632: '  <div>\\n\\n    <div>'", () => {
		const input = `
  <div>

    <div>
`;
		const expected = `
  <div>
<pre><code>&lt;div&gt;
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 154, line 2646: 'Foo\\n<div>\\nbar\\n</div>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 155, line 2663: '<div>\\nbar\\n</div>\\n*foo*'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 156, line 2678: 'Foo\\n<a href=\"bar\">\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 157, line 2719: '<div>\\n\\n*Emphasized* text.\\n\\n</div>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 158, line 2732: '<div>\\n*Emphasized* text.\\n</div>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 159, line 2754: '<table>\\n\\n<tr>\\n\\n<td>\\nHi\\n</td>\\n\\n</tr>\\n\\n</table>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 160, line 2781: '<table>\\n\\n  <tr>\\n\\n    <td>\\n      Hi\\n    </td>\\n\\n  </tr>\\n\\n</table>'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 161, line 2829: '[foo]: /url \"title\"\\n\\n[foo]'", () => {
		const input = `
[foo]: /url "title"

[foo]
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 162, line 2838: '   [foo]: \\n      /url  \\n           'the title'  \\n\\n[foo]'", () => {
		const input = `
   [foo]: 
      /url  
           'the title'  

[foo]
`;
		const expected = `
<p><a href="/url" title="the title">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 163, line 2849: '[Foo*bar\\]]:my_(url) 'title (with parens)'\\n\\n[Foo*bar\\]]'", () => {
		const input = `
[Foo*bar\\]]:my_(url) 'title (with parens)'

[Foo*bar\\]]
`;
		const expected = `
<p><a href="my_(url)" title="title (with parens)">Foo*bar]</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 164, line 2858: '[Foo bar]:\\n<my url>\\n'title'\\n\\n[Foo bar]'", () => {
		const input = `
[Foo bar]:
<my url>
'title'

[Foo bar]
`;
		const expected = `
<p><a href="my%20url" title="title">Foo bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 165, line 2871: '[foo]: /url '\\ntitle\\nline1\\nline2\\n'\\n\\n[foo]'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 166, line 2890: '[foo]: /url 'title\\n\\nwith blank line'\\n\\n[foo]'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 167, line 2905: '[foo]:\\n/url\\n\\n[foo]'", () => {
		const input = `
[foo]:
/url

[foo]
`;
		const expected = `
<p><a href="/url">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 168, line 2917: '[foo]:\\n\\n[foo]'", () => {
		const input = `
[foo]:

[foo]
`;
		const expected = `
<p>[foo]:</p>
<p>[foo]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 169, line 2929: '[foo]: <>\\n\\n[foo]'", () => {
		const input = `
[foo]: <>

[foo]
`;
		const expected = `
<p><a href="">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 170, line 2940: '[foo]: <bar>(baz)\\n\\n[foo]'", () => {
		const input = `
[foo]: <bar>(baz)

[foo]
`;
		const expected = `
<p>[foo]: <bar>(baz)</p>
<p>[foo]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test('Example 171, line 2953: \'[foo]: /url\\bar\\*baz "foo\\"bar\\baz"\\n\\n[foo]\'', () => {
		const input = `
[foo]: /url\\bar\\*baz "foo\\"bar\\baz"

[foo]
`;
		const expected = `
<p><a href="/url%5Cbar*baz" title="foo&quot;bar\\baz">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 172, line 2964: '[foo]\\n\\n[foo]: url'", () => {
		const input = `
[foo]

[foo]: url
`;
		const expected = `
<p><a href="url">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 173, line 2976: '[foo]\\n\\n[foo]: first\\n[foo]: second'", () => {
		const input = `
[foo]

[foo]: first
[foo]: second
`;
		const expected = `
<p><a href="first">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 174, line 2989: '[FOO]: /url\\n\\n[Foo]'", () => {
		const input = `
[FOO]: /url

[Foo]
`;
		const expected = `
<p><a href="/url">Foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 175, line 2998: '[ΑΓΩ]: /φου\\n\\n[αγω]'", () => {
		const input = `
[ΑΓΩ]: /φου

[αγω]
`;
		const expected = `
<p><a href="/%CF%86%CE%BF%CF%85">αγω</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 176, line 3010: '[foo]: /url'", () => {
		const input = `
[foo]: /url
`;
		const expected = `

`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 177, line 3018: '[\\nfoo\\n]: /url\\nbar'", () => {
		const input = `
[
foo
]: /url
bar
`;
		const expected = `
<p>bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 178, line 3031: '[foo]: /url \"title\" ok'", () => {
		const input = `
[foo]: /url "title" ok
`;
		const expected = `
<p>[foo]: /url &quot;title&quot; ok</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 179, line 3040: '[foo]: /url\\n\"title\" ok'", () => {
		const input = `
[foo]: /url
"title" ok
`;
		const expected = `
<p>&quot;title&quot; ok</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 180, line 3051: '    [foo]: /url \"title\"\\n\\n[foo]'", () => {
		const input = `
    [foo]: /url "title"

[foo]
`;
		const expected = `
<pre><code>[foo]: /url &quot;title&quot;
</code></pre>
<p>[foo]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 181, line 3065: '```\\n[foo]: /url\\n```\\n\\n[foo]'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 182, line 3080: 'Foo\\n[bar]: /baz\\n\\n[bar]'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 183, line 3095: '# [Foo]\\n[foo]: /url\\n> bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 184, line 3106: '[foo]: /url\\nbar\\n===\\n[foo]'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 185, line 3116: '[foo]: /url\\n===\\n[foo]'", () => {
		const input = `
[foo]: /url
===
[foo]
`;
		const expected = `
<p>===
<a href="/url">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test('Example 186, line 3129: \'[foo]: /foo-url "foo"\\n[bar]: /bar-url\\n  "bar"\\n[baz]: /baz-url\\n\\n[foo],\\n[bar],\\n[baz]\'', () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 187, line 3150: '[foo]\\n\\n> [foo]: /url'", () => {
		const input = `
[foo]

> [foo]: /url
`;
		const expected = `
<p><a href="/url">foo</a></p>
<blockquote>
</blockquote>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 188, line 3167: '[foo]: /url'", () => {
		const input = `
[foo]: /url
`;
		const expected = `

`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 189, line 3184: 'aaa\\n\\nbbb'", () => {
		const input = `
aaa

bbb
`;
		const expected = `
<p>aaa</p>
<p>bbb</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 190, line 3196: 'aaa\\nbbb\\n\\nccc\\nddd'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 191, line 3212: 'aaa\\n\\n\\nbbb'", () => {
		const input = `
aaa


bbb
`;
		const expected = `
<p>aaa</p>
<p>bbb</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 192, line 3225: '  aaa\\n bbb'", () => {
		const input = `
  aaa
 bbb
`;
		const expected = `
<p>aaa
bbb</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 193, line 3237: 'aaa\\n             bbb\\n                                       ccc'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 194, line 3251: '   aaa\\nbbb'", () => {
		const input = `
   aaa
bbb
`;
		const expected = `
<p>aaa
bbb</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 195, line 3260: '    aaa\\nbbb'", () => {
		const input = `
    aaa
bbb
`;
		const expected = `
<pre><code>aaa
</code></pre>
<p>bbb</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 196, line 3274: 'aaa     \\nbbb     '", () => {
		const input = `
aaa     
bbb     
`;
		const expected = `
<p>aaa<br />
bbb</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 197, line 3291: '  \\n\\naaa\\n  \\n\\n# aaa\\n\\n  '", () => {
		const input = `
  

aaa
  

# aaa

  
`;
		const expected = `
<p>aaa</p>
<h1>aaa</h1>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 198, line 3326: '| foo | bar |\\n| --- | --- |\\n| baz | bim |'", () => {
		const input = `
| foo | bar |
| --- | --- |
| baz | bim |
`;
		const expected = `
<table>
<thead>
<tr>
<th>foo</th>
<th>bar</th>
</tr>
</thead>
<tbody>
<tr>
<td>baz</td>
<td>bim</td>
</tr>
</tbody>
</table>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 199, line 3350: '| abc | defghi |\\n:-: | -----------:\\nbar | baz'", () => {
		const input = `
| abc | defghi |
:-: | -----------:
bar | baz
`;
		const expected = `
<table>
<thead>
<tr>
<th align="center">abc</th>
<th align="right">defghi</th>
</tr>
</thead>
<tbody>
<tr>
<td align="center">bar</td>
<td align="right">baz</td>
</tr>
</tbody>
</table>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 200, line 3374: '| f\\|oo  |\\n| ------ |\\n| b `\\|` az |\\n| b **\\|** im |'", () => {
		const input = `
| f\\|oo  |
| ------ |
| b \`\\|\` az |
| b **\\|** im |
`;
		const expected = `
<table>
<thead>
<tr>
<th>f|oo</th>
</tr>
</thead>
<tbody>
<tr>
<td>b <code>|</code> az</td>
</tr>
<tr>
<td>b <strong>|</strong> im</td>
</tr>
</tbody>
</table>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 201, line 3400: '| abc | def |\\n| --- | --- |\\n| bar | baz |\\n> bar'", () => {
		const input = `
| abc | def |
| --- | --- |
| bar | baz |
> bar
`;
		const expected = `
<table>
<thead>
<tr>
<th>abc</th>
<th>def</th>
</tr>
</thead>
<tbody>
<tr>
<td>bar</td>
<td>baz</td>
</tr>
</tbody>
</table>
<blockquote>
<p>bar</p>
</blockquote>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 202, line 3425: '| abc | def |\\n| --- | --- |\\n| bar | baz |\\nbar\\n\\nbar'", () => {
		const input = `
| abc | def |
| --- | --- |
| bar | baz |
bar

bar
`;
		const expected = `
<table>
<thead>
<tr>
<th>abc</th>
<th>def</th>
</tr>
</thead>
<tbody>
<tr>
<td>bar</td>
<td>baz</td>
</tr>
<tr>
<td>bar</td>
<td></td>
</tr>
</tbody>
</table>
<p>bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 203, line 3457: '| abc | def |\\n| --- |\\n| bar |'", () => {
		const input = `
| abc | def |
| --- |
| bar |
`;
		const expected = `
<p>| abc | def |
| --- |
| bar |</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 204, line 3471: '| abc | def |\\n| --- | --- |\\n| bar |\\n| bar | baz | boo |'", () => {
		const input = `
| abc | def |
| --- | --- |
| bar |
| bar | baz | boo |
`;
		const expected = `
<table>
<thead>
<tr>
<th>abc</th>
<th>def</th>
</tr>
</thead>
<tbody>
<tr>
<td>bar</td>
<td></td>
</tr>
<tr>
<td>bar</td>
<td>baz</td>
</tr>
</tbody>
</table>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 205, line 3499: '| abc | def |\\n| --- | --- |'", () => {
		const input = `
| abc | def |
| --- | --- |
`;
		const expected = `
<table>
<thead>
<tr>
<th>abc</th>
<th>def</th>
</tr>
</thead>
</table>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 206, line 3565: '> # Foo\\n> bar\\n> baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 207, line 3580: '># Foo\\n>bar\\n> baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 208, line 3595: '   > # Foo\\n   > bar\\n > baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 209, line 3610: '    > # Foo\\n    > bar\\n    > baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 210, line 3625: '> # Foo\\n> bar\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 211, line 3641: '> bar\\nbaz\\n> foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 212, line 3665: '> foo\\n---'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 213, line 3685: '> - foo\\n- bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 214, line 3703: '>     foo\\n    bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 215, line 3716: '> ```\\nfoo\\n```'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 216, line 3732: '> foo\\n    - bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 217, line 3756: '>'", () => {
		const input = `
>
`;
		const expected = `
<blockquote>
</blockquote>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 218, line 3764: '>\\n>  \\n> '", () => {
		const input = `
>
>  
> 
`;
		const expected = `
<blockquote>
</blockquote>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 219, line 3776: '>\\n> foo\\n>  '", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 220, line 3789: '> foo\\n\\n> bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 221, line 3811: '> foo\\n> bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 222, line 3824: '> foo\\n>\\n> bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 223, line 3838: 'foo\\n> bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 224, line 3852: '> aaa\\n***\\n> bbb'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 225, line 3870: '> bar\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 226, line 3881: '> bar\\n\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 227, line 3893: '> bar\\n>\\nbaz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 228, line 3909: '> > > foo\\nbar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 229, line 3924: '>>> foo\\n> bar\\n>>baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 230, line 3946: '>     code\\n\\n>    not code'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 231, line 4000: 'A paragraph\\nwith two lines.\\n\\n    indented code\\n\\n> A block quote.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 232, line 4022: '1.  A paragraph\\n    with two lines.\\n\\n        indented code\\n\\n    > A block quote.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 233, line 4055: '- one\\n\\n two'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 234, line 4067: '- one\\n\\n  two'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 235, line 4081: ' -    one\\n\\n     two'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 236, line 4094: ' -    one\\n\\n      two'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 237, line 4116: '   > > 1.  one\\n>>\\n>>     two'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 238, line 4143: '>>- one\\n>>\\n  >  > two'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 239, line 4162: '-one\\n\\n2.two'", () => {
		const input = `
-one

2.two
`;
		const expected = `
<p>-one</p>
<p>2.two</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 240, line 4175: '- foo\\n\\n\\n  bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 241, line 4192: '1.  foo\\n\\n    ```\\n    bar\\n    ```\\n\\n    baz\\n\\n    > bam'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 242, line 4220: '- Foo\\n\\n      bar\\n\\n\\n      baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 243, line 4242: '123456789. ok'", () => {
		const input = `
123456789. ok
`;
		const expected = `
<ol start="123456789">
<li>ok</li>
</ol>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 244, line 4251: '1234567890. not ok'", () => {
		const input = `
1234567890. not ok
`;
		const expected = `
<p>1234567890. not ok</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 245, line 4260: '0. ok'", () => {
		const input = `
0. ok
`;
		const expected = `
<ol start="0">
<li>ok</li>
</ol>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 246, line 4269: '003. ok'", () => {
		const input = `
003. ok
`;
		const expected = `
<ol start="3">
<li>ok</li>
</ol>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 247, line 4280: '-1. not ok'", () => {
		const input = `
-1. not ok
`;
		const expected = `
<p>-1. not ok</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 248, line 4303: '- foo\\n\\n      bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 249, line 4320: '  10.  foo\\n\\n           bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 250, line 4339: '    indented code\\n\\nparagraph\\n\\n    more code'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 251, line 4354: '1.     indented code\\n\\n   paragraph\\n\\n       more code'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 252, line 4376: '1.      indented code\\n\\n   paragraph\\n\\n       more code'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 253, line 4403: '   foo\\n\\nbar'", () => {
		const input = `
   foo

bar
`;
		const expected = `
<p>foo</p>
<p>bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 254, line 4413: '-    foo\\n\\n  bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 255, line 4430: '-  foo\\n\\n   bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 256, line 4458: '-\\n  foo\\n-\\n  ```\\n  bar\\n  ```\\n-\\n      baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 257, line 4484: '-   \\n  foo'", () => {
		const input = `
-   
  foo
`;
		const expected = `
<ul>
<li>foo</li>
</ul>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 258, line 4498: '-\\n\\n  foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 259, line 4512: '- foo\\n-\\n- bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 260, line 4527: '- foo\\n-   \\n- bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 261, line 4542: '1. foo\\n2.\\n3. bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 262, line 4557: '*'", () => {
		const input = `
*
`;
		const expected = `
<ul>
<li></li>
</ul>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 263, line 4567: 'foo\\n*\\n\\nfoo\\n1.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 264, line 4589: ' 1.  A paragraph\\n     with two lines.\\n\\n         indented code\\n\\n     > A block quote.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 265, line 4613: '  1.  A paragraph\\n      with two lines.\\n\\n          indented code\\n\\n      > A block quote.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 266, line 4637: '   1.  A paragraph\\n       with two lines.\\n\\n           indented code\\n\\n       > A block quote.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 267, line 4661: '    1.  A paragraph\\n        with two lines.\\n\\n            indented code\\n\\n        > A block quote.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 268, line 4691: '  1.  A paragraph\\nwith two lines.\\n\\n          indented code\\n\\n      > A block quote.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 269, line 4715: '  1.  A paragraph\\n    with two lines.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 270, line 4728: '> 1. > Blockquote\\ncontinued here.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 271, line 4745: '> 1. > Blockquote\\n> continued here.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 272, line 4773: '- foo\\n  - bar\\n    - baz\\n      - boo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 273, line 4799: '- foo\\n - bar\\n  - baz\\n   - boo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 274, line 4816: '10) foo\\n    - bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 275, line 4832: '10) foo\\n   - bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 276, line 4847: '- - foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 277, line 4860: '1. - 2. foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 278, line 4879: '- # Foo\\n- Bar\\n  ---\\n  baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 279, line 5108: '- [ ] foo\\n- [x] bar'", () => {
		const input = `
- [ ] foo
- [x] bar
`;
		const expected = `
<ul>
<li><input type="checkbox" disabled="" /> foo</li>
<li><input type="checkbox" checked="" disabled="" /> bar</li>
</ul>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 280, line 5120: '- [x] foo\\n  - [ ] bar\\n  - [x] baz\\n- [ ] bim'", () => {
		const input = `
- [x] foo
  - [ ] bar
  - [x] baz
- [ ] bim
`;
		const expected = `
<ul>
<li><input type="checkbox" checked="" disabled="" /> foo
<ul>
<li><input type="checkbox" disabled="" /> bar</li>
<li><input type="checkbox" checked="" disabled="" /> baz</li>
</ul>
</li>
<li><input type="checkbox" disabled="" /> bim</li>
</ul>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 281, line 5172: '- foo\\n- bar\\n+ baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 282, line 5187: '1. foo\\n2. bar\\n3) baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 283, line 5206: 'Foo\\n- bar\\n- baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 284, line 5283: 'The number of windows in my house is\\n14.  The number of doors is 6.'", () => {
		const input = `
The number of windows in my house is
14.  The number of doors is 6.
`;
		const expected = `
<p>The number of windows in my house is
14.  The number of doors is 6.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 285, line 5293: 'The number of windows in my house is\\n1.  The number of doors is 6.'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 286, line 5307: '- foo\\n\\n- bar\\n\\n\\n- baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 287, line 5328: '- foo\\n  - bar\\n    - baz\\n\\n\\n      bim'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 288, line 5358: '- foo\\n- bar\\n\\n<!-- -->\\n\\n- baz\\n- bim'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 289, line 5379: '-   foo\\n\\n    notcode\\n\\n-   foo\\n\\n<!-- -->\\n\\n    code'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 290, line 5410: '- a\\n - b\\n  - c\\n   - d\\n  - e\\n - f\\n- g'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 291, line 5431: '1. a\\n\\n  2. b\\n\\n   3. c'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 292, line 5455: '- a\\n - b\\n  - c\\n   - d\\n    - e'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 293, line 5475: '1. a\\n\\n  2. b\\n\\n    3. c'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 294, line 5498: '- a\\n- b\\n\\n- c'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 295, line 5520: '* a\\n*\\n\\n* c'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 296, line 5542: '- a\\n- b\\n\\n  c\\n- d'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 297, line 5564: '- a\\n- b\\n\\n  [ref]: /url\\n- d'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 298, line 5587: '- a\\n- ```\\n  b\\n\\n\\n  ```\\n- c'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 299, line 5613: '- a\\n  - b\\n\\n    c\\n- d'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 300, line 5637: '* a\\n  > b\\n  >\\n* c'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 301, line 5657: '- a\\n  > b\\n  ```\\n  c\\n  ```\\n- d'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 302, line 5680: '- a'", () => {
		const input = `
- a
`;
		const expected = `
<ul>
<li>a</li>
</ul>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 303, line 5689: '- a\\n  - b'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 304, line 5706: '1. ```\\n   foo\\n   ```\\n\\n   bar'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 305, line 5725: '* foo\\n  * bar\\n\\n  baz'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 306, line 5743: '- a\\n  - b\\n  - c\\n\\n- d\\n  - e\\n  - f'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 307, line 5777: '`hi`lo`'", () => {
		const input = `
\`hi\`lo\`
`;
		const expected = `
<p><code>hi</code>lo\`</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 308, line 5791: '\\!\\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\-\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\\\\\]\\^\\_\\`\\{\\|\\}\\~'", () => {
		const input = `
\\!\\"\\#\\$\\%\\&\\'\\(\\)\\*\\+\\,\\-\\.\\/\\:\\;\\<\\=\\>\\?\\@\\[\\\\\\]\\^\\_\\\`\\{\\|\\}\\~
`;
		const expected = `
<p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\\]^_\`{|}~</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 309, line 5801: '\\→\\A\\a\\ \\3\\φ\\«'", () => {
		const input = `
\\	\\A\\a\\ \\3\\φ\\«
`;
		const expected = `
<p>\\	\\A\\a\\ \\3\\φ\\«</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 310, line 5811: '\\*not emphasized*\\n\\<br/> not a tag\\n\\[not a link](/foo)\\n\\`not code`\\n1\\. not a list\\n\\* not a list\\n\\# not a heading\\n\\[foo]: /url \"not a reference\"\\n\\&ouml; not a character entity'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 311, line 5836: '\\\\*emphasis*'", () => {
		const input = `
\\\\*emphasis*
`;
		const expected = `
<p>\\<em>emphasis</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 312, line 5845: 'foo\\\\nbar'", () => {
		const input = `
foo\\
bar
`;
		const expected = `
<p>foo<br />
bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 313, line 5857: '`` \\[\\` ``'", () => {
		const input = `
\`\` \\[\\\` \`\`
`;
		const expected = `
<p><code>\\[\\\`</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 314, line 5864: '    \\[\\]'", () => {
		const input = `
    \\[\\]
`;
		const expected = `
<pre><code>\\[\\]
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 315, line 5872: '~~~\\n\\[\\]\\n~~~'", () => {
		const input = `
~~~
\\[\\]
~~~
`;
		const expected = `
<pre><code>\\[\\]
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 316, line 5882: '<http://example.com?find=\\*>'", () => {
		const input = `
<http://example.com?find=\\*>
`;
		const expected = `
<p><a href="http://example.com?find=%5C*">http://example.com?find=\\*</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 317, line 5889: '<a href=\"/bar\\/)\">'", () => {
		const input = `
<a href="/bar\\/)">
`;
		const expected = `
<a href="/bar\\/)">
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 318, line 5899: '[foo](/bar\\* \"ti\\*tle\")'", () => {
		const input = `
[foo](/bar\\* "ti\\*tle")
`;
		const expected = `
<p><a href="/bar*" title="ti*tle">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 319, line 5906: '[foo]\\n\\n[foo]: /bar\\* \"ti\\*tle\"'", () => {
		const input = `
[foo]

[foo]: /bar\\* "ti\\*tle"
`;
		const expected = `
<p><a href="/bar*" title="ti*tle">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 320, line 5915: '``` foo\\+bar\\nfoo\\n```'", () => {
		const input = `
\`\`\` foo\\+bar
foo
\`\`\`
`;
		const expected = `
<pre><code class="language-foo+bar">foo
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 321, line 5952: '&nbsp; &amp; &copy; &AElig; &Dcaron;\\n&frac34; &HilbertSpace; &DifferentialD;\\n&ClockwiseContourIntegral; &ngE;'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 322, line 5971: '&#35; &#1234; &#992; &#0;'", () => {
		const input = `
&#35; &#1234; &#992; &#0;
`;
		const expected = `
<p># Ӓ Ϡ �</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 323, line 5984: '&#X22; &#XD06; &#xcab;'", () => {
		const input = `
&#X22; &#XD06; &#xcab;
`;
		const expected = `
<p>&quot; ആ ಫ</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 324, line 5993: '&nbsp &x; &#; &#x;\\n&#987654321;\\n&#abcdef0;\\n&ThisIsNotDefined; &hi?;'", () => {
		const input = `
&nbsp &x; &#; &#x;
&#987654321;
&#abcdef0;
&ThisIsNotDefined; &hi?;
`;
		const expected = `
<p>&amp;nbsp &amp;x; &amp;#; &amp;#x;
&amp;#987654321;
&amp;#abcdef0;
&amp;ThisIsNotDefined; &amp;hi?;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 325, line 6010: '&copy'", () => {
		const input = `
&copy
`;
		const expected = `
<p>&amp;copy</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 326, line 6020: '&MadeUpEntity;'", () => {
		const input = `
&MadeUpEntity;
`;
		const expected = `
<p>&amp;MadeUpEntity;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 327, line 6031: '<a href=\"&ouml;&ouml;.html\">'", () => {
		const input = `
<a href="&ouml;&ouml;.html">
`;
		const expected = `
<a href="&ouml;&ouml;.html">
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 328, line 6038: '[foo](/f&ouml;&ouml; \"f&ouml;&ouml;\")'", () => {
		const input = `
[foo](/f&ouml;&ouml; "f&ouml;&ouml;")
`;
		const expected = `
<p><a href="/f%C3%B6%C3%B6" title="föö">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 329, line 6045: '[foo]\\n\\n[foo]: /f&ouml;&ouml; \"f&ouml;&ouml;\"'", () => {
		const input = `
[foo]

[foo]: /f&ouml;&ouml; "f&ouml;&ouml;"
`;
		const expected = `
<p><a href="/f%C3%B6%C3%B6" title="föö">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 330, line 6054: '``` f&ouml;&ouml;\\nfoo\\n```'", () => {
		const input = `
\`\`\` f&ouml;&ouml;
foo
\`\`\`
`;
		const expected = `
<pre><code class="language-föö">foo
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 331, line 6067: '`f&ouml;&ouml;`'", () => {
		const input = `
\`f&ouml;&ouml;\`
`;
		const expected = `
<p><code>f&amp;ouml;&amp;ouml;</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 332, line 6074: '    f&ouml;f&ouml;'", () => {
		const input = `
    f&ouml;f&ouml;
`;
		const expected = `
<pre><code>f&amp;ouml;f&amp;ouml;
</code></pre>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 333, line 6086: '&#42;foo&#42;\\n*foo*'", () => {
		const input = `
&#42;foo&#42;
*foo*
`;
		const expected = `
<p>*foo*
<em>foo</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 334, line 6094: '&#42; foo\\n\\n* foo'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 335, line 6105: 'foo&#10;&#10;bar'", () => {
		const input = `
foo&#10;&#10;bar
`;
		const expected = `
<p>foo

bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 336, line 6113: '&#9;foo'", () => {
		const input = `
&#9;foo
`;
		const expected = `
<p>	foo</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 337, line 6120: '[a](url &quot;tit&quot;)'", () => {
		const input = `
[a](url &quot;tit&quot;)
`;
		const expected = `
<p>[a](url &quot;tit&quot;)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 338, line 6148: '`foo`'", () => {
		const input = `
\`foo\`
`;
		const expected = `
<p><code>foo</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 339, line 6159: '`` foo ` bar ``'", () => {
		const input = `
\`\` foo \` bar \`\`
`;
		const expected = `
<p><code>foo \` bar</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 340, line 6169: '` `` `'", () => {
		const input = `
\` \`\` \`
`;
		const expected = `
<p><code>\`\`</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 341, line 6177: '`  ``  `'", () => {
		const input = `
\`  \`\`  \`
`;
		const expected = `
<p><code> \`\` </code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 342, line 6186: '` a`'", () => {
		const input = `
\` a\`
`;
		const expected = `
<p><code> a</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 343, line 6195: '` b `'", () => {
		const input = `
\` b \`
`;
		const expected = `
<p><code> b </code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 344, line 6203: '` `\\n`  `'", () => {
		const input = `
\` \`
\`  \`
`;
		const expected = `
<p><code> </code>
<code>  </code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 345, line 6214: '``\\nfoo\\nbar  \\nbaz\\n``'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 346, line 6224: '``\\nfoo \\n``'", () => {
		const input = `
\`\`
foo 
\`\`
`;
		const expected = `
<p><code>foo </code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 347, line 6235: '`foo   bar \\nbaz`'", () => {
		const input = `
\`foo   bar 
baz\`
`;
		const expected = `
<p><code>foo   bar  baz</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 348, line 6252: '`foo\\`bar`'", () => {
		const input = `
\`foo\\\`bar\`
`;
		const expected = `
<p><code>foo\\</code>bar\`</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 349, line 6263: '``foo`bar``'", () => {
		const input = `
\`\`foo\`bar\`\`
`;
		const expected = `
<p><code>foo\`bar</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 350, line 6269: '` foo `` bar `'", () => {
		const input = `
\` foo \`\` bar \`
`;
		const expected = `
<p><code>foo \`\` bar</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 351, line 6281: '*foo`*`'", () => {
		const input = `
*foo\`*\`
`;
		const expected = `
<p>*foo<code>*</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 352, line 6290: '[not a `link](/foo`)'", () => {
		const input = `
[not a \`link](/foo\`)
`;
		const expected = `
<p>[not a <code>link](/foo</code>)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 353, line 6300: '`<a href=\"`\">`'", () => {
		const input = `
\`<a href="\`">\`
`;
		const expected = `
<p><code>&lt;a href=&quot;</code>&quot;&gt;\`</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 354, line 6309: '<a href=\"`\">`'", () => {
		const input = `
<a href="\`">\`
`;
		const expected = `
<p><a href="\`">\`</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 355, line 6318: '`<http://foo.bar.`baz>`'", () => {
		const input = `
\`<http://foo.bar.\`baz>\`
`;
		const expected = `
<p><code>&lt;http://foo.bar.</code>baz&gt;\`</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 356, line 6327: '<http://foo.bar.`baz>`'", () => {
		const input = `
<http://foo.bar.\`baz>\`
`;
		const expected = `
<p><a href="http://foo.bar.%60baz">http://foo.bar.\`baz</a>\`</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 357, line 6337: '```foo``'", () => {
		const input = `
\`\`\`foo\`\`
`;
		const expected = `
<p>\`\`\`foo\`\`</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 358, line 6344: '`foo'", () => {
		const input = `
\`foo
`;
		const expected = `
<p>\`foo</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 359, line 6353: '`foo``bar``'", () => {
		const input = `
\`foo\`\`bar\`\`
`;
		const expected = `
<p>\`foo<code>bar</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 360, line 6570: '*foo bar*'", () => {
		const input = `
*foo bar*
`;
		const expected = `
<p><em>foo bar</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 361, line 6580: 'a * foo bar*'", () => {
		const input = `
a * foo bar*
`;
		const expected = `
<p>a * foo bar*</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 362, line 6591: 'a*\"foo\"*'", () => {
		const input = `
a*"foo"*
`;
		const expected = `
<p>a*&quot;foo&quot;*</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 363, line 6600: '* a *'", () => {
		const input = `
* a *
`;
		const expected = `
<p>* a *</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 364, line 6609: 'foo*bar*'", () => {
		const input = `
foo*bar*
`;
		const expected = `
<p>foo<em>bar</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 365, line 6616: '5*6*78'", () => {
		const input = `
5*6*78
`;
		const expected = `
<p>5<em>6</em>78</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 366, line 6625: '_foo bar_'", () => {
		const input = `
_foo bar_
`;
		const expected = `
<p><em>foo bar</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 367, line 6635: '_ foo bar_'", () => {
		const input = `
_ foo bar_
`;
		const expected = `
<p>_ foo bar_</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 368, line 6645: 'a_\"foo\"_'", () => {
		const input = `
a_"foo"_
`;
		const expected = `
<p>a_&quot;foo&quot;_</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 369, line 6654: 'foo_bar_'", () => {
		const input = `
foo_bar_
`;
		const expected = `
<p>foo_bar_</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 370, line 6661: '5_6_78'", () => {
		const input = `
5_6_78
`;
		const expected = `
<p>5_6_78</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 371, line 6668: 'пристаням_стремятся_'", () => {
		const input = `
пристаням_стремятся_
`;
		const expected = `
<p>пристаням_стремятся_</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 372, line 6678: 'aa_\"bb\"_cc'", () => {
		const input = `
aa_"bb"_cc
`;
		const expected = `
<p>aa_&quot;bb&quot;_cc</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 373, line 6689: 'foo-_(bar)_'", () => {
		const input = `
foo-_(bar)_
`;
		const expected = `
<p>foo-<em>(bar)</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 374, line 6701: '_foo*'", () => {
		const input = `
_foo*
`;
		const expected = `
<p>_foo*</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 375, line 6711: '*foo bar *'", () => {
		const input = `
*foo bar *
`;
		const expected = `
<p>*foo bar *</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 376, line 6720: '*foo bar\\n*'", () => {
		const input = `
*foo bar
*
`;
		const expected = `
<p>*foo bar
*</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 377, line 6733: '*(*foo)'", () => {
		const input = `
*(*foo)
`;
		const expected = `
<p>*(*foo)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 378, line 6743: '*(*foo*)*'", () => {
		const input = `
*(*foo*)*
`;
		const expected = `
<p><em>(<em>foo</em>)</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 379, line 6752: '*foo*bar'", () => {
		const input = `
*foo*bar
`;
		const expected = `
<p><em>foo</em>bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 380, line 6765: '_foo bar _'", () => {
		const input = `
_foo bar _
`;
		const expected = `
<p>_foo bar _</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 381, line 6775: '_(_foo)'", () => {
		const input = `
_(_foo)
`;
		const expected = `
<p>_(_foo)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 382, line 6784: '_(_foo_)_'", () => {
		const input = `
_(_foo_)_
`;
		const expected = `
<p><em>(<em>foo</em>)</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 383, line 6793: '_foo_bar'", () => {
		const input = `
_foo_bar
`;
		const expected = `
<p>_foo_bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 384, line 6800: '_пристаням_стремятся'", () => {
		const input = `
_пристаням_стремятся
`;
		const expected = `
<p>_пристаням_стремятся</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 385, line 6807: '_foo_bar_baz_'", () => {
		const input = `
_foo_bar_baz_
`;
		const expected = `
<p><em>foo_bar_baz</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 386, line 6818: '_(bar)_.'", () => {
		const input = `
_(bar)_.
`;
		const expected = `
<p><em>(bar)</em>.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 387, line 6827: '**foo bar**'", () => {
		const input = `
**foo bar**
`;
		const expected = `
<p><strong>foo bar</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 388, line 6837: '** foo bar**'", () => {
		const input = `
** foo bar**
`;
		const expected = `
<p>** foo bar**</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 389, line 6848: 'a**\"foo\"**'", () => {
		const input = `
a**"foo"**
`;
		const expected = `
<p>a**&quot;foo&quot;**</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 390, line 6857: 'foo**bar**'", () => {
		const input = `
foo**bar**
`;
		const expected = `
<p>foo<strong>bar</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 391, line 6866: '__foo bar__'", () => {
		const input = `
__foo bar__
`;
		const expected = `
<p><strong>foo bar</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 392, line 6876: '__ foo bar__'", () => {
		const input = `
__ foo bar__
`;
		const expected = `
<p>__ foo bar__</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 393, line 6884: '__\\nfoo bar__'", () => {
		const input = `
__
foo bar__
`;
		const expected = `
<p>__
foo bar__</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 394, line 6896: 'a__\"foo\"__'", () => {
		const input = `
a__"foo"__
`;
		const expected = `
<p>a__&quot;foo&quot;__</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 395, line 6905: 'foo__bar__'", () => {
		const input = `
foo__bar__
`;
		const expected = `
<p>foo__bar__</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 396, line 6912: '5__6__78'", () => {
		const input = `
5__6__78
`;
		const expected = `
<p>5__6__78</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 397, line 6919: 'пристаням__стремятся__'", () => {
		const input = `
пристаням__стремятся__
`;
		const expected = `
<p>пристаням__стремятся__</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 398, line 6926: '__foo, __bar__, baz__'", () => {
		const input = `
__foo, __bar__, baz__
`;
		const expected = `
<p><strong>foo, <strong>bar</strong>, baz</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 399, line 6937: 'foo-__(bar)__'", () => {
		const input = `
foo-__(bar)__
`;
		const expected = `
<p>foo-<strong>(bar)</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 400, line 6950: '**foo bar **'", () => {
		const input = `
**foo bar **
`;
		const expected = `
<p>**foo bar **</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 401, line 6963: '**(**foo)'", () => {
		const input = `
**(**foo)
`;
		const expected = `
<p>**(**foo)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 402, line 6973: '*(**foo**)*'", () => {
		const input = `
*(**foo**)*
`;
		const expected = `
<p><em>(<strong>foo</strong>)</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 403, line 6980: '**Gomphocarpus (*Gomphocarpus physocarpus*, syn.\\n*Asclepias physocarpa*)**'", () => {
		const input = `
**Gomphocarpus (*Gomphocarpus physocarpus*, syn.
*Asclepias physocarpa*)**
`;
		const expected = `
<p><strong>Gomphocarpus (<em>Gomphocarpus physocarpus</em>, syn.
<em>Asclepias physocarpa</em>)</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 404, line 6989: '**foo \"*bar*\" foo**'", () => {
		const input = `
**foo "*bar*" foo**
`;
		const expected = `
<p><strong>foo &quot;<em>bar</em>&quot; foo</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 405, line 6998: '**foo**bar'", () => {
		const input = `
**foo**bar
`;
		const expected = `
<p><strong>foo</strong>bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 406, line 7010: '__foo bar __'", () => {
		const input = `
__foo bar __
`;
		const expected = `
<p>__foo bar __</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 407, line 7020: '__(__foo)'", () => {
		const input = `
__(__foo)
`;
		const expected = `
<p>__(__foo)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 408, line 7030: '_(__foo__)_'", () => {
		const input = `
_(__foo__)_
`;
		const expected = `
<p><em>(<strong>foo</strong>)</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 409, line 7039: '__foo__bar'", () => {
		const input = `
__foo__bar
`;
		const expected = `
<p>__foo__bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 410, line 7046: '__пристаням__стремятся'", () => {
		const input = `
__пристаням__стремятся
`;
		const expected = `
<p>__пристаням__стремятся</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 411, line 7053: '__foo__bar__baz__'", () => {
		const input = `
__foo__bar__baz__
`;
		const expected = `
<p><strong>foo__bar__baz</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 412, line 7064: '__(bar)__.'", () => {
		const input = `
__(bar)__.
`;
		const expected = `
<p><strong>(bar)</strong>.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 413, line 7076: '*foo [bar](/url)*'", () => {
		const input = `
*foo [bar](/url)*
`;
		const expected = `
<p><em>foo <a href="/url">bar</a></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 414, line 7083: '*foo\\nbar*'", () => {
		const input = `
*foo
bar*
`;
		const expected = `
<p><em>foo
bar</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 415, line 7095: '_foo __bar__ baz_'", () => {
		const input = `
_foo __bar__ baz_
`;
		const expected = `
<p><em>foo <strong>bar</strong> baz</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 416, line 7102: '_foo _bar_ baz_'", () => {
		const input = `
_foo _bar_ baz_
`;
		const expected = `
<p><em>foo <em>bar</em> baz</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 417, line 7109: '__foo_ bar_'", () => {
		const input = `
__foo_ bar_
`;
		const expected = `
<p><em><em>foo</em> bar</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 418, line 7116: '*foo *bar**'", () => {
		const input = `
*foo *bar**
`;
		const expected = `
<p><em>foo <em>bar</em></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 419, line 7123: '*foo **bar** baz*'", () => {
		const input = `
*foo **bar** baz*
`;
		const expected = `
<p><em>foo <strong>bar</strong> baz</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 420, line 7129: '*foo**bar**baz*'", () => {
		const input = `
*foo**bar**baz*
`;
		const expected = `
<p><em>foo<strong>bar</strong>baz</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 421, line 7153: '*foo**bar*'", () => {
		const input = `
*foo**bar*
`;
		const expected = `
<p><em>foo**bar</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 422, line 7166: '***foo** bar*'", () => {
		const input = `
***foo** bar*
`;
		const expected = `
<p><em><strong>foo</strong> bar</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 423, line 7173: '*foo **bar***'", () => {
		const input = `
*foo **bar***
`;
		const expected = `
<p><em>foo <strong>bar</strong></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 424, line 7180: '*foo**bar***'", () => {
		const input = `
*foo**bar***
`;
		const expected = `
<p><em>foo<strong>bar</strong></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 425, line 7191: 'foo***bar***baz'", () => {
		const input = `
foo***bar***baz
`;
		const expected = `
<p>foo<em><strong>bar</strong></em>baz</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 426, line 7197: 'foo******bar*********baz'", () => {
		const input = `
foo******bar*********baz
`;
		const expected = `
<p>foo<strong><strong><strong>bar</strong></strong></strong>***baz</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 427, line 7206: '*foo **bar *baz* bim** bop*'", () => {
		const input = `
*foo **bar *baz* bim** bop*
`;
		const expected = `
<p><em>foo <strong>bar <em>baz</em> bim</strong> bop</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 428, line 7213: '*foo [*bar*](/url)*'", () => {
		const input = `
*foo [*bar*](/url)*
`;
		const expected = `
<p><em>foo <a href="/url"><em>bar</em></a></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 429, line 7222: '** is not an empty emphasis'", () => {
		const input = `
** is not an empty emphasis
`;
		const expected = `
<p>** is not an empty emphasis</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 430, line 7229: '**** is not an empty strong emphasis'", () => {
		const input = `
**** is not an empty strong emphasis
`;
		const expected = `
<p>**** is not an empty strong emphasis</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 431, line 7242: '**foo [bar](/url)**'", () => {
		const input = `
**foo [bar](/url)**
`;
		const expected = `
<p><strong>foo <a href="/url">bar</a></strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 432, line 7249: '**foo\\nbar**'", () => {
		const input = `
**foo
bar**
`;
		const expected = `
<p><strong>foo
bar</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 433, line 7261: '__foo _bar_ baz__'", () => {
		const input = `
__foo _bar_ baz__
`;
		const expected = `
<p><strong>foo <em>bar</em> baz</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 434, line 7268: '__foo __bar__ baz__'", () => {
		const input = `
__foo __bar__ baz__
`;
		const expected = `
<p><strong>foo <strong>bar</strong> baz</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 435, line 7275: '____foo__ bar__'", () => {
		const input = `
____foo__ bar__
`;
		const expected = `
<p><strong><strong>foo</strong> bar</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 436, line 7282: '**foo **bar****'", () => {
		const input = `
**foo **bar****
`;
		const expected = `
<p><strong>foo <strong>bar</strong></strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 437, line 7289: '**foo *bar* baz**'", () => {
		const input = `
**foo *bar* baz**
`;
		const expected = `
<p><strong>foo <em>bar</em> baz</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 438, line 7296: '**foo*bar*baz**'", () => {
		const input = `
**foo*bar*baz**
`;
		const expected = `
<p><strong>foo<em>bar</em>baz</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 439, line 7303: '***foo* bar**'", () => {
		const input = `
***foo* bar**
`;
		const expected = `
<p><strong><em>foo</em> bar</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 440, line 7310: '**foo *bar***'", () => {
		const input = `
**foo *bar***
`;
		const expected = `
<p><strong>foo <em>bar</em></strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 441, line 7319: '**foo *bar **baz**\\nbim* bop**'", () => {
		const input = `
**foo *bar **baz**
bim* bop**
`;
		const expected = `
<p><strong>foo <em>bar <strong>baz</strong>
bim</em> bop</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 442, line 7328: '**foo [*bar*](/url)**'", () => {
		const input = `
**foo [*bar*](/url)**
`;
		const expected = `
<p><strong>foo <a href="/url"><em>bar</em></a></strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 443, line 7337: '__ is not an empty emphasis'", () => {
		const input = `
__ is not an empty emphasis
`;
		const expected = `
<p>__ is not an empty emphasis</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 444, line 7344: '____ is not an empty strong emphasis'", () => {
		const input = `
____ is not an empty strong emphasis
`;
		const expected = `
<p>____ is not an empty strong emphasis</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 445, line 7354: 'foo ***'", () => {
		const input = `
foo ***
`;
		const expected = `
<p>foo ***</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 446, line 7361: 'foo *\\**'", () => {
		const input = `
foo *\\**
`;
		const expected = `
<p>foo <em>*</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 447, line 7368: 'foo *_*'", () => {
		const input = `
foo *_*
`;
		const expected = `
<p>foo <em>_</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 448, line 7375: 'foo *****'", () => {
		const input = `
foo *****
`;
		const expected = `
<p>foo *****</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 449, line 7382: 'foo **\\***'", () => {
		const input = `
foo **\\***
`;
		const expected = `
<p>foo <strong>*</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 450, line 7389: 'foo **_**'", () => {
		const input = `
foo **_**
`;
		const expected = `
<p>foo <strong>_</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 451, line 7400: '**foo*'", () => {
		const input = `
**foo*
`;
		const expected = `
<p>*<em>foo</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 452, line 7407: '*foo**'", () => {
		const input = `
*foo**
`;
		const expected = `
<p><em>foo</em>*</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 453, line 7414: '***foo**'", () => {
		const input = `
***foo**
`;
		const expected = `
<p>*<strong>foo</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 454, line 7421: '****foo*'", () => {
		const input = `
****foo*
`;
		const expected = `
<p>***<em>foo</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 455, line 7428: '**foo***'", () => {
		const input = `
**foo***
`;
		const expected = `
<p><strong>foo</strong>*</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 456, line 7435: '*foo****'", () => {
		const input = `
*foo****
`;
		const expected = `
<p><em>foo</em>***</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 457, line 7445: 'foo ___'", () => {
		const input = `
foo ___
`;
		const expected = `
<p>foo ___</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 458, line 7452: 'foo _\\__'", () => {
		const input = `
foo _\\__
`;
		const expected = `
<p>foo <em>_</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 459, line 7459: 'foo _*_'", () => {
		const input = `
foo _*_
`;
		const expected = `
<p>foo <em>*</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 460, line 7466: 'foo _____'", () => {
		const input = `
foo _____
`;
		const expected = `
<p>foo _____</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 461, line 7473: 'foo __\\___'", () => {
		const input = `
foo __\\___
`;
		const expected = `
<p>foo <strong>_</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 462, line 7480: 'foo __*__'", () => {
		const input = `
foo __*__
`;
		const expected = `
<p>foo <strong>*</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 463, line 7487: '__foo_'", () => {
		const input = `
__foo_
`;
		const expected = `
<p>_<em>foo</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 464, line 7498: '_foo__'", () => {
		const input = `
_foo__
`;
		const expected = `
<p><em>foo</em>_</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 465, line 7505: '___foo__'", () => {
		const input = `
___foo__
`;
		const expected = `
<p>_<strong>foo</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 466, line 7512: '____foo_'", () => {
		const input = `
____foo_
`;
		const expected = `
<p>___<em>foo</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 467, line 7519: '__foo___'", () => {
		const input = `
__foo___
`;
		const expected = `
<p><strong>foo</strong>_</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 468, line 7526: '_foo____'", () => {
		const input = `
_foo____
`;
		const expected = `
<p><em>foo</em>___</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 469, line 7536: '**foo**'", () => {
		const input = `
**foo**
`;
		const expected = `
<p><strong>foo</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 470, line 7543: '*_foo_*'", () => {
		const input = `
*_foo_*
`;
		const expected = `
<p><em><em>foo</em></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 471, line 7550: '__foo__'", () => {
		const input = `
__foo__
`;
		const expected = `
<p><strong>foo</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 472, line 7557: '_*foo*_'", () => {
		const input = `
_*foo*_
`;
		const expected = `
<p><em><em>foo</em></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 473, line 7567: '****foo****'", () => {
		const input = `
****foo****
`;
		const expected = `
<p><strong><strong>foo</strong></strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 474, line 7574: '____foo____'", () => {
		const input = `
____foo____
`;
		const expected = `
<p><strong><strong>foo</strong></strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 475, line 7585: '******foo******'", () => {
		const input = `
******foo******
`;
		const expected = `
<p><strong><strong><strong>foo</strong></strong></strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 476, line 7594: '***foo***'", () => {
		const input = `
***foo***
`;
		const expected = `
<p><em><strong>foo</strong></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 477, line 7601: '_____foo_____'", () => {
		const input = `
_____foo_____
`;
		const expected = `
<p><em><strong><strong>foo</strong></strong></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 478, line 7610: '*foo _bar* baz_'", () => {
		const input = `
*foo _bar* baz_
`;
		const expected = `
<p><em>foo _bar</em> baz_</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 479, line 7617: '*foo __bar *baz bim__ bam*'", () => {
		const input = `
*foo __bar *baz bim__ bam*
`;
		const expected = `
<p><em>foo <strong>bar *baz bim</strong> bam</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 480, line 7626: '**foo **bar baz**'", () => {
		const input = `
**foo **bar baz**
`;
		const expected = `
<p>**foo <strong>bar baz</strong></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 481, line 7633: '*foo *bar baz*'", () => {
		const input = `
*foo *bar baz*
`;
		const expected = `
<p>*foo <em>bar baz</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 482, line 7642: '*[bar*](/url)'", () => {
		const input = `
*[bar*](/url)
`;
		const expected = `
<p>*<a href="/url">bar*</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 483, line 7649: '_foo [bar_](/url)'", () => {
		const input = `
_foo [bar_](/url)
`;
		const expected = `
<p>_foo <a href="/url">bar_</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test('Example 484, line 7656: \'*<img src="foo" title="*"/>\'', () => {
		const input = `
*<img src="foo" title="*"/>
`;
		const expected = `
<p>*<img src="foo" title="*"/></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 485, line 7663: '**<a href=\"**\">'", () => {
		const input = `
**<a href="**">
`;
		const expected = `
<p>**<a href="**"></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 486, line 7670: '__<a href=\"__\">'", () => {
		const input = `
__<a href="__">
`;
		const expected = `
<p>__<a href="__"></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 487, line 7677: '*a `*`*'", () => {
		const input = `
*a \`*\`*
`;
		const expected = `
<p><em>a <code>*</code></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 488, line 7684: '_a `_`_'", () => {
		const input = `
_a \`_\`_
`;
		const expected = `
<p><em>a <code>_</code></em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 489, line 7691: '**a<http://foo.bar/?q=**>'", () => {
		const input = `
**a<http://foo.bar/?q=**>
`;
		const expected = `
<p>**a<a href="http://foo.bar/?q=**">http://foo.bar/?q=**</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 490, line 7698: '__a<http://foo.bar/?q=__>'", () => {
		const input = `
__a<http://foo.bar/?q=__>
`;
		const expected = `
<p>__a<a href="http://foo.bar/?q=__">http://foo.bar/?q=__</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 491, line 7714: '~~Hi~~ Hello, world!'", () => {
		const input = `
~~Hi~~ Hello, world!
`;
		const expected = `
<p><del>Hi</del> Hello, world!</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 492, line 7723: 'This ~~has a\\n\\nnew paragraph~~.'", () => {
		const input = `
This ~~has a

new paragraph~~.
`;
		const expected = `
<p>This ~~has a</p>
<p>new paragraph~~.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 493, line 7734: 'This will ~~~not~~~ strike.'", () => {
		const input = `
This will ~~~not~~~ strike.
`;
		const expected = `
<p>This will ~~~not~~~ strike.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 494, line 7817: '[link](/uri \"title\")'", () => {
		const input = `
[link](/uri "title")
`;
		const expected = `
<p><a href="/uri" title="title">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 495, line 7826: '[link](/uri)'", () => {
		const input = `
[link](/uri)
`;
		const expected = `
<p><a href="/uri">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 496, line 7835: '[link]()'", () => {
		const input = `
[link]()
`;
		const expected = `
<p><a href="">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 497, line 7842: '[link](<>)'", () => {
		const input = `
[link](<>)
`;
		const expected = `
<p><a href="">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 498, line 7851: '[link](/my uri)'", () => {
		const input = `
[link](/my uri)
`;
		const expected = `
<p>[link](/my uri)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 499, line 7857: '[link](</my uri>)'", () => {
		const input = `
[link](</my uri>)
`;
		const expected = `
<p><a href="/my%20uri">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 500, line 7866: '[link](foo\\nbar)'", () => {
		const input = `
[link](foo
bar)
`;
		const expected = `
<p>[link](foo
bar)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 501, line 7874: '[link](<foo\\nbar>)'", () => {
		const input = `
[link](<foo
bar>)
`;
		const expected = `
<p>[link](<foo
bar>)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 502, line 7885: '[a](<b)c>)'", () => {
		const input = `
[a](<b)c>)
`;
		const expected = `
<p><a href="b)c">a</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 503, line 7893: '[link](<foo\\>)'", () => {
		const input = `
[link](<foo\\>)
`;
		const expected = `
<p>[link](&lt;foo&gt;)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 504, line 7902: '[a](<b)c\\n[a](<b)c>\\n[a](<b>c)'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 505, line 7914: '[link](\\(foo\\))'", () => {
		const input = `
[link](\\(foo\\))
`;
		const expected = `
<p><a href="(foo)">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 506, line 7923: '[link](foo(and(bar)))'", () => {
		const input = `
[link](foo(and(bar)))
`;
		const expected = `
<p><a href="foo(and(bar))">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 507, line 7932: '[link](foo\\(and\\(bar\\))'", () => {
		const input = `
[link](foo\\(and\\(bar\\))
`;
		const expected = `
<p><a href="foo(and(bar)">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 508, line 7939: '[link](<foo(and(bar)>)'", () => {
		const input = `
[link](<foo(and(bar)>)
`;
		const expected = `
<p><a href="foo(and(bar)">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 509, line 7949: '[link](foo\\)\\:)'", () => {
		const input = `
[link](foo\\)\\:)
`;
		const expected = `
<p><a href="foo):">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 510, line 7958: '[link](#fragment)\\n\\n[link](http://example.com#fragment)\\n\\n[link](http://example.com?foo=3#frag)'", () => {
		const input = `
[link](#fragment)

[link](http://example.com#fragment)

[link](http://example.com?foo=3#frag)
`;
		const expected = `
<p><a href="#fragment">link</a></p>
<p><a href="http://example.com#fragment">link</a></p>
<p><a href="http://example.com?foo=3#frag">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 511, line 7974: '[link](foo\\bar)'", () => {
		const input = `
[link](foo\\bar)
`;
		const expected = `
<p><a href="foo%5Cbar">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 512, line 7990: '[link](foo%20b&auml;)'", () => {
		const input = `
[link](foo%20b&auml;)
`;
		const expected = `
<p><a href="foo%20b%C3%A4">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 513, line 8001: '[link](\"title\")'", () => {
		const input = `
[link]("title")
`;
		const expected = `
<p><a href="%22title%22">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 514, line 8010: '[link](/url \"title\")\\n[link](/url 'title')\\n[link](/url (title))'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test('Example 515, line 8024: \'[link](/url "title \\"&quot;")\'', () => {
		const input = `
[link](/url "title \\"&quot;")
`;
		const expected = `
<p><a href="/url" title="title &quot;&quot;">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 516, line 8034: '[link](/url \"title\")'", () => {
		const input = `
[link](/url "title")
`;
		const expected = `
<p><a href="/url%C2%A0%22title%22">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test('Example 517, line 8043: \'[link](/url "title "and" title")\'', () => {
		const input = `
[link](/url "title "and" title")
`;
		const expected = `
<p>[link](/url &quot;title &quot;and&quot; title&quot;)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 518, line 8052: '[link](/url 'title \"and\" title')'", () => {
		const input = `
[link](/url 'title "and" title')
`;
		const expected = `
<p><a href="/url" title="title &quot;and&quot; title">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 519, line 8076: '[link](   /uri\\n  \"title\"  )'", () => {
		const input = `
[link](   /uri
  "title"  )
`;
		const expected = `
<p><a href="/uri" title="title">link</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 520, line 8087: '[link] (/uri)'", () => {
		const input = `
[link] (/uri)
`;
		const expected = `
<p>[link] (/uri)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 521, line 8097: '[link [foo [bar]]](/uri)'", () => {
		const input = `
[link [foo [bar]]](/uri)
`;
		const expected = `
<p><a href="/uri">link [foo [bar]]</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 522, line 8104: '[link] bar](/uri)'", () => {
		const input = `
[link] bar](/uri)
`;
		const expected = `
<p>[link] bar](/uri)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 523, line 8111: '[link [bar](/uri)'", () => {
		const input = `
[link [bar](/uri)
`;
		const expected = `
<p>[link <a href="/uri">bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 524, line 8118: '[link \\[bar](/uri)'", () => {
		const input = `
[link \\[bar](/uri)
`;
		const expected = `
<p><a href="/uri">link [bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 525, line 8127: '[link *foo **bar** `#`*](/uri)'", () => {
		const input = `
[link *foo **bar** \`#\`*](/uri)
`;
		const expected = `
<p><a href="/uri">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 526, line 8134: '[![moon](moon.jpg)](/uri)'", () => {
		const input = `
[![moon](moon.jpg)](/uri)
`;
		const expected = `
<p><a href="/uri"><img src="moon.jpg" alt="moon" /></a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 527, line 8143: '[foo [bar](/uri)](/uri)'", () => {
		const input = `
[foo [bar](/uri)](/uri)
`;
		const expected = `
<p>[foo <a href="/uri">bar</a>](/uri)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 528, line 8150: '[foo *[bar [baz](/uri)](/uri)*](/uri)'", () => {
		const input = `
[foo *[bar [baz](/uri)](/uri)*](/uri)
`;
		const expected = `
<p>[foo <em>[bar <a href="/uri">baz</a>](/uri)</em>](/uri)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 529, line 8157: '![[[foo](uri1)](uri2)](uri3)'", () => {
		const input = `
![[[foo](uri1)](uri2)](uri3)
`;
		const expected = `
<p><img src="uri3" alt="[foo](uri2)" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 530, line 8167: '*[foo*](/uri)'", () => {
		const input = `
*[foo*](/uri)
`;
		const expected = `
<p>*<a href="/uri">foo*</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 531, line 8174: '[foo *bar](baz*)'", () => {
		const input = `
[foo *bar](baz*)
`;
		const expected = `
<p><a href="baz*">foo *bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 532, line 8184: '*foo [bar* baz]'", () => {
		const input = `
*foo [bar* baz]
`;
		const expected = `
<p><em>foo [bar</em> baz]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 533, line 8194: '[foo <bar attr=\"](baz)\">'", () => {
		const input = `
[foo <bar attr="](baz)">
`;
		const expected = `
<p>[foo <bar attr="](baz)"></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 534, line 8201: '[foo`](/uri)`'", () => {
		const input = `
[foo\`](/uri)\`
`;
		const expected = `
<p>[foo<code>](/uri)</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 535, line 8208: '[foo<http://example.com/?search=](uri)>'", () => {
		const input = `
[foo<http://example.com/?search=](uri)>
`;
		const expected = `
<p>[foo<a href="http://example.com/?search=%5D(uri)">http://example.com/?search=](uri)</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 536, line 8246: '[foo][bar]\\n\\n[bar]: /url \"title\"'", () => {
		const input = `
[foo][bar]

[bar]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 537, line 8261: '[link [foo [bar]]][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[link [foo [bar]]][ref]

[ref]: /uri
`;
		const expected = `
<p><a href="/uri">link [foo [bar]]</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 538, line 8270: '[link \\[bar][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[link \\[bar][ref]

[ref]: /uri
`;
		const expected = `
<p><a href="/uri">link [bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 539, line 8281: '[link *foo **bar** `#`*][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[link *foo **bar** \`#\`*][ref]

[ref]: /uri
`;
		const expected = `
<p><a href="/uri">link <em>foo <strong>bar</strong> <code>#</code></em></a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 540, line 8290: '[![moon](moon.jpg)][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[![moon](moon.jpg)][ref]

[ref]: /uri
`;
		const expected = `
<p><a href="/uri"><img src="moon.jpg" alt="moon" /></a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 541, line 8301: '[foo [bar](/uri)][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[foo [bar](/uri)][ref]

[ref]: /uri
`;
		const expected = `
<p>[foo <a href="/uri">bar</a>]<a href="/uri">ref</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 542, line 8310: '[foo *bar [baz][ref]*][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[foo *bar [baz][ref]*][ref]

[ref]: /uri
`;
		const expected = `
<p>[foo <em>bar <a href="/uri">baz</a></em>]<a href="/uri">ref</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 543, line 8325: '*[foo*][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
*[foo*][ref]

[ref]: /uri
`;
		const expected = `
<p>*<a href="/uri">foo*</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 544, line 8334: '[foo *bar][ref]\\n\\n[ref]: /uri'", () => {
		const input = `
[foo *bar][ref]

[ref]: /uri
`;
		const expected = `
<p><a href="/uri">foo *bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 545, line 8346: '[foo <bar attr=\"][ref]\">\\n\\n[ref]: /uri'", () => {
		const input = `
[foo <bar attr="][ref]">

[ref]: /uri
`;
		const expected = `
<p>[foo <bar attr="][ref]"></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 546, line 8355: '[foo`][ref]`\\n\\n[ref]: /uri'", () => {
		const input = `
[foo\`][ref]\`

[ref]: /uri
`;
		const expected = `
<p>[foo<code>][ref]</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 547, line 8364: '[foo<http://example.com/?search=][ref]>\\n\\n[ref]: /uri'", () => {
		const input = `
[foo<http://example.com/?search=][ref]>

[ref]: /uri
`;
		const expected = `
<p>[foo<a href="http://example.com/?search=%5D%5Bref%5D">http://example.com/?search=][ref]</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 548, line 8375: '[foo][BaR]\\n\\n[bar]: /url \"title\"'", () => {
		const input = `
[foo][BaR]

[bar]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 549, line 8386: '[Толпой][Толпой] is a Russian word.\\n\\n[ТОЛПОЙ]: /url'", () => {
		const input = `
[Толпой][Толпой] is a Russian word.

[ТОЛПОЙ]: /url
`;
		const expected = `
<p><a href="/url">Толпой</a> is a Russian word.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 550, line 8398: '[Foo\\n  bar]: /url\\n\\n[Baz][Foo bar]'", () => {
		const input = `
[Foo
  bar]: /url

[Baz][Foo bar]
`;
		const expected = `
<p><a href="/url">Baz</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 551, line 8411: '[foo] [bar]\\n\\n[bar]: /url \"title\"'", () => {
		const input = `
[foo] [bar]

[bar]: /url "title"
`;
		const expected = `
<p>[foo] <a href="/url" title="title">bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 552, line 8420: '[foo]\\n[bar]\\n\\n[bar]: /url \"title\"'", () => {
		const input = `
[foo]
[bar]

[bar]: /url "title"
`;
		const expected = `
<p>[foo]
<a href="/url" title="title">bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 553, line 8461: '[foo]: /url1\\n\\n[foo]: /url2\\n\\n[bar][foo]'", () => {
		const input = `
[foo]: /url1

[foo]: /url2

[bar][foo]
`;
		const expected = `
<p><a href="/url1">bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 554, line 8476: '[bar][foo\\!]\\n\\n[foo!]: /url'", () => {
		const input = `
[bar][foo\\!]

[foo!]: /url
`;
		const expected = `
<p>[bar][foo!]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 555, line 8488: '[foo][ref[]\\n\\n[ref[]: /uri'", () => {
		const input = `
[foo][ref[]

[ref[]: /uri
`;
		const expected = `
<p>[foo][ref[]</p>
<p>[ref[]: /uri</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 556, line 8498: '[foo][ref[bar]]\\n\\n[ref[bar]]: /uri'", () => {
		const input = `
[foo][ref[bar]]

[ref[bar]]: /uri
`;
		const expected = `
<p>[foo][ref[bar]]</p>
<p>[ref[bar]]: /uri</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 557, line 8508: '[[[foo]]]\\n\\n[[[foo]]]: /url'", () => {
		const input = `
[[[foo]]]

[[[foo]]]: /url
`;
		const expected = `
<p>[[[foo]]]</p>
<p>[[[foo]]]: /url</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 558, line 8518: '[foo][ref\\[]\\n\\n[ref\\[]: /uri'", () => {
		const input = `
[foo][ref\\[]

[ref\\[]: /uri
`;
		const expected = `
<p><a href="/uri">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 559, line 8529: '[bar\\\\]: /uri\\n\\n[bar\\\\]'", () => {
		const input = `
[bar\\\\]: /uri

[bar\\\\]
`;
		const expected = `
<p><a href="/uri">bar\\</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 560, line 8540: '[]\\n\\n[]: /uri'", () => {
		const input = `
[]

[]: /uri
`;
		const expected = `
<p>[]</p>
<p>[]: /uri</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 561, line 8550: '[\\n ]\\n\\n[\\n ]: /uri'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 562, line 8573: '[foo][]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[foo][]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 563, line 8582: '[*foo* bar][]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
[*foo* bar][]

[*foo* bar]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title"><em>foo</em> bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 564, line 8593: '[Foo][]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[Foo][]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">Foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 565, line 8606: '[foo] \\n[]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[foo] 
[]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a>
[]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 566, line 8626: '[foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[foo]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 567, line 8635: '[*foo* bar]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
[*foo* bar]

[*foo* bar]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title"><em>foo</em> bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 568, line 8644: '[[*foo* bar]]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
[[*foo* bar]]

[*foo* bar]: /url "title"
`;
		const expected = `
<p>[<a href="/url" title="title"><em>foo</em> bar</a>]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 569, line 8653: '[[bar [foo]\\n\\n[foo]: /url'", () => {
		const input = `
[[bar [foo]

[foo]: /url
`;
		const expected = `
<p>[[bar <a href="/url">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 570, line 8664: '[Foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
[Foo]

[foo]: /url "title"
`;
		const expected = `
<p><a href="/url" title="title">Foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 571, line 8675: '[foo] bar\\n\\n[foo]: /url'", () => {
		const input = `
[foo] bar

[foo]: /url
`;
		const expected = `
<p><a href="/url">foo</a> bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 572, line 8687: '\\[foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
\\[foo]

[foo]: /url "title"
`;
		const expected = `
<p>[foo]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 573, line 8699: '[foo*]: /url\\n\\n*[foo*]'", () => {
		const input = `
[foo*]: /url

*[foo*]
`;
		const expected = `
<p>*<a href="/url">foo*</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 574, line 8711: '[foo][bar]\\n\\n[foo]: /url1\\n[bar]: /url2'", () => {
		const input = `
[foo][bar]

[foo]: /url1
[bar]: /url2
`;
		const expected = `
<p><a href="/url2">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 575, line 8720: '[foo][]\\n\\n[foo]: /url1'", () => {
		const input = `
[foo][]

[foo]: /url1
`;
		const expected = `
<p><a href="/url1">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 576, line 8730: '[foo]()\\n\\n[foo]: /url1'", () => {
		const input = `
[foo]()

[foo]: /url1
`;
		const expected = `
<p><a href="">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 577, line 8738: '[foo](not a link)\\n\\n[foo]: /url1'", () => {
		const input = `
[foo](not a link)

[foo]: /url1
`;
		const expected = `
<p><a href="/url1">foo</a>(not a link)</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 578, line 8749: '[foo][bar][baz]\\n\\n[baz]: /url'", () => {
		const input = `
[foo][bar][baz]

[baz]: /url
`;
		const expected = `
<p>[foo]<a href="/url">bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 579, line 8761: '[foo][bar][baz]\\n\\n[baz]: /url1\\n[bar]: /url2'", () => {
		const input = `
[foo][bar][baz]

[baz]: /url1
[bar]: /url2
`;
		const expected = `
<p><a href="/url2">foo</a><a href="/url1">baz</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 580, line 8774: '[foo][bar][baz]\\n\\n[baz]: /url1\\n[foo]: /url2'", () => {
		const input = `
[foo][bar][baz]

[baz]: /url1
[foo]: /url2
`;
		const expected = `
<p>[foo]<a href="/url1">bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 581, line 8797: '![foo](/url \"title\")'", () => {
		const input = `
![foo](/url "title")
`;
		const expected = `
<p><img src="/url" alt="foo" title="title" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 582, line 8804: '![foo *bar*]\\n\\n[foo *bar*]: train.jpg \"train & tracks\"'", () => {
		const input = `
![foo *bar*]

[foo *bar*]: train.jpg "train & tracks"
`;
		const expected = `
<p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 583, line 8813: '![foo ![bar](/url)](/url2)'", () => {
		const input = `
![foo ![bar](/url)](/url2)
`;
		const expected = `
<p><img src="/url2" alt="foo bar" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 584, line 8820: '![foo [bar](/url)](/url2)'", () => {
		const input = `
![foo [bar](/url)](/url2)
`;
		const expected = `
<p><img src="/url2" alt="foo bar" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 585, line 8834: '![foo *bar*][]\\n\\n[foo *bar*]: train.jpg \"train & tracks\"'", () => {
		const input = `
![foo *bar*][]

[foo *bar*]: train.jpg "train & tracks"
`;
		const expected = `
<p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 586, line 8843: '![foo *bar*][foobar]\\n\\n[FOOBAR]: train.jpg \"train & tracks\"'", () => {
		const input = `
![foo *bar*][foobar]

[FOOBAR]: train.jpg "train & tracks"
`;
		const expected = `
<p><img src="train.jpg" alt="foo bar" title="train &amp; tracks" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 587, line 8852: '![foo](train.jpg)'", () => {
		const input = `
![foo](train.jpg)
`;
		const expected = `
<p><img src="train.jpg" alt="foo" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 588, line 8859: 'My ![foo bar](/path/to/train.jpg  \"title\"   )'", () => {
		const input = `
My ![foo bar](/path/to/train.jpg  "title"   )
`;
		const expected = `
<p>My <img src="/path/to/train.jpg" alt="foo bar" title="title" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 589, line 8866: '![foo](<url>)'", () => {
		const input = `
![foo](<url>)
`;
		const expected = `
<p><img src="url" alt="foo" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 590, line 8873: '![](/url)'", () => {
		const input = `
![](/url)
`;
		const expected = `
<p><img src="/url" alt="" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 591, line 8882: '![foo][bar]\\n\\n[bar]: /url'", () => {
		const input = `
![foo][bar]

[bar]: /url
`;
		const expected = `
<p><img src="/url" alt="foo" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 592, line 8891: '![foo][bar]\\n\\n[BAR]: /url'", () => {
		const input = `
![foo][bar]

[BAR]: /url
`;
		const expected = `
<p><img src="/url" alt="foo" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 593, line 8902: '![foo][]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![foo][]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo" title="title" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 594, line 8911: '![*foo* bar][]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
![*foo* bar][]

[*foo* bar]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo bar" title="title" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 595, line 8922: '![Foo][]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![Foo][]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="Foo" title="title" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 596, line 8934: '![foo] \\n[]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![foo] 
[]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo" title="title" />
[]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 597, line 8947: '![foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![foo]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo" title="title" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 598, line 8956: '![*foo* bar]\\n\\n[*foo* bar]: /url \"title\"'", () => {
		const input = `
![*foo* bar]

[*foo* bar]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="foo bar" title="title" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 599, line 8967: '![[foo]]\\n\\n[[foo]]: /url \"title\"'", () => {
		const input = `
![[foo]]

[[foo]]: /url "title"
`;
		const expected = `
<p>![[foo]]</p>
<p>[[foo]]: /url &quot;title&quot;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 600, line 8979: '![Foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
![Foo]

[foo]: /url "title"
`;
		const expected = `
<p><img src="/url" alt="Foo" title="title" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 601, line 8991: '!\\[foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
!\\[foo]

[foo]: /url "title"
`;
		const expected = `
<p>![foo]</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 602, line 9003: '\\![foo]\\n\\n[foo]: /url \"title\"'", () => {
		const input = `
\\![foo]

[foo]: /url "title"
`;
		const expected = `
<p>!<a href="/url" title="title">foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 603, line 9036: '<http://foo.bar.baz>'", () => {
		const input = `
<http://foo.bar.baz>
`;
		const expected = `
<p><a href="http://foo.bar.baz">http://foo.bar.baz</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 604, line 9043: '<http://foo.bar.baz/test?q=hello&id=22&boolean>'", () => {
		const input = `
<http://foo.bar.baz/test?q=hello&id=22&boolean>
`;
		const expected = `
<p><a href="http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean">http://foo.bar.baz/test?q=hello&amp;id=22&amp;boolean</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 605, line 9050: '<irc://foo.bar:2233/baz>'", () => {
		const input = `
<irc://foo.bar:2233/baz>
`;
		const expected = `
<p><a href="irc://foo.bar:2233/baz">irc://foo.bar:2233/baz</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 606, line 9059: '<MAILTO:FOO@BAR.BAZ>'", () => {
		const input = `
<MAILTO:FOO@BAR.BAZ>
`;
		const expected = `
<p><a href="MAILTO:FOO@BAR.BAZ">MAILTO:FOO@BAR.BAZ</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 607, line 9071: '<a+b+c:d>'", () => {
		const input = `
<a+b+c:d>
`;
		const expected = `
<p><a href="a+b+c:d">a+b+c:d</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 608, line 9078: '<made-up-scheme://foo,bar>'", () => {
		const input = `
<made-up-scheme://foo,bar>
`;
		const expected = `
<p><a href="made-up-scheme://foo,bar">made-up-scheme://foo,bar</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 609, line 9085: '<http://../>'", () => {
		const input = `
<http://../>
`;
		const expected = `
<p><a href="http://../">http://../</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 610, line 9092: '<localhost:5001/foo>'", () => {
		const input = `
<localhost:5001/foo>
`;
		const expected = `
<p><a href="localhost:5001/foo">localhost:5001/foo</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 611, line 9101: '<http://foo.bar/baz bim>'", () => {
		const input = `
<http://foo.bar/baz bim>
`;
		const expected = `
<p>&lt;http://foo.bar/baz bim&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 612, line 9110: '<http://example.com/\\[\\>'", () => {
		const input = `
<http://example.com/\\[\\>
`;
		const expected = `
<p><a href="http://example.com/%5C%5B%5C">http://example.com/\\[\\</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 613, line 9132: '<foo@bar.example.com>'", () => {
		const input = `
<foo@bar.example.com>
`;
		const expected = `
<p><a href="mailto:foo@bar.example.com">foo@bar.example.com</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 614, line 9139: '<foo+special@Bar.baz-bar0.com>'", () => {
		const input = `
<foo+special@Bar.baz-bar0.com>
`;
		const expected = `
<p><a href="mailto:foo+special@Bar.baz-bar0.com">foo+special@Bar.baz-bar0.com</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 615, line 9148: '<foo\\+@bar.example.com>'", () => {
		const input = `
<foo\\+@bar.example.com>
`;
		const expected = `
<p>&lt;foo+@bar.example.com&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 616, line 9157: '<>'", () => {
		const input = `
<>
`;
		const expected = `
<p>&lt;&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 617, line 9164: '< http://foo.bar >'", () => {
		const input = `
< http://foo.bar >
`;
		const expected = `
<p>&lt; http://foo.bar &gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 618, line 9171: '<m:abc>'", () => {
		const input = `
<m:abc>
`;
		const expected = `
<p>&lt;m:abc&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 619, line 9178: '<foo.bar.baz>'", () => {
		const input = `
<foo.bar.baz>
`;
		const expected = `
<p>&lt;foo.bar.baz&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 620, line 9185: 'http://example.com'", () => {
		const input = `
http://example.com
`;
		const expected = `
<p><a href="http://example.com">http://example.com</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 621, line 9192: 'foo@bar.example.com'", () => {
		const input = `
foo@bar.example.com
`;
		const expected = `
<p><a href="mailto:foo@bar.example.com">foo@bar.example.com</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 622, line 9221: 'www.commonmark.org'", () => {
		const input = `
www.commonmark.org
`;
		const expected = `
<p><a href="http://www.commonmark.org">www.commonmark.org</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 623, line 9229: 'Visit www.commonmark.org/help for more information.'", () => {
		const input = `
Visit www.commonmark.org/help for more information.
`;
		const expected = `
<p>Visit <a href="http://www.commonmark.org/help">www.commonmark.org/help</a> for more information.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 624, line 9241: 'Visit www.commonmark.org.\\n\\nVisit www.commonmark.org/a.b.'", () => {
		const input = `
Visit www.commonmark.org.

Visit www.commonmark.org/a.b.
`;
		const expected = `
<p>Visit <a href="http://www.commonmark.org">www.commonmark.org</a>.</p>
<p>Visit <a href="http://www.commonmark.org/a.b">www.commonmark.org/a.b</a>.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 625, line 9255: 'www.google.com/search?q=Markup+(business)\\n\\nwww.google.com/search?q=Markup+(business)))\\n\\n(www.google.com/search?q=Markup+(business))\\n\\n(www.google.com/search?q=Markup+(business)'", () => {
		const input = `
www.google.com/search?q=Markup+(business)

www.google.com/search?q=Markup+(business)))

(www.google.com/search?q=Markup+(business))

(www.google.com/search?q=Markup+(business)
`;
		const expected = `
<p><a href="http://www.google.com/search?q=Markup+(business)">www.google.com/search?q=Markup+(business)</a></p>
<p><a href="http://www.google.com/search?q=Markup+(business)">www.google.com/search?q=Markup+(business)</a>))</p>
<p>(<a href="http://www.google.com/search?q=Markup+(business)">www.google.com/search?q=Markup+(business)</a>)</p>
<p>(<a href="http://www.google.com/search?q=Markup+(business)">www.google.com/search?q=Markup+(business)</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 626, line 9274: 'www.google.com/search?q=(business))+ok'", () => {
		const input = `
www.google.com/search?q=(business))+ok
`;
		const expected = `
<p><a href="http://www.google.com/search?q=(business))+ok">www.google.com/search?q=(business))+ok</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 627, line 9285: 'www.google.com/search?q=commonmark&hl=en\\n\\nwww.google.com/search?q=commonmark&hl;'", () => {
		const input = `
www.google.com/search?q=commonmark&hl=en

www.google.com/search?q=commonmark&hl;
`;
		const expected = `
<p><a href="http://www.google.com/search?q=commonmark&amp;hl=en">www.google.com/search?q=commonmark&amp;hl=en</a></p>
<p><a href="http://www.google.com/search?q=commonmark">www.google.com/search?q=commonmark</a>&amp;hl;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 628, line 9296: 'www.commonmark.org/he<lp'", () => {
		const input = `
www.commonmark.org/he<lp
`;
		const expected = `
<p><a href="http://www.commonmark.org/he">www.commonmark.org/he</a>&lt;lp</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 629, line 9307: 'http://commonmark.org\\n\\n(Visit https://encrypted.google.com/search?q=Markup+(business))\\n\\nAnonymous FTP is available at ftp://foo.bar.baz.'", () => {
		const input = `
http://commonmark.org

(Visit https://encrypted.google.com/search?q=Markup+(business))

Anonymous FTP is available at ftp://foo.bar.baz.
`;
		const expected = `
<p><a href="http://commonmark.org">http://commonmark.org</a></p>
<p>(Visit <a href="https://encrypted.google.com/search?q=Markup+(business)">https://encrypted.google.com/search?q=Markup+(business)</a>)</p>
<p>Anonymous FTP is available at <a href="ftp://foo.bar.baz">ftp://foo.bar.baz</a>.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 630, line 9333: 'foo@bar.baz'", () => {
		const input = `
foo@bar.baz
`;
		const expected = `
<p><a href="mailto:foo@bar.baz">foo@bar.baz</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 631, line 9341: 'hello@mail+xyz.example isn't valid, but hello+xyz@mail.example is.'", () => {
		const input = `
hello@mail+xyz.example isn't valid, but hello+xyz@mail.example is.
`;
		const expected = `
<p>hello@mail+xyz.example isn't valid, but <a href="mailto:hello+xyz@mail.example">hello+xyz@mail.example</a> is.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 632, line 9351: 'a.b-c_d@a.b\\n\\na.b-c_d@a.b.\\n\\na.b-c_d@a.b-\\n\\na.b-c_d@a.b_'", () => {
		const input = `
a.b-c_d@a.b

a.b-c_d@a.b.

a.b-c_d@a.b-

a.b-c_d@a.b_
`;
		const expected = `
<p><a href="mailto:a.b-c_d@a.b">a.b-c_d@a.b</a></p>
<p><a href="mailto:a.b-c_d@a.b">a.b-c_d@a.b</a>.</p>
<p>a.b-c_d@a.b-</p>
<p>a.b-c_d@a.b_</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 633, line 9375: 'mailto:foo@bar.baz\\n\\nmailto:a.b-c_d@a.b\\n\\nmailto:a.b-c_d@a.b.\\n\\nmailto:a.b-c_d@a.b/\\n\\nmailto:a.b-c_d@a.b-\\n\\nmailto:a.b-c_d@a.b_\\n\\nxmpp:foo@bar.baz\\n\\nxmpp:foo@bar.baz.'", () => {
		const input = `
mailto:foo@bar.baz

mailto:a.b-c_d@a.b

mailto:a.b-c_d@a.b.

mailto:a.b-c_d@a.b/

mailto:a.b-c_d@a.b-

mailto:a.b-c_d@a.b_

xmpp:foo@bar.baz

xmpp:foo@bar.baz.
`;
		const expected = `
<p><a href="mailto:foo@bar.baz">mailto:foo@bar.baz</a></p>
<p><a href="mailto:a.b-c_d@a.b">mailto:a.b-c_d@a.b</a></p>
<p><a href="mailto:a.b-c_d@a.b">mailto:a.b-c_d@a.b</a>.</p>
<p><a href="mailto:a.b-c_d@a.b">mailto:a.b-c_d@a.b</a>/</p>
<p>mailto:a.b-c_d@a.b-</p>
<p>mailto:a.b-c_d@a.b_</p>
<p><a href="xmpp:foo@bar.baz">xmpp:foo@bar.baz</a></p>
<p><a href="xmpp:foo@bar.baz">xmpp:foo@bar.baz</a>.</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 634, line 9406: 'xmpp:foo@bar.baz/txt\\n\\nxmpp:foo@bar.baz/txt@bin\\n\\nxmpp:foo@bar.baz/txt@bin.com'", () => {
		const input = `
xmpp:foo@bar.baz/txt

xmpp:foo@bar.baz/txt@bin

xmpp:foo@bar.baz/txt@bin.com
`;
		const expected = `
<p><a href="xmpp:foo@bar.baz/txt">xmpp:foo@bar.baz/txt</a></p>
<p><a href="xmpp:foo@bar.baz/txt@bin">xmpp:foo@bar.baz/txt@bin</a></p>
<p><a href="xmpp:foo@bar.baz/txt@bin.com">xmpp:foo@bar.baz/txt@bin.com</a></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 635, line 9420: 'xmpp:foo@bar.baz/txt/bin'", () => {
		const input = `
xmpp:foo@bar.baz/txt/bin
`;
		const expected = `
<p><a href="xmpp:foo@bar.baz/txt">xmpp:foo@bar.baz/txt</a>/bin</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 636, line 9502: '<a><bab><c2c>'", () => {
		const input = `
<a><bab><c2c>
`;
		const expected = `
<p><a><bab><c2c></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 637, line 9511: '<a/><b2/>'", () => {
		const input = `
<a/><b2/>
`;
		const expected = `
<p><a/><b2/></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 638, line 9520: '<a  /><b2\\ndata=\"foo\" >'", () => {
		const input = `
<a  /><b2
data="foo" >
`;
		const expected = `
<p><a  /><b2
data="foo" ></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 639, line 9531: '<a foo=\"bar\" bam = 'baz <em>\"</em>'\\n_boolean zoop:33=zoop:33 />'", () => {
		const input = `
<a foo="bar" bam = 'baz <em>"</em>'
_boolean zoop:33=zoop:33 />
`;
		const expected = `
<p><a foo="bar" bam = 'baz <em>"</em>'
_boolean zoop:33=zoop:33 /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 640, line 9542: 'Foo <responsive-image src=\"foo.jpg\" />'", () => {
		const input = `
Foo <responsive-image src="foo.jpg" />
`;
		const expected = `
<p>Foo <responsive-image src="foo.jpg" /></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 641, line 9551: '<33> <__>'", () => {
		const input = `
<33> <__>
`;
		const expected = `
<p>&lt;33&gt; &lt;__&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 642, line 9560: '<a h*#ref=\"hi\">'", () => {
		const input = `
<a h*#ref="hi">
`;
		const expected = `
<p>&lt;a h*#ref=&quot;hi&quot;&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 643, line 9569: '<a href=\"hi'> <a href=hi'>'", () => {
		const input = `
<a href="hi'> <a href=hi'>
`;
		const expected = `
<p>&lt;a href=&quot;hi'&gt; &lt;a href=hi'&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 644, line 9578: '< a><\\nfoo><bar/ >\\n<foo bar=baz\\nbim!bop />'", () => {
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

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 645, line 9593: '<a href='bar'title=title>'", () => {
		const input = `
<a href='bar'title=title>
`;
		const expected = `
<p>&lt;a href='bar'title=title&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 646, line 9602: '</a></foo >'", () => {
		const input = `
</a></foo >
`;
		const expected = `
<p></a></foo ></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 647, line 9611: '</a href=\"foo\">'", () => {
		const input = `
</a href="foo">
`;
		const expected = `
<p>&lt;/a href=&quot;foo&quot;&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 648, line 9620: 'foo <!-- this is a --\\ncomment - with hyphens -->'", () => {
		const input = `
foo <!-- this is a --
comment - with hyphens -->
`;
		const expected = `
<p>foo <!-- this is a --
comment - with hyphens --></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 649, line 9628: 'foo <!-- this is a --\\ncomment - with hyphens -->'", () => {
		const input = `
foo <!-- this is a --
comment - with hyphens -->
`;
		const expected = `
<p>foo <!-- this is a --
comment - with hyphens --></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 650, line 9636: 'foo <!--> foo -->\\n\\nfoo <!---> foo -->'", () => {
		const input = `
foo <!--> foo -->

foo <!---> foo -->
`;
		const expected = `
<p>foo <!--> foo --&gt;</p>
<p>foo <!---> foo --&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 651, line 9648: 'foo <?php echo $a; ?>'", () => {
		const input = `
foo <?php echo $a; ?>
`;
		const expected = `
<p>foo <?php echo $a; ?></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 652, line 9657: 'foo <!ELEMENT br EMPTY>'", () => {
		const input = `
foo <!ELEMENT br EMPTY>
`;
		const expected = `
<p>foo <!ELEMENT br EMPTY></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 653, line 9666: 'foo <![CDATA[>&<]]>'", () => {
		const input = `
foo <![CDATA[>&<]]>
`;
		const expected = `
<p>foo <![CDATA[>&<]]></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 654, line 9676: 'foo <a href=\"&ouml;\">'", () => {
		const input = `
foo <a href="&ouml;">
`;
		const expected = `
<p>foo <a href="&ouml;"></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 655, line 9685: 'foo <a href=\"\\*\">'", () => {
		const input = `
foo <a href="\\*">
`;
		const expected = `
<p>foo <a href="\\*"></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test('Example 656, line 9692: \'<a href="\\"">\'', () => {
		const input = `
<a href="\\"">
`;
		const expected = `
<p>&lt;a href=&quot;&quot;&quot;&gt;</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test.skip("Example 657, line 9723: '<strong> <title> <style> <em>\\n\\n<blockquote>\\n  <xmp> is disallowed.  <XMP> is also disallowed.\\n</blockquote>'", () => {
		const input = `
<strong> <title> <style> <em>

<blockquote>
  <xmp> is disallowed.  <XMP> is also disallowed.
</blockquote>
`;
		const expected = `
<p><strong> &lt;title> &lt;style> <em></p>
<blockquote>
  &lt;xmp> is disallowed.  &lt;XMP> is also disallowed.
</blockquote>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 658, line 9745: 'foo  \\nbaz'", () => {
		const input = `
foo  
baz
`;
		const expected = `
<p>foo<br />
baz</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 659, line 9757: 'foo\\\\nbaz'", () => {
		const input = `
foo\\
baz
`;
		const expected = `
<p>foo<br />
baz</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 660, line 9768: 'foo       \\nbaz'", () => {
		const input = `
foo       
baz
`;
		const expected = `
<p>foo<br />
baz</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 661, line 9779: 'foo  \\n     bar'", () => {
		const input = `
foo  
     bar
`;
		const expected = `
<p>foo<br />
bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 662, line 9788: 'foo\\\\n     bar'", () => {
		const input = `
foo\\
     bar
`;
		const expected = `
<p>foo<br />
bar</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 663, line 9800: '*foo  \\nbar*'", () => {
		const input = `
*foo  
bar*
`;
		const expected = `
<p><em>foo<br />
bar</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 664, line 9809: '*foo\\\\nbar*'", () => {
		const input = `
*foo\\
bar*
`;
		const expected = `
<p><em>foo<br />
bar</em></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 665, line 9820: '`code  \\nspan`'", () => {
		const input = `
\`code  
span\`
`;
		const expected = `
<p><code>code   span</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 666, line 9828: '`code\\\\nspan`'", () => {
		const input = `
\`code\\
span\`
`;
		const expected = `
<p><code>code\\ span</code></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 667, line 9838: '<a href=\"foo  \\nbar\">'", () => {
		const input = `
<a href="foo  
bar">
`;
		const expected = `
<p><a href="foo  
bar"></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 668, line 9847: '<a href=\"foo\\\\nbar\">'", () => {
		const input = `
<a href="foo\\
bar">
`;
		const expected = `
<p><a href="foo\\
bar"></p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 669, line 9860: 'foo\\'", () => {
		const input = `
foo\\
`;
		const expected = `
<p>foo\\</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 670, line 9867: 'foo  '", () => {
		const input = `
foo  
`;
		const expected = `
<p>foo</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 671, line 9874: '### foo\\'", () => {
		const input = `
### foo\\
`;
		const expected = `
<h3>foo\\</h3>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 672, line 9881: '### foo  '", () => {
		const input = `
### foo  
`;
		const expected = `
<h3>foo</h3>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 673, line 9896: 'foo\\nbaz'", () => {
		const input = `
foo
baz
`;
		const expected = `
<p>foo
baz</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 674, line 9908: 'foo \\n baz'", () => {
		const input = `
foo 
 baz
`;
		const expected = `
<p>foo
baz</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 675, line 9928: 'hello $.;'there'", () => {
		const input = `
hello $.;'there
`;
		const expected = `
<p>hello $.;'there</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 676, line 9935: 'Foo χρῆν'", () => {
		const input = `
Foo χρῆν
`;
		const expected = `
<p>Foo χρῆν</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});

	test("Example 677, line 9944: 'Multiple     spaces'", () => {
		const input = `
Multiple     spaces
`;
		const expected = `
<p>Multiple     spaces</p>
`;

		const docSpaced = parse(input, gfm);
		const htmlSpaced = renderHtml(docSpaced, gfm.renderers);
		expect(htmlSpaced.trim()).toBe(expected.trim());

		const docTrimmed = parse(input.substring(1, input.length - 1), gfm);
		const htmlTrimmed = renderHtml(docTrimmed, gfm.renderers);
		expect(htmlTrimmed.trim()).toBe(expected.trim());
	});
});
