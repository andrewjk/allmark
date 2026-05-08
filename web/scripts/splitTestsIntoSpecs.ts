import fs from "node:fs";
import path from "node:path";
import { fileURLToPath } from "node:url";

import { renderHtmlSync } from "cmark-gfm";
const options = {
	footnotes: true,
	extensions: {
		strikethrough: true,
		table: true,
		tasklist: true,
		autolink: true,
	},
};

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

interface TestCase {
	description: string;
	input: string;
	output: string;
}

function extractTests(content: string): TestCase[] {
	const tests: TestCase[] = [];
	const testPattern = /test\s*\(\s*"([^"]+)"\s*,\s*\(\)\s*=>\s*{([^}]+(?:\{[^}]*\}[^}]*)*)}\s*\)/gs;
	let match;

	while ((match = testPattern.exec(content)) !== null) {
		const description = match[1];
		const testBody = match[2];

		const inputMatch = testBody.match(/const\s+input\s*=\s*`((?:[^`\\]|\\.)*)`/s);
		const expectedMatch = testBody.match(/const\s+expected\s*=\s*`((?:[^`\\]|\\.)*)`/s);

		const input = inputMatch
			? inputMatch[1]
					.replace(/\\n/g, "\n")
					.replace(/\\r/g, "\r")
					.replace(/\\t/g, "\t")
					.replace(/\\"/g, '"')
					.replace(/\\\\/g, "\\")
					.replace(/\\`/g, "`")
			: "";

		let output = expectedMatch
			? expectedMatch[1]
					.replace(/\\n/g, "\n")
					.replace(/\\r/g, "\r")
					.replace(/\\t/g, "\t")
					.replace(/\\"/g, '"')
					.replace(/\\\\/g, "\\")
					.replace(/\\`/g, "`")
			: renderHtmlSync(input, options);

		//if (testBody.includes(".substring(1)")) {
		//	output = output.substring(1);
		//}

		tests.push({ description, input, output });
	}

	return tests;
}

function generateSpec(tests: TestCase[]): string {
	return tests
		.map((test) => {
			const escapedDescription = test.description.replace(/`/g, "\\`");
			return `"${escapedDescription}"

${"`".repeat(32)} example
${test.input.substring(1, test.input.length - 1)}
.
${test.output.substring(1, test.output.length - 1)}
${"`".repeat(32)}`;
		})
		.join("\n\n");
}

function main() {
	const testDir = path.join(__dirname, "../test");
	const specDir = path.join(__dirname, "../specs");

	if (!fs.existsSync(specDir)) {
		fs.mkdirSync(specDir, { recursive: true });
	}

	const files = fs.readdirSync(testDir);
	const prefixes = ["core-", "gfm-", "ext-"];

	for (const file of files) {
		const prefix = prefixes.find((p) => file.startsWith(p));
		if (!prefix || !file.endsWith(".test.ts")) {
			continue;
		}

		const filePath = path.join(testDir, file);
		const content = fs.readFileSync(filePath, "utf-8");
		const tests = extractTests(content);

		if (tests.length === 0) {
			continue;
		}

		const specName = file.replace(".test.ts", ".txt");
		const specPath = path.join(specDir, specName);
		const specContent = generateSpec(tests);

		fs.writeFileSync(specPath, specContent, "utf-8");
		console.log(`Generated ${specPath} with ${tests.length} tests`);
	}
}

main();
