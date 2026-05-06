import { describe, expect, test } from "vite-plus/test";

import extended from "../src/rulesets/extended";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

describe("deletion", () => {
	test("deletion single", () => {
		const input = `
This text was {-deleted-} recently.
`;
		const expected = `
<p>This text was <del class="markdown-deletion">deleted</del> recently.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion double", () => {
		const input = `
This text was {--deleted--} recently.
`;
		const expected = `
<p>This text was <del class="markdown-deletion">deleted</del> recently.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion triple", () => {
		const input = `
This text was {---deleted---} recently.
`;
		const expected = `
<p>This text was {---deleted---} recently.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion single character", () => {
		const input = `
text {-a-} more
`;
		const expected = `
<p>text <del class="markdown-deletion">a</del> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion with spaces", () => {
		const input = `
text {-with spaces-} more
`;
		const expected = `
<p>text <del class="markdown-deletion">with spaces</del> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion at start of paragraph", () => {
		const input = `
{-deleted-} This is new.
`;
		const expected = `
<p><del class="markdown-deletion">deleted</del> This is new.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion at end of paragraph", () => {
		const input = `
This is {-deleted-}
`;
		const expected = `
<p>This is <del class="markdown-deletion">deleted</del></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion with punctuation", () => {
		const input = `
text {-word!-} more
`;
		const expected = `
<p>text <del class="markdown-deletion">word!</del> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion with special characters", () => {
		const input = `
text {-a-b-} more
`;
		const expected = `
<p>text <del class="markdown-deletion">a-b</del> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion adjacent to text", () => {
		const input = `
test{-ing-}test
`;
		const expected = `
<p>test<del class="markdown-deletion">ing</del>test</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("empty deletion", () => {
		const input = `
text{--}text
`;
		const expected = `
<p>text{--}text</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion with markdown inside", () => {
		const input = `
text {-**bold**-}
`;
		const expected = `
<p>text <del class="markdown-deletion"><strong>bold</strong></del></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion with code inside", () => {
		const input = `
text {-\`code\`-}
`;
		const expected = `
<p>text <del class="markdown-deletion"><code>code</code></del></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("escaped braces should not be deletion", () => {
		const input = `
text \\{-not deletion\\-}
`;
		const expected = `
<p>text {-not deletion-}</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("unmatched opening deletion", () => {
		const input = `
text {-not closed
`;
		const expected = `
<p>text {-not closed</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("unmatched closing deletion", () => {
		const input = `
text not opened-}
`;
		const expected = `
<p>text not opened-}</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion in list item", () => {
		const input = `
- Item with {-deletion-}
`;
		const expected = `
<ul>
<li>Item with <del class="markdown-deletion">deletion</del></li>
</ul>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion in blockquote", () => {
		const input = `
> Quote with {-deletion-}
`;
		const expected = `
<blockquote>
<p>Quote with <del class="markdown-deletion">deletion</del></p>
</blockquote>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion with plus inside", () => {
		const input = `
text {-plus - inside-}
`;
		const expected = `
<p>text <del class="markdown-deletion">plus - inside</del></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion at beginning of document", () => {
		const input = `
{-Start-} of document.
`;
		const expected = `
<p><del class="markdown-deletion">Start</del> of document.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion at end of document", () => {
		const input = `
End of {-document-}
`;
		const expected = `
<p>End of <del class="markdown-deletion">document</del></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("multiple deletions in one line", () => {
		const input = `
{-first-} and {-second-} and {-third-}
`;
		const expected = `
<p><del class="markdown-deletion">first</del> and <del class="markdown-deletion">second</del> and <del class="markdown-deletion">third</del></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion with starting emphasis", () => {
		const input = `
{-deleted *text-} that shouldn't be bold*
`;
		const expected = `
<p><del class="markdown-deletion">deleted *text</del> that shouldn't be bold*</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("deletion with ending emphasis", () => {
		const input = `
*this text should be {-deleted but not bold*-}
`;
		const expected = `
<p>*this text should be <del class="markdown-deletion">deleted but not bold*</del></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});
});
