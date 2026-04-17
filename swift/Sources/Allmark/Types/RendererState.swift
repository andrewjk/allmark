import Foundation
import OrderedCollections

/// State maintained during rendering.
public struct RendererState {
	/// The renderers being used.
	public var renderers: OrderedDictionary<String, Renderer>
	/// The accumulated output string.
	public var output: String
	/// Footnote nodes to render at the end.
	public var footnotes: [MarkdownNode]
	/// Current nesting depth.
	public var depth: Int
	/// Current quote nesting depth.
	public var quoteDepth: Int
	/// Current list nesting depth.
	public var listDepth: Int

	public init(renderers: OrderedDictionary<String, Renderer>, output: String, footnotes: [MarkdownNode], depth: Int, quoteDepth: Int, listDepth: Int = 0) {
		self.renderers = renderers
		self.output = output
		self.footnotes = footnotes
		self.depth = depth
		self.quoteDepth = quoteDepth
		self.listDepth = listDepth
	}
}
