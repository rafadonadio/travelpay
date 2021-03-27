<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AMUsuario.aspx.cs" Inherits="AMUsuario"  Title="Alta de Usuarios" %>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <table class="TablaComun">
        <tr>
            <td style="background-color: white">Nombre</td>
            <td style="background-color: white">
                <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" Width="200px" Font-Size="Medium"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Requerido" ControlToValidate="txtNombre" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td style="background-color: white">
				Rol</td>
            <td style="background-color: white">
				<asp:DropDownList ID="cboRol" runat="server" 
					Width="155px" Font-Size="Medium">
					<asp:ListItem Value="2">Administrador</asp:ListItem>
					<asp:ListItem Selected="True" Value="3">Vendedor</asp:ListItem>
				</asp:DropDownList>&nbsp;
			</td>
        </tr>
        <tr>
            <td style="background-color: white">Email</td>
            <td style="background-color: white">
                <asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="200px" 
                    Font-Size="Small"></asp:TextBox>
                &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Requerido" ControlToValidate="txtEmail" Display="Dynamic"></asp:RequiredFieldValidator>
				&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
					Display="Dynamic" ErrorMessage="Ingrese un email válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <td colspan="2" align="center" style="background-color: white">
                <cc1:CaptchaControl ID="CaptchaControl1" runat="server" Width="297px" Font-Size="12"  Text="Ingrese el código de seguridad."  CaptchaMaxTimeout="3600"/>        
                <br />
                <asp:Button ID="btnDarDeAlta" runat="server" Text="Aceptar" OnClick="btnDarDeAlta_Click" Font-Size="Small" Width="95px" />
            </td>
        </tr>
    </table>
    <br />
    <span style="font-size: 10pt; color: #ff0000">*Tenga en cuenta que los datos ingresados no serán divulgados, 
                                                            por conscuencia tenga bien tener presente y registrados los datos ingresados 
                                                            debido a que el sistema no permitirá el ingreso mediante usuarios con claves de 
                                                            autenticación que den coincidencia con los datos ingresados en la cuenta de 
                                                            email; nombre y apellido; y CUIT/DNI.*
        </span>                                
  </asp:Content>

