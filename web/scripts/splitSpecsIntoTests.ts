import { promises as fs } from "node:fs";
import path from "node:path";

await splitSpecsIntoTests("spec-cm.txt");
await splitSpecsIntoTests("spec-gfm.txt");

async function splitSpecsIntoTests(specFile: string) {
	const specPath = path.join(".", "test", specFile);
	const input = await fs.readFile(specPath, "utf-8");
	const lines = input.split("\n");

	let tests: {
		input: string;
		expected: string;
		header: string;
	}[] = [];
	for (let i = 0; i < lines.length; i++) {
		if (lines[i].startsWith("```````````````````````````````` example")) {
			let example: string[] = [];
			for (let j = i + 1; j < lines.length; j++) {
				if (lines[j].startsWith("````````````````````````````````")) {
					let parts = example.join("\n").replaceAll("→", "\t").split("\n.");
					let input = parts[0];
					let expected = parts[1] ?? "";
					if (expected.startsWith("\n")) {
						expected = expected.substring(1);
					}
					let header = `Example ${tests.length + 1}, line ${i + 1}: '${input.replaceAll("\n", "\\n").replaceAll("\t", "→")}'`;
					tests.push({ input, expected, header });
					i = j;
					break;
				} else {
					example.push(lines[j]);
				}
			}
		}
	}

	const testName = specFile.split(".")[0];

	const output = `
import { describe, expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";

describe("${testName}", () => {
${tests
	.map((t) => {
		return `
	test("${t.header.replaceAll("\\", "\\\\").replaceAll('"', '\\"')}", () => {
		const input = \`\n${t.input.replaceAll("\\", "\\\\").replaceAll("`", "\\`")}\n\`;
		const expected = \`\n${t.expected.replaceAll("\\", "\\\\").replaceAll("`", "\\`")}\n\`;
		const doc = parse(input.substring(1, input.length -1));
		const html = renderHtml(doc);
		expect(html.trim()).toBe(expected.trim());
	});
`.slice(1);
	})
	.join("\n")}
});
`.trimStart();
	const testPath = path.join(".", "test", testName + ".test.ts");
	await fs.writeFile(testPath, output);
}
