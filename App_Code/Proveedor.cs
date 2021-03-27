using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace Core
{
	/// <summary>
	/// Summary description for Proveedor
	/// </summary>
	public class Proveedor
	{
		public Proveedor()
		{
		}
		#region Propiedades
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		private string gateway;

		public string Gateway
		{
			get { return gateway; }
			set { gateway = value; }
		}

		private string idGateway;

		public string IdGateway
		{
			get { return idGateway; }
			set { idGateway = value; }
		}
		private string email;

		public string Email
		{
			get { return email; }
			set { email = value; }
		}
		private string cuit;

		public string CUIT
		{
			get { return cuit; }
			set { cuit = value; }
		}
		private string iibb;

		public string IIBB
		{
			get { return iibb; }
			set { iibb = value; }
		}
		private string razonSocial;

		public string RazonSocial
		{
			get { return razonSocial; }
			set { razonSocial = value; }
		}
		private DateTime alta;

		public DateTime Alta
		{
			get { return alta; }
			set { alta = value; }
		}

		private string nombreResp;

		public string NombreResp
		{
			get { return nombreResp; }
			set { nombreResp = value; }
		}

		private string apellidoResp;

		public string ApellidoResp
		{
			get { return apellidoResp; }
			set { apellidoResp = value; }
		}

		private string dniResp;

		public string DNIResp
		{
			get { return dniResp; }
			set { dniResp = value; }
		}

		private string cargoResp;

		public string CargoResp
		{
			get { return cargoResp; }
			set { cargoResp = value; }
		}

		public string Responsable
		{
			get { return apellidoResp + ((!String.IsNullOrEmpty(apellidoResp) && !String.IsNullOrEmpty(nombreResp)) ? ", " : String.Empty) + nombreResp; }
		}
		private int cntTransacciones;

		public int CntTransacciones
		{
			get { return cntTransacciones; }
			set { cntTransacciones = value; }
		}
		private double montoTransacciones;

		public double MontoTransacciones
		{
			get { return montoTransacciones; }
			set { montoTransacciones = value; }
		}
		private string urlImagen;

		public string UrlImagen
		{
			get { return urlImagen; }
			set { urlImagen = value; }
		}

		private string telefonos;

		public string Telefonos
		{
			get { return telefonos; }
			set { telefonos = value; }
		}

		private string tarjetas;

		public string Tarjetas
		{
			get { return GetTarjetas(); }
			set { SetTarjetas(value); }
		}

		private string claveEncNPS;

		public string ClaveEncNPS
		{
			get { return claveEncNPS; }
			set { claveEncNPS = value; }
		}
		private bool devNPS;

		public bool DevNPS
		{
			get { return devNPS; }
			set { devNPS = value; }
		}

		private int diasVencimiento;
		public int DiasVencimiento
		{
			get { return diasVencimiento; }
			set { diasVencimiento = value; }
		}
		#endregion Propiedades

		private string GetTarjetas()
		{
			StringBuilder sb = new StringBuilder();
			if (!string.IsNullOrEmpty(americanExpress))
			{
				sb.Append(americanExpress + "|");
			}
			if (!string.IsNullOrEmpty(cabal))
			{
				sb.Append(cabal + "|");
			}
			if (!string.IsNullOrEmpty(diners))
			{
				sb.Append(diners + "|");
			}
			if (!string.IsNullOrEmpty(mastercard))
			{
				sb.Append(mastercard + "|");
			}
			if (!string.IsNullOrEmpty(naranja))
			{
				sb.Append(naranja + "|");
			}
			if (!string.IsNullOrEmpty(nevada))
			{
				sb.Append(nevada + "|");
			}
			if (!string.IsNullOrEmpty(visa))
			{
				sb.Append(visa + "|");
			}
			if (sb.Length > 0)
			{
				sb = sb.Remove(sb.Length - 1, 1);
			}
			return sb.ToString();
		}
		private void SetTarjetas(string tarjetas)
		{
			string[] valTarj = tarjetas.Split("|".ToCharArray());
			string marcaTarjeta;
			foreach (string tarjeta in valTarj)
			{
				if (gateway == "NPS - Sub1")
					marcaTarjeta = CodigosNPS.GetTarjeta(tarjeta).ToLower();
				else if (gateway == "ePayments")
					marcaTarjeta = CodigosEPAY.GetTarjeta(tarjeta).ToLower();
				else if (gateway == "PagoFacil")
					//TODO: VER: si Pago Facil va como GateWay
					marcaTarjeta = CodigosEPAY.GetTarjeta(tarjeta).ToLower();
				else
					throw new Exception("Nombre de Gateway incorrecto.");
				switch (marcaTarjeta)
				{
					case "american express": SetAmericanExpress = tarjeta;
						break;
					case "cabal": SetCabal = tarjeta;
						break;
					case "diners": SetDiners = tarjeta;
						break;
					case "mastercard": SetMastercard = tarjeta;
						break;
					case "naranja": SetNaranja = tarjeta;
						break;
					case "nevada": SetNevada = tarjeta;
						break;
					case "visa": SetVisa = tarjeta;
						break;

				};
			}
		}
		#region Manejo de Tarjetas
		private string americanExpress;
		public string SetAmericanExpress
		{
			set { americanExpress = value; }
		}
		private string cabal;
		public string SetCabal
		{
			set { cabal = value; }
		}
		private string diners;
		public string SetDiners
		{
			set { diners = value; }
		}
		private string mastercard;
		public string SetMastercard
		{
			set { mastercard = value; }
		}
		private string naranja;
		public string SetNaranja
		{
			set { naranja = value; }
		}
		private string nevada;
		public string SetNevada
		{
			set { nevada = value; }
		}
		private string visa;
		public string SetVisa
		{
			set { visa = value; }
		}
		public bool TieneAmericanExpress
		{
			get { return !string.IsNullOrEmpty(americanExpress); }
		}
		public bool TieneCabal
		{
			get { return !string.IsNullOrEmpty(cabal); }
		}
		public bool TieneDiners
		{
			get { return !string.IsNullOrEmpty(diners); }
		}
		public bool TieneMastercard
		{
			get { return !string.IsNullOrEmpty(mastercard); }
		}
		public bool TieneNaranja
		{
			get { return !string.IsNullOrEmpty(naranja); }
		}
		public bool TieneNevada
		{
			get { return !string.IsNullOrEmpty(nevada); }
		}
		public bool TieneVisa
		{
			get { return !string.IsNullOrEmpty(visa); }
		}
		public string AmericanExpress
		{
			get { return americanExpress; }
		}
		public string Cabal
		{
			get { return cabal; }
		}
		public string Diners
		{
			get { return diners; }
		}
		public string Mastercard
		{
			get { return mastercard; }
		}
		public string Naranja
		{
			get { return naranja; }
		}
		public string Nevada
		{
			get { return nevada; }
		}
		public string Visa
		{
			get { return visa; }
		}

		public string GetCodeCard(string descriptionCard)
		{
			if (gateway == "NPS - Sub1")
				return CodigosNPS.GetCodeCard(descriptionCard);
			else //if (gateway == "ePayments")
				return CodigosEPAY.GetCodeCard(descriptionCard);
			
		}
		#endregion Manejo de Tarjetas
		public static string logoDefault = "images/Logos/travelPay.png";
	}

}