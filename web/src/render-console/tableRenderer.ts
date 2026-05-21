import type MarkdownNode from "../types/MarkdownNode";
import type Renderer from "../types/Renderer";
import type RendererState from "../types/RendererState";
import ANSI from "./ansi";

const renderer: Renderer = {
	name: "table",
	render,
};
export default renderer;

function render(node: MarkdownNode, state: RendererState): void {
	const style = ANSI.dim;
	const reset = ANSI.reset;

	const children = node.children ?? [];
	if (children.length === 0) return;

	const headerRow = children[0];
	const dataRows = children.slice(1);

	const headerCells = headerRow.children ?? [];
	const cellTexts: string[][] = [];

	const maxColumns = Math.max(headerCells.length, ...dataRows.map((r) => r.children?.length ?? 0));
	const columnWidths = Array.from({ length: maxColumns }).fill(0) as number[];

	for (let i = 0; i < headerCells.length; i++) {
		const text = getTextFromNode(headerCells[i]);
		if (!cellTexts[0]) cellTexts[0] = [];
		cellTexts[0][i] = text;
		columnWidths[i] = Math.max(columnWidths[i], text.length + 2);
	}

	for (let r = 0; r < dataRows.length; r++) {
		const row = dataRows[r];
		const rowCells = row.children ?? [];
		if (!cellTexts[r + 1]) cellTexts[r + 1] = [];
		for (let c = 0; c < rowCells.length; c++) {
			const text = getTextFromNode(rowCells[c]);
			cellTexts[r + 1][c] = text;
			columnWidths[c] = Math.max(columnWidths[c], text.length + 2);
		}
	}

	let targetWidths = columnWidths;
	let wrappedTexts: string[][][] | undefined;

	if (state.lineWidth !== undefined) {
		const totalWidth = 1 + columnWidths.reduce((a, b) => a + b, 0) + maxColumns;
		if (totalWidth > state.lineWidth) {
			const fitWidths = fitColumns(columnWidths, state.lineWidth, maxColumns, cellTexts);
			wrappedTexts = wrapAllCells(cellTexts, fitWidths);
			targetWidths = Array.from<number>({ length: maxColumns }).fill(2);
			for (let c = 0; c < maxColumns; c++) {
				for (let r = 0; r < cellTexts.length; r++) {
					for (const line of wrappedTexts[r][c]) {
						targetWidths[c] = Math.max(targetWidths[c], line.length + 2);
					}
				}
			}
		}
	}

	const makeLine = (left: string, mid: string, right: string) => {
		let line = left;
		for (let i = 0; i < targetWidths.length; i++) {
			line += "─".repeat(targetWidths[i]);
			if (i < targetWidths.length - 1) {
				line += mid;
			}
		}
		line += right;
		return `${style}${line}${reset}\n`;
	};

	const getAlign = (rowIdx: number, colIdx: number): string => {
		const row = rowIdx === 0 ? headerRow : dataRows[rowIdx - 1];
		return row.children?.[colIdx]?.info ?? "";
	};

	state.output += makeLine("┌", "┬", "┐");

	if (headerCells.length > 0) {
		renderRow(state, style, reset, cellTexts, 0, targetWidths, maxColumns, getAlign, wrappedTexts);
	}

	state.output += makeLine("├", "┼", "┤");

	for (let r = 0; r < dataRows.length; r++) {
		renderRow(
			state,
			style,
			reset,
			cellTexts,
			r + 1,
			targetWidths,
			maxColumns,
			getAlign,
			wrappedTexts,
		);
	}

	state.output += makeLine("└", "┴", "┘");
}

function renderRow(
	state: RendererState,
	style: string,
	reset: string,
	cellTexts: string[][],
	rowIdx: number,
	targetWidths: number[],
	maxColumns: number,
	getAlign: (row: number, col: number) => string,
	wrappedTexts: string[][][] | undefined,
): void {
	let maxLines = 1;
	const cellLines: string[][] = [];
	for (let c = 0; c < maxColumns; c++) {
		if (wrappedTexts?.[rowIdx]?.[c]) {
			cellLines.push(wrappedTexts[rowIdx][c]);
		} else {
			cellLines.push([cellTexts[rowIdx]?.[c] ?? ""]);
		}
		maxLines = Math.max(maxLines, cellLines[c].length);
	}

	for (let line = 0; line < maxLines; line++) {
		state.output += `${style}│${reset}`;
		for (let c = 0; c < maxColumns; c++) {
			const text = cellLines[c]?.[line] ?? "";
			const align = getAlign(rowIdx, c);
			state.output += ` ${padText(text, targetWidths[c] - 2, align)}${style}│${reset}`;
		}
		state.output += "\n";
	}
}

function fitColumns(
	columnWidths: number[],
	lineWidth: number,
	numColumns: number,
	cellTexts: string[][],
): number[] {
	const available = lineWidth - 1 - numColumns;
	const targetWidths = [...columnWidths];

	const minWidths = columnWidths.map((_, colIdx) => {
		let maxWordLen = 1;
		for (const row of cellTexts) {
			const text = row[colIdx] ?? "";
			for (const word of text.split(" ")) {
				maxWordLen = Math.max(maxWordLen, word.length);
			}
		}
		return maxWordLen + 2;
	});

	while (targetWidths.reduce((a, b) => a + b, 0) > available) {
		let maxIdx = 0;
		for (let i = 1; i < targetWidths.length; i++) {
			if (targetWidths[i] > targetWidths[maxIdx]) maxIdx = i;
		}
		if (targetWidths[maxIdx] <= minWidths[maxIdx]) break;
		targetWidths[maxIdx]--;
	}

	return targetWidths;
}

function wrapAllCells(cellTexts: string[][], targetWidths: number[]): string[][][] {
	const result: string[][][] = [];
	for (let r = 0; r < cellTexts.length; r++) {
		result[r] = [];
		for (let c = 0; c < targetWidths.length; c++) {
			const text = cellTexts[r]?.[c] ?? "";
			result[r][c] = wrapText(text, targetWidths[c] - 2);
		}
	}
	return result;
}

function wrapText(text: string, maxWidth: number): string[] {
	if (text.length <= maxWidth) return [text];
	const words = text.split(" ");
	const lines: string[] = [];
	let currentLine = "";
	for (const word of words) {
		if (currentLine.length === 0) {
			currentLine = word;
		} else if (currentLine.length + 1 + word.length <= maxWidth) {
			currentLine += " " + word;
		} else {
			lines.push(currentLine);
			currentLine = word;
		}
	}
	if (currentLine) lines.push(currentLine);
	return lines;
}

function getTextFromNode(node: MarkdownNode): string {
	if (node.type === "text") {
		return node.content;
	}
	if (node.children) {
		return node.children.map(getTextFromNode).join("");
	}
	return "";
}

function padText(text: string, width: number, align: string): string {
	if (align === "right") {
		return text.padStart(width) + " ";
	}
	if (align === "center") {
		const leftPad = Math.floor((width - text.length) / 2);
		const rightPad = width - text.length - leftPad + 1;
		return " ".repeat(leftPad) + text + " ".repeat(rightPad);
	}
	return text.padEnd(width) + " ";
}
