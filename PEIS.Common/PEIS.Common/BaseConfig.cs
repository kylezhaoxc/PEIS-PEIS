using System;
using System.IO;
using System.Web;
using System.Xml;

namespace PEIS.Common
{
	public class BaseConfig
	{
		public static readonly string ReportReceipteDays = GetConfigValue("ReportReceipteDays");

		public static readonly string SectionTagPrintRel = GetConfigValue("SectionTagPrintRel");

		public static readonly string VideoCaptureSection = GetConfigValue("VideoCaptureSection");

		public static readonly string VideoImagesListSection = GetConfigValue("VideoImagesListSection");

		public static readonly string ComReadSection = GetConfigValue("ComReadSection");

		public static readonly string IsFileUploadMulitUrl = GetConfigValue("IsFileUploadMulitUrl");

		public static readonly string DiseaseLevelWarning = GetConfigValue("DiseaseLevelWarning");

		public static readonly string CustomerFileSavePath = GetConfigValue("GetCustomerFileSavePath");

		public static readonly string CustomerFileResultFlagSavePath = GetConfigValue("GetCustomerFileResultFlagSavePath");

		public static readonly string WriteCustomerFileSection = GetConfigValue("WriteCustomerFileSection");

		public static readonly string MenuModuleType = GetConfigValue("MenuModuleType");

		public static readonly string ImageFileUploadUrl = GetConfigValue("ImageFileUploadUrl");

		internal static string GetConfigValue(string Target)
		{
			string XmlPath = HttpContext.Current.Server.MapPath("~/config/base/base.config");
			//string XmlPath = text;
			bool[] cdata = new bool[1];
			return GetConfigValue(Target, XmlPath, cdata);
		}

		internal static string FileStreamReadFile(string filePath)
		{
			string result = string.Empty;
			try
			{
				FileStream fileStream = new FileStream(filePath, FileMode.Open);
				StreamReader streamReader = new StreamReader(fileStream);
				result = streamReader.ReadToEnd();
				streamReader.Close();
				streamReader.Dispose();
				fileStream.Close();
				fileStream.Dispose();
			}
			catch (System.Exception ex)
			{
			}
			return result;
		}

		internal static string GetConfigValue(string Target, string XmlPath, params bool[] cdata)
		{
			XmlDocument xmlDocument = new XmlDocument();
			try
			{
				string xml = SecretCommon.AES.Decrypt(BaseConfig.FileStreamReadFile(XmlPath));
                //string xml = BaseConfig.FileStreamReadFile(XmlPath);
				xmlDocument.LoadXml(xml);
			}
			catch (System.Exception ex)
			{
				xmlDocument.Load(XmlPath);
			}
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNode = documentElement.SelectSingleNode(Target);
			string result;
			if (xmlNode != null)
			{
				if (cdata != null && cdata[0])
				{
					result = xmlNode.InnerText;
				}
				else
				{
					result = xmlNode.InnerXml;
				}
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}
	}
}
