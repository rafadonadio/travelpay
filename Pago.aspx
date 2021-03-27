<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="Pago.aspx.cs" Inherits="Pago" Title="Pago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script>
	function Submit(action){
		document.forms["botones"].egp_data.value = document.getElementById('<%= gatewayData.ClientID %>').value;	
		document.forms["botones"].action = action;	
		document.forms["botones"].submit();
	}
	</script>

		Datos llegados por post: <asp:Label ID="lblDatosEnc" runat="server" ></asp:Label><br />
		<input id="btnOK" type="button" value="Ok" onclick="Submit('PagoAceptado.aspx');" />
		<input id="btnError" type="button" value="Error" onclick="Submit('PagoRechazado.aspx');" />
		<input id="btnCancel" type="button" value="Cancel" onclick="Submit('PagoCancelado.aspx');" />
		<asp:HiddenField ID="gatewayData" runat="server" />
	
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
	<form id="botones" method="post" action="PagoAceptado.aspx">
		<input id="egp_data" name="egp_data" type="hidden" />
		</form>
</asp:Content>


