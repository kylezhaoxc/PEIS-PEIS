using PEIS.Common;
using System;
using System.Configuration;

namespace PEIS.DBUtility
{
	public class PubConstant
	{
		public static string ConnectionString
		{
			get
			{
				string text = ConfigurationManager.AppSettings["ConnectionString"];
				string a = ConfigurationManager.AppSettings["ConStringEncrypt"].ToLower();
				if (a == "true")
				{
					text = Secret.AES.Decrypt(text);
				}
				return text;
			}
		}

		public static string GetConnectionString(string configName)
		{
			string text = ConfigurationManager.AppSettings[configName];
			string a = ConfigurationManager.AppSettings["ConStringEncrypt"].ToLower();
			if (a == "true")
			{
				text = Secret.AES.Decrypt(text);
			}
			return text;
		}
	}
}
