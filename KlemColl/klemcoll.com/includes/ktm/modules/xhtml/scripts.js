
var $IndentChar="  ";var $TagsNoTextContent='^'+['applet','colgroup','map','object','ol','ul','optgroup','select','table','tbody','tfoot','thead','tr'].join("$|^")+'$';var $TagsNoContent='^'+['img','hr','br','wbr','input'].join("$|^")+'$';var $UseAcceptedAttributesList=true;var $AcceptedAttributes="kt_onclick,abbr,accept-charset,accessKey,action,align,alt,archive,axis,background,bgColor,border,borderColor,borderColorDark,borderColorLight,cellPadding,cellSpacing,ch,charset,checked,chOff,cite,class,classid,clear,code,codeBase,codeType,color,cols,colSpan,compact,coords,dir,disabled,enctype,face,for,frame,headers,height,hideFocus,href,hreflang,hspace,id,label,lang,language,maxLength,media,method,multiple,name,noHref,noShade,noWrap,readOnly,rel,rev,rows,rowSpan,rules,scope,shape,size,src,standby,start,style,summary,tabIndex,target,title,type,useMap,vAlign,value,vspace,width,wrap".split(',');var $MaxJsExecutionTime=50*1000;var $StripTagReg='^'+['b','strong','span','font','i','em'].join("$|^")+'$';var NewLineAfterTagsReg='^'+["table","tbody","ul","ol","p"].join("$|^").toLowerCase()+'$';var $FormattingTags={"table":{"NLBeforeStartTag":false,"NLAfterStartTag":true,"NLBeforeEndTag":false,"NLAfterEndTag":true,"indent":false,"SelectContent":false},"tbody":{"NLBeforeStartTag":false,"NLAfterStartTag":true,"NLBeforeEndTag":true,"NLAfterEndTag":true,"indent":false,"SelectContent":false},"tr":{"NLBeforeStartTag":false,"NLAfterStartTag":true,"NLBeforeEndTag":true,"NLAfterEndTag":true,"indent":true,"SelectContent":false},"td":{"NLBeforeStartTag":false,"NLAfterStartTag":false,"NLBeforeEndTag":false,"NLAfterEndTag":true,"indent":true,"SelectContent":true},"ol":{"NLBeforeStartTag":false,"NLAfterStartTag":true,"NLBeforeEndTag":true,"NLAfterEndTag":true,"indent":false,"SelectContent":false},"ul":{"NLBeforeStartTag":false,"NLAfterStartTag":true,"NLBeforeEndTag":true,"NLAfterEndTag":true,"indent":false,"SelectContent":false},"li":{"NLBeforeStartTag":false,"NLAfterStartTag":false,"NLBeforeEndTag":false,"NLAfterEndTag":true,"indent":true,"SelectContent":true},"p":{"NLBeforeStartTag":false,"NLAfterStartTag":false,"NLBeforeEndTag":false,"NLAfterEndTag":true,"indent":false,"SelectContent":true}};function late_setTextareaValue(idx){ktmls[idx].setTextareaValue(ktmls[idx].laterTextareaValue);delete ktmls[idx].laterTextareaValue;};ktml.prototype.setTextareaValue=function(value){value=this.replaceTags(value);value=value.replace(/\xA0/gi,'&nbsp;');value=value.replace(/<t(d|h)([^>]*)>\x20<\/t(d|h)>/gi,"<t$1$2>&nbsp;</t$3>");value=value.replace(/\r\n/g,"\n");value=value.replace(/\n\n/g,"\n");value=value.replace(/\n\n/g,"\n");var match_start=value.match(/<br[^>]*ktml_hidden ktml_selstart[^>]*>/i);var match_end=value.match(/<br[^>]*ktml_hidden ktml_selend[^>]*>/i);var pmatch_start=value.match(/<p[^>]*>\s*<br[^>]*ktml_hidden ktml_selstart[^>]*>\s*<\/p>/i);var pmatch_end=value.match(/<p[^>]*>\s*<br[^>]*ktml_hidden ktml_selend[^>]*>\s*<\/p>/i);if(pmatch_start){match_start=pmatch_start;}
if(pmatch_end){match_end=pmatch_end;}
if(is.ie){this.textarea.value=value;this.textarea.focus();var selectionRange=this.textarea.createTextRange();selectionRange.collapse(true);selectionRange.select();if(match_start&&match_end){var found=selectionRange.findText(match_start[0],1000000000,0);if(found){var steper=selectionRange.duplicate();var delta=1;steper.collapse(false);while(delta){delta=steper.moveEnd("character",1);steper.select();if(steper.text!=""&&!/[\r\t\n]/i.test(steper.text)){steper.moveEnd("character",-1);selectionRange.setEndPoint("EndToStart",steper);break;}
steper.collapse(false);}
selectionRange.select();selectionRange.text="";var s1=selectionRange.duplicate();}
if(found){found=selectionRange.findText(match_end[0],1000000000,0);selectionRange.select();selectionRange.text="";var s2=selectionRange.duplicate();s2.setEndPoint("StartToStart",s1);s2.select();}}}
if(is.mozilla){var match_sel=false;if(!/[\r\n]/.test(value)){match_sel=value.match(/(?:<br[^>]*ktml_hidden ktml_selstart[^>]*>[\r\n\s]*)([\w\W]*?)(?:[\r\n\s]*<br[^>]*ktml_hidden ktml_selend[^>]*>)/i);}
var sel_start=0;var sel_end=0;if(pmatch_start){sel_start=value.indexOf(pmatch_start[0]);value=value.replace(/[\s\n]*<p[^>]*>\s*<br[^>]*ktml_hidden ktml_selstart[^>]*>[\s\n]*<\/p>[\s\n]*/gi,'');}else if(match_start){sel_start=value.indexOf(match_start[0]);value=value.replace(/\s*<br[^>]*ktml_hidden ktml_selstart[^>]*>[\s\n]*/gi,'');}
if(pmatch_end){sel_end=value.indexOf(pmatch_end[0]);value=value.replace(/[\s\n]*<p[^>]*>\s*<br[^>]*ktml_hidden ktml_selend[^>]*>[\s\n]*<\/p>[\s\n]*/gi,'');}else if(match_end){sel_end=value.indexOf(match_end[0]);value=value.replace(/<br[^>]*ktml_hidden ktml_selend[^>]*>/gi,'');}
this.textarea.value=value;this.textarea.focus();if(sel_start>=0&&sel_end>=0){if(match_sel){this.setTextareaSelection(sel_start,sel_start);window.find(match_sel[1]+"",false,false,false,false,false,false);}else{this.setTextareaSelection(sel_start,sel_end);var num_lines=value.match(/\n/g);num_lines=num_lines?num_lines.length:0;var line_height=this.textarea.scrollHeight/(num_lines+1);var top_line=value.substring(0,sel_start).match(/\n/g);top_line=top_line?top_line.length:0;var bottom_line=value.substring(0,sel_end).match(/\n/g);bottom_line=(bottom_line?bottom_line.length:top_line)+1;this.textarea.scrollTop=line_height*bottom_line-this.textarea.offsetHeight;}}}
if(/^\s*(?:<p>)?\s*<br\s*\/?>\s*(?:<\/p>)?\s*$/i.test(this.textarea.value)){this.textarea.value='';}
if(/^\s*<p>(?:&nbsp;)?<\/p>\s*$/i.test(this.textarea.value)){this.textarea.value='';}}
function JsFormatter_hook_onsetdisplaymode(){this.formatter.format();this.laterTextareaValue=HandleOutgoingText(this,this.formatter.formattedSource);window.setTimeout("late_setTextareaValue("+this.counter+")",1);};JsFormatter=function(obj){this.type="js";this.owner=obj.counter;this.lastNode=null;for(var tagName in $FormattingTags){this.addTag($FormattingTags[tagName],tagName);}};JsFormatter.prototype.format=function(){inside_a_p_2=false;this.formattedSource="";this.length=0;this.nlafterendtag="\r\n";this.newLines=0;this.formatDateStart=new Date();window.ktmls[this.owner].selectedNode=window.ktmls[this.owner].logic_getSelectedNode();var str=this.getNodeContentSource();return str;};JsFormatter.prototype.save=function(){this.format();window.ktmls[this.owner].formElement.value=HandleOutgoingText(window.ktmls[this.owner],this.formattedSource);};function getNodeAttributes(el){var str='';function ParseAttrNode(attr){var str='';if(!attr){return"";}
var attrName=attr.nodeName.toLowerCase();if(attrName=="value"&&el.value!=""){str+=' value="'+String_htmlencode(el.value)+'"';}else if(attrName=="name"&&el.name!=""){str+=' name="'+el.name+'"';}else if(attrName=="id"&&el.id!=""){str+=' id="'+el.id+'"';}else if((attr.specified||(el.tagName=='A'&&(attrName=='name')))&&(attr.nodeValue||attrName=="style")){if(typeof(attr.nodeValue)=="string"&&!attr.nodeValue.match(/_moz/i)){str+=' '+attrName;str+='="'+String_htmlencode(attr.nodeValue)+'"';}else if(typeof(attr.nodeValue)=="number"){str+=' '+attrName;str+='="'+attr.nodeValue+'"';}else if(attrName=="style"&&el.style.cssText!=""){str+=' style="'+el.style.cssText.replace(/[\"]/g,"&quot;")+'"'}else if(attrName=="checked"){str+=' checked="true"';}else if(attrName=="multiple"){str+=' multiple="true"';}}
return str;}
if($UseAcceptedAttributesList){for(var i=0;i<$AcceptedAttributes.length;i++){var attr=el.attributes.getNamedItem($AcceptedAttributes[i]);str+=ParseAttrNode(attr);}}else{for(var i=0;i<el.attributes.length;i++){var attr=el.attributes[i];str+=ParseAttrNode(attr);}}
return str;};JsFormatter.prototype.getNodeContentSource=function(el,deep){var str="";var txtContent="";if(typeof(el)=="undefined"){el=window.ktmls[this.owner].edit.body;}
if(typeof(deep)=="undefined"){deep=0;}
if(el.tagName=="TD"&&(el.innerHTML=="&nbsp;"||el.innerHTML==" ")){str+=this.add("&nbsp;");}else{var i=0;for(;i<el.childNodes.length;i++){if(el.childNodes[i].parentNode.nodeName!=el.nodeName){continue;}
if(el.childNodes[i].nodeType==TEXT_NODE){if(is.mozilla&&el.nodeName.toLowerCase().match($TagsNoTextContent)){continue;}
txtContent=String_htmlencode(el.childNodes[i].nodeValue);str+=this.add(txtContent);}else if(el.childNodes[i].nodeType==COMMENT_NODE){str+=this.add('<!--'+el.childNodes[i].nodeValue+'-->');}else{var z=this.getNodeSource(el.childNodes[i],deep);if(z==null){return null;}
str+=z;}}
if(i==0){try{if(el.nodeName.toLowerCase()=='p'){str+=this.add('&nbsp;');}else{str+=this.add('');}}catch(e){}}}
if((new Date()-this.formatDateStart)>$MaxJsExecutionTime){return null;}else{return str;}};JsFormatter.prototype.addTag=function(tagName,bst,ast,bet,aet,indent,sc){if(typeof(tagName)=="object"){this[bst]=tagName;}else{this[tagName]={"NLBeforeStartTag":bst,"NLAfterStartTag":ast,"NLBeforeEndTag":bet,"NLAfterEndTag":aet,"indent":indent,"SelectContent":sc};}};inside_a_p_2=false;JsFormatter.prototype.getNodeSource=function(el,deep){function nIndents(n,indentChar){var s="";for(var i=0;i<n;i++){s+=indentChar;}
return s;}
var cached_inside_a_p=inside_a_p_2;var selNode=window.ktmls[this.owner].selectedNode;if(typeof(el)=="undefined"){el=window.ktmls[this.owner].edit.body;}
if(typeof(deep)=="undefined"){deep=0;}
var nl="\r\n";var beforeStartTag="";var beforeEndTag="";var afterStartTag="";var afterEndTag="";var indent=false;var indentHowMuch=0;var indentHowMuch2=0;var foundSelection=false;var SELECT_TAG_CONTENT=0;var tagDesc=null;var prevTagDesc=null;var tagName=el.tagName.toLowerCase();if(tagName.match(new RegExp("^\\/",""))){return"";}
if(tagName=="img"&&(orig=el.getAttribute("orig"))){str=this.add(unescape(orig));return str;}
if(tagName=='td'){inside_a_p_2=false;}
if(tagName=='p'){inside_a_p_2=true;}
if(this[tagName]){tagDesc=this[tagName];}
if(this.prevTag&&this[this.prevTag.tagName.toLowerCase()]){prevTagDesc=this[this.prevTag.tagName.toLowerCase()];}
if(el==window.ktmls[this.owner].selectedNode){foundSelection=true;}
if(tagDesc&&$IndentChar!=null){var parTagName=el.parentElement.tagName.toLowerCase();beforeStartTag+=(parTagName.match($TagsNoTextContent)?nIndents(deep+(tagDesc.indent||!tagDesc.ident&&parTagName.match($TagsNoTextContent)&&tagName==parTagName?1:0),$IndentChar):(tagName=="p"&&is.ie&&this.prevTag&&this.prevTag.tagName.toLowerCase()=="p"?nIndents(deep,$IndentChar):""));afterStartTag+=tagDesc.NLAfterStartTag?nl:"";beforeEndTag+=(tagDesc.NLBeforeEndTag?(this.nlafterendtag==nl?"":nl):"")+(tagName.match($TagsNoTextContent)?nIndents(deep+(tagDesc.indent||!tagDesc.ident&&parTagName.match($TagsNoTextContent)&&tagName==parTagName?1:0),$IndentChar):"");afterEndTag+=tagDesc.NLAfterEndTag?(tagName=="p"&&is.ie||parTagName=="body"||parTagName!="body"&&parTagName.match($TagsNoTextContent)?nl:""):"";this.nlafterendtag=tagDesc.NLAfterEndTag?nl:"";indentHowMuch+=tagDesc.indent?1:0;indentHowMuch2+=!tagDesc.indent&&parTagName.match($TagsNoTextContent)&&tagName==parTagName?1:0;SELECT_TAG_CONTENT+=tagDesc.SelectContent?1:0;}
SELECT_TAG_CONTENT+=tagName.match($TagsNoContent)?0:1;var str="";var attrs=getNodeAttributes(el);this.prevTag=el;str+=this.add(beforeStartTag);if(tagName!='p'||!cached_inside_a_p){str+=this.add('<'+tagName+attrs);}
if(tagName.match($TagsNoContent)){if(tagName!='p'||!cached_inside_a_p){str+=this.add('/>');}}else{if(tagName!='p'||!cached_inside_a_p){str+=this.add('>');}
str+=this.add(afterStartTag);var inner=this.getNodeContentSource(el,deep+indentHowMuch+indentHowMuch2);if(inner==null){return null;}
if(inner==''){str+=this.add('</'+tagName+'>');if(tagName=='iframe'){return str;}}
if(tagName.match($StripTagReg)&&inner==""){return"";}
str+=inner;str+=this.add(beforeEndTag);if(tagName!='p'||!cached_inside_a_p){str+=this.add('</'+tagName+'>');}}
str+=this.add(afterEndTag);if(tagName=='p'){inside_a_p_2=cached_inside_a_p;}
if((new Date()-this.formatDateStart)>$MaxJsExecutionTime){return null;}else{return str;}};JsFormatter.prototype.add=function(str){str=util_removeSiteNameFromHTML(window.ktmls[this.owner],str);this.length+=str.length;this.formattedSource+=str;var m=str.match(/(\r\n)/gi);if(m){this.newLines+=m.length;}
return str;};function XHTMLFormatter_hook_onsetdisplaymode(){this.textarea.value='';utility.window.blockInterface();var tmp=this.formatter;this.formatter.doneForSave=false;var str=HandleOutgoingText(this);this.formatter.format(str,function(){var ifr=document.getElementById('xhtml_iframe_'+window.ktmls[tmp.owner].name);var value='';if(ifr.contentWindow.document.getElementsByTagName('textarea')){value=ifr.contentWindow.document.getElementsByTagName('textarea')[0].value;}else{try{value=ifr.contentWindow.document.body.innerHTML;}catch(e){value='error';}}
var parsed_value=null;try{parsed_value=eval('('+value+')');}catch(e){}
var obj=window.ktmls[tmp.owner];if(value=='error'||(parsed_value!=null&&typeof(parsed_value.error)!="xml"&&parsed_value.error)){if((parsed_value!=null&&parsed_value.error)){alert("An error has occured: "+utility.string.getInnerText(parsed_value.error.message));}
obj.textarea.value=HandleIncomingText(obj,obj.getPlainSource(),"CODE");}else{obj.setTextareaValue(value);}
utility.window.unblockInterface();utility.dom.removeIframeLoad(ifr);ifr.contentWindow.location.replace(obj.iframeSRC);});};function XHTMLFormatter(obj){this.type="tidy";this.owner=obj.counter;this.doneForSave=false;return this;};function XHTMLFormatter_exists(ktmlObj,callback){ktmlObj.makeRequest('xhtml','xhtml',{'action':'test'},function(result){if(typeof(result)=='object'&&(result==null||result.error)){callback(false);}else{callback(true);}});};XHTMLFormatter.prototype.createAndSubmitForm=function(str){var oname=window.ktmls[this.owner].name;var iname='xhtml_iframe_'+oname;var action=window.KtmlAbsoluteServicePath;action+=(/\?/.test(action)?"&":"?")+"encoding="+window.ktmls[this.owner].charset;var frm=utility.dom.createForm({'name':"xhtml_form_"+oname,'id':"xhtml_form_"+oname,'action':action,'target':iname},[['id',window.ktmls[this.owner].id],['module','xhtml'],['method','xhtml'],['encoding',window.ktmls[this.owner].charset],['action','cleanup'],['xhtml_text',str],['RawRequest','1']]);document.forms["xhtml_form_"+oname].submit();document.body.removeChild(frm);};XHTMLFormatter.prototype.format=function(str,ready_func){this.createAndSubmitForm(str);var ifr=document.getElementById('xhtml_iframe_'+window.ktmls[this.owner].name);utility.dom.addIframeLoad(ifr,ready_func);};XHTMLFormatter.prototype.save=function(ready_func){var counter=this.owner;var str=HandleOutgoingText(window.ktmls[counter]);if(str){this.format(str,function(){var k=window.ktmls[counter];var ifr=document.getElementById('xhtml_iframe_'+k.name);ifr.onload=null;ifr.onreadystatechange=null;var value='';if(ifr.contentWindow.document.getElementsByTagName('textarea')){value=ifr.contentWindow.document.getElementsByTagName('textarea')[0].value;}else{try{value=eval(ifr.contentWindow.document.body.innerHTML);}catch(e){value='error';}}
if(value=='error'){k.formElement.value=HandleIncomingText(k);}else{k.formElement.value=k.replaceTags(value);}
ready_func(ready_func.eventArg);});}else{window.ktmls[counter].formElement.value='';ready_func(ready_func.eventArg);}};XHTMLFormatter.exists=XHTMLFormatter_exists;function KTML_makeHolders(){this.selectedNode=this.logic_getSelectedNode();var rng=this.edit.selection.createRange();if(this.edit.selection.type=='None'){var nodesArray=[rng.parentElement()];}else{var nodesArray=this.getSelectionAsNodes(rng);}
if(nodesArray.length==0){return;}
var first=nodesArray[0];var last=nodesArray[nodesArray.length-1];if(Array_indexOf(['img','table','hr','input','textarea'],first.nodeName.toLowerCase())>=0&&nodesArray.length==1){var par=first.parentNode;par.insertBefore(utility.dom.createElement('br',{'style':'display: none','class':'ktml_hidden ktml_selstart'},this.cw),first);utility.dom.insertAfter(utility.dom.createElement('br',{'style':'display: none','class':'ktml_hidden ktml_selend'},this.cw),first);}else{if(first.childNodes.length){first.insertBefore(utility.dom.createElement('br',{'style':'display: none','class':'ktml_hidden ktml_selstart'},this.cw),first.childNodes[0]);}else{var br=utility.dom.createElement('br',{'style':'display: none','class':'ktml_hidden ktml_selstart'},this.cw);try{first.appendChild(br);}catch(e){}}
if(typeof last!='undefined'&&last.nodeType&&last.nodeType==1){last.appendChild(utility.dom.createElement('br',{'style':'display: none','class':'ktml_hidden ktml_selend'},this.cw));}}};function xhtml_runonce(){ktml.prototype.setTextareaSelection=ktml_setTextareaSelection;var do_it=false;for(var i=0;i<window.ktmls.length&&!do_it;i++){var view_source=window.ktmls[i].getModuleProperty('xhtml','xhtml_view_source')||'false';var save=window.ktmls[i].getModuleProperty('xhtml','xhtml_save')||'false';do_it=do_it||view_source=='true';do_it=do_it||save=='true';}
if(do_it){var xhtml_exists=false;XHTMLFormatter.exists(window.ktmls[0],function(xhtml_exists){for(var i=0;i<window.ktmls.length;i++){if(typeof window.ktmls[i]['xhtml_runeach_done']!="undefined"){continue;}
zxhtml_runeach_async(window.ktmls[i],xhtml_exists);window.ktmls[i]['xhtml_runeach_done']=true;}});}};function zxhtml_runeach_async(k,xhtml_exists){var view_source=k.getModuleProperty('xhtml','xhtml_view_source')=='true';var save=k.getModuleProperty('xhtml','xhtml_save')=='true';k.flags.xhtml_exists=xhtml_exists;k.flags.view_source=view_source;k.flags.save=save;if(xhtml_exists){var name=new String(k.name);if(view_source||save){k.formatter=new XHTMLFormatter(k);}
if(view_source){k.hooks['onsetdisplaymode']=[];k.hooks['onsetdisplaymode_beforeinvisible'].push(KTML_makeHolders);k.hooks['onsetdisplaymode'].push(XHTMLFormatter_hook_onsetdisplaymode);}}else{if(view_source||save){k.formatter=new JsFormatter(k);}
if(view_source){k.hooks['onsetdisplaymode']=[];k.hooks['onsetdisplaymode_beforeinvisible'].push(KTML_makeHolders);k.hooks['onsetdisplaymode'].push(JsFormatter_hook_onsetdisplaymode);}}};