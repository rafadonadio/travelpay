// JScript File
function GetXmlHttpObject()
{
//alert("GetXmlHttpObject");
	var xmlHttp=null;
	if(navigator.appName == "Microsoft Internet Explorer"){
		// Internet Explorer
		try
		{
//		alert(1);
			xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
		}
		catch (ex)
		{
//		alert(2);
			xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
		}
	}
	else{
		try
		{
//			alert(0);
			// Firefox, Opera 8.0+, Safari
			xmlHttp=new XMLHttpRequest();
		}
		catch (e)
		{
		}
	}
	return xmlHttp;
}


