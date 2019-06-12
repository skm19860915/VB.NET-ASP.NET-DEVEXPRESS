<%@ Page Language="VB" AutoEventWireup="false" Inherits="Klemcoll.Website.Default2" EnableEventValidation="false" Codebehind="AddEditImage.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Prints Order Form</title>
    <link href="Styles.css" rel="stylesheet" type="text/css" />
    <script language="javascript">

var SelectedPapertype ;

function SavePapertype() {
    SelectedPapertype = document.getElementById("PaperTypeDropDown").selectedIndex;
    size_pricing(false, null);
}

function SetBW() {
    var myControl ;
    myControl = document.getElementById("PaperTypeDropDown");
    // Empty the drop down box of any choices
    for (var q=myControl.options.length;q>=0;q--) myControl.options[q]=null;
    
    SetItem(myControl, 'Pearl RC', 'Pearl RC (default)');
    SetItem(myControl, 'Glossy RC', 'Glossy RC');
    //SetItem(myControl, 'Semigloss Fiber', 'Semigloss Fiber');
    SetItem(myControl, 'Pearl Fiber', 'Pearl Fiber');
    
    if (SelectedPapertype != null) {
        if (SelectedPapertype <= 3) {
            myControl.selectedIndex = SelectedPapertype ;
        }
    } else if (document.getElementById("SelectedPaperType").value != '') {
        //myControl.value = document.p1.SelectedPaperType.value;
        //alert(document.p1.SelectedPaperType.value);
        for (var intI = 0; intI < myControl.options.length -1; intI++) {
            //alert(myControl.options[intI].value);
            if (myControl.options[intI].value == document.getElementById("SelectedPaperType").value) {
                myControl.options[intI].selected = true;
            }
        }

    }
    
}

function SetColor() {
    var myControl ;
    myControl = document.getElementById("PaperTypeDropDown");
    // Empty the drop down box of any choices
    for (var q=myControl.options.length;q>=0;q--) myControl.options[q]=null;
    
    SetItem(myControl, 'Glossy', 'Glossy RC (default for size 8 x 10)');
    SetItem(myControl, 'Pearl RC', 'Pearl RC (default for larger sizes)');
    SetItem(myControl, 'Pearl Fiber', 'Pearl Fiber');
    
    if (SelectedPapertype != null) {
        if (SelectedPapertype <= 1) {
            myControl.selectedIndex = SelectedPapertype ;
        }
    } else if (document.getElementById("SelectedPaperType").value != '') {
        myControl.value = document.getElementById("SelectedPaperType").value;
    
    }
       
}
    

function SetItem(control, value, text) {
    myEle = document.createElement("option") ;
    myEle.setAttribute('value', value);
        txt = document.createTextNode(text);
        myEle.appendChild(txt);
        control.appendChild(myEle);

}

function no_edit_onFocus()
	{
		price = document.getElementById("Price").value	//read and save price for resetting onChange by user
	}

	function no_edit_onChange()
	{
		//prevent users from trying to change price manually
		if (document.getElementById("Price").value == price)	//no need to reset price
			return
		else
		{
			document.getElementById("Price").value = price	//reset price, javascript sets field onChange set to true
			alert("Price calculations are automatic. There is no need to edit this field.")
			document.p1.ref.focus() //move cursor to another field
		}
	}

// -->

    function size_changing() {
        if (document.getElementById("PaperTypeDropDown").options.length < 4) {
            if (document.getElementById("size").value == "8 x 10") {
                document.getElementById("PaperTypeDropDown").selectedIndex = 0
            }
            else {
                var ref = document.getElementById('ref').value
                oRef = new String(ref)
                fL = oRef.substring(0, 1).toLowerCase(); 	//fL is first letter of ref string
                lL = oRef.substring(oRef.length - 2, oRef.length).toLowerCase();
                lL2 = oRef.substring(oRef.length - 3, oRef.length).toLowerCase();

                if (fL != 'c' || lL == 'bw' || lL2 == 'bwc') {
                    document.getElementById("PaperTypeDropDown").selectedIndex = 0
                }
                else {
                    document.getElementById("PaperTypeDropDown").selectedIndex = 1
                }
            }
        }
    }

    var imageRefMessageCount = 0;
    function size_pricing(resetPaperType, sizechanging)
	{
	
	    var size
		//var finish
		var ref, oRef, fL, lL, lL2, bwOption, Color,paperType
		var color_add
		var i

		//size = document.p1.size.options[0].selected
		size = document.getElementById('size').value
		//finish = document.getElementById('finish').value
		ref = document.getElementById('ref').value		//image reference number
		qty = document.getElementById('qty').value
		if (document.getElementById("PaperTypeDropDown").selectedIndex > 0) {
		    paperType = document.getElementById("PaperTypeDropDown").options[document.getElementById("PaperTypeDropDown").selectedIndex].value
		} else {
		    paperType = " "
		}
		jsEn = document.getElementById('javascriptEnabled').value;
		if (qty <= 0)
		{
			alert("Quantity must be greater than 0.")
			document.p1.qty.value = 1
			return false
		}
		
		

		oRef = new String(ref)
		fL = oRef.substring(0,1).toLowerCase();		//fL is first letter of ref string
		lL = oRef.substring(oRef.length - 2, oRef.length).toLowerCase();
		lL2 = oRef.substring(oRef.length - 3, oRef.length).toLowerCase();


		
//      REMOVED 11/23/2007 by Emanuel Fuentes per Peter Sax's request that suffixes not influence image pricing.
//		if (lL == 'bw' || lL2 == 'bwc') {
//		        bwOption = true
//		    }else{
//		        bwOption = false
//		   }
		   
		//add $75 per print for color prints, if color finish selected make sure
		//reference number begins with a 'c' or 'C', a reference number may begin
		//with a 'C' even if black and white finish selected but color pricing is used
		 if (fL != 'c') 
			 {
			 // Image doesn't start with a C
			 Color = false;
			 color_add = -10;
	        }
	     else
	        {
			 // refid starts with a C
	         if (lL == 'bw' || lL2 == 'bwc')
			    {
			    Color = false;
			    color_add = 30;
			    }
			 else
			    {
			    Color = true;
			    color_add = 30;
			    }
	    }
	    
	    if (resetPaperType) {
	        if (Color == true) {
	                SetColor();
	            } else {
	                SetBW();
	        }
		}	 			
		//alert("fL = " + fL + "  color_add = " + color_add + "  size = " + size + "  finish = " + finish)
		switch (size)
		{
			case "8 x 10" :
//			    if (paperType.indexOf('Fiber') != -1) {
//			        document.getElementById('Price').value = (85 + color_add) * qty;
//				} else {
//				    if (Color == false){
//				        color_add = 5;
//				    }
//				    document.getElementById('Price').value = (60 + color_add) * qty;
//				}
				document.getElementById('Price').value = 50 * qty
				break;

			case "11 x 14" :
//			    if (paperType.indexOf('Fiber') != -1) {
//			        if (Color == false){color_add = 5}
//				    document.getElementById('Price').value = (245 + color_add) * qty;
//				} else {
//				    if (Color == false){color_add = -20}
//				    document.getElementById('Price').value = (220 + color_add) * qty;
//				}
			    document.getElementById('Price').value = 100 * qty
				break;

			case "16 x 20" :
//			    if (paperType.indexOf('Fiber') != -1) {
//			        if (Color == false){color_add = 5}
//				    document.getElementById('Price').value = (345 + color_add) * qty;
//				} else {
//				    if (Color == false){color_add = -20}
//				    document.getElementById('Price').value = (320 + color_add) * qty;
//				}
			    document.getElementById('Price').value = 150 * qty
				break;

			case "Other" :
				document.getElementById('Price').value = "Will Quote";
				break;

			default : document.getElementById('Price').value = "50";
		}

if (ref == "") {
    if (imageRefMessageCount == 0) {
        alert("Please enter an Image Reference Number.")
        document.getElementById('ref').focus()
        imageRefMessageCount += 1;
    }
    return false
} else {

}
	    		
		if (document.getElementById("Price").value == "") {
		    // set a default price in case the price is blank.
		    docuement.getElementById("Price").value = "50"
		}
		
		if (document.getElementById("Border").value == 'Other') {
		    document.getElementById("Price").value = "Will Quote"
		}
		if (sizechanging != null) {
		    size_changing();
		}

}

        function popup(mylink, windowname)
        {
        if (! window.focus)return true;
        var href;
        if (typeof(mylink) == 'string')
           href=mylink;
        else
           href=mylink.href;
        window.open(href, windowname, 'width=400,height=400,scrollbars=no');
        return false;
        }
    </script>
</head>
<body onload="size_pricing(true, null);">
    <form id="p1" runat="server">
    <div>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 846px">
            <tr>
                <td colspan="1" style="height: 186px">
                    <img alt="The Klemantaski Collection, a library of motorsport photography" src="Images/Head_Main.jpg" /></td>
            </tr>
            <tr>
                <td style="background-color: #FFFFFF;" align="center">
                    <br />
    <table style="width: 700px; height: 100%" class="OrderFormTable">
        <tr>
            <td colspan="4" class="OrderFormSubTitle">
                The Klemantaski Collection - Prints Order Form</td>
        </tr>
        <tr>
            <td class="OrderFormCell" style="width: 208px; font-weight: bold; text-align: right;">
                Image Reference Number</td>
            <td style="width: 245px; text-align: left;">
                <asp:TextBox ID="ref" runat="server" onBlur="size_pricing(true, null)"></asp:TextBox></td>
            <td class="OrderFormCell" style="font-weight: bold; width: 102px" align="right">
                Quantity</td>
            <td class="OrderFormCell" style="font-weight: bold;" align="right">
                Price</td>
        </tr>
        <tr>
            <td class="OrderFormCell" style="width: 208px; font-weight: bold; text-align: right;">
                Paper Sizes</td>
            <td style="width: 245px; text-align: left;">
                <asp:DropDownList ID="size" runat="server" Width="224px" onChange="size_pricing(true, 1);">
                    <asp:ListItem Selected="True" Value="8 x 10">8 x 10</asp:ListItem>
                    <asp:ListItem Value="11 x 14">11 x 14</asp:ListItem>
                    <asp:ListItem Value="16 x 20">16 X 20</asp:ListItem>
                    <asp:ListItem Value="Other">Other: Provide Special Instructions</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 102px" align="right">
                <asp:TextBox ID="qty" runat="server" Width="32px" MaxLength="3" onChange="size_pricing(false, null)">1</asp:TextBox></td>
            <td align="right">
                <asp:TextBox ID="Price" runat="server" Width="64px" onChange="no_edit_onChange()" onFocus="no_edit_onFocus()"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="OrderFormCell" style="width: 208px; font-weight: bold; text-align: right;">
                Paper Type
            </td>
            <td style="width: 245px; text-align: left;">
                <asp:DropDownList ClientIDMode="Static" ID="PaperTypeDropDown" runat="server" Width="224px" 
                    onChange="SavePapertype();" >
                </asp:DropDownList></td>
            <td colspan="2">
                <asp:Label ID="StatusLabel" runat="server" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <td class="OrderFormCell" style="width: 208px; font-weight: bold; text-align: right;">
                Border</td>
            <td style="width: 245px; text-align: left;">
                <asp:DropDownList ID="Border" runat="server" Width="224px" onChange="size_pricing(false, null)">
                    <asp:ListItem Selected="True" Value="Yes: Standard">Yes: Standard</asp:ListItem>
                    <asp:ListItem Value="None">None: Provide Special Instructions</asp:ListItem>
                    <asp:ListItem Value="Other">Other: Provide Special Instructions</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 102px">
            </td>
            <td style="width: 95px">
            </td>
        </tr>

        <tr>
            <td class="OrderFormCell" style="width: 208px; font-weight: bold; text-align: right;">
                Toning for B&W Only</td>
            <td style="width: 245px; text-align: left;">
                <asp:DropDownList ID="Toning" runat="server" Width="224px" onChange="size_pricing(false, null)">
                    <asp:ListItem Value="No">No (default)</asp:ListItem>
                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                </asp:DropDownList></td>
            <td style="width: 102px"><a href="/ContentPages/ToningPopup.htm" onclick="return popup(this, 'toningpopup')">What is this?</a>
            </td>
            <td style="width: 95px">
            </td>
        </tr>

        <tr>
            <td class="OrderFormCell" style="font-weight: bold; vertical-align: top;" colspan="2">
                Special Instructions / Memo</td>
            <td style="text-align: right" colspan="2">
                </td>
        </tr>
        <tr>
            <td colspan="3" rowspan="2">
                <asp:TextBox ID="MemoTextBox" runat="server" Height="200px" TextMode="MultiLine" Width="432px"></asp:TextBox>
            </td>
            <td style="text-align: right; vertical-align: top;"><asp:Button ID="SaveButton" runat="server" CssClass="ButtonClass" Text="Add to Cart" Width="104px" /><br /><br /><asp:Button ID="CancelButton" runat="server" CssClass="ButtonClass" Text="Cancel" Width="104px" /></td>
        </tr>
        <tr>
            <td style="width: 95px">
            </td>
        </tr>
    </table>
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="1" style="text-align: center;">
                    </td>
            </tr>
        </table>
        &nbsp;
        <asp:HiddenField ID="javascriptEnabled" runat="server" />
        <asp:HiddenField ID="SelectedPaperType" runat="server" />
    </div>
    </form>
</body>
</html>

