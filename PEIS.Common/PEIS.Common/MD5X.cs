using System;
using System.Security.Cryptography;
using System.Text;

namespace PEIS.Common
{
	public class MD5X
	{
		public static string MD5(string str)
		{
			System.Security.Cryptography.MD5 mD = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
			byte[] array = mD.ComputeHash(bytes);
			string text = "";
			for (int i = 0; i < array.Length; i++)
			{
				text += array[i].ToString("x").PadLeft(2, '0');
			}
			return text.ToUpper();
		}
	}
}
