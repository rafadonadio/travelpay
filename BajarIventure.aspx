<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BajarIventure.aspx.cs" Inherits="BajarIventure" %>

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
		<table class="TablaComun" runat="server" id="tblIventure2" border="2">
			<tr runat="server">
				<td style="background-color: #C0C0C0; height: 20px; text-align: center; font-size: 20px;
					color: #800000; font-family: 'Berlin Sans FB';" colspan="3" id="TD1" runat="server">
					BAJAR - Solicitud</td>
			</tr>
			<tr runat="server">
				<td style="background-color: white;" id="TD4" runat="server" class="style3">
					Id_Solicitud</td>
				<td style="background-color: white; width: 306px; height: 20px;" id="TD2" runat="server">
					<asp:TextBox ID="txtIdIventure" runat="server" Height="20px" Width="113px" 
                        Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 20px;" id="TD3" runat="server">
				</td>
			</tr>
			<tr>
				<td style="background-color: white;" class="style3">
					Empresa</td>
				<td style="background-color: white; width: 306px; height: 20px;">
					<asp:TextBox ID="txtProveedor" runat="server" Height="20px" MaxLength="100" 
                        Width="113px" Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 20px;">
				</td>
			</tr>
			<tr>
				<td style="background-color: white; height: 20px;">
					Vendedor</td>
				<td style="background-color: white; width: 306px; height: 20px;">
					<asp:TextBox ID="txtVendedor" runat="server" Height="20px" MaxLength="100" 
                        Width="113px" Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 20px;">
				</td>
			</tr>
			<tr>
				<td style="background-color: white">
					Detalle</td>
				<td style="background-color: white; width: 306px;">
					<asp:TextBox Width="300px" ID="txtDetalle" runat="server" MaxLength="100" 
                        Height="88px" Font-Size="Small"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px;">
				</td>
			</tr>
			<tr>
				<td style="background-color: white">
					Importe</td>
				<td style="background-color: white; width: 306px;">
					<asp:TextBox Width="113px" ID="txtImporte" runat="server" MaxLength="21" 
                        Height="20px" Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white">
					Cnt. Cuotas </td>
				<td style="background-color: white; width: 306px;">
					<asp:DropDownList ID="cboCntCuotas" runat="server" Font-Size="Medium">
								<asp:ListItem Selected=True Value=1>01</asp:ListItem>
								<asp:ListItem Value=2>02</asp:ListItem>
								<asp:ListItem Value=3>03</asp:ListItem>
								<asp:ListItem Value=4>04</asp:ListItem>
								<asp:ListItem Value=5>05</asp:ListItem>
								<asp:ListItem Value=6>06</asp:ListItem>
								<asp:ListItem Value=7>07</asp:ListItem>
								<asp:ListItem Value=8>08</asp:ListItem>
								<asp:ListItem Value=9>09</asp:ListItem>
								<asp:ListItem Value=10>10</asp:ListItem>
								<asp:ListItem Value=11>11</asp:ListItem>
								<asp:ListItem Value=12>12</asp:ListItem>
								<asp:ListItem Value=18>18</asp:ListItem>
								<asp:ListItem Value=24>24</asp:ListItem>
								<asp:ListItem Value=36>36</asp:ListItem>
							</asp:DropDownList>
				</td>
				<td style="background-color: white; width: 106px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td colspan="3" style="background-color: #669999; height: 21px; font-size: medium;
					color: white;">
					<b>Informaci&oacute;n del cliente</b></td>
			</tr>
			<tr>
				<td style="background-color: white;" class="style6">
					Apellido</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtApellido" runat="server" Height="20px" MaxLength="100" 
                        Width="301px" Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white;" class="style6">
					Nombre</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtNombre" runat="server" Height="20px" MaxLength="100" 
                        Width="301px" Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white;" class="style6">
					CUIT / CUIL DNI</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtCUITDNI" runat="server" Height="20px" MaxLength="15" 
                        Width="301px" Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white;" class="style6">
					Domicilio</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtDomicilio" runat="server" Height="20px" MaxLength="100" 
                        Width="301px" Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white;" class="style6">
					Tel&eacute;fonos</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtTelefonos" runat="server" Height="20px" MaxLength="50" 
                        Width="301px" Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;</td>
			</tr>
			<tr>
				<td style="background-color: white;" class="style6">
					Email</td>
				<td style="background-color: white; width: 306px; height: 21px;">
					<asp:TextBox ID="txtEmail" runat="server" Height="20px" MaxLength="100" 
                        Width="301px" Font-Size="Medium"></asp:TextBox>
				</td>
				<td style="background-color: white; width: 106px; height: 21px;">
					&nbsp;
				</td>
			</tr>
		</table>
		<br /><div align=center>
	<span style="font-size: 10pt; color: #ff0000" runat="server" id="lblTenga" >* EL PRESENTE DOCUMENTO NO ES VÁLIDO COMO COMPROBANTE FISCAL *</span></div>
		<table style="width: 509px">
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
