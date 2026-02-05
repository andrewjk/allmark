import { existsSync, promises as fs } from "node:fs";
import path from "node:path";

let baseurl = "https://raw.githubusercontent.com/";

let tableUrl = `${baseurl}wldtyp/starlist.dev/refs/heads/main/table.md`;
let tableFolder = "./test/github-top";
if (!existsSync(tableFolder)) {
	await fs.mkdir(tableFolder);
}
let tableFile = path.join(tableFolder, "table.md");
await getit(tableUrl, tableFile);
let table = await fs.readFile(tableFile, "utf8");

for (let line of table.split("\n")) {
	let name = line.split("|")[5];
	if (name && name.includes("/")) {
		name = name.trim();

		let mdFile = `./test/github-top/${name.replaceAll("/", "-")}-readme.md`;
		if (!existsSync(mdFile)) {
			let url = `${baseurl}${name}/refs/heads/main/README.md`;
			if (!(await getit(url, mdFile))) {
				url = `${baseurl}${name}/refs/heads/main/README`;
				if (!(await getit(url, mdFile))) {
					url = `${baseurl}${name}/refs/heads/master/README.md`;
					if (!(await getit(url, mdFile))) {
						url = `${baseurl}${name}/refs/heads/master/README`;
						if (!(await getit(url, mdFile))) {
							console.log(`NOT FOUND: '${name}'`);
						}
					}
				}
			}
		}
		/*
		if (existsSync(mdFile)) {
			let md = await fs.readFile(mdFile, "utf8");

			let gfmHtml = await gfm.renderHtml(md);
			let gfmHtmlFile = path.join(
				path.dirname(mdFile),
				path.basename(mdFile, ".md") + "-gfm.html",
			);
			await fs.writeFile(gfmHtmlFile, gfmHtml);

			let html = renderHtml(parse(md, false));
			let htmlFile = path.join(path.dirname(mdFile), path.basename(mdFile, ".md") + ".html");
			await fs.writeFile(htmlFile, html);
		}
		*/
	}
}

async function getit(url: string, file: string) {
	try {
		let response = await fetch(url);
		if (response.status !== 200) {
			response = await fetch(url.toLowerCase());
		}
		if (response.status === 200) {
			console.log(`found: '${url}'`);

			let text = await response.text();
			await fs.writeFile(file, text);
			await sleep(1000);

			return true;
		}
	} catch {
		// Eh
	}

	return false;
}

function sleep(ms: number) {
	return new Promise((resolve) => {
		setTimeout(resolve, ms);
	});
}
