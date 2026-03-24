import { expect, test } from "vite-plus/test";

import parse from "../src/parse";
import render from "../src/render";
import core from "../src/rulesets/core";
import htmlRenderers from "../src/rulesets/htmlRenderers";

test("basic parse", () => {
	const input = `
# Test

Here is some text

* Tight item 1
* Tight item 2

- Loose item 1

- Loose item 2

## Subtest

Here is some more text
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
<h2>Subtest</h2>
<p>Here is some more text</p>
`.trimStart();
	const doc = parse(input, core);
	const html = render(doc, htmlRenderers);
	expect(html).toBe(expected);
});
