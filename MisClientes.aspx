<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="MisClientes.aspx.cs" Inherits="MisClientes" Title="Mis Clientes" %>

<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<script>
	function EliminarCliente(cuitDni){
		if(!confirm("¿Está seguro que quiere eliminar el cliente '" + cuitDni+"'?"))   
				return false;
	}
	</script>
		<ajax:AjaxPanel ID="AjaxPanel1" runat="server">
		<asp:Label runat="server" ID="lblAviso" CssClass="aviso"></asp:Label>
		<asp:GridView ID="grilla" runat="server" AutoGenerateColumns="False" CssClass="grillas" 
			HeaderStyle-CssClass="grillaEncabezado" RowStyle-CssClass="GrillafilaComun" AlternatingRowStyle-CssClass="GrillafilaAlternativa"
			BorderColor="Black" CellPadding="3" AllowPaging="True" PageSize="10" 
            OnPageIndexChanging="grilla_PageIndexChanging" Font-Size="Small" Width="550px" OnRowDataBound="grilla_RowDataBound">
			<RowStyle CssClass="GrillafilaComun" />
			<PagerStyle CssClass="grillaEncabezado" Height="16" />
			<Columns>
				<asp:BoundField DataField="Apellido" HeaderText="Apellido" />
				<asp:BoundField DataField="Nombre" HeaderText="Nombre" />
				<asp:BoundField DataField="CuitDni" HeaderText="Cuit / Dni" />
				<asp:BoundField DataField="Telefonos" HeaderText="Telefonos" />
				<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign=Left>
					<ItemTemplate>
						<asp:ImageButton runat="server" ID="imgIventure" ImageUrl="~/images/nuevoIventure.gif"
							Width="19" Height="19" ToolTip='Crear solicitud'
							OnClick="BtnCrearIventure" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
						<asp:ImageButton runat="server" ID="ImageButton2" ImageUrl="~/images/detalle.gif"
							Width="19" Height="19" ToolTip='<%# "Detalles de " +  DataBinder.Eval(Container.DataItem, "Apellido").ToString() + ", " + DataBinder.Eval(Container.DataItem, "Nombre").ToString()  %>'
							OnClick="BtnVer" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
						<asp:ImageButton ID="imgEliminar" ImageUrl="~/images/Eliminar.gif" Width="19" Height="19"
							ToolTip="Eliminar" runat="server" OnClick="EliminarCliente" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />

					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
			<HeaderStyle CssClass="grillaEncabezado" />
			<AlternatingRowStyle CssClass="GrillafilaAlternativa" />
		</asp:GridView>
	</ajax:AjaxPanel>
</asp:Content>
