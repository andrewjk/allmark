import type Renderer from "../types/Renderer";
import { render } from "./headingRenderer";

const renderer: Renderer = {
	name: "html_underline",
	render,
};
export default renderer;
