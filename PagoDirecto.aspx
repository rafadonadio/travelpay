<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="PagoDirecto.aspx.cs" Inherits="PagoDirecto" Title="PagoDirecto" %>

<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

	<script>

function getData(cardId)
{
	
	var url = "GetData.ashx?IdIventure="+document.getElementById('<%= txtIdIventure.ClientID %>').value +"&CardId="+cardId;
	//var handler = new ActiveXObject('Microsoft.XMLHTTP');
    var handler = GetXmlHttpObject();
	handler.open('get', url, false);
	handler.send(null); 
	document.forms["GatewayData"].DatosEnc.value = handler.responseText;
	
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
		document.getElementById('<%= btnDonar.ClientID %>').disabled=true;
		
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
		document.getElementById('<%= btnDonar.ClientID %>').disabled=true;
	}
	
	document.forms["formEPayments"].EPAY_ShopCode.value = document.getElementById('<%= EPAY_ShopCode_NoPost.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_OrderCode.value = document.getElementById('<%= txtIdIventure.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_OrderAmount.value = document.getElementById('<%= txtImporte.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_PlanQuotas.value = 1;	
	document.forms["formEPayments"].EPAY_PlanPaymentType.value = document.getElementById('<%= cboTarjeta.ClientID %>').value;	
	//document.forms["formEPayments"].EPAY_UserDocNumber.value = document.getElementById(' txtCUITDNI.ClientID ').value;	
	document.forms["formEPayments"].EPAY_UserDocNumber.value = ""; //No tenemos este valor en el pago directo :|
	document.forms["formEPayments"].EPAY_UserEmail.value = document.getElementById('<%= txtEmail.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_UserFirstName.value = document.getElementById('<%= txtNombre.ClientID %>').value;	
	document.forms["formEPayments"].EPAY_UserLastName.value = document.getElementById('<%= txtApellido.ClientID %>').value;
	document.forms["formEPayments"].submit();
}
/* End EPayments*/



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
		//var v_domicilio_usr =	document.getElementById(' txtDomicilio.ClientID ').value.toString().substring(0, 100);	//Direccion del usuario.
		var v_domicilio_usr =	"";	//No tenemos este dato en el pagoDirecto //Direccion del usuario.
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
		<ajax:AjaxPanel runat="server" ID="ajxpnl" Height="651px" Width="508px">
			<div id="divIventure">
				<table class="TablaComun" runat="server" id="tblIventure" name="tblIventure">
					<tr>
						<td style="background-color: #C0C0C0; height: 20px; text-align: center; font-size: 20px;
							color: #800000; font-family: 'Berlin Sans FB';" colspan="3">
							Pago Directo</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 203px; font-size: small;">
							Empresa</td>
						<td style="background-color: white; width: 301px; height: 21px;">
							<asp:TextBox ID="txtProveedor" runat="server" Height="20px" MaxLength="100" Width="301px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
							</td>
						<td style="background-color: white; width: 50px; height: 20px;"><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProveedor"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 203px; font-size: small;">
							Apellido</td>
						<td style="background-color: white; width: 301px; height: 21px;">
							
							<asp:TextBox ID="txtApellido" runat="server" Height="20px" MaxLength="100" Width="301px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
						</td>
						<td style="background-color: white; width: 50px; height: 21px;">
							<asp:RequiredFieldValidator ID="rqvApellido" runat="server" ControlToValidate="txtApellido"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 203px; font-size: small;">
							Nombre</td>
						<td style="background-color: white; width: 3801px; height: 21px;">
							<asp:TextBox ID="txtNombre" runat="server" Height="20px" MaxLength="100" Width="301px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
						</td>
						<td style="background-color: white; width: 50px; height: 21px;">
							<asp:RequiredFieldValidator ID="rqvNombre" runat="server" ControlToValidate="txtNombre"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator>
						</td>
					</tr>
					<tr>
						<td style="background-color: white; height: 21px; width: 203px; font-size: small;">
							Email</td>
						<td style="background-color: white; width: 285px; height: 21px;">
							<asp:TextBox ID="txtEmail" runat="server" Height="20px" MaxLength="100" Width="301px"
								Font-Size="Medium" CssClass="lerum1"></asp:TextBox>
							<span style="cursor: hand"></span>
						</td>
						<td style="background-color: white; width: 50px; height: 21px;">
							&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmail"
								Display="Dynamic" ErrorMessage="Requerido"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
									ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
									Display="Dynamic" ErrorMessage="Ingrese un email válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
						</td>
					</tr>
					<tr>
						<td style="background-color: white; width: 203px; font-size: small;">
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
						<td style="background-color: white; height: 21px; width: 203px;">
							<asp:Label ID="lblTarjeta" runat="server" Style="font-size: small">Tarjeta de 
                            crédito</asp:Label></td>
						<td style="background-color: white; width: 301px; height: 21px;" nowrap="nowrap">
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
							<asp:Label ID="lblSeleccioneTarjeta" runat="server" ForeColor="Red" Width="116px"></asp:Label>
							<asp:RequiredFieldValidator ID="rfvTarjeta" runat="server" ControlToValidate="cboTarjeta"
								Display="Dynamic" ErrorMessage="RequiredFieldValidator">Seleccione una tarjeta</asp:RequiredFieldValidator></td>
					</tr>
					<tr>
						<td align="center" colspan="3" style="background-color: white">
							<cc1:CaptchaControl ID="CaptchaControl1" runat="server" Font-Size="12" Text="Ingrese el código de seguridad."
								Width="370px" CaptchaMaxTimeout="3600" CssClass="lerum1" />
							&nbsp;<br />
							
							<asp:Button ID="btnDonar" runat="server" Font-Size="Small" OnClick="btnDonar_Click"
								Text="Donar" Width="95px" Font-Bold="True" />
							<br />
						</td>
					</tr>
					<tr>
						<td align="center" colspan="3" style="background-color: white">
							<asp:Label ID="lblAviso" CssClass="aviso" runat="server"></asp:Label></td>
					</tr>
				</table>
			</div><asp:HiddenField ID="hdnIdCliente" runat="server" /><asp:HiddenField ID="txtIdIventure" runat="server"></asp:HiddenField>
		</ajax:AjaxPanel>
	</div>
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
</asp:Content>
