import Foundation

@MainActor
let consoleAlertRenderer = Renderer(
	name: "alert",
	render: renderConsoleAlert
)

@MainActor
func renderConsoleAlert(_ node: MarkdownNode, _ state: inout RendererState, _ first: Bool?, _ last: Bool?, _ decode: Bool?) {
	let styles = getConsoleStyles()
	let type = node.markup.lowercased()
	let style = styles["alert\(type.capitalized)"] ?? styles["alertNote"] ?? ""
	let icons: [String: String] = [
		"note": "📝",
		"tip": "💡",
		"important": "❗",
		"warning": "⚠️",
		"caution": "🚨",
	]
	let icon = icons[type] ?? icons["note"]
	if !state.output.isEmpty && !state.output.hasSuffix("\n") {
		state.output += "\n"
	}
	state.output += "\(style)\(icon ?? "📝") \(type.capitalized):\(ansiReset)\n"
	renderChildren(node: node, state: &state)
}
