import type Renderer from "../types/Renderer";
import { render } from "./renderHeading";

const renderer: Renderer = {
	name: "html_underline",
	render,
};
export default renderer;
