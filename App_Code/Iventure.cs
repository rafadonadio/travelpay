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
/// Summary description for Iventure
/// </summary>
	public class Iventure
	{

		public Iventure()
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
		private Cliente cliente;

		public Cliente Cliente
		{
			get { return cliente; }
			set { cliente = value; }
		}
		private string tipoAereo;

		public string TipoAereo
		{
			get { return tipoAereo; }
			set { tipoAereo = value; }
		}

		private string detalleAereo;

		public string DetalleAereo
		{
			get { return detalleAereo; }
			set { detalleAereo = value; }
		}

		private double importeAereo;

		public double ImporteAereo
		{
			get { return importeAereo; }
			set { importeAereo = value; }
		}

		private string ciudadEstadia;

		public string CiudadEstadia
		{
			get { return ciudadEstadia; }
			set { ciudadEstadia = value; }
		}
		private int pasajerosEstadia;

		public int PasajerosEstadia
		{
			get { return pasajerosEstadia; }
			set { pasajerosEstadia = value; }
		}

		private double importeEstadia;

		public double ImporteEstadia
		{
			get { return importeEstadia; }
			set { importeEstadia = value; }
		}

		private string detalleAutomovil;

		public string DetalleAutomovil
		{
			get { return detalleAutomovil; }
			set { detalleAutomovil = value; }
		}

		private double importeAutomovil;

		public double ImporteAutomovil
		{
			get { return importeAutomovil; }
			set { importeAutomovil = value; }
		}
		private double importeTotal;

		public double ImporteTotal
		{
			get { return importeTotal; }
			set { importeTotal = value; }
		}

		private DateTime? vencimiento;

		public DateTime? Vencimiento
		{
			get { return vencimiento; }
			set { vencimiento = value; }
		}

		private string estado;

		public string Estado
		{
			get {
				if (vencimiento < DateTime.Today && estado == Util.PENDIENTE) {
					return Util.VENCIDO;
				}
				return estado; }
			set { estado = value; }
		}
	
		public string RazonSocialProveedor {
			get { return Proveedor.RazonSocial; }
		}
		public string ApellidoNombreCliente
		{
			get { return Cliente.ApellidoNombre; }
		}

		private string vendedor;

		public string Vendedor
		{
			get { return vendedor; }
			set { vendedor = value; }
		}

		private int idVendedor;

		public int IdVendedor
		{
			get { return idVendedor; }
			set { idVendedor = value; }
		}
		private int cntCuotas;

		public int CntCuotas
		{
			get { return cntCuotas; }
			set { cntCuotas = value; }
		}

		private DateTime? fechaPagado;

		public DateTime? FechaPagado
		{
			get { return fechaPagado; }
			set { fechaPagado = value; }
		}
		private bool esDonacion;

		public bool EsDonacion
		{
			get { return esDonacion; }
			set { esDonacion = value; }
		}
	
	}		
}

