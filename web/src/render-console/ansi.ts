export const ANSI = {
	reset: "\x1b[0m",
	bold: "\x1b[1m",
	dim: "\x1b[2m",
	gray: "\x1b[90m",
	red: "\x1b[31m",
	green: "\x1b[32m",
	yellow: "\x1b[33m",
	blue: "\x1b[34m",
	magenta: "\x1b[35m",
	cyan: "\x1b[36m",
	orange: "\x1b[38;5;208m",
	underline: "\x1b[4m",
};

export type Styles = Record<string, string>;

export function createStyles(ansi: typeof ANSI): Styles {
	return {
		heading1: `${ansi.bold}${ansi.cyan}`,
		heading2: `${ansi.bold}${ansi.blue}`,
		heading3: `${ansi.bold}${ansi.magenta}`,
		heading4: `${ansi.bold}`,
		heading5: `${ansi.dim}${ansi.bold}`,
		heading6: `${ansi.dim}${ansi.bold}`,
		strong: `${ansi.bold}${ansi.orange}`,
		emphasis: ansi.yellow,
		code: ansi.green,
		link: `${ansi.blue}${ansi.underline ?? ""}`,
		blockQuote: ansi.gray,
		codeBlock: ansi.dim,
		thematicBreak: ansi.dim,
		alertNote: `${ansi.blue}`,
		alertTip: `${ansi.green}`,
		alertImportant: `${ansi.magenta}`,
		alertWarning: `${ansi.yellow}`,
		alertCaution: `${ansi.red}`,
		reset: ansi.reset,
	};
}
