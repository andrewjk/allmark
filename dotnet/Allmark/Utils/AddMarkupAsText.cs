namespace Allmark;

using Allmark.Types;

public static partial class Utils
{
	public static void AddMarkupAsText(string markup, InlineParserState state, MarkdownNode parent)
	{
		var lastNode = parent.Children!.LastOrDefault();
		var haveText = lastNode != null && lastNode.Type == "text";
		var text = haveText ? lastNode! : NewNode("text", false, state.I, state.Line, 1, "", 0, null);
		text.Markup += markup;
		if (!haveText)
		{
			parent.Children ??= new List<MarkdownNode>();
			parent.Children.Add(text);
		}
		state.I += markup.Length;
	}
}
