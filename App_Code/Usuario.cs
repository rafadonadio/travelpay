using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;



/// <summary>
/// Summary description for Usuario
/// </summary>
namespace Core
{
	public enum Rol {SuperAdmin, Administrador, Vendedor, Cliente};
	public class Usuario
	{
		private const string symmProvider = "DESCryptoServiceProvider";

		private int? id;
		private string nombre;
		private string password;
		private string email;
		private int rol;
		private int? idProveedor;
		private int? idCliente;
		private int cantidadIventures;

		public Usuario()
		{
		}
		public Usuario(int? idUsuario, string nombre, string password, string email, int rol, int? idProveedor, int? idCliente)
		{
			this.id = idUsuario;
			this.nombre = nombre;
			this.password = password;
			this.email = email;
			this.rol = rol;
			this.idProveedor = idProveedor;
			this.idCliente = idCliente;
		}

		public int? Id {
			set { id=value;}
			get { return id;}
		}
		public string Nombre
		{
			set { nombre = value; }
			get { return nombre; }
		}
		public string Password
		{
			set { password = value; }
			get { return password; }
		}
		
		/*public string Password
		{
			get { return Serialize(password); }
			set { Deserialize(value); }
		}*/

		public string Email
		{
			set { email = value; }
			get { return email; }
		}
		public int Rol
		{
			set { rol = value; }
			get { return rol; }
		}
		private string rolName;
		public string RolName
		{
			set { rolName = value; }
			get { return rolName; }
		}

		public int? IdProveedor
		{
			set { idProveedor = value; }
			get { return idProveedor; }
		}
		public int? IdCliente
		{
			set { idCliente = value; }
			get { return idCliente; }
		}
		public bool IsProveedor {
			get { return IdProveedor.HasValue; }
		}
		public bool IsCliente	
		{
			get { return IdCliente.HasValue; }
		}
		public bool IsSuper
		{
			get { return !IdProveedor.HasValue && !IdCliente.HasValue; }
		}
		public bool IsAdminProveedor
		{
			get { return Rol==2; }
		}
		public bool IsVendedor
		{
			get { return Rol == 3; }
		}

		public int CantidadIventures
		{
			set { cantidadIventures= value; }
			get { return cantidadIventures; }
		}
		
	//    public byte[] Serialize(string password)
	//    {
	//        Stream str = new MemoryStream();
	//        BinaryFormatter formater = new BinaryFormatter();
	//        try
	//        {
	//            formater.Serialize(str, password);
	//        }
	//        catch (Exception)
	//        {
	//            return null;
	//        }

	//        byte[] ret = new byte[str.Length];
	//        str.Position = 0;
	//        str.Read(ret, 0, Convert.ToInt32(str.Length));

	//        return ret;
	//    }
	//    public string Deserialize(byte[] serializedObject)
	//    {
	//        Stream str = new MemoryStream(serializedObject);
	//        BinaryFormatter formater = new BinaryFormatter();
	//        string password = null;
	//        try
	//        {
	//            password = (formater.Deserialize(str) as string);
	//        }
	//        catch (Exception)
	//        {
	//            return null;
	//        }
	//        return password;
	//    }

		public static string Encriptar(string plainText)
		{
			/*string cipherText = Cryptographer.EncryptSymmetric(symmProvider, plainText);*/
			/*string key;
			string cipherText = Cryptography.DESEncrypt(plainText, out key );
			return key + cipherText;*/
			return Cryptography.Encriptar(plainText);

		}

		public static string Desencriptar(string cipherText)
		{
			/*string plainText = Cryptographer.DecryptSymmetric(symmProvider, cipherText);*/
			/*if (cipherText != null && cipherText.Length > 12)
			{
				string key = cipherText.Substring(0, 12);
				string plainText = Cryptography.DESDecrypt(cipherText.Substring(12), key);
				return plainText;
			}
			else return null;*/
			return Cryptography.Desencriptar(cipherText);
		}



	}
}
