<%@ WebService Language="C#" Class="TravelPayWS" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using Core;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class TravelPayWS : System.Web.Services.WebService
{
	/// <summary>
	/// Crea una solicitud por medio del webService
	/// </summary>
	/// <param name="xmlDoc">Documento xml con los datos necesario para la creación</param>
	/// <returns>Código de respuesta
	/// <list type="table"><listheader><term>Código</term><description>Descripción</description></listheader>
	/// <item><term>1</term><description>Solicitud creada con éxito</description></item>
	/// <item><term>2</term><description>Usuario no autorizado para crear Iventures</description></item>
	/// <item><term>11</term><description>No se encontró el valor del nombre del usuario en el xml. El nombre del usuario es obligatorio.</description></item>
	/// <item><term>12</term><description>No se encontró el valor de la clave del usuario en el xml. La clave del usuario es obligatoria.</description></item>
	/// <item><term>21</term><description>No se encontró el valor del detalle en el xml. El detalle es obligatorio.</description></item>
	/// <item><term>22</term><description>No se encontró el valor del importe en el xml. El importe es obligatorio.</description></item>
	/// <item><term>23</term><description>Valor del importe inválido en el xml</description></item>
	/// <item><term>24</term><description>No se encontró el valor de la cantidad de cuotas en el xml. La cantidad de cuotas es obligatoria.</description></item>
	/// <item><term>25</term><description>Valor de la cantidad de cuotas inválido en el xml.</description></item>
	/// <item><term>31</term><description>No se encontró el valor del CUITDNI en el xml. El CUITDNI es obligatorio</description></item>
	/// <item><term>32</term><description>No se encontró el valor del email en el xml. El email es obligatorio.</description></item>
	/// <item><term>-1</term><description>Error. Se quiere crear un cliente y el Email de cliente ya existe.</description></item>
	/// <item><term>-4</term><description>Error. Se quiere crear un cliente y el Email de cliente ya existe.</description></item>
	/// <item><term>-2</term><description>Error. Se quiere crear un cliente y el Cuit de cliente ya está registrado</description></item>
	/// <item><term>-3</term><description>Error. Se quiere crear un cliente y el Cuit de cliente ya está registrado</description></item>
	/// </list>
	/// </returns>
	[WebMethod]
	public int CrearIventure(XmlDocument xmlDoc)
	{
		string userName, password;
		string detalle;
		double importe;
		int cntCuotas;
		Usuario usuario;
		int resDatos = ObtenerDatos(xmlDoc, out userName, out password, out detalle, out importe, out cntCuotas);
		if (resDatos == 1)
		{
			if (IsAuthorized(userName, password, out usuario))
			{
				Cliente cliente = new Cliente();
				int resCliente = CrearCliente(xmlDoc, out cliente);
				if (resCliente == 1)
				{
					return CrearIventure(detalle, importe, cntCuotas, cliente, usuario);
				}
				else
				{
					return resCliente;
				}
			}
			else
			{

				return 2;//Usuario no autorizado para crear Iventures
			}
		}
		else
		{
			return resDatos;
		}
	}
	private int ObtenerDatos(XmlDocument xmlDoc, out string userName, out string password, out string detalle, out double importe, out int cntCuotas)
	{
		userName = "";
		password = "";
		detalle = "";
		importe = -1;
		cntCuotas = -1;
		XmlNode node;
		try
		{
			node = xmlDoc.SelectSingleNode("//Iventure/Usuario/Nombre");
			userName = node.InnerXml;
			if (String.IsNullOrEmpty(userName))
			{
				return 11; //No se encontró el valor del nombre del usuario en el xml. El nombre del usuario es obligatorio.
			}
		}
		catch
		{
			return 11; //No se encontró el valor del nombre del usuario en el xml. El nombre del usuario es obligatorio.
		}
		try
		{
			node = xmlDoc.SelectSingleNode("//Iventure/Usuario/Clave");
			password = node.InnerXml;
			if (String.IsNullOrEmpty(password))
			{
				return 12; //No se encontró el valor de la clave del usuario en el xml. La clave del usuario es obligatoria.
			}
		}
		catch
		{
			return 12; //No se encontró el valor de la clave del usuario en el xml. La clave del usuario es obligatoria.
		}
		try
		{
			node = xmlDoc.SelectSingleNode("//Iventure/Detalle");
			detalle = node.InnerXml;
			if (String.IsNullOrEmpty(detalle))
			{
				return 21; //No se encontró el valor del detalle en el xml. El detalle es obligatorio.
			}
		}
		catch
		{
			return 21; //No se encontró el valor del detalle en el xml. El detalle es obligatorio.
		}
		try
		{
			node = xmlDoc.SelectSingleNode("//Iventure/Importe");
			importe = Convert.ToDouble(node.InnerXml);
		}
		catch
		{
			if (String.IsNullOrEmpty(node.InnerXml))
				return 22; //No se encontró el valor del importe en el xml. El importe es obligatorio.
			else
				return 23; //Valor del importe inválido en el xml.
		}
		try
		{
			node = xmlDoc.SelectSingleNode("//Iventure/CantidadCuotas");
			cntCuotas = Convert.ToInt32(node.InnerXml);
		}
		catch
		{
			if (String.IsNullOrEmpty(node.InnerXml))
				return 24; //No se encontró el valor de la cantidad de cuotas en el xml. La cantidad de cuotas es obligatoria.
			else
				return 25; //Valor de la cantidad de cuotas inválido en el xml.
		}
		return 1; //Se obtuvieron los datos correctamente 


	}
	private int CrearCliente(XmlDocument xmlDoc, out Cliente cliente)
	{
		XmlNode node;
		cliente = new Cliente();
		node = xmlDoc.SelectSingleNode("//Iventure/Cliente/Nombre");
		cliente.Nombre = node.InnerXml;
		node = xmlDoc.SelectSingleNode("//Iventure/Cliente/Apellido");
		cliente.Apellido = node.InnerXml;
		node = xmlDoc.SelectSingleNode("//Iventure/Cliente/CUITDNI");
		cliente.CuitDni = node.InnerXml;
		node = xmlDoc.SelectSingleNode("//Iventure/Cliente/Domicilio");
		cliente.Domicilio = node.InnerXml;
		node = xmlDoc.SelectSingleNode("//Iventure/Cliente/Email");
		cliente.Email = node.InnerXml;
		node = xmlDoc.SelectSingleNode("//Iventure/Cliente/Telefonos");
		cliente.Telefonos = node.InnerXml;
		if (String.IsNullOrEmpty(cliente.CuitDni))
		{
			return 31;//No se encontró el valor del CUITDNI en el xml. El CUITDNI es obligatorio.
		}
		if (String.IsNullOrEmpty(cliente.Email))
		{
			return 32;//No se encontró el valor del email en el xml. El email es obligatorio.
		}
		return 1;
	}
	private bool IsAuthorized(string userName, string password, out Usuario usuario)
	{
		usuario = FacadeDao.GetUsuario(userName, password);
		if (usuario != null && usuario.IsProveedor && (usuario.IsAdminProveedor || usuario.IsVendedor))
			return true;
		else
			return false;
	}
	private int CrearIventure(string detalle, double importe, int cntCuotas, Cliente cliente, Usuario usuario)
	{
		Core.Iventure ivent = new Core.Iventure();
		ivent.Proveedor = new Proveedor();
		ivent.Proveedor.Id = usuario.IdProveedor.Value;
		ivent.IdVendedor = usuario.Id.Value;
		ivent.ImporteTotal = importe;
		ivent.CntCuotas = cntCuotas;
		ivent.DetalleAereo = detalle;
		ivent.Cliente = cliente;
		ivent.EsDonacion = false;

		string pass = cliente.Nombre.ToLower() + DateTime.Now.GetHashCode().ToString().Replace("-", "").Trim();
		Usuario usu = new Usuario();
		usu.Nombre = cliente.CuitDni;
		usu.Password = pass;
		usu.Email = cliente.Email;
		int result = FacadeDao.CrearIventure(ref ivent, usu, null); //cambiar el null por la imagen de empresa
		return result;
		//if (result == 1)
		//{
		//    Response.Redirect("MisIventures.aspx");
		//}
		//else if (result == -1 || result == -4)
		//{
		//    throw (new Exception("Email de cliente existente"));
		//}
		//else if (result == -2 || result == -3)
		//{
		//    throw (new Exception("Cuit de cliente ya registrado"));
		//}
		//else
		//{
		//    throw (new Exception("Ocurrió un error en el alta de la solicitud"));
		//}
	}

}

