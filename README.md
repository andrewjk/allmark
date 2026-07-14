# Allmark

A full-featured Markdown parser for web, desktop and mobile.

## Features

- **Multiple implementations**: TypeScript (for the web), Swift (for Mac and iOS), C# (for Windows), Zig (for embedding)
- **Compatible**: supports CommonMark and/or Github Flavored Markdown (GFM) with some extra rules included too
- **Extensible**: add your custom block and inline rules to the default collections
- **Multiple outputs**: render to HTML or console, with an extensible render system if you need other targets
- **Fast enough**: faster than [micromark](https://github.com/micromark/micromark), slower than [markdown-it](https://github.com/markdown-it/markdown-it)
- **CLI tool**: transform Markdown to HTML or display it directly in the console

## Getting Started

> [!WARNING]  
> Make sure to sanitize the HTML output that is obtained from Allmark.

### Web/Node

Use your package manager to install Allmark:

```bash
npm install allmark
```

And then call `transform` with the ruleset and renderers you want to use:

```javascript
import { transform, extended, htmlRenderers } from "allmark";

const input = `
# Allmark
Hello from *Markdown*
`;
const html = transform(input, extended, htmlRenderers);
```

### Swift

The Swift version of Allmark can be installed from https://github.com/andrewjk/allmark-swift.

From Xcode, select `File` > `Add package dependencies` and follow the instructions.

Or add the following to your Package.swift file:

```swift
dependencies: [
    .package(url: "https://github.com/andrewjk/allmark-swift", from: "1.0.16")
]
```

And then call `transform` with the ruleset and renderers you want to use:

```swift
import Allmark

let input = """
            # Allmark
            Hello from *Markdown*
            """
let html = _transform(src: input, rules: extendedRuleSet, renderers: htmlRenderers)
```

### .NET/C#

Install Allmark from NuGet: https://www.nuget.org/packages/Allmark/

And then call `Transformer.Execute` with the ruleset and renderers you want to use:

```c#
using Allmark;
using Allmark.Rulesets;

var input = @"
# Allmark
Hello from *Markdown*
";
var html = Transformer.Execute(input, Extended.RuleSet, HtmlRenderers.Renderers);
```

### Zig

The Zig version of Allmark can be installed from https://github.com/andrewjk/allmark-zig.

TODO: Zig package and instructions

And then call `transform` with the ruleset and renderers you want to use:

```zig
const transform = @import("allmark").transform;
const extended = @import("allmark").extended;
const htmlRenderers = @import("allmark").htmlRenderers;

const input =
    \\# Allmark
    \\Hello from *Markdown*
    ;
const gpa = std.testing.allocator; // Your allocator
const rules = try extended.init(gpa);
defer extended.deinit(&rules, gpa);
const renderers = try htmlRenderers.init(gpa);
defer htmlRenderers.deinit(&renderers, gpa);

const html = try transform(gpa, input, rules, renderers, null);
defer gpa.free(html);
```

## Compatibility

The [CommonMark spec](https://spec.commonmark.org/0.31.2/) passes with two exceptions (`---\n---` and `*foo [bar* baz]`).

The [GFM spec](https://github.github.com/gfm/) passes with the same two exceptions as CommonMark, plus an exception for disallowed tags (you should properly sanitize HTML generated through Allmark).

Allmark departs from the spec in requiring two underlines for a heading, to prevent rich text editing jumping around when adding a list under a paragraph.

The C# version fails the specs for the input `[ẞ]\n\n[SS]: /url` due to a lack of Unicode folding.

The Zig version fails the specs for the inputs `[ΑΓΩ]: /φου\\n\\n[αγω]`, `[ẞ]\n\n[SS]: /url` and `[Толпой][Толпой] is a Russian word.\\n\\n[ТОЛПОЙ]: /url` due to a lack of extended Unicode support.

## Rulesets and Rendering

Allmark ships with the following rules included.

### Core

- Block
  - Quote
  - Indented code
  - Fenced code
  - Heading
  - Heading (with underline)
  - HTML block
  - Bulleted list
  - Numbered list
  - Thematic break
  - Autolink
- Inline
  - Code span
  - Emphasis (bold and italic)
  - Hard break
  - HTML span
  - Links (with references)

### GFM

Everything in core, plus:

- Block
  - Alert
  - Footnote (with references)
  - Task item list
- Inline
  - Extended autolink
  - Strikethrough

### Extended

Everything in GFM, plus:

- Inline
  - Highlight
  - Subscript
  - Superscript
  - Critic marks
    - Insertion
    - Deletion
    - Comment

## Extensibility

In TypeScript, you can add rules by implementing the `BlockRule` or `InlineRule` interfaces and add renderers by implementing the `Renderer` interface. Then add your custom rules or renderers to the standard rule or renderer collections, or create entirely new collections.

## Benchmark

The following benchmark results are from running the `bench` script from the bench folder on my laptop:

| Package       | Min (ms) | Median (ms) | Mean (ms) | Max (ms) | Std Dev (ms) | Samples |
| ------------- | -------- | ----------- | --------- | -------- | ------------ | ------- |
| 'allmark'     | '0.75'   | '0.77'      | '0.79'    | '1.86'   | '0.06'       | 1263    |
| 'markdown-it' | '0.50'   | '0.52'      | '0.53'    | '1.06'   | '0.04'       | 1901    |
| 'micromark'   | '5.12'   | '5.48'      | '6.06'    | '11.92'  | '1.21'       | 166     |
| 'cmark-gfm'   | '0.19'   | '0.20'      | '0.21'    | '1.27'   | '0.02'       | 4845    |

## CLI

```bash
# Install globally
pnpm add -g allmark

# Display markdown in the console
allmark input.md

# Write markdown to a HTML file
allmark input.md -o output.html
```

Options:

- `-o, --output`: Output file path (defaults to stdout)
- `-r, --ruleset`: Ruleset to use (core, gfm, extended; default: extended)
- `-f, --format`: Output format (html, console; default: console)
- `-h, --help`: Show help message

## Demo

There's a barebones demo app included in this repository. To run it:

```bash
git clone https://github.com/andrewjk/allmark
cd allmark/demo
pnpm install
pnpm dev
```
