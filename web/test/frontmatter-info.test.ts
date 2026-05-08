import { expect, test } from "vite-plus/test";

import parse from "../src/parse";
import core from "../src/rulesets/core";

test("frontmatter is stored in document.info", () => {
	const input = `
---
title: Test
date: 2024-01-01
---

# Heading

Content
`;

	const doc = parse(input, core);
	expect(doc.info).toBe("---\ntitle: Test\ndate: 2024-01-01\n---");
});

test("frontmatter not recognized when not at document start", () => {
	const input = `
# Heading

---
title: Test
---

Content
`;

	const doc = parse(input, core);
	expect(doc.info).toBeUndefined();
});

test("frontmatter with single line", () => {
	const input = `
---
title: Test
---

# Heading
`;

	const doc = parse(input, core);
	expect(doc.info).toBe("---\ntitle: Test\n---");
});
