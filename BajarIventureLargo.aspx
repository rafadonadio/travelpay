<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BajarIventureLargo.aspx.cs" Inherits="BajarIventureLargo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Solicitud</title>
	<link href="css/default.css" rel="stylesheet" type="text/css" />

	<script>
	function Imprimir(button){
		button.style.visibility = "hidden";
		document.getElementById("btnGuardar").style.visibility = "hidden";
		window.print();
		button.style.visibility = "visible";
		document.getElementById("btnGuardar").style.visibility = "visible";
	}	
	function Guardar(){
		document.getElementById("btnImprimir").style.visibility = "hidden";
		document.getElementById("btnGuardar").style.visibility = "hidden";
		var idIventure = document.getElementById('<%= txtIdIventure.ClientID %>').value
		window.document.execCommand('saveas', null, "Solicitud_" + idIventure + ".html");
		document.getElementById("btnImprimir").style.visibility = "visible";
		document.getElementById("btnGuardar").style.visibility = "visible";
	}
	
	function Cerrar(){
		window.close();
	}
	
	</script>

    <style type="text/css">
        .style1
        {
            background-color: #C0C0C0;
        }
        .style2
        {
            font-family: "Berlin Sans FB Demi";
            background-color: #C0C0C0;
        }
        .style3
        {
            height: 20px;
            width: 85px;
        }
        .style4
        {
            width: 85px;
        }
        .style5
        {
            height: 22px;
            width: 85px;
        }
        .style6
        {
            height: 21px;
            width: 85px;
        }
    </style>

</head>
<body>
	<form id="form1" runat="server">
		<table class="TablaComun" runat="server" id="tblIventure2" border=2>
			<tr runat="server">
				<td style="background-color: #C0C0C0; height: 20px; text-align: center; font-size: 20px;
					color: #800000; font-family: 'Berlin Sans FB';" colspan="3" id="TD1" runat="server">
					BAJAR - Solicitud</td>
			</tr>
			<tr runat="server">
				<td style="background-color: white; " id="TD4" runat="server" class="style3">
					Id_Solicitud</td>
				<td style="background-color: white; width: 306px; height: 20px;" id="TD2" runat="server">
					<asp:TextBox ID="txtIdIventure" runat="server" Height="20px" Width="113px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 20px;" id="TD3" runat="server">
				</td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style3">
					Proveedor</td>
				<td style="background-color: white; width: 306px; height: 20px;">
					<asp:TextBox ID="txtProveedor" runat="server" Height="20px" MaxLength="100" Width="113px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 20px;">
				</td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style3">
					Vencimiento</td>
				<td style="background-color: white; width: 306px; height: 20px;">
					<asp:TextBox ID="txtVencimiento" runat="server" Height="20px" MaxLength="10" Width="113px"></asp:TextBox>&nbsp;
				</td>
				<td style="background-color: white; width: 106px; height: 20px;">
				</td>
			</tr>
			<tr>
				<td colspan="3" 
                    style="height: 20px; font-size: medium; color: #000080; background-color: #C0C0C0;">
					<span style="font-family: 'Berlin Sans FB Demi'; color: #000080;" 
                        class="style1">Paquete</span></td>
			</tr>
			<tr>
				<td style="background-color: white" class="style4">
					Tipo</td>
				<td style="background-color: white; width: 306px;">
					<asp:DropDownList ID="cboAereo" Font-Size="Small" runat="server">
						<asp:ListItem Value="Ida y vuelta">Ida y vuelta</asp:ListItem>
						<asp:ListItem Value="S&oacute;lo ida">S&oacute;lo ida</asp:ListItem>
						<asp:ListItem Value="M&uacute;ltiples destinos">M&uacute;ltiples destinos</asp:ListItem>
						<asp:ListItem Value="Sin a&eacute;reo">Sin a&eacute;reo</asp:ListItem>
					</asp:DropDownList>
				</td>
				<td style="background-color: white; width: 106px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white" class="style4">
					Detalle</td>
				<td style="background-color: white; width: 306px;">
					<asp:TextBox Width="300px" ID="txtDetalle" runat="server" MaxLength="100" Height="50px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; font-size: small;" class="style4">
					Importe Paquete</td>
				<td style="background-color: white; width: 306px;">
					<asp:TextBox Width="113px" ID="txtImporteAereo" runat="server" MaxLength="21" Height="20px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="height: 22px; text-align: left; font-size: medium; color: #000080; font-family: 'Berlin Sans FB Demi'; background-color: #C0C0C0;" 
                    colspan="3">
					<span class="style1">Estad&iacute;a</span></td>
			</tr>
			<tr>
				<td style="background-color: white" class="style4">
					Estad&iacute;a/Ciudad</td>
				<td style="background-color: white; width: 306px;">
					<asp:TextBox ID="txtEstadia" runat="server" Height="50px" MaxLength="50" Width="300px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style5">
					Pasajeros</td>
				<td style="background-color: white; width: 306px; height: 22px;">
					<asp:TextBox Width="113px" ID="txtPasajeros" runat="server" MaxLength="50" Height="20px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 22px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; font-size: small;" class="style5">
					Importe Estad&iacute;a</td>
				<td style="background-color: white; width: 306px; height: 22px;">
					<asp:TextBox ID="txtImporteEstadia" runat="server" Height="20px" MaxLength="21" Width="113px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 22px;">
				</td>
			</tr>
			<tr>
				<td colspan="3" 
                    style="height: 22px; color: #000080; font-size: medium; background-color: #C0C0C0;">
					<span class="style2">Traslados</span></td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style6">
					Detalles</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtDetallesAuto" runat="server" Height="50px" MaxLength="100" Width="300px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; font-size: small;" class="style6">
					Importe Traslados</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtImporteAuto" runat="server" Height="20px" MaxLength="21" Width="113px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; font-size: small;" class="style6">
					&nbsp;</td>
				<td style="background-color: white; width: 306px; height: 21px; text-align: right;
					font-size: small;">
					<b style="text-align: right">&nbsp;&nbsp; Importe Total</b></td>
				<td style="background-color: white; width: 106px; height: 21px;">
					<asp:TextBox ID="txtImporteTotal" runat="server" Height="20px" MaxLength="21" Width="113px"></asp:TextBox>
				</td>
			</tr>
			<tr>
				<td colspan="3" style="background-color: #669999; height: 21px; font-size: medium;
					color: white;">
					<b>Informaci&oacute;n del cliente</b></td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style6">
					Apellido</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtApellido" runat="server" Height="20px" MaxLength="100" Width="301px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style6">
					Nombre</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtNombre" runat="server" Height="20px" MaxLength="100" Width="301px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style6">
					CUIT / CUIL DNI</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtCUITDNI" runat="server" Height="20px" MaxLength="15" Width="301px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style6">
					Domicilio</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtDomicilio" runat="server" Height="20px" MaxLength="100" Width="301px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style6">
					Tel&eacute;fonos</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtTelefonos" runat="server" Height="20px" MaxLength="50" Width="301px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white; " class="style6">
					Email</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtEmail" runat="server" Height="20px" MaxLength="100" Width="235px"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;
				</td>
			</tr>
		</table>
		<table style="width: 509px"  >
			<tr>
				<td align="center">
					<input id="btnImprimir" type="button" value="Imprimir" onclick="Imprimir(this);" />
					<input id="btnGuardar" type="button" value="Guardar como..." onclick="Guardar();">
					<input id="btnCerrar" type="button" value="Cerrar" onclick="Cerrar();">
				</td>
			</tr>
		</table>
	</form>
</body>
</html>
