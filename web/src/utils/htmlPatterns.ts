// A tag name consists of an ASCII letter followed by zero or more ASCII
// letters, digits, or hyphens (-).
const TAG_NAME = "[a-zA-Z][a-zA-Z0-9-]*";

// An attribute name consists of an ASCII letter, _, or :, followed by zero or
// more ASCII letters, digits, _, ., :, or -. (Note: This is the XML
// specification restricted to ASCII. HTML5 is laxer.)
const ATTRIBUTE_NAME = "[a-zA-Z_:][a-zA-Z0-9_.:-]*";

// An unquoted attribute value is a nonempty string of characters not including
// whitespace, ", ', =, <, >, or `.
const UNQUOTED_VALUE = "[^\\s\"'=<>`]+";

// A single-quoted attribute value consists of ', zero or more characters not
// including ', and a final '.
const SINGLE_QUOTED_VALUE = "'[^']+'";

// A double-quoted attribute value consists of ", zero or more characters not
// including ", and a final ".
const DOUBLE_QUOTED_VALUE = '"[^"]+"';

// An attribute value consists of an unquoted attribute value, a single-quoted
// attribute value, or a double-quoted attribute value.
const ATTRIBUTE_VALUE = `(?:${UNQUOTED_VALUE}|${SINGLE_QUOTED_VALUE}|${DOUBLE_QUOTED_VALUE})`;

// An attribute value specification consists of optional whitespace, a =
// character, optional whitespace, and an attribute value.
const ATTRIBUTE_VALUE_SPEC = `\\s*=\\s*(?:${ATTRIBUTE_VALUE})`;

// An attribute consists of whitespace, an attribute name, and an optional
// attribute value specification.
const ATTRIBUTE = `\\s(?:${ATTRIBUTE_NAME})(?:${ATTRIBUTE_VALUE_SPEC})*`;

// An open tag consists of a < character, a tag name, zero or more attributes,
// optional whitespace, an optional / character, and a > character.
const OPEN_TAG: string = `<(?:${TAG_NAME})(?:${ATTRIBUTE})*\\s*/*>`;

// A closing tag consists of the string </, a tag name, optional whitespace, and
// the character >.
const CLOSE_TAG: string = `</(?:${TAG_NAME})*\\s*>`;

// An HTML comment consists of <!-- + text + -->, where text does not start with
// > or ->, does not end with -, and does not contain --. (See the HTML5 spec.)
const COMMENT = "<!---?>|<!--(?:[^-]|-[^-]|--[^>])*-->";

// A processing instruction consists of the string <?, a string of characters
// not including the string ?>, and the string ?>.
const INSTRUCTION = "<\\?[^\\?]*\\?>";

// A declaration consists of the string <!, a name consisting of one or more
// uppercase ASCII letters, whitespace, a string of characters not including the
// character >, and the character >.
const DECLARATION = "<![A-Z]+\\s+[^>]+>";

// A CDATA section consists of the string <![CDATA[, a string of characters not
// including the string ]]>, and the string ]]>.
const CDATA = "<!\\[CDATA\\[.*?\\]\\]>";

export { OPEN_TAG, CLOSE_TAG, COMMENT, INSTRUCTION, DECLARATION, CDATA };
