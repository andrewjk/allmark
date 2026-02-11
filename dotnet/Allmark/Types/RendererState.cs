namespace Allmark.Types;

using System.Text;

/// <summary>
/// Represents the state during rendering.
/// </summary>
public record RendererState
{
	public required Dictionary<string, Renderer> Renderers { get; set; }

	public required StringBuilder Output { get; set; }
	public required List<MarkdownNode> Footnotes { get; set; }
}
