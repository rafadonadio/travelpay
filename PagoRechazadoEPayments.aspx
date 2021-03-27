<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PagoRechazadoEPayments.aspx.cs" Inherits="PagoRechazadoEPayments" Title="Pago Rechazado" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajax:AjaxPanel runat="server" ID="ajxpnl">
	<table class="TablaComun" align=center>
		<tr><td style="font-size: medium; text-align: center;">Se ha rechazado su pago.</td></tr>
		<tr><td><br /></td></tr>
		<tr><td><asp:Label ID="lblData" runat="server"></asp:Label></td></tr>
	</table>
    </ajax:AjaxPanel>
</asp:Content>