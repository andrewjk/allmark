import alertRenderer from "../render-console/alertRenderer";
import blockQuoteRenderer from "../render-console/blockQuoteRenderer";
import codeBlockRenderer from "../render-console/codeBlockRenderer";
import codeFenceRenderer from "../render-console/codeFenceRenderer";
import codeSpanRenderer from "../render-console/codeSpanRenderer";
import commentRenderer from "../render-console/commentRenderer";
import deletionRenderer from "../render-console/deletionRenderer";
import emphasisRenderer from "../render-console/emphasisRenderer";
import footnoteRenderer from "../render-console/footnoteRenderer";
import hardBreakRenderer from "../render-console/hardBreakRenderer";
import headingRenderer from "../render-console/headingRenderer";
import headingUnderlineRenderer from "../render-console/headingUnderlineRenderer";
import highlightRenderer from "../render-console/highlightRenderer";
import htmlBlockRenderer from "../render-console/htmlBlockRenderer";
import htmlSpanRenderer from "../render-console/htmlSpanRenderer";
import imageRenderer from "../render-console/imageRenderer";
import insertionRenderer from "../render-console/insertionRenderer";
import linkRenderer from "../render-console/linkRenderer";
import listBulletedRenderer from "../render-console/listBulletedRenderer";
import listOrderedRenderer from "../render-console/listOrderedRenderer";
import listTaskItemRenderer from "../render-console/listTaskItemRenderer";
import paragraphRenderer from "../render-console/paragraphRenderer";
import strikethroughRenderer from "../render-console/strikethroughRenderer";
import strongRenderer from "../render-console/strongRenderer";
//import subscriptRenderer from "../render-console/subscriptRenderer";
//import superscriptRenderer from "../render-console/superscriptRenderer";
import tableRenderer from "../render-console/tableRenderer";
import textRenderer from "../render-console/textRenderer";
import thematicBreakRenderer from "../render-console/thematicBreakRenderer";
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
	//[subscriptRenderer.name, subscriptRenderer],
	//[superscriptRenderer.name, superscriptRenderer],
	[tableRenderer.name, tableRenderer],
	[textRenderer.name, textRenderer],
	[thematicBreakRenderer.name, thematicBreakRenderer],
]);

export default renderers;
