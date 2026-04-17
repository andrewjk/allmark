import type Renderer from "../types/Renderer";
import render from "./listRenderer";

const renderer: Renderer = {
	name: "list_ordered",
	render: (node, state) => render(node, state, true),
};
export default renderer;
