using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data.Common;
using System.Web.SessionState;
using System.Globalization;
using System.Xml;



namespace Core
{
	public class FacadeDao
	{
		private static  Database db = DatabaseFactory.CreateDatabase();
		private static Database dbMFD = DatabaseFactory.CreateDatabase("MFDConnString");
		

		#region Common
		private static SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
		public static void EjecutarSQL(string sql)
		{
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.ExecuteNonQuery(dbCommand);
		}
		public static object DevolverUnValor(string sql)
		{
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			return db.ExecuteScalar(dbCommand);
		}
		public static DataTable DevolverDataTable(string sql)
		{
			DataTable dt = new DataTable();
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			DataSet ds = db.ExecuteDataSet(dbCommand);
			if (ds.Tables.Count > 0)
				dt = ds.Tables[0];
			return dt;
		}
		public static DataTable DevolverDataTableMFD(string sql)
		{
			DataTable dt = new DataTable();
			DbCommand dbCommand = dbMFD.GetSqlStringCommand(sql);
			DataSet ds = db.ExecuteDataSet(dbCommand);
			if (ds.Tables.Count > 0)
				dt = ds.Tables[0];
			return dt;
		}
		public static bool EnviarMail(string destination, string subject, string body, object sessionLogo, bool ingresoSitio)
		{
			SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["SMTP"], Convert.ToInt32(ConfigurationManager.AppSettings["PortSMTP"]));
			client.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["usuarioSMTP"], ConfigurationManager.AppSettings["passwordSMTP"]);



			MailAddress from = new MailAddress(ConfigurationManager.AppSettings["mailSender"], "Travel Pay", System.Text.Encoding.UTF8);
			MailAddress to = new MailAddress(destination);
			MailMessage message = new MailMessage(from, to);
			message.BodyEncoding = System.Text.Encoding.UTF8;
			message.Subject = subject;
			message.SubjectEncoding = System.Text.Encoding.UTF8;
			message.IsBodyHtml = true;
			string logo = "";
			if (sessionLogo != null)
			{
				if (((string)(sessionLogo)).StartsWith("http") || ((string)(sessionLogo)).StartsWith("www"))
				{
					logo = (string)sessionLogo;
				}
				else
				{
					logo = "http://" + ConfigurationManager.AppSettings["urlTravelPay"] +"/"+ (string)sessionLogo;
				}
			}
			else{
				logo = "http://" + ConfigurationManager.AppSettings["urlTravelPay"] + "/images/travelpay.png";
			}
			string encabezado = "<table align=Center style='border:solid 1px black;width:675px'><tr><td align=center><img src='"+logo+"'></td></tr><tr><td align=left>";
			StringBuilder legal = new StringBuilder();
			if(ingresoSitio)
				legal.Append("<p align=center><a href='http://" + ConfigurationManager.AppSettings["urlTravelPay"] + "/' target='_new'>Ingresar al Sitio</a></p> ");
			legal.Append("<br><font size=1>La información contenida en este correo es confidencial y para uso exclusivo "); 
			legal.Append("de los destinatarios del mismo. Esta prohibido a las personas o entidades "); 
			legal.Append("que no sean los destinatarios de este correo, realizar cualquier tipo de "); 
			legal.Append("modificación, copia o distribución del mismo. Toda la información contenida "); 
			legal.Append("en este email está sujeta a los términos y condiciones generales que se "); 
			legal.Append("establecen en la Sección Legales. Si Usted recibe este correo por error, ");
			legal.Append("tenga a bien notificar al emisor y eliminarlo.</font> "); 
			string footer = "</td></tr><tr><td><br><br><br></td></tr><tr><td align=center>" + legal + "</td></tr></table>";

			message.Body = encabezado + body + footer;
			client.Send(message);
			return true;
		}
		#endregion Common

		#region Usuario
		public static Usuario GetUsuario(string usuario, string password)
		{
			Usuario usu = null;
			string passEncrypt = "";// Usuario.Encriptar(password);
			passEncrypt = (string)DevolverUnValor("SELECT Password FROM Usuario WHERE Nombre='" + usuario + "'");
			string passDesencrypt = Usuario.Desencriptar(passEncrypt);
			if (password == passDesencrypt)
			{
				DataTable dt = DevolverDataTable("SELECT IdUsuario, Nombre, Email, Rol, IdProveedor, IdCliente FROM Usuario WHERE Nombre='" + usuario + "'");
				if (dt.Rows.Count > 0)
				{
					usu = new Usuario();
					usu.Id = (int)dt.Rows[0]["IdUsuario"];
					usu.Nombre = (string)dt.Rows[0]["Nombre"];
					usu.Email = (string)dt.Rows[0]["Email"];
					usu.Rol = (int)dt.Rows[0]["Rol"];
					usu.IdProveedor = (dt.Rows[0]["IdProveedor"] == DBNull.Value) ? null : (int?)dt.Rows[0]["IdProveedor"];
					usu.IdCliente = (dt.Rows[0]["IdCliente"] == DBNull.Value) ? null : (int?)dt.Rows[0]["IdCliente"];
				}
			}
			return usu;
		}
		public static Usuario GetUsuario(int idUsuario)
		{
			Usuario usu = null;
			DataTable dt = DevolverDataTable("SELECT IdUsuario, Nombre, Email, Rol FROM Usuario WHERE Id ='" + idUsuario + "'");
			if (dt.Rows.Count > 0)
			{
				usu = new Usuario();
				usu.Id = (int)dt.Rows[0]["IdUsuario"];
				usu.Nombre = (string)dt.Rows[0]["Nombre"];
				usu.Email = (string)dt.Rows[0]["Email"];
				usu.Rol = (int)dt.Rows[0]["Rol"];
			}
			return usu;
		}
		public static List<Usuario> GetUsuarios(int? idProveedor)
		{
			List<Usuario> list = new List<Usuario>();
			Usuario usu = null;
			StringBuilder sql = new StringBuilder();
			sql.Append("SELECT [IdUsuario],[Nombre],[Email],"+ Environment.NewLine);
			sql.Append("	CASE Rol"+ Environment.NewLine);
			sql.Append("		WHEN 1 THEN 'Super Admin'"+ Environment.NewLine);
			sql.Append("		WHEN 2 THEN 'Administrador'"+ Environment.NewLine);
			sql.Append("		WHEN 3 THEN 'Vendedor'"+ Environment.NewLine);
			sql.Append("		WHEN 4 THEN 'Cliente'"+ Environment.NewLine);
			sql.Append("		ELSE 'Rol inexistente' "+ Environment.NewLine);
			sql.Append("	END RolName, Rol, Count(idIventure) CntIventures   " + Environment.NewLine);
			sql.Append("FROM Usuario u left outer join iventure i" + Environment.NewLine);
			sql.Append("on i.Vendedor = u.IdUsuario" + Environment.NewLine);
			if (idProveedor.HasValue)
				sql.Append("WHERE u.idProveedor = "+idProveedor.Value + Environment.NewLine);
			sql.Append("GROUP BY u.[IdUsuario],[Nombre],[Email],[Rol]" + Environment.NewLine);
			sql.Append("ORDER BY [Nombre]" + Environment.NewLine);
			DataTable dt = DevolverDataTable(sql.ToString());
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					usu = new Usuario();
					usu.Id = (int)dr["IdUsuario"];
					usu.Nombre = (string)dr["Nombre"];
					usu.Email = Convert.ToString(dr["Email"]);
					usu.Rol = (int)dr["Rol"];
					usu.RolName = Convert.ToString(dr["RolName"]);
					usu.CantidadIventures = (int)dr["CntIventures"];
					list.Add(usu);
				}
			}
			return list;
		}
		public static List<Usuario> GetVendedores(int idProveedor)
		{
			List<Usuario> list = new List<Usuario>();
			Usuario usu = null;
			DbCommand dbCommand = db.GetStoredProcCommand("spGetVendedores", idProveedor);
			DataTable dt = new DataTable();
			DataSet ds = db.ExecuteDataSet(dbCommand);
			if (ds.Tables.Count > 0)
				dt = ds.Tables[0];

			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					usu = new Usuario();
					usu.Id = (int)dr["IdUsuario"];
					usu.Nombre = (string)dr["Nombre"];
					list.Add(usu);
				}
			}
			return list;
		}
		
		public static int CrearUsuario(Usuario usuario)
		{
			int result=0;
			DataTable dt = new DataTable();
			DbCommand dbCommand = db.GetStoredProcCommand("spInsertUsuario", usuario.Nombre, Usuario.Encriptar(usuario.Password), usuario.Email, usuario.Rol, usuario.IdProveedor, usuario.IdCliente, result);
			try
			{
				db.ExecuteScalar(dbCommand);
				result = (int)dbCommand.Parameters["@Resultado"].Value;

			}
			catch (System.Data.SqlClient.SqlException ex)
			{
				result = -100;
			}
			return result;
		}
		public static void BorrarUsuario(int idUsuario) {
			EjecutarSQL("DELETE FROM [dbo].[Usuario] WHERE idUsuario = " + idUsuario.ToString());
		}
		public static Usuario RecuperarPassword(string nombreUsuario, string email) {
			Usuario usu = null;
			if (!string.IsNullOrEmpty(nombreUsuario) || !string.IsNullOrEmpty(email))
			{
				StringBuilder sql = new StringBuilder("SELECT Nombre, Password, Email FROM Usuario WHERE" + Environment.NewLine);
				if (!string.IsNullOrEmpty(nombreUsuario))
					sql.Append(" Nombre = '" + nombreUsuario + "' AND ");
				if (!string.IsNullOrEmpty(email))
					sql.Append(" Email = '" + email + "' AND ");
				DataTable dt = FacadeDao.DevolverDataTable(sql.Remove(sql.Length - 4, 4).ToString());
				if (dt.Rows.Count == 1) {
					usu = new Usuario();
					usu.Nombre = dt.Rows[0]["Nombre"].ToString();
					usu.Password = Usuario.Desencriptar(dt.Rows[0]["Password"].ToString());
					usu.Email = dt.Rows[0]["Email"].ToString();
				}
			}
			return usu;
		}
		#endregion Usuario

		#region Proveedor
		public static Proveedor GetProveedor(int idProveedor)
		{
			Proveedor prov = null;
			DataTable dt = DevolverDataTable("SELECT [IdProveedor],[Gateway],[IdGateway],[Email],[CUIT],[IIBB],[RazonSocial],[FechaAlta],[NombreResp],[ApellidoResp],[DNIResp],[CargoResp],[UrlImagen],[Telefonos],[Tarjetas], [CntTransacciones], [MontoTransacciones], [ClaveEncNPS], [DevNPS], [DiasVencimiento] FROM [Proveedor] WHERE [IdProveedor]=" + idProveedor.ToString());
			if (dt.Rows.Count > 0)
			{
				prov = new Proveedor();
				prov.Id = (int)dt.Rows[0]["IdProveedor"];
				prov.Gateway = Convert.ToString(dt.Rows[0]["Gateway"]);
				prov.IdGateway = Convert.ToString(dt.Rows[0]["IdGateway"]);
				prov.Email = Convert.ToString(dt.Rows[0]["Email"]);
				prov.CUIT = (string)dt.Rows[0]["CUIT"];
				prov.IIBB = Convert.ToString(dt.Rows[0]["IIBB"]);
				prov.RazonSocial = (string)dt.Rows[0]["RazonSocial"];
				prov.Alta = (DateTime)dt.Rows[0]["FechaAlta"];
				prov.NombreResp = Convert.ToString(dt.Rows[0]["NombreResp"]);
				prov.ApellidoResp = (string)dt.Rows[0]["ApellidoResp"];
				prov.DNIResp = (string)dt.Rows[0]["DNIResp"];
				prov.CargoResp = Convert.ToString(dt.Rows[0]["CargoResp"]);
				prov.UrlImagen = Convert.ToString(dt.Rows[0]["UrlImagen"]);
				prov.Telefonos = Convert.ToString(dt.Rows[0]["Telefonos"]);
				prov.Tarjetas = Convert.ToString(dt.Rows[0]["Tarjetas"]);
				prov.CntTransacciones = ((dt.Rows[0]["CntTransacciones"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["CntTransacciones"]));
				prov.ClaveEncNPS =  Convert.ToString(dt.Rows[0]["ClaveEncNPS"]);
				prov.DevNPS = ((dt.Rows[0]["DevNPS"] == DBNull.Value) ? true : Convert.ToBoolean(dt.Rows[0]["DevNPS"]));
				prov.MontoTransacciones = ((dt.Rows[0]["MontoTransacciones"] == DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["MontoTransacciones"]));
				prov.DiasVencimiento = ((dt.Rows[0]["DiasVencimiento"] == DBNull.Value) ? Convert.ToInt32(ConfigurationManager.AppSettings["DiasVencimiento"]) : Convert.ToInt32(dt.Rows[0]["DiasVencimiento"]));
				
			}
			return prov;
		}
		public static Proveedor GetProveedor(string idGateway)
		{
			Proveedor prov = null;
			DataTable dt = DevolverDataTable("SELECT [IdProveedor],[Gateway],[IdGateway],[Email],[CUIT],[IIBB],[RazonSocial],[FechaAlta],[NombreResp],[ApellidoResp],[DNIResp],[CargoResp],[UrlImagen],[Telefonos],[Tarjetas], [CntTransacciones], [MontoTransacciones], [ClaveEncNPS], [DevNPS], [DiasVencimiento] FROM [Proveedor] WHERE [IdGateway]='" + idGateway + "'");
			if (dt.Rows.Count > 0)
			{
				prov = new Proveedor();
				prov.Id = (int)dt.Rows[0]["IdProveedor"];
				prov.Gateway = Convert.ToString(dt.Rows[0]["Gateway"]);
				prov.IdGateway = Convert.ToString(dt.Rows[0]["IdGateway"]);
				prov.Email = Convert.ToString(dt.Rows[0]["Email"]);
				prov.CUIT = (string)dt.Rows[0]["CUIT"];
				prov.IIBB = Convert.ToString(dt.Rows[0]["IIBB"]);
				prov.RazonSocial = (string)dt.Rows[0]["RazonSocial"];
				prov.Alta = (DateTime)dt.Rows[0]["FechaAlta"];
				prov.NombreResp = Convert.ToString(dt.Rows[0]["NombreResp"]);
				prov.ApellidoResp = (string)dt.Rows[0]["ApellidoResp"];
				prov.DNIResp = (string)dt.Rows[0]["DNIResp"];
				prov.CargoResp = Convert.ToString(dt.Rows[0]["CargoResp"]);
				prov.UrlImagen = Convert.ToString(dt.Rows[0]["UrlImagen"]);
				prov.Telefonos = Convert.ToString(dt.Rows[0]["Telefonos"]);
				prov.Tarjetas = Convert.ToString(dt.Rows[0]["Tarjetas"]);
				prov.CntTransacciones = ((dt.Rows[0]["CntTransacciones"] == DBNull.Value) ? 0 : Convert.ToInt32(dt.Rows[0]["CntTransacciones"]));
				prov.ClaveEncNPS = Convert.ToString(dt.Rows[0]["ClaveEncNPS"]);
				prov.DevNPS = ((dt.Rows[0]["DevNPS"] == DBNull.Value) ? true : Convert.ToBoolean(dt.Rows[0]["DevNPS"]));
				prov.MontoTransacciones = ((dt.Rows[0]["MontoTransacciones"] == DBNull.Value) ? 0 : Convert.ToDouble(dt.Rows[0]["MontoTransacciones"]));
				prov.DiasVencimiento = ((dt.Rows[0]["DiasVencimiento"] == DBNull.Value) ? Convert.ToInt32(ConfigurationManager.AppSettings["DiasVencimiento"]) : Convert.ToInt32(dt.Rows[0]["DiasVencimiento"]));

			}
			return prov;
		}
		public static List<Proveedor> GetProveedores()
		{
			List<Proveedor> list = new List<Proveedor>();
			Proveedor prov = null;
			DataTable dt = DevolverDataTable("SELECT [IdProveedor],[CUIT],[RazonSocial],[NombreResp],[ApellidoResp] FROM [dbo].[Proveedor] ORDER BY [ApellidoResp],[NombreResp]");
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					prov = new Proveedor();
					prov.Id = (int)dr["IdProveedor"];
					prov.CUIT = (string)dr["CUIT"];
					prov.RazonSocial = (string)dr["RazonSocial"];
					prov.NombreResp = (string)dr["NombreResp"];
					prov.ApellidoResp = (string)dr["ApellidoResp"];
					list.Add(prov);
				}
			}
			return list;
		}
		public static int CrearProveedor(Proveedor proveedor)
		{
			int result;
			DbCommand dbCommand = db.GetStoredProcCommand("spInsertProveedor", proveedor.Gateway, proveedor.IdGateway, proveedor.Email, proveedor.CUIT, proveedor.IIBB, proveedor.RazonSocial, proveedor.Alta, proveedor.NombreResp, proveedor.ApellidoResp, proveedor.DNIResp, proveedor.CargoResp, proveedor.UrlImagen, proveedor.Telefonos, proveedor.Tarjetas, proveedor.ClaveEncNPS, proveedor.DevNPS, proveedor.DiasVencimiento);
			try
			{
				db.ExecuteNonQuery(dbCommand);
				result = 1;
			}
			catch (System.Data.SqlClient.SqlException ex)
			{
				result = -100;
			}
			return result;
		}
		public static int ActualizarProveedor(Proveedor proveedor) {
			int result;
			DbCommand dbCommand = db.GetStoredProcCommand("spUpdateProveedor", proveedor.Id, proveedor.Gateway, proveedor.IdGateway, proveedor.Email, proveedor.CUIT, proveedor.IIBB, proveedor.RazonSocial, proveedor.Alta, proveedor.NombreResp, proveedor.ApellidoResp, proveedor.DNIResp, proveedor.CargoResp, proveedor.UrlImagen, proveedor.Telefonos, proveedor.Tarjetas, proveedor.ClaveEncNPS, proveedor.DevNPS, proveedor.DiasVencimiento);
			try
			{
				db.ExecuteNonQuery(dbCommand);
				result = 1;
			}
			catch (System.Data.SqlClient.SqlException ex)
			{
				result = -100;
			}
			return result;
		}
		public static string GetLogo(int idProveedor)
		{
			object logo = DevolverUnValor("SELECT [UrlImagen]FROM [dbo].[Proveedor] WHERE [IdProveedor]=" + idProveedor.ToString());
			if (logo == DBNull.Value)
			{
				return Proveedor.logoDefault;
			}
			else {
				return (string)logo;
			}
		}
		
		#endregion Proveedor

		#region Cliente
		public static Cliente GetCliente(int idCliente)
		{
			Cliente cli = null;
			DataTable dt = DevolverDataTable("SELECT c.[IdCliente],p.[IdProveedor],p.[Gateway],p.Tarjetas, c.[CuitDni],c.[Nombre],c.[Apellido],c.[Domicilio],c.[Email],c.[Telefonos] FROM [Cliente]c, [Proveedor] p  WHERE p.IdProveedor=c.IdProveedor AND [IdCliente]=" + idCliente.ToString());
			if (dt.Rows.Count > 0)
			{
				cli = new Cliente();
				cli.Id = (int)dt.Rows[0]["IdCliente"];
				cli.Proveedor = new Proveedor();
				cli.Proveedor.Id = (int)dt.Rows[0]["IdProveedor"];
				cli.Proveedor.Gateway = Convert.ToString(dt.Rows[0]["Gateway"]);
				cli.Proveedor.Tarjetas = Convert.ToString(dt.Rows[0]["Tarjetas"]);
				cli.CuitDni = (string)dt.Rows[0]["CuitDni"];
				cli.Nombre = (string)dt.Rows[0]["Nombre"];
				cli.Apellido = (string)dt.Rows[0]["Apellido"];
				cli.Domicilio = Convert.ToString(dt.Rows[0]["Domicilio"]);
				cli.Email = Convert.ToString(dt.Rows[0]["Email"]);
				cli.Telefonos = Convert.ToString(dt.Rows[0]["Telefonos"]);
			}
			return cli;
		}
		public static List<Cliente> GetClientes(int idProveedor)
		{
			List<Cliente> list = new List<Cliente>();
			Cliente cli = null;
			//DataTable dt = DevolverDataTable("SELECT [IdCliente],[IdProveedor],[CuitDni],[Nombre],[Apellido],[Domicilio],[Email],[Telefonos] FROM [dbo].[Cliente] WHERE IdProveedor = " + idProveedor + " ORDER BY [Apellido],[Nombre]");
			DbCommand dbCommand = db.GetStoredProcCommand("spGetClientes", idProveedor);
			DataTable dt = new DataTable();
			DataSet ds = db.ExecuteDataSet(dbCommand);
			if (ds.Tables.Count > 0)
				dt = ds.Tables[0];
			
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					cli = new Cliente();
					cli.Id = (int)dr["IdCliente"];
					cli.Proveedor = new Proveedor();
					cli.Proveedor.Id = (int)dr["IdProveedor"];
					cli.CuitDni = (string)dr["CuitDni"];
					cli.Nombre = (string)dr["Nombre"];
					cli.Apellido = (string)dr["Apellido"];
					cli.Domicilio = Convert.ToString(dr["Domicilio"]);
					cli.Email = Convert.ToString(dr["Email"]);
					cli.Telefonos = Convert.ToString(dr["Telefonos"]);
					cli.SoloLectura = (dr["SoloLectura"].ToString()=="1");
					list.Add(cli);
				}
			}
			return list;
		}
		public static List<Cliente> GetClientes(int idProveedor, string cuit, string email)
		{
			List<Cliente> list = new List<Cliente>();
			Cliente cli = null;
			StringBuilder query = new StringBuilder();
			query.Append("SELECT [IdCliente],[IdProveedor],[CuitDni],[Nombre],[Apellido],[Domicilio],[Email],[Telefonos] FROM [Cliente] WHERE IdProveedor = " + idProveedor );
			if (!string.IsNullOrEmpty(cuit))
				query.Append(" AND [CuitDni] like ('%" + cuit +"%')");
			if (!string.IsNullOrEmpty(email)) 
				query.Append(" AND [Email] like ('%" + email + "%')");
			query.Append(" ORDER BY [Apellido],[Nombre]");
			DataTable dt = DevolverDataTable(query.ToString());
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					cli = new Cliente();
					cli.Id = (int)dr["IdCliente"];
					cli.Proveedor = new Proveedor();
					cli.Proveedor.Id = (int)dr["IdProveedor"];
					cli.CuitDni = (string)dr["CuitDni"];
					cli.Nombre = (string)dr["Nombre"];
					cli.Apellido = (string)dr["Apellido"];
					cli.Domicilio = Convert.ToString(dr["Domicilio"]);
					cli.Email = Convert.ToString(dr["Email"]);
					cli.Telefonos = Convert.ToString(dr["Telefonos"]);
					list.Add(cli);
				}
			}
			return list;
		}
		public static int CrearCliente(Cliente cliente, Usuario usuario)
		{
			int result=0;
			DbCommand dbCommand = db.GetStoredProcCommand("spInsertCliente", cliente.Proveedor.Id, cliente.CuitDni, cliente.Nombre, cliente.Apellido, cliente.Domicilio, cliente.Email, cliente.Telefonos, Usuario.Encriptar(usuario.Password), result);

			try
			{
				db.ExecuteNonQuery(dbCommand);
				result = (int)dbCommand.Parameters["@Resultado"].Value;
			}
			catch (System.Data.SqlClient.SqlException ex)
			{
				result = -100;
			}

			return result;
		}
		public static int ActualizarCliente(Cliente cliente)
		{
			int result = 0;
			DbCommand dbCommand = db.GetStoredProcCommand("spUpdateCliente", cliente.Id, cliente.CuitDni, cliente.Nombre, cliente.Apellido, cliente.Domicilio, cliente.Email, cliente.Telefonos, result);

			try
			{
				db.ExecuteNonQuery(dbCommand);
				result = (int)dbCommand.Parameters["@Resultado"].Value;
			}
			catch (System.Data.SqlClient.SqlException ex)
			{
				result = -100;
			}

			return result;
		}
		public static void BorrarCliente(int idCliente)
		{
			//EjecutarSQL("DELETE FROM Cliente WHERE idCliente = " + idCliente.ToString());
			DbCommand dbCommand = db.GetStoredProcCommand("spDeleteCliente", idCliente);
			db.ExecuteNonQuery(dbCommand);
		}
		#endregion Cliente

		#region Iventure
		#region Iventure Largo
		//public static List<Iventure> GetIventure(int? idCliente, int? idProveedor)
		//{
		//    List<Iventure> list = new List<Iventure>();
		//    Iventure iventure = null;
		//    StringBuilder sql = new StringBuilder();
		//    sql.Append(" SELECT [IdIventure], p.[IdProveedor], p.[RazonSocial], [ImporteTotal], [Vencimiento], [Estado]");
		//    sql.Append(" FROM dbo.Iventure i, dbo.Proveedor p");
		//    sql.Append(" WHERE i.IdProveedor = p.IdProveedor ");
		//    if (idCliente.HasValue)
		//        sql.Append(" AND IdCliente=" + idCliente);
		//    if (idProveedor.HasValue)
		//        sql.Append(" AND p.IdProveedor = " + idProveedor.Value);

		//    DataTable dt = Core.FacadeDao.DevolverDataTable(sql.ToString());
		//    if (dt.Rows.Count > 0)
		//    {
		//        foreach (DataRow dr in dt.Rows)
		//        {
		//            iventure = new Iventure();
		//            iventure.Id = (int)dr["IdIventure"];
		//            iventure.Proveedor = new Proveedor();
		//            iventure.Proveedor.RazonSocial = (string)dr["RazonSocial"];
		//            iventure.ImporteTotal = Convert.ToDouble(dr["ImporteTotal"]);
		//            iventure.Vencimiento = (dr["Vencimiento"] == DBNull.Value) ? null : (DateTime?)dr["Vencimiento"];
		//            iventure.Estado = (string)dr["Estado"];
		//            list.Add(iventure);
		//        }
		//    }
		//    return list;
		//}
		//public static Iventure GetIventure(int idIventure)
		//{
		//    Iventure iventure = null;
		//    StringBuilder sql = new StringBuilder();
		//    sql.Append(" SELECT	[IdIventure],p.[IdProveedor], p.RazonSocial, p.Email EmailProv,c.[IdCliente], c.CUITDNI, c.Nombre, c.Apellido, c.Domicilio, c.Email, c.Telefonos," + Environment.NewLine);
		//    sql.Append(" 		[TipoAereo],[DetalleAereo],[ImporteAereo],[CiudadEstadia],[PasajerosEstadia],[ImporteEstadia]," + Environment.NewLine);
		//    sql.Append(" 		[DetallesAutomovil],[ImporteAutomovil],[ImporteTotal],[Vencimiento], [Estado]" + Environment.NewLine);
		//    sql.Append(" FROM	[dbo].[Iventure] i, dbo.Cliente c, dbo.Proveedor p" + Environment.NewLine);
		//    sql.Append(" WHERE i.IdCliente = c.IdCliente AND i.IdProveedor = p.IdProveedor");
		//    sql.Append(" AND IdIventure=" + idIventure);

		//    DataTable dt = Core.FacadeDao.DevolverDataTable(sql.ToString());
		//    if (dt.Rows.Count > 0)
		//    {
		//        DataRow dr = dt.Rows[0];
		//        iventure = new Iventure();
		//        iventure.Id = (int)dr["IdIventure"];
		//        iventure.Vencimiento = (dr["Vencimiento"] == DBNull.Value) ? null : (DateTime?)dr["Vencimiento"];
		//        iventure.DetalleAereo = Convert.ToString(dr["DetalleAereo"]);
		//        iventure.TipoAereo = Convert.ToString(dr["TipoAereo"]); ;
		//        iventure.ImporteAereo = Convert.ToDouble(dr["ImporteAereo"]);
		//        iventure.CiudadEstadia = Convert.ToString(dr["CiudadEstadia"]); ;
		//        iventure.PasajerosEstadia = Convert.ToInt32(dr["PasajerosEstadia"]);
		//        iventure.ImporteEstadia = Convert.ToDouble(dr["ImporteEstadia"]);
		//        iventure.DetalleAutomovil = Convert.ToString(dr["DetallesAutomovil"]); ;
		//        iventure.ImporteAutomovil = Convert.ToDouble(dr["ImporteAutomovil"]);
		//        iventure.ImporteTotal = Convert.ToDouble(dr["ImporteTotal"]);
		//        iventure.Estado = (string)dr["Estado"];
		//        Proveedor proveedor = new Proveedor();
		//        iventure.Proveedor = proveedor;
		//        proveedor.Id = (int)dr["IdProveedor"];
		//        proveedor.RazonSocial = Convert.ToString(dr["RazonSocial"]);
		//        proveedor.Email = Convert.ToString(dr["EmailProv"]);
		//        Cliente cliente = new Cliente();
		//        iventure.Cliente = cliente;
		//        cliente.Nombre = Convert.ToString(dr["Nombre"]);
		//        cliente.Apellido = Convert.ToString(dr["Apellido"]);
		//        cliente.CuitDni = Convert.ToString(dr["CuitDni"]);
		//        cliente.Domicilio = Convert.ToString(dr["Domicilio"]);
		//        cliente.Email = Convert.ToString(dr["Email"]);
		//        cliente.Telefonos = Convert.ToString(dr["Telefonos"]);
		//    }
		//    return iventure;
		//}
		//public static int CrearIventure(Iventure iventure, Usuario usuario)
		//{
		//    int result = 0, seCreoClient = 0;
		//    //System.Data.SqlClient.SqlCommand com = new System.Data.SqlClient.SqlCommand();
		//    DbCommand dbCommand = db.GetStoredProcCommand("spInsertIventure", iventure.Proveedor.Id, iventure.TipoAereo, iventure.DetalleAereo,
		//    iventure.ImporteAereo, iventure.CiudadEstadia, iventure.PasajerosEstadia, iventure.ImporteEstadia, iventure.DetalleAutomovil, iventure.ImporteAutomovil,
		//    iventure.ImporteTotal, iventure.Vencimiento, iventure.Cliente.CuitDni, iventure.Cliente.Nombre, iventure.Cliente.Apellido, iventure.Cliente.Domicilio,
		//    iventure.Cliente.Email, iventure.Cliente.Telefonos, Usuario.Encriptar(usuario.Password), result, seCreoClient);

		//    try
		//    {
		//        db.ExecuteNonQuery(dbCommand);
		//        result = (int)dbCommand.Parameters["@Resultado"].Value;//Convert.ToInt32(dr[0]);
		//        if (((int)dbCommand.Parameters["@SeCreoCliente"].Value) == 1)
		//        {
		//            FacadeDao.EnviarMail(iventure.Cliente.Email, "Alta de usuario en TravelPay", "Datos de ingreso a TravelPay web:<br><br>Usuario: " + usuario.Nombre + "<br>Password: " + usuario.Password);
		//        }
		//        if (result == 1)
		//        {
		//            FacadeDao.EnviarMail(iventure.Cliente.Email, "Tiene una nueva solicitud en Travel Pay.", "Usted ha recibido una nueva solicitud con fecha de vencimiento " + iventure.Vencimiento.Value.ToShortDateString());

		//        }
		//    }
		//    catch (System.Data.SqlClient.SqlException ex)
		//    {
		//        result = -100;
		//    }

		//    return result;
		//}
		//public static int ActualizarIventure(Iventure iventure)
		//{
		//    int result;
		//    DbCommand dbCommand = db.GetStoredProcCommand("spUpdatetIventure", iventure.Id, iventure.TipoAereo, iventure.DetalleAereo,
		//    iventure.ImporteAereo, iventure.CiudadEstadia, iventure.PasajerosEstadia, iventure.ImporteEstadia, iventure.DetalleAutomovil, iventure.ImporteAutomovil,
		//    iventure.ImporteTotal, iventure.Vencimiento);

		//    try
		//    {
		//        db.ExecuteNonQuery(dbCommand);
		//        result = 1;
		//    }
		//    catch (System.Data.SqlClient.SqlException ex)
		//    {
		//        result = -100;
		//    }

		//    return result;
		//}

		#endregion Iventure Largo
		public static List<Iventure> GetIventure(int? idCliente, int? idProveedor, int? idVendedor)
		{ 
			List<Iventure> list = new List<Iventure>();
			Iventure iventure = null;
			StringBuilder sql = new StringBuilder();
			sql.Append(" SELECT [IdIventure], p.[IdProveedor], p.[RazonSocial], c.Apellido, c.Nombre,[ImporteTotal], [Vencimiento], [Estado], [FechaPago]");
			sql.Append(" FROM dbo.Iventure i, dbo.Proveedor p, Cliente c");
			sql.Append(" WHERE i.IdProveedor = p.IdProveedor ");
			sql.Append(" AND i.IdCliente = c.IdCliente");
			if (idCliente.HasValue) 
				sql.Append(" AND c.IdCliente=" + idCliente);
			if (idProveedor.HasValue)
				sql.Append(" AND p.IdProveedor = " + idProveedor.Value);
			if (idVendedor.HasValue)
				sql.Append(" AND i.Vendedor = " + idVendedor.Value);
			sql.Append(" ORDER BY IdIventure desc ");

			DataTable dt = Core.FacadeDao.DevolverDataTable(sql.ToString());
			if (dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
					iventure = new Iventure();
					iventure.Id = (int)dr["IdIventure"];
					iventure.Proveedor = new Proveedor();
					iventure.Proveedor.RazonSocial = (string)dr["RazonSocial"];
					iventure.Cliente = new Cliente();
					iventure.Cliente.Apellido = (string)dr["Apellido"];
					iventure.Cliente.Nombre = (string)dr["Nombre"];
					iventure.ImporteTotal = Convert.ToDouble(dr["ImporteTotal"]);
					iventure.Vencimiento = (dr["Vencimiento"] == DBNull.Value) ? null : (DateTime?)dr["Vencimiento"];
					iventure.Estado = (string)dr["Estado"];
					iventure.FechaPagado = (dr["FechaPago"] == DBNull.Value) ? null : (DateTime?)dr["FechaPago"];
					list.Add(iventure);
				}
			}
			return list;
		}
		public static Iventure GetIventure(int idIventure)
		{
			Iventure iventure = null;
			StringBuilder sql = new StringBuilder();
			sql.Append(" SELECT	[IdIventure], p.[IdProveedor], p.[IdGateway], p.[Gateway], p.RazonSocial, p.Email EmailProv, p.UrlImagen,p.Tarjetas, p.ClaveEncNPS, p.DevNPS,i.Vendedor IdVendedor, u.Nombre Vendedor, c.[IdCliente], c.CUITDNI, c.Nombre, c.Apellido, c.Domicilio, c.Email, c.Telefonos," + Environment.NewLine);
			sql.Append(" 		[DetalleAereo],[ImporteTotal],[CntCuotas], [Vencimiento], [Estado]" + Environment.NewLine);
			sql.Append(" FROM	[dbo].[Iventure] i, dbo.Cliente c, dbo.Proveedor p, dbo.Usuario u" + Environment.NewLine);
			sql.Append(" WHERE i.IdCliente = c.IdCliente AND i.IdProveedor = p.IdProveedor AND u.IdUsuario = i.Vendedor");
			sql.Append(" AND IdIventure=" + idIventure);

			DataTable dt = Core.FacadeDao.DevolverDataTable(sql.ToString());
			if (dt.Rows.Count > 0)
			{
				DataRow dr = dt.Rows[0];
				iventure = new Iventure();
				iventure.Id = (int)dr["IdIventure"];
				iventure.DetalleAereo = Convert.ToString(dr["DetalleAereo"]);
				iventure.ImporteTotal = Convert.ToDouble(dr["ImporteTotal"]);
				iventure.CntCuotas = Convert.ToInt32(dr["CntCuotas"]);
				iventure.Vencimiento = (dt.Rows[0]["Vencimiento"] == DBNull.Value) ? null :(DateTime?)Convert.ToDateTime(dr["Vencimiento"]);
				iventure.Estado = (string)dr["Estado"];
				iventure.IdVendedor = Convert.ToInt32(dr["IdVendedor"]); 
				iventure.Vendedor = (string)dr["Vendedor"];
				Proveedor proveedor = new Proveedor();
				iventure.Proveedor = proveedor;
				proveedor.Id = (int)dr["IdProveedor"];
				proveedor.RazonSocial = Convert.ToString(dr["RazonSocial"]);
				proveedor.IdGateway = Convert.ToString(dr["IdGateway"]);
				proveedor.Gateway = Convert.ToString(dr["Gateway"]);
				proveedor.Email = Convert.ToString(dr["EmailProv"]);
				proveedor.UrlImagen = Convert.ToString(dr["UrlImagen"]);
				proveedor.Tarjetas = Convert.ToString(dt.Rows[0]["Tarjetas"]);
				proveedor.ClaveEncNPS = Convert.ToString(dt.Rows[0]["ClaveEncNPS"]);
				proveedor.DevNPS = ((dt.Rows[0]["DevNPS"] == DBNull.Value) ? true : Convert.ToBoolean(dt.Rows[0]["DevNPS"]));
				Cliente cliente = new Cliente();
				iventure.Cliente = cliente;
				cliente.Nombre = Convert.ToString(dr["Nombre"]);
				cliente.Apellido = Convert.ToString(dr["Apellido"]);
				cliente.CuitDni = Convert.ToString(dr["CuitDni"]);
				cliente.Domicilio = Convert.ToString(dr["Domicilio"]);
				cliente.Email = Convert.ToString(dr["Email"]);
				cliente.Telefonos = Convert.ToString(dr["Telefonos"]); 
			}
			return iventure;
		}
		public static int CrearIventure(ref Iventure iventure, Usuario usuario, object sessionLogo)
		{
			int result = 0, seCreoClient = 0, idIventure = 0;
			object idCli=DBNull.Value;
			if(iventure.Cliente.Id!=0) idCli = iventure.Cliente.Id;
			DbCommand dbCommand = db.GetStoredProcCommand("spInsertIventure", iventure.Proveedor.Id, iventure.IdVendedor, iventure.DetalleAereo,
			iventure.ImporteTotal, idCli, iventure.Cliente.CuitDni, iventure.Cliente.Nombre, iventure.Cliente.Apellido, iventure.Cliente.Domicilio, 
			iventure.Cliente.Email, iventure.Cliente.Telefonos, Usuario.Encriptar(usuario.Password), iventure.CntCuotas, iventure.Vencimiento, result, seCreoClient, idIventure, iventure.EsDonacion);

			try
			{
				db.ExecuteNonQuery(dbCommand);
				result = (int)dbCommand.Parameters["@Resultado"].Value;
				idIventure = (int)dbCommand.Parameters["@IdIventure"].Value;
				iventure.Id = idIventure;
				//Comentado porque ahora no se envia mas el email por creación usuario
				//if (((int)dbCommand.Parameters["@SeCreoCliente"].Value) == 1)
				//{
				//    FacadeDao.EnviarMail(iventure.Cliente.Email, "Alta de usuario en TravelPay", "Datos de ingreso a TravelPay web:<br><br>Usuario: " + usuario.Nombre + "<br>Password: " + usuario.Password,sessionLogo);
				//}
				if (result == 1 && !iventure.EsDonacion)
				{
					string queryString = "IdIventure=" + idIventure.ToString();
					queryString = Cryptography.Encriptar(queryString);
					FacadeDao.EnviarIventureEmail(idIventure, sessionLogo);
			

				}
			}
			catch (System.Data.SqlClient.SqlException ex)
			{
				result = -100;
			}

			return result;
		}
		public static void EnviarIventureEmail(int idIventure, object logo)
		{

			Core.Iventure iventure = FacadeDao.GetIventure(idIventure);
			string queryString = "IdIventure=" + iventure.Id.ToString();
			queryString = Cryptography.Encriptar(queryString);
			StringBuilder body = new StringBuilder();
			body.Append("<table align=\"center\" border=\"2\">");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: #C0C0C0; height: 20px; text-align: center; font-size: 20px;color: #800000; font-family: 'Berlin Sans FB';\" colspan=\"3\">Detalle de su solicitud</td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td colspan=\"3\">");
			body.Append("		<table align=\"center\" border=\"0\">");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white; \" >Nro. Solicitud</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 20px;\">" + iventure.Id.ToString() + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 20px;\"></td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white; \" >Empresa</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 20px;\">" + iventure.Proveedor.RazonSocial + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 20px;\"></td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white; \">Vendedor</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 20px;\">" + iventure.Vendedor + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 20px;\"></td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white; \">Detalle</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 21px;\">" + iventure.DetalleAereo + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 21px;\">&nbsp;</td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white; \">Importe</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 21px;\">" + iventure.ImporteTotal.ToString("F2") + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 21px;\">&nbsp;</td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white; \">Cnt. Cuotas  </td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 21px;\">" + iventure.CntCuotas.ToString() + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 21px;\">&nbsp;</td>");
			body.Append("	</tr>"); 


			body.Append("		</table>");
			body.Append("		</td>");
			body.Append("	</tr>");
			
			body.Append("	<tr>");
			body.Append("		<td colspan=\"3\" style=\"background-color: #669999; height: 21px; font-size: medium;color: white;\"><b>Informaci&oacute;n del cliente</b></td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white;\" >Apellido</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 21px;\">" + iventure.Cliente.Apellido + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 21px;\">&nbsp;</td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white;\" >Nombre</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 21px;\">" + iventure.Cliente.Nombre + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 21px;\">&nbsp;</td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white;\" >CUIT / CUIL DNI</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 21px;\">" + iventure.Cliente.CuitDni + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 21px;\">&nbsp;</td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white;\">Domicilio</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 21px;\">" + iventure.Cliente.Domicilio + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 21px;\">&nbsp;</td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white;\" >Tel&eacute;fonos</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 21px;\">" + iventure.Cliente.Telefonos + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 21px;\">&nbsp;</td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td style=\"background-color: white;\" >Email</td>");
			body.Append("		<td style=\"background-color: white; width: 306px; height: 21px;\">" + iventure.Cliente.Email + "</td>");
			body.Append("		<td style=\"background-color: white; width: 106px; height: 21px;\">&nbsp;</td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td colspan=\"3\" style=\"background-color: #669999; height: 21px; font-size: medium;color: white;\"><b>Pagar la solicitud</b></td>");
			body.Append("	</tr>");
			body.Append("	<tr>");
			body.Append("		<td colspan=\"3\" align=\"center\" style=\"background-color: white; text-align: center; \"><a href=\"" + ConfigurationManager.AppSettings["Dominio"] + "Iventure.aspx?" + queryString + "\">Pagar la solicitud</a></td>");
			body.Append("	</tr>");
			body.Append("</table>");
			string razonSocial = iventure.Proveedor.RazonSocial.Replace("Á", "A").Replace("á", "a");
			razonSocial = iventure.Proveedor.RazonSocial.Replace("É", "E").Replace("é", "e");
			razonSocial = iventure.Proveedor.RazonSocial.Replace("Í", "I").Replace("í", "i");
			razonSocial = iventure.Proveedor.RazonSocial.Replace("Ó", "O").Replace("ó", "o");
			razonSocial = iventure.Proveedor.RazonSocial.Replace("Ú", "U").Replace("ú", "u");
			razonSocial = iventure.Proveedor.RazonSocial.Replace("\"", "").Replace("'", "");
			FacadeDao.EnviarMail(iventure.Cliente.Email, "Tiene una solicitud de " + razonSocial + ".", body.ToString(), logo, false);
		}
		public static int ActualizarIventure(Iventure iventure)
		{
			int result;
			DbCommand dbCommand = db.GetStoredProcCommand("spUpdatetIventure", iventure.Id, iventure.DetalleAereo, iventure.ImporteTotal, iventure.CntCuotas, iventure.Vencimiento, iventure.IdVendedor, iventure.EsDonacion);

			try
			{
				db.ExecuteNonQuery(dbCommand);
				result = 1;
			}
			catch (System.Data.SqlClient.SqlException ex)
			{
				result = -100;
			}

			return result;
		}
		public static void CancelarIventure(int idIventure)
		{
			EjecutarSQL("UPDATE Iventure SET Estado='CANCELADO' WHERE IdIventure=" + idIventure.ToString());
		}
		public static void BorrarIventure(int idIventure)
		{
			EjecutarSQL("DELETE Iventure WHERE IdIventure=" + idIventure.ToString());
		}
		public static void ProcesarPago(int idIventure, double importePagado)
		{
			ProcesarPago(idIventure, importePagado, DateTime.Now); 
		}
		public static void ProcesarPago(int idIventure)
		{
			ProcesarPago(idIventure, DateTime.Now);
		}
		public static void ProcesarPago(int idIventure, double importePagado, DateTime fechaPago)
		{
			DbCommand dbCommand = db.GetStoredProcCommand("spProcesarPago", idIventure, importePagado, fechaPago, null, null);
			db.ExecuteNonQuery(dbCommand);
		}
		public static void ProcesarPago(int idIventure, double importePagado, DateTime fechaPago, Int64 trnID, int authorizCode)
		{
			DbCommand dbCommand = db.GetStoredProcCommand("spProcesarPago", idIventure, importePagado, fechaPago, trnID, authorizCode);
			db.ExecuteNonQuery(dbCommand);
		}
		

		public static void ProcesarPago(int idIventure, DateTime fechaPago)
		{
			DbCommand dbCommand = db.GetStoredProcCommand("spProcesarPago", idIventure, null, fechaPago, null, null);
			db.ExecuteNonQuery(dbCommand);
		}
		
		public static void ActualizarEstados()
		{
			DbCommand dbCommand = db.GetStoredProcCommand("spUpdatetIventureState");
			db.ExecuteNonQuery(dbCommand);
		}
		public static bool IventurePagado(int idIventure)
		{
			DbCommand dbCommand = db.GetStoredProcCommand("spIventurePagado", idIventure);
			int result = (int)db.ExecuteScalar(dbCommand);
			return (result == 1);
		}

		#endregion Iventure

		#region Validaciones
		public static bool ExisteUsuarioEmail(string email)
		{
			int exist = (int)DevolverUnValor("IF Exists(SELECT IdUsuario FROM Usuario WHERE Email = '" + email + "') SELECT 1 ELSE SELECT 0");
			return (exist == 1);
		}
		public static bool ExisteClienteCuitDni(string cuitDni)
		{
			int exist = (int)DevolverUnValor("IF Exists(SELECT IdCliente FROM Cliente WHERE CuitDni = '" + cuitDni + "') SELECT 1 ELSE SELECT 0");
			return (exist == 1);
		}
		#endregion Validaciones

		#region MFD CAE Counter
		public static void SetCAEByCUIT(string cuit, string cae, string nroComprobante, double idCAEws, bool produccion)
		{
			DbCommand dbCommand = dbMFD.GetStoredProcCommand("spSetCAEByCUIT", cuit, cae, nroComprobante, idCAEws, (produccion)?1:0);
			db.ExecuteNonQuery(dbCommand);
		}
		#endregion MFD CAE Counter
	}
}
