# Allmark for the Web

A full-featured Markdown parser for the web.

## Features

- **CommonMark support**: Almost full compliance with the CommonMark spec
- **GFM extensions**: Tables, task lists, strikethrough, autolinks, footnotes, alerts
- **Extended ruleset**: Additional markdown features (comments, insertions, deletions, etc)
- **CLI tool**: Convert markdown files from the command line
- **Multiple output formats**: HTML or console rendering

## Installation

```bash
pnpm add allmark
```

## Usage

### API

The simple approach:

```ts
import { consoleRenderers, core, extended, gfm, htmlRenderers, transform } from "allmark";

const markdown = "# Hello, world!";
const html = transform(markdown, gfm, htmlRenderers);
```

For more control:

```ts
import { consoleRenderers, core, extended, gfm, htmlRenderers, parse, render } from "allmark";

const markdown = "# Hello, world!";
const document = parse(markdown, gfm);
const html = render(document, htmlRenderers);
```

### CLI

```bash
# Install globally
pnpm add -g allmark

# Convert markdown to HTML
allmark input.md

# With options
allmark input.md -o output.html -r gfm -f html
```

CLI options:

- `-o, --output`: Output file path (defaults to stdout)
- `-r, --ruleset`: Ruleset to use (core, gfm, extended; default: extended)
- `-f, --format`: Output format (html, console; default: html)
- `-h, --help`: Show help message

## Rulesets

| Ruleset    | Description                           |
| ---------- | ------------------------------------- |
| `core`     | CommonMark spec only                  |
| `gfm`      | CommonMark + GitHub Flavored Markdown |
| `extended` | GFM + additional features             |

## Development

```bash
pnpm check        # TypeScript type checking + linting
pnpm build        # Type check + bundle
pnpm test         # Run tests (watch mode)
pnpm format       # Format with Prettier
```
