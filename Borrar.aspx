<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Borrar.aspx.cs" Inherits="Borrar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>

</head>
<body>
<form runat=server id=ppp><asp:Button ID="Button1"
		   runat="server" Text="Button" OnClick="Button1_Click" /></form>
   <form id=GateWayData action="https://services.nps.com.ar/_ws_res_code_desa.php" method=post>
	   <input id="EncData" name="EncData" type="hidden" runat=server />		
	   <input id="MerchantID" name="MerchantID" type="hidden" value="test_travelpay" />
	   <input id="Submit1" name="Submit1" type="submit" value="submit" />
	   <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
	   <hr />
	   
	</form>
	<table border="1">
		<tr>
			<td style="width: 100px">
			</td>
			<td style="width: 100px">
			</td>
			<td style="width: 100px">
			</td>
		</tr>
		<tr>
			<td style="width: 100px">
			</td>
			<td style="width: 100px">
			</td>
			<td style="width: 100px">
			</td>
		</tr>
		<tr>
			<td style="width: 100px">
			</td>
			<td style="width: 100px">
			</td>
			<td style="width: 100px">
			</td>
		</tr>
	</table>
</body>
</html>
