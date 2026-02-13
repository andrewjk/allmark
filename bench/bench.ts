import markdownit from "markdown-it";
import mditfootnote from "markdown-it-footnote";
// @ts-ignore
import mdittasklist from "markdown-it-task-lists";
import { micromark } from "micromark";
import { gfm, gfmHtml } from "micromark-extension-gfm";
import fs from "node:fs";
import { Bench } from "tinybench";
import parse from "../web/src/parse";
import renderHtml from "../web/src/renderHtml";
import gfmx from "../web/src/rulesets/gfm"
import { renderHtmlSync } from "cmark-gfm"

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
}
fs.writeFileSync(
	cmarkHtmlFile,
	renderHtmlSync(markdownSource, cmarkOptions)
);

// MICROMARK
const micromarkHtmlFile = "./full-micromark.html";
fs.writeFileSync(
	micromarkHtmlFile,
	micromark(markdownSource, {
		extensions: [gfm()],
		htmlExtensions: [gfmHtml()],
	})
);

// ALLMARK
const allmarkHtmlFile = "./full-allmark.html";
const root = parse(markdownSource, gfmx);
fs.writeFileSync(allmarkHtmlFile, renderHtml(root, gfmx.renderers)
);

// MARKDOWN-IT
const md = markdownit().use(mditfootnote).use(mdittasklist);
const encode = md.utils.lib.mdurl.encode;
md.normalizeLink = (url: string) => encode(url);
md.normalizeLinkText = (str: string) => str;
fs.writeFileSync(
	"./full-markdown-it.html",
	md.render(markdownSource)
);

const bench = new Bench({ name: "simple benchmark", time: 100 });

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
		renderHtml(doc, gfmx.renderers);
	})
	.add("cmark-gfm", () => {
		renderHtmlSync(markdownSource, cmarkOptions);
	})

await bench.run();

console.log(bench.name);
console.table(bench.table());
