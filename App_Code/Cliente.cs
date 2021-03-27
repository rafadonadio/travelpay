using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Core
{
	/// <summary>
	/// Summary description for Cliente
	/// </summary>
	public class Cliente
	{
		public Cliente()
		{
		}
		
		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}
		private Proveedor proveedor;

		public Proveedor Proveedor
		{
			get { return proveedor; }
			set { proveedor = value; }
		}
	
		private string nombre;

		public string Nombre
		{
			get { return nombre; }
			set { nombre = value; }
		}

		private string apellido;

		public string Apellido
		{
			get { return apellido; }
			set { apellido = value; }
		}

		private string cuitDni;

		public string CuitDni
		{
			get { return cuitDni; }
			set { cuitDni = value; }
		}

		private string domicilio;

		public string Domicilio
		{
			get { return domicilio; }
			set { domicilio = value; }
		}

		private string email;

		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		private string telefonos;

		public string Telefonos
		{
			get { return telefonos; }
			set { telefonos = value; }
		}

		public string ApellidoNombre
		{
			get {
				string separador = "";
				if (!string.IsNullOrEmpty(apellido) && !string.IsNullOrEmpty(nombre)) {
					separador = ", ";
				}
				return apellido + separador + nombre; }
		}
		private bool soloLectura;

		public bool SoloLectura
		{
			get { return soloLectura; }
			set { soloLectura = value; }
		}
	
	
	}
}
