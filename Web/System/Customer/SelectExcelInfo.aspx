<%@ Page Language="c#" CodeBehind="SelectExcelInfo.aspx.cs" AutoEventWireup="false"
    Inherits="PEIS.Web.System.Customer.SelectExcelInfo" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>从客户名单中选择表</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Css/style.css" type="text/css" rel="stylesheet">
    <link href="../Commons/Css/MrDocStyle.css" type="text/css" rel="stylesheet">
    <style type="text/css">
        TABLE
        {
            font-family: 宋体;
            font-size: 9pt;
        }
    </style>
    <script src="/template/blue/js/jquery.min.js"></script>
    <script src="/template/blue/js/artDialog4.1.7/artDialog.source.js?skin=default"></script>
    <script src="/template/blue/js/Commom.js"></script>
    <script language="javascript">
        //        function Cancel() {
        //            parent.dialogArguments.ReturnValue = "cancel";
        //            window.close();
        //        }
        function SetTemplageDownloadUrl() {
            var TemplateType = document.getElementById("DropDownFileFormat").value;

            if (TemplateType == "1") {
                document.getElementById("TdTemplate2").style.display = "none";
                document.getElementById("TdTemplate1").style.display = "block";
            }
            else {
                document.getElementById("TdTemplate1").style.display = "none";
                document.getElementById("TdTemplate2").style.display = "block";
            }
            //
        }
        //        function DoSelect() {
        //            parent.dialogArguments.ReturnValue = "ok";
        //            parent.dialogArguments.WorkSheetName = document.getElementById("ddlSelectExcelInfo").value;
        //            window.close();
        //        }
        function Cancel() {
            var data = { ExcelFilePath: "", ReturnValue: "cancel", WorkSheetName: "", FileFormat: "" };
            parent.parent.art.dialog.data("aExcelFileInfo", data);
            parent.parent.art.dialog.get('OperWindowFrame1').close();
        }
        function DoSelect() {
            var data = { ExcelFilePath: "", ReturnValue: "ok", WorkSheetName: document.getElementById("ddlSelectExcelInfo").value, FileFormat: "" };
            parent.parent.art.dialog.data("aExcelFileInfo", data);
            parent.parent.art.dialog.get('OperWindowFrame1').close();
        }
    </script>
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table id="Table2" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td valign="middle" align="center">
                <table id="Table1" cellspacing="1" cellpadding="1" align="center" border="0">
                    <tr>
                        <td style="height: 37px; font-size: 14px" align="center" height="37">
                            导入
                            <asp:Label ID="lblType" Style="font-size: 14px; font-weight: bold" runat="server"></asp:Label>&nbsp;Excel文件
                            <hr>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <font face="宋体">
                                <table id="Table3" cellspacing="1" cellpadding="1" border="0">
                                    <tr>
                                        <td>
                                            选择表名：
                                        </td>
                                        <td>
                                            <select id="ddlSelectExcelInfo" runat="server" name="ddlSelectExcelInfo" style="width: 166px">
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <font face="宋体">
                                <br>
                                &nbsp;
                                <%--<a class="MrDocButton" id="btnImportX" style="width: 70px; height: 23px; text-decoration: none"
                                    onclick="DoSelect();" href="#" ms_positioning="FlowLayout">导入文件</a>
                                --%>
                                <input type="button" onclick="DoSelect();" value="导入文件" />
                                <asp:LinkButton ID="lbImport" runat="server"></asp:LinkButton>&nbsp;
                                <%--<a class="MrDocButton"
                                    id="btnCancel" style="width: 70px; height: 23px; text-decoration: none" onclick="Cancel()"
                                    href="#" runat="server" ms_positioning="FlowLayout">取消</a>--%>
                                <input type="button" onclick="Cancel();" value=" 取 消 " />
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br>
                            <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label><input id="hdCover"
                                type="hidden" runat="server" name="hdCover"><font face="宋体">&nbsp;</font>
                        </td>
                    </tr>
                </table>
                <br>
                <input id="HdWorkSheetName" type="hidden" name="HdWorkSheetName" runat="server">
                <input id="HdExcelFilePath" type="hidden" name="HdExcelFilePath" runat="server">
                <input id="HdError" type="hidden" name="HdError" runat="server">
                <input id="HdOpt" type="hidden" name="HdOpt" runat="server">
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
