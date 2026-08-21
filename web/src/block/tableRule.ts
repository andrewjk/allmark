import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import { COLON_CODE, DASH_CODE, PIPE_CODE } from "../utils/charCodes";
import getEndOfLine from "../utils/getEndOfLine";
import isEscaped from "../utils/isEscaped";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";
import newBlock from "../utils/newBlock";

const rule: BlockRule = {
	name: "table",
	testStart,
	// Just close the table every time, and check whether the last node was a
	// table in testStart. That way we can interrupt tables with e.g.
	// blockquotes, even if the blockquote contains a pipe
	testContinue: () => false,
};
export default rule;

function testStart(state: BlockParserState, parent: MarkdownNode) {
	if (parent.acceptsContent) {
		return false;
	}

	// We may already have a table
	let lastNode = parent.children?.at(-1);
	if (!state.hasBlankLine && lastNode?.type === "table") {
		let endOfLine = getEndOfLine(state);

		let headers = lastNode.children![0].children!.map((c) => c.info ?? "");

		let row = newBlock("table_row", state.i, state.line, "", 0);
		let rowLength = endOfLine - state.i;
		row.length = rowLength;
		lastNode.children!.push(row);

		let rowSrc = state.src.substring(state.i, state.i + rowLength);
		let pipePositions = loadPipePositions(rowSrc);

		let rowContent = rowSrc.trim().replaceAll(/(^\||\|$)/g, "");
		let rowParts = rowContent.split(/(?<!\\)\|/);
		rowParts.length = headers.length;

		for (let j = 0; j < rowParts.length; j++) {
			parseTableCell(row, state, j, rowParts, headers, pipePositions);
		}

		lastNode.length = endOfLine - lastNode.index;

		state.i = endOfLine;
		return true;
	}

	// "The delimiter row consists of cells whose only content are hyphens (-),
	// and optionally, a leading or trailing colon (:), or both, to indicate
	// left, right, or center alignment respectively"
	let charCode = state.src.charCodeAt(state.i);
	if (
		state.indent <= 3 &&
		(charCode === PIPE_CODE || charCode === DASH_CODE || charCode === COLON_CODE)
	) {
		let cells: string[] = [charCode === COLON_CODE ? "left" : ""];
		let end = state.i + 1;
		let lastCharCode = charCode;
		for (; end < state.src.length; end++) {
			let nextCharCode = state.src.charCodeAt(end);
			if (nextCharCode === PIPE_CODE) {
				cells.push("");
				lastCharCode = nextCharCode;
			} else if (nextCharCode === DASH_CODE) {
				lastCharCode = nextCharCode;
			} else if (nextCharCode === COLON_CODE) {
				let x = cells.length - 1;
				if (lastCharCode === PIPE_CODE) {
					cells[x] = "left";
				} else {
					cells[x] = cells[x] ? "center" : "right";
				}
				lastCharCode = nextCharCode;
			} else if (isNewLine(nextCharCode)) {
				end++;
				break;
			} else if (isSpace(nextCharCode)) {
				continue;
			} else {
				return false;
			}
		}
		if (lastCharCode === PIPE_CODE) {
			cells.pop();
		}

		let haveParagraph =
			parent.type === "paragraph" && !parent.blankAfter && /[^\s]/.test(parent.content);
		if (haveParagraph) {
			// "The header row must match the delimiter row in the number of
			// cells. If not, a table will not be recognized"
			let headerCellCount = 1;
			let headerContent = parent.content.trim().replaceAll(/(^\||\|$)/g, "");
			for (let i = 0; i < headerContent.length; i++) {
				if (headerContent.charCodeAt(i) === PIPE_CODE && !isEscaped(headerContent, i)) {
					headerCellCount++;
				}
			}
			if (cells.length !== headerCellCount) {
				return false;
			}

			let headerIndex = parent.index;
			let headerLength = parent.content.length;
			if (parent.content.endsWith("\n") || parent.content.endsWith("\r")) {
				headerLength--;
			}
			let header = newBlock("table_header", headerIndex, state.line, "", 0);
			header.length = headerLength;
			parent.children!.push(header);

			let headerSrc = parent.content.substring(0, headerLength);
			let pipePositions = loadPipePositions(headerSrc);

			let headerParts = headerContent.split(/(?<!\\)\|/);
			for (let j = 0; j < headerParts.length; j++) {
				parseTableCell(header, state, j, headerParts, cells, pipePositions);
			}

			parent.type = "table";
			parent.content = "";
			parent.markup = state.src.substring(state.i, end);
			parent.length = end - parent.index;
			state.i = end;
			return true;
		}
	}

	return false;
}

function loadPipePositions(line: string) {
	let pipePositions: number[] = [];
	let haveEndPipe = false;
	for (let i = 0; i < line.length; i++) {
		if (line.charCodeAt(i) === PIPE_CODE && !isEscaped(line, i)) {
			pipePositions.push(i);
			haveEndPipe = true;
		} else if (!isSpace(line.charCodeAt(i))) {
			// Make sure there's a start pipe position
			if (pipePositions.length === 0) {
				pipePositions.push(0);
			}
			haveEndPipe = false;
		}
	}
	// Make sure there's an end pipe position
	if (!haveEndPipe) {
		pipePositions.push(line.length - 1);
	}
	return pipePositions;
}

function parseTableCell(
	row: MarkdownNode,
	state: BlockParserState,
	index: number,
	parts: string[],
	headers: string[],
	pipePositions: number[],
) {
	let text = parts[index];
	let cellStart = pipePositions[index];
	let cellEnd = pipePositions[index + 1];
	let cellLength = cellEnd - cellStart + 1;
	let contentStart =
		row.index +
		cellStart +
		((text ?? "").trim().length > 0 ? (text ?? "").indexOf((text ?? "").trim()) + 1 : 0);
	let cell = newBlock("table_cell", row.index + cellStart, state.line, "", 0);
	cell.length = cellLength;
	cell.info = headers[index];
	row.children!.push(cell);

	let content = newBlock("table_cell_content", contentStart, state.line, "", 0);
	content.content = (text ?? "").trim().replaceAll("\\\|", "|");
	cell.children = [content];
}
