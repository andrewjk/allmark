namespace Allmark;

using Allmark.Types;

public static partial class Utils
{
    public static MarkdownNode NewText(int index, int line, string content, int indent)
    {
        return new MarkdownNode
        {
            Type = "text",
            Block = false,
            Index = index,
            Length = 0,
            Line = line,
            Markup = "",
            Delimiter = "",
            Content = content,
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
