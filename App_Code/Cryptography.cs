using System;
using System.Security.Cryptography;
using System.Text;



namespace Core
{
	/// <summary>
	/// Maneja la encriptación de textos
	/// </summary>
	public class Cryptography
	{
		/// <summary>
		/// Constructor por default
		/// </summary>
		static Cryptography()
		{
		}

		/// <summary>
		/// Encripta un texto
		/// </summary>
		/// <param name="message">Texto a encriptar</param>
		/// <param name="key">Clave que se deberá utilizar para desencriptar</param>
		/// <returns>Texto encriptado</returns>
		private static string DESEncrypt(string message, out string key)
		{ 
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			key = Convert.ToBase64String(des.Key);
			des.Mode = CipherMode.ECB;
			ICryptoTransform encryptor = des.CreateEncryptor();
			byte[] data = Encoding.Unicode.GetBytes(message);
			byte[] dataEncrypted = encryptor.TransformFinalBlock(data, 0, data.Length);
			return Convert.ToBase64String(dataEncrypted);
		}
		/// <summary>
		/// Desencripta un texto
		/// </summary>
		/// <param name="message">Texto a desencriptar</param>
		/// <param name="key">Clave utilizada para desencriptar</param>
		/// <returns>Texto desencriptado</returns>
		private static string DESDecrypt(string message, string key)
		{ 
			DESCryptoServiceProvider des = new DESCryptoServiceProvider();
			des.Key = Convert.FromBase64String(key);
			des.Mode = CipherMode.ECB;
			ICryptoTransform decryptor = des.CreateDecryptor();
			byte[] data = Convert.FromBase64String(message);
			byte[] dataDecrypted = decryptor.TransformFinalBlock(data, 0, data.Length);
			return Encoding.Unicode.GetString(dataDecrypted);
 		}

		public static string Encriptar(string plainText)
		{
			string key;
			string cipherText = Cryptography.DESEncrypt(plainText, out key);
			return key + cipherText;

		}

		public static string Desencriptar(string cipherText)
		{
			if (cipherText != null && cipherText.Length > 12)
			{
				string key = cipherText.Substring(0, 12);
				string plainText = Cryptography.DESDecrypt(cipherText.Substring(12), key);
				return plainText;
			}
			else return null;
		}
	}
}
