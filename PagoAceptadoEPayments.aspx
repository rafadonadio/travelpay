<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PagoAceptadoEPayments.aspx.cs" Inherits="PagoAceptadoEPayments" Title="Pago Aceptado" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajax:AjaxPanel runat="server" ID="ajxpnl">
	<table class="TablaComun" align=center>
		<tr><td style="font-size: medium; text-align: center;">Se ha realizado su pago 
            correctamente.</td></tr>
		<tr><td><br /></td></tr>
		<tr><td><asp:Label ID="lblData" runat="server"></asp:Label></td></tr>
	</table>
    </ajax:AjaxPanel>
</asp:Content>

