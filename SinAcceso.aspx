<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SinAcceso.aspx.cs" Inherits="SinAcceso" Title="Sin Acceso" %>
<%@ Register Assembly="MagicAjax" Namespace="MagicAjax.UI.Controls" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajax:AjaxPanel runat="server" ID="ajxpnl">
    <table align="center" class="will" width="300">
        <tr>
            <td Style="font-size: 10pt; color: #ff0000" Font-Bold="True">No tiene acceso a esta página.<br/>Comuniquese con el administrador.<br /> Muchas Gracias.
			</td>
        </tr>

    </table>
    </ajax:AjaxPanel>
</asp:Content>

