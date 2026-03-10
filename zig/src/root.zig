//! By convention, root.zig is the root source file when making a package.
const std = @import("std");
const Io = std.Io;

pub const parse = struct {
    pub const execute = @import("parse/parse.zig").parse;
    pub const parseBlock = @import("parse/parseBlock.zig").parseBlock;
    pub const parseBlockInlines = @import("parse/parseBlockInlines.zig").parseBlockInlines;
    pub const parseIndent = @import("parse/parseIndent.zig").parseIndent;
    pub const parseInline = @import("parse/parseInline.zig").parseInline;
    pub const parseLine = @import("parse/parseLine.zig").parseLine;
};

pub const render = @import("render.zig").render;
pub const transform = @import("transform.zig").transform;
pub const renderHtml = struct {
    pub const textRenderer = @import("render/textRenderer.zig").textRenderer;
    pub const emphasisRenderer = @import("render/emphasisRenderer.zig").emphasisRenderer;
    pub const strongRenderer = @import("render/strongRenderer.zig").strongRenderer;
    pub const deletionRenderer = @import("render/deletionRenderer.zig").deletionRenderer;
    pub const insertionRenderer = @import("render/insertionRenderer.zig").insertionRenderer;
    pub const blockQuoteRenderer = @import("render/blockQuoteRenderer.zig").blockQuoteRenderer;
    pub const codeBlockRenderer = @import("render/codeBlockRenderer.zig").codeBlockRenderer;
    pub const codeFenceRenderer = @import("render/codeFenceRenderer.zig").codeFenceRenderer;
    pub const codeSpanRenderer = @import("render/codeSpanRenderer.zig").codeSpanRenderer;
    pub const hardBreakRenderer = @import("render/hardBreakRenderer.zig").hardBreakRenderer;
    pub const htmlBlockRenderer = @import("render/htmlBlockRenderer.zig").htmlBlockRenderer;
    pub const htmlSpanRenderer = @import("render/htmlSpanRenderer.zig").htmlSpanRenderer;
    pub const linkRenderer = @import("render/linkRenderer.zig").linkRenderer;
    pub const imageRenderer = @import("render/imageRenderer.zig").imageRenderer;
    pub const alertRenderer = @import("render/alertRenderer.zig").alertRenderer;
    pub const paragraphRenderer = @import("render/paragraphRenderer.zig").paragraphRenderer;
    pub const headingRenderer = @import("render/headingRenderer.zig").headingRenderer;
    pub const headingUnderlineRenderer = @import("render/headingUnderlineRenderer.zig").headingUnderlineRenderer;
    pub const tableCellRenderer = @import("render/tableCellRenderer.zig").tableCellRenderer;
    pub const tableHeaderRenderer = @import("render/tableHeaderRenderer.zig").tableHeaderRenderer;
    pub const tableRowRenderer = @import("render/tableRowRenderer.zig").tableRowRenderer;
    pub const tableRenderer = @import("render/tableRenderer.zig").tableRenderer;
    pub const footnoteRenderer = @import("render/footnoteRenderer.zig").footnoteRenderer;
    pub const footnoteListRenderer = @import("render/footnoteListRenderer.zig").renderFootnoteList;
    pub const listTaskItemRenderer = @import("render/listTaskItemRenderer.zig").listTaskItemRenderer;
    pub const listRenderer = @import("render/listRenderer.zig").listBulletedRenderer;
    pub const listOrderedRenderer = @import("render/listOrderedRenderer.zig").listOrderedRenderer;
    pub const thematicBreakRenderer = @import("render/thematicBreakRenderer.zig").thematicBreakRenderer;
    pub const subscriptRenderer = @import("render/subscriptRenderer.zig").subscriptRenderer;
    pub const superscriptRenderer = @import("render/superscriptRenderer.zig").superscriptRenderer;
    pub const highlightRenderer = @import("render/highlightRenderer.zig").highlightRenderer;
    pub const strikethroughRenderer = @import("render/strikethroughRenderer.zig").strikethroughRenderer;
    pub const renderNode = @import("render/renderNode.zig").renderNode;
    pub const renderChildren = @import("render/renderChildren.zig").renderChildren;
    pub const renderTag = @import("render/renderTag.zig").renderTag;
    pub const renderSelfClosedTag = @import("render/renderSelfClosedTag.zig").renderSelfClosedTag;
    pub const renderUtils = @import("render/renderUtils.zig").renderUtils;
};

pub const core = @import("rulesets/core.zig");
pub const gfm = @import("rulesets/gfm.zig");
pub const extended = @import("rulesets/extended.zig");
pub const RuleSet = @import("types/RuleSet.zig").RuleSet;
