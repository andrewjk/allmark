import { readFileSync } from "node:fs";
import { dirname, join } from "node:path";
import { fileURLToPath } from "node:url";

import { describe, expect, test } from "vitest";

import consoleRenderers from "../src/rulesets/consoleRenderers";
import core from "../src/rulesets/core";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import { createStream, streamChunk } from "../src/stream";
import transform from "../src/transform";

const __dirname = dirname(fileURLToPath(import.meta.url));
const fullMarkdown = readFileSync(join(__dirname, "full-markdown.md"), "utf-8");

describe("stream", () => {
	// HACK: these take a long time, so only uncomment them when you are working on streaming
	test("streaming all length chunks produces same result as full parse (HTML)", () => {
		const expected = transform(fullMarkdown, core, htmlRenderers);

		for (let CHUNK_SIZE = 1; CHUNK_SIZE < 100; CHUNK_SIZE += 3) {
			const state = createStream(core, htmlRenderers);

			let last = "";
			for (let i = 0; i < fullMarkdown.length; i += CHUNK_SIZE) {
				const chunk = fullMarkdown.slice(i, i + CHUNK_SIZE);
				last = streamChunk(state, chunk);
			}

			const actual = last.replace(/<!--STREAM-->/g, "");
			expect(actual).toBe(expected);
		}
	});

	test("streaming all length chunks produces same result as full parse (console)", () => {
		const expected = transform(fullMarkdown, core, consoleRenderers);

		for (let CHUNK_SIZE = 1; CHUNK_SIZE < 100; CHUNK_SIZE += 3) {
			const state = createStream(core, consoleRenderers);

			let last = "";
			for (let i = 0; i < fullMarkdown.length; i += CHUNK_SIZE) {
				const chunk = fullMarkdown.slice(i, i + CHUNK_SIZE);
				last = streamChunk(state, chunk);
			}

			const actual = last.replaceAll("\x1B]8226;STREAM\x07", "");
			expect(actual).toBe(expected);
		}
	});

	test("streaming 10-char chunks produces same result as full parse (HTML)", () => {
		const expected = transform(fullMarkdown, core, htmlRenderers);

		const state = createStream(core, htmlRenderers);
		const CHUNK_SIZE = 10;

		let last = "";
		for (let i = 0; i < fullMarkdown.length; i += CHUNK_SIZE) {
			const chunk = fullMarkdown.slice(i, i + CHUNK_SIZE);
			last = streamChunk(state, chunk);
		}

		const actual = last.replace(/<!--STREAM-->/g, "");
		expect(actual).toBe(expected);
	});

	test("streaming 10-char chunks produces same result as full parse (console)", () => {
		const expected = transform(fullMarkdown, core, consoleRenderers);

		const state = createStream(core, consoleRenderers);
		const CHUNK_SIZE = 10;

		let last = "";
		for (let i = 0; i < fullMarkdown.length; i += CHUNK_SIZE) {
			const chunk = fullMarkdown.slice(i, i + CHUNK_SIZE);
			last = streamChunk(state, chunk);
		}

		const actual = last.replaceAll("\x1B]8226;STREAM\x07", "");
		expect(actual).toBe(expected);
	});
});
