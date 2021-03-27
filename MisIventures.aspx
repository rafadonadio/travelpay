<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="MisIventures.aspx.cs" Inherits="MisIventures" Title="Mis Solicitudes" %>

<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<script>
	function CancelarIventure(id){
		if(!confirm("¿Está seguro que quiere cancelar la solicitud '" + id+"'?"))   
				return false;
	}
	function BorrarIventure(id){
		if(!confirm("¿Está seguro que quiere borrar la solicitud '" + id+"'?"))   
				return false;
	}

	function getDataReprocesarNPS(idIventure)
	{
		var url = "GetDataReprocesarNPS.aspx?IdIventure="+idIventure+"&MerchantID="+document.forms["GatewayData"].MerchantID.value;
	    //var handler = new ActiveXObject('Microsoft.XMLHTTP');
		var handler = GetXmlHttpObject();
		handler.open('get', url, false);
		handler.send(null); 
		document.forms["GatewayData"].EncData.value = handler.responseText;
		handler = null;
	}
	function getDataReprocesarEPayments(idIventure)
	{
		var url = "GetDataReprocesarEPayments.ashx?IdIventure="+idIventure+"&shopCode="+document.forms["FormEPay"].shopCode.value;
	    //var handler = new ActiveXObject('Microsoft.XMLHTTP');
		var handler = GetXmlHttpObject();
		handler.open('get', url, false);
		handler.send(null);
		debugger; 
		//document.forms["FormEPay"].EncData.value = handler.responseText;
		debugger;
		handler = null;
	}

	function SubmitReprocesarNPS(idIventure){
		document.forms["GatewayData"].MerchantID.value = document.getElementById('<%= MerchantID_NoPost.ClientID %>').value;	
		getDataReprocesarNPS(idIventure);
		var url = document.getElementById('<%= NPS_GatewayData_Url_NoPost.ClientID %>').value.toString(); 
		document.forms["GatewayData"].action = url;
		document.forms["GatewayData"].submit();
	}
	function SubmitReprocesarEPayments(idIventure){
	debugger;
		document.forms["FormEPay"].shopCode.value = document.getElementById('<%= MerchantID_NoPost.ClientID %>').value;	
		getDataReprocesarEPayments(idIventure);
		//document.forms["FormEPay"].submit();
		alert('ReProcesar por EPayments');
	}
	</script>

	<ajax:AjaxPanel ID="AjaxPanel1" runat="server">
		<asp:Label runat="server" ID="lblAviso" CssClass="aviso"></asp:Label>
		<asp:GridView ID="grilla" runat="server" AutoGenerateColumns="False" CssClass="grillas"
			HeaderStyle-CssClass="grillaEncabezado" RowStyle-CssClass="GrillafilaComun" AlternatingRowStyle-CssClass="GrillafilaAlternativa"
			BorderColor="Black" CellPadding="3" OnRowDataBound="grilla_RowDataBound" AllowPaging="True" AllowSorting=true
			PageSize="10" OnPageIndexChanging="grilla_PageIndexChanging" Font-Size="Small" 
            Width="526px"  OnSorting="grilla_Sorting">
			<RowStyle CssClass="GrillafilaComun" />
			<PagerStyle CssClass="grillaEncabezado" Height="16" />
			<Columns>
				<asp:BoundField DataField="ApellidoNombreCliente" HeaderText="Cliente" SortExpression="Cliente" />
				<asp:BoundField DataField="RazonSocialProveedor" HeaderText="Empresa" />
				<asp:BoundField DataField="Id" HeaderText="Solicitud" SortExpression="Id" />
				<asp:BoundField DataField="ImporteTotal" HeaderText="Total" DataFormatString="$ {0:F2}"
					ItemStyle-HorizontalAlign="Right">
					<ItemStyle HorizontalAlign="Right" />
				</asp:BoundField>
				<asp:BoundField DataField="FechaPagado" HeaderText="F. Pago" DataFormatString="{0:dd/MM/yy}" />
				<asp:BoundField DataField="Estado" HeaderText="Estado" ItemStyle-HorizontalAlign="center"  SortExpression="Estado">
					<ItemStyle HorizontalAlign="Center" />
				</asp:BoundField>
				<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign="left">
					<ItemTemplate>
						<asp:Label ID="lblEspacios" runat="server" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
						<asp:ImageButton ID="imgReprocesar" ImageUrl="~/images/reprocesar.gif" 
							ToolTip="Actualizar estado" runat="server"   />
						<asp:ImageButton ID="imgReenviar" ImageUrl="~/images/reenviar.gif" 
							ToolTip="Reenviar Mail" runat="server" OnClick="BtnReenviar" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
						<asp:ImageButton ID="ImageButton1" ImageUrl="~/images/diskette.gif" Width="19" Height="19"
							ToolTip="Download" runat="server" OnClick="BtnDownload" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
						<asp:ImageButton ID="ImageButton2" ImageUrl="~/images/detalle.gif" Width="19" Height="19"
							ToolTip="Detalles" runat="server" OnClick="BtnVer" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
						<asp:ImageButton ID="imgCancelar" ImageUrl="~/images/Eliminar.gif" Width="19" Height="19"
							ToolTip="Cancelar" runat="server" OnClick="CancelarIventure" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
						<asp:Label ID="lblEspCancelar" Visible=false runat="server" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:Label>
						<asp:ImageButton ID="imgBorrar" Visible=false ImageUrl="~/images/Borrar.gif" Width="19" Height="19"
							ToolTip="Borrar" runat="server" OnClick="BorrarIventure" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
					</ItemTemplate>
				    <ItemStyle HorizontalAlign="Left" />
				</asp:TemplateField>
			</Columns>
			<HeaderStyle CssClass="grillaEncabezado" />
			<AlternatingRowStyle CssClass="GrillafilaAlternativa" />
		</asp:GridView>
		<asp:HiddenField ID="MerchantID_NoPost" runat="server" />
		<asp:HiddenField ID="NPS_GatewayData_Url_NoPost" runat="server" />
	</ajax:AjaxPanel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
	<form id=GatewayData method=post>
	   <input id="EncData" name="EncData" type="hidden" />		
	   <input id="MerchantID" name="MerchantID" type="hidden"  />
	</form>
	<form id=FormEPay action="https://www.epayments.hsbc.com.ar/public/ssl/ExternalDispatcher.asmx" method=post>
	   <input id="XML" name="XML" type="hidden" />		
	   <input id="shopCode" name="shopCode" type="hidden"  />
	</form>

</asp:Content>