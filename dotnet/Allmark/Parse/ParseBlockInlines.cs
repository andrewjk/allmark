namespace Allmark.Parse;

using Allmark.Types;
using System.Text.RegularExpressions;

public static class ParseBlockInlines
{
	public static void Execute(
		MarkdownNode parent,
		Dictionary<string, InlineRule> rules,
		Dictionary<string, LinkReference> refs,
		Dictionary<string, FootnoteReference> footnotes
	)
	{
		if (parent.Type == "html_block")
		{
			return;
		}

		// TODO: These should be done in rules
		if (parent.Type == "code_block")
		{
			// "Blank lines preceding or following a code block are not included in it"
			// "A code block can have all empty lines as its content"
			string content = parent.Content;
			if (Regex.IsMatch(content, @"[^\s]"))
			{
				// HACK: Not sure about this logic:
				content = Regex.Replace(content, @"(^\n\s+\n|\n\s*\n$)", "");
				// TODO: Should be treating EOF as a newline
				if (!content.EndsWith("\n"))
				{
					content += "\n";
				}
			}
			var text = Utils.NewNode("text", false, parent.Index, parent.Line, 1, content, 0, null);
			parent.Children!.Add(text);
			return;
		}
		else if (parent.Type == "code_fence")
		{
			// "Fences can be indented. If the opening fence is indented, content lines will
			// have equivalent opening indentation removed, if present"
			string content = parent.Content;
			if (Regex.IsMatch(content, @"[^\s]"))
			{
				if (parent.Indent > 0)
				{
					content = Regex.Replace(content, @$"(^|\n) {{1,{parent.Indent}}}", "$1");
				}
				// HACK: Not sure about this logic:
				content = Regex.Replace(content, @"^\n\s+\n", "");
				// TODO: Should be treating EOF as a newline
				if (!content.EndsWith("\n"))
				{
					content += "\n";
				}
			}
			var text = Utils.NewNode("text", false, parent.Index, parent.Line, 1, content, 0, null);
			parent.Children!.Add(text);
			return;
		}

		var state = new InlineParserState
		{
			Rules = rules,
			// "Final spaces are stripped before inline parsing"
			Src = parent.Content.TrimEnd(),
			I = 0,
			Line = parent.Line,
			LineStart = 0,
			Indent = 0,
			Delimiters = [],
			Refs = refs,
			Footnotes = footnotes,
		};

		ParseInline.Execute(state, parent);

		// TODO: Do this first so we don't have to check whether it's a block?
		if (parent.Children != null)
		{
			foreach (var child in parent.Children)
			{
				if (child.Block)
				{
					Execute(child, rules, refs, footnotes);
				}
			}
		}
	}
}
