
function LinkPI(owner){this.owner=owner;this.a_id="Properties_a_id_"+this.owner.name;this.a_href="Properties_a_href_"+this.owner.name;this.a_target="Properties_a_target_"+this.owner.name;this.a_title="Properties_a_title_"+this.owner.name;this.a_href_browse="Properties_a_href_browse_"+this.owner.name;var idx=this.owner.toolbar.indexOfName('insert_file');if(idx<0){document.getElementById(this.a_href_browse).style.visibility="hidden";}};function LinkPI_apply(propName,propValue){switch(propName){case'id':if(propValue){this.owner.inspectedNode.setAttribute('id',propValue);}else{this.owner.inspectedNode.removeAttribute('id');}
break;case'target':this.target_changed(propValue);break;case'title':this.title_changed(propValue);break;case'href':this.href_changed(encodeURI(propValue));break;}
if(this.owner.inspectedNode){this.owner.inspectedNode.removeAttribute('name');}
try{fixFocusHack();}
catch(e){}};function LinkPI_inspect(){var inspected=this.owner.inspectedNode;util_safeSetFieldValue(this.a_id,inspected.getAttribute("id"));if(this.owner.flags.link_pi_next_focus_href){window.focus();document.getElementById(this.a_href).focus();}
var href='';if(is.ie){var m=inspected.outerHTML.match(/\shref='?"?([^\s'"]*)/i);if(m){href=m[1];}}else{href=inspected.getAttribute("href");}
var propValue=util_removeSiteNameFromHREF(this.owner,decodeURI(href));util_safeSetFieldValue(this.a_href,propValue?propValue:'');util_safeSetFieldValue(this.a_title,inspected.getAttribute("title"));var curr_target=inspected.getAttribute("target");if(/_blank|_top|_self|_parent/i.test(curr_target)){utility.dom.selectOption(document.getElementById(this.a_target),curr_target);}else if(curr_target){utility.dom.selectOption(document.getElementById(this.a_target),"custom");}else{utility.dom.selectOption(document.getElementById(this.a_target),"");}
if(this.owner.flags.link_pi_next_focus_href){window.focus();this.owner.flags.link_pi_next_focus_href=false;document.getElementById(this.a_href).focus();}};function LinkPI_target_changed(propValue){var inspected=this.owner.inspectedNode;if(propValue!=''&&propValue!='false'){if(propValue=='custom'){propValue=prompt(translate("Custom frame:",this.owner.config.UILanguage),"");}
if(propValue){propValue=String(propValue);propValue=propValue.replace(/\x27/g,"\x5c\x27");inspected.setAttribute('target',propValue);}}else{if(inspected){inspected.removeAttribute('target');}}};function LinkPI_title_changed(propValue){var inspected=this.owner.inspectedNode;if(propValue!=''&&propValue!='false'){propValue=String(propValue);propValue=propValue.replace(/'/g,"\\'");inspected.setAttribute("title",propValue);}else{inspected.removeAttribute('title');}};function LinkPI_href_changed(propValue){var inspected=this.owner.inspectedNode;if(propValue!=''&&propValue!='false'){propValue=String(propValue);propValue=propValue.replace(/'/g,"\\'");inspected.setAttribute("href",propValue);}else{if(this.owner.savedSelection){this.owner.util_restoreSelection();}else{this.owner.cw.focus();}
if(is.mozilla){var sel=this.owner.cw.getSelection();sel.collapseToEnd();var rng=sel.getRangeAt(0);var rngo=rng.cloneRange();try{rng.selectNode(inspected);}catch(err){}}
this.owner.edit.execCommand(KtmlGetCommand("unlink"),false,false);if(is.mozilla){rngo.select();}
this.owner.displayChanged();}
if(this.owner.undo){this.owner.undo.addEdit();}};LinkPI.prototype.apply=LinkPI_apply;LinkPI.prototype.inspect=LinkPI_inspect;LinkPI.prototype.target_changed=LinkPI_target_changed;LinkPI.prototype.title_changed=LinkPI_title_changed;LinkPI.prototype.href_changed=LinkPI_href_changed;window.KtmlPIObjects["a"]=LinkPI;