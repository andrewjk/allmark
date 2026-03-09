import alertRenderer from "../render/alertRenderer";
import blockQuoteRenderer from "../render/blockQuoteRenderer";
import codeBlockRenderer from "../render/codeBlockRenderer";
import codeFenceRenderer from "../render/codeFenceRenderer";
import codeSpanRenderer from "../render/codeSpanRenderer";
import commentRenderer from "../render/commentRenderer";
import deletionRenderer from "../render/deletionRenderer";
import emphasisRenderer from "../render/emphasisRenderer";
import footnoteListRenderer from "../render/footnoteListRenderer";
import footnoteRenderer from "../render/footnoteRenderer";
import hardBreakRenderer from "../render/hardBreakRenderer";
import headingRenderer from "../render/headingRenderer";
import headingUnderlineRenderer from "../render/headingUnderlineRenderer";
import highlightRenderer from "../render/highlightRenderer";
import htmlBlockRenderer from "../render/htmlBlockRenderer";
import htmlSpanRenderer from "../render/htmlSpanRenderer";
import imageRenderer from "../render/imageRenderer";
import insertionRenderer from "../render/insertionRenderer";
import linkRenderer from "../render/linkRenderer";
import listBulletedRenderer from "../render/listBulletedRenderer";
import listOrderedRenderer from "../render/listOrderedRenderer";
import listTaskItemRenderer from "../render/listTaskItemRenderer";
import paragraphRenderer from "../render/paragraphRenderer";
import strikethroughRenderer from "../render/strikethroughRenderer";
import strongRenderer from "../render/strongRenderer";
import subscriptRenderer from "../render/subscriptRenderer";
import superscriptRenderer from "../render/superscriptRenderer";
import tableCellRenderer from "../render/tableCellRenderer";
import tableHeaderRenderer from "../render/tableHeaderRenderer";
import tableRenderer from "../render/tableRenderer";
import tableRowRenderer from "../render/tableRowRenderer";
import textRenderer from "../render/textRenderer";
import thematicBreakRenderer from "../render/thematicBreakRenderer";
import type Renderer from "../types/Renderer";

const renderers: Map<string, Renderer> = new Map([
	[alertRenderer.name, alertRenderer],
	[blockQuoteRenderer.name, blockQuoteRenderer],
	[codeBlockRenderer.name, codeBlockRenderer],
	[codeFenceRenderer.name, codeFenceRenderer],
	[codeSpanRenderer.name, codeSpanRenderer],
	[commentRenderer.name, commentRenderer],
	[deletionRenderer.name, deletionRenderer],
	[emphasisRenderer.name, emphasisRenderer],
	[footnoteRenderer.name, footnoteRenderer],
	[footnoteListRenderer.name, footnoteListRenderer],
	[hardBreakRenderer.name, hardBreakRenderer],
	[headingRenderer.name, headingRenderer],
	[headingUnderlineRenderer.name, headingUnderlineRenderer],
	[highlightRenderer.name, highlightRenderer],
	[htmlBlockRenderer.name, htmlBlockRenderer],
	[htmlSpanRenderer.name, htmlSpanRenderer],
	[imageRenderer.name, imageRenderer],
	[insertionRenderer.name, insertionRenderer],
	[linkRenderer.name, linkRenderer],
	[listBulletedRenderer.name, listBulletedRenderer],
	[listOrderedRenderer.name, listOrderedRenderer],
	[listTaskItemRenderer.name, listTaskItemRenderer],
	[paragraphRenderer.name, paragraphRenderer],
	[strikethroughRenderer.name, strikethroughRenderer],
	[strongRenderer.name, strongRenderer],
	[subscriptRenderer.name, subscriptRenderer],
	[superscriptRenderer.name, superscriptRenderer],
	[tableRenderer.name, tableRenderer],
	[tableCellRenderer.name, tableCellRenderer],
	[tableHeaderRenderer.name, tableHeaderRenderer],
	[tableRowRenderer.name, tableRowRenderer],
	[textRenderer.name, textRenderer],
	[thematicBreakRenderer.name, thematicBreakRenderer],
]);

export default renderers;
