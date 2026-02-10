import type Renderer from "../types/Renderer";
import renderList from "./renderList";

const renderer: Renderer = {
	name: "list_ordered",
	render: renderList,
};
export default renderer;
