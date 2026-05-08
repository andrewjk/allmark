import { describe, expect, test } from "vite-plus/test";

import core from "../src/rulesets/core";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

describe("frontmatters", () => {
	test("Frontmatter with YAML delimiters", () => {
		const input = `
---
title: Test
date: 2024-01-01
---

# Heading

Content
`;
		const expected = `
<h1>Heading</h1>
<p>Content</p>
`.substring(1);

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("Frontmatter at document start only", () => {
		const input = `
# Heading

---
title: Test
---

Content
`;
		const expected = `
<h1>Heading</h1>
<hr />
<h2>title: Test</h2>
<p>Content</p>
`.substring(1);

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("Frontmatter with single line content", () => {
		const input = `
---
title: Test
---

# Heading
`;
		const expected = `
<h1>Heading</h1>
`.substring(1);

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("Frontmatter with multiple lines", () => {
		const input = `
---
title: Test
date: 2024-01-01
author: John Doe
tags:
  - one
  - two
---

# Heading
`;
		const expected = `
<h1>Heading</h1>
`.substring(1);

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("Frontmatter with closing delimiter on separate line", () => {
		const input = `
---
title: Test

---

# Heading
`;
		const expected = `
<h1>Heading</h1>
`.substring(1);

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});

	test("Frontmatter with content following closing delimiter", () => {
		const input = `
---
title: Test
---
Content here
`;
		const expected = `
<p>Content here</p>
`.substring(1);

		const htmlSpaced = transform(input, core, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), core, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\n", "\r\n"), core, htmlRenderers);
		expect(htmlCrLf.replaceAll("\r\n", "\n")).toBe(expected);
	});
});
