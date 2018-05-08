using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace PEIS.Common
{
	public class Secret
	{
		public class MD5
		{
			public static string Encrypt(string Input, bool Half)
			{
				string text = FormsAuthentication.HashPasswordForStoringInConfigFile(Input, "MD5").ToLower();
				if (Half)
				{
					text = text.Substring(8, 16);
				}
				return text;
			}

			public static string Encrypt(string Input)
			{
				return Secret.MD5.Encrypt(Input, true);
			}
		}

		public class DES
		{
			private static string sDESKey = "tjzxscrmyy";

			public static string Encrypt(string Text)
			{
				return Secret.DES.Encrypt(Text, Secret.DES.sDESKey);
			}

			public static string Encrypt(string Text, string sKey)
			{
				System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServiceProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
				byte[] bytes = System.Text.Encoding.Default.GetBytes(Text);
				dESCryptoServiceProvider.Key = System.Text.Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
				dESCryptoServiceProvider.IV = System.Text.Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
				MemoryStream memoryStream = new MemoryStream();
				System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, dESCryptoServiceProvider.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
				cryptoStream.Write(bytes, 0, bytes.Length);
				cryptoStream.FlushFinalBlock();
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				byte[] array = memoryStream.ToArray();
				for (int i = 0; i < array.Length; i++)
				{
					byte b = array[i];
					stringBuilder.AppendFormat("{0:X2}", b);
				}
				return stringBuilder.ToString();
			}

			public static string Decrypt(string Text)
			{
				return Secret.DES.Decrypt(Text, Secret.DES.sDESKey);
			}

			public static string Decrypt(string Text, string sKey)
			{
				System.Security.Cryptography.DESCryptoServiceProvider dESCryptoServiceProvider = new System.Security.Cryptography.DESCryptoServiceProvider();
				int num = Text.Length / 2;
				byte[] array = new byte[num];
				for (int i = 0; i < num; i++)
				{
					int num2 = Convert.ToInt32(Text.Substring(i * 2, 2), 16);
					array[i] = (byte)num2;
				}
				dESCryptoServiceProvider.Key = System.Text.Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
				dESCryptoServiceProvider.IV = System.Text.Encoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
				MemoryStream memoryStream = new MemoryStream();
				System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, dESCryptoServiceProvider.CreateDecryptor(), System.Security.Cryptography.CryptoStreamMode.Write);
				cryptoStream.Write(array, 0, array.Length);
				cryptoStream.FlushFinalBlock();
				return System.Text.Encoding.Default.GetString(memoryStream.ToArray());
			}
		}

		public class RSA
		{
			public string Encrypt(string xmlPublicKey, string EncryptString)
			{
				System.Security.Cryptography.RSACryptoServiceProvider rSACryptoServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider();
				rSACryptoServiceProvider.FromXmlString(xmlPublicKey);
				byte[] bytes = new System.Text.UnicodeEncoding().GetBytes(EncryptString);
				byte[] inArray = rSACryptoServiceProvider.Encrypt(bytes, false);
				return Convert.ToBase64String(inArray);
			}

			public string Decrypt(string xmlPrivateKey, string DecryptString)
			{
				System.Security.Cryptography.RSACryptoServiceProvider rSACryptoServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider();
				rSACryptoServiceProvider.FromXmlString(xmlPrivateKey);
				byte[] rgb = Convert.FromBase64String(DecryptString);
				byte[] bytes = rSACryptoServiceProvider.Decrypt(rgb, false);
				return new System.Text.UnicodeEncoding().GetString(bytes);
			}

			public void RSAKey(out string xmlKeys, out string xmlPublicKey)
			{
				System.Security.Cryptography.RSACryptoServiceProvider rSACryptoServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider();
				xmlKeys = rSACryptoServiceProvider.ToXmlString(true);
				xmlPublicKey = rSACryptoServiceProvider.ToXmlString(false);
			}
		}

		public class AES
		{
			private static string strDecryptBegin = "decrypt:";

			private static string Key
			{
				get
				{
					return "M[h%~s9,#}>w*j{+01n.;69'<Q!r+K&Y";
				}
			}

			private static string IV
			{
				get
				{
					return "gk*~】f^8U)b@d#m!";
				}
			}

			public static string Encrypt(string strEncrypt, bool retStrOriginal)
			{
				byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Secret.AES.Key);
				byte[] bytes2 = System.Text.Encoding.UTF8.GetBytes(Secret.AES.IV);
				byte[] bytes3 = System.Text.Encoding.UTF8.GetBytes(strEncrypt);
				string result = string.Empty;
				System.Security.Cryptography.Rijndael rijndael = System.Security.Cryptography.Rijndael.Create();
				try
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, rijndael.CreateEncryptor(bytes, bytes2), System.Security.Cryptography.CryptoStreamMode.Write))
						{
							cryptoStream.Write(bytes3, 0, bytes3.Length);
							cryptoStream.FlushFinalBlock();
							result = Convert.ToBase64String(memoryStream.ToArray());
						}
					}
				}
				catch
				{
					if (retStrOriginal)
					{
						result = strEncrypt;
					}
					else
					{
						result = string.Empty;
					}
				}
				rijndael.Clear();
				return result;
			}

			public static string Encrypt(string strEncrypt)
			{
				return Secret.AES.Encrypt(strEncrypt, false);
			}

			public static string EncryptPrefix(string strEncrypt)
			{
				string result;
				if (string.IsNullOrEmpty(strEncrypt))
				{
					result = strEncrypt;
				}
				else if (SecretConfig.IsDataEncryption == "0")
				{
					result = strEncrypt;
				}
				else
				{
					string text = Secret.AES.Encrypt(strEncrypt, true);
					result = ((text == strEncrypt) ? strEncrypt : (Secret.AES.strDecryptBegin + text));
				}
				return result;
			}

			public static string Decrypt(string strDecrypt, bool retStrOriginal)
			{
				string result = string.Empty;
				System.Security.Cryptography.Rijndael rijndael = System.Security.Cryptography.Rijndael.Create();
				try
				{
					byte[] bytes = System.Text.Encoding.UTF8.GetBytes(Secret.AES.Key);
					byte[] bytes2 = System.Text.Encoding.UTF8.GetBytes(Secret.AES.IV);
					byte[] array = Convert.FromBase64String(strDecrypt);
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (System.Security.Cryptography.CryptoStream cryptoStream = new System.Security.Cryptography.CryptoStream(memoryStream, rijndael.CreateDecryptor(bytes, bytes2), System.Security.Cryptography.CryptoStreamMode.Write))
						{
							cryptoStream.Write(array, 0, array.Length);
							cryptoStream.FlushFinalBlock();
							result = System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
						}
					}
				}
				catch
				{
					if (retStrOriginal)
					{
						result = strDecrypt;
					}
					else
					{
						result = string.Empty;
					}
				}
				rijndael.Clear();
				return result;
			}

			public static string Decrypt(string strDecrypt)
			{
				return Secret.AES.Decrypt(strDecrypt, false);
			}

			public static string DecryptPrefix(string strDecrypt)
			{
				string result;
				if (strDecrypt.StartsWith(Secret.AES.strDecryptBegin))
				{
					strDecrypt = strDecrypt.Substring(Secret.AES.strDecryptBegin.Length);
					if (string.IsNullOrEmpty(strDecrypt))
					{
						result = strDecrypt;
					}
					else
					{
						string text = Secret.AES.Decrypt(strDecrypt, true);
						result = ((text == strDecrypt) ? (Secret.AES.strDecryptBegin.ToString() + strDecrypt) : text);
					}
				}
				else
				{
					result = strDecrypt;
				}
				return result;
			}

			public static string DecryptPrefix(string strOrgDecrypt, bool IsMulti)
			{
				string[] array = strOrgDecrypt.Split(new char[]
				{
					';'
				});
				string text = string.Empty;
				string str = string.Empty;
				string text2 = string.Empty;
				string result;
				try
				{
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text3 = array2[i];
						string[] array3 = text3.Split(new char[]
						{
							' '
						});
						if (array3.Length > 1)
						{
							str = array3[0];
							text2 = array3[1];
							text = text + str + " " + Secret.AES.Decrypt(text2.Replace("decrypt:", string.Empty), true);
						}
						else
						{
							text += text3;
						}
					}
				}
				catch
				{
					result = strOrgDecrypt;
					return result;
				}
				result = text;
				return result;
			}

			public static DataTable Decrypt(DataTable dt)
			{
				foreach (DataColumn dataColumn in dt.Columns)
				{
					DataRow[] array = dt.Select(dataColumn.ColumnName + " like '" + Secret.AES.strDecryptBegin + "%' ");
					if (array != null && array.Length > 0)
					{
						for (int i = 0; i < array.Length; i++)
						{
							array[i][dataColumn.ColumnName] = Secret.AES.DecryptPrefix(array[i][dataColumn.ColumnName].ToString());
						}
					}
				}
				return dt;
			}

			public static DataSet Decrypt(DataSet ds)
			{
				for (int i = 0; i < ds.Tables.Count; i++)
				{
					foreach (DataColumn dataColumn in ds.Tables[i].Columns)
					{
						if (dataColumn.DataType.FullName.ToLower() == "system.string" || dataColumn.DataType.FullName.ToLower() == "system.object")
						{
							DataRow[] array = ds.Tables[i].Select(dataColumn.ColumnName + " like '" + Secret.AES.strDecryptBegin + "%' ");
							if (array != null && array.Length > 0)
							{
								for (int j = 0; j < array.Length; j++)
								{
									array[j][dataColumn.ColumnName] = Secret.AES.DecryptPrefix(array[j][dataColumn.ColumnName].ToString());
								}
							}
							else
							{
								array = ds.Tables[i].Select(dataColumn.ColumnName + " like '%] " + Secret.AES.strDecryptBegin + "%' ");
								if (array != null && array.Length > 0)
								{
									for (int j = 0; j < array.Length; j++)
									{
										array[j][dataColumn.ColumnName] = Secret.AES.DecryptPrefix(array[j][dataColumn.ColumnName].ToString(), true);
									}
								}
							}
						}
					}
				}
				return ds;
			}
		}
	}
}
