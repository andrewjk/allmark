import type Renderer from "../types/Renderer";
import { render } from "./renderCodeBlock";

const renderer: Renderer = {
	name: "code_fence",
	render,
};
export default renderer;
