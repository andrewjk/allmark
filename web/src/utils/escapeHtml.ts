const HTML_ESCAPES: Record<string, string> = {
	"&": "&amp;",
	"<": "&lt;",
	">": "&gt;",
	'"': "&quot;",
};

export default function escapeHtml(text: string): string {
	// Fast path: nothing to escape, return the original string
	if (!/[&<>"]/.test(text)) {
		return text;
	}
	return text.replace(/[&<>"]/g, (char) => {
		return HTML_ESCAPES[char];
	});
}
