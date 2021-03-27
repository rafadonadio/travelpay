<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="ReporteCAE.aspx.cs" Inherits="ReporteCAE" Title="Reporte CAE" %>

<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<ajax:AjaxPanel ID="AjaxPanel1" runat="server">
		<table class="TablaComun" style="height: 48px">
			<tr>
				<td>
					Ambiente</td>
				<td>
					<asp:DropDownList ID="cboAmbiente" runat="server">
						<asp:ListItem Value="0">Desarrollo</asp:ListItem>
						<asp:ListItem Selected="True" Value="1">Producci&#243;n</asp:ListItem>
					</asp:DropDownList></td>
			</tr>
			<tr>
				<td>
					Empresas</td>
				<td>
					<asp:ListBox runat="server" ID="cboEmpresa" SelectionMode="Multiple" 
                        Font-Size="Small" Width="200px"></asp:ListBox></td>
			</tr>
			<tr>
				<td>
					Fecha desde</td>
				<td>
					<asp:TextBox ID="txtFechaDesde" runat="server" Font-Size="Small"></asp:TextBox>&nbsp;<a href="javascript:cal1.popup();"
						runat="server" id="linkCal">
						<img src="images/cal.gif" width="16" height="16" border="0" alt="haga click para seleccionar la fecha"
							id="IMG1" style="font-size: medium"></a> &nbsp;
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtFechaDesde"
						Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
					&nbsp;<asp:RangeValidator ID="rgvFechaDesde" runat="server" ControlToValidate="txtFechaDesde"
						ErrorMessage="Fecha desde inválida" Display="Dynamic" Type="Date"></asp:RangeValidator>
				</td>
			</tr>
			<tr>
				<td>
					Fecha hasta</td>
				<td>
					<asp:TextBox ID="txtFechaHasta" runat="server" Font-Size="Small"></asp:TextBox>&nbsp;<a href="javascript:cal2.popup();" runat="server" id="A1">
						<img src="images/cal.gif" width="16" height="16" border="0" alt="haga click para seleccionar la fecha"
							id="IMG2" style="font-size: medium"></a> &nbsp;
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFechaHasta"
						Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
					&nbsp;<asp:RangeValidator ID="rgvFechaHasta" runat="server" ControlToValidate="txtFechaHasta"
						ErrorMessage="Fecha hasta inválida" Display="Dynamic" Type="Date"></asp:RangeValidator>					
				</td>
			</tr>
			<tr>
				<td colspan="2" align="center">
					<asp:Button ID="btnReporte" runat="server" Text="Reporte Transacciones" OnClick="btnReporte_Click" /></td>
			</tr>
		</table>
		<br />
		<asp:Label runat="server" ID="lblAviso" CssClass="aviso"></asp:Label>
		<asp:GridView ID="grilla" runat="server" AutoGenerateColumns="False" CssClass="grillas"
			HeaderStyle-CssClass="grillaEncabezado" RowStyle-CssClass="GrillafilaComun" AlternatingRowStyle-CssClass="GrillafilaAlternativa"
			BorderColor="Black" CellPadding="3" Font-Size="Small" >
			<RowStyle CssClass="GrillafilaComun" />
			<Columns>
				<asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy}"/>
				<asp:BoundField DataField="CUIT" HeaderText="CUIT" />
				<asp:BoundField DataField="CAE" HeaderText="CAE"/>
				<asp:BoundField DataField="NroComprobante" HeaderText="NroComprobante"/>
				<asp:BoundField DataField="IdCabecera" HeaderText="IdCabecera"/>
			</Columns>
			<HeaderStyle CssClass="grillaEncabezado" />
			<AlternatingRowStyle CssClass="GrillafilaAlternativa" />
		</asp:GridView>
	</ajax:AjaxPanel>
	<script>
	var cal1 = new calendar1(document.getElementById("<%= txtFechaDesde.ClientID %>"));
	cal1.year_scroll = true;
	cal1.time_comp = false;
	var cal2 = new calendar1(document.getElementById("<%= txtFechaHasta.ClientID %>"));
	cal2.year_scroll = true;
	cal2.time_comp = false;
	</script>

</asp:Content>
