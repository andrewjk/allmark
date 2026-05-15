import alertRenderer from "../render-console/alertRenderer";
import blockQuoteRenderer from "../render-console/blockQuoteRenderer";
import codeBlockRenderer from "../render-console/codeBlockRenderer";
import codeFenceRenderer from "../render-console/codeFenceRenderer";
import codeSpanRenderer from "../render-console/codeSpanRenderer";
import commentRenderer from "../render-console/commentRenderer";
import deletionRenderer from "../render-console/deletionRenderer";
import emphasisRenderer from "../render-console/emphasisRenderer";
import footnoteListRenderer from "../render-console/footnoteListRenderer";
import footnoteRefRenderer from "../render-console/footnoteRefRenderer";
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
import streamMarkRenderer from "../render-console/streamMarkRenderer";
import strikethroughRenderer from "../render-console/strikethroughRenderer";
import strongRenderer from "../render-console/strongRenderer";
//import subscriptRenderer from "../render-console/subscriptRenderer";
//import superscriptRenderer from "../render-console/superscriptRenderer";
import tableRenderer from "../render-console/tableRenderer";
import textRenderer from "../render-console/textRenderer";
import thematicBreakRenderer from "../render-console/thematicBreakRenderer";
import type Renderer from "../types/Renderer";

const renderers: Renderer[] = [
	alertRenderer,
	blockQuoteRenderer,
	codeBlockRenderer,
	codeFenceRenderer,
	codeSpanRenderer,
	commentRenderer,
	deletionRenderer,
	emphasisRenderer,
	footnoteRenderer,
	footnoteRefRenderer,
	footnoteListRenderer,
	hardBreakRenderer,
	headingRenderer,
	headingUnderlineRenderer,
	highlightRenderer,
	htmlBlockRenderer,
	htmlSpanRenderer,
	imageRenderer,
	insertionRenderer,
	linkRenderer,
	listBulletedRenderer,
	listOrderedRenderer,
	listTaskItemRenderer,
	paragraphRenderer,
	streamMarkRenderer,
	strikethroughRenderer,
	strongRenderer,
	//subscriptRenderer,
	//superscriptRenderer,
	tableRenderer,
	textRenderer,
	thematicBreakRenderer,
];

export default renderers;
