<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AMCliente.aspx.cs" Inherits="AMCliente"  Title="Administración de Clientes" %>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table class="TablaComun">
        <tr>
            <td style="background-color: white; width: 109px; font-size: small;">Nombre</td>
            <td style="background-color: white">
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" Width="200px" 
                    Font-Size="Medium"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Requerido" ControlToValidate="txtNombre" Display="Dynamic"></asp:RequiredFieldValidator>
				<asp:HiddenField ID="txtIdCliente" runat="server" />
			</td>
        </tr>
        <tr>
            <td style="background-color: white; width: 109px; font-size: small;">Apellido</td>
            <td style="background-color: white">
                <asp:TextBox ID="txtApellido" runat="server" MaxLength="100" Width="200px" 
                    Font-Size="Medium"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Requerido" ControlToValidate="txtApellido" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="background-color: white; width: 109px; font-size: small;">CUIT / CUIL / 
                DNI</td>
            <td style="background-color: white">
                <asp:TextBox ID="txtDNI" MaxLength="11" runat="server" Width="200px" 
                    Font-Size="Medium"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Requerido" ControlToValidate="txtDNI" Display="Dynamic"></asp:RequiredFieldValidator>
				&nbsp;<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtDNI"
					Display="Dynamic" ErrorMessage="Ingrese un DNI válido" MaximumValue="2028057496777"
					MinimumValue="1000000" Type="Double"></asp:RangeValidator></td>
        </tr>
        <tr>
            <td style="background-color: white; width: 109px; font-size: small;">Domicilio</td>
            <td style="background-color: white">
                <asp:TextBox ID="txtDomicilio" runat="server" MaxLength="100" Width="200px" 
                    Font-Size="Medium"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Requerido" ControlToValidate="txtDomicilio" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="background-color: white; width: 109px; font-size: small;">Email</td>
            <td style="background-color: white">
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="200px" 
                    Font-Size="Medium"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Display="Dynamic"
                    ErrorMessage="Requerido" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
                &nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                    ErrorMessage="Ingrese un mail válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmail"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td style="background-color: white; width: 109px; font-size: small;">Teléfonos</td>
            <td style="background-color: white">
                <asp:TextBox ID="txtTelefonos" runat="server" MaxLength="50" Width="200px" 
                    Font-Size="Medium"></asp:TextBox>
                </td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="background-color: white">
                <cc1:CaptchaControl ID="CaptchaControl1" runat="server" Width="320px" 
                    Font-Size="12"  Text="Ingrese el código de seguridad." 
                    style="font-size: small"  CaptchaMaxTimeout="3600"/>        
                <br />
                <asp:Button ID="btnAceptar" runat="server" Text="Aceptar" OnClick="btnAceptar_Click" Font-Size="Small" Width="95px" />
            </td>
        </tr>
    </table>
    <br />
    <span style="font-size: 10pt; color: #ff0000">*Tenga en cuenta que los datos ingresados no serán divulgados, por conscuencia tenga 
                                                            bien tener presente resguardar los datos ingresados debido 
                                                            a que el sistema no permitirá el ingreso mediante usuarios con claves de 
                                                            autenticación que den coincidencia con los datos ingresados en la cuenta de 
                                                            email; nombre y apellido; y/o CUIT/DNI.*<br />
    </span>
</asp:Content>

