import fs from "node:fs";

import { renderHtmlSync } from "cmark-gfm";
import markdownit from "markdown-it";
import mditfootnote from "markdown-it-footnote";
// @ts-ignore
import mdittasklist from "markdown-it-task-lists";
import { micromark } from "micromark";
import { gfm, gfmHtml } from "micromark-extension-gfm";
import { Bench, type ConsoleTableConverter, type Task, formatNumber } from "tinybench";

import parse from "../web/src/parse";
import render from "../web/src/render";
import gfmx from "../web/src/rulesets/gfm";
import htmlRenderers from "../web/src/rulesets/htmlRenderers";

// Markdown file from https://gist.github.com/allysonsilva/85fff14a22bbdf55485be947566cc09e

const markdownFile = "./full-markdown.md";
const markdownSource = fs.readFileSync(markdownFile, "utf-8");

// CMARK-GFM
const cmarkHtmlFile = "./full-cmark-gfm.html";
const cmarkOptions = {
	footnotes: true,
	unsafe: true,
	extensions: {
		table: true,
		strikethrough: true,
		tagfilter: true,
		autolink: true,
		tasklist: true,
	},
};
fs.writeFileSync(cmarkHtmlFile, renderHtmlSync(markdownSource, cmarkOptions));

// MICROMARK
const micromarkHtmlFile = "./full-micromark.html";
fs.writeFileSync(
	micromarkHtmlFile,
	micromark(markdownSource, {
		extensions: [gfm()],
		htmlExtensions: [gfmHtml()],
	}),
);

// ALLMARK
const allmarkHtmlFile = "./full-allmark.html";
const doc = parse(markdownSource, gfmx);
fs.writeFileSync(allmarkHtmlFile, render(doc, htmlRenderers));

// MARKDOWN-IT
const md = markdownit().use(mditfootnote).use(mdittasklist);
const encode = md.utils.lib.mdurl.encode;
md.normalizeLink = (url: string) => encode(url);
md.normalizeLinkText = (str: string) => str;
fs.writeFileSync("./full-markdown-it.html", md.render(markdownSource));

const bench = new Bench({ name: "simple benchmark", iterations: 100 });

bench
	.add("markdown-it", () => {
		// Replace normalizers to more primitive, for more "honest" compare.
		// Default ones can cause 1.5x slowdown.
		const md = markdownit();
		const encode = md.utils.lib.mdurl.encode;
		md.normalizeLink = (url: string) => encode(url);
		md.normalizeLinkText = (str: string) => str;
		md.render(markdownSource);
	})
	.add("micromark", () => {
		micromark(markdownSource, {
			extensions: [gfm()],
			htmlExtensions: [gfmHtml()],
		});
	})
	.add("allmark", () => {
		const doc = parse(markdownSource, gfmx);
		render(doc, htmlRenderers);
	})
	.add("cmark-gfm", () => {
		renderHtmlSync(markdownSource, cmarkOptions);
	});

await bench.run();

const tableConverter: ConsoleTableConverter = (task: Task): Record<string, number | string> => {
	const state = task.result.state;
	return {
		"Task name": task.name,
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
