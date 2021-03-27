<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="AMProveedor.aspx.cs" Inherits="AMProveedor" Title="Alta de Empresas" %>

<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<script>
	function eliminarUsuario(usu, rol){
		var msg = "¿Está seguro que quiere eliminar al usuario '" + usu+"'?";
		if(rol == "administrador"){
			msg += "\nTenga en cuenta que es administrador de la empresa.";
		}
		if(!confirm(msg))   
				return false;
	}
	function OpenImage(){
		image = document.getElementById('<%= txtImagen.ClientID %>').value;
		w=window.open('about:blank','','resizable=1, scrollbars=1, top=400, left=500, width=230, height=230');
		w.document.write("<html><head><title>Prueba Imagen</title></head><body><img src='"+image+"'></body></html>" );
	}
	function EnableCreditsCards(nps, ePayments){
		var valGateway = document.getElementById('<%= cboGateway.ClientID %>').value;
		if(valGateway == nps){
			//NPS
			document.getElementById('<%= chkAmerican.ClientID %>').disabled = false;
			document.getElementById('<%= chkCabal.ClientID %>').disabled = false;
			document.getElementById('<%= chkDiners.ClientID %>').disabled = false;
			document.getElementById('<%= chkMastercard.ClientID %>').disabled = false;
			document.getElementById('<%= chkNaranja.ClientID %>').disabled = false;
			document.getElementById('<%= chkNevada.ClientID %>').disabled = false;
			document.getElementById('<%= chkVisa.ClientID %>').disabled = false;
		}
		else if(valGateway == ePayments){
			//ePayments
			document.getElementById('<%= chkAmerican.ClientID %>').disabled = false;
			document.getElementById('<%= chkCabal.ClientID %>').checked = false;
			document.getElementById('<%= chkCabal.ClientID %>').disabled = true;
			document.getElementById('<%= chkDiners.ClientID %>').checked = false;
			document.getElementById('<%= chkDiners.ClientID %>').disabled = true;
			document.getElementById('<%= chkMastercard.ClientID %>').disabled = false;
			document.getElementById('<%= chkNaranja.ClientID %>').checked = false;
			document.getElementById('<%= chkNaranja.ClientID %>').disabled = true;
			document.getElementById('<%= chkNevada.ClientID %>').checked = false;
			document.getElementById('<%= chkNevada.ClientID %>').disabled = true;
			document.getElementById('<%= chkVisa.ClientID %>').disabled = false;
		
		}
		else{
			//Inhabilito todos
			document.getElementById('<%= chkAmerican.ClientID %>').checked = false;
			document.getElementById('<%= chkAmerican.ClientID %>').disabled = true;
			document.getElementById('<%= chkCabal.ClientID %>').checked = false;
			document.getElementById('<%= chkCabal.ClientID %>').disabled = true;
			document.getElementById('<%= chkDiners.ClientID %>').checked = false;
			document.getElementById('<%= chkDiners.ClientID %>').disabled = true;
			document.getElementById('<%= chkMastercard.ClientID %>').checked = false;
			document.getElementById('<%= chkMastercard.ClientID %>').disabled = true;
			document.getElementById('<%= chkNaranja.ClientID %>').checked = false;
			document.getElementById('<%= chkNaranja.ClientID %>').disabled = true;
			document.getElementById('<%= chkNevada.ClientID %>').checked = false;
			document.getElementById('<%= chkNevada.ClientID %>').disabled = true;
			document.getElementById('<%= chkVisa.ClientID %>').checked = false;
			document.getElementById('<%= chkVisa.ClientID %>').disabled = true;
		}
	}
	function ValidarSeleccionTarjetas(val, args){
		args.IsValid= document.getElementById('<%= chkAmerican.ClientID %>').checked || 
		document.getElementById('<%= chkCabal.ClientID %>').checked || 
		document.getElementById('<%= chkDiners.ClientID %>').checked || 
		document.getElementById('<%= chkMastercard.ClientID %>').checked || 
		document.getElementById('<%= chkNaranja.ClientID %>').checked || 
		document.getElementById('<%= chkNevada.ClientID %>').checked || 
		document.getElementById('<%= chkVisa.ClientID %>').checked ; 
	}
	</script>

	<table class="TablaComun">
		<tr>
			<td style="width: 189px; background-color: white">
				Email</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtEmail" runat="server" Width="236px" MaxLength="100"></asp:TextBox>
				&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="Requerido"
					ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
				&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
					Display="Dynamic" ErrorMessage="Ingrese un mail válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				CUIT</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtCuit" runat="server" MaxLength="11" Width="105px"></asp:TextBox>
				<br />
				(ingrese los 11 caracteres sin guiones)
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Requerido"
					ControlToValidate="txtCuit" Display="Dynamic"></asp:RequiredFieldValidator>
				&nbsp;<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtCuit"
					Display="Dynamic" ErrorMessage="Ingrese un CUIT valido" MaximumValue="2028057496777"
					MinimumValue="100000000" Type="Double"></asp:RangeValidator></td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Ingresos Brutos</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtIIBB" runat="server" MaxLength="15" Height="18px" Width="200px"></asp:TextBox>
				&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Requerido"
					ControlToValidate="txtIIBB" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Razón Social</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtRazonsocial" runat="server" Width="246px" MaxLength="100"></asp:TextBox>
				&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Requerido"
					ControlToValidate="txtRazonsocial" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Fecha de Alta en el sistema</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtFechaAlta" runat="server" Width="125px"></asp:TextBox>
				&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Requerido"
					ControlToValidate="txtFechaAlta" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Operador Gateway</td>
			<td style="width: 649px; background-color: white;">
				<ajax:AjaxPanel runat="server" ID="ajxpnl" Width="156px">
					<asp:DropDownList ID="cboGateway" runat="server" Height="20px" Width="155px">
						<asp:ListItem Value="False">Seleccionar...</asp:ListItem>
						<asp:ListItem>NPS - Sub1</asp:ListItem>
					</asp:DropDownList>
				</ajax:AjaxPanel>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator43" runat="server" ErrorMessage="Requerido"
					ControlToValidate="cboGateway" Display="Dynamic" InitialValue="False"></asp:RequiredFieldValidator>
			</td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Id_Gateway</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtIdGateway" runat="server" MaxLength="15" Height="18px" Width="200px"></asp:TextBox>
				&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtIdGateway"
					ErrorMessage="Requerido"></asp:RequiredFieldValidator></td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Clave Enc. - NPS</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtClaveEncNPS" runat="server" MaxLength="100" Height="18px" Width="242px"></asp:TextBox>
			</td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Amb. Pruebas - NPS</td>
			<td style="width: 649px; background-color: white;">
				<asp:DropDownList ID="cboDevNPS" runat="server">
				<asp:ListItem Selected=True Value="True" Text="Si"></asp:ListItem>
				<asp:ListItem Value="False" Text="No"></asp:ListItem>
				</asp:DropDownList>	</td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Imagen</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtImagen" runat="server" Height="18px" Width="200px"></asp:TextBox>
				<img src="Images/imagen.jpg" onclick="OpenImage()"  alt="Probar Imagen" style="cursor:hand"/>
				&nbsp;&nbsp;</td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Teléfonos</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtTelefonos" runat="server" Width="246px" MaxLength="50"></asp:TextBox></td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Días para vencimiento por defecto</td>
			<td style="width: 649px; background-color: white;">
				<asp:TextBox ID="txtDiasVencimiento" runat="server" Width="246px" MaxLength="3"></asp:TextBox>
				&nbsp;&nbsp;<asp:RequiredFieldValidator ID="rqvDiasVto" runat="server" ControlToValidate="txtDiasVencimiento"
					Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
				<asp:RangeValidator ID="rgvDiasVto" runat="server" ControlToValidate="txtDiasVencimiento"
					Display="Dynamic" ErrorMessage="Cantidad de días inválido" MaximumValue="365"
					MinimumValue="0" Type="Integer"></asp:RangeValidator></td>
		</tr>
		<tr>
			<td style="width: 189px; background-color: white">
				Tarjetas</td>
			<td style="width: 649px; background-color: white;">
				<table width="100%">
					<tr>
						<td width="28%"><input type="checkbox" ID="chkAmerican" runat="server" description="American Express" />American Express</td>
						<td width="24%"><input type="checkbox" ID="chkCabal" runat="server" description="Cabal"/>Cabal</td>
						<td width="24%"><input type="checkbox" ID="chkDiners" runat="server" description="Diners"/>Diners</td>
						<td width="24%"><input type="checkbox" ID="chkMastercard" runat="server" description="Mastercard"/>Mastercard</td>
					</tr>
					<tr>
						<td width="28%"><input type="checkbox" ID="chkNaranja" runat="server" description="Naranja"/>Naranja</td>
						<td width="24%"><input type="checkbox" ID="chkNevada" runat="server" description="Nevada"/>Nevada</td>
						<td width="24%"><input type="checkbox" ID="chkVisa" runat="server" description="Visa"/>Visa</td>
						<td width="24%"></td>
					</tr>
					<tr><td colspan=4><asp:CustomValidator ID="cmvTarjetaValidator" runat="server" ErrorMessage="Debe seleccionar al menos un medio de pago" ClientValidationFunction="ValidarSeleccionTarjetas" ControlToValidate="cboGateway"></asp:CustomValidator></td></tr>
				</table>
			</td>
		</tr>
		
	</table>
	<asp:HiddenField ID="txtIdProveedor" runat="server" />
	<br />
	<font class="Titulos">Responsable</font>
	<table class="TablaComun" width="300">
		<tr>
			<td style="height: 22px; background-color: white;">
				Nombre</td>
			<td style="height: 22px; background-color: white;">
				<asp:TextBox ID="txtNombreResponsable" runat="server" Width="247px"></asp:TextBox>
				&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Requerido"
					ControlToValidate="txtnombreResponsable" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
		<tr>
			<td style="height: 22px; background-color: white">
				Apellido</td>
			<td style="height: 22px; background-color: white">
				<asp:TextBox ID="txtApellidoResponsable" runat="server" Width="244px"></asp:TextBox>
				&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Requerido"
					ControlToValidate="txtApellidoResponsable" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
		<tr>
			<td style="height: 22px; background-color: white">
				DNI</td>
			<td style="height: 22px; background-color: white">
				<asp:TextBox ID="txtDNIResponsable" MaxLength="8" runat="server" Width="124px"></asp:TextBox>
				&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Requerido"
					ControlToValidate="txtdniResponsable" Display="Dynamic"></asp:RequiredFieldValidator>
				&nbsp;<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtdniResponsable"
					Display="Dynamic" ErrorMessage="Ingrese un DNI válido" MaximumValue="2028057496777"
					MinimumValue="1000000" Type="Double"></asp:RangeValidator></td>
		</tr>
		<tr>
			<td style="height: 22px; background-color: white">
				Cargo</td>
			<td style="height: 22px; background-color: white">
				<asp:TextBox ID="txtCargoResponsable" runat="server" Width="166px"></asp:TextBox>
				&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Requerido"
					ControlToValidate="txtcargoResponsable" Display="Dynamic"></asp:RequiredFieldValidator></td>
		</tr>
	</table>

	<script language="JavaScript">			
		var cal1 = new calendar1(document.getElementById("<%= txtFechaAlta.ClientID %>"));
		cal1.year_scroll = true;
		cal1.time_comp = false;
	</script>

	<br />
	<font class="Titulos">CUENTAS DE USUARIO</font>
	<br />
	<asp:GridView ID="grilla" runat="server" AutoGenerateColumns="False" CssClass="grillas"
		HeaderStyle-CssClass="grillaEncabezado" RowStyle-CssClass="GrillafilaComun" AlternatingRowStyle-CssClass="GrillafilaAlternativa"
		BorderColor="Black" CellPadding="3" OnRowDataBound="grilla_RowDataBound" Visible="true"
		AllowPaging="True" PageSize="10" OnPageIndexChanging="grilla_PageIndexChanging">
		<RowStyle CssClass="GrillafilaComun" />
		<PagerStyle CssClass="grillaEncabezado" Height="16" />
		<Columns>
			<asp:BoundField DataField="Nombre" HeaderText="Nombre" />
			<asp:BoundField DataField="Email" HeaderText="Email" />
			<asp:BoundField DataField="RolName" HeaderText="Rol" />
			<asp:TemplateField HeaderText="Acciones">
				<ItemTemplate>
					<asp:ImageButton ID="imgBorrar" ImageUrl="~/images/Eliminar.gif" Width="19" Height="19"
						ToolTip='<%# "Eliminar " +  DataBinder.Eval(Container.DataItem, "Nombre").ToString() +" - "+  DataBinder.Eval(Container.DataItem, "RolName").ToString() %>'
						runat="server" OnClick="BorrarUsuario" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
		<HeaderStyle CssClass="grillaEncabezado" />
		<AlternatingRowStyle CssClass="GrillafilaAlternativa" />
	</asp:GridView>
	<br />
	<asp:Label ID="lblBtnUsuario" runat="server" CssClass="aviso"></asp:Label>
	<asp:Button ID="btnCrearUsuario" runat="server" OnClick="btnCrearUsuario_Click" Text="Crear nuevo usuario" /><br />
	<br />
	<font class="Titulos">Estadísticas</font>
	<table class="TablaComun" style="width: 276px">
		<tr>
			<td >
				Cantidad de Transacciones</td>
			<td >
				<asp:TextBox ID="txtCntTransacciones" runat="server" Width="80px" Enabled=false></asp:TextBox>
				</td>
		</tr>
		<tr>
			<td >
				Monto Transacciones</td>
			<td >
				<asp:TextBox ID="txtMontoTransacciones" runat="server" Width="80px" Enabled="False"></asp:TextBox>
				</td>
		</tr>
	</table>
	<br />

	<ajax:AjaxPanel runat="server" ID="ajxlk">
		<cc1:CaptchaControl ID="CaptchaControl1" runat="server" Width="297px" Font-Size="Smaller"
			Text="Ingrese el código de seguridad."  CaptchaMaxTimeout="3600"/>
		<br />
		<asp:Button ID="btnAgregar" runat="server" Text="Aceptar" OnClick="btnAgregar_Click" />
	</ajax:AjaxPanel>
	<br />
	<span style="font-size: 10pt; color: #ff0000">*Tenga en cuenta que los datos ingresados
		no serán divulgados, por conscuencia tenga bien tener presente
		y registrados los datos ingresados debido a que el sistema no permitirá el ingreso
		mediante usuarios con claves de autenticación que den coincidencia con los datos
		ingresados en la cuenta de email; nombre y apellido; y CUIT/DNI.*</span><br />
</asp:Content>
