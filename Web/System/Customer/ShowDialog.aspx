<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowDialog.aspx.cs" Inherits="PEIS.Web.System.Customer.ShowDialog" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<%=title%>
		</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<iframe frameborder=0 width="100%" height="100%" src='<%=frameSrc%>'></iframe>
		</form>
	</body>
</HTML>
