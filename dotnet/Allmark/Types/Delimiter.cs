namespace Allmark.Types;

/// <summary>
/// Represents a delimiter found during inline parsing.
/// </summary>
public record Delimiter
{
    public required string Markup { get; set; }
    public required int Start { get; set; }
    public required int Length { get; set; }
    public bool Handled { get; set; }
    /// <summary>
    /// Precedence for delimiter matching (higher = takes precedence).
    /// </summary>
    public int? Precedence { get; set; }
}
