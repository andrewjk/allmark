import { assert, expect, test } from "vite-plus/test";

import parse from "../src/parse";
import render from "../src/render";
import core from "../src/rulesets/core";
import htmlRenderers from "../src/rulesets/htmlRenderers";

test("basic parse", () => {
	const input = `
# Test ☺️

Here is some text
 *with* bold stuff

* Tight item 1
* Tight item 2

- Loose item 1

- Loose item 2

## Subtest

Here is some more text
`;
	const expected = `
<h1>Test ☺️</h1>
<p>Here is some text
<em>with</em> bold stuff</p>
<ul>
<li>Tight item 1</li>
<li>Tight item 2</li>
</ul>
<ul>
<li>
<p>Loose item 1</p>
</li>
<li>
<p>Loose item 2</p>
</li>
</ul>
<h2>Subtest</h2>
<p>Here is some more text</p>
`.trimStart();

	const doc = parse(input, core);
	const html = render(doc, htmlRenderers);
	expect(html).toBe(expected);

	assert(doc.children);
	expect(doc.children[0].type).toBe("heading");
	expect(doc.children[0].index).toBe(1);
	expect(doc.children[0].length).toBe(9);

	const start = doc.children[0].index;
	const end = start + doc.children[0].length;
	expect(input.substring(start, end)).toBe("# Test ☺️");

	// Let's try it with raw \r
	let input2 = input.replaceAll("\r\n", "\r").replaceAll("\n", "\r");
	const doc2 = parse(input2, core);
	const html2 = render(doc2, htmlRenderers);
	expect(html2.replaceAll("\r\n", "\n").replaceAll("\r", "\n")).toBe(expected);

	assert(doc2.children);
	expect(doc2.children[0].type).toBe("heading");
	expect(doc2.children[0].index).toBe(1);
	expect(doc2.children[0].length).toBe(9);

	const start2 = doc2.children[0].index;
	const end2 = start2 + doc2.children[0].length;
	expect(input2.substring(start2, end2)).toBe("# Test ☺️");
});
