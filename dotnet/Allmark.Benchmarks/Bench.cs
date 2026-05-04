using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using Perfolizer.Horology;
using System.Reflection;
using Allmark;
using Allmark.Rulesets;

namespace Allmark.Benchmarks;

[MemoryDiagnoser]
[SimpleJob(warmupCount: 3, iterationCount: 10)]
public class Bench
{
    private static readonly string Markdown = LoadMarkdown();

    private static string LoadMarkdown()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var resourceName = "Allmark.Benchmarks.full-markdown.md";

        using var stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null)
        {
            throw new FileNotFoundException($"Could not find embedded resource: {resourceName}");
        }

        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    [Benchmark]
    public void BenchMarkdownToHtmlWithGfm()
    {
        var doc = Parser.Execute(Markdown, Gfm.RuleSet);
        var html = Renderer.Execute(doc, HtmlRenderers.Renderers);

        Consume(html);
    }

    private static void Consume<T>(T value)
    {
        _ = value;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        var config = DefaultConfig.Instance
            .AddDiagnoser(MemoryDiagnoser.Default)
            .AddColumn(StatisticColumn.Median)
            .AddColumn(StatisticColumn.Min)
            .AddColumn(StatisticColumn.Max)
            .AddColumn(StatisticColumn.StdDev)
            .WithOptions(ConfigOptions.DisableOptimizationsValidator)
            .WithSummaryStyle(SummaryStyle.Default.WithTimeUnit(TimeUnit.Millisecond));

        BenchmarkRunner.Run<Bench>(config);
    }
}
