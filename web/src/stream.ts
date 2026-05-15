import parseBlockInlines from "./parse/parseBlockInlines";
import { parseLine } from "./parse/parseLine";
import renderChildren from "./render/renderChildren";
import type LinkReference from "./types/LinkReference";
import type MarkdownNode from "./types/MarkdownNode";
import type Renderer from "./types/Renderer";
import type RendererState from "./types/RendererState";
import type RuleSet from "./types/RuleSet";
import type StreamState from "./types/StreamState";
import newBlock from "./utils/newBlock";
import normalizeLabel from "./utils/normalizeLabel";

// Create a new streaming parser state. The mark string is placed between stable and unstable output.
export function createStream(rules: RuleSet, renderers: Renderer[]): StreamState {
	let document = newBlock("document", 0, 1, "", 0);

	return {
		rules,
		renderers,
		src: "",
		document,
		output: "",
		mark: getRenderMark(renderers),
		prevKeptCount: 0,
		parserState: {
			rules: rules.blocks,
			rulesMap: new Map(rules.blocks.map((b) => [b.name, b])),
			src: "",
			i: 0,
			line: 0,
			lineStart: 0,
			indent: 0,
			isEscaped: false,
			maybeContinue: false,
			hasBlankLine: false,
			openNodes: [document],
			refs: {},
			footnotes: {},
		},
	};
}

// Process one chunk of markdown, returning the full output so far.
// Kept blocks (before the deletion boundary) are never re-parsed.
// Re-parsed blocks get fresh inline parsing and rendering each chunk.
// The cached prefix is reused when possible to avoid re-rendering kept blocks.
export function streamChunk(state: StreamState, chunk: string): string {
	state.src += chunk;
	state.parserState.src = state.src;

	// Find where to start deleting: 2 real blocks from the end, with trailing link_refs
	// treated as part of the last real block's group.
	let children = state.document.children!;
	let deleteFrom = findDeleteFrom(children);

	// Splice off unstable blocks and record where to restart re-parsing.
	// Use findLineStart to include blank lines before the block so the parser
	// sees the full context needed for correct block detection (e.g. indentation).
	let keptCount = 0;
	let restartIndex = 0;

	if (deleteFrom < children.length) {
		keptCount = deleteFrom;
		restartIndex = findLineStart(state.src, children[deleteFrom].index);
		children.splice(deleteFrom);
	} else {
		children.length = 0;
		keptCount = 0;
	}

	// Collect refs from kept link_ref blocks only — avoids stale entries from blocks
	// that were temporarily parsed as link_ref during earlier chunks.
	let keptRefs = collectKeptRefs(state.parserState.refs, state.src, children, keptCount);
	let keptFootnotes = { ...state.parserState.footnotes };

	// Reset parser state for re-parsing from the restart position
	state.parserState.refs = {};
	state.parserState.footnotes = {};
	state.parserState.i = restartIndex;
	state.parserState.line = 0;
	state.parserState.lineStart = restartIndex;
	state.parserState.indent = 0;
	state.parserState.isEscaped = false;
	state.parserState.maybeContinue = false;
	state.parserState.hasBlankLine = false;
	state.parserState.openNodes = [state.document];

	// Stage 1: Re-parse blocks from restartIndex to end of source
	while (state.parserState.i < state.parserState.src.length) {
		parseLine(state.parserState);
	}

	// Close any remaining open nodes
	let j = state.parserState.openNodes.length;
	while (j--) {
		let openNode = state.parserState.openNodes[j];
		openNode.length = state.parserState.i - openNode.index;
		let rule = state.parserState.rulesMap.get(openNode.type);
		if (rule?.closeNode !== undefined) {
			rule.closeNode(state.parserState, openNode);
		}
	}

	// Merge kept refs/footnotes that weren't re-discovered during re-parse
	for (let key in keptRefs) {
		if (!state.parserState.refs[key]) {
			state.parserState.refs[key] = keptRefs[key];
		}
	}
	for (let key in keptFootnotes) {
		if (!state.parserState.footnotes[key]) {
			state.parserState.footnotes[key] = keptFootnotes[key];
		}
	}

	// Stage 2: Parse inlines for all re-parsed blocks
	for (let i = keptCount; i < children.length; i++) {
		resetInlineChildren(children[i]);
		parseBlockInlines(
			children[i],
			state.rules.inlines,
			state.parserState.refs,
			state.parserState.footnotes,
		);
	}

	// Reuse cached prefix when keptCount hasn't decreased.
	// If it decreased (rare), re-render the full prefix from block 0.
	let renderFrom = state.prevKeptCount;
	if (keptCount < renderFrom) {
		renderFrom = 0;
	}

	let prefix = "";
	if (renderFrom > 0) {
		let markIdx = state.output.indexOf(state.mark);
		if (markIdx !== -1) {
			prefix = state.output.substring(0, markIdx);
		}
	}
	let promoted = renderRaw(children.slice(renderFrom, keptCount), state.renderers);
	let tail = renderRaw(children.slice(keptCount), state.renderers);

	let output = prefix + promoted + state.mark + tail;
	if (output.length > 0) {
		output = output.replace(/\n*$/, "\n");
	}

	state.prevKeptCount = keptCount;
	state.output = output;
	return state.output;
}

// Find the deletion boundary by walking backwards from the end.
// Trailing link_ref blocks are treated as part of the last real block and always
// deleted with it. Then we count 2 real blocks (skipping interspersed link_refs).
function getRenderMark(renderers: Renderer[]) {
	let mark = "!!!";
	let streamRenderer = renderers.find((r) => r.name === "stream_mark");
	if (streamRenderer !== undefined) {
		let node = newBlock("", 0, 0, "", 0);
		let state = {
			renderersMap: new Map(),
			output: "",
			footnotes: [],
			footnoteRefs: {},
			listDepth: 0,
		};
		streamRenderer.render(node, state);
		mark = state.output;
	}
	return mark;
}

function findDeleteFrom(children: MarkdownNode[]): number {
	let end = children.length;
	while (end > 0 && children[end - 1].type.endsWith("_ref")) {
		end--;
	}

	let realCount = 0;
	for (let i = end - 1; i >= 0; i--) {
		if (children[i].type === "link_ref") {
			continue;
		}
		realCount++;
		if (realCount === 2) {
			return i;
		}
	}
	return 0;
}

// Collect ref entries only from link_ref blocks in the kept portion.
// Extracts the label from source text since link_ref nodes don't store it.
function collectKeptRefs(
	refs: Record<string, LinkReference>,
	src: string,
	children: MarkdownNode[],
	keptCount: number,
): Record<string, LinkReference> {
	let result: Record<string, LinkReference> = {};
	for (let i = 0; i < keptCount; i++) {
		if (children[i].type.endsWith("_ref")) {
			let node = children[i];
			let blockSrc = src.substring(node.index, node.index + node.length);
			let bracketEnd = blockSrc.indexOf("]:");
			if (bracketEnd !== -1) {
				let label = normalizeLabel(blockSrc.substring(1, bracketEnd));
				if (refs[label]) {
					result[label] = refs[label];
				}
			}
		}
	}
	return result;
}

// Walk backwards from pos to find the start of the line, including preceding blank lines.
// Ensures the parser sees blank-line context needed for correct block detection.
function findLineStart(src: string, pos: number): number {
	let i = pos;
	while (i > 0 && src[i - 1] !== "\n") i--;
	while (i > 0 && src[i - 1] === "\n") i--;
	if (i > 0) i++;
	return i;
}

// Render children into a fresh RendererState without trailing newline normalization.
// Preserves inter-block newlines (e.g. console renderers use \n\n between blocks).
function renderRaw(children: MarkdownNode[], renderers: Renderer[]): string {
	if (children.length === 0) return "";
	let wrapper = newBlock("document", 0, 1, "", 0);
	wrapper.children = children;
	let state: RendererState = {
		renderersMap: new Map(renderers.map((r) => [r.name, r])),
		output: "",
		footnotes: [],
		footnoteRefs: {},
		listDepth: 0,
	};
	renderChildren(wrapper, state);
	return state.output;
}

// Strip inline (non-block) children so parseBlockInlines can re-populate them
function resetInlineChildren(node: MarkdownNode): void {
	if (!node.children) return;
	node.children = node.children.filter((child) => child.block);
	for (let child of node.children) {
		resetInlineChildren(child);
	}
}
