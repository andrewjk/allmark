namespace Allmark.Block;

using Allmark.Types;

public static class CodeFenceRule
{
	public static BlockRule Create()
	{
		return new BlockRule
		{
			Name = "code_fence",
			TestStart = TestStart,
			TestContinue = TestContinue,
		};
	}

	private static bool TestStart(BlockParserState state, MarkdownNode parent)
	{
		// A fenced code block can't be started in a block that accepts content
		if (parent.AcceptsContent)
		{
			return false;
		}

		var ch = Utils.GetChar(state.Src, state.I);
		if (state.Indent <= 3 && (ch == '`' || ch == '~'))
		{
			var matched = 1;
			var end = state.I + 1;
			var haveSpace = false;
			for (; end < state.Src.Length; end++)
			{
				var nextChar = state.Src[end];
				if (nextChar == ch)
				{
					if (haveSpace)
					{
						return false;
					}
					matched++;
				}
				else if (Utils.IsNewLine(nextChar))
				{
					break;
				}
				else if (Utils.IsSpace(nextChar))
				{
					haveSpace = true;
				}
				else
				{
					break;
				}
			}

			if (matched >= 3)
			{
				MarkdownNode? closedNode = null;

				var markup = new string(ch, matched);

				var info = "";
				if (state.I + matched < state.Src.Length && !Utils.IsNewLine(state.Src[state.I + matched]))
				{
					end = Utils.GetEndOfLine(state);
					info = state.Src.Substring(state.I + matched, end - (state.I + matched));

					// "Info strings for backtick code blocks cannot contain backticks"
					// But "Info strings for tilde code blocks can contain backticks and tildes"?!
					// TODO: What if it's escaped?
					if (ch == '`' && info.Contains('`'))
					{
						return false;
					}

					info = Utils.DecodeEntities(info);
					info = Utils.EscapeBackslashes(info);
				}
				else
				{
					// TODO: I think the \n should go into the content and then it
					// can be rendered without getting fancy about calculating where
					// newlines go?
					end++;
				}

				if (state.MaybeContinue)
				{
					state.MaybeContinue = false;
					for (var i = 0; i < state.OpenNodes.Count - 1; i++)
					{
						var node = state.OpenNodes.ElementAt(i);
						if (node.MaybeContinuing)
						{
							node.MaybeContinuing = false;
							closedNode = node;
							// Pop until we reach this node
							var newLength = state.OpenNodes.Count - i - 1;
							while (state.OpenNodes.Count > newLength)
							{
								state.OpenNodes.Pop();
							}
							break;
						}
					}
					parent = state.OpenNodes.Peek();
				}

				// If there's an open paragraph, close it
				// TODO: Is there a better way to do this??
				if (parent.Type == "paragraph")
				{
					closedNode = state.OpenNodes.Pop();
					parent = state.OpenNodes.Peek();
				}

				if (closedNode != null)
				{
					Utils.CloseNode(state, closedNode);
				}

				var code = Utils.NewNode("code_fence", true, state.I, state.Line, 1, markup, state.Indent, []);
				code.AcceptsContent = true;
				code.Info = info;

				state.I = end;

				if (state.HasBlankLine && parent.Children!.Count > 0)
				{
					parent.Children[^1].BlankAfter = true;
					state.HasBlankLine = false;
				}

				parent.Children!.Add(code);
				state.OpenNodes.Push(code);

				return true;
			}
		}

		return false;
	}

	private static bool TestContinue(BlockParserState state, MarkdownNode node)
	{
		if (state.HasBlankLine)
		{
			node.Content += new string(' ', state.Indent);
			return true;
		}

		var ch = Utils.GetChar(state.Src, state.I);
		if (state.Indent <= 3 && (ch == '`' || ch == '~'))
		{
			// This might be a closing fence, and if so, we can close the block
			if (node.Markup.StartsWith(ch.ToString()))
			{
				var endMatched = 0;
				var end = state.I;
				for (; end < state.Src.Length; end++)
				{
					var nextChar = state.Src[end];
					if (nextChar == ch)
					{
						endMatched++;
					}
					else
					{
						break;
					}
				}

				if (endMatched >= node.Markup.Length)
				{
					// "Closing code fences cannot have info strings"
					for (; end < state.Src.Length; end++)
					{
						var nextChar = state.Src[end];
						if (Utils.IsNewLine(nextChar))
						{
							break;
						}
						else if (Utils.IsSpace(nextChar))
						{
							continue;
						}
						else
						{
							return true;
						}
					}

					state.I = end;
					return false;
				}
			}
		}

		return true;
	}
}
