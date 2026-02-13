const std = @import("std");

pub const TAG_NAME = "[a-zA-Z][a-zA-Z0-9-]*";
pub const ATTRIBUTE_NAME = "[a-zA-Z_:][a-zA-Z0-9_.:-]*";
pub const UNQUOTED_VALUE = "[^\\s\"'=<>`]+";
pub const SINGLE_QUOTED_VALUE = "'[^']+'";
pub const DOUBLE_QUOTED_VALUE = "\"[^\"]+\"";
pub const ATTRIBUTE_VALUE = "(?:" ++ UNQUOTED_VALUE ++ "|" ++ SINGLE_QUOTED_VALUE ++ "|" ++ DOUBLE_QUOTED_VALUE ++ ")";
pub const ATTRIBUTE_VALUE_SPEC = "\\s*=\\s*(?:" ++ ATTRIBUTE_VALUE ++ ")";
pub const ATTRIBUTE = "\\s(?:" ++ ATTRIBUTE_NAME ++ ")(?:" ++ ATTRIBUTE_VALUE_SPEC ++ ")*";
pub const OPEN_TAG = "<(?:" ++ TAG_NAME ++ ")(?:" ++ ATTRIBUTE ++ ")*\\s*/*>";
pub const CLOSE_TAG = "</(?:" ++ TAG_NAME ++ ")*\\s*>";
pub const COMMENT = "<!---?>|<!--(?:[^-]|-[^-]|--[^>])*-->";
pub const INSTRUCTION = "<\\?[^\\?]*\\?>";
pub const DECLARATION = "<![A-Z]+\\s+[^>]+>";
pub const CDATA = "<!\\[CDATA\\[.*?\\]\\]>";
