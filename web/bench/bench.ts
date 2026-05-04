import fs from "node:fs";

import { Bench, type ConsoleTableConverter, type Task, formatNumber } from "tinybench";

import parse from "../src/parse";
import render from "../src/render";
import gfmx from "../src/rulesets/gfm";
import htmlRenderers from "../src/rulesets/htmlRenderers";

// Markdown file from https://gist.github.com/allysonsilva/85fff14a22bbdf55485be947566cc09e

const markdownFile = "./bench/full-markdown.md";
const markdownSource = fs.readFileSync(markdownFile, "utf-8");

const bench = new Bench({ name: "simple benchmark", iterations: 100 });

bench.add("allmark", () => {
	const doc = parse(markdownSource, gfmx);
	render(doc, htmlRenderers);
});

await bench.run();

const tableConverter: ConsoleTableConverter = (task: Task): Record<string, number | string> => {
	const state = task.result.state;
	return {
		...(state === "aborted-with-statistics" || state === "completed"
			? {
					"Min (ms)": formatNumber(task.result.latency.min),
					"Median (ms)": formatNumber(task.result.latency.p50),
					"Mean (ms)": formatNumber(task.result.latency.mean),
					"Max (ms)": formatNumber(task.result.latency.max),
					"Std Dev (ms)": formatNumber(task.result.latency.sd),
					Samples: task.result.latency.samplesCount,
				}
			: state !== "errored"
				? {
						Min: "N/A",
						Median: "N/A",
						Mean: "N/A",
						Max: "N/A",
						StdDev: "N/A",
						Samples: "N/A",
						Remarks: state,
					}
				: {
						Error: task.result.error.message,
						Stack: task.result.error.stack ?? "N/A",
					}),
		...(state === "aborted-with-statistics" && {
			Remarks: state,
		}),
	};
};

console.log(bench.name);
console.table(bench.table(tableConverter));
