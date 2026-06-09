namespace Allmark;

using Allmark.Rulesets;
using Allmark.Types;

public static class Transformer
{
    public static string Execute(string src, RuleSet rules, OutputRenderer[] renderers, RenderOptions? options = null)
    {
        var doc = Parser.Execute(src, rules);
        return Renderer.Execute(doc, renderers, options);
    }
}
