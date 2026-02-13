# AGENTS.md

Guidelines for AI coding agents working in the allmark repository.

## Project Overview

Allmark is a TypeScript Markdown parser library supporting CommonMark and GitHub Flavored Markdown (GFM).

- **Language**: TypeScript (ES modules)
- **Package Manager**: pnpm (v10.x)
- **Location**: `/web` directory contains the main library

## Build/Lint/Test Commands

Run from `/web` directory:

```bash
pnpm check              # TypeScript + oxlint
pnpm build              # Type check + bundle with tsdown
pnpm test               # Run all tests (watch mode)
pnpm test -- --run      # Run tests once (CI mode)
pnpm test -- testName   # Run single test by name pattern
pnpm format             # Format with Prettier
```

## Code Style Guidelines

### TypeScript Configuration
- **Target**: ES2022 with ESNext modules
- **Strict mode**: Enabled with full strictNullChecks
- **Isolated modules/declarations**: Required
- Use `erasableSyntaxOnly` - no TypeScript-specific runtime features

### Import/Export Patterns

```typescript
// Type imports must use explicit `import type`
import type BlockParserState from "../types/BlockParserState";

// Regular imports for runtime values
import closeNode from "../utils/closeNode";

// Default exports for modules
const rule: BlockRule = { name: "heading", testStart, testContinue };
export default rule;
```

### Import Order (Prettier-enforced)
1. Parent imports (`^[../]`)
2. Local imports (`^[./]`)

### Naming Conventions
- **Files**: camelCase (e.g., `testHeading.ts`)
- **Types/Interfaces**: PascalCase (e.g., `MarkdownNode`)
- **Functions**: camelCase (e.g., `testStart`)

### Formatting (Prettier)
- **Indentation**: Tabs
- **Print width**: 100
- **Trailing commas**: Always (except JSON)
- **Semicolons**: Required
- **Quotes**: Double quotes

### Type Patterns
- Use `interface` for object types (not `type` aliases)
- Explicit return types on exported functions
- Use optional chaining (`?.`) and nullish coalescing (`??`)
- Non-null assertions (`!`) acceptable when safe

### Error Handling
- Use early returns for guard clauses
- No try/catch for parsing logic - return false/undefined instead

## Architecture

- `/block` - Block parsing rules
- `/inline` - Inline parsing rules  
- `/parse` - Core parsing logic
- `/render` - HTML rendering functions
- `/types` - TypeScript interfaces
- `/utils` - Helper functions
- `/rules` - Rule sets (`core.ts`, `gfm.ts`, `extended.ts`)
- `/test` - Vitest test files (located at `/web/test/`)

## Testing Patterns

```typescript
import { expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import core from "../src/rulesets/core";

test("description", () => {
    const root = parse(input, core, false);
    const html = renderHtml(root, core.renderers);
    expect(html).toBe(expected);
});
```

---

## Swift Project

Swift port located in `/swift` directory.

### Swift Commands

Run from `/swift`:

```bash
swift build                    # Build package
swift test                     # Run tests
swift test --filter testName   # Run single test
swift-format --in-place --recursive Sources/ Tests/
```

### Swift Code Style

- **Files/Protocols**: PascalCase (e.g., `BlockRule.swift`)
- **Functions/Properties**: camelCase
- TypeScript `interface` → Swift `protocol`
- TypeScript `Map<string, T>` → Swift `[String: T]`
- Optional protocol methods → Extensions with default implementations
- **Formatting**: Tabs, 100 char line length, `///` docs

### Swift Example

```swift
import Foundation

protocol BlockRule {
    var name: String { get }
    func testStart(state: BlockParserState, parent: MarkdownNode) -> Bool
}

extension BlockRule {
    func closeNode(state: BlockParserState, parent: MarkdownNode) {
        // Default: no cleanup
    }
}
```

### Swift Testing

Uses Swift Testing framework (not XCTest):

```swift
import Testing
@testable import allmark

@Test func example() async throws {
    #expect(actual == expected)
}
```

---

## .NET Project

.NET port located in `/dotnet` directory.

### .NET Commands

Run from `/dotnet`:

```bash
dotnet build                    # Build solution
dotnet test                     # Run tests
dotnet test --filter "BasicParse"  # Run single test by name
dotnet format Allmark.sln       # Format code
```

### .NET Code Style

- **Language**: C# (.NET 10.0)
- **Types/Records**: PascalCase (e.g., `BlockRule`, `MarkdownNode`, `InlineRule`)
- **Classes/Methods**: PascalCase (e.g., `ParagraphRule.Create()`, `Parse.Execute()`)
- **Properties**: PascalCase (e.g., `Name`, `TestStart`)
- **Local variables**: camelCase
- TypeScript `interface` → C# `record` or `record class`
- TypeScript `Map<string, T>` → C# `Dictionary<string, T>`
- **Formatting**: Tabs, `///` XML docs
- **Null reference types**: Enabled (`Nullable enable`)
- **Implicit usings**: Enabled

### .NET Example

```csharp
namespace Allmark.Block;

using Allmark.Types;
using static Allmark.Utils.NewNode;

public static class ParagraphRule
{
    public static BlockRule Create()
    {
        return new BlockRule
        {
            Name = "paragraph",
            TestStart = TestStart,
            TestContinue = TestContinue,
        };
    }

    private static bool TestStart(BlockParserState state, MarkdownNode parent)
    {
        if (parent.AcceptsContent)
        {
            return false;
        }

        var paragraph = Utils.NewNode("paragraph", true, state.I, state.Line, 1, "", 0, []);
        state.OpenNodes.Push(paragraph);

        return true;
    }
}
```

### .NET Testing

Uses MSTest framework:

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Allmark;

namespace Allmark.Tests;

[TestClass]
public class ParserTests
{
    [TestMethod]
    public void BasicParse()
    {
        var root = Parse.Execute(input, Core.RuleSet, false);
        var html = RenderHtml.Execute(root, Core.RuleSet.Renderers);

        Assert.AreEqual(expected, html);
    }
}
```
