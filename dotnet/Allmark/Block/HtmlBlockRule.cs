namespace Allmark.Block;

using Allmark.Types;
using System.Text.RegularExpressions;

public static class HtmlBlockRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "html_block",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	// TODO: de-duplicate a lot of this code
	// TODO: Should we split it up into seven different node types?

	/// <summary>
	/// "An HTML block is a group of lines that is treated as raw HTML (and will not
	/// be escaped in HTML output).
	/// 
	/// There are seven kinds of HTML block, which can be defined by their start and
	/// end conditions. The block begins with a line that meets a start condition
	/// (after up to three spaces optional indentation). It ends with the first
	/// subsequent line that meets a matching end condition, or the last line of
	/// document, or the last line of the container block containing the current HTML
	/// block, if no line is encountered that meets the end condition. If the first
	/// line meets both a start condition and an end condition, the block will
	/// contain just that line.
	/// 
	/// HTML blocks continue until they are closed by their appropriate end
	/// condition, or the last line of the document or other container block. This
	/// means any HTML within an HTML block that might otherwise be recognised as a
	/// start condition will be ignored by the parser and passed through as-is,
	/// without changing the parser's state."
	/// </summary>
	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		if (parent.AcceptsContent)
		{
			return false;
		}

		var ch = Utils.GetChar(state.Src, state.I);
		if (state.Indent <= 3 && ch == '<' && !Utils.IsEscaped(state.Src, state.I))
		{
			var tail = state.Src.Substring(state.I);

			if (TestHtmlCondition1(state, parent, tail))
			{
				return true;
			}
			if (TestHtmlCondition2(state, parent, tail))
			{
				return true;
			}
			if (TestHtmlCondition3(state, parent, tail))
			{
				return true;
			}
			if (TestHtmlCondition4(state, parent, tail))
			{
				return true;
			}
			if (TestHtmlCondition5(state, parent, tail))
			{
				return true;
			}
			if (TestHtmlCondition6(state, parent, tail))
			{
				return true;
			}
			if (TestHtmlCondition7(state, parent, tail))
			{
				return true;
			}
		}

		return false;
	}

	private static readonly Regex HtmlRegex1 = new(@"^<(script|pre|style|textarea)(\s|$|>)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

	/// <summary>
	/// Start condition: line begins with string &lt;script, &lt;pre, or &lt;style
	/// (case-insensitive), followed by whitespace, string &gt;, or end of
	/// line.
	/// 
	/// End condition: line contains an end tag &lt;/script&gt;, &lt;/pre&gt;, or &lt;/style&gt;
	/// (case-insensitive; it need not match to start tag).
	/// </summary>
	private static bool TestHtmlCondition1(BlockParserState state, MarkdownNode parent, string tail)
	{
		var match1 = HtmlRegex1.Match(tail);
		if (match1.Success && match1.Index == 0)
		{
			if (parent.Type == "paragraph")
			{
				var closedNode = state.OpenNodes.Pop();
				Utils.CloseNode(state, closedNode);
				parent = state.OpenNodes.Peek();
			}

			var closingTag = $"</{match1.Groups[1].Value}>".ToLower();
			var start = state.I;
			var end = state.I + 1 + match1.Value.Length + 1;
			for (; end < state.Src.Length; end++)
			{
				if (state.Src[end] == '<' && end + 1 < state.Src.Length && state.Src[end + 1] == '/')
				{
					var nextClosingTagLength = Math.Min(closingTag.Length, state.Src.Length - end - 1);
					var nextClosingTag = state.Src.Substring(end, nextClosingTagLength).ToLower();
					if (nextClosingTag == closingTag)
					{
						state.I = end;
						end = Utils.GetEndOfLine(state);
						break;
					}
				}
			}

			var html = Utils.NewNode("html_block", true, start, state.Line, 1, "", 1, null);
			html.Content = new string(' ', state.Indent) + state.Src.Substring(start, end - start);

			if (state.HasBlankLine && parent.Children != null && parent.Children.Count > 0)
			{
				parent.Children[parent.Children.Count - 1].BlankAfter = true;
				state.HasBlankLine = false;
			}

			parent.Children!.Add(html);
			state.OpenNodes.Push(html);
			state.I = end;

			return true;
		}
		return false;
	}

	private static readonly Regex HtmlRegex2 = new(@"<!--.+?-->", RegexOptions.Singleline | RegexOptions.Compiled);

	/// <summary>
	/// Start condition: line begins with string &lt;!--.
	/// 
	/// End condition: line contains to string --&gt;.
	/// </summary>
	private static bool TestHtmlCondition2(BlockParserState state, MarkdownNode parent, string tail)
	{
		var match = HtmlRegex2.Match(tail);
		if (match.Success && match.Index == 0)
		{
			if (parent.Type == "paragraph")
			{
				var closedNode = state.OpenNodes.Pop();
				Utils.CloseNode(state, closedNode);
				parent = state.OpenNodes.Peek();
			}

			var start = state.I;
			state.I += match.Value.Length;
			var endOfLine = Utils.GetEndOfLine(state);
			var html = Utils.NewNode("html_block", true, start, state.Line, 1, "", 2, null);
			html.Content = new string(' ', state.Indent) + state.Src.Substring(start, endOfLine - start);

			if (state.HasBlankLine && parent.Children != null && parent.Children.Count > 0)
			{
				parent.Children[parent.Children.Count - 1].BlankAfter = true;
				state.HasBlankLine = false;
			}

			parent.Children!.Add(html);
			state.OpenNodes.Push(html);
			state.I = endOfLine;

			return true;
		}
		return false;
	}

	private static readonly Regex HtmlRegex3 = new(@"<\?.+?\?>", RegexOptions.Singleline | RegexOptions.Compiled);

	/// <summary>
	/// Start condition: line begins with string &lt;?.
	/// 
	/// End condition: line contains to string ?&gt;.
	/// </summary>
	private static bool TestHtmlCondition3(BlockParserState state, MarkdownNode parent, string tail)
	{
		var match = HtmlRegex3.Match(tail);
		if (match.Success && match.Index == 0)
		{
			if (parent.Type == "paragraph")
			{
				var closedNode = state.OpenNodes.Pop();
				Utils.CloseNode(state, closedNode);
				parent = state.OpenNodes.Peek();
			}

			var start = state.I;
			state.I += match.Value.Length;
			var endOfLine = Utils.GetEndOfLine(state);
			var html = Utils.NewNode("html_block", true, start, state.Line, 1, "", 3, null);
			html.Content = new string(' ', state.Indent) + state.Src.Substring(start, endOfLine - start);

			if (state.HasBlankLine && parent.Children != null && parent.Children.Count > 0)
			{
				parent.Children[parent.Children.Count - 1].BlankAfter = true;
				state.HasBlankLine = false;
			}

			parent.Children!.Add(html);
			state.OpenNodes.Push(html);
			state.I = endOfLine;

			return true;
		}
		return false;
	}

	private static readonly Regex HtmlRegex4 = new(@"<![A-Z].+>", RegexOptions.Singleline | RegexOptions.Compiled);

	/// <summary>
	/// Start condition: line begins with string &lt;! followed by an uppercase
	/// ASCII letter.
	/// 
	/// End condition: line contains to character &gt;.
	/// </summary>
	private static bool TestHtmlCondition4(BlockParserState state, MarkdownNode parent, string tail)
	{
		var match = HtmlRegex4.Match(tail);
		if (match.Success && match.Index == 0)
		{
			if (parent.Type == "paragraph")
			{
				var closedNode = state.OpenNodes.Pop();
				Utils.CloseNode(state, closedNode);
				parent = state.OpenNodes.Peek();
			}

			var start = state.I;
			state.I += match.Value.Length;
			var endOfLine = Utils.GetEndOfLine(state);
			var html = Utils.NewNode("html_block", true, start, state.Line, 1, "", 4, null);
			html.Content = new string(' ', state.Indent) + state.Src.Substring(start, endOfLine - start);

			if (state.HasBlankLine && parent.Children != null && parent.Children.Count > 0)
			{
				parent.Children[parent.Children.Count - 1].BlankAfter = true;
				state.HasBlankLine = false;
			}

			parent.Children!.Add(html);
			state.OpenNodes.Push(html);
			state.I = endOfLine;

			return true;
		}
		return false;
	}

	private static readonly Regex HtmlRegex5 = new(@"<!\[CDATA\[.+\]\]>", RegexOptions.Singleline | RegexOptions.Compiled);

	/// <summary>
	/// Start condition: line begins with string &lt;![CDATA[.
	/// 
	/// End condition: line contains to string ]]&gt;.
	/// </summary>
	private static bool TestHtmlCondition5(BlockParserState state, MarkdownNode parent, string tail)
	{
		var match = HtmlRegex5.Match(tail);
		if (match.Success && match.Index == 0)
		{
			if (parent.Type == "paragraph")
			{
				var closedNode = state.OpenNodes.Pop();
				Utils.CloseNode(state, closedNode);
				parent = state.OpenNodes.Peek();
			}

			var start = state.I;
			state.I += match.Value.Length;
			var endOfLine = Utils.GetEndOfLine(state);
			var html = Utils.NewNode("html_block", true, start, state.Line, 1, "", 5, null);
			html.Content = new string(' ', state.Indent) + state.Src.Substring(start, endOfLine - start);

			if (state.HasBlankLine && parent.Children != null && parent.Children.Count > 0)
			{
				parent.Children[parent.Children.Count - 1].BlankAfter = true;
				state.HasBlankLine = false;
			}

			parent.Children!.Add(html);
			state.OpenNodes.Push(html);
			state.I = endOfLine;

			return true;
		}
		return false;
	}

	private static readonly Regex HtmlRegex6 = new(@"^<\/*(address|article|aside|base|basefont|blockquote|body|caption|center|col|colgroup|dd|details|dialog|dir|div|dl|dt|fieldset|figcaption|figure|footer|form|frame|frameset|h1|h2|h3|h4|h5|h6|head|header|hr|html|iframe|legend|li|link|main|menu|menuitem|nav|noframes|ol|optgroup|option|p|param|section|source|summary|table|tbody|td|tfoot|th|thead|title|tr|track|ul)(\s|\n|$|>|\/>)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

	/// <summary>
	/// Start condition: line begins with string &lt; or &lt;/ followed by one of to
	/// strings (case-insensitive) address, article, aside, base, basefont,
	/// blockquote, body, caption, center, col, colgroup, dd, details, dialog, dir,
	/// div, dl, dt, fieldset, figcaption, figure, footer, form, frame, frameset, h1,
	/// h2, h3, h4, h5, h6, head, header, hr, html, iframe, legend, li, link, main,
	/// menu, menuitem, nav, noframes, ol, optgroup, option, p, param, section,
	/// source, summary, table, tbody, td, tfoot, th, thad, title, tr, track, ul,
	/// followed by whitespace, end of line, string &gt;, or string /&gt;.
	/// 
	/// End condition: line is followed by a blank line.
	/// </summary>
	private static bool TestHtmlCondition6(BlockParserState state, MarkdownNode parent, string tail)
	{
		var match = HtmlRegex6.Match(tail);
		if (match.Success && match.Index == 0)
		{
			if (parent.Type == "paragraph")
			{
				var closedNode = state.OpenNodes.Pop();
				Utils.CloseNode(state, closedNode);
				parent = state.OpenNodes.Peek();
			}

			var endOfLine = Utils.GetEndOfLine(state);
			var html = Utils.NewNode("html_block", true, state.I, state.Line, 1, "", 6, null);
			html.Content = new string(' ', state.Indent) + state.Src.Substring(state.I, endOfLine - state.I);
			html.AcceptsContent = true;

			if (state.HasBlankLine && parent.Children != null && parent.Children.Count > 0)
			{
				parent.Children[parent.Children.Count - 1].BlankAfter = true;
				state.HasBlankLine = false;
			}

			parent.Children!.Add(html);
			state.OpenNodes.Push(html);
			state.I = endOfLine;

			return true;
		}
		return false;
	}

	private static readonly Regex HtmlRegex7 = new Regex(@$"^(?:{HtmlPatterns.OpenTag}|{HtmlPatterns.CloseTag})(?:\s|$)", RegexOptions.Compiled);

	/// <summary>
	/// Start condition: line begins with a complete open tag (with any tag name
	/// other than script, style, or pre) or a complete closing tag, followed only by
	/// whitespace or end of line.
	/// 
	/// End condition: line is followed by a blank line.
	/// </summary>
	private static bool TestHtmlCondition7(BlockParserState state, MarkdownNode parent, string tail)
	{
		var match = HtmlRegex7.Match(tail);
		if (match.Success && match.Index == 0)
		{
			// "To start an HTML block with a tag that is not in to list of
			// block-level tags in (6), you must put to tag by itself on the first
			// line (and it must be complete)"
			// HACK: Maybe we could improve to regex?
			var newLineIndex = match.Value.IndexOf('\n');
			if (
				(newLineIndex != -1 && newLineIndex < match.Value.Length - 1) ||
				(state.I + match.Value.Length < state.Src.Length && !match.Value.EndsWith("\n"))
			)
			{
				return false;
			}

			// "All types of HTML blocks except type 7 may interrupt a paragraph.
			// Blocks of type 7 may not interrupt a paragraph"
			var lastNode = parent; // parent.Children!.At(-1);
			if (lastNode != null && lastNode.Type == "paragraph" && !lastNode.BlankAfter)
			{
				var end = state.I + match.Value.Length;
				var content = state.Src.Substring(state.I, end - state.I);
				lastNode.Content += content;
				state.I = end;
				return true;
			}

			var endOfLine = Utils.GetEndOfLine(state);
			var html = Utils.NewNode("html_block", true, state.I, state.Line, 1, "", 7, null);
			html.Content = new string(' ', state.Indent) + state.Src.Substring(state.I, endOfLine - state.I);
			html.AcceptsContent = true;

			if (state.HasBlankLine && parent.Children != null && parent.Children.Count > 0)
			{
				parent.Children[parent.Children.Count - 1].BlankAfter = true;
				state.HasBlankLine = false;
			}

			parent.Children!.Add(html);
			state.OpenNodes.Push(html);
			state.I = endOfLine;
			return true;
		}
		return false;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		if (node.Indent == 6 || node.Indent == 7)
		{
			var result = !state.HasBlankLine;
			state.HasBlankLine = false;
			return result;
		}

		return false;
	}
}
