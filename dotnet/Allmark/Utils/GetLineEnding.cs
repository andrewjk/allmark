namespace Allmark;

using Allmark.Types;

public static partial class Utils
{
    public static string GetLineEnding(BlockParserState state, int endOfLine)
    {
        return GetChar(state.Src, endOfLine) == '\n'
            ? "\n"
            : GetChar(state.Src, endOfLine) == '\r'
                ? GetChar(state.Src, endOfLine + 1) == '\n'
                    ? "\r\n"
                    : "\r"
                : "";
    }
}
