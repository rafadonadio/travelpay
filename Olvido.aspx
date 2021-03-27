<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Olvido.aspx.cs" Inherits="Olvido" Title="¿Olvidó su contraseña?" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajax:AjaxPanel runat="server" ID="ajxpnl">
    <table align="center" class="will">
        <tr>
            <td colspan=2 style="font-size: medium">Ingrese el usuario o el mail para recuperar 
                el password</td>
            
        </tr>
        <tr>
            <td align=left style="width: 26px">Usuario</td>
            <td align=left>
                <asp:TextBox ID="txtUsuario" runat="server" Font-Size="Small"></asp:TextBox></td>
        </tr>
        <tr>
            <td align=left style="width: 26px">Email</td>
            <td align=left>
                <asp:TextBox ID="txtEmail" runat="server" Font-Size="Small"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2" align=Center><asp:Button runat="server" ID="btnRecuperar" Text="Recuperar Password" OnClick="btnRecuperar_click" /></td>
        </tr>
    </table>
    </ajax:AjaxPanel>
</asp:Content>

