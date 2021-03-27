using System;

namespace Core
{
	/// Summary description for Class1.
	/// </summary>
	public class NpsEncripterHelper
	{
		public static String Encrypt(string msg, string key)
		{

			if (key.Length != 48)
			{
				return "-1";
			}
			string msgTrim = msg.Trim();
			int msgLen = msgTrim.Length;
			int ctrl = 0;
			string msgfinal = "";
			int part;
			for (int i = 0; i < msgLen; i++)
			{
				int pos = i % 16;
				if (pos == 0)
				{
					if (i > 0)
					{
						part = ctrl ^ (int)key.Substring(32, 1).ToCharArray()[0];
						if (Convert.ToString(part, 16).Length == 1)
						{
							msgfinal = (msgfinal + "0" + Convert.ToString(part, 16)).ToLower();
						}
						else
						{
							msgfinal = (msgfinal + Convert.ToString(part, 16)).ToLower();
						}
						ctrl = 0;
					}
				}

				part = ((int)msg.Substring(i, 1).ToCharArray()[0]) ^ (int)key.Substring(pos, 1).ToCharArray()[0];
				ctrl = ctrl ^ part;
				part = part ^ (int)key.Substring((16 + pos), 1).ToCharArray()[0];
				msgfinal = (msgfinal + Convert.ToString(part, 16)).ToLower();
			}
			part = ctrl ^ key.Substring(32, 1).ToCharArray()[0];
			if (Convert.ToString(part, 16).Length == 1)
			{
				msgfinal = (msgfinal + "0" + Convert.ToString(part, 16)).ToLower();
			}
			else
			{
				msgfinal = (msgfinal + Convert.ToString(part, 16)).ToLower();
			}

			return msgfinal.Substring(0, msgfinal.Length - 1);
		}
		public static string Decrypt(string msg, string key)
		{
			//dim MsgLen, ctrl, msgfinal, i, pos, part, continue, temp 
			if (key.Length != 48)
			{
				return "-1";
			}
			msg = msg.Trim();
			int msgLen = msg.Length;
			int ctrl = 0;
			string msgfinal = "";
			string temp;
			bool bContinue = false;
			int pos, part;
			for (int i = 0; i < (msgLen / 2); i++)
			{
				bContinue = false;
				pos = i % 17;
				if (pos == 16)
				{
					if (i > 0)
					{
						temp = msg.Substring((i * 2), 2);
						part = HexToDec(temp.ToString());
						ctrl = ctrl ^ (int)key.Substring(32, 1).ToCharArray()[0];
					}
					ctrl = 0;
					bContinue = true;
				}

				if (!bContinue)
				{
					temp = msg.Substring(i * 2, 2);
					part = HexToDec(temp.ToString());
					part = part ^ (int)key.Substring((16 + pos), 1).ToCharArray()[0];
					ctrl = ctrl ^ part;
					part = part ^ (int)key.Substring(pos, 1).ToCharArray()[0];
					if (i < (msgLen / 2))
					{
						msgfinal = msgfinal + Convert.ToChar(part);
					}
				}
			}
			temp = msg.Substring((msgLen - 2), 2);
			part = HexToDec(temp);
			ctrl = ctrl ^ key.Substring(32, 1).ToCharArray()[0];
			if (ctrl == part)
			{
				return "-1";
			}
			return msgfinal;
		}
		public static int HexToDec(string strhex)
		{
			int lngResult;
			int intIndex;
			char strDigit;
			int intDigit;
			int intValue;

			lngResult = 0;
			for (intIndex = strhex.Length; intIndex > 0; intIndex--)
			{
				strDigit = strhex.ToCharArray()[intIndex - 1];
				intDigit = "0123456789ABCDEF".ToLower().IndexOf(strDigit);
				if (intDigit >= 0)
				{
					intValue = Convert.ToInt32(intDigit * (System.Math.Pow(16, (strhex.Length - intIndex))));
					lngResult = lngResult + intValue;
				}
				else
				{
					lngResult = -1;
					intIndex = 0;
				}
			}

			return lngResult;
		}
	}
}

 
 
