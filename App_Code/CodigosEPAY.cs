using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for CodigosEPAY
/// </summary>
public class CodigosEPAY
{
	private static Hashtable codigos = new Hashtable();
	private static Hashtable tarjetas = new Hashtable();
	static CodigosEPAY()
	{
		#region Códigos Operaciones
		//codigos.Add(-1, "APROBADA");
		//codigos.Add(00, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(01, "NO AUTORIZADA - VERIFIQUE SUS DATOS");
		//codigos.Add(02, "NO AUTORIZADA - VERIFIQUE SUS DATOS");
		//codigos.Add(03, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(04, "NO AUTORIZADA");
		//codigos.Add(05, "DENEGADA");
		//codigos.Add(07, "NO AUTORIZADA");
		//codigos.Add(08, "APROBADA");
		//codigos.Add(11, "APROBADA");
		//codigos.Add(12, "NO AUTORIZADA");
		//codigos.Add(13, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(14, "TARJETA INVÁLIDA");
		//codigos.Add(19, "REPITA TRANSACCIÓN");
		//codigos.Add(25, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(30, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(31, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(33, "TARJETA VENCIDA");
		//codigos.Add(34, "TARJETA VENCIDA");
		//codigos.Add(38, "DENEGADA");
		//codigos.Add(39, "DENEGADA");
		//codigos.Add(41, "DENEGADA");
		//codigos.Add(43, "DENEGADA");
		//codigos.Add(45, "NO OPERA EN CUOTAS");
		//codigos.Add(46, "TARJETA NO VIGENTE");
		//codigos.Add(47, "DENEGADA");
		//codigos.Add(48, "DENEGADA. EXCEDE CANT. MAX. CUOTAS");
		//codigos.Add(49, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(50, "DENEGADA. SUPERA LIMITE COMPRA");
		//codigos.Add(51, "DENEGADA. FONDOS INSUFICIENTES");
		//codigos.Add(53, "DENEGADA. CUENTA INEXISTENTE");
		//codigos.Add(54, "DENEGADA. TARJETA VENCIDA");
		//codigos.Add(55, "DENEGADA. DATOS INCORRECTOS");
		//codigos.Add(56, "DENEGADA. TARJETA NO HABILITADA");
		//codigos.Add(57, "DENEGADA. TRANSACCION NO PERMITIDA");
		//codigos.Add(58, "DENEGADA. TRANSACCION NO PERMITIDA");
		//codigos.Add(61, "DENEGADA. SUPERA LIMITE COMPRA");
		//codigos.Add(65, "DENEGADA. SUPERA LIMITE COMPRA");
		//codigos.Add(76, "NO AUTORIZADA - VERIFIQUE SUS DATOS");
		//codigos.Add(77, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(78, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(80, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(82, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(89, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(91, "NO AUTORIZADA. INTENTE NUEVAMENTE");
		//codigos.Add(94, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(95, "NO AUTORIZADA. INTENTE NUEVAMENTE");
		//codigos.Add(96, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(110, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(111, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(120, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(121, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(130, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(131, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(140, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(141, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(142, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(143, "DENEGADA. DATOS INCORRECTOS");
		//codigos.Add(144, "DENEGADA. COD. SEGURIDAD REQUERIDO");
		//codigos.Add(145, "DENEGADA. DATOS INCORRECTOS");
		//codigos.Add(146, "DENEGADA. TARJETA NO ENCONTRADA");
		//codigos.Add(147, "DENEGADA. TARJETA VENCIDA");
		//codigos.Add(148, "DENEGADA. VENCIMIENTO INVÁLIDO");
		//codigos.Add(150, "DENEGADA. TRANSACCION INVÁLIDA");
		//codigos.Add(151, "DENEGADA. TRANSACCIÓN NO HABILITADA");
		//codigos.Add(152, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(153, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(154, "NO AUTORIZADA. INTENTE NUEVAMENTE");
		//codigos.Add(160, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(161, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(170, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(171, "NO AUTORIZADA. TRN YA ANULADA");
		//codigos.Add(172, "NO AUTORIZADA. TRN YA REVERSADA");
		//codigos.Add(173, "NO AUTORIZADA. TRN EN LOTE CERRADO");
		//codigos.Add(176, "NO AUTORIZADA. EXCEDE RESERVA");
		//codigos.Add(180, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(181, "FALLO DE COMUNICACIÓN");
		//codigos.Add(182, "FALLO DE COMUNICACIÓN. REINTENTE");
		//codigos.Add(183, "FALLO COMUNICACIÓN. LLAMAR ADMIN.");
		//codigos.Add(201, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(202, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(203, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(204, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(205, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(206, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(207, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(208, "NO AUTORIZADA. LLAMAR ADMIN. SITIO");
		//codigos.Add(209, "ORDER ID DUPLICADO");
		//codigos.Add(210, "TIPO DE TARJETA INVÁLIDO");
		//codigos.Add(211, "LA TARJETA INGRESADA POR EL USUARIO NO APLICA A LA PROMOCIÓN ENVIADA POR EL COMERCIO");
		//codigos.Add(212, "TARJETA EN LISTA NEGRA");
		//codigos.Add(216, "POOL SIN TERMINALES INTERNAS LIBRES");
		//codigos.Add(220, "TIEMPO AGOTADO PARA LA PAGINA");
		//codigos.Add(221, "COMERCIO NO CONFIGURADO PARA OPERAR EN PRODUCCIÓN");
		//codigos.Add(222, "SERVICIO BPAG NO SE ENCUENTRA HABILITADO");
		//codigos.Add(223, "SERVICIO BPAG NO SE ENCUENTRA HABILITADO PARA EL COMERCIO");
		//codigos.Add(224, "ERROR DE COMUNICACION CON EL WEB SERVICE BPAG.");
		//codigos.Add(225, "ERROR INTERNO EN EL WEB SERVICE BPAG");
		//codigos.Add(226, "ERROR INTERNO EN EL WEB SERVICE BPAG");
		//codigos.Add(227, "TRANSACCION RECHAZADA");
		//codigos.Add(228, "ERROR AL REGISTRAR REQUERIMIENTO");
		
#endregion Códigos Operaciones
		#region Códigos Tarjetas
		tarjetas.Add("AMEX", "American Express");
		tarjetas.Add("MASTERCARD", "Mastercard");
		tarjetas.Add("VISA", "Visa");
		#endregion Códigos Tarjetas
	}

	/*public static string GetDescripcion(int codigo) {
		if (codigos[codigo] != null)
			return (string)codigos[codigo];
		else
			return "Código inválido.";
	}*/

	public static string GetTarjeta(string codigo)
	{
		if (tarjetas[codigo] != null)
			return (string)tarjetas[codigo];
		else
			return "Código de tarjeta inválido.";
	}
	public static bool IsAuthorized(string messageDescrip)
	{
		return messageDescrip.ToLower() == "authorized";
	}

	#region Códigos Tarjetas
	public static string GetCodeCard(string descriptionCard)
	{
		IEnumerator enu = tarjetas.GetEnumerator();
		DictionaryEntry dEntry;
		while (enu.MoveNext()) {
			dEntry = (DictionaryEntry)enu.Current;
			if (dEntry.Value.ToString().ToLower() == descriptionCard.ToLower()) {
				return dEntry.Key.ToString();
			}
		}
		return "Código de tarjeta inválido.";
	}


	#endregion Códigos Tarjetas

}
