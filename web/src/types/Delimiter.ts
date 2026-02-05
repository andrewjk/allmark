export default interface Delimiter {
	markup: string;
	start: number;
	length: number;
	handled?: boolean;
}
