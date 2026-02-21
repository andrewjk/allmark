import * as fs from "node:fs";
import { beforeEach, describe, expect, test, vi } from "vitest";
import type { Mock } from "vitest";
import { getRuleset, parseArgs } from "../src/bin/index";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";

vi.mock("node:fs", () => ({
	readFileSync: vi.fn(),
	writeFileSync: vi.fn(),
}));

function resetMocks(): void {
	vi.clearAllMocks();
}

describe("CLI parseArgs", () => {
	test("parses basic input file", () => {
		resetMocks();
		const args = ["input.md"];
		const result = parseArgs(args);
		expect(result).toEqual({
			input: "input.md",
			output: null,
			ruleset: "extended",
			format: "html",
		});
	});

	test("parses input file with output", () => {
		resetMocks();
		const args = ["input.md", "--output", "output.html"];
		const result = parseArgs(args);
		expect(result).toEqual({
			input: "input.md",
			output: "output.html",
			ruleset: "extended",
			format: "html",
		});
	});

	test("parses input file with ruleset core", () => {
		resetMocks();
		const args = ["input.md", "--ruleset", "core"];
		const result = parseArgs(args);
		expect(result).toEqual({ input: "input.md", output: null, ruleset: "core", format: "html" });
	});

	test("parses input file with ruleset gfm", () => {
		resetMocks();
		const args = ["input.md", "--ruleset", "gfm"];
		const result = parseArgs(args);
		expect(result).toEqual({ input: "input.md", output: null, ruleset: "gfm", format: "html" });
	});

	test("parses input file with ruleset extended", () => {
		resetMocks();
		const args = ["input.md", "--ruleset", "extended"];
		const result = parseArgs(args);
		expect(result).toEqual({
			input: "input.md",
			output: null,
			ruleset: "extended",
			format: "html",
		});
	});

	test("parses all options", () => {
		resetMocks();
		const args = ["input.md", "--output", "output.html", "--ruleset", "gfm"];
		const result = parseArgs(args);
		expect(result).toEqual({
			input: "input.md",
			output: "output.html",
			ruleset: "gfm",
			format: "html",
		});
	});

	test("parses options in different order", () => {
		resetMocks();
		const args = ["--ruleset", "core", "input.md", "--output", "out.html"];
		const result = parseArgs(args);
		expect(result).toEqual({
			input: "input.md",
			output: "out.html",
			ruleset: "core",
			format: "html",
		});
	});

	test("parses -o shortcut for output", () => {
		resetMocks();
		const args = ["input.md", "-o", "output.html"];
		const result = parseArgs(args);
		expect(result).toEqual({
			input: "input.md",
			output: "output.html",
			ruleset: "extended",
			format: "html",
		});
	});

	test("parses -r shortcut for ruleset", () => {
		resetMocks();
		const args = ["input.md", "-r", "gfm"];
		const result = parseArgs(args);
		expect(result).toEqual({ input: "input.md", output: null, ruleset: "gfm", format: "html" });
	});

	test("parses both shortcuts -o and -r", () => {
		resetMocks();
		const args = ["input.md", "-o", "out.html", "-r", "core"];
		const result = parseArgs(args);
		expect(result).toEqual({
			input: "input.md",
			output: "out.html",
			ruleset: "core",
			format: "html",
		});
	});

	test("parses mixed shortcuts and long options", () => {
		resetMocks();
		const args = ["-r", "gfm", "input.md", "--output", "out.html"];
		const result = parseArgs(args);
		expect(result).toEqual({
			input: "input.md",
			output: "out.html",
			ruleset: "gfm",
			format: "html",
		});
	});
});

describe("CLI parseArgs error handling", () => {
	let mockConsoleError: Mock;

	beforeEach(() => {
		resetMocks();
		vi.spyOn(globalThis.process, "exit").mockImplementation(() => {
			throw new Error("process.exit called");
		}) as Mock;
		mockConsoleError = vi.spyOn(console, "error").mockImplementation(() => {}) as Mock;
	});

	test("exits with error when no input file", () => {
		expect(() => parseArgs([])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith("Error: No input file specified");
	});

	test("exits with error when multiple input files", () => {
		expect(() => parseArgs(["input1.md", "input2.md"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith(expect.stringContaining("Multiple input files"));
	});

	test("exits with error when --output has no value", () => {
		expect(() => parseArgs(["input.md", "--output"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith("Error: --output requires a file path");
	});

	test("exits with error when -o has no value", () => {
		expect(() => parseArgs(["input.md", "-o"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith("Error: -o requires a file path");
	});

	test("exits with error when --ruleset has no value", () => {
		expect(() => parseArgs(["input.md", "--ruleset"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith(
			"Error: --ruleset requires a value (core, gfm, or extended)",
		);
	});

	test("exits with error when -r has no value", () => {
		expect(() => parseArgs(["input.md", "-r"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith(
			"Error: -r requires a value (core, gfm, or extended)",
		);
	});

	test("exits with error when -r has invalid ruleset", () => {
		expect(() => parseArgs(["input.md", "-r", "invalid"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith(expect.stringContaining("Invalid ruleset"));
	});

	test("exits with error when invalid ruleset", () => {
		expect(() => parseArgs(["input.md", "--ruleset", "invalid"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith(expect.stringContaining("Invalid ruleset"));
	});

	test("exits successfully on --help", () => {
		expect(() => parseArgs(["--help"])).toThrow("process.exit called");
	});

	test("exits successfully on -h", () => {
		expect(() => parseArgs(["-h"])).toThrow("process.exit called");
	});

	test("exits with error on unknown long option", () => {
		expect(() => parseArgs(["input.md", "--unknown"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith(expect.stringContaining("Unknown option"));
	});

	test("exits with error on unknown short option", () => {
		expect(() => parseArgs(["input.md", "-x"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith(expect.stringContaining("Unknown option '-x'"));
	});

	test("exits with error on unknown short option -a", () => {
		expect(() => parseArgs(["input.md", "-a"])).toThrow("process.exit called");
		expect(mockConsoleError).toHaveBeenCalledWith(expect.stringContaining("Unknown option '-a'"));
	});
});

describe("CLI main integration", () => {
	beforeEach(() => {
		resetMocks();
	});

	test("parses markdown to HTML with core ruleset", () => {
		const ruleset = getRuleset("core");
		const markdown = "# Heading\n\nParagraph text";
		const document = parse(markdown, ruleset, false);
		const html = renderHtml(document);
		expect(html).toContain("<h1>Heading</h1>");
		expect(html).toContain("<p>Paragraph text</p>");
	});

	test("parses markdown to HTML with gfm ruleset", () => {
		const ruleset = getRuleset("gfm");
		const markdown = "# Heading\n\n- List item 1\n- List item 2";
		const document = parse(markdown, ruleset, false);
		const html = renderHtml(document);
		expect(html).toContain("<h1>Heading</h1>");
		expect(html).toContain("<ul>");
	});

	test("parses markdown to HTML with extended ruleset", () => {
		const ruleset = getRuleset("extended");
		const markdown = "# Heading\n\n**bold** text";
		const document = parse(markdown, ruleset, false);
		const html = renderHtml(document);
		expect(html).toContain("<h1>Heading</h1>");
		expect(html).toContain("<strong>bold</strong>");
	});

	test("writes output to file when output is specified", () => {
		const html = "<h1>Test</h1>";
		fs.writeFileSync("output.html", html, "utf-8");
		expect(fs.writeFileSync).toHaveBeenCalledWith("output.html", html, "utf-8");
	});
});

describe("CLI getRuleset", () => {
	test("returns core ruleset", () => {
		const ruleset = getRuleset("core");
		expect(ruleset).toBeDefined();
	});

	test("returns gfm ruleset", () => {
		const ruleset = getRuleset("gfm");
		expect(ruleset).toBeDefined();
	});

	test("returns extended ruleset", () => {
		const ruleset = getRuleset("extended");
		expect(ruleset).toBeDefined();
	});
});
