import type BlockParserState from "../types/BlockParserState";
import type BlockRule from "../types/BlockRule";
import type MarkdownNode from "../types/MarkdownNode";
import closeNode from "../utils/closeNode";
import getEndOfLine from "../utils/getEndOfLine";
import isEscaped from "../utils/isEscaped";
import isNewLine from "../utils/isNewLine";
import isSpace from "../utils/isSpace";
import newNode from "../utils/newNode";

const rule: BlockRule = {
	name: "table",
	testStart,
	testContinue,
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

		let headers = lastNode.children![0].children!.map((c) => c.info);

		let row = newNode("table_row", true, state.i, state.line, 1, "", 0, []);
		lastNode.children!.push(row);

		let rowContent = state.src
			.substring(state.i, endOfLine)
			.trim()
			.replaceAll(/(^\||\|$)/g, "");
		let rowParts = rowContent.split(/(?<!\\)\|/);
		rowParts.length = headers.length;

		let ri = 0;
		for (let text of rowParts) {
			let cell = newNode("table_cell", true, state.i, state.line, 1, "", 0, []);
			cell.content = (text ?? "").trim().replaceAll("\\\|", "|");
			cell.info = headers[ri++];
			row.children!.push(cell);
		}

		state.i = endOfLine;
		return true;
	}

	// "The delimiter row consists of cells whose only content are hyphens (-),
	// and optionally, a leading or trailing colon (:), or both, to indicate
	// left, right, or center alignment respectively"
	let char = state.src[state.i];
	if (state.indent <= 3 && (char === "|" || char === "-" || char === ":")) {
		let cells: string[] = [char === ":" ? "left" : ""];
		let end = state.i + 1;
		let lastChar = char;
		for (; end < state.src.length; end++) {
			let nextChar = state.src[end];
			if (nextChar === "|") {
				cells.push("");
				lastChar = nextChar;
			} else if (nextChar === "-") {
				lastChar = nextChar;
			} else if (nextChar === ":") {
				let x = cells.length - 1;
				if (lastChar === "|") {
					cells[x] = "left";
				} else {
					cells[x] = cells[x] ? "center" : "right";
				}
				lastChar = nextChar;
			} else if (isNewLine(nextChar)) {
				// TODO: Handle windows crlf
				end++;
				break;
			} else if (isSpace(state.src.charCodeAt(end))) {
				continue;
			} else {
				return false;
			}
		}
		if (lastChar === "|") {
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
				if (headerContent[i] === "|" && !isEscaped(headerContent, i)) {
					headerCellCount++;
				}
			}
			if (cells.length !== headerCellCount) {
				return false;
			}

			let closedNode: MarkdownNode | undefined;

			if (state.maybeContinue) {
				state.maybeContinue = false;
				let i = state.openNodes.length;
				while (i-- > 1) {
					let node = state.openNodes[i];
					if (node.maybeContinuing) {
						node.maybeContinuing = false;
						closedNode = node;
						state.openNodes.length = i;
						break;
					}
				}
				parent = state.openNodes.at(-1)!;
			}

			if (closedNode !== undefined) {
				closeNode(state, closedNode);
			}

			let header = newNode("table_header", true, state.i, state.line, 1, "", 0, []);
			parent.children!.push(header);

			let headerParts = headerContent.split(/(?<!\\)\|/);
			let hi = 0;
			for (let text of headerParts) {
				let cell = newNode("table_cell", true, state.i, state.line, 1, "", 0, []);
				cell.content = text.trim().replaceAll("\\\|", "|");
				cell.info = cells[hi++];
				header.children!.push(cell);
			}

			parent.type = "table";
			parent.content = "";
			parent.markup = state.src.substring(state.i, end);
			state.i = end;
			return true;
		}
	}

	return false;
}

function testContinue(_state: BlockParserState, _node: MarkdownNode) {
	// Just close the table every time, and check whether the last node was a
	// table in testStart. That way we can interrupt tables with e.g.
	// blockquotes, even if the blockquote contains a pipe
	return false;
}
