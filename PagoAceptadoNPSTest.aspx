<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
	CodeFile="PagoAceptadoNPSTest.aspx.cs" Inherits="PagoAceptadoNPSTest" Title="Pago Aceptado" %>

<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script>
function Imprimir(){
document.getElementById("Encabezado").style.visibility = "hidden";
document.getElementById("FinEncabezado").style.visibility = "hidden";
document.getElementById("filaTitulo").style.visibility = "hidden";
document.getElementById("filaFecha").style.visibility = "hidden";
document.getElementById("FilaFooter").style.visibility = "hidden";
document.getElementById("tblStatus").style.visibility = "hidden";
document.getElementById("filaBoton").style.visibility = "hidden";
document.getElementById("celdaBody").style.borderRightStyle="none";
document.getElementById("celdaBody").style.borderTopStyle="none";
document.getElementById("celdaBody").style.borderLeftStyle="none";

document.title = "Comprobante de pago -- "+ document.getElementById('<%= lblDescripcion.ClientID %>').innerHTML;

window.print();

document.getElementById("Encabezado").style.visibility = "visible";
document.getElementById("FinEncabezado").style.visibility = "visible";
document.getElementById("filaTitulo").style.visibility = "visible";
document.getElementById("filaFecha").style.visibility = "visible";
document.getElementById("FilaFooter").style.visibility = "visible";
document.getElementById("tblStatus").style.visibility = "visible";
document.getElementById("filaBoton").style.visibility = "visible";
document.getElementById("celdaBody").style.borderRightStyle="solid";
document.getElementById("celdaBody").style.borderTopStyle="solid";
document.getElementById("celdaBody").style.borderLeftStyle="solid";

}
</script>

	<ajax:AjaxPanel runat="server" ID="ajxpnl">
		<table class="TablaComun" align="center" id=tblStatus name=tblStatus>
			<tr>
				<td style="font-size: medium; text-align: center;">
					Se ha realizado su pago correctamente.</td>
			</tr>
			<tr>
				<td>
					<br />
				</td>
			</tr>
			<tr>
				<td>
					<asp:Label ID="lblData" runat="server"></asp:Label></td>
			</tr>
		</table>
		<p></p>
		<table class="Comun" border="1"  align="center">
			<tr>
				<td>
					<table align="center">
						<tr>
							<td colspan="7" align="center">
								<asp:Image ID="imgEmpresa" runat="server"  Width="70px"/>
							</td>
						</tr>
						<tr>
							<td colspan="7" align="center">
							Comprobante de pago
							</td>
						</tr>
						<tr>
							<td colspan="7" align="center">
							<b><asp:Label ID="lblDescripcion" runat="server"></asp:Label></b>
							</td>
						</tr>
						<tr>
							<td colspan="7">
								<hr />
							</td>
						</tr>
						<tr>
							<td>
								&nbsp;&nbsp;&nbsp;</td>
							<td>
								Fecha</td>
							<td>
								&nbsp;&nbsp;&nbsp;</td>
							<td>
								Hora</td>
							<td>
								&nbsp;&nbsp;&nbsp;</td>
							<td>
								Nro. Trans.</td>
							<td>
								&nbsp;&nbsp;&nbsp;</td>
						</tr>
						<tr>
							<td>
								&nbsp;&nbsp;&nbsp;</td>
							<td>
								<asp:Label ID="lblFecha" runat="server"></asp:Label></td>
							<td>
								&nbsp;&nbsp;&nbsp;</td>
							<td>
								<asp:Label ID="lblHora" runat="server"></asp:Label></td>
							<td>
								&nbsp;&nbsp;&nbsp;</td>
							<td>
								<asp:Label ID="lblTrans" runat="server"></asp:Label></td>
							<td>
								&nbsp;&nbsp;&nbsp;</td>
						</tr>
						<tr>
							<td colspan="7">
								<hr />
							</td>
						</tr>
						<tr align=left>
							<td colspan="7">
								<table>
									<tr>
										<td align="left">
											Importe:&nbsp;</td>
										<td align="left">$
											<asp:Label ID="lblImporte" runat="server"></asp:Label></td>
									</tr>
									<tr>
										<td align="left">
											Nro. Solicitud:&nbsp;</td>
										<td align="left">
											<asp:Label ID="lblIdIventure" runat="server"></asp:Label></td>
									</tr>
									<tr>
										<td align="left">
											Cod. Autorizaci&oacuten:&nbsp;</td>
										<td align="left">
											<asp:Label ID="lblCodAut" runat="server"></asp:Label></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td colspan="7">
								<hr />
							</td>
						</tr>
						<tr>
							<td colspan="7" align="center">
								<asp:Image ID="imgTravel" runat="server"  ImageUrl="~/Images/travelmail.jpg" Width="70px"/>
							</td>
						</tr>
						
					</table>
					</td>
			</tr>
		</table>
		<table  align=center>
		<tr id=filaBoton><td align=center><input id="btnImprimir" type="button" value="Imprimir" onclick="Imprimir();"/></td></tr>
		</table>
	</ajax:AjaxPanel>
</asp:Content>
