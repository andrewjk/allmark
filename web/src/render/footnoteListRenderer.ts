import type RendererState from "../types/RendererState";
import renderChildren from "./renderChildren";

export default function renderFootnoteList(state: RendererState): void {
	state.output += `<section class="footnotes">\n<ol>\n`;
	let number = 1;
	for (let node of state.footnotes) {
		let label = number++;
		let id = `fn${label}`;
		let href = `#fnref${label}`;
		state.output += `<li id="${id}">`;
		renderChildren(node, state);
		if (state.output.endsWith("</p>\n")) {
			state.output = state.output.slice(0, state.output.length - 5);
		}
		state.output += ` <a href="${href}" class="footnote-backref">↩</a></p>\n</li>\n`;
	}
	state.output += `</ol>\n</section>`;
}
