export function isAlpha(code: number): boolean {
	return (code > 64 && code < 91) || (code > 96 && code < 123);
}

export function isNumeric(code: number): boolean {
	return code > 47 && code < 58;
}

export function isAlphaNumeric(code: number): boolean {
	return isAlpha(code) || isNumeric(code);
}
