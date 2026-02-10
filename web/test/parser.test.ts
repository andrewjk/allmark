import { expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import core from "../src/rules/core";

test("basic parse", () => {
	const input = `
# Test

Here is some text

* Tight item 1
* Tight item 2

- Loose item 1

- Loose item 2
`;
	const expected = `
<h1>Test</h1>
<p>Here is some text</p>
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
`.trimStart();
	const root = parse(input, core, false);
	//console.log(JSON.stringify(root, null, 2));
	const html = renderHtml(root, core.renderers);
	expect(html).toBe(expected);
});
