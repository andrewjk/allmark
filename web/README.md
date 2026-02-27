# Allmark for the Web

TypeScript implementation of Allmark, a Markdown parser supporting CommonMark and GitHub Flavored Markdown (GFM).

## Features

- **CommonMark support**: Full compliance with the CommonMark spec
- **GFM extensions**: Tables, task lists, strikethrough, autolinks, footnotes, alerts
- **Extended ruleset**: Additional markdown features
- **CLI tool**: Convert markdown files from the command line
- **Multiple output formats**: HTML or console rendering

## Installation

```bash
pnpm install
```

## Usage

### API

```ts
import { core, extended, gfm, parse, renderHtml } from "allmark";

const markdown = "# Hello, world!";
const document = parse(markdown, gfm, false);
const html = renderHtml(document);
```

### CLI

```bash
# Build the CLI
pnpm build

# Convert markdown to HTML
pnpm cli input.md

# With options
pnpm cli input.md -o output.html -r gfm -f html
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
