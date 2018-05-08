using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Xml;

namespace PEIS.Common
{
	public class IPConfig
	{
		private static string[] Columns = new string[]
		{
			"GUID",
			"DepartName",
			"DepartCode",
			"DefaultHeaderCode",
			"BeginIP",
			"EndIP",
			"Enable"
		};

		private static DataTable dt = new DataTable();

		private static string ConfigName = IPConfig.GetAssemblyLocation("IPSetting.xml");

		public static string GetAssemblyLocation(string FileName)
		{
			string result = FileName;
			try
			{
				if (FileName.LastIndexOf("\\") <= -1)
				{
					result = System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\")) + "\\" + FileName;
				}
			}
			catch
			{
			}
			return result;
		}

		public static string GetIPList(string ConfigName, ref string ErrorMsg)
		{
			if (IPConfig.dt.Columns.Count == 0)
			{
				string[] columns = IPConfig.Columns;
				for (int i = 0; i < columns.Length; i++)
				{
					string columnName = columns[i];
					IPConfig.dt.Columns.Add(columnName);
				}
			}
			IPConfig.dt.Clear();
			List<IPConfigModel> list = new List<IPConfigModel>();
			string result;
			if (!File.Exists(ConfigName))
			{
				ErrorMsg = "文件[" + ConfigName + "]不存在！";
				result = null;
			}
			else
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(ConfigName);
					XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Depart");
					foreach (XmlNode xmlNode in xmlNodeList)
					{
						DataRow dataRow = IPConfig.dt.NewRow();
						dataRow["GUID"] = xmlNode.SelectSingleNode("GUID").InnerText.Trim();
						dataRow["DepartName"] = xmlNode.SelectSingleNode("DepartName").InnerText.Trim();
						dataRow["DepartCode"] = xmlNode.SelectSingleNode("DepartCode").InnerText.Trim();
						dataRow["DefaultHeaderCode"] = xmlNode.SelectSingleNode("DefaultHeaderCode").InnerText.Trim();
						dataRow["BeginIP"] = xmlNode.SelectSingleNode("BeginIP").InnerText.Trim();
						dataRow["EndIP"] = xmlNode.SelectSingleNode("EndIP").InnerText.Trim();
						dataRow["Enable"] = xmlNode.SelectSingleNode("Enable").InnerText.Trim();
						IPConfig.dt.Rows.Add(dataRow);
					}
				}
				catch
				{
				}
				result = JsonHelperFont.Instance.DataTableToJSON(IPConfig.dt, "dataList");
			}
			return result;
		}

		public static DataTable GetAllIPList(string ConfigName, ref string ErrorMsg)
		{
			if (IPConfig.dt.Columns.Count == 0)
			{
				string[] columns = IPConfig.Columns;
				for (int i = 0; i < columns.Length; i++)
				{
					string columnName = columns[i];
					IPConfig.dt.Columns.Add(columnName);
				}
			}
			IPConfig.dt.Clear();
			List<IPConfigModel> list = new List<IPConfigModel>();
			DataTable result;
			if (!File.Exists(ConfigName))
			{
				ErrorMsg = "文件[" + ConfigName + "]不存在！";
				result = null;
			}
			else
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(ConfigName);
					XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Depart");
					foreach (XmlNode xmlNode in xmlNodeList)
					{
						DataRow dataRow = IPConfig.dt.NewRow();
						dataRow["GUID"] = xmlNode.SelectSingleNode("GUID").InnerText.Trim();
						dataRow["DepartName"] = xmlNode.SelectSingleNode("DepartName").InnerText.Trim();
						dataRow["DepartCode"] = xmlNode.SelectSingleNode("DepartCode").InnerText.Trim();
						dataRow["DefaultHeaderCode"] = xmlNode.SelectSingleNode("DefaultHeaderCode").InnerText.Trim();
						dataRow["BeginIP"] = xmlNode.SelectSingleNode("BeginIP").InnerText.Trim();
						dataRow["EndIP"] = xmlNode.SelectSingleNode("EndIP").InnerText.Trim();
						dataRow["Enable"] = xmlNode.SelectSingleNode("Enable").InnerText.Trim();
						IPConfig.dt.Rows.Add(dataRow);
					}
				}
				catch
				{
				}
				result = IPConfig.dt;
			}
			return result;
		}

		public static DataTable GetIPList(string ConfigName, string GUID, ref string ErrorMsg)
		{
			if (IPConfig.dt.Columns.Count == 0)
			{
				string[] columns = IPConfig.Columns;
				for (int i = 0; i < columns.Length; i++)
				{
					string columnName = columns[i];
					IPConfig.dt.Columns.Add(columnName);
				}
			}
			IPConfig.dt.Clear();
			List<IPConfigModel> list = new List<IPConfigModel>();
			DataTable result;
			if (!File.Exists(ConfigName))
			{
				ErrorMsg = "文件[" + ConfigName + "]不存在！";
				result = null;
			}
			else
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(ConfigName);
					XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Depart");
					foreach (XmlNode xmlNode in xmlNodeList)
					{
						if (GUID.Trim() == xmlNode.SelectSingleNode("GUID").InnerText.Trim())
						{
							DataRow dataRow = IPConfig.dt.NewRow();
							dataRow["GUID"] = xmlNode.SelectSingleNode("GUID").InnerText.Trim();
							dataRow["DepartName"] = xmlNode.SelectSingleNode("DepartName").InnerText.Trim();
							dataRow["DepartCode"] = xmlNode.SelectSingleNode("DepartCode").InnerText.Trim();
							dataRow["DefaultHeaderCode"] = xmlNode.SelectSingleNode("DefaultHeaderCode").InnerText.Trim();
							dataRow["BeginIP"] = xmlNode.SelectSingleNode("BeginIP").InnerText.Trim();
							dataRow["EndIP"] = xmlNode.SelectSingleNode("EndIP").InnerText.Trim();
							dataRow["Enable"] = xmlNode.SelectSingleNode("Enable").InnerText.Trim();
							IPConfig.dt.Rows.Add(dataRow);
						}
					}
				}
				catch
				{
				}
				result = IPConfig.dt;
			}
			return result;
		}

		public static List<IPConfigModel> GetIPServer(string ConfigName, ref string ErrorMsg)
		{
			List<IPConfigModel> list = new List<IPConfigModel>();
			List<IPConfigModel> result;
			if (!File.Exists(ConfigName))
			{
				ErrorMsg = "文件[" + ConfigName + "]不存在！";
				result = null;
			}
			else
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(ConfigName);
					XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Depart");
					foreach (XmlNode xmlNode in xmlNodeList)
					{
						list.Add(new IPConfigModel
						{
							GUID = xmlNode.SelectSingleNode("GUID").InnerText.Trim(),
							DepartName = xmlNode.SelectSingleNode("DepartName").InnerText.Trim(),
							DepartCode = xmlNode.SelectSingleNode("DepartCode").InnerText.Trim(),
							DefaultHeaderCode = xmlNode.SelectSingleNode("DefaultHeaderCode").InnerText.Trim(),
							BeginIP = xmlNode.SelectSingleNode("BeginIP").InnerText.Trim(),
							EndIP = xmlNode.SelectSingleNode("EndIP").InnerText.Trim(),
							Enable = xmlNode.SelectSingleNode("Enable").InnerText.Trim()
						});
					}
				}
				catch
				{
				}
				result = list;
			}
			return result;
		}

		public static List<IPConfigModel> GetIPServer(ref string ErrorMsg)
		{
			List<IPConfigModel> list = new List<IPConfigModel>();
			List<IPConfigModel> result;
			if (!File.Exists(IPConfig.ConfigName))
			{
				ErrorMsg = "文件[" + IPConfig.ConfigName + "]不存在！";
				result = null;
			}
			else
			{
				try
				{
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(IPConfig.ConfigName);
					XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Depart");
					foreach (XmlNode xmlNode in xmlNodeList)
					{
						list.Add(new IPConfigModel
						{
							GUID = xmlNode.SelectSingleNode("GUID").InnerText.Trim(),
							DepartName = xmlNode.SelectSingleNode("DepartName").InnerText.Trim(),
							DepartCode = xmlNode.SelectSingleNode("DepartCode").InnerText.Trim(),
							DefaultHeaderCode = xmlNode.SelectSingleNode("DefaultHeaderCode").InnerText.Trim(),
							BeginIP = xmlNode.SelectSingleNode("BeginIP").InnerText.Trim(),
							EndIP = xmlNode.SelectSingleNode("EndIP").InnerText.Trim(),
							Enable = xmlNode.SelectSingleNode("Enable").InnerText.Trim()
						});
					}
				}
				catch
				{
				}
				result = list;
			}
			return result;
		}

		public void SetIPServer(string NodeName, string NodeValue)
		{
		}

		public static void SaveIPServer(List<IPConfigModel> IPConfigModelList)
		{
			if (IPConfigModelList.Count > 0)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(IPConfig.ConfigName);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("IPSetting");
				xmlNode.RemoveAll();
				foreach (IPConfigModel current in IPConfigModelList)
				{
					XmlElement xmlElement = xmlDocument.CreateElement("Depart");
					XmlElement xmlElement2 = xmlDocument.CreateElement("GUID");
					XmlElement xmlElement3 = xmlDocument.CreateElement("DepartName");
					XmlElement xmlElement4 = xmlDocument.CreateElement("DepartCode");
					XmlElement xmlElement5 = xmlDocument.CreateElement("DefaultHeaderCode");
					XmlElement xmlElement6 = xmlDocument.CreateElement("BeginIP");
					XmlElement xmlElement7 = xmlDocument.CreateElement("EndIP");
					XmlElement xmlElement8 = xmlDocument.CreateElement("Enable");
					xmlElement2.InnerText = current.GUID;
					xmlElement3.InnerText = current.DepartName;
					xmlElement4.InnerText = current.DepartCode;
					xmlElement5.InnerText = "11";
					xmlElement6.InnerText = current.BeginIP;
					xmlElement7.InnerText = current.EndIP;
					xmlElement8.InnerText = current.Enable;
					xmlElement.AppendChild(xmlElement2);
					xmlElement.AppendChild(xmlElement3);
					xmlElement.AppendChild(xmlElement4);
					xmlElement.AppendChild(xmlElement5);
					xmlElement.AppendChild(xmlElement6);
					xmlElement.AppendChild(xmlElement7);
					xmlElement.AppendChild(xmlElement8);
					xmlNode.AppendChild(xmlElement);
				}
				xmlDocument.Save(IPConfig.ConfigName);
			}
		}

		public static int RemoveIPServer(string ConfigName, string GUID)
		{
			int result;
			if (!File.Exists(ConfigName))
			{
				result = -1;
			}
			else
			{
				int num = 0;
				if (GUID != string.Empty)
				{
					string[] array = GUID.Trim().Split(new char[]
					{
						','
					});
					XmlDocument xmlDocument = new XmlDocument();
					xmlDocument.Load(ConfigName);
					XmlNode xmlNode = xmlDocument.SelectSingleNode("IPSetting");
					XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Depart");
					foreach (XmlNode xmlNode2 in xmlNodeList)
					{
						string[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							string text = array2[i];
							if (text != string.Empty)
							{
								if (xmlNode2.SelectSingleNode("GUID").InnerText == text)
								{
									xmlNode.RemoveChild(xmlNode2);
									num++;
								}
							}
						}
					}
					xmlDocument.Save(ConfigName);
				}
				result = num;
			}
			return result;
		}

		public static int SaveIPServer(string ConfigName, List<IPConfigModel> IPConfigModelList, ref int ModifyCount)
		{
			int num = 0;
			ModifyCount = 0;
			if (IPConfigModelList.Count > 0)
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(ConfigName);
				XmlNode xmlNode = xmlDocument.SelectSingleNode("IPSetting");
				XmlNodeList xmlNodeList = xmlDocument.DocumentElement.SelectNodes("Depart");
				string text = string.Empty;
				foreach (XmlNode xmlNode2 in xmlNodeList)
				{
					foreach (IPConfigModel current in IPConfigModelList)
					{
						if (xmlNode2.SelectSingleNode("GUID").InnerText.Trim() == current.GUID.Trim())
						{
							text = text + current.GUID.Trim() + ",";
							xmlNode2.SelectSingleNode("DepartName").InnerText = current.DepartName.Trim();
							xmlNode2.SelectSingleNode("DepartCode").InnerText = current.DepartCode.Trim();
							xmlNode2.SelectSingleNode("DefaultHeaderCode").InnerText = current.DefaultHeaderCode.Trim();
							xmlNode2.SelectSingleNode("BeginIP").InnerText = current.BeginIP.Trim();
							xmlNode2.SelectSingleNode("EndIP").InnerText = current.EndIP.Trim();
							xmlNode2.SelectSingleNode("Enable").InnerText = current.Enable.Trim();
							ModifyCount++;
						}
					}
				}
				foreach (IPConfigModel current in IPConfigModelList)
				{
					if (!text.Contains(current.GUID + ","))
					{
						XmlElement xmlElement = xmlDocument.CreateElement("Depart");
						XmlElement xmlElement2 = xmlDocument.CreateElement("GUID");
						XmlElement xmlElement3 = xmlDocument.CreateElement("DepartName");
						XmlElement xmlElement4 = xmlDocument.CreateElement("DepartCode");
						XmlElement xmlElement5 = xmlDocument.CreateElement("DefaultHeaderCode");
						XmlElement xmlElement6 = xmlDocument.CreateElement("BeginIP");
						XmlElement xmlElement7 = xmlDocument.CreateElement("EndIP");
						XmlElement xmlElement8 = xmlDocument.CreateElement("Enable");
						xmlElement2.InnerText = current.GUID.Trim();
						xmlElement3.InnerText = current.DepartName.Trim();
						xmlElement4.InnerText = current.DepartCode.Trim();
						xmlElement5.InnerText = current.DefaultHeaderCode.Trim();
						xmlElement6.InnerText = current.BeginIP.Trim();
						xmlElement7.InnerText = current.EndIP.Trim();
						xmlElement8.InnerText = current.Enable.Trim();
						xmlElement.AppendChild(xmlElement2);
						xmlElement.AppendChild(xmlElement3);
						xmlElement.AppendChild(xmlElement4);
						xmlElement.AppendChild(xmlElement5);
						xmlElement.AppendChild(xmlElement6);
						xmlElement.AppendChild(xmlElement7);
						xmlElement.AppendChild(xmlElement8);
						xmlNode.AppendChild(xmlElement);
						num++;
					}
				}
				xmlDocument.Save(ConfigName);
			}
			return num;
		}
	}
}
