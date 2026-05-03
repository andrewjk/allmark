import alertRenderer from "../render/alertRenderer";
import blockQuoteRenderer from "../render/blockQuoteRenderer";
import codeBlockRenderer from "../render/codeBlockRenderer";
import codeFenceRenderer from "../render/codeFenceRenderer";
import codeSpanRenderer from "../render/codeSpanRenderer";
import commentRenderer from "../render/commentRenderer";
import deletionRenderer from "../render/deletionRenderer";
import emphasisRenderer from "../render/emphasisRenderer";
import footnoteListRenderer from "../render/footnoteListRenderer";
import footnoteRefRenderer from "../render/footnoteRefRenderer";
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
import tableRenderer from "../render/tableRenderer";
import textRenderer from "../render/textRenderer";
import thematicBreakRenderer from "../render/thematicBreakRenderer";
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
	footnoteListRenderer,
	footnoteRefRenderer,
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
	strikethroughRenderer,
	strongRenderer,
	subscriptRenderer,
	superscriptRenderer,
	tableRenderer,
	textRenderer,
	thematicBreakRenderer,
];

export default renderers;
