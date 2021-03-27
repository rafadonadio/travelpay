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

public partial class PruebaPagoFacil : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
		int i = 0;
		Label1.Text = Request.Form["DatosEnc"];
    }
	protected void Button1_Click(object sender, EventArgs e)
	{
		//Variables a completar con los datos del sitio, de la compra y del usuario.
		 string v_id_emec = "33350001";                      //Número de identificación del Sitio de Compra. (valor asignado por Pago Fácil).
		 string v_id_operacion = "123456";                   //Identificación de la Compra/Cliente.
		 string v_id_moneda = "01";                          //Identificación de la moneda: 01 pesos.
		 string v_va_monto = "4500";                         //Monto de la compra. Formato 6e 2d. Ej. 4500= $45.-
		 string v_fecha_transaccion = "03072001";            //Fecha de la compra. Formato DDMMAAAA.
		 string v_hora_transaccion = "1546";                 //Hora de la compra. Formato HHMM.
		 string v_dias_vigencia = "5";                       //Días de vigencia de la compra.
		 string v_email_usr = "fgaviria@pagofacil.com.ar";   //email del usuario (email al que se le enviará el comprobante).
		 string v_nombre_usr = "Federico";                   //Nombre del usuario.
		 string v_apellido_usr = "Gaviria";                  //Apellido del usuario.
		 string v_domicilio_usr = "Moreno 490";              //Direccion del usuario.
		 string v_localidad_usr = "Capital Federal";         //Localidad del usuario.
		 string v_provincia_usr = "Capital Federal";    	    //Provincia del usuario.
		 string v_pais_usr = "Argentina";                    //Pais del usuario.
		 string v_email_alter = "info@e-pagofacil.com";      //email alternativo de envio.

		Response.Redirect("http://desa.e-pagofacil.com/2130/index.php?p_id_emec=" + v_id_emec + "&p_id_operacion=" + v_id_operacion + "&p_id_mone_ecom=" + v_id_moneda + "&p_va_monto=" + v_va_monto + "&p_fe_transaccion=" + v_fecha_transaccion + "&p_fe_hora=" + v_hora_transaccion + "&p_dias_vigencia=" + v_dias_vigencia + "&p_direccion_email_usua=" + v_email_usr + "&p_nombre_usuario=" + v_nombre_usr + "&p_apellido_usuario=" + v_apellido_usr + "&p_domicilio_usuario=" + v_domicilio_usr + "&p_localidad=" + v_localidad_usr + "&p_provincia_usuario=" + v_provincia_usr + "&p_pais_usuario=" + v_pais_usr + "&p_direccion_email_alter=" + v_email_alter);
	}
}
