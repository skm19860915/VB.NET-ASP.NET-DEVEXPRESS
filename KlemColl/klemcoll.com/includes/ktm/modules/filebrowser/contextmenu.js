
has_imaging=false;img_flags={hasimg:'',resize:0,rotate:0,crop:0,degrade:0,flip:0,sharpen:0,blur:0,contrast:0,brightness:0};multiple_selection=false;var dont_overwrite_utility=['utility.dom.getPixels','utility.dom.getBorderBox','utility.dom.setBorderBox','utility.dom.bringIntoView','utility.dom.putElementAt'];var tmp=null;var stmp='';for(var i=0;i<dont_overwrite_utility.length;i++){stmp=dont_overwrite_utility[i];tmp=eval(stmp);if(typeof(tmp)!='function'){eval(stmp+'=opener.'+stmp.replace(/\./g,'_'));}}
function cm_rename(elem){if(item_selection_type=="none"){alert(trans(i18n.NO_SELECTION));}
if(item_selection_type=="file"){var completeFileName=current_file.replace(/[^0-9a-z\.,;\-_\(\)\[\]\s]/gi,'_');completeFileName=completeFileName.match(/^([0-9a-z\.,;\-_\(\)\[\]\s]+)(\.[a-z0-9]+)$/i);var fileName=completeFileName[1];var valid_file_name=fileName.replace(/[^0-9a-z\.,;\-_\(\)\[\]\s]/gi,'_');var new_name=prompt(trans(i18n.RENAME_FILE,current_file),valid_file_name);while(new_name&&new_name.match(/[^0-9a-z\.,;\-_\(\)\[\]\s]/i)){new_name=new_name.replace(/[^0-9a-z\.,;\-_\(\)\[\]\s]/gi,'_');new_name=prompt(trans(i18n.INVALID_FILE_NAME),new_name);if(new_name!=null){new_name=String_trim(new_name);}}
if(new_name!=null){new_name=String_trim(new_name);}
if(new_name==fileName||!new_name){return;}
utility.window.blockInterface();ktml.makeRequest('file',"rename",{"submode":browse_submode,"filename":current_path+current_file,"new_filebasename":new_name},function(result){if(typeof result=='object'&&result.error){alert(utility.string.getInnerText(result.error.message));utility.window.unblockInterface();return;};current_selection.removeAll();current_file=result.replace(/.*?([^\/]*)$/i,"$1");refresh2();utility.window.unblockInterface();});}else{var path=current_path.replace(new RegExp("^\\/",""),'');path=path.replace(new RegExp("[\\/]$",""),'');var m=path.match(new RegExp("(.*?)([^\\/]+)$",""));if(!m){return;}
var folder=m[1];var folder_name=m[2];var valid_folder_name=folder_name.replace(/[^0-9a-z\.,;\-_\(\)\[\]\s]/gi,'_');var new_name=prompt(trans(i18n.RENAME_FOLDER,folder_name),valid_folder_name);if(!new_name||new_name==folder_name){return;}
while(new_name&&new_name.match(/[^0-9a-z\.,;\-_\(\)\[\]\s]/i)){new_name=new_name.replace(/[^0-9a-z\.,;\-_\(\)\[\]\s]/gi,'_');new_name=prompt(trans(i18n.INVALID_FOLDER_NAME),new_name);}
if(new_name!=null){new_name=String_trim(new_name);}
if(!new_name||new_name==folder_name){return;}
utility.window.blockInterface();ktml.makeRequest('folder',"rename",{"folder":folder,"submode":browse_submode,"old_foldername":folder_name,"new_foldername":new_name},function(result){if(typeof result=='object'&&result.error){alert(utility.string.getInnerText(result.error.message));utility.window.unblockInterface();return;}
current_selection.removeAll();current_path="/"+folder+new_name+"/"+current_file;update_current_path(current_path);refresh2();utility.window.unblockInterface();});}}
function cm_duplicate(elem){if(!allow_file_operations){return;}
cm_copy();cm_paste();}
function cm_rotateLeft(elem){cm_bulkImageOperations("rotate_left")}
function cm_rotateRight(elem){cm_bulkImageOperations("rotate_right")}
function cm_rotate(angle){}
function cm_copy(elem){if(!allow_file_operations){return;}
file_list.markforcut("",true);clipboard.removeAll();clipboard.add(current_selection.paths,"copy");}
function cm_delete(elem){delete_selected();}
function cm_cut(elem){if(!allow_file_operations){return;}
clipboard.removeAll();clipboard.add(current_selection.paths,"cut");file_list.markforcut(current_selection.paths,true)}
function cm_paste(elem){var i,files;if(clipboard.paths.length==0){return;}
file_list.op(clipboard.operation,clipboard.paths,"",function(err){files=[];clipboard.removeAll();current_selection.removeAll();if(err.length>0){for(i=0;i<err.length;i++){files.push(err.file);}
clipboard.add(files);current_selection.add(files);file_list.markforcut(files,true);alert(trans(i18n.CLIPBOARD_OPERATION_FAILED,trans(i18n[clipboard.operation]),files.join("\n")));}
refresh2();})}
function cm_insertThumbnailLink(){var filename=current_file;var destdir=file_list.path;window.selectedFile=file_list.get_item(filename);utility.window.blockInterface();window.resumeOperationts=cm_resumeInsertThumbnailLink;dlgWindow=utility.window.openWindow("ResizeImageParametersWindow"+parseInt(Math.random()*1000000000),KtmlRoot+"modules/filebrowser/resizeparams.html?counter="+ktml.counter,240,140);dlgWindow.purpose='insert_thumbnail';};function cm_resumeInsertThumbnailLink(dlgResult){var dialogResult={};Object_weave_safe(dialogResult,dlgResult);if(dialogResult.dlgResult){var filename=current_file;var destdir=file_list.path;var old_file_name=destdir+filename;ktml.makeRequest("file","copy",{"filename":old_file_name,"submode":browse_submode,"new_filefolder":destdir},function(copy_result){if(typeof copy_result=='object'&&copy_result.error){if(is.mozilla){closeMe=true;}
alert(utility.string.getInnerText(copy_result.error.message));utility.window.unblockInterface();return;};var old_file_name=copy_result.match(/([^\/]*)$/)[1];var new_file_name=old_file_name;var parts=filename.match(/(.*?)\.([^\.]*)$/);var file_extension='';if(parts){file_extension=parts[2];new_file_name=parts[1]+"_"+dialogResult.width+"x"+dialogResult.height;}
if(file_list.file_exists(new_file_name+"."+file_extension)){var idx=1;while(file_list.file_exists(new_file_name+"_"+idx+"."+file_extension)){idx++;}
new_file_name+="_"+idx;}
ktml.makeRequest('file',"rename",{"submode":browse_submode,"filename":destdir+old_file_name,"new_filebasename":new_file_name},function(rename_result){if(typeof rename_result=='object'&&rename_result.error){ktml.makeRequest('file',"delete",{"submode":browse_submode,"filename":destdir+old_file_name});if(is.mozilla){closeMe=true;}
alert(utility.string.getInnerText(rename_result.error.message));utility.window.unblockInterface();return;};thumbnailPath=rename_result;var params={'module':'image','method':'resize','width':dialogResult.width,'height':dialogResult.height,'keep_proportion':dialogResult.keep_proportion};var tmp_folder=thumbnailPath.replace(new RegExp("[^\\/]*$",""),"");var tmp_filename=thumbnailPath.replace(new RegExp(".*\\/",""),"");var default_params={folder:tmp_folder,rel_filename:tmp_filename};Object_weave_safe(default_params,params);ktml.makeRequest(params.module,params.method,default_params,function(resize_result){if(typeof resize_result=='object'&&resize_result.error){ktml.makeRequest('file',"delete",{"submode":browse_submode,"filename":thumbnailPath});if(is.mozilla){closeMe=true;}
alert(utility.string.getInnerText(resize_result.error.message));utility.window.unblockInterface();return;};if(is.mozilla){closeMe=true;}
var iw=parseInt(resize_result.width,10);var ih=parseInt(resize_result.height,10);var hrefImage=window.topOpener.ktml_location.server+encodeURI(currentUploadFolder+selectedFile.path).replace(/(\/\/)/gi,'/');var hrefThumbnail=window.topOpener.ktml_location.server+encodeURI(currentUploadFolder+thumbnailPath);var openIMGLocation=KtmlRoot+"modules/filebrowser/display_image.html?image="+hrefImage;var thumbIMGLocation=hrefThumbnail;var hrefIMGLocation=' kt_onclick="window.open(\''+openIMGLocation+'\', \'\', \'width='+selectedFile.width+',height='+selectedFile.height+',resizable=1,location=no,toolbar=no,scrollbars=no,status=no,titlebar=no,menubar=no,modal=yes,dependent=yes\'); return false;"';var insert_html='<a href="'+openIMGLocation+'" '+hrefIMGLocation+' target="_img"><img src="'+thumbIMGLocation+'" width="'+iw+'" height="'+ih+'" border="0" alt="'+selectedFile.name+'" title="'+selectedFile.name+'"/></a>';ktml.insertHTML(insert_html,'first-node');utility.window.unblockInterface();clickedCancel();});});});}else{utility.window.unblockInterface();dlgWindow.close();if(is.mozilla){closeMe=true;}}}
function cm_resizeImages(){var multipleSelection=(current_selection.paths&&(current_selection.paths.length>1))?true:false;window.selectedFile=!multipleSelection?file_list.get_item(current_file):null;window.resumeOperationts=cm_resumeResizeImages;var dlg=utility.window.openWindow("ResizeImageParametersWindow"+parseInt(Math.random()*1000000000),"resizeparams.html?counter="+ktml.counter,240,140);dlg.purpose='resize_image';}
function cm_resumeResizeImages(dlgResult){var dialogResult={};Object_weave_safe(dialogResult,dlgResult);if(dialogResult.dlgResult){var params={module:'image',method:'resize','width':dialogResult.width,'height':dialogResult.height,'keep_proportion':dialogResult.keep_proportion};executeBulkOperations(params);}}
function cm_compressImages(){utility.window.openWindow("CompressImageParametersWindow"+parseInt(Math.random()*1000000000),"compressparams.html?counter="+ktml.counter,220,80);}
function cm_resumeCompressImages(dlgResult){var params={module:'image',method:'degrade','degradejpeg_procent':dlgResult.degradejpeg_procent};executeBulkOperations(params);}
function cm_bulkImageOperations(op){var params={};switch(op){case"insert_thumbnail_link":cm_insertThumbnailLink();return;case"rotate_left":params={module:'image',method:'rotate','angle':'270'};break;case"rotate_right":params={module:'image',method:'rotate','angle':'90'};break;case"bright_up":params={module:'image',method:'brightness','intensity':'increase'};break;case"bright_down":params={module:'image',method:'brightness','intensity':'decrease'};break;case"contr_up":params={module:'image',method:'contrast','intensity':'increase'};break;case"contr_down":params={module:'image',method:'contrast','intensity':'decrease'};break;case"resize":cm_resizeImages();return;case"compress":cm_compressImages();return;}
executeBulkOperations(params);}
__globalNumImageOperations=0;__globalNumImageOperationsLeft=0;__globalDeleteUndoOperationsLeft=0;__globalErrorMessages=[];function executeBulkOperations(params){if(__globalNumImageOperationsLeft!=0){alert(__globalNumImageOperationsLeft);return;}
__globalNumImageOperations=0;__globalNumImageOperationsLeft=0;__globalErrorMessages=[];utility.window.blockInterface();for(var i=0;i<current_selection.paths.length;i++){__globalNumImageOperations+=isImage(current_selection.paths[i])?1:0;}
for(var i=0;i<current_selection.paths.length;i++){var torotate=current_selection.paths[i];var tmp_folder=torotate.replace(new RegExp("[^\\/]*$",""),"");var currentIsImage=isImage(torotate);if(!currentIsImage){continue;}
var tmp_filename=torotate.replace(new RegExp(".*\\/",""),"");var file=file_list.children[file_list.getChildIndexByName(tmp_filename)]
if(file){if(file.state=='error'){__globalNumImageOperations--;continue;}
file.revealed=false;file.setState("LOADING");}
__globalNumImageOperationsLeft++;var default_params={folder:tmp_folder,rel_filename:tmp_filename,submode:browse_submode};Object_weave_safe(default_params,params);ktml.makeRequest(params.module,params.method,default_params,function(__result1){__globalNumImageOperationsLeft--;if(typeof __result1=='object'&&__result1.error){__globalErrorMessages.push(__result1.error.message);}
if(__globalNumImageOperationsLeft==0){str=[];str=__globalErrorMessages.join("\n");if(str!=''){alert(utility.string.getInnerText(str));}
__globalDeleteUndoOperationsLeft=__globalNumImageOperations;for(var j=0;j<current_selection.paths.length;j++){var todelete=current_selection.paths[j];var d_folder=todelete.replace(new RegExp("[^\\/]*$",""),"");var currentIsImage=isImage(todelete);if(currentIsImage){var file=file_list.get_item(todelete.substring(d_folder.length));if(file&&file.state=='error'){continue;}
var d_filename=todelete.replace(new RegExp(".*\\/",""),"");ktml.makeRequest('image',"deleteundo",{"folder":d_folder,"submode":browse_submode,"rel_filename":d_filename},function(result2){__globalDeleteUndoOperationsLeft--;if(__globalDeleteUndoOperationsLeft==0){updatePhotosDisplay();}});}}}});}}
function refreshSelectedImage(folder,filename,doDeferredUnblock){var item=file_list.get_item(filename);if(item&&item.state=='error'){if(doDeferredUnblock){bulkRefreshNumFiles++;if(bulkRefreshNumFiles==__globalNumImageOperations){if(is.mozilla){closeMe=true;}
utility.window.unblockInterface();}}
return;}
ktml.makeRequest('image',"createpreview",{"folder":folder,"submode":browse_submode,"rel_filename":filename,'size':'small'},function(result){var item=file_list.get_item(filename);item.setState("READY");if(item.state=="error"||typeof(result)=='object'&&result.error){if(result){alert(utility.string.getInnerText(result.error.message));}
if(doDeferredUnblock){bulkRefreshNumFiles++;if(bulkRefreshNumFiles==__globalNumImageOperations){if(is.mozilla){closeMe=true;}
utility.window.unblockInterface();}}
return;}
result=result[0];item.update(result);if(doDeferredUnblock){bulkRefreshNumFiles++;if(bulkRefreshNumFiles==__globalNumImageOperations){if(is.mozilla){closeMe=true;}
utility.window.unblockInterface();}}else{if(is.mozilla){closeMe=true;}}});}
closeMe=false;function updatePhotosDisplay(){bulkRefreshNumFiles=0;for(var j=0;j<current_selection.paths.length;j++){var todelete=current_selection.paths[j];var d_folder=todelete.replace(new RegExp("[^\\/]*$",""),"");var currentIsImage=isImage(todelete);if(currentIsImage){var d_filename=todelete.replace(new RegExp(".*\\/",""),"");refreshSelectedImage(d_folder,d_filename,true);}}}
function cm_clipboardNotEmpty(elem,e){return clipboard.length();}
function cm_editImage(elem,e){image_edit();}
function cm_zoomTemplate(){var wnd=utility.window.openWindow("TemplateZoomIn"+parseInt(Math.random()*1000000000),"zoomin.html",800,600);}
function cm_notOnCanvas(elem,e){var o=utility.dom.setEventVars(e);el=o.targ;while(el.tagName.toLowerCase()!="body"&&!el.getAttribute("has_context_menu")){el=el.parentNode;}
if(el.getAttribute("has_context_menu")&&el.getAttribute("item_id")){return true;}
return false;}
function cm_isImage(elem,e){var o=utility.dom.setEventVars(e);el=o.targ;while(el.tagName.toLowerCase()!="body"&&!el.getAttribute("has_context_menu")){el=el.parentNode;}
if(el.getAttribute("has_context_menu")&&el.getAttribute("item_id")){var file=KTStorage.get(el.getAttribute("item_id"));return file.state!="error"&&isImage(file.name);}}
function cm_isTemplate(){return browse_submode=="templates"&&item_selection_type=="file";}
function cm_isFolder(){return item_selection_type=="folder"&&allow_folder_operations;}
function cm_isFile(){return item_selection_type=="file"&&allow_file_operations;}
function makeContextMenu(){var elem=document.body;var options={"uniqueID":ktml.getUniqueID(),"parentElem":elem,"containerElem":elem};var contextMenu=null;var contextMenuID=null;if(typeof(HTMLPopupMenu)!='undefined'){contextMenu=new HTMLPopupMenu(options,FileBrowserPopupMenuItems);contextMenuID=contextMenu.id;}
utility.dom.attachEvent(document.body,"oncontextmenu",function(e){var o=utility.dom.setEventVars(e);el=o.targ;while(el.tagName.toLowerCase()!="body"&&!el.getAttribute("has_context_menu")){el=el.parentNode;}
if(el.getAttribute("has_context_menu")+""=="true"){var item_id=el.getAttribute("item_id");if(item_id){var item=KTStorage.get(item_id);if(item.type!="Folder"){if(!e.ctrlKey&&item.selected!=true){KTStorage.get(item.list_id).selectChild(item.name,true);}}else{item.select();}}
if(contextMenuID){KTStorage.get(contextMenuID).show(e);}}
utility.dom.stopEvent(e);return false;});}
FileBrowserPopupMenuItems=[{"name":"Rename","action":cm_rename,"show_if":"cm_isFolder() || cm_isFile() && !multiple_selection","enable_if":true,"image":"core/img/s.gif"},{"name":"Duplicate","action":cm_duplicate,"show_if":cm_isFile,"enable_if":true,"image":"core/img/s.gif"},{"name":"Delete","action":cm_delete,"show_if":"cm_isFolder() || cm_isFile()","enable_if":true,"image":"core/img/s.gif"},{"name":"-","show_if":"cm_isFolder() || cm_isFile()","enable_if":true},{"name":"Copy","action":cm_copy,"show_if":"cm_isFile()","enable_if":cm_isFile,"image":"core/img/s.gif"},{"name":"Cut","action":cm_cut,"show_if":"cm_isFile()","enable_if":cm_isFile,"image":"core/img/s.gif"},{"name":"Paste","action":cm_paste,"show_if":"cm_isFolder() || cm_isFile() || cm_clipboardNotEmpty()","enable_if":cm_clipboardNotEmpty,"image":"core/img/s.gif"},{"name":"-","show_if":cm_isImage,"enable_if":true},{"name":"Edit image","action":cm_editImage,"show_if":cm_isImage,"action_event":"mouseup","enable_if":"has_imaging && !multiple_selection","image":"core/img/insert_image.gif"},{"name":"Insert Thumbnail","action":"cm_bulkImageOperations('insert_thumbnail_link')","show_if":cm_isImage,"action_event":"mouseup","enable_if":"img_flags.resize && !multiple_selection","image":"modules/filebrowser/img/insert_thumb.gif"},{"name":"Rotate Left","action":"cm_bulkImageOperations('rotate_left')","show_if":cm_isImage,"enable_if":"img_flags.rotate","image":"modules/filebrowser/img/rotate_left.gif"},{"name":"Rotate Right","action":"cm_bulkImageOperations('rotate_right')","show_if":cm_isImage,"enable_if":"img_flags.rotate","image":"modules/filebrowser/img/rotate_right.gif"},{"name":"Image Operations","action":"->","show_if":cm_isImage,"enable_if":"has_imaging","image":"core/img/insert_image.gif","MenuItems":[{"name":"Resize Image","action":"cm_bulkImageOperations('resize')","show_if":true,"action_event":"mouseup","enable_if":"img_flags.resize","image":"modules/filebrowser/img/resize.gif"},{"name":"Increase Brightness","action":"cm_bulkImageOperations('bright_up')","show_if":true,"enable_if":"img_flags.brightness","image":"modules/filebrowser/img/bright_up.gif"},{"name":"Decrease Brightness","action":"cm_bulkImageOperations('bright_down')","show_if":true,"enable_if":"img_flags.brightness","image":"modules/filebrowser/img/bright_down.gif"},{"name":"Increase Contrast","action":"cm_bulkImageOperations('contr_up')","show_if":true,"enable_if":"img_flags.contrast","image":"modules/filebrowser/img/contr_up.gif"},{"name":"Decrease Contrast","action":"cm_bulkImageOperations('contr_down')","show_if":true,"enable_if":"img_flags.contrast","image":"modules/filebrowser/img/contr_down.gif"},{"name":"Compress","action":"cm_bulkImageOperations('compress')","show_if":true,"action_event":"mouseup","enable_if":"img_flags.degrade","image":"modules/filebrowser/img/compress.gif"}]},{"name":"-","show_if":cm_isTemplate,"enable_if":true},{"name":"Edit Template","action":null,"show_if":null,"enable_if":"!multiple_selection && false","image":"core/img/s.gif"},{"name":"Preview","action":cm_zoomTemplate,"show_if":cm_isTemplate,"enable_if":"!multiple_selection","action_event":"mouseup","image":"core/img/s.gif"}];