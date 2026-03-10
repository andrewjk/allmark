namespace Allmark;

using Allmark.Rulesets;
using Allmark.Types;

public static class Transformer
{
    public static string Execute(string src, RuleSet rules, Dictionary<string, OutputRenderer> renderers)
    {
        var doc = Parser.Execute(src, rules);
        return Renderer.Execute(doc, renderers);
    }
}
