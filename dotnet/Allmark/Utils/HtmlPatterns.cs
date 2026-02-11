namespace Allmark;

public static class HtmlPatterns
{
	// A tag name consists of an ASCII letter followed by zero or more ASCII
	// letters, digits, or hyphens (-).
	private const string TagName = @"[a-zA-Z][a-zA-Z0-9-]*";

	// An attribute name consists of an ASCII letter, _, or :, followed by zero or
	// more ASCII letters, digits, _, ., :, or -. (Note: This is the XML
	// specification restricted to ASCII. HTML5 is laxer.)
	private const string AttributeName = @"[a-zA-Z_:][a-zA-Z0-9_.:-]*";

	// An unquoted attribute value is a nonempty string of characters not including
	// whitespace, ", ', =, <, >, or `.
	private const string UnquotedValue = @"[^\s""'=<>`]+";

	// A single-quoted attribute value consists of ', zero or more characters not
	// including ', and a final '.
	private const string SingleQuotedValue = @"'[^']+'";

	// A double-quoted attribute value consists of ", zero or more characters not
	// including ", and a final ".
	private const string DoubleQuotedValue = @"""[^""]+""";

	// An attribute value consists of an unquoted attribute value, a single-quoted
	// attribute value, or a double-quoted attribute value.
	private const string AttributeValue = @$"(?:{UnquotedValue}|{SingleQuotedValue}|{DoubleQuotedValue})";

	// An attribute value specification consists of optional whitespace, a =
	// character, optional whitespace, and an attribute value.
	private const string AttributeValueSpec = @$"\s*=\s*(?:{AttributeValue})";

	// An attribute consists of whitespace, an attribute name, and an optional
	// attribute value specification.
	private const string Attribute = @$"\s(?:{AttributeName})(?:{AttributeValueSpec})*";

	// An open tag consists of a < character, a tag name, zero or more attributes,
	// optional whitespace, an optional / character, and a > character.
	public const string OpenTag = @$"<(?:{TagName})(?:{Attribute})*\s*/*>";

	// A closing tag consists of the string </, a tag name, optional whitespace, and
	// the character >.
	public const string CloseTag = @$"</(?:{TagName})*\s*>";

	// An HTML comment consists of <!-- + text + -->, where text does not start with
	// > or ->, does not end with -, and does not contain --. (See the HTML5 spec.)
	public const string Comment = @"<!---?>|<!--(?:[^-]|-[^-]|--[^>])*-->";

	// A processing instruction consists of the string <?, a string of characters
	// not including the string ?>, and the string ?>.
	public const string Instruction = @"<\?[^\?]*\?>";

	// A declaration consists of the string <!, a name consisting of one or more
	// uppercase ASCII letters, whitespace, a string of characters not including the
	// character >, and the character >.
	public const string Declaration = @"<![A-Z]+\s+[^>]+>";

	// A CDATA section consists of the string <![CDATA[, a string of characters not
	// including the string ]]>, and the string ]]>. [^ ... ]?
	public const string Cdata = @"<!\[CDATA\[.*?\]\]>";
}
