
cropperPointSize={'width':4,'height':3};var sel;var zf=1;function zoom(where,btn){if(btn&&btn.className=='btn_disabled'){return false;}
show();if(typeof(where)!="undefined"){switch(where){case"in":zf*=1.1;break;case"out":zf=zf/1.1;break;case"clear":zf=1;setButtonState("btn_zoom_in",true);setButtonState("btn_zoom_out",true);setButtonState("btn_zoom_clear",false);if(zImage.width==0){xRatio=yRatio=0;return;}
zImage.style.width="";zImage.style.height="";zCropperImage.style.width="";zCropperImage.style.height="";zShadow.style.width=(zImage.width-(is.ie?cropperPointSize.width*2:0))+"px";zShadow.style.height=(zImage.height-(is.ie?cropperPointSize.height*2:0))+"px";zImageBox=utility.dom.getBBox(zImage);xRatio=editImageWidth/(zImageBox.width-2*cropperPointSize.width);yRatio=editImageHeight/(zImageBox.height-2*cropperPointSize.height);return;default:zf=where;}}
var ziW=parseInt(origw*zf*1.1);var ziH=parseInt(origh*zf*1.1);var zoW=parseInt(origw*zf/1.1);var zoH=parseInt(origh*zf/1.1);setButtonState("btn_zoom_in",ziW<10000&&ziH<10000);setButtonState("btn_zoom_out",zoW>100&&zoH>100);setButtonState("btn_zoom_clear",zf!=1);var newW=parseInt(origw*zf);var newH=parseInt(origh*zf);zImage.style.width=newW+"px";zImage.style.height=newH+"px";zShadow.style.width=newW+"px";zShadow.style.height=newH+"px";zCropperImage.style.width=newW+"px";zCropperImage.style.height=newH+"px";zImageBox=utility.dom.getBBox(zImage);xRatio=editImageWidth/(zImageBox.width-2*cropperPointSize.width);yRatio=editImageHeight/(zImageBox.height-2*cropperPointSize.height);if(is.mozilla){zCanvas.style.width=(zCanvas.offsetWidth-1)+"px";zCanvas.style.width=(zCanvas.offsetWidth+1)+"px";}}
function ImageCropper(el,kp,textwidth,textheight,textx,texty){this.container=el;this.container.onselectstart=function(){return false;};rhn=document.getElementById("resize_handle_north");rhs=document.getElementById("resize_handle_south");rhw=document.getElementById("resize_handle_west");rhe=document.getElementById("resize_handle_east");this.image=zImage;this.imageBox=utility.dom.getBBox(zImage);zShadowBox=utility.dom.getBBox(zShadow);this.selection=zCropper;zShadow.style.width=(this.image.width-(is.ie?cropperPointSize.width*2:0))+"px";zShadow.style.height=(this.image.height-(is.ie?cropperPointSize.height*2:0))+"px";xRatio=editImageWidth/(this.imageBox.width-2*cropperPointSize.width);yRatio=editImageHeight/(this.imageBox.height-2*cropperPointSize.height);this.image.GALLERYIMG="no";this.points=[];this.kp=kp;this.textwidth=textwidth;this.textheight=textheight;this.textx=textx;this.texty=texty;this.pointsize=cropperPointSize;this.initialize();}
ImageCropper.prototype.initialize=function(){var _this=this;if(this.kp){if(this.kp.checked==false){this.keep=false;}else{this.keep=true;}
utility.dom.attachEvent(this.kp,'click',KeepCheck,1,false);}else{this.keep=false;}
sel={x:0,y:0,w:0,h:0};utility.dom.attachEvent(zShadow,'click',function(){_this.hide();},1,false);DNDDom.dhtml.setMovable("theImage",DND_HANDLE_FOR+"theSelection");zImage.onStartMove=function(e,m){_this.imageBox=utility.dom.getBBox(_this.image);sel={x:e.clientX,y:e.clientY,w:0,h:0}
m.FLBox=utility.dom.getBBox(zImage);if(e.clientY-cropperPointSize.height<=m.FLBox.y||e.clientX-cropperPointSize.width<=m.FLBox.x){return false;}
if(e.clientX>=m.FLBox.x+m.FLBox.width-cropperPointSize.width||e.clientY>=m.FLBox.y+m.FLBox.height-cropperPointSize.height){return false;}
m.FLBox.height-=cropperPointSize.height;m.FLBox.width-=cropperPointSize.width;zShadowBox=utility.dom.getBBox(zShadow);m.currentX=m.startX=e.clientX;m.currentY=m.startY=e.clientY;m.scrollX=zCanvas.scrollLeft;m.scrollY=zCanvas.scrollTop;m.control="southeast";zSelection.style.width=0+"px";zSelection.style.height=0+"px";putElOverBox(zSelection,m.currentX,m.currentY,m.FLBox);zSelection.style.display="block"
return true;}
zImage.onMove=function(e,m){m.currentX=e.clientX;m.currentY=e.clientY;putElOverBox(zSelection,m.startX,m.startY,m.FLBox);var d=fixDelta(m,sel);zSelectionBox=utility.dom.getBBox(zSelection);var ow=d.w;var oh=d.h;d=fitBoxInBox(zSelectionBox,m.FLBox,sel,"southeast",d);m.currentX+=d.w-ow;m.currentY+=d.h-oh;zSelection.style.width=(sel.w+d.w)+"px"
zSelection.style.height=(sel.h+d.h)+"px";}
zImage.onEndMove=function(e,m){sel.x=m.startX;sel.w=m.currentX-m.startX;sel.y=m.startY;sel.h=m.currentY-m.startY;sel.scrollLeft=zCanvas.scrollLeft;sel.scrollTop=zCanvas.scrollTop;zSelection.style.display="none";if(sel.w<3&&sel.h<3){return;}
d={};d.x=0;d.y=0;d.w=0;d.h=0;paintLayout(sel,d,m.FLBox);_this.show();show('crop');}
DNDDom.dhtml.setMovable("theCropper");zCropper.onStartMove=function(e,m){var o=utility.dom.setEventVars(e);var mtch=o.targ.id.match(/resize_handle_([^_]*)/i);if(mtch){m.control=mtch[1];}else{m.control="move";}
m.FLBox=utility.dom.getBBox(zImage);zShadowBox=utility.dom.getBBox(zShadow);m.currentX=m.startX=e.clientX;m.currentY=m.startY=e.clientY;sel.x+=sel.scrollLeft-zCanvas.scrollLeft;sel.y+=sel.scrollTop-zCanvas.scrollTop;sel.scrollLeft=zCanvas.scrollLeft;sel.scrollTop=zCanvas.scrollTop;m.scrollX=zCanvas.scrollLeft;m.scrollY=zCanvas.scrollTop;var d=fixDelta(m,sel);zCropperBox=utility.dom.getBBox(zCropper);d=fitBoxInBox(zCropperBox,zShadowBox,sel,m.control,d);paintLayout(sel,d,m.FLBox);return true;}
zCropper.onMove=function(e,m){m.currentX=e.clientX;m.currentY=e.clientY;var d=fixDelta(m,sel);zCropperBox=utility.dom.getBBox(zCropper);d=fitBoxInBox(zCropperBox,zShadowBox,sel,m.control,d);paintLayout(sel,d,m.FLBox);if(is.mozilla){zCanvas.style.width=(zCanvas.offsetWidth-1)+"px";zCanvas.style.width=(zCanvas.offsetWidth+1)+"px";}
_this.setValue();}
zCropper.onEndMove=function(e,m){m.currentX=e.clientX;m.currentY=e.clientY;var d=fixDelta(m,sel);zCropperBox=utility.dom.getBBox(zCropper);d=fitBoxInBox(zCropperBox,zShadowBox,sel,m.control,d);sel.x=sel.x+d.x;sel.y=sel.y+d.y;sel.w=sel.w+d.w;sel.h=sel.h+d.h;_this.setValue();}}
ImageCropper.prototype.initialCrop=function(){var img=utility.dom.getBBox(zImage);zCropperImage.GALLERYIMG="no";sel.scrollLeft=0;sel.scrollTop=0;sel.x=img.x+5;sel.y=img.y+4;sel.w=(img.width>98)?98:img.width/2;sel.h=(img.height>98)?98:img.height/2;d={};d.x=0;d.y=0;d.w=0;d.h=0;paintLayout(sel,d,img);this.show();show('crop');}
function paintLayout(sel,d,box){putElOverBox(zCropper,sel.x+d.x,sel.y+d.y,box);zCropper.style.width=(sel.w+d.w)+"px"
zCropper.style.height=(sel.h+d.h)+"px";rhn.style.left=(sel.w+d.w-8)/2+"px";rhs.style.left=(sel.w+d.w-8)/2+"px";rhw.style.top=(sel.h+d.h-8)/2+"px";rhe.style.top=(sel.h+d.h-8)/2+"px";zCropperImage.style.clip="rect("+(sel.y+d.y-box.y)+"px "+(sel.x+d.x+sel.w+d.w-box.x)+"px "+(sel.y+d.y+sel.h+d.h-box.y)+"px "+(sel.x+d.x-box.x)+"px)";zCropperImage.style.left=(-sel.x-d.x+box.x)+"px"
zCropperImage.style.top=(-sel.y-d.y+box.y)+"px";}
function fixDelta(m,sel){var d={}
d.x=m.currentX-m.startX+(zCanvas.scrollLeft-m.scrollX);d.y=m.currentY-m.startY+(zCanvas.scrollTop-m.scrollY);d.w=0;d.h=0;switch(m.control){case"move":break;case"north":d.x=0;d.h=-d.y;break;case"east":d.w=d.x;d.x=0;d.y=0;break;case"south":d.x=0;d.h=d.y;d.y=0;break;case"west":d.w=-d.x;d.y=0;break;case"southeast":d.w=d.x;d.x=0;d.h=d.y;d.y=0;break;}
return d;}
function putElOverBox(el,x,y,box){el.style.left=(x-box.x-1)+"px";el.style.top=(y-box.y-1)+"px";}
function fitBoxInBox(b1,b2,sel,direction,d){if(sel.x+d.x<b2.x+1){d.x=b2.x-sel.x+1;if(direction!="move"){d.w=-d.x;}}
if(sel.w+d.w<0){d.w=-sel.w;d.x=direction=="west"?sel.w:0;}
if(sel.y+d.y<b2.y+1){d.y=b2.y-sel.y+1;if(direction!="move"){d.h=-d.y;}}
if(sel.h+d.h<0){d.h=-sel.h;d.y=direction=="north"?sel.h:0;}
if(sel.x+sel.w+d.x+d.w>b2.x+b2.width-1){if(direction=="move"){d.x=b2.x+b2.width-1-sel.x-sel.w;}else{d.w=b2.x+b2.width-1-sel.x-sel.w;}}
if(sel.y+sel.h+d.y+d.h>b2.y+b2.height-1){if(direction=="move"){d.y=b2.y+b2.height-1-sel.y-sel.h;}else{d.h=b2.y+b2.height-1-sel.y-sel.h;}}
return d;}
function DragHelper(image_cropper){this.owner=image_cropper;this.sx=0;this.sy=0;this.dragging=false;this.down=false;}
ImageCropper.prototype.resizeTo=function(x,y,w,h){if(w<0){w=-w;x-=w;}
if(h<0){h=-h;y-=h;}
this.selection.style.left=x+'px';this.selection.style.top=y+'px';if(is.ie){this.selectionImage.style.width="0px";this.selectionImage.style.height="0px";}
this.selection.style.width=w+'px';this.selection.style.height=h+'px';}
ImageCropper.prototype.setBars=function(){this.setValue();}
ImageCropper.prototype.setValue=function(){var selection_pos=utility.dom.getBBox(this.selection);var width=selection_pos.width;var height=selection_pos.height;width=Math.round(width*xRatio);height=Math.round(height*yRatio);var x1=selection_pos.x;var y1=selection_pos.y;var fix=is.ie?1:0;x1=x1-this.imageBox.x-cropperPointSize.width+fix;y1=y1-this.imageBox.y-cropperPointSize.height+fix;if(this.textx){this.textx.value=Math.round(x1*xRatio);}
if(this.texty){this.texty.value=Math.round(y1*yRatio);}
this.textwidth.value=width;this.textheight.value=height;}
ImageCropper.prototype.hide=function(){zShadow.style.display="none";zCropperImage.style.display="none";zCropper.style.display="none";}
ImageCropper.prototype.show=function(){zShadow.style.display="block";zShadowBox=utility.dom.getBBox(zShadow);zCropper.style.display="block";zCropperImage.style.display="block";this.setValue();}
function KeepCheck(e){var o=utility.dom.setEventVars(e);var jso=cropObj;if(o.targ.checked){jso.proportion=jso.selection.offsetWidth/jso.selection.offsetHeight;jso.keep=true;}else{jso.keep=false;}}
function restrictValue(value,min,max){if(value<=min){return min;}else if(value>=max){return max;}else{return value;}}