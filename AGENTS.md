# AGENTS.md

Guidelines for AI coding agents working in the allmark repository.

## Project Overview

Allmark is a Markdown parser library supporting CommonMark and GitHub Flavored Markdown (GFM).

Multi-language implementation:

- **TypeScript** (main): `/web` directory
- **Swift**: `/swift` directory
- **.NET**: `/dotnet` directory
- **Zig**: `/zig` directory

## Build/Lint/Test Commands

### TypeScript (`/web`)

```bash
pnpm check              # TypeScript + oxlint
pnpm build              # Type check + bundle with tsdown
pnpm test               # Run all tests (watch mode)
pnpm test -- --run      # Run tests once (CI mode)
pnpm test -- testName   # Run single test by name pattern
pnpm format             # Format with Prettier
pnpm cli <input.md>     # Run CLI to convert MD to HTML
pnpm bench              # Run Allmark-only benchmarks
```

### TypeScript Comparative Benchmarks (`/bench`)

```bash
pnpm bench              # Run comparative benchmarks (Allmark vs markdown-it, micromark, cmark-gfm)
```

### Swift (`/swift`)

```bash
swift build                     # Build package
swift test                      # Run tests
swift test --filter testName    # Run single test
swift test -c release --filter Bench.benchMarkdownToHtmlWithGfm  # Run benchmark in release mode
swift-format --in-place --recursive Sources/ Tests/
```

### .NET (`/dotnet`)

```bash
dotnet build                    # Build solution
dotnet test                     # Run tests
dotnet test --filter "BasicParse"  # Run single test by name
dotnet format Allmark.sln       # Format code
dotnet run --project Allmark.Benchmarks/Allmark.Benchmarks.csproj  # Run benchmarks
```

### Zig (`/zig`)

```bash
zig build                       # Build
zig build test                  # Run all tests (no output means all tests succeeded)
zig test test/parser_test.zig   # Run single test file
zig build bench -Doptimize=ReleaseFast  # Run benchmarks in release mode
```

## TypeScript Code Style

- Target: ES2022 with ESNext modules, strict mode, `erasableSyntaxOnly`
- Type imports: `import type BlockParserState from "../types/BlockParserState";`
- Runtime imports: `import closeNode from "../utils/closeNode";`
- Default exports: `const rule: BlockRule = { name: "heading", testStart, testContinue }; export default rule;`
- Import order: Parent (`^[../]`) then local (`^[./]`)
- **Files**: camelCase, **Types**: PascalCase, **Functions**: camelCase
- Formatting: Tabs, 100 width, trailing commas: always, semicolons: required, double quotes
- Use `interface` for objects, explicit returns on exports, `?.`/`??` operators, `!` safe
- Error handling: early returns, no try/catch for parsing (return false/undefined)

## Swift Code Style

- **Files/Protocols**: PascalCase, **Functions/Properties**: camelCase
- TypeScript `interface` → Swift `struct`, `Map<string, T>` → `[String: T]`
- Closures with `inout` require `@MainActor`
- Formatting: Tabs, 100 chars, `///` docs

```swift
struct BlockRule {
    var name: String
    var testStart: @MainActor (inout BlockParserState, MarkdownNode) -> Bool
    var testContinue: @MainActor (inout BlockParserState, MarkdownNode) -> Bool
    var closeNode: @MainActor (inout BlockParserState, MarkdownNode) -> Void
}
```

## .NET Code Style

- **Language**: C# (.NET 10.0), **Types**: PascalCase, **Methods**: PascalCase, **Locals**: camelCase
- TypeScript `interface` → C# `record`, `Map<string, T>` → `Dictionary<string, T>`
- Formatting: Tabs, `///` XML docs, nullable enabled, implicit usings enabled

```csharp
public static class ParagraphRule
{
    public static BlockRule Create()
    {
        return new BlockRule { Name = "paragraph", TestStart = TestStart };
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent)
    {
        if (parent.AcceptsContent) { return false; }
        return true;
    }
}
```

## Zig Code Style

- **Files**: camelCase, **Types**: PascalCase, **Functions**: camelCase
- TypeScript `interface` → Zig `struct`, `Map<string, T>` → `std.StringHashMap(T)`
- Use `pub const` for module-level exports

```zig
pub const BlockRule = struct {
    name: []const u8,
    testStart: *const fn (state: *BlockParserState, parent: *MarkdownNode) bool,
    testContinue: *const fn (state: *BlockParserState, parent: *MarkdownNode) bool,
    closeNode: ?*const fn (state: *BlockParserState, parent: *MarkdownNode) void = null,
};
```

## Architecture

All implementations share similar structure:

- `/block` - Block parsing rules
- `/inline` - Inline parsing rules
- `/parse` - Core parsing logic
- `/render` - HTML rendering functions
- `/types` - Type definitions
- `/utils` - Helper functions
- `/rulesets` - Rule sets (`core`, `gfm`, `extended`)

## Testing Patterns

### TypeScript (Vitest)

```typescript
import { expect, test } from "vitest";
import parse from "../src/parse";
import render from "../src/render";
import core from "../src/rulesets/core";
import htmlRenderers from "../src/rulesets/htmlRenderers";

test("description", () => {
	const root = parse(input, core);
	const html = render(root, htmlRenderers);
	expect(html).toBe(expected);
});
```

### Swift (Swift Testing)

```swift
import Testing
@testable import allmark

@Test func example() async throws {
    let root = parse(src: input, rules: coreRuleSet, debug: false)
    let html = renderHtml(doc: root, renderers: coreRuleSet.renderers)
    #expect(actual == expected)
}
```

### .NET (MSTest)

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark;

[TestMethod]
public void BasicParse()
{
    var root = Parse.Execute(input, Core.RuleSet, false);
    var html = RenderHtml.Execute(root, Core.RuleSet.Renderers);
    Assert.AreEqual(expected, html);
}
```

### Zig

```zig
test "basic parse" {
    const root = parse(allocator, input, coreRuleSet, false);
    const html = renderHtml(root, coreRuleSet.renderers);
    try std.testing.expectEqualStrings(expected, html);
}
```

## Test Generation Infrastructure

All tests are generated from spec files in `/web/specs/` using the `splitSpecsIntoTests.ts` script. This ensures test consistency across all language implementations.

### Spec File Format

Spec files use the CommonMark spec format:

`````````````````````````````````
"description"

```````````````````````````````` example
input markdown
.
expected html output
`````````````````````````````````

````

- Tests can be skipped by adding `(skip)` after "example"
- Tab characters are represented as `→` in spec files

### Running Test Generation

```bash
cd web
node --import tsx/esm scripts/splitSpecsIntoTests.ts
````

### Adding New Tests

1. Add test case to appropriate spec file in `/web/specs/`
2. Run the test generation script
3. Verify tests pass in all language implementations
4. Commit both the spec file and generated test files
