namespace Allmark.Types;

/// <summary>
/// Defines a renderer for converting markdown nodes to output format.
/// </summary>
public record Renderer
{
	public required string Name { get; init; }
	public required Action<MarkdownNode, RendererState, bool?, bool?, bool?> Render { get; init; }
}
