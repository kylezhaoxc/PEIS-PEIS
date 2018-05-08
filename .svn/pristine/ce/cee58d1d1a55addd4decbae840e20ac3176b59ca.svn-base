using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PEIS.Common
{
	public class DES
	{
		public static string DesPwd = "!@#$%^&*";

		private static System.Security.Cryptography.DESCryptoServiceProvider des = new System.Security.Cryptography.DESCryptoServiceProvider();

		private static System.Security.Cryptography.ICryptoTransform ct;

		private static MemoryStream ms;

		private static System.Security.Cryptography.CryptoStream cs;

		private static byte[] byt;

		private static DES curDes = null;

		public static DES Instance(string pwd)
		{
			if (DES.curDes == null)
			{
				DES.curDes = new DES(pwd);
			}
			return DES.curDes;
		}

		private DES(string pwd)
		{
			byte[] bytes = new System.Text.UnicodeEncoding().GetBytes(pwd);
			byte[] array = ((System.Security.Cryptography.HashAlgorithm)System.Security.Cryptography.CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes);
			char c = (char)array[0];
			string text = c.ToString();
			for (int i = 1; i < 16; i++)
			{
				string arg_49_0 = text;
				c = (char)array[i];
				text = arg_49_0 + c.ToString();
			}
			DES.des.Key = System.Text.Encoding.ASCII.GetBytes(text.Substring(8, 8));
			DES.des.IV = System.Text.Encoding.ASCII.GetBytes(text.Substring(0, 8));
		}

		public string EncryptString(string Value)
		{
			DES.ct = DES.des.CreateEncryptor(DES.des.Key, DES.des.IV);
			DES.byt = System.Text.Encoding.UTF8.GetBytes(Value);
			DES.ms = new MemoryStream();
			DES.cs = new System.Security.Cryptography.CryptoStream(DES.ms, DES.ct, System.Security.Cryptography.CryptoStreamMode.Write);
			DES.cs.Write(DES.byt, 0, DES.byt.Length);
			DES.cs.FlushFinalBlock();
			DES.cs.Close();
			return Convert.ToBase64String(DES.ms.ToArray());
		}

		public string DecryptString(string Value)
		{
			string result;
			try
			{
				DES.ct = DES.des.CreateDecryptor(DES.des.Key, DES.des.IV);
				DES.byt = Convert.FromBase64String(Value);
				DES.ms = new MemoryStream();
				DES.cs = new System.Security.Cryptography.CryptoStream(DES.ms, DES.ct, System.Security.Cryptography.CryptoStreamMode.Write);
				DES.cs.Write(DES.byt, 0, DES.byt.Length);
				DES.cs.FlushFinalBlock();
				DES.cs.Close();
				result = System.Text.Encoding.UTF8.GetString(DES.ms.ToArray());
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
