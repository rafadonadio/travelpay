<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="IventureTest.aspx.cs" Inherits="IventureTest" Title="Solicitud" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<script>

function Imprimir(){
	
	var idIventure = document.getElementById('<%= txtIdIventure.ClientID %>').value;
	var w= window.open("BajarIventure.aspx?IdIventure=" + idIventure  ,'BajarSolicitud', 'resizable=1, scrollbars=1, top=30, left=30, width=535'); 
	w.focus(); 
}
function getData(cardId)
{
	
	var url = "GetDataTest.aspx?IdIventure="+document.getElementById('<%= txtIdIventure.ClientID %>').value +"&CardId="+cardId;
	//var handler = new ActiveXObject('Microsoft.XMLHTTP');
    var handler = GetXmlHttpObject();
	handler.open('get', url, false);
	handler.send(null); 
	//document.forms["GatewayData"].DatosEnc.value = handler.responseText;
		alert("response: "+handler.responseText); 
	alert("Datos Enc -1: " + document.forms["GatewayData"].DatosEnc.value);
	document.forms["GatewayData"].DatosEnc.value = handler.responseText;
	alert("Datos Enc -2: " + document.forms["GatewayData"].DatosEnc.value);

	
	handler = null;
}


function SubmitNPS(){
	
	document.forms["GatewayData"].egp_MerchantID.value = document.getElementById('<%= egp_MerchantID_NoPost.ClientID %>').value;	
	document.forms["GatewayData"].dev.value = document.getElementById('<%= dev_NoPost.ClientID %>').value;	
	var cardId =  document.getElementById('<%= cboTarjeta.ClientID %>').value;
	if(cardId == ""){
		document.getElementById('<%= lblSeleccioneTarjeta.ClientID %>').innerHTML = "Seleccione una tarjeta";
	}
	else{
		document.getElementById('<%= btnPagar.ClientID %>').disabled=true;
		
		getData(cardId);
		document.forms["GatewayData"].submit();
	}
}


function clienteDialog() {
	var	Id;
	var Nombre;
	var Apellido;
	var CUITDNI;
	var Domicilio;
	var Email;
	var Telefonos;
		
}

/* Begin EPayments*/
function SubmitEPayments(){
	var cardId =  document.getElementById('<%= cboTarjeta.ClientID %>').value;
	if(cardId == ""){
		document.getElementById('<%= lblSeleccioneTarjeta.ClientID %>').innerHTML = "Seleccione una tarjeta";
		return;
	}
	else{
		document.getElementById('<%= btnPagar.ClientID %>').disabled=true;
	}
	
	document.forms["formEPayments"].EPAY_ShopCode.value = document.getElementById('<%= EPAY_ShopCode_NoPost.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_OrderCode.value = document.getElementById('<%= txtIdIventure.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_OrderAmount.value = document.getElementById('<%= txtImporte.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_PlanQuotas.value = document.getElementById('<%= cboCntCuotas.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_PlanPaymentType.value = document.getElementById('<%= cboTarjeta.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_UserDocNumber.value = document.getElementById('<%= txtCUITDNI.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_UserEmail.value = document.getElementById('<%= txtEmail.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_UserFirstName.value = document.getElementById('<%= txtNombre.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_UserLastName.value = document.getElementById('<%= txtApellido.ClientID %>').value;
	document.forms["formEPayments"].submit();
}
/* End EPayments*/

function modalWin(tipo) {

	if(navigator.appName == "Microsoft Internet Explorer") {
		return window.showModalDialog("BuscarCliente.aspx?tipo="+tipo+"&cuit=" + document.getElementById('<%= txtCUITDNI.ClientID %>').value + "&email=" + document.getElementById('<%= txtEmail.ClientID %>').value, clienteDialog,
		"dialogWidth:35em;dialogHeight:26em;center:1");
	} else {
		//return window.open("BuscarCliente.aspx?tipo="+tipo+"&cuit=" + document.getElementById('<%= txtCUITDNI.ClientID %>').value + "&email=" + document.getElementById('<%= txtEmail.ClientID %>').value, clienteDialog, 'height=405,width=550,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes,center:1');
		return window.open("BuscarCliente.aspx?tipo="+tipo+"&cuit=" + document.getElementById('<%= txtCUITDNI.ClientID %>').value + "&email=" + document.getElementById('<%= txtEmail.ClientID %>').value, clienteDialog, 'height=405,width=550,toolbar=no,directories=no,status=no,menubar=no,scrollbars=no,resizable=no,modal=yes,center:1');
	}
}


function Buscar(tipo){
	
	
	/*if (showModalDialog( "BuscarCliente.aspx?tipo="+tipo+"&cuit=" + document.getElementById('<%= txtCUITDNI.ClientID %>').value + "&email=" + document.getElementById('<%= txtEmail.ClientID %>').value , clienteDialog, "dialogWidth:35em;dialogHeight:26em;center:1")==false)
		event.returnValue = false;
	*/
	var resModal =  modalWin(tipo);
	//showModalDialog( "BuscarCliente.aspx?tipo="+tipo+"&cuit=" + document.getElementById('<%= txtCUITDNI.ClientID %>').value + "&email=" + document.getElementById('<%= txtEmail.ClientID %>').value , clienteDialog, "dialogWidth:35em;dialogHeight:26em;center:1")
	if (resModal==false)
		event.returnValue = false;
	else
	{
		if(clienteDialog.Id != -1 )
		{
			if(clienteDialog.Id!= null )
			{
				document.getElementById('<%= hdnIdCliente.ClientID %>').value = clienteDialog.Id;
				document.getElementById('<%= txtNombre.ClientID %>').value = clienteDialog.Nombre;
				document.getElementById('<%= txtApellido.ClientID %>').value = clienteDialog.Apellido;
				document.getElementById('<%= txtCUITDNI.ClientID %>').value = clienteDialog.CUITDNI;
				document.getElementById('<%= txtDomicilio.ClientID %>').value = clienteDialog.Domicilio;
				document.getElementById('<%= txtEmail.ClientID %>').value = clienteDialog.Email;
				document.getElementById('<%= txtTelefonos.ClientID %>').value = clienteDialog.Telefonos;
				document.getElementById('<%= txtNombre.ClientID %>').disabled=true;
				document.getElementById('<%= txtApellido.ClientID %>').disabled=true;
				document.getElementById('<%= txtCUITDNI.ClientID %>').disabled=true;
				document.getElementById('<%= txtDomicilio.ClientID %>').disabled=true;
				document.getElementById('<%= txtEmail.ClientID %>').disabled=true;
				document.getElementById('<%= txtTelefonos.ClientID %>').disabled=true;
			}
			else
			{
				document.getElementById('<%= hdnIdCliente.ClientID %>').value = "";
				document.getElementById('<%= txtNombre.ClientID %>').value = "";
				document.getElementById('<%= txtApellido.ClientID %>').value = "";
				document.getElementById('<%= txtCUITDNI.ClientID %>').value = "";
				document.getElementById('<%= txtDomicilio.ClientID %>').value = "";
				document.getElementById('<%= txtEmail.ClientID %>').value = "";
				document.getElementById('<%= txtTelefonos.ClientID %>').value = "";
				document.getElementById('<%= txtNombre.ClientID %>').disabled=false;
				document.getElementById('<%= txtApellido.ClientID %>').disabled=false;
				document.getElementById('<%= txtCUITDNI.ClientID %>').disabled=false;
				document.getElementById('<%= txtDomicilio.ClientID %>').disabled=false;
				document.getElementById('<%= txtEmail.ClientID %>').disabled=false;
				document.getElementById('<%= txtTelefonos.ClientID %>').disabled=false;			}
				event.returnValue = false;
			}
		}
	}


	function SubmitPagoFacil(){
		//Variables a completar con los datos del sitio, de la compra y del usuario.
		var v_id_emec = document.getElementById('<%= PagoFacil_Id_NoPost.ClientID %>').value;                      //Nmero de identificacin del Sitio de Compra. (valor asignado por Pago Fcil).
		var v_id_operacion = document.getElementById('<%= txtIdIventure.ClientID %>').value;                   //Identificacin de la Compra/Cliente.
		var v_id_moneda = "01";                          //Identificacin de la moneda: 01 pesos.
		//TODO VER: Necesitamos el Id del Sitio de Compra
		//TODO: VER: Que hacemos en los casos en los que hay mas de 6 enteros y mas de 3 decimales?
		var monto = document.getElementById('<%= txtImporte.ClientID %>').value; //Monto de la compra. Formato 6e 2d. Ej. 4500= $45.-
		arrMonto = monto.split(",") 
		if(arrMonto.length == 2){
			arrMonto[0] = arrMonto[0].toString();
			arrMonto[1] = arrMonto[1].toString();
			if(arrMonto[1].length == 0){
				arrMonto[1] = "00";
			}
			if(arrMonto[1].length == 1){
				arrMonto[1] = arrMonto[1] + "0";
			}
			if(arrMonto[1].length > 2){
				arrMonto[1] = arrMonto[1].substring(0, 2);
			}
			monto = arrMonto[0] + arrMonto[1];
		}
		else if (arrMonto.length == 1){
			monto = arrMonto[0].toString() + "00";
		}
		
		var v_va_monto = monto;
		
		var myDate = new Date();
		var theYear = myDate.getFullYear().toString();
		var theMonth = (myDate.getMonth()+1).toString();
		var theToday = myDate.getDate().toString();

		if(theMonth.length < 2){
			theMonth = "0" + theMonth;
		}
		if(theToday.length < 2){
			theToday = "0" + theToday;
		}
		var theHours = myDate.getHours().toString();
		var theMinutes = myDate.getMinutes().toString();
		if(theHours.length < 2){
			theHours = "0" + theHours;
		}
		if(theMinutes.length < 2){
			theMinutes = "0" + theMinutes;
		}
		var v_fecha_transaccion = theToday + theMonth + theYear;	//Fecha de la compra. Formato DDMMAAAA.
		var v_hora_transaccion = theHours + theMinutes;				//Hora de la compra. Formato HHMM.
		var v_dias_vigencia = document.getElementById('<%= PagoFacil_Vigencia_NoPost.ClientID %>').value;                       //Das de vigencia de la compra.
		var v_email_usr =		document.getElementById('<%= txtEmail.ClientID %>').value.toString().substring(0, 50);		//email del usuario (email al que se le enviar el comprobante).
		var v_nombre_usr =		document.getElementById('<%= txtNombre.ClientID %>').value.toString().substring(0, 80);		//Nombre del usuario.
		var v_apellido_usr =	document.getElementById('<%= txtApellido.ClientID %>').value.toString().substring(0, 50);	//Apellido del usuario.
		var v_domicilio_usr =	document.getElementById('<%= txtDomicilio.ClientID %>').value.toString().substring(0, 100);	//Direccion del usuario.
		var v_localidad_usr = "";			//Localidad del usuario.
		var v_provincia_usr = "";			//Provincia del usuario.
		var v_pais_usr = "Argentina";		//Pais del usuario.
		var v_email_alter = "";				//email alternativo de envio.
		var urlGet = document.getElementById('<%= PagoFacil_Url_NoPost.ClientID %>').value.toString() + "?p_id_emec=" + v_id_emec + "&p_id_operacion=" + v_id_operacion + "&p_id_mone_ecom=" + v_id_moneda + "&p_va_monto=" + v_va_monto + "&p_fe_transaccion=" + v_fecha_transaccion + "&p_fe_hora=" + v_hora_transaccion + "&p_dias_vigencia=" + v_dias_vigencia + "&p_direccion_email_usua=" + v_email_usr + "&p_nombre_usuario=" + v_nombre_usr + "&p_apellido_usuario=" + v_apellido_usr + "&p_domicilio_usuario=" + v_domicilio_usr + "&p_localidad=" + v_localidad_usr + "&p_provincia_usuario=" + v_provincia_usr + "&p_pais_usuario=" + v_pais_usr + "&p_direccion_email_alter=" + v_email_alter; 
		document.forms["formPagoFacil"].action = urlGet;
		document.forms["formPagoFacil"].submit();
	}

	</script>

	<div style="overflow: auto;" align=center>
		<ajax:AjaxPanel runat="server" ID="ajxpnl" Height="670px">
			<div id="divIventure" name="divIventure">
				<table class="TablaComun" runat="server" id="tblIventure" >
					<tr>
						<td style="background-color: #C0C0C0; height: 20px; text-align: center; font-size: 20px;
							color: #800000; font-family: 'Berlin Sans FB';" colspan="3">
							Detalle de su solicitud</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 20px; width: 105px; font-size: small;">
							Nro. Solicitud</td>
						<td style="background-color: white; width: 285px; height: 20px;">
							<asp:TextBox ID="txtIdIventure" runat="server" Height="20px" Width="113px" CssClass="lerum1"></asp:TextBox>
						</td>
						<td style="background-color: white; width: 50px; height: 20px;">
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 20px; width: 105px; font-size: small;">
							Empresa</td>
						<td style="background-color: white; width: 285px; height: 20px;">
							<asp:TextBox ID="txtProveedor" runat="server" Height="20px" MaxLength="100" Width="113px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
							&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProveedor"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator></td>
						<td style="background-color: white; width: 50px; height: 20px;">
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 20px; width: 105px; font-size: small;">
							Vendedor</td>
						<td style="background-color: white; width: 285px; height: 20px;">
							<asp:DropDownList ID="cboVendedores" runat="server" Font-Size="Medium" CssClass="lerum1">
							</asp:DropDownList></td>
						<td style="background-color: white; width: 50px; height: 20px;">
						</td>
					</tr>
					<tr>
						<td style="background-color: white; width: 105px; font-size: small;">
							Detalle</td>
						<td style="background-color: white; width: 285px;">
							<asp:TextBox Width="300px" ID="txtDetalle" runat="server" MaxLength="100" Height="88px"
								TextMode="MultiLine" Font-Size="Small" CssClass="lerum1"></asp:TextBox>
						</td>
						<td style="background-color: white; width: 50px;">
							<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDetalle"
								Display="Dynamic" ErrorMessage="Requerido" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
					</tr>
					<tr>
						<td style="background-color: white; width: 105px; font-size: small;">
							Importe</td>
						<td style="background-color: white; width: 285px;">
							<asp:TextBox Width="113px" ID="txtImporte" runat="server" MaxLength="21" Height="20px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
							&nbsp;<asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtImporte"
								ErrorMessage="Importe inválido" MaximumValue="500000000" MinimumValue="0" Type="Double"
								Display="Dynamic" SetFocusOnError="True"></asp:RangeValidator>
							<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtImporte"
								Display="Dynamic" ErrorMessage="Requerido" SetFocusOnError="True"></asp:RequiredFieldValidator></td>
						<td style="background-color: white; width: 50px;">
							&nbsp;</td>
					</tr>
					<tr>
						<td style="background-color: white; width: 105px; font-size: small;">
							Cuotas</td>
						<td style="background-color: white; width: 285px;">
							<asp:DropDownList ID="cboCntCuotas" runat="server" Font-Size="Medium" CssClass="lerum1">
								<asp:ListItem Selected="True" Value="1">01</asp:ListItem>
								<asp:ListItem Value="2">02</asp:ListItem>
								<asp:ListItem Value="3">03</asp:ListItem>
								<asp:ListItem Value="4">04</asp:ListItem>
								<asp:ListItem Value="5">05</asp:ListItem>
								<asp:ListItem Value="6">06</asp:ListItem>
								<asp:ListItem Value="7">07</asp:ListItem>
								<asp:ListItem Value="8">08</asp:ListItem>
								<asp:ListItem Value="9">09</asp:ListItem>
								<asp:ListItem Value="10">10</asp:ListItem>
								<asp:ListItem Value="11">11</asp:ListItem>
								<asp:ListItem Value="12">12</asp:ListItem>
								<asp:ListItem Value="18">18</asp:ListItem>
								<asp:ListItem Value="24">24</asp:ListItem>
								<asp:ListItem Value="36">36</asp:ListItem>
							</asp:DropDownList>
						</td>
						<td style="background-color: white; width: 50px;">
							&nbsp;</td>
					</tr>
					<tr>
						<td style="background-color: white; width: 105px; font-size: small;">
							Fecha hasta</td>
						<td style="background-color: white; width: 285px;">
							<asp:TextBox ID="txtFechaHasta" runat="server" Font-Size="Small" CssClass="lerum1"></asp:TextBox>&nbsp;<a
								href="javascript:cal2.popup();" runat="server" id="A1">
								<img src="images/cal.gif" width="16" height="16" border="0" alt="haga click para seleccionar la fecha"
									id="imgCalendar" runat="server" style="font-size: medium"></a> &nbsp;</td>
						<td style="background-color: white; width: 50px;">
							<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFechaHasta"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
							&nbsp;<asp:RangeValidator ID="rgvFechaHasta" runat="server" ControlToValidate="txtFechaHasta"
								ErrorMessage="Fecha hasta inválida" Display="Dynamic" Type="Date"></asp:RangeValidator>
						</td>
					</tr>
					<tr>
						<td colspan="3" style="background-color: #669999; height: 21px; font-size: medium;
							color: white;">
							<b>Información del cliente</b></td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 105px; font-size: small;">
							Apellido</td>
						<td style="background-color: white; width: 285px; height: 21px;">
							<asp:HiddenField ID="hdnIdCliente" runat="server" />
							<asp:TextBox ID="txtApellido" runat="server" Height="20px" MaxLength="100" Width="301px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
						</td>
						<td style="background-color: white; width: 50px; height: 21px;">
							<asp:RequiredFieldValidator ID="rqvApellido" runat="server" ControlToValidate="txtApellido"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 105px; font-size: small;">
							Nombre</td>
						<td style="background-color: white; width: 285px; height: 21px;">
							<asp:TextBox ID="txtNombre" runat="server" Height="20px" MaxLength="100" Width="301px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
						</td>
						<td style="background-color: white; width: 50px; height: 21px;">
							<asp:RequiredFieldValidator ID="rqvNombre" runat="server" ControlToValidate="txtNombre"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 105px; font-size: small;">
							CUIT / CUIL / DNI</td>
						<td style="background-color: white; width: 285px; height: 21px;">
							<asp:TextBox ID="txtCUITDNI" runat="server" Height="20px" MaxLength="15" Width="301px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox></td>
						<td style="background-color: white; width: 50px; height: 21px;">
							<span style="cursor: hand">
								<asp:Image ID="imgBuscarCuit" runat="server" ImageUrl="~/Images/Buscar.gif" /></span>
							<asp:RequiredFieldValidator ID="rqvCUITDNI" runat="server" ControlToValidate="txtCUITDNI"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 105px; font-size: small;">
							Domicilio</td>
						<td style="background-color: white; width: 285px; height: 21px;">
							<asp:TextBox ID="txtDomicilio" runat="server" Height="20px" MaxLength="100" Width="301px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
						</td>
						<td style="background-color: white; width: 50px; height: 21px;">
							<asp:RequiredFieldValidator ID="rqvDomicilio" runat="server" ControlToValidate="txtDomicilio"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 105px; font-size: small;">
							Teléfonos</td>
						<td style="background-color: white; width: 285px; height: 21px;">
							<asp:TextBox ID="txtTelefonos" runat="server" Height="20px" MaxLength="50" Width="301px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
						</td>
						<td style="background-color: white; width: 50px; height: 21px;">
							&nbsp;</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 105px; font-size: small;">
							Email</td>
						<td style="background-color: white; width: 285px; height: 21px;">
							<asp:TextBox ID="txtEmail" runat="server" Height="20px" MaxLength="100" Width="235px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
							<span style="cursor: hand">
								<asp:Image ID="imgBuscarEmail" runat="server" ImageUrl="~/Images/Buscar.gif" /></span></td>
						<td style="background-color: white; width: 50px; height: 21px;">
							&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
									ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
									Display="Dynamic" ErrorMessage="Ingrese un email válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 105px;">
							<asp:Label ID="lblTarjeta" runat="server" Style="font-size: small">Tarjeta de 
                            crédito</asp:Label></td>
						<td style="background-color: white; width: 285px; height: 21px;" nowrap="nowrap">
							<asp:DropDownList ID="cboTarjeta" runat="server" Font-Size="Small" CssClass="lerum1">
								<asp:ListItem>[Seleccione una tarjeta de cr&#233;dito...]</asp:ListItem>
								<asp:ListItem Value="1">American Express</asp:ListItem>
								<asp:ListItem Value="8">Cabal</asp:ListItem>
								<asp:ListItem Value="2">Diners</asp:ListItem>
								<asp:ListItem Value="5">Mastercard</asp:ListItem>
								<asp:ListItem Value="9">Naranja</asp:ListItem>
								<asp:ListItem Value="21">Nevada</asp:ListItem>
								<asp:ListItem Value="14">Visa</asp:ListItem>
							</asp:DropDownList>
						</td>
						<td style="background-color: white; width: 50px; height: 21px;">
							<asp:Label ID="lblSeleccioneTarjeta" runat="server" ForeColor="Red" Width="116px"></asp:Label></td>
					</tr>
					<tr>
						<td align="center" colspan="3" style="background-color: white">
							<cc1:CaptchaControl ID="CaptchaControl1" runat="server" Font-Size="12" Text="Ingrese el código de seguridad."
								Width="370px" CaptchaMaxTimeout="3600" CssClass="lerum1" />
							<br />
							<asp:Button ID="btnMail" runat="server" Font-Size="Small" Text="Reenviar mail" Width="95px"
								OnClick="btnMail_Click" />
							<asp:Button ID="btnDarDeAlta" runat="server" Font-Size="Small" OnClick="btnDarDeAlta_Click"
								Text="Grabar" Width="95px" UseSubmitBehavior="False" />
							<asp:Button ID="btnPagar" runat="server" Font-Size="Small" Text="Pagar" Width="95px"
								CausesValidation="False" Font-Bold="True" />
							&nbsp;<asp:Button ID="btnImprimir" runat="server" Font-Size="Small" Text="Imprimir"
								Width="95px" />
							&nbsp;<asp:Button ID="btnActualizar" runat="server" Font-Size="Small" OnClick="btnGrabar_Click"
								Text="Grabar" Width="95px" />
							<br />
						</td>
					</tr>
					<tr>
						<td align="center" colspan="3" style="background-color: white">
							<asp:Label ID="lblAviso" CssClass="aviso" runat="server" ></asp:Label></td>
					</tr>
				</table>
			</div>
		</ajax:AjaxPanel>
	</div>
	<br />
	<span style="font-size: 10pt; color: #ff0000" runat="server" id="lblTenga">* EL PRESENTE DOCUMENTO
		NO ES VÁLIDO COMO COMPROBANTE FISCAL<br>
		Tenga en cuenta que los datos ingresados en la Plataforma Travelpay no serán divulgados,
		por conscuencia tenga presente resguardar los datos ingresados. *<br />
	</span>
	<asp:HiddenField ID="gatewayData" runat="server" />
	<input id="egp_MerchantID_NoPost" name="egp_MerchantID_NoPost" type="hidden" runat="server" />
	<input id="dev_NoPost" name="dev_NoPost" type="hidden" runat="server" />
	<input id="EPAY_ShopCode_NoPost" name="EPAY_ShopCode_NoPost" type="hidden" runat="server" />
	<input id="PagoFacil_Id_NoPost" name="PagoFacil_Id_NoPost" type="hidden" runat="server" />
	<input id="PagoFacil_Vigencia_NoPost" name="PagoFacil_Vigencia_NoPost" type="hidden"
		runat="server" />
	<input id="PagoFacil_Url_NoPost" name="PagoFacil_Url_NoPost" type="hidden" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
	<!--PruebaPagoFacil.aspx-->
	<form id="GatewayData" action="https://services.nps.com.ar/index.php" method="post">
		<input id="DatosEnc" name="DatosEnc" type="hidden" />
		<input id="egp_MerchantID" name="egp_MerchantID" type="hidden" />
		<input id="dev" name="dev" type="hidden" />
	</form>
	<form id="formEPayments" action="https://www.epayments.hsbc.com.ar/Public/Ssl/Routing.aspx"
		method="post">
		<input type="hidden" id="EPAY_ShopCode" name="ShopCode">
		<input type="hidden" id="EPAY_OrderCode" name="OrderCode">
		<input type="hidden" id="EPAY_OrderAmount" name="OrderAmount">
		<input type="hidden" id="EPAY_Currency" name="Currency" value="ARS">
		<input type="hidden" id="EPAY_AutomaticCapture" name="AutomaticCapture" value="1">
		<input type="hidden" id="EPAY_ExpirationHours" name="ExpirationHours" value="1">
		<input type="hidden" id="EPAY_PlanQuotas" name="PlanQuotas">
		<input type="hidden" id="EPAY_PlanPaymentType" name="PlanPaymentType">
		<input type="hidden" id="EPAY_UserDocId" name="UserDocId" value="DNI">
		<input type="hidden" id="EPAY_UserDocNumber" name="UserDocNumber">
		<input type="hidden" id="EPAY_UserEmail" name="UserEmail">
		<input type="hidden" id="EPAY_UserFirstName" name="UserFirstName">
		<input type="hidden" id="EPAY_UserLastName" name="UserLastName">
	</form>
	<form id="formPagoFacil" method="GET">
	</form>

	<script>
	var cal2 = new calendar1(document.getElementById("<%= txtFechaHasta.ClientID %>"));
	cal2.year_scroll = true;
	cal2.time_comp = false;
	</script>

</asp:Content>
