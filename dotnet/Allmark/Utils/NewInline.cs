namespace Allmark;

using Allmark.Types;

public static partial class Utils
{
    public static MarkdownNode NewInline(string type, int index, int line, string markup, int indent)
    {
        return new MarkdownNode
        {
            Type = type,
            Block = false,
            Index = index,
            Length = 0,
            Line = line,
            Markup = markup,
            Delimiter = "",
            Content = "",
            Indent = indent,
            Subindent = 0,
            AcceptsContent = false,
            MaybeContinuing = false,
            BlankAfter = false,
            Loose = false,
            Children = null
        };
    }
}
