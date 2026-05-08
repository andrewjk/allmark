import { promises as fs } from "node:fs";
import path from "node:path";

processSpecsFolder();
await splitSpecsIntoTests("spec-cm.txt", "core");
await splitSpecsIntoTests("spec-gfm.txt", "gfm");

async function processSpecsFolder() {
	const specsDir = path.join(".", "specs");
	const files = await fs.readdir(specsDir);

	for (const file of files) {
		if (!file.endsWith(".txt")) {
			continue;
		}

		const specPath = path.join(specsDir, file);
		const content = await fs.readFile(specPath, "utf-8");

		if (file.startsWith("core-")) {
			const testName = file.replace(".txt", "");
			await generateTestFile(testName, content, "core", true, getDescribeName("core", file));
			await generateCSharpTestFile(testName, content, "Core", "Core");
			await generateSwiftTestFile(testName, content, "core");
		} else if (file.startsWith("gfm-")) {
			const testName = file.replace(".txt", "");
			await generateTestFile(
				testName,
				content,
				"gfm",
				// TODO: cmark-gfm doesn't produce alerts?
				!file.includes("alert"),
				getDescribeName("gfm", file),
			);
			await generateCSharpTestFile(testName, content, "Gfm", "Gfm");
			await generateSwiftTestFile(testName, content, "gfm");
		} else if (file.startsWith("ext-")) {
			const testName = file.replace(".txt", "");
			await generateTestFile(testName, content, "extended", false, getDescribeName("ext", file));
			await generateCSharpTestFile(testName, content, "Ext", "Extended");
			await generateSwiftTestFile(testName, content, "extended");
		}
	}
}

function getDescribeName(prefix: string, fileName: string): string {
	const name = fileName.replace(`${prefix}-`, "").replace(".txt", "");
	const baseName = name.replace(/-/g, " ");

	if (prefix === "core") {
		if (baseName === "fenced code") return "fenced code";
		if (baseName === "indented code") return "indented code";
		if (baseName === "links") return "links";
		if (baseName === "list bulleted") return "bulleted lists";
		if (baseName === "list ordered") return "ordered lists";
		return `${baseName}s`;
	}

	//if (prefix === "ext") {
	//	if (baseName === "comment") return "critic comments";
	//	if (baseName === "deletion") return "critic deletions";
	//	if (baseName === "insertion") return "critic insertions";
	//	if (baseName === "highlight") return "highlight";
	//	return `${baseName}s`;
	//}

	return baseName;
}

interface TestExample {
	input: string;
	expected: string;
	description: string;
	skip: boolean;
}

function parseSpecFile(content: string, withDescriptions: boolean): TestExample[] {
	const lines = content.split("\n");
	const examples: TestExample[] = [];
	let currentDescription = "";

	for (let i = 0; i < lines.length; i++) {
		const line = lines[i];

		if (withDescriptions && line.startsWith('"') && line.endsWith('"')) {
			currentDescription = line.slice(1, -1);
			continue;
		}

		if (line.includes("```````````````````````````````` example")) {
			const skip = line.includes("(skip)");
			let exampleLines: string[] = [];
			for (let j = i + 1; j < lines.length; j++) {
				if (lines[j].startsWith("````````````````````````````````")) {
					const exampleText = exampleLines.join("\n");
					const parts = exampleText.replaceAll("→", "\t").split("\n.");
					let input = parts[0];
					let expected = parts[1] ?? "";
					if (expected.startsWith("\n")) {
						expected = expected.substring(1);
					}

					const description = withDescriptions
						? currentDescription
						: `Example ${examples.length + 1}, line ${i + 1}: '${input.replaceAll("\n", "\\n").replaceAll("\t", "→")}'`;

					examples.push({ input, expected, description, skip });
					currentDescription = "";
					i = j;
					break;
				} else {
					exampleLines.push(lines[j]);
				}
			}
		}
	}

	return examples;
}

async function generateTestFile(
	testName: string,
	content: string,
	ruleSetName: string,
	withRenderHtmlSync: boolean,
	describeName: string,
) {
	const examples = parseSpecFile(content, true);
	const output = buildOutput(testName, examples, ruleSetName, withRenderHtmlSync, describeName);
	const testPath = path.join(".", "test", testName + ".test.ts");
	await fs.writeFile(testPath, output);
	console.log(`Generated ${testPath} with ${examples.length} tests`);
}

function buildOutput(
	testName: string,
	examples: TestExample[],
	ruleSetName: string,
	withRenderHtmlSync: boolean,
	describeName: string,
) {
	const imports = withRenderHtmlSync ? `import { renderHtmlSync } from "cmark-gfm";\n` : "";

	const options =
		testName === "core-html-block"
			? "{ unsafe: true }"
			: withRenderHtmlSync && testName.startsWith("gfm-")
				? `{
	footnotes: true,
	extensions: {
		strikethrough: true,
		table: true,
		tasklist: true,
		autolink: true,
	},
}`
				: "";

	return `${imports}import { describe, expect, test } from "vite-plus/test";

import ${ruleSetName} from "../src/rulesets/${ruleSetName}";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";
${options ? `\nconst options = ${options};\n` : ""}
describe("${describeName}", () => {
${examples
	.map((example) => {
		const escapedDescription = example.description.replaceAll("\\", "\\\\");
		const escapedInput = example.input.replaceAll("\\", "\\\\").replaceAll("`", "\\`");
		const escapedExpected = example.expected
			? "\n" + example.expected.replaceAll("\\", "\\\\").replaceAll("`", "\\`") + "\n"
			: "\n";

		return `	${example.skip ? "// TODO:\n\ttest.skip" : "test"}("${escapedDescription}", () => {
		const input = \`
${escapedInput}
\`;
		const expected = \`${escapedExpected}\`.substring(1);${
			withRenderHtmlSync
				? `
		expect(expected).toBe(renderHtmlSync(input${options ? ", options" : ""}));`
				: ""
		}

		const htmlSpaced = transform(input, ${ruleSetName}, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), ${ruleSetName}, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\\n", "\\r\\n"), ${ruleSetName}, htmlRenderers);
		expect(htmlCrLf.replaceAll("\\r\\n", "\\n")).toBe(expected);
	});`;
	})
	.join("\n\n")}
});
`.trimStart();
}

async function splitSpecsIntoTests(specFile: string, ruleSetName: string) {
	const specPath = path.join(".", "specs", specFile);
	const input = await fs.readFile(specPath, "utf-8");
	const examples = parseSpecFile(input, false);

	const testName = specFile.split(".")[0];
	const output = buildOutputForSpecTests(testName, ruleSetName, examples);
	const testPath = path.join(".", "test", testName + ".test.ts");
	await fs.writeFile(testPath, output);
	console.log(`Generated ${testPath} with ${examples.length} tests`);

	const csharpClassName = toPascalCase(testName) + "Tests";
	const csharpRuleSetName = toPascalCase(ruleSetName);
	const csharpOutput = buildCSharpOutput(csharpClassName, examples, csharpRuleSetName);
	const csharpPath = path.join("..", "dotnet", "Allmark.Tests", csharpClassName + ".cs");
	await fs.writeFile(csharpPath, csharpOutput);
	console.log(`Generated ${csharpPath} with ${examples.length} tests`);

	const swiftStructName = toPascalCase(testName) + "Tests";
	const swiftOutput = buildSwiftOutput(swiftStructName, examples, ruleSetName);
	const swiftPath = path.join("..", "swift", "Tests", "AllmarkTests", swiftStructName + ".swift");
	await fs.writeFile(swiftPath, swiftOutput);
	console.log(`Generated ${swiftPath} with ${examples.length} tests`);
}

function buildOutputForSpecTests(testName: string, ruleSetName: string, examples: TestExample[]) {
	return `
import { describe, expect, test } from "vite-plus/test";

import ${ruleSetName} from "../src/rulesets/${ruleSetName}";
import htmlRenderers from "../src/rulesets/htmlRenderers";
import transform from "../src/transform";

describe("${testName}", () => {
${examples
	.map((example) => {
		let singleQuoteCount = 0;
		let doubleQuoteCount = 0;
		for (let char of example.description) {
			if (char === "'") singleQuoteCount++;
			else if (char === '"') doubleQuoteCount++;
		}
		let escapedDescription = example.description.replaceAll("\\", "\\\\");
		escapedDescription =
			doubleQuoteCount > singleQuoteCount
				? "'" + escapedDescription.replaceAll("'", "\\'") + "'"
				: '"' + escapedDescription.replaceAll('"', '\\"') + '"';
		const escapedInput = example.input.replaceAll("\\", "\\\\").replaceAll("`", "\\`");
		const escapedExpected = example.expected
			? "\n" + example.expected.replaceAll("\\", "\\\\").replaceAll("`", "\\`") + "\n"
			: "\n";

		return `	${example.skip ? "// TODO:\n\ttest.skip" : "test"}(${escapedDescription}, () => {
		const input = \`
${escapedInput}
\`;
		const expected = \`${escapedExpected}\`.substring(1);

		const htmlSpaced = transform(input, ${ruleSetName}, htmlRenderers);
		expect(htmlSpaced).toBe(expected);

		const htmlTrimmed = transform(input.substring(1, input.length - 1), ${ruleSetName}, htmlRenderers);
		expect(htmlTrimmed).toBe(expected);

		const htmlCrLf = transform(input.replaceAll("\\n", "\\r\\n"), ${ruleSetName}, htmlRenderers);
		expect(htmlCrLf.replaceAll("\\r\\n", "\\n")).toBe(expected);
	});`;
	})
	.join("\n\n")}
});
`.trimStart();
}

async function generateCSharpTestFile(
	testName: string,
	content: string,
	_classPrefix: string,
	ruleSetName: string,
) {
	const examples = parseSpecFile(content, true);
	const className = toPascalCase(testName) + "Tests";
	const output = buildCSharpOutput(className, examples, ruleSetName);
	const testPath = path.join("..", "dotnet", "Allmark.Tests", className + ".cs");
	await fs.writeFile(testPath, output);
	console.log(`Generated ${testPath} with ${examples.length} tests`);
}

function buildCSharpOutput(className: string, examples: TestExample[], ruleSetName: string) {
	return `using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark.Rulesets;

namespace Allmark.Tests;

[TestClass]
public class ${className}
{
${examples
	.map((example) => {
		const methodName = toPascalCase(example.description);
		const escapedInput = example.input.replaceAll('"', '""');
		const escapedExpected = example.expected ? example.expected.replaceAll('"', '""') : "";
		const expectedString = escapedExpected ? `\n${escapedExpected}\n` : "\n";

		return `    ${example.skip ? "// TODO:\n    [Ignore]\n    " : ""}[TestMethod]
    public void ${methodName}()
    {
        var input = @"
${escapedInput}
";
        var expected = @"${expectedString}".Substring(1);

        var htmlSpaced = Transformer.Execute(input, ${ruleSetName}.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlSpaced);

        var htmlTrimmed = Transformer.Execute(input[1..^1], ${ruleSetName}.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlTrimmed);

        var htmlCrLf = Transformer.Execute(input.Replace("\\n", "\\r\\n"), ${ruleSetName}.RuleSet, HtmlRenderers.Renderers);
        Assert.AreEqual(expected, htmlCrLf.Replace("\\r\\n", "\\n"));
    }`;
	})
	.join("\n\n")}
}`;
}

function toPascalCase(str: string): string {
	let result = str
		.replace(/[^a-zA-Z0-9\s-]/g, "")
		.replace(/(^|[-\s])(\w)/g, (_, __, char) => char.toUpperCase())
		.replace(/[-\s]/g, "");

	if (/^\d/.test(result)) {
		result = "_" + result;
	}

	return result;
}

async function generateSwiftTestFile(testName: string, content: string, ruleSetName: string) {
	const examples = parseSpecFile(content, true);
	const structName = toPascalCase(testName) + "Tests";
	const output = buildSwiftOutput(structName, examples, ruleSetName);
	const testPath = path.join("..", "swift", "Tests", "AllmarkTests", structName + ".swift");
	await fs.writeFile(testPath, output);
	console.log(`Generated ${testPath} with ${examples.length} tests`);
}

function buildSwiftOutput(structName: string, examples: TestExample[], ruleSetName: string) {
	return `@testable import Allmark
import Testing

struct ${structName} {
${examples
	.map((example) => {
		const methodName = toCamelCase(example.description);
		const escapedInput = example.input
			.replaceAll("\\", "\\\\")
			//.replaceAll('\\\\"', '\\"')
			.split("\n")
			.map((line) => (line ? `		${line}` : ""))
			.join("\n");
		const escapedExpected = example.expected
			? example.expected
					.replaceAll("\\", "\\\\")
					//.replaceAll('\\\\"', '\\"')
					.split("\n")
					.map((line) => (line ? `		${line}` : ""))
					.join("\n") + "\n"
			: "";

		return `	${example.skip ? "// TODO:\n\t/* @Test */" : "@Test"} func ${methodName}() async {
		let input = """

${escapedInput}

		"""

		let expected = """
${escapedExpected}
		"""

		await MainActor.run {
			let htmlSpaced = _transform(src: input, rules: ${ruleSetName}RuleSet, renderers: htmlRenderers)
			#expect(htmlSpaced == expected)

			let inputTrimmed = String(input[input.index(after: input.startIndex) ..< input.index(before: input.endIndex)])
			let htmlTrimmed = _transform(src: inputTrimmed, rules: ${ruleSetName}RuleSet, renderers: htmlRenderers)
			#expect(htmlTrimmed == expected)

			let inputCrLf = input.replacingOccurrences(of: "\\n", with: "\\r\\n")
			let htmlCrLf = _transform(src: inputCrLf, rules: ${ruleSetName}RuleSet, renderers: htmlRenderers)
			#expect(htmlCrLf.replacingOccurrences(of: "\\r\\n", with: "\\n") == expected)
		}
	}`;
	})
	.join("\n\n")}
}`;
}

function toCamelCase(str: string): string {
	const pascal = toPascalCase(str);
	return pascal.charAt(0).toLowerCase() + pascal.slice(1);
}
