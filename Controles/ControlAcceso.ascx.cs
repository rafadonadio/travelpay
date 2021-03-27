using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Core;

public partial class Controles_ControlAcceso : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
	{
		bool acceso = true;
		Usuario usu = (Usuario)(Session["Usuario"]);
		string url = Page.AppRelativeVirtualPath.Substring(Page.AppRelativeTemplateSourceDirectory.Length).ToLower();
		if (url == "misiventures.aspx" && (usu == null || usu.IsSuper)) acceso = false; //Solo no accede el super admin
		//Lo saque porque ahora puede entrar anonimos -- else if (url == "iventure.aspx" && usu.IsSuper) acceso = false; //Solo no accede el super admin
		else if (url == "amproveedor.aspx" && (usu == null || !usu.IsSuper)) acceso = false; //Solo accede el super admin
		else if (url == "amcliente.aspx" && (usu == null || !usu.IsAdminProveedor)) acceso = false; //Solo accede el admin proveedor
		else if (url == "amusuario.aspx" && (usu == null || !(usu.IsSuper || usu.IsAdminProveedor))) acceso = false; //el super admin y el admin proveedor
		else if (url == "reportetransacciones.aspx" && (usu == null || !usu.IsSuper)) acceso = false; //Solo accede el super admin
		else if (url == "reporteoperador.aspx" && (usu == null || !usu.IsAdminProveedor)) acceso = false; //Solo accede el admin proveedor
		else if (url == "reportecae.aspx" && (usu == null || !usu.IsSuper)) acceso = false; //Solo accede el admin proveedor
		if (!acceso)
		{
			Response.Redirect("Index2.aspx");
		}
    }
}
/*
 if (usu != null)
		{
			if (url == "misiventures.aspx" && usu.IsSuper) acceso = false; //Solo no accede el super admin
			//Lo saque porque ahora puede entrar anonimos -- else if (url == "iventure.aspx" && usu.IsSuper) acceso = false; //Solo no accede el super admin
			else if (url == "amproveedor.aspx" && !usu.IsSuper) acceso = false; //Solo accede el super admin
			else if (url == "amcliente.aspx" && !usu.IsAdminProveedor) acceso = false; //Solo accede el admin proveedor
			else if (url == "amusuario.aspx" && !(usu.IsSuper || usu.IsAdminProveedor)) acceso = false; //el super admin y el admin proveedor
			else if (url == "reportetransacciones.aspx" && !usu.IsSuper) acceso = false; //Solo accede el super admin
			else if (url == "reporteoperador.aspx" && !usu.IsAdminProveedor) acceso = false; //Solo accede el admin proveedor
			if (!acceso)
			{
				Response.Redirect("Index2.aspx");
			}
		}
 
 */