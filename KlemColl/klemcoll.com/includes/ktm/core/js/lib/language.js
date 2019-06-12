// Copyright 2001-2005 Interakt Online. All rights reserved.

function lang_translatepage(content, lang, wnd) {
	var regexp_repl = new RegExp("\\|\\|([^\\|\\|]*)\\|\\|", "gi");

	while ((arr = regexp_repl.exec(content)) != null){
		if (translated = wnd.translate(arr[1], lang)) {
			var repl = new RegExp("\\|\\|"+arr[1]+"\\|\\|", "gi");
			if(typeof content.replace != 'function') {
				return content.toString();
			}
			try {
				content = content.replace(repl, translated);
			} catch (e) { }
		}
	}
	return content;
}
