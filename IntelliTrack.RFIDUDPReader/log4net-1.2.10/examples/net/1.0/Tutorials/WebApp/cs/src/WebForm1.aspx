<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="WebApp.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox id="txtAdd1" style="Z-INDEX: 100; LEFT: 88px; POSITION: absolute; TOP: 80px" runat="server" Width="25px"></asp:TextBox>
			<asp:TextBox id="txtAdd2" style="Z-INDEX: 105; LEFT: 144px; POSITION: absolute; TOP: 80px" runat="server" Width="25px"></asp:TextBox>
			<asp:TextBox id="txtAdd3" style="Z-INDEX: 106; LEFT: 216px; POSITION: absolute; TOP: 80px" runat="server" Width="25px"></asp:TextBox>
			<asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 126px; POSITION: absolute; TOP: 80px" runat="server" Width="18px" Height="2px">+</asp:Label>
			<asp:Label id="Label2" style="Z-INDEX: 102; LEFT: 184px; POSITION: absolute; TOP: 80px" runat="server" Width="8px" Height="20px">=</asp:Label>
			<asp:Button id="btnCalcAdd" style="Z-INDEX: 103; LEFT: 272px; POSITION: absolute; TOP: 80px" runat="server" Width="63px" Height="29px" Text="Calc"></asp:Button>
			<asp:TextBox id="txtSub1" style="Z-INDEX: 100; LEFT: 88px; POSITION: absolute; TOP: 160px" runat="server" Width="25px"></asp:TextBox>
			<asp:TextBox id="txtSub2" style="Z-INDEX: 105; LEFT: 144px; POSITION: absolute; TOP: 160px" runat="server" Width="25px"></asp:TextBox>
			<asp:TextBox id="txtSub3" style="Z-INDEX: 106; LEFT: 216px; POSITION: absolute; TOP: 160px" runat="server" Width="25px"></asp:TextBox>
			<asp:Label id="Label3" style="Z-INDEX: 101; LEFT: 126px; POSITION: absolute; TOP: 160px" runat="server" Width="18px" Height="2px">-</asp:Label>
			<asp:Label id="Label4" style="Z-INDEX: 102; LEFT: 184px; POSITION: absolute; TOP: 160px" runat="server" Width="8px" Height="20px">=</asp:Label>
			<asp:Button id="btnCalcSub" style="Z-INDEX: 103; LEFT: 272px; POSITION: absolute; TOP: 160px" runat="server" Width="63px" Height="29px" Text="Calc"></asp:Button>
		</form>
	</body>
</HTML>
