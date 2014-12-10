<%@ Page Language="C#" %>
<%@ Register TagPrefix="aspxform" Namespace="XFormDesigner.Framework.Web.UI" Assembly="XFormDesigner.Framework" %>
<script runat="server">

    // Insert page code here
    //

</script>
<html xmlns:xform="xmlns:xform">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8" />
    <title>BPM Form</title> <style>
    BODY {FONT-SIZE: 12px; FONT-FAMILY: verdana}
    TABLE {border-collapse: collapse; FONT-SIZE: 12px; FONT-FAMILY: verdana}
    P {PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; MARGIN: 0px; PADDING-TOP: 0px; FONT-SIZE: 12px; FONT-FAMILY: verdana}
    TD {PADDING-RIGHT: 0px; PADDING-LEFT: 2px; PADDING-BOTTOM: 0px}
    TD.NoPadding {PADDING-RIGHT: 0px; PADDING-LEFT: 0px; PADDING-BOTTOM: 0px; PADDING-TOP: 0px}
    INPUT {HEIGHT: 20px}
    INPUT.UL {BORDER-RIGHT: medium none; BORDER-TOP: medium none; BORDER-LEFT: medium none; BORDER-BOTTOM: #33ff33 1px solid}
    TEXTAREA {FONT-SIZE:12px}
    </style>
</head>
<body>
    <form runat="server">
        <!-- Insert content here -->
        <table style="BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none" cellspacing="0" cellpadding="0" width="801" align="center" border="0">
            <tbody>
                <tr>
                    <td style="BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none" rowspan="2" width="202">
                        <asp:Image id="Image1" runat="server" BorderStyle="None" ImageUrl="/BPM/YZSoft/Forms/Helps/Images/haili.jpg" Height="50px" Width="200px"></asp:Image>
                    </td>
                    <td style="BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none" rowspan="2" width="397" align="center">
                        <font size="6" face="宋体"><strong>快递申请单
</strong></font> 
                    </td>
                    <td style="BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none" height="39" colspan="2">
                        &nbsp; 
                        <div align="right">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td height="30" width="97">
                        <p align="right">
                            <font color="blue"><strong></strong></font>
                        </p>
                    </td>
                    <td width="97">
                    </td>
                </tr>
                <tr>
                    <td height="30">
                    </td>
                    <td>
                    </td>
                    <td>
                        <p align="right">
                            <strong><font color="#0000ff">单号：</font></strong> 
                        </p>
                    </td>
                    <td>
					<!-- LF_ProgramDevCheck.SN -->
                        <aspxform:XTextBox id="XTextBox1" runat="server" BorderStyle="None" BorderColor="MintCream" BorderWidth="1px" width="100%" HiddenInput="False" XDataBind="ExpressTab.SN
" Max="0" Min="0" ForeColor="Blue" TextAlign="Center" ValueToDisplayText Value ReadOnly="True">自动生成</aspxform:XTextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <table style="BORDER-TOP: medium none; BORDER-RIGHT: medium none; BORDER-BOTTOM: medium none; BORDER-LEFT: medium none" cellspacing="0" cellpadding="0" width="800" align="center" border="0">
            <tbody>
			<tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid; BORDER-LEFT: silver 1px solid" bgcolor="#e0e0e0" height="30"  colspan="8">
                        <strong><font size="2">&nbsp;申请人信息</font></strong></td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       申请人 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabproposerStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.proposerStr"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       公司 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabcompanyStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.companyStr"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       部门 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabpartStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.partStr"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       日期 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XDateTimePicker id="ExpressTabCteateDat" runat="server" BorderStyle="None" Width="100%" BorderColor="#DCDCDC" BorderWidth="1" XDataBind="ExpressTab.CteateDat"></aspxform:XDateTimePicker> </td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid; BORDER-LEFT: silver 1px solid" bgcolor="#e0e0e0" height="30"  colspan="8">
                        <strong><font size="2">&nbsp;寄件人信息</font></strong></td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       寄件人 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabshipperStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.shipperStr"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       电话 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabshipperTelStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.shipperTelStr"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       固定电话 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabshipperPhoneStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.shipperPhoneStr"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       邮编 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabshipperPostStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.shipperPostStr"></aspxform:XTextBox> </td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       单位名称 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="7">
                       <aspxform:XTextBox id="ExpressTabshipperUnit7Str" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.shipperUnit7Str"></aspxform:XTextBox> </td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       寄件人地址 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="7">
                       <aspxform:XTextBox id="ExpressTabshipperAdd7Str" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.shipperAdd7Str"></aspxform:XTextBox> </td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid; BORDER-LEFT: silver 1px solid" bgcolor="#e0e0e0" height="30"  colspan="8">
                        <strong><font size="2">&nbsp;收件人信息</font></strong></td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       收件人 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabtoStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.toStr"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       电话 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabTelStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.TelStr"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       固定电话 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabPhoneStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.PhoneStr"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       邮编 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabPostStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.PostStr"></aspxform:XTextBox> </td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       单位名称 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="7">
                       <aspxform:XTextBox id="ExpressTabunit7Str" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.unit7Str"></aspxform:XTextBox> </td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       详细地址 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="7">
                       <aspxform:XTextBox id="ExpressTabadd7Str" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.add7Str"></aspxform:XTextBox> </td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid; BORDER-LEFT: silver 1px solid" bgcolor="#e0e0e0" height="30"  colspan="8">
                        <strong><font size="2">&nbsp;发货信息</font></strong></td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       物品内容 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="7">
                       <aspxform:XTextBox id="ExpressTabthing7Str" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.thing7Str"></aspxform:XTextBox> </td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       数量 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabquantityInt" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.quantityInt"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       重量 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabweightDec" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.weightDec"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       付款方式 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       现付、到付 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       快递方式 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabexpressTypeStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.expressTypeStr"></aspxform:XTextBox> </td></tr><tr><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       发货件数 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabeQuanInt" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.eQuanInt"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       计费重量 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabeWeightDec" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.eWeightDec"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       运费 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabTotalDec" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.TotalDec"></aspxform:XTextBox> </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       取货人 </td><td style="BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid; BORDER-BOTTOM: silver 1px solid;text-align:center; BORDER-LEFT: silver 1px solid"  height="30" colspan="1">
                       <aspxform:XTextBox id="ExpressTabebodyStr" runat="server" BorderStyle="None" BorderColor="#DCDCDC" BorderWidth="1" width="100%"  XDataBind="ExpressTab.ebodyStr"></aspxform:XTextBox> </td></tr>
            </tbody>
        </table>
		<table style="BORDER-BOTTOM: medium none; BORDER-LEFT: medium none; BORDER-TOP: medium none; BORDER-RIGHT: medium none" border="0" cellspacing="0" cellpadding="0" width="800" align="center">
            <tbody>
                <tr>
                    <td style="BORDER-BOTTOM: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid" bgcolor="#e0e0e0" height="30" width="800">
                        <strong><font size="2">&nbsp; 审核意见</font></strong></td>
                </tr>
                <tr>
                    <td style="BORDER-BOTTOM: silver 1px solid; BORDER-LEFT: silver 1px solid; BORDER-TOP: medium none; BORDER-RIGHT: silver 1px solid">
                        <div align="center">
                            <aspxform:XCommentsTextBox id="XCommentsTextBox1" runat="server" Height="60px" BorderStyle="None" width="100%" BorderWidth="1" BorderColor="#DCDCDC" TextMode="MultiLine" Rows="3"></aspxform:XCommentsTextBox>
                        </div>
                    </td>
                </tr>
            </tbody>
        </table>

        <div align="center">
            <aspxform:XSignTrace id="XSignTrace2" runat="server" BorderStyle="None" Width="800px" BorderColor="#dcdcdc" BorderWidth="1"></aspxform:XSignTrace>
            <aspxform:XProcessButtonList id="XProcessButtonList1" runat="server" BorderStyle="None"></aspxform:XProcessButtonList>
        </div>
    </form>
</body>
</html>
