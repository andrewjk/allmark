# AGENTS.md

This file provides guidelines for AI coding agents working in the allmark repository.

## Project Overview

Allmark is a TypeScript Markdown parser library supporting CommonMark and GitHub Flavored Markdown (GFM).

- **Language**: TypeScript (ES modules)
- **Package Manager**: pnpm (v10.x)
- **Location**: `/web` directory contains the main library

## Build/Lint/Test Commands

All commands should be run from the `/web` directory:

```bash
# Type checking and linting
pnpm check              # Run TypeScript checker + oxlint

# Building
pnpm build              # Type check + bundle with tsdown

# Testing
pnpm test               # Run all tests with Vitest
pnpm test -- testName   # Run a single test by name
pnpm test -- --run    # Run tests once (CI mode)

# Utility scripts
pnpm test:split         # Split spec files into individual tests
pnpm test:dl            # Download GitHub top repos for testing

# Formatting
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
import type BlockRule from "../types/BlockRule";

// Regular imports for runtime values
import closeNode from "../utils/closeNode";

// Default exports for modules
const rule: BlockRule = { name: "heading", testStart, testContinue };
export default rule;

// Default export for functions
export default function parse(src: string, rules: RuleSet, debug = false): MarkdownNode { }
```

### Import Order (Prettier-enforced)
1. Parent imports (`^[../]`)
2. Local imports (`^[./]`)

### Naming Conventions
- **Files**: camelCase (e.g., `testHeading.ts`, `renderHtml.ts`)
- **Types/Interfaces**: PascalCase (e.g., `MarkdownNode`, `BlockParserState`)
- **Functions**: camelCase (e.g., `testStart`, `renderNode`)
- **Constants**: camelCase for rule objects

### Formatting (Prettier)
- **Indentation**: Tabs (not spaces)
- **Print width**: 100
- **Trailing commas**: Always (except JSON)
- **Semicolons**: Required
- **Quotes**: Double quotes

### Type Patterns
- Use `interface` for object types (not `type` aliases)
- Explicit return types on exported functions
- Use optional chaining (`?.`) and nullish coalescing (`??`)
- Non-null assertions (`!`) acceptable when safe

### Code Organization
- Each block/inline rule is a separate file with default export
- Rules export an object with `name`, `testStart`, and optionally `testContinue`
- JSDoc comments for public APIs and spec references
- Inline comments explaining Markdown spec compliance

### Error Handling
- TypeScript strict mode catches most issues
- Use early returns for guard clauses
- No try/catch for parsing logic - return false/undefined instead

## Architecture

- `/block` - Block parsing rules (20 files)
- `/inline` - Inline parsing rules (13 files)  
- `/parse` - Core parsing logic
- `/types` - TypeScript interfaces
- `/utils` - Helper functions
- `/rules` - Rule set definitions (`core.ts`, `gfm.ts`)
- `/test` - Vitest test files

## Testing Patterns

```typescript
import { expect, test } from "vitest";
import parse from "../src/parse";
import renderHtml from "../src/renderHtml";
import core from "../src/rules/core";

test("description", () => {
    const input = `# Markdown`;
    const expected = `<h1>Markdown</h1>\n`;
    const root = parse(input, core, false);
    const html = renderHtml(root);
    expect(html).toBe(expected);
});
```
