
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Doble Confirmación empresaTest2</title>
</head>
<script>
function Redirect(){
var url = document.location.toString();
url = url.replace("DC_empresaTest2", "DobleConfirmacion")
document.forms["form1"].action = url + "&MerchantId=travelPay";
document.forms["form1"].submit()
}
</script>
<body onload="Redirect()">
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
