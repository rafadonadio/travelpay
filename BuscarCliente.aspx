<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuscarCliente.aspx.cs" Inherits="BuscarCliente" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" >
	<link href="css/default.css" rel="stylesheet" type="text/css">
	<title>Buscar Cliente</title>
	<base target=_self></base>
	<script>
		window.dialogArguments.Id = -1;
		
		function SetCliente(aceptar){
			if(aceptar){
				var idCliente;
				list = document.getElementsByName("searchCliente");
				if(list!=null){
				
					for(var i=0; i<list.length; i++){
						if(list[i].checked){
							window.dialogArguments.Id = list[i].value;;
							window.dialogArguments.Nombre= list[i].nombre;
							window.dialogArguments.Apellido= list[i].apellido;
							window.dialogArguments.CUITDNI= list[i].cuitDni;
							window.dialogArguments.Domicilio= list[i].domicilio;
							window.dialogArguments.Email= list[i].email;
							window.dialogArguments.Telefonos= list[i].telefonos;
							break;
						}
					}
				}
			}
			else{
				window.dialogArguments.Id = -1;
			}
			window.close();	
		}
	</script>

</head>
<body>
	<form id="form1" runat="server">
		<div>
		<br />
			<table class="TablaComun" align=center>
			
				<tr>
					<td><b>Selecciones un Cliente</b></td>
				</tr>
				<tr>
					<td>&nbsp;&nbsp;</td>
				</tr>
				<tr><td>Dato de búsqueda&nbsp;&nbsp;
					<asp:DropDownList ID="cboTipoDato" runat="server">
						<asp:ListItem Value="cuit">CUIT / CUIL / DNI</asp:ListItem>
						<asp:ListItem Value="email">Email</asp:ListItem>
					</asp:DropDownList></td></tr>
				<tr><td>Ingrese el valor a buscar&nbsp;&nbsp;<asp:TextBox ID="txtData" runat="server"></asp:TextBox>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtData"
						ErrorMessage="Requerido"></asp:RequiredFieldValidator></td></tr>
				<tr><td align=center>
					<asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" /></td></tr>
				<tr><td><hr /></td></tr>
				<tr>
					<td>
						<asp:GridView ID="grilla" runat="server" AutoGenerateColumns="False" CssClass="grillas"
							HeaderStyle-CssClass="grillaEncabezado" RowStyle-CssClass="GrillafilaComun" AlternatingRowStyle-CssClass="GrillafilaAlternativa"
							BorderColor="Black" CellPadding="3" AllowPaging="True" PageSize="6" OnPageIndexChanging="grilla_PageIndexChanging"
							OnRowDataBound="grilla_RowDataBound">
							<RowStyle CssClass="GrillafilaComun" />
							<PagerStyle CssClass="grillaEncabezado" Height="16" />
							<Columns>
								<asp:TemplateField HeaderText="Seleccionar">
									<ItemTemplate>
										<asp:Label ID="lblRadio" runat="server" Text="Label"></asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:BoundField DataField="Apellido" HeaderText="Apellido" />
								<asp:BoundField DataField="Nombre" HeaderText="Nombre" />
								<asp:BoundField DataField="CuitDni" HeaderText="Cuit / Dni" />
								<asp:BoundField DataField="Domicilio" HeaderText="Domicilio" />
								<asp:BoundField DataField="Email" HeaderText="Email" />
								<asp:BoundField DataField="Telefonos" HeaderText="Telefonos" />
							</Columns>
							<HeaderStyle CssClass="grillaEncabezado" />
							<AlternatingRowStyle CssClass="GrillafilaAlternativa" />
						</asp:GridView>
				</tr>
				<tr>
					<td>
						<asp:Label runat="server" ID="lblAviso" CssClass="aviso"></asp:Label></td>
				</tr>
				<tr>
					<td align="center">
						<input id="btnAceptar" type="button" value="Aceptar" onclick="SetCliente(true);" />
						<input id="btnCancelar" type="button" value="Cancelar" onclick="SetCliente(false);" />
					</td>
				</tr>
			</table>
		</div>
	</form>
</body>
</html>
