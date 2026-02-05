/**
 * "To normalize a label, strip off the opening and closing brackets, perform
 * the Unicode case fold, strip leading and trailing whitespace and collapse
 * consecutive internal whitespace to a single space."
 */

export default function normalizeLabel(text: string): string {
	return text.toLowerCase().toUpperCase().trim().replaceAll(/\s+/g, " ");
}
