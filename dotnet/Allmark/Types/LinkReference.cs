namespace Allmark.Types;

/// <summary>
/// Represents a link reference definition in markdown.
/// </summary>
public record LinkReference
{
	public required string Url { get; init; }
	public required string Title { get; init; }
}
