import type Renderer from "../types/Renderer";
import { render } from "./headingRenderer";

const renderer: Renderer = {
	name: "heading_underline",
	render,
};
export default renderer;
