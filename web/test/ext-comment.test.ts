import { describe, expect, test } from "vite-plus/test";

import extended from "../src/rulesets/extended";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

describe("criticmark comment", () => {
	test("comment basic", () => {
		const input = `
This text was {>>commented<<} recently.
 
`;
		const expected = `
<p>This text was <span class="markdown-comment">commented</span> recently.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment single character", () => {
		const input = `
text {>>a<<} more
`;
		const expected = `
<p>text <span class="markdown-comment">a</span> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with spaces", () => {
		const input = `
text {>>with spaces<<} more
`;
		const expected = `
<p>text <span class="markdown-comment">with spaces</span> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment at start of paragraph", () => {
		const input = `
{>>commented<<} This is new.
`;
		const expected = `
<p><span class="markdown-comment">commented</span> This is new.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment at end of paragraph", () => {
		const input = `
This is {>>commented<<}
`;
		const expected = `
<p>This is <span class="markdown-comment">commented</span></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with punctuation", () => {
		const input = `
text {>>word!<<} more
`;
		const expected = `
<p>text <span class="markdown-comment">word!</span> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with special characters", () => {
		const input = `
text {>>a-b<<} more
`;
		const expected = `
<p>text <span class="markdown-comment">a-b</span> more</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment adjacent to text", () => {
		const input = `
test{>>ing<<}test
`;
		const expected = `
<p>test<span class="markdown-comment">ing</span>test</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("empty comment", () => {
		const input = `
text{>><<}text
`;
		const expected = `
<p>text{&gt;&gt;&lt;&lt;}text</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with markdown inside", () => {
		const input = `
text {>>**bold**<<}
`;
		const expected = `
<p>text <span class="markdown-comment"><strong>bold</strong></span></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with code inside", () => {
		const input = `
text {>>\`code\`<<}
`;
		const expected = `
<p>text <span class="markdown-comment"><code>code</code></span></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("escaped braces should not be comment", () => {
		const input = `
text \\{>>not comment<<\\}
`;
		const expected = `
<p>text {&gt;&gt;not comment&lt;&lt;}</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("unmatched opening comment", () => {
		const input = `
text {>>not closed
`;
		const expected = `
<p>text {&gt;&gt;not closed</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("unmatched closing comment", () => {
		const input = `
text not opened<<}
`;
		const expected = `
<p>text not opened&lt;&lt;}</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment in list item", () => {
		const input = `
- Item with {>>comment<<}
`;
		const expected = `
<ul>
<li>Item with <span class="markdown-comment">comment</span></li>
</ul>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment in blockquote", () => {
		const input = `
> Quote with {>>comment<<}
`;
		const expected = `
<blockquote>
<p>Quote with <span class="markdown-comment">comment</span></p>
</blockquote>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with angle brackets inside", () => {
		const input = `
text {>>some <text> inside<<}
`;
		const expected = `
<p>text <span class="markdown-comment">some <text> inside</span></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment at beginning of document", () => {
		const input = `
{>>Start<<} of document.
`;
		const expected = `
<p><span class="markdown-comment">Start</span> of document.</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment at end of document", () => {
		const input = `
End of {>>document<<}
`;
		const expected = `
<p>End of <span class="markdown-comment">document</span></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("multiple comments in one line", () => {
		const input = `
{>>first<<} and {>>second<<} and {>>third<<}
`;
		const expected = `
<p><span class="markdown-comment">first</span> and <span class="markdown-comment">second</span> and <span class="markdown-comment">third</span></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with starting emphasis", () => {
		const input = `
{>>comment *text<<} that shouldn't be bold*
`;
		const expected = `
<p><span class="markdown-comment">comment *text</span> that shouldn't be bold*</p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with ending emphasis", () => {
		const input = `
*this text should be {>>commented but not bold*<<}
`;
		const expected = `
<p>*this text should be <span class="markdown-comment">commented but not bold*</span></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with plus signs inside", () => {
		const input = `
text {>>plus + sign<<}
`;
		const expected = `
<p>text <span class="markdown-comment">plus + sign</span></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment with minus signs inside", () => {
		const input = `
text {>>minus - sign<<}
`;
		const expected = `
<p>text <span class="markdown-comment">minus - sign</span></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});

	test("comment nested with other critic marks", () => {
		const input = `
text {+insertion {>>comment<<} end+}
`;
		const expected = `
<p>text <ins class="markdown-insertion">insertion <span class="markdown-comment">comment</span> end</ins></p>
`.substring(1);

		const htmlSpaced = transform(input, extended, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), extended, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);
	});
});
