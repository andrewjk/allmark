import type Renderer from "../types/Renderer";
import renderList from "./renderList";

const renderer: Renderer = {
	name: "list_bulleted",
	render: renderList,
};
export default renderer;
