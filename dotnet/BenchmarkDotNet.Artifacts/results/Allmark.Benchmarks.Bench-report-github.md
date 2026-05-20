```

BenchmarkDotNet v0.14.0, macOS 26.5 (25F71) [Darwin 25.5.0]
Apple M2, 1 CPU, 8 logical and 8 physical cores
.NET SDK 10.0.102
  [Host]     : .NET 10.0.2 (10.0.225.61305), Arm64 RyuJIT AdvSIMD
  Job-COIKSE : .NET 10.0.2 (10.0.225.61305), Arm64 RyuJIT AdvSIMD

IterationCount=10  WarmupCount=3  

```
| Method                     | Mean      | Error     | StdDev    | Median    | Min       | Max       | Gen0     | Gen1     | Allocated |
|--------------------------- |----------:|----------:|----------:|----------:|----------:|----------:|---------:|---------:|----------:|
| BenchMarkdownToHtmlWithGfm | 0.6913 ms | 0.0513 ms | 0.0340 ms | 0.6888 ms | 0.6435 ms | 0.7485 ms | 589.8438 | 204.1016 |   4.72 MB |
