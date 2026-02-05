// TODO: Is this faster or slower than escapeBackslashes?
export default function escapePunctuation(text: string): string {
	return text.replaceAll(/\\([!"#$%&'()*+,-./:;<=>?@[\\\]^_`{|}~])/g, "$1");
}
