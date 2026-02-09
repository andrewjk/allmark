import type MarkdownNode from "./types/MarkdownNode";
import decodeEntities from "./utils/decodeEntities";
import escapeHtml from "./utils/escapeHtml";
import escapePunctuation from "./utils/escapePunctuation";
import isSpace from "./utils/isSpace";

interface Result {
	html: string;
	footnotes: MarkdownNode[];
}

export default function renderHtml(root: MarkdownNode): string {
	let result = { html: "", footnotes: [] };
	renderNode(root, result);

	if (result.footnotes.length > 0) {
		renderFootnoteList(result);
	}

	return result.html;
}

function renderNode(
	node: MarkdownNode,
	result: Result,
	first = false,
	last = false,
	decode = true,
) {
	switch (node.type) {
		case "document": {
			renderChildren(node, result, decode);
			break;
		}
		case "heading":
		case "heading_underline": {
			renderHeader(node, result);
			break;
		}
		case "paragraph": {
			renderTag(node, result, "p");
			break;
		}
		case "thematic_break": {
			renderSelfClosedTag(node, result, "hr");
			break;
		}
		case "list_bulleted":
		case "list_ordered": {
			renderList(node, result);
			break;
		}
		case "list_item": {
			renderTag(node, result, "li");
			break;
		}
		case "list_task_item": {
			let checked = !isSpace(node.markup.charCodeAt(1)) ? ` checked=""` : ``;
			result.html += `<input type="checkbox"${checked} disabled="" /> `;
			break;
		}
		case "block_quote": {
			renderTag(node, result, "blockquote");
			break;
		}
		case "code_block":
		case "code_fence": {
			renderCodeBlock(node, result);
			break;
		}
		case "table": {
			renderTable(node, result);
			break;
		}
		case "table_header":
		case "table_row": {
			renderTag(node, result, "tr");
			break;
		}
		case "table_cell": {
			renderTag(node, result, "td");
			break;
		}
		case "emphasis": {
			renderTag(node, result, "em");
			break;
		}
		case "strikethrough": {
			renderTag(node, result, "del");
			break;
		}
		case "strong": {
			renderTag(node, result, "strong");
			break;
		}
		case "link": {
			renderLink(node, result);
			break;
		}
		case "image": {
			renderImage(node, result);
			break;
		}
		case "hard_break": {
			result.html += "<br />\n";
			break;
		}
		case "code_span": {
			renderTag(node, result, "code", false);
			break;
		}
		case "html_block":
		case "html_span": {
			result.html += node.content;
			break;
		}
		case "text": {
			renderText(node, result, first, last, decode);
			break;
		}
		case "footnote": {
			renderFootnote(node, result);
			break;
		}
		case "footnote_ref": {
			// Footnote definitions are not rendered inline
			break;
		}
		case "alert": {
			renderAlert(node, result);
			break;
		}
		case "superscript": {
			renderTag(node, result, "sup");
			break;
		}
		case "subscript": {
			renderTag(node, result, "sub");
			break;
		}
		case "highlight": {
			renderTag(node, result, "mark");
			break;
		}
		case "insertion": {
			renderInsertion(node, result);
			break;
		}
		case "deletion": {
			renderDeletion(node, result);
			break;
		}
	}
}

function renderChildren(node: MarkdownNode, result: Result, decode = true) {
	let children = node.children;
	if (children && children.length) {
		let trim =
			node.type !== "code_block" && node.type !== "code_fence" && node.type !== "code_span";
		for (let [i, child] of children.entries()) {
			let first = i === 0;
			let last = i === children.length - 1;
			renderNode(child, result, trim && first, trim && last, decode);
		}
	}
}

function renderHeader(node: MarkdownNode, result: Result) {
	startNewLine(node, result);
	let level = 0;
	if (node.markup.startsWith("#")) {
		level = node.markup.length;
	} else if (node.markup.includes("=")) {
		level = 1;
	} else if (node.markup.includes("-")) {
		level = 2;
	}
	result.html += `<h${level}>`;
	innerNewLine(node, result);
	renderChildren(node, result);
	result.html += `</h${level}>`;
	endNewLine(node, result);
}

function renderList(node: MarkdownNode, result: Result) {
	// TODO: Can we remove paragraphs when parsing instead?

	let ordered = node.type === "list_ordered";
	let start = "";
	if (ordered) {
		let startNumber = parseInt(node.markup.substring(0, node.markup.length - 1));
		if (startNumber !== 1) {
			start = ` start="${startNumber}"`;
		}
	}

	startNewLine(node, result);
	result.html += `<${ordered ? `ol${start}` : `ul`}>`;
	innerNewLine(node, result);

	// "A list is loose if any of its constituent list items are separated by
	// blank lines, or if any of its constituent list items directly contain two
	// block-level elements with a blank line between them. Otherwise a list is
	// tight."
	let loose = false;
	for (let i = 0; i < node.children!.length - 1; i++) {
		let child = node.children![i]!;

		// A list item has a blank line after if its last child has a blank line after
		let grandchild = child.children?.at(-1);
		if (grandchild?.blankAfter) {
			child.blankAfter = true;
		}

		if (child.blankAfter) {
			loose = true;
			break;
		}
	}
	for (let i = 0; i < node.children!.length; i++) {
		let child = node.children![i]!;
		for (let j = 0; j < child.children!.length - 1; j++) {
			let first = child.children![j];
			let second = child.children![j + 1];
			if (first.block && first.blankAfter && second.block) {
				loose = true;
				break;
			}
		}
	}

	for (let item of node.children!) {
		result.html += "<li>";
		for (let [i, child] of item.children!.entries()) {
			if (!loose && child.type === "paragraph") {
				// Skip paragraphs under list items to make the list tight
				renderChildren(child, result);
			} else {
				if (i === 0) {
					innerNewLine(item, result);
				}
				renderNode(child, result, i === item.children!.length - 1);
			}
		}
		result.html += "</li>";
		endNewLine(node, result);
	}

	result.html += `</${ordered ? `ol` : `ul`}>`;
	endNewLine(node, result);
}

function renderCodeBlock(node: MarkdownNode, result: Result) {
	startNewLine(node, result);
	let lang = node.info ? ` class="language-${node.info.trim().split(" ")[0]}"` : "";
	result.html += `<pre><code${lang}>`;
	renderChildren(node, result, false);
	result.html += "</code></pre>";
	endNewLine(node, result);
}

function renderTable(node: MarkdownNode, result: Result) {
	startNewLine(node, result);
	result.html += "<table>\n<thead>\n<tr>\n";
	for (let cell of node.children![0].children!) {
		renderTableCell(cell, result, "th");
	}
	result.html += "</tr>\n</thead>\n";
	if (node.children!.length > 1) {
		result.html += "<tbody>\n";
		for (let row of node.children!.slice(1)) {
			result.html += "<tr>\n";
			for (let cell of row.children!) {
				renderTableCell(cell, result, "td");
			}
			result.html += "</tr>\n";
		}
		result.html += "</tbody>\n";
	}
	result.html += "</table>";
	endNewLine(node, result);
}

function renderTableCell(node: MarkdownNode, result: Result, tag: string) {
	startNewLine(node, result);
	let align = node.info ? ` align="${node.info}"` : "";
	result.html += `<${tag}${align}>`;
	innerNewLine(node, result);
	renderChildren(node, result);
	result.html += `</${tag}>`;
	endNewLine(node, result);
}

function renderLink(node: MarkdownNode, result: Result) {
	startNewLine(node, result);
	let title = node.title ? ` title="${node.title}"` : "";
	result.html += `<a href="${node.info}"${title}>`;
	renderChildren(node, result);
	result.html += "</a>";
	endNewLine(node, result);
}

function renderImage(node: MarkdownNode, result: Result) {
	startNewLine(node, result);
	let alt = getChildText(node);
	let title = node.title ? ` title="${node.title}"` : "";
	result.html += `<img src="${node.info}" alt="${alt}"${title} />`;
	endNewLine(node, result);
}

function renderFootnote(node: MarkdownNode, result: Result) {
	if (result.footnotes.find((f) => f.info === node.info) === undefined) {
		result.footnotes.push(node);
	}
	let label = result.footnotes.length;
	let id = `fnref${label}`;
	let href = `#fn${label}`;
	result.html += `<sup class="footnote-ref"><a href="${href}" id="${id}">${label}</a></sup>`;
}

function renderFootnoteList(result: Result) {
	result.html += `<section class="footnotes">\n<ol>\n`;
	let number = 1;
	for (let node of result.footnotes) {
		let label = number++;
		let id = `fn${label}`;
		let href = `#fnref${label}`;
		result.html += `<li id="${id}">`;
		renderChildren(node, result);
		if (result.html.endsWith("</p>\n")) {
			result.html = result.html.slice(0, result.html.length - 5);
		}
		result.html += ` <a href="${href}" class="footnote-backref">↩</a></p>\n</li>\n`;
	}
	result.html += `</ol>\n</section>`;
}

function renderAlert(node: MarkdownNode, result: Result) {
	startNewLine(node, result);
	result.html += `<div class="markdown-alert markdown-alert-${node.markup}">
<p class="markdown-alert-title">${node.markup.substring(0, 1).toUpperCase() + node.markup.substring(1)}</p>`;
	renderChildren(node, result);
	result.html += "</div>";
	endNewLine(node, result);
}

function renderInsertion(node: MarkdownNode, result: Result) {
	startNewLine(node, result);
	result.html += `<ins class="markdown-insertion">`;
	renderChildren(node, result);
	result.html += "</ins>";
	endNewLine(node, result);
}

function renderDeletion(node: MarkdownNode, result: Result) {
	startNewLine(node, result);
	result.html += `<del class="markdown-deletion">`;
	renderChildren(node, result);
	result.html += "</del>";
	endNewLine(node, result);
}

function getChildText(node: MarkdownNode) {
	let text = "";
	if (node.children) {
		for (let child of node.children) {
			if (child.type === "text") {
				text += child.markup;
			} else {
				text += getChildText(child);
			}
		}
	}
	return text;
}

function renderTag(node: MarkdownNode, result: Result, tag: string, decode = true) {
	startNewLine(node, result);
	result.html += `<${tag}>`;
	// Block nodes with no children still need a newline
	if (node.block && node.children?.length === 0) {
		result.html += "\n";
	} else {
		innerNewLine(node, result);
	}
	renderChildren(node, result, decode);
	result.html += `</${tag}>`;
	endNewLine(node, result);
}

function renderSelfClosedTag(node: MarkdownNode, result: Result, tag: string) {
	startNewLine(node, result);
	result.html += `<${tag} />`;
	endNewLine(node, result);
}

function renderText(
	node: MarkdownNode,
	result: Result,
	first: boolean,
	last: boolean,
	decode: boolean,
) {
	let markup = node.markup;
	if (first) {
		markup = markup.trimStart();
	}
	if (last) {
		markup = markup.trimEnd();
	}
	if (decode) {
		markup = decodeEntities(markup);
		markup = escapePunctuation(markup);
	}
	markup = escapeHtml(markup);
	result.html += markup;
}

function startNewLine(node: MarkdownNode, result: Result) {
	if (result.html.length && node.block && !result.html.endsWith("\n")) {
		result.html += "\n";
	}
}

function innerNewLine(node: MarkdownNode, result: Result) {
	if (node.block && node.children && node.children[0]?.block) {
		result.html += "\n";
	}
}

function endNewLine(node: MarkdownNode, result: Result) {
	if (node.block) {
		result.html += "\n";
	}
}
