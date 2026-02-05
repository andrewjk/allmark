import markdownit from "markdown-it";
import mditfootnote from "markdown-it-footnote";
import { micromark } from "micromark";
import { gfm, gfmHtml } from "micromark-extension-gfm";
import fs from "node:fs";
import { Bench } from "tinybench";
import parse from "../web/src/parse";
import renderHtml from "../web/src/renderHtml";
import gfmx from "../web/src/rules/gfm"

// Markdown file from https://gist.github.com/allysonsilva/85fff14a22bbdf55485be947566cc09e

// MICROMARK
const markdownFile = "./full-markdown.md";
const markdownSource = fs.readFileSync(markdownFile, "utf-8");
const markdownHtmlFile = "./full-micromark.html";
fs.writeFileSync(
	markdownHtmlFile,
	micromark(markdownSource, {
		extensions: [gfm()],
		htmlExtensions: [gfmHtml()],
	})
);

// ALLMARK
const allmarkHtmlFile = "./full-allmark.html";
const root = parse(markdownSource, gfmx);
fs.writeFileSync(allmarkHtmlFile, renderHtml(root)
);

// MARKDOWN-IT
const md = markdownit().use(mditfootnote);
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
		renderHtml(doc);
	});

await bench.run();

console.log(bench.name);
console.table(bench.table());
