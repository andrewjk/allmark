import type Renderer from "../types/Renderer";
import { render } from "./codeBlockRenderer";

const renderer: Renderer = {
	name: "code_fence",
	render,
};
export default renderer;
