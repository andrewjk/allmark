namespace Allmark.Render;

using Allmark.Types;
using System.Text.RegularExpressions;

public static class ConsoleListRenderer
{
    public static void Render(MarkdownNode node, RendererState state, bool ordered)
    {
        state.Depth++;

        var loose = IsLooseList(node);

        var counter = 1;
        if (ordered && node.Markup.Length > 0)
        {
            var match = Regex.Match(node.Markup, @"^(\d+)");
            if (match.Success && int.TryParse(match.Groups[1].Value, out int num))
            {
                counter = num;
            }
        }

        if (node.Children != null)
        {
            foreach (var item in node.Children)
            {
                var prefix = ordered ? $"{counter}." : RenderToConsole.ConsoleBullets[Math.Min(state.Depth - 1, RenderToConsole.ConsoleBullets.Length - 1)];
                if (ordered)
                {
                    counter++;
                }

                if (item.Children != null)
                {
                    for (int i = 0; i < item.Children.Count; i++)
                    {
                        var child = item.Children[i];
                        if (!loose && child.Type == "paragraph")
                        {
                            var indent = new string(' ', (state.Depth - 1) * 2);
                            if (i == 0)
                            {
                                state.Output.Append($"{indent}{prefix} ");
                            }
                            RenderChildren.Execute(child, state);
                            state.Output.Append('\n');
                        }
                        else
                        {
                            var indent = new string(' ', (state.Depth - 1) * 2);
                            if (i == 0)
                            {
                                state.Output.Append($"{indent}{prefix} ");
                            }
                            if (state.Renderers.TryGetValue(child.Type, out var renderer))
                            {
                                renderer.Render(child, state, false, false, true);
                            }
                        }
                    }
                }
            }
        }

        state.Depth--;
    }

    private static bool IsLooseList(MarkdownNode node)
    {
        if (node.Children != null)
        {
            for (int i = 0; i < node.Children.Count - 1; i++)
            {
                var child = node.Children[i];
                var grandchild = child.Children?.Count > 0 ? child.Children[child.Children.Count - 1] : null;
                if (grandchild?.BlankAfter == true)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
