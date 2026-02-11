namespace Allmark;

public static partial class Utils
{
	public static bool IsPunctuation(int code)
	{
		switch (code)
		{
			case 0x21: // !
			case 0x22: // "
			case 0x23: // #
			case 0x24: // $
			case 0x25: // %
			case 0x26: // &
			case 0x27: // '
			case 0x28: // (
			case 0x29: // )
			case 0x2a: // *
			case 0x2b: // +
			case 0x2c: // ,
			case 0x2d: // -
			case 0x2e: // .
			case 0x2f: // /
			case 0x3a: // :
			case 0x3b: // ;
			case 0x3c: // <
			case 0x3d: // =
			case 0x3e: // >
			case 0x3f: // ?
			case 0x40: // @
			case 0x5b: // [
			case 0x5c: // \
			case 0x5d: // ]
			case 0x5e: // ^
			case 0x5f: // _
			case 0x60: // `
			case 0x7b: // {
			case 0x7c: // |
			case 0x7d: // }
			case 0x7e: // ~
				return true;
			default:
				return false;
		}
	}
}
