<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CambioPass.aspx.cs" Inherits="CambioPass" Title="Cambiar Contraseña" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>
<%@ Register TagPrefix="cc1" Namespace="WebControlCaptcha" Assembly="WebControlCaptcha" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajax:AjaxPanel runat="server" ID="ajxpnl">
    <table align="center" class="will">
        <tr>
            <td>Password Anterior</td>
            <td>
                <asp:TextBox ID="txtPassViejo" TextMode="Password" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Password Nuevo</td>
            <td>
                <asp:TextBox ID="txtPassNuevo" TextMode="Password" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Repita el Password Nuevo</td>
            <td>
                <asp:TextBox ID="txtPassNuevoRepeat" TextMode="Password" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan=2 align=center>
               <cc1:CaptchaControl ID="CaptchaControl1" runat="server" Width="297px" Font-Size="Smaller" Text="Ingrese el código de seguridad"  CaptchaMaxTimeout="3600"/>    
               <asp:Button runat="server" ID="btnAceptar" Text="Cambiar Contraseña" OnClick="btnCambiar_Click" /></td>
        </tr>
    </table>
    </ajax:AjaxPanel>
    <br />
    <span style="font-size: 10pt; color: #ff0000">* No divulgue ni deje constancia de dicho
        cambio de password ya que usted será el único responsable ante cualquier contingencia que pueda suceder.
        En caso de que se encuentre en un lugar público recuerde tomar sus recaudos.*<br />
    </span>
</asp:Content>