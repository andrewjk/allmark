import Foundation

@MainActor
let headingUnderlineRenderer = Renderer(
	name: "html_underline",
	render: renderHeading
)
