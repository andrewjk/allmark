export default function isSpace(code: number): boolean {
	switch (code) {
		case 0x09: // \t
		case 0x0a: // \n
		case 0x0b: // \v
		case 0x0c: // \f
		case 0x0d: // \r
		case 0x20: // space
			return true;
		default:
			return false;
	}
}
