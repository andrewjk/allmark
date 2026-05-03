namespace Allmark.Types;

using System.Text;

/// <summary>
/// Represents state during rendering.
/// </summary>
public record RendererState
{
    public required Dictionary<string, OutputRenderer> RenderersMap { get; set; }

    public required StringBuilder Output { get; set; }
    public required List<MarkdownNode> Footnotes { get; set; }
    public required Dictionary<string, MarkdownNode> FootnoteRefs { get; set; }
    public required int ListDepth { get; set; }
}
