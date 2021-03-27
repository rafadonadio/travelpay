<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="MisProveedores.aspx.cs" Inherits="MisProveedores" Title="Mis Empresas" %>

<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<ajax:AjaxPanel ID="AjaxPanel1" runat="server">
		<asp:Label runat="server" ID="lblAviso" CssClass="aviso"></asp:Label>
		<asp:GridView ID="grilla" runat="server" AutoGenerateColumns="False" CssClass="grillas"
			HeaderStyle-CssClass="grillaEncabezado" RowStyle-CssClass="GrillafilaComun" AlternatingRowStyle-CssClass="GrillafilaAlternativa"
			BorderColor="Black" CellPadding="3" AllowPaging="True" PageSize="10" 
            OnPageIndexChanging="grilla_PageIndexChanging" Font-Size="Small">
			<RowStyle CssClass="GrillafilaComun" />
			<PagerStyle CssClass="grillaEncabezado" Height="16" />
			<Columns>
				<asp:BoundField DataField="RazonSocial" HeaderText="Razon Social" />
				<asp:BoundField DataField="Cuit" HeaderText="Cuit" />
				<asp:BoundField DataField="Responsable" HeaderText="Responsable" />
				<asp:TemplateField HeaderText="Acciones">
					<ItemTemplate>
						<asp:ImageButton runat="server" ID="ImageButton2" ImageUrl="~/images/detalle.gif"
							Width="19" Height="19" ToolTip='<%# "Detalles de &laquo; " +  DataBinder.Eval(Container.DataItem, "RazonSocial").ToString() +" &raquo;" %>'
							OnClick="BtnVer" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
			<HeaderStyle CssClass="grillaEncabezado" />
			<AlternatingRowStyle CssClass="GrillafilaAlternativa" />
		</asp:GridView>
		<br />
		<asp:Button ID="btnProveedor" runat="server" OnClick="btnProveedor_Click" Text="Crear Empresa" />
	</ajax:AjaxPanel>
</asp:Content>
