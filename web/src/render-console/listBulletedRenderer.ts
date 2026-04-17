import type Renderer from "../types/Renderer";
import render from "./listRenderer";

const renderer: Renderer = {
	name: "list_bulleted",
	render: (node, state) => render(node, state, false),
};
export default renderer;
