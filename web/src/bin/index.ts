import { readFileSync, writeFileSync } from "node:fs";

import parse from "../parse";
import render from "../render";
import consoleRenderers from "../rulesets/consoleRenderers";
import core from "../rulesets/core";
import extended from "../rulesets/extended";
import gfm from "../rulesets/gfm";
import htmlRenderers from "../rulesets/htmlRenderers";
import type RuleSet from "../types/RuleSet";

type Ruleset = "core" | "gfm" | "extended";
type Format = "html" | "console";

export function printUsage(): void {
	console.error(
		"Usage: allmark <input-file> [-o <file>] [-r <core|gfm|extended>] [-f <html|console>]",
	);
	console.error("  input-file   Path to the markdown file to convert");
	console.error("  -o, --output Path to output file (optional, prints to stdout by default)");
	console.error("  -r, --ruleset Ruleset to use: core, gfm, or extended (default: extended)");
	console.error("  -f, --format Output format: html or console (default: html)");
	console.error("  -h, --help   Show this help message");
}

export function parseArgs(args: string[]): {
	input: string;
	output: string | null;
	ruleset: Ruleset;
	format: Format;
} {
	const result = {
		input: "",
		output: null as string | null,
		ruleset: "extended" as Ruleset,
		format: "html" as Format,
	};
	let i = 0;

	while (i < args.length) {
		const arg = args[i];

		if (arg === "--output" || arg === "-o") {
			if (i + 1 >= args.length) {
				console.error(`Error: ${arg} requires a file path`);
				printUsage();
				process.exit(1);
			}
			result.output = args[i + 1];
			i += 2;
		} else if (arg === "--ruleset" || arg === "-r") {
			if (i + 1 >= args.length) {
				console.error(`Error: ${arg} requires a value (core, gfm, or extended)`);
				printUsage();
				process.exit(1);
			}
			const ruleset = args[i + 1] as Ruleset;
			if (ruleset !== "core" && ruleset !== "gfm" && ruleset !== "extended") {
				console.error(
					`Error: Invalid ruleset '${ruleset as string}'. Must be core, gfm, or extended`,
				);
				printUsage();
				process.exit(1);
			}
			result.ruleset = ruleset;
			i += 2;
		} else if (arg === "--format" || arg === "-f") {
			if (i + 1 >= args.length) {
				console.error(`Error: ${arg} requires a value (html or console)`);
				printUsage();
				process.exit(1);
			}
			const format = args[i + 1] as Format;
			if (format !== "html" && format !== "console") {
				console.error(`Error: Invalid format '${format as string}'. Must be html or console`);
				printUsage();
				process.exit(1);
			}
			result.format = format;
			i += 2;
		} else if (arg === "--help" || arg === "-h") {
			printUsage();
			process.exit(0);
		} else if (arg.startsWith("--")) {
			console.error(`Error: Unknown option '${arg}'`);
			printUsage();
			process.exit(1);
		} else if (arg.startsWith("-")) {
			console.error(`Error: Unknown option '${arg}'`);
			printUsage();
			process.exit(1);
		} else {
			if (result.input) {
				console.error(`Error: Multiple input files specified: '${result.input}' and '${arg}'`);
				printUsage();
				process.exit(1);
			}
			result.input = arg;
			i += 1;
		}
	}

	if (!result.input) {
		console.error("Error: No input file specified");
		printUsage();
		process.exit(1);
	}

	return result;
}

export function getRuleset(name: Ruleset): RuleSet {
	switch (name) {
		case "core":
			return core;
		case "gfm":
			return gfm;
		case "extended":
			return extended;
	}
}

export function main(): void {
	const args = parseArgs(process.argv.slice(2));

	try {
		const markdown = readFileSync(args.input, "utf-8");
		const ruleset = getRuleset(args.ruleset);
		const document = parse(markdown, ruleset);
		let output: string;

		if (args.format === "console") {
			output = render(document, consoleRenderers);
		} else {
			output = render(document, htmlRenderers);
		}

		if (args.output) {
			writeFileSync(args.output, output, "utf-8");
		} else {
			console.log(output);
		}
	} catch (error) {
		if (error instanceof Error) {
			if (error.message.includes("ENOENT")) {
				console.error(`Error: File not found: '${args.input}'`);
			} else {
				console.error(`Error: ${error.message}`);
			}
		} else {
			console.error("Error: Unknown error occurred");
		}
		process.exit(1);
	}
}

if (import.meta.url === `file://${process.argv[1]}`) {
	main();
}
