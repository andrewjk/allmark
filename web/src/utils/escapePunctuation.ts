// TODO: Is this faster or slower than escapeBackslashes?
export default function escapePunctuation(text: string): string {
	// Fast path: no backslashes means nothing to unescape
	if (!text.includes("\\")) {
		return text;
	}
	return text.replaceAll(/\\([!"#$%&'()*+,-./:;<=>?@[\\\]^_`{|}~])/g, "$1");
}
