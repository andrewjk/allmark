import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import { getChildren, forEachChild } from "../utils/nodeUtils";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "table",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.dim;
	const reset = ANSI.reset;

	const children = getChildren(node);
	if (children.length === 0) return;

	const headerRow = children[0];
	const dataRows = children.slice(1);

	const headerCells = getChildren(headerRow);
	const cellTexts: string[][] = [];

	const maxColumns = Math.max(headerCells.length, ...dataRows.map((r) => getChildren(r).length));
	const columnWidths = Array.from({ length: maxColumns }).fill(0) as number[];

	for (let i = 0; i < headerCells.length; i++) {
		const text = getTextFromNode(headerCells[i]);
		if (!cellTexts[0]) cellTexts[0] = [];
		cellTexts[0][i] = text;
		columnWidths[i] = Math.max(columnWidths[i], text.length + 2);
	}

	for (let r = 0; r < dataRows.length; r++) {
		const row = dataRows[r];
		const rowCells = getChildren(row);
		if (!cellTexts[r + 1]) cellTexts[r + 1] = [];
		for (let c = 0; c < rowCells.length; c++) {
			const text = getTextFromNode(rowCells[c]);
			cellTexts[r + 1][c] = text;
			columnWidths[c] = Math.max(columnWidths[c], text.length + 2);
		}
	}

	const makeLine = (left: string, mid: string, right: string, sep: string) => {
		let line = left;
		for (let i = 0; i < columnWidths.length; i++) {
			line += "─".repeat(columnWidths[i]);
			if (i < columnWidths.length - 1) {
				line += i === 0 ? mid : sep;
			}
		}
		line += right;
		return `${style}${line}${reset}\n`;
	};

	state.output += makeLine("┌", "┬", "┐", "┬");

	if (headerCells.length > 0) {
		state.output += `${style}│${reset}`;
		for (let i = 0; i < headerCells.length; i++) {
			const text = cellTexts[0]?.[i] ?? "";
			const align = headerCells[i].info ?? "";
			state.output += ` ${padText(text, columnWidths[i] - 2, align)}${style}│${reset}`;
		}
		state.output += "\n";
	}

	state.output += makeLine("├", "┼", "┤", "┼");

	for (let r = 0; r < dataRows.length; r++) {
		const row = dataRows[r];
		const rowCells = getChildren(row);
		state.output += `${style}│${reset}`;
		for (let c = 0; c < columnWidths.length; c++) {
			const text = cellTexts[r + 1]?.[c] ?? "";
			const align = rowCells[c]?.info ?? "";
			state.output += ` ${padText(text, columnWidths[c] - 2, align)}${style}│${reset}`;
		}
		state.output += "\n";
	}

	state.output += makeLine("└", "┴", "┘", "┴");
}

function getTextFromNode(node: MarkdownNode): string {
	if (node.type === "text") {
		return node.content;
	}
	let text = "";
	forEachChild(node, (child) => {
		text += getTextFromNode(child);
	});
	return text;
}

function padText(text: string, width: number, align: string): string {
	if (align === "right") {
		return text.padStart(width) + " ";
	}
	if (align === "center") {
		const leftPad = Math.floor((width - text.length) / 2);
		const rightPad = width - text.length - leftPad;
		return " ".repeat(leftPad) + text + " ".repeat(rightPad);
	}
	return text.padEnd(width) + " ";
}
