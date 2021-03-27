<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="MisUsuarios.aspx.cs" Inherits="MisUsuarios" Title="Mis Usuarios" %>

<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<script>
	function eliminarUsuario(usu){
		if(!confirm("¿Está seguro que quiere eliminar al usuario '" + usu+"'?"))   
				return false;
	}
	</script>

	<ajax:AjaxPanel ID="AjaxPanel1" runat="server">
		<asp:Label runat="server" ID="lblAviso" CssClass="aviso"></asp:Label>
		<asp:GridView ID="grilla" runat="server" AutoGenerateColumns="False" CssClass="grillas"
			HeaderStyle-CssClass="grillaEncabezado" RowStyle-CssClass="GrillafilaComun" AlternatingRowStyle-CssClass="GrillafilaAlternativa"
			BorderColor="Black" CellPadding="3" OnRowDataBound="grilla_RowDataBound" AllowPaging="True"
			PageSize="10" OnPageIndexChanging="grilla_PageIndexChanging" Font-Size="Small">
			<RowStyle CssClass="GrillafilaComun" />
			<PagerStyle CssClass="grillaEncabezado" Height="16" />
			<Columns>
				<asp:BoundField DataField="Nombre" HeaderText="Nombre" />
				<asp:BoundField DataField="Email" HeaderText="Email" />
				<asp:BoundField DataField="RolName" HeaderText="Rol" />
				<asp:TemplateField HeaderText="Acciones" ItemStyle-HorizontalAlign=Left>
					<ItemTemplate>
						<asp:ImageButton runat="server" ID="imgReenviar" ImageUrl="~/images/reenviar.gif" 
						ToolTip="Reenviar Mail" OnClick="BtnReenviar"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Nombre") %>' />
						<asp:ImageButton runat="server" ID="imgBorrar" ImageUrl="~/images/Eliminar.gif" Width="19" Height="19" 
						ToolTip='<%# "Eliminar " +  DataBinder.Eval(Container.DataItem, "Nombre").ToString() +" - "+  DataBinder.Eval(Container.DataItem, "RolName").ToString() %>'
						OnClick="BorrarUsuario" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
					</ItemTemplate>
				    <ItemStyle HorizontalAlign="Left" />
				</asp:TemplateField>
			</Columns>
			<HeaderStyle CssClass="grillaEncabezado" />
			<AlternatingRowStyle CssClass="GrillafilaAlternativa" />
		</asp:GridView>
		<br />
		<asp:Button ID="btnCrearUsuario" runat="server" OnClick="btnCrearUsuario_Click" Text="Crear nuevo usuario" />
	</ajax:AjaxPanel>
</asp:Content>
