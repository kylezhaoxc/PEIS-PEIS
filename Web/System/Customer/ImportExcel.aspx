<%@ Page Language="c#" CodeBehind="ImportExcel.aspx.cs" AutoEventWireup="false" Inherits="PEIS.Web.System.Customer.ImportExcel" %>

<html>
<head>
    <title>����ͻ�����</title>
    <style type="text/css">
        TABLE
        {
            font-family: ����;
            font-size: 9pt;
        }
    </style>
    <script language="javascript">
        function Cancel() {
            var data = { ExcelFilePath: "", ReturnValue: "cancel", WorkSheetName: "", FileFormat: "" };
            parent.parent.art.dialog.data("aExcelFileInfo", data);
            parent.parent.art.dialog.get('OperWindowFrame').close();
        }
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
                            �ϴ�
                            <asp:Label ID="lblType" Style="font-size: 14px; font-weight: bold" runat="server"></asp:Label>&nbsp;Excel�ļ�
                            <hr>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <font face="����">
                                <table id="Table3" cellspacing="1" cellpadding="1" border="0">
                                    <tr>
                                        <td>
                                            <input id="upload" style="width: 240px; height: 22px" type="file" runat="server"
                                                name="upload">
                                        </td>
                                    </tr>
                                </table>
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <font face="����">
                                <br>
                                &nbsp;<%-- <a class="MrDocButton" id="btnImportX" style="width: 70px; height: 23px; text-decoration: none"
                                    onclick="javascript:document.getElementById('HdOpt').value='1';Form1.submit();"
                                    href="#" ms_positioning="FlowLayout">�ϴ��ļ�</a>--%>
                                <input type="button" onclick="javascript:document.getElementById('HdOpt').value='1';Form1.submit();"
                                    value=" �� �� " />
                                <asp:LinkButton ID="lbImport" runat="server"></asp:LinkButton>&nbsp;
                                <%--<a class="MrDocButton"
                                    id="btnCancel" style="width: 70px; height: 23px; text-decoration: none" onclick="Cancel()"
                                    href="#" runat="server" ms_positioning="FlowLayout">ȡ��</a>--%>
                                <input type="button" onclick="Cancel()" value=" ȡ �� " />
                            </font>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br>
                            <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label><input id="hdCover"
                                type="hidden" runat="server" name="hdCover"><font face="����">&nbsp;</font>
                        </td>
                    </tr>
                    <tr>
                        <td id="TdTemplate1" style="display: none">
                            <a href="TempFile/Template1.xls" target="_blank">���ؿͻ������ļ�ģ��</a>
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
