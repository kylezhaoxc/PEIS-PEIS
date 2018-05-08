using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;

namespace PEIS.Common
{
	public class Public
	{
		public static string rootDir
		{
			get
			{
				string text = HttpContext.Current.Request.ApplicationPath;
				if (text == "/")
				{
					text = string.Empty;
				}
				return text;
			}
		}

		public static string nopic
		{
			get
			{
				string result;
				if (ConfigurationManager.AppSettings["nopic"] != null)
				{
					result = ConfigurationManager.AppSettings["nopic"].ToString();
				}
				else
				{
					result = "/common/images/nopic.jpg";
				}
				return result;
			}
		}

		public static string nohead
		{
			get
			{
				string result;
				if (ConfigurationManager.AppSettings["nohead"] != null)
				{
					result = ConfigurationManager.AppSettings["nohead"].ToString();
				}
				else
				{
					result = "/common/images/nohead.gif";
				}
				return result;
			}
		}

		public static string LostDot(string input)
		{
			string result;
			if (string.IsNullOrEmpty(input))
			{
				result = string.Empty;
			}
			else if (input.IndexOf(",") > -1)
			{
				int num = input.LastIndexOf(",");
				if (num + 1 == input.Length)
				{
					result = input.Remove(num);
				}
				else
				{
					result = input;
				}
			}
			else
			{
				result = input;
			}
			return result;
		}

		public static bool ValidateIP(string LimitedIP)
		{
			string clientIP = Public.GetClientIP();
			bool result;
			if (string.IsNullOrEmpty(LimitedIP))
			{
				result = true;
			}
			else
			{
				LimitedIP.Replace(".", "\\.");
				LimitedIP.Replace("*", "[^\\.]{1,3}");
				Regex regex = new Regex(LimitedIP, RegexOptions.Compiled);
				Match match = regex.Match(clientIP);
				result = !match.Success;
			}
			return result;
		}

		public static void DelFile(string path)
		{
			try
			{
				if (File.Exists(path))
				{
					File.Delete(path);
				}
			}
			catch
			{
			}
		}

		public static void DelDir(string path)
		{
			try
			{
				if (Directory.Exists(path))
				{
					Directory.Delete(path);
				}
			}
			catch
			{
			}
		}

		public static string GetClientIP()
		{
			string result;
			if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
			{
				result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
			}
			else
			{
				result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
			}
			return result;
		}

		public static string GetReferrerUrl()
		{
			Uri urlReferrer = HttpContext.Current.Request.UrlReferrer;
			string result;
			if (urlReferrer != null)
			{
				result = urlReferrer.ToString();
			}
			else
			{
				result = string.Empty;
			}
			return result;
		}

		public static void SaveXmlConfig(string strTarget, string strValue, string strSource)
		{
			string filename = HttpContext.Current.Server.MapPath(strSource);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(filename);
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNodeList elementsByTagName = documentElement.GetElementsByTagName(strTarget);
			elementsByTagName[0].InnerXml = strValue;
			xmlDocument.Save(filename);
		}

		public static string getTimeSpan(DateTime time1)
		{
			DateTime now = DateTime.Now;
			DateTime d = time1;
			System.TimeSpan timeSpan = now - d;
			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			double num = (double)timeSpan.Minutes;
			int seconds = timeSpan.Seconds;
			if (seconds < 0)
			{
			}
			string result;
			if (days == 0 && hours == 0 && num == 0.0)
			{
				result = "刚刚更新";
			}
			else if (days == 0 && hours == 0)
			{
				result = num + "分钟前";
			}
			else if (days == 0)
			{
				result = hours + "小时前";
			}
			else
			{
				result = time1.ToString("yyyy-MM-dd HH:mm");
			}
			return result;
		}

		public static string getTimeLEXYearSpan(DateTime time1)
		{
			DateTime now = DateTime.Now;
			DateTime d = time1;
			System.TimeSpan timeSpan = now - d;
			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			double num = (double)timeSpan.Minutes;
			int seconds = timeSpan.Seconds;
			string result;
			if (days == 0 && hours == 0 && num == 0.0)
			{
				result = "刚刚更新";
			}
			else if (days == 0 && hours == 0)
			{
				result = num + "分钟前";
			}
			else if (days == 0)
			{
				result = hours + "小时前";
			}
			else
			{
				result = time1.ToString("yyyy年MM月dd日");
			}
			return result;
		}

		public static string getTimeEXYearSpan(DateTime time1)
		{
			DateTime now = DateTime.Now;
			DateTime d = time1;
			System.TimeSpan timeSpan = now - d;
			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			double num = (double)timeSpan.Minutes;
			int seconds = timeSpan.Seconds;
			string result;
			if (days == 0 && hours == 0 && num == 0.0)
			{
				result = "刚刚更新";
			}
			else if (days == 0 && hours == 0)
			{
				result = num + "分钟前";
			}
			else if (days == 0)
			{
				result = hours + "小时前";
			}
			else
			{
				result = time1.ToString("yyyy-MM-dd");
			}
			return result;
		}

		public static string getTimeEXMinutesSpan(DateTime time1)
		{
			DateTime now = DateTime.Now;
			DateTime d = time1;
			System.TimeSpan timeSpan = now - d;
			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			double num = (double)timeSpan.Minutes;
			int seconds = timeSpan.Seconds;
			string result;
			if (days == 0 && hours == 0 && num == 0.0)
			{
				result = "刚刚更新";
			}
			else if (days == 0 && hours == 0)
			{
				result = num + "分钟前";
			}
			else if (days == 0)
			{
				result = hours + "小时前";
			}
			else if (days == 1)
			{
				result = "昨天" + d.ToString("HH:mm");
			}
			else if (days == 2)
			{
				result = "前天" + d.ToString("HH:mm");
			}
			else
			{
				result = time1.ToString("yyyy-MM-dd HH:mm");
			}
			return result;
		}

		public static string getTimeEXSpan(DateTime time1)
		{
			DateTime now = DateTime.Now;
			DateTime d = time1;
			System.TimeSpan timeSpan = now - d;
			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			double num = (double)timeSpan.Minutes;
			int seconds = timeSpan.Seconds;
			string result;
			if (days == 0 && hours == 0 && num == 0.0)
			{
				result = "刚刚更新";
			}
			else if (days == 0 && hours == 0)
			{
				result = num + "分钟前";
			}
			else if (days == 0)
			{
				result = hours + "小时前";
			}
			else
			{
				result = time1.ToString("MM-dd");
			}
			return result;
		}

		public static string getTimeEXPINSpan(DateTime time1)
		{
			DateTime now = DateTime.Now;
			DateTime d = time1;
			System.TimeSpan timeSpan = now - d;
			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			double num = (double)timeSpan.Minutes;
			int seconds = timeSpan.Seconds;
			string result;
			if (days == 0 && hours == 0 && num == 0.0)
			{
				result = "刚刚更新";
			}
			else if (days == 0 && hours == 0)
			{
				result = num + "分钟前";
			}
			else if (days == 0)
			{
				result = hours + "小时前";
			}
			else
			{
				result = time1.ToString("MM月dd日");
			}
			return result;
		}

		public static string getTimeEXTSpan(DateTime time1)
		{
			DateTime now = DateTime.Now;
			DateTime d = time1;
			System.TimeSpan timeSpan = now - d;
			int days = timeSpan.Days;
			int hours = timeSpan.Hours;
			double num = (double)timeSpan.Minutes;
			int seconds = timeSpan.Seconds;
			string result;
			if (days == 0 && hours == 0 && num == 0.0)
			{
				result = "刚刚";
			}
			else if (days == 0 && hours == 0)
			{
				result = num + "分钟前";
			}
			else if (days == 0)
			{
				result = hours + "小时前";
			}
			else if (days == 1)
			{
				result = "昨天" + d.ToString("HH:mm");
			}
			else if (days == 2)
			{
				result = "前天" + d.ToString("HH:mm");
			}
			else
			{
				result = time1.ToString("MM月dd日");
			}
			return result;
		}

		public static string getDaySpan(DateTime time1)
		{
			return (DateTime.Now - time1).Days.ToString();
		}

		public static string GetTextFileContent(string filePath)
		{
			string result = string.Empty;
			if (File.Exists(filePath))
			{
				try
				{
					using (StreamReader streamReader = new StreamReader(filePath))
					{
						result = streamReader.ReadToEnd();
					}
				}
				catch
				{
				}
			}
			return result;
		}

		public static string ReplaceSpace(string str)
		{
			string text = str.Replace(" ", "&nbsp;");
			return text.Replace("\n", "<BR />");
		}

		public static string GetFileEXT(string filename)
		{
			string result;
			if (string.IsNullOrEmpty(filename))
			{
				result = "";
			}
			else if (filename.IndexOf(".") == -1)
			{
				result = "";
			}
			else
			{
				int num = -1;
				if (filename.IndexOf("\\") != -1)
				{
					num = filename.LastIndexOf("\\");
				}
				string[] array = filename.Substring(num + 1).Split(new char[]
				{
					'.'
				});
				result = array[1];
			}
			return result;
		}

		public static string GetXMLBaseValue(string target)
		{
			return Public.GetXMLValue(target, "~/config/base/base.config");
		}

		public static string GetXMLValue(string Target, string Path)
		{
			string result;
			try
			{
				string filename = HttpContext.Current.Server.MapPath(Path);
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(filename);
				XmlElement documentElement = xmlDocument.DocumentElement;
				XmlNode xmlNode = documentElement.SelectSingleNode(Target);
				if (xmlNode != null)
				{
					result = xmlNode.InnerXml;
				}
				else
				{
					result = string.Empty;
				}
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		public static void setXmlInnerText(string filepath, string xpath, string value)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string filename = HttpContext.Current.Server.MapPath(filepath);
			xmlDocument.Load(filename);
			XmlNode xmlNode = xmlDocument.SelectSingleNode(xpath);
			if (xmlNode != null)
			{
				xmlNode.InnerText = value;
				xmlDocument.Save(filename);
			}
		}

		public static DataTable GetSecurityLevelDataFromEnum()
		{
			DataTable dataTable = new DataTable();
			System.Type typeFromHandle = typeof(EnumSecurityLevel);
			if (dataTable.Columns.Count == 0)
			{
				dataTable.Columns.Add("key", typeof(int));
				dataTable.Columns.Add("value", typeof(string));
			}
			dataTable.Clear();
			foreach (int num in System.Enum.GetValues(typeFromHandle))
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["key"] = num;
				dataRow["value"] = System.Enum.GetName(typeFromHandle, num);
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}

		public static bool CheckIPV4(string ipAddress)
		{
			string pattern = "((25[0-5]|2[0-4]\\d|1\\d{2}|0?[1-9]\\d|0?0?\\d)\\.){3}(25[0-5]|2[0-4]\\d|1\\d{2}|0?[1-9]\\d|0?0?\\d)";
			return Regex.IsMatch(ipAddress, pattern);
		}

		public static string GetDateDiff(string Title, DateTime BeginDate, DateTime EndDate)
		{
			System.TimeSpan ts = new System.TimeSpan(BeginDate.Ticks);
			System.TimeSpan timeSpan = new System.TimeSpan(EndDate.Ticks);
			return Title + "耗时:" + timeSpan.Subtract(ts).Duration().TotalSeconds.ToString() + "秒";
		}

		public static string GetGuid(string ReplaceTitle, string ReplaceToTitle)
		{
			return System.Guid.NewGuid().ToString().Replace(ReplaceTitle, ReplaceToTitle);
		}

		public static Image ReadImage(byte[] bytes)
		{
			Image result = null;
			using (MemoryStream memoryStream = new MemoryStream(bytes))
			{
				memoryStream.Write(bytes, 0, bytes.Length);
				result = Image.FromStream(memoryStream, true);
				memoryStream.Close();
			}
			return result;
		}
	}
}
