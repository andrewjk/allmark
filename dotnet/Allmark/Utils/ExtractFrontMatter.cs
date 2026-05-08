namespace Allmark;

using Allmark.Types;
using System.Text.RegularExpressions;

public static partial class Utils
{
    private const char DASH = '-';
    private static readonly Regex OpeningPattern = new Regex(@"^---\s*\r?\n");

    public static string? ExtractFrontMatter(MarkdownNode document, string src, int index)
    {
        string? frontmatter = null;

        if (src[index] == DASH && OpeningPattern.IsMatch(src.Substring(index)))
        {
            int contentEnd = -1;
            for (int j = index + 3; j < src.Length; j++)
            {
                if (src[j] == DASH && OpeningPattern.IsMatch(src.Substring(j)))
                {
                    contentEnd = src.Length;
                    for (int k = j + 3; k < src.Length; k++)
                    {
                        if (src[k] == '\n')
                        {
                            contentEnd = k;
                            break;
                        }
                    }
                }
            }
            if (contentEnd != -1)
            {
                frontmatter = src.Substring(index, contentEnd - index);
                int i = contentEnd;
                document.Line = src.Substring(0, i).Count(c => c == '\n') + 1;
            }
        }

        return frontmatter;
    }
}
