
var help_mode=false;var status_message='';imageExtensions='gif,xbm,png,jpeg,jpg,jpe,jfif,tiff,tif,bmp';imageExtensionsRegExp=new RegExp("\\.(?:"+imageExtensions.split(",").join("|")+")$","i");movieExtensions='swf,mov,asf,wmv,mpeg,avi,mpg';movieExtensionsRegExp=new RegExp("\\.(?:"+movieExtensions.split(",").join("|")+")$","i");function isImage(fName){return browse_submode=="media"&&imageExtensionsRegExp.test(fName);};function isMovie(fName){return movieExtensionsRegExp.test(fName);};templateExtensions='htm,html';templateExtensionsRegExp=new RegExp("\\.(?:"+templateExtensions.split(",").join("|")+")$","i");function isTemplate(fName){return templateExtensionsRegExp.test(fName);};current_path="/";current_file="";current_selection=new PathsArray();item_selection_type="none";folder_tree=null;file_list=null;function update_current_path(src){var d;if(typeof(src)!="undefined"){if(src.indexOf("/")!=0){src="/"+src;}
current_path=src.replace(new RegExp("[^\\/]+$",""),"");current_file=src.replace(new RegExp("^\\/.*\\/",""),"");}
d=document.getElementById("location_bar");d.value=current_path.replace(new RegExp("^\\/"),"");ktml.setModuleProperty(browse_submode,'lastUsedFolder',current_path.replace(new RegExp("^\\/"),""));};function setStatus(msg){window.defaultStatus=msg;window.status=msg;};i18n={};function trans(msg_id){if(opener){win=window.topOpener;}else{win=window;}
if(typeof(i18n[msg_id])=="undefined"){i18n[msg_id]=win.translate(msg_id);}
var s=i18n[msg_id];var args=[];args[0]=s;var exec='s = utility.string.sprintf(args[0]';for(var i=1;i<trans.arguments.length;i++){args[i]=trans.arguments[i];exec+=', args['+i+']';}
exec+=')';try{eval(exec);}catch(err){}
s=String_trim(s.replace(/\/r\/n/g,'\n'));return s;};if(typeof(KTStorage)=="undefined"){KTStorage=new ObjectStorage("ktml")}
var getvars=new QueryString();ktml=window.opener.ktmls[getvars.find('id')];browse_submode=ktml.getModuleProperty('filebrowser','browse_submode');browse_filter=ktml.getModuleProperty('filebrowser','browse_filter');browse_submode_detail=ktml.getModuleProperty('filebrowser','submodeDetail');currentUploadFolder=ktml.getModuleProperty(browse_submode,'UploadFolderUrl')
fileUploadFolder=ktml.getModuleProperty('file','UploadFolderUrl')
mediaUploadFolder=ktml.getModuleProperty('media','UploadFolderUrl')
allow_file_operations=(browse_submode!="templates")||(browse_submode=="templates"&&ktml.getModuleProperty('templates','DenySave')!='true');allow_folder_operations=allow_file_operations;var allow={file:{all:0,documents:1,media:0,images:0,templates:0},media:{all:0,documents:0,media:1,images:1,templates:0},templates:{all:0,documents:0,media:0,images:0,templates:1}}
if(fileUploadFolder==mediaUploadFolder){allow.file.all=1;allow.file.media=1;allow.file.images=1;allow.media.all=1;allow.media.documents=1;}
var has_media=ktml.toolbar.indexOfName('insert_image')>=0;var has_file=ktml.toolbar.indexOfName('insert_file')>=0;var has_template=ktml.toolbar.indexOfName('insert_template')>=0;allow.media.documents&=has_file;allow.file.images&=has_media;allow.file.media&=has_media;allow.templates.templates&=has_template;function init(refresh){if(!window.opener){document.write("You do not have access directly to this area of the editor.");window.close();return;}
window.focus();if(typeof(refresh)=="undefined"){utility.window.setModal();if(is.ie){document.charset=opener.document.charset;}
document.title=lang_translatepage("||Remote File Explorer||",ktml.config.UILanguage,window.opener);document.body.innerHTML=lang_translatepage(document.body.innerHTML,ktml.config.UILanguage,window.opener);set_i18n();}
setStatus(trans(i18n.DEFAULT_STATUS));var getvars,filter,selectedSrc,lastUsedFolder;if(typeof(refresh)=="undefined"){var version=com.deconcept.FlashObjectUtil.getPlayerVersion();if(opener.ktml_init_object.server=="jsp"||is.mozilla||version.major<8){document.getElementById('multiple').style.display='none';document.getElementById('file').style.display='';}
if(opener.ktml_init_object.server!="jsp"&&is.ie&&version.major>=8){var fo=new FlashObject("file-upload.swf","fileupload","1","1","7","#FF6600");fo.write("file-upload-container");}
utility.dom.attachEvent(is.ie?document.body:document.documentElement,"onkeydown",keyboard_manager);init_selection();window.KtmlDirDepth=window.topOpener.KtmlDirDepth;window.KtmlRoot=window.topOpener.KtmlRoot;document.getElementsByTagName('form')[0].action=window.topOpener.KtmlAbsoluteServicePath;document.getElementById('id').value=ktml.id;filter=document.getElementById('filetype_selector');var option_value='';for(var i=filter.options.length-1;i>=0;i--){option_value=filter.options[i].value
if(!allow[browse_submode][option_value]){filter.options[i]=null;}}
utility.dom.selectOption(filter,browse_filter);submode_detail=ktml.getModuleProperty('filebrowser','submodeDetail');selectedSrc=ktml.getModuleProperty(browse_submode,'selectedSrc');lastUsedFolder=ktml.getModuleProperty(browse_submode,'lastUsedFolder');fileupload_change();fileBrowserType=ktml.getModuleProperty('filebrowser','fileBrowserType');if(!fileBrowserType||Array_indexOf(["image","media","file"],browse_submode)!=-1){fileBrowserType="Files";}
if(selectedSrc){update_current_path(selectedSrc);}else if(lastUsedFolder){update_current_path(lastUsedFolder);}
if(browse_submode=="templates"&&submode_detail=="saveas"){saveas_el=document.getElementById("template_saveas_name");saveas_el.focus();}
var response=ktml.makeRequest('image',"checkcapabilities",{},function(result){if(typeof(result)=='object'&&result.error){alert(utility.string.getInnerText(result.error.message));return;}
img_flags=result;has_imaging=result.hasimg!='';set_buttons_state();fileupload_change();});}
if(refresh){filter=document.getElementById('filetype_selector');browse_filter=filter.value;}
if(browse_submode=="templates"){set_buttons_state();fileupload_change();}else{document.getElementById("template_saveas_name").style.display='none';set_buttons_state();fileupload_change();}
if(typeof(refresh)=="undefined"){makeContextMenu();}
clipboard=new PathsArray();thumber=new ThumbnailCreator();folder_tree=new FolderTree(ktml,"folders_list",current_path);folder_tree.onnodeselect=function(path){if(path!=current_path){update_current_path(path);setStatus(trans(i18n.LOADING_FOLDER,path));set_buttons_state();current_selection.removeAll();ktml.setModuleProperty(browse_submode,'lastUsedFolder',current_path.replace(new RegExp("^\\/",""),""));file_list.dispose();file_list.list_path(path,"","");}};if(file_list){file_list.clear();file_list.list_path(current_path,browse_filter,current_file);}else{file_list=new FileList(ktml,"files_container",current_path,current_file,{"filter":browse_filter,"type":fileBrowserType});file_list.onitemselect=function(lname,silent){if(typeof(silent)=="undefined"){silent=false;}
current_selection.add(current_path+lname);current_file=lname;if(!silent){if(current_selection.paths.length>1){status_message=trans(i18n.SELECTED_N_FILES,current_selection.paths.length);}else{status_message=trans(i18n.SELECTED_FILE,lname);}
setStatus(status_message);set_buttons_state();}
if(browse_submode=="templates"&&submode_detail=="saveas"){document.getElementById("template_saveas_name").value=lname;document.getElementById("template_saveas_name").focus();document.getElementById("template_saveas_name").select();}};file_list.onitemdeselect=function(lname,silent){if(typeof(silent)=="undefined"){silent=false;}
current_selection.remove(current_path+lname);current_file="";if(current_selection.paths.length==1){current_file=current_selection.paths[0].replace(/(.*?)([^\/]*)$/,'$2')}
if(!silent){setStatus(current_selection.paths.length>1?trans(i18n.SELECTED_N_FILES,current_selection.paths.length):((current_selection.paths.length==1)?trans(i18n.SELECTED_FILE,current_file):trans(i18n.DEFAULT_STATUS)));set_buttons_state();}};file_list.onload=function(path,result){var node;node=folder_tree.path_to_node(path);if(!result.length){result.length=0;}
node.filteredfiles=result.length;var foldersCount=node.subfolders||0;var _statusStr=trans(i18n.FOLDER_CONTAINS,foldersCount,node.filteredfiles);window.setTimeout("setStatus('"+_statusStr+"')",100);node.setDetails({subfolders:foldersCount,allfiles:node.allfiles,filteredfiles:node.filteredfiles});if(typeof(global_response)!='undefined'&&global_response!=null&&global_response.length){if(typeof(global_response)=="string"){var path=global_response;var lname=path.substr(path.lastIndexOf('/')+1,path.length);var item=file_list.get_item(lname);if(item){item.select();}}else{try{if(/function Array()/.test(global_response.constructor)){for(var j=0;j<global_response.length;j++){if(typeof(global_response[j])!="object"){var path=global_response[j];var lname=global_response[j].substr(global_response[j].lastIndexOf('/')+1,global_response[j].length);var item=file_list.get_item(lname);item.select();}}}}catch(e){};}
set_buttons_state();global_response=null;}};}};function keyboard_manager(e){var o=utility.dom.setEventVars(e);var k=o.e.keyCode;if(",input,textarea,select,".indexOf(","+o.targ.tagName.toLowerCase()+",")>=0){return true;}
if(o.e.ctrlKey&&k==65){utility.dom.stopEvent(o.e);file_list.selectAll();window.setTimeout("document_clearselection()",0);}
if(o.e.ctrlKey&&k==67){cm_copy();}
if(o.e.ctrlKey&&k==88){cm_cut();}
if(o.e.ctrlKey&&k==86){cm_paste();}
if(k==46){delete_selected();}};function document_clearselection(){if(is.ie){document.selection.clear();}else{window.getSelection().removeAllRanges();}};function refresh2(){init(true);};function userFriendlyErr(result){var failedOperation=new RegExp("(blur)|(sharpen)|(contrast)|(brightness)","i");var noLibAvailable=new RegExp("(NO_LIB)|(imagemagick is required)","gi");if(noLibAvailable.test(result)){var operation=failedOperation.exec(result)?failedOperation.exec(result)[0].toLowerCase():'required';var template="The server does not own the needed libraries to perform the '%s' operation.";result=utility.string.sprintf(template,operation)+"\nIMAGE MAGICK is either not installed or not properly configured.";}
return result;};function gotopath(path){if(typeof(path)=="undefined"){path="/";path+=document.getElementById('location_bar').value;}
if(path.lastIndexOf("/")!=path.length-1){path+="/";}
folder_tree.select_path(path);};function check_location_text(o,e){if(e.keyCode==13){gotopath(o.value);return false;}
return true;};function folder_new(){if(!allow_folder_operations){return;}
var nodeSelected=folder_tree.path_to_node(folder_tree.selected_path);if(nodeSelected.childrenStatus==0){nodeSelected.getChildren(function(){node.setOpen(1);createNewFolder()});return;}
if(nodeSelected.childrenStatus==3){createNewFolder();}};function createNewFolder(prevUserValue){var nodeSelected=folder_tree.path_to_node(folder_tree.selected_path);var userMsg;if(!prevUserValue){userMsg=trans(i18n.PLEASE_ENTER_NEW_FOLDER_NAME);}else if(nodeSelected.getChildIndexByName(prevUserValue)>=0){userMsg=trans(i18n.FOLDER_EXISTS);}
var newDefaultName=trans(i18n.DEFAULT_NEW_FOLDER_NAME);var uniqueSuffix=1;while(nodeSelected.getChildIndexByName(newDefaultName)>=0){uniqueSuffix++;newDefaultName=i18n.DEFAULT_NEW_FOLDER_NAME+' '+uniqueSuffix;}
newDefaultName=prevUserValue||newDefaultName;var newFolderName=(userMsg)?prompt(userMsg,newDefaultName):prevUserValue;if(newFolderName){while(newFolderName&&newFolderName.match(/[^0-9a-z\.,;\-_\(\)\[\]\s]/i)){newFolderName=newFolderName.replace(/[^0-9a-z\.,;\-_\(\)\[\]\s]/gi,'_');newFolderName=prompt(trans(i18n.INVALID_FOLDER_NAME),newFolderName);if(newFolderName!=null){newFolderName=String_trim(newFolderName);}}
if(newFolderName!=null){newFolderName=String_trim(newFolderName);}
if(!newFolderName){return;}
if(nodeSelected.getChildIndexByName(newFolderName)>=0){newFolderName=prompt(trans(i18n.FOLDER_EXISTS),newFolderName);createNewFolder(newFolderName);return;}
utility.window.blockInterface();folder_tree.mkdir(newFolderName,function(){utility.window.unblockInterface();});}}
function image_edit(){var fileName=current_file;var btn_edit=document.getElementById('btn_edit');if(has_imaging&&btn_edit.className.indexOf("btn_disabled")==-1&&isImage(fileName)){ktml.setModuleProperty('filebrowser','editImagePath',current_path.replace(new RegExp("^\\/",""),'')+fileName);utility.window.openWindow("EditImageWindow"+parseInt(Math.random()*1000000000),"editimage.html?id="+ktml.counter,665,490);}};var lastTopScroll=0;var preventQuickDeleteFlag=false;var deletionRequestRunning=false;function delete_selected(){if(deletionRequestRunning){return;}
var toDelete,confirm_str;if(current_path!="/"){toDelete=current_path;}
if(current_file!=""){toDelete=current_file;}
if(!toDelete){return;}
if(item_selection_type=="file"){if(!allow_file_operations){return;}}else if(item_selection_type=="folder"){if(!allow_folder_operations){return;}
if(current_path=="/"){return;}}else{return;}
if(item_selection_type=='folder'){if(preventQuickDeleteFlag){return;}
var tmp=folder_tree.path_to_node(folder_tree.selected_path);var must_confirm=false;if(tmp.allfiles!=0||tmp.subfolders!=0){must_confirm=true;}
var ret=true;if(must_confirm){confirm_str=trans(i18n.CONFIRM_FOLDER_DELETE,tmp.subfolders,tmp.allfiles);ret=confirm(confirm_str);}
if(ret){deletionRequestRunning=true;folder_tree.rmdir("",function(){utility.window.unblockInterface();deletionRequestRunning=false;})}}else if(item_selection_type=='file'){if(current_selection.paths.length==0){return;}
confirm_str=trans(i18n.CONFIRM_FILE_DELETE,current_selection.paths.length);if(confirm(confirm_str)){preventQuickDeleteFlag=true;utility.window.blockInterface();lastTopScroll=document.getElementById('files_list').scrollTop;var path=current_selection.paths[0];var path=path.substr(0,path.lastIndexOf("/"));deletionRequestRunning=true;file_list.del(current_selection.paths,function(){for(var i=0;i<files_deleted.length;i++){file_list.removeChild(files_deleted[i]);}
files_deleted=[];current_selection.removeAll();current_file="";refresh2();utility.window.unblockInterface();deletionRequestRunning=false;var _execString="document.getElementById('files_list').scrollTop="+lastTopScroll+";"+"if(document.getElementById('files_list').scrollTop=="+lastTopScroll+"){window.clearInterval(RESET_SCROLL)}";RESET_SCROLL=window.setInterval(_execString,100);});}
set_buttons_state();}};function clickedSelected(){var fileToUpload=document.getElementById('file').value;if(browse_submode=="templates"&&submode_detail=="saveas"){template_saveas();return false;}else
if(fileToUpload==''){insert_selected();return false;}else{var uploadIframe=document.getElementById('uploadIframe');document.getElementById('module').value='file';document.getElementById('submode').value=browse_submode;document.getElementById('resize').value='false';document.getElementById('encoding').value=ktml.charset;document.getElementById('folder').value=current_path;utility.window.blockInterface();file_list.deselectAll();utility.dom.addIframeLoad(uploadIframe,function(){utility.window.unblockInterface();var responseObj=uploadIframe.contentWindow.document.getElementsByTagName('textarea');var responseObj_id='';var responseObj_value='';if(responseObj&&responseObj.length){responseObj=responseObj[0];responseObj_id=responseObj.id;responseObj_value=responseObj.value;}else{responseObj_id='error';responseObj_value=uploadIframe.contentWindow.document.body.innerHTML;}
uploadIframe.onload=null;uploadIframe.onreadystatechange=null;uploadIframe.contentWindow.location.replace('../../../../'+ktml.iframeSRC);if(responseObj_id=='error'){var err=null;var response=null;try{response=eval('('+responseObj_value+')');}catch(err){}
if(response&&response.error){alert(utility.string.getInnerText(response.error.message));}else if(err||err===null){if(opener.KtmlDevelopment){alert("Cannot parse server response: \n==== Error number:"+(err?err.number:"NA")+"\n==== Module, method:file, upload\n==== Server response "+(is.ie?"(CTRL+C to copy)":"")+"\n"+responseObj_value+"==== End server response");}else{alert(opener.translate("cannot parse server"));}
ktml.logger.log(opener.LOG_ERROR,'SP',"Module file, method upload<br>==== Server response ====\r\n<xmp class=\"sp_error\">"+responseObj_value+"</xmp>\r\n==== End server response ====");}}else{global_response=responseObj_value;refresh2();}
fileupload_change(true);var fileToUploadControl=document.getElementById('file');fileToUploadControl.parentNode.innerHTML='<input name="Filedata" id="file" type="file" onchange="fileupload_change(false)" size="24" class="ktml_button" />';});document.getElementsByTagName('form')[0].submit();return false;}};function template_saveas(){var templateSaveAsName=document.getElementById("template_saveas_name").value;if(!/[0-9a-zA-Z\s_\-]+$/i.test(templateSaveAsName)){alert(opener.translate(i18n.INVALID_TEMPLATE_NAME,ktml.config.UILanguage));document.getElementById("template_saveas_name").focus();document.getElementById("template_saveas_name").select();return;}
var idx=file_list.getChildIndexByName(templateSaveAsName);if(idx!=-1){if(!confirm(opener.translate("Overwrite?",ktml.config.UILanguage))){return;}}
templateSaveAsName=templateSaveAsName.replace(/\.ktpl$/i,'');var template_content=ktml.getModuleProperty('templates','template_content');var action=opener.KtmlAbsoluteServicePath;action+=(/\?/.test(action)?"&":"?")+"encoding="+ktml.charset;var options={'name':"template_save_form_"+ktml.name,'id':"template_save_form_"+ktml.name,'method':'POST','action':action,'target':"template_save_iframe_"+ktml.name};var inputs=[['id',ktml.id],['module','templates'],['method','write'],['encoding',ktml.charset],['folder',current_path],['submode',browse_submode],['template_name',templateSaveAsName],['content',template_content]];var frm=utility.dom.createElement("FORM",{name:options.name,id:options.id,action:options.action,encoding:ktml.charset,method:options.method,style:"display: none"},opener);Array_each(inputs,function(input,i){frm.appendChild(utility.dom.createElement('INPUT',{'type':'hidden','id':input[0],'name':input[0],'value':input[1]},opener));});frm=opener.document.body.appendChild(frm);frm.target=options.target;var ifr=opener.document.getElementById('template_save_iframe_'+ktml.name);utility.dom.addIframeLoad(ifr,function(){var ifr=opener.document.getElementById('template_save_iframe_'+ktml.name);var result=null;var value='';try{value=ifr.contentWindow.document.body.innerHTML;result=eval(value);ifr.onload=null;}catch(e){result='error';ifr.onload=null;}
if(result=='error'){if(opener.KtmlDevelopment){alert("Cannot parse server response: \n=========\ntemplates,write\n"+value+"\n=========");}else{alert(opener.translate("cannot parse server"));}}else if(typeof(result)=='object'&&result.error){alert(result.error.message);document.getElementById("template_saveas_name").focus();document.getElementById("template_saveas_name").select();return;}
utility.window.hideModalBlocker(window.topOpener);window.setTimeout("utility.window.close()",1);});opener.document.forms["template_save_form_"+ktml.name].submit();opener.document.body.removeChild(frm);};function insert_selected(){if(current_file==""){alert(trans(i18n.PLEASE_SELECT_SOMETHING));return;}
var callBackObject=ktml.getModuleProperty('filebrowser','callback');var obj=callBackObject[0];var func=callBackObject[1];var param=callBackObject[2];ktml.insert_count=current_selection.paths.length;for(i=0;i<current_selection.paths.length;i++){if(browse_submode=="templates"){obj[func](file_list.get_item(current_file).template_content);}else{var fileName=current_selection.paths[i];fileName=fileName.replace(/^\x2f/,'');var fullFileName=window.topOpener.ktml_location.server+ktml.getModuleProperty(browse_submode,'UploadFolderUrl');fullFileName+=fileName;fullFileName+='?size='+file_list.get_item(current_file).size;if(browse_submode_detail=="forceurl"){obj[func](param,fullFileName);}else{obj[func](param,fullFileName,isImage(fileName),isMovie(fileName));}}}
utility.window.hideModalBlocker(window.topOpener);utility.window.close();}
function clickedCancel(){utility.window.hideModalBlocker(window.topOpener);utility.window.close();}
function set_button_enabled(btn,state){if(state){utility.dom.classNameRemove(btn,'btn_disabled');}else{utility.dom.classNameAdd(btn,'btn_disabled');}}
function set_buttons_state(){var somethingIsSelected;var btn_home=document.getElementById('btn_home');set_button_enabled(btn_home,current_path!="/");somethingIsSelected=false;if(current_path!="/"||current_file!=""||current_selection.paths.length>0){somethingIsSelected=true;}
var btn_delete=document.getElementById('btn_delete');if(!somethingIsSelected||!(allow_file_operations||allow_folder_operations)||(item_selection_type=="folder"&&current_path=="/")){set_button_enabled(btn_delete,false);}else{set_button_enabled(btn_delete,true);}
var btn_new=document.getElementById('btn_new');set_button_enabled(btn_new,allow_folder_operations);multiple_selection=current_selection.paths.length!=1
var btn_edit=document.getElementById('btn_edit');var file_error=false;if(current_selection.paths.length==1){var file=file_list.get_item(current_file);if(file){file_error=file.state=='error';}}
set_button_enabled(btn_edit,has_imaging&&somethingIsSelected&&!multiple_selection&&isImage(current_file)&&!file_error);};function init_selection(){DNDDom.dhtml.setMovable("files_list",DND_HANDLE_FOR+"file_list_selection");FLS=document.getElementById("file_list_selection");var files_list_node=DNDDom.getElement("files_list");files_list_node.onStartMove=function(e,m){if(e.shiftKey){return false;}
if(m.target.parentNode.tagName=="BODY"){return false;}
m.FLBox=utility.dom.getBox(DNDDom.getElement("files_list"));m.scrollBarWidth=(m.source.offsetWidth-m.source.scrollWidth);if(m.FLBox.width+m.FLBox.x-m.scrollBarWidth<=e.clientX){return false;}
m.autoScrollAmount=7;m.autoScrollFactor=is.ie?10:3;m.target.style.width="0px";m.target.style.height="0px";m.currentX=m.startX=e.clientX-m.FLBox.x+document.getElementById("files_list").scrollLeft;m.currentY=m.startY=e.clientY-m.FLBox.y+document.getElementById("files_list").scrollTop;m.target.style.left=m.startX+"px";m.target.style.top=m.startY+"px";return true;};files_list_node.onMove=function(e,m){document_clearselection();if(m.FLBox.x>=e.clientX){m.currentX=0;}else if(m.FLBox.width+m.FLBox.x-m.scrollBarWidth<=e.clientX){m.currentX=m.FLBox.width-m.scrollBarWidth;}else{m.currentX=e.clientX-m.FLBox.x;}
if(m.FLBox.y>=e.clientY){DNDDom.mover.autoScrollAmount=parseInt((m.FLBox.y-e.clientY+2)/m.autoScrollFactor*2+5);window.clearInterval(m.interval2);m.interval2=null;if(!m.interval1){m.interval1=window.setInterval("file_list.div.scrollTop -= DNDDom.mover.autoScrollAmount; if(is.mozilla)file_list.onscroll();",60);}
m.currentY=0;}else if(m.FLBox.height+m.FLBox.y<=e.clientY-2){DNDDom.mover.autoScrollAmount=parseInt((e.clientY-2-m.FLBox.height-m.FLBox.y)/m.autoScrollFactor*2+7);m.currentY=m.FLBox.height-1;window.clearInterval(m.interval1);m.interval1=null;if(!m.interval2){m.interval2=window.setInterval("file_list.div.scrollTop += DNDDom.mover.autoScrollAmount; if(is.mozilla)file_list.onscroll();",60);}}else{DNDDom.mover.autoScrollAmount=7;window.clearInterval(m.interval1);m.interval1=null;window.clearInterval(m.interval2);m.interval2=null;m.currentY=e.clientY-m.FLBox.y;}
m.currentX+=document.getElementById("files_list").scrollLeft;m.currentY+=document.getElementById("files_list").scrollTop;var dx=m.currentX-m.startX;var dy=m.currentY-m.startY;if(m.target.style.display!="block"){if(Math.abs(dx)>4||Math.abs(dy)>4){m.target.style.display="block";}else{return;}}
if(dx>=2){m.target.style.left=(m.startX)+"px";m.target.style.width=(dx-2)+"px";}else if(dx==0){m.target.style.left=(m.startX)+"px";m.target.style.width=(0)+"px";}else if(dx==1){m.target.style.left=(m.startX)+"px";m.target.style.width=(0)+"px";}else if(dx==-1){m.target.style.left=(m.startX-1)+"px";m.target.style.width=(0)+"px";}else{m.target.style.left=(m.startX+dx)+"px";m.target.style.width=(-dx-1)+"px";}
if(dy>=2){m.target.style.top=(m.startY)+"px";m.target.style.height=(dy-2)+"px";}else if(dy==0){m.target.style.top=(m.startY)+"px";m.target.style.height=(0)+"px";}else if(dy==1){m.target.style.top=(m.startY)+"px";m.target.style.height=(0)+"px";}else if(dy==-1){m.target.style.top=(m.startY-1)+"px";m.target.style.height=(0)+"px";}else if(dy==-2){m.target.style.top=(m.startY-2)+"px";m.target.style.height=(1)+"px";}else{m.target.style.top=(m.startY+dy+0)+"px";m.target.style.height=(-dy-1)+"px";}};files_list_node.onEndMove=function(e,m){window.clearInterval(m.interval1);m.interval1=null;window.clearInterval(m.interval2);m.interval2=null;if(m.target.style.display=="none"){return true;}
m.target.style.display="none";if(!e.ctrlKey){file_list.deselectAll();}
file_list.cancelClick=true;var itemWidth=125;var itemHeight=137;var col1=parseInt(m.startX/itemWidth);var row1=parseInt(m.startY/itemHeight);var col2=parseInt(m.currentX/itemWidth);var row2=parseInt(m.currentY/itemHeight);var silent=!(row1==row2&&col1==col2);var sel_count;for(var i=Math.min(row1,row2);i<=Math.max(row1,row2);i++){for(var j=Math.min(col1,col2);j<=Math.max(col1,col2);j++){if((j+i*4)<file_list.children.length){file_list.children[j+i*4].select(!silent,silent);}}}
if(silent){setStatus(current_selection.paths.length?trans(i18n.SELECTED_N_FILES,current_selection.paths.length):trans(i18n.DEFAULT_STATUS));set_buttons_state();}};};function fileupload_change(forceSelect){var fileToUpload=document.getElementById('file').value;var button_label=((fileToUpload&&!forceSelect)?trans(i18n.BTN_UPLOAD):trans(i18n.BTN_SELECT));if(browse_submode=="templates"&&submode_detail=="saveas"){button_label=trans(i18n.BTN_SAVE);}
document.getElementById('filetype_label').style.visibility='';var filter=document.getElementById('filetype_selector');if(browse_submode=='templates'){filter.disabled=true;document.getElementById("template_saveas_name").style.display="none";document.getElementById("template_saveas_label").style.display="none";document.getElementById('file').style.display='none';document.getElementById('multiple').style.display='none';}
if(browse_submode=="templates"&&submode_detail=="saveas"){document.getElementById('multiple').style.display='none';document.getElementById('file').style.display='none';document.getElementById("template_saveas_name").style.display="";document.getElementById("template_saveas_label").style.display="";document.getElementById("template_saveas_name").focus();}
document.getElementById('upload_select').value=button_label;}
function showFriendlySize(size){size/=1024;var ret=parseInt(size);if(ret<1){ret=1;}
return ret+' KB';};function filter_files(){refresh2();};function set_i18n(){i18n.BTN_SAVE="Save";i18n.BTN_UPLOAD="Upload";i18n.BTN_SELECT="Insert";i18n.DEFAULT_STATUS="Remote File Explorer";i18n.LOADING_FOLDER="Loading folder: %s";i18n.SELECTED_N_FILES="Selected: %d files";i18n.SELECTED_FILE="Selected file: %s";i18n.FOLDER_CONTAINS="Folder contains: %s folder(s) and %s file(s).";i18n.LOADING="Loading...";i18n.NO_FILES="No items that match the filter were found.";i18n.DIMENSIONS="Dimensions:";i18n.SIZE="Size";i18n.PLEASE_SELECT_SOMETHING="Please select an image or file, then click the \"Insert\" button to insert it into your document.";i18n.FILE_UPLOAD_ERRORS="The following errors have occured:\r\n%s";i18n.PLEASE_ENTER_NEW_FOLDER_NAME="Enter new folder name";i18n.DEFAULT_NEW_FOLDER_NAME="New Folder";i18n.INVALID_FOLDER_NAME="Folder name invalid";i18n.INVALID_FILE_NAME="File name invalid";i18n.FOLDER_EXISTS="Folder exists";i18n.CONFIRM_FILE_DELETE="Are you sure you want to delete %d selected file(s)?";i18n.CONFIRM_FOLDER_DELETE="Are you sure you want to delete the selected folder (%d subfolder(s), %d file(s))?";i18n.LBL_FILE_NAME="File name:";i18n.LBL_FILE_UPLOAD="File Upload:";i18n.NO_SELECTION="No selection!";i18n.INVALID_TEMPLATE_NAME="Invalid template file name.";i18n.copy="copy";i18n.cut="cut";i18n.CLIPBOARD_OPERATION_FAILED="Could not %s the following files:%s";i18n.RENAME_FILE="Rename file";i18n.RENAME_FOLDER="Rename folder";};function getFlashMovieObject(movieName){return document.getElementById(movieName);};function flash_file_upload(){utility.window.blockInterface();ktml.makeRequest('file',"resetuploadstatus",{submode:browse_submode},function(result){var url=opener.KtmlAbsoluteServicePath;var arr=[];arr.push('id='+document.getElementById('id').value);arr.push('module='+document.getElementById('module').value);arr.push('method='+document.getElementById('method').value);arr.push('submode='+browse_submode);arr.push('encoding='+ktml.charset);arr.push('folder='+current_path);arr.push('sharpen=false');arr.push('resize=false');arr.push('keep_proportion=false');arr.push('savetosession=true');url=url+'?'+arr.join('&');var flashMovie=getFlashMovieObject("fileupload");flashMovie.SetVariable("url",url);var MaxFileSize=ktml.getModuleProperty('filebrowser','MaxFileSize');flashMovie.SetVariable("maxSize",MaxFileSize);flashMovie.SetVariable("complete_function",'flash_file_upload_complete');flashMovie.SetVariable("cancel_function",'flash_file_upload_cancel');flashMovie.SetVariable("onHTTPError_function",'flash_file_upload_onHTTPError');flashMovie.SetVariable("file_types",document.getElementById('filetype_selector').value);var allowed_file_types=ktml.getModuleProperty('file','AllowedFileTypes');var allowed_media_types=ktml.getModuleProperty('media','AllowedFileTypes');var allowed_template_types="ktpl";var allowed_image_types=allowed_media_types;flashMovie.SetVariable("AllowedMediaTypes",allowed_media_types);flashMovie.SetVariable("AllowedFileTypes",allowed_file_types);flashMovie.SetVariable("AllowedTemplateTypes",allowed_template_types);flashMovie.SetVariable("AllowedImageTypes",allowed_image_types);file_list.deselectAll();flashMovie.Play();})};function flash_file_upload_onHTTPError(err){alert(err);utility.window.unblockInterface();};function flash_file_upload_complete(err){var response=ktml.makeRequest('file',"getuploadstatus",{submode:browse_submode},function(result){if(typeof(result)=='object'&&result.error){alert(utility.string.getInnerText(result.error.message));utility.window.unblockInterface();return;}
var s='';if(result&&result.length){for(var j=0;j<result.length;j++){if(typeof(result[j])=="object"){s+=result[j].error.message+"\r\n";}}}
if(s!=''||err!=''){alert(trans(i18n.FILE_UPLOAD_ERRORS,s+err));}
utility.window.unblockInterface();global_response=result;refresh2();});};function flash_file_upload_cancel(){utility.window.unblockInterface();};function IEClean(){if(is.ie){if(folder_tree){try{folder_tree.dispose();}catch(err){}}
if(file_list){try{file_list.dispose();}catch(err){}
file_list.div=null;file_list.container=null;}}};