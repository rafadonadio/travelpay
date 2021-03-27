<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Login.ascx.cs" Inherits="Controles_Login" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<style type="text/css">
    .style1
    {
        border-left: thin solid #666666;
        border-right: thin solid #666666;
        border-bottom: thin solid #666666;
        padding-left: 3px;
        padding-right: 3px;
        padding-top: 5px;
    }
</style>
<ajax:AjaxPanel runat="server" ID="ajxpnl" BackColor="White" Width="108px">
	<asp:Panel ID="pnlLogOut" runat="server">
		<table border="0" cellpadding="0" cellspacing="0" bgcolor="#ffff00" style="text-align: center;">
			<tr>
				<td valign="top" style="border: thin solid #666666; background-color: white; text-align: center;
					color: maroon; height: 33px; padding-left: 20px; padding-right: 20px; padding-top: 15px;">
					<strong><span style="font-size: 16pt">
						<img src="Images/logina.JPG" /></span></strong><br />
					<table class="will">
						<tr>
							<td>
								Usuario</td>
						</tr>
						<tr>
							<td>
								<asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td>
								Password</td>
						</tr>
						<tr>
							<td>
								<asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
							</td>
						</tr>
					</table>
					&nbsp; &nbsp;<br />
					<asp:ImageButton ID="btnValidar" runat="server" CausesValidation="false" Height="30px"
						ImageUrl="~/Images/go.jpg" OnClick="btnLogin_Click" Width="96px" /><br />
					<a href="Olvido.aspx"><font size="1" style="font-weight: bolder; font-size: 12px;
						color: blue; font-style: italic">¿olvido su usuario o contraseña?</font></a></td>
			</tr>
		</table>
		<br />
	</asp:Panel>
	<asp:Panel ID="pnlLogIn" runat="server">
		<table border="0" cellpadding="0" cellspacing="0" bgcolor="White" style="text-align: center;"
			align="center">
			<tr>
				<td height="40" colspan="2" valign="middle" align="center" style="background-color: white;"
					class="style1">
					<font size="2"><b style="text-align: center">Bienvenido<br/>
						Usuario:
						<asp:Label runat="server" ID="lblUsuarioLogueado" ForeColor="red"></asp:Label>
						<br />
						<b>
							<asp:Label ID="lblTipoUsu" runat="server"></asp:Label>
						</b>
						<asp:Label ID="lblNombre" runat="server" ForeColor="Red"></asp:Label></b></font></td>
			</tr>
			<tr align="center">
				<td valign="middle" align="center" style="background-color: white;" class="style1">
					<br />
					&nbsp;<asp:Label ID="lblMenu" runat="server" Style="cursor: hand;" CssClass="boton"
						BackColor="Transparent" BorderStyle="None" Font-Bold="True" Font-Size="8" ForeColor="Black"></asp:Label>
					<br /><div align=center>
					<asp:Button  aID="Button2" Style="cursor: hand;" CssClass="boton" CausesValidation="false"
						runat="server" Text="Cambiar Contraseña" OnClientClick="window.location.href='cambioPass.aspx';"
						BackColor="Transparent" BorderStyle="None" Font-Bold="True" Font-Size="8" ForeColor="Black" /></div>
					<br />
					&nbsp;<br/>
					<div align=center><asp:Button ID="btnLogOut" CssClass="boton" OnClientClick="return window.confirm('Está seguro de salir del sistema?');"
						CausesValidation="false" runat="server" Text="Cerrar Sesión" OnClick="btnLogOff_Click" /></div>
				</td>
			</tr>
		</table>
		<br />
	</asp:Panel>
</ajax:AjaxPanel>
