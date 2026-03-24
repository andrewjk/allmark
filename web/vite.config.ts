import { type UserConfig, defineConfig } from "vite-plus";

export default defineConfig({
	fmt: {
		ignorePatterns: ["/dist"],
		useTabs: true,
		printWidth: 100,
		sortImports: {},
		trailingComma: "all",
		overrides: [
			{
				files: ["*.json", "*.jsonc"],
				options: {
					trailingComma: "none",
				},
			},
		],
	},
	lint: {
		options: {
			typeAware: true,
			typeCheck: true,
		},
		rules: {
			"no-control-regex": 0,
		},
	},
	pack: {
		entry: ["src/index.ts", "src/bin/index.ts"],
	},
}) satisfies UserConfig as UserConfig;
