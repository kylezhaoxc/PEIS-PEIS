using System;
using System.Data;
using System.Threading;
using System.Web;
using System.Xml;

namespace PEIS.Common
{
	public class Customer_Waste
	{
		private static int _DefaultNumLength = 4;

		private static string _path = HttpContext.Current.Server.MapPath("~/config/base/base.config");

		private static int IsCheckSubScrib = 0;

		private static int DefaultCurCustomerNum = -1;

		private static int DefaultCurSubScribNum = -1;

		private static int DefaultCurTeamSubScribNum = -1;

		private static int DefaultMaxCustomerNum = 9999;

		private static int DefaultMaxSubScribNum = 9999;

		private static int DefaultMaxTeamSubScribNum = 9999;

		private static int DefaultCurInternatSubScribNum = -1;

		private static int DefaultMaxInternatSubScribNum = 9999;

		private static string CurKey = string.Empty;

		private static int DefaultMaxNum = 0;

		private static int DefaultCode = 0;

		private static System.Threading.ReaderWriterLock FileRWLocker = new System.Threading.ReaderWriterLock();

		public static string TestCreateMaxNum(string HeaderCode, string ClientIP)
		{
			XmlDocument xmlDocument = new XmlDocument();
			ClientIP = ((ClientIP == string.Empty) ? "0" : ClientIP);
			string text = string.Empty;
			string text2 = string.Empty;
			string str = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			string text7 = string.Empty;
			string text8 = string.Empty;
			bool flag = false;
			lock ("0")
			{
				text8 = DateTime.Now.ToString("MMddyyyy");
				if (ClientIP.Length != 15)
				{
					string[] array = ClientIP.Split(new char[]
					{
						'.'
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text9 = array2[i];
						text4 += text9.PadLeft(3, '0');
					}
				}
				else
				{
					text4 = ClientIP.Replace(".", string.Empty);
				}
				xmlDocument.Load(Customer_Waste._path);
				XmlElement documentElement = xmlDocument.DocumentElement;
				XmlNodeList xmlNodeList = documentElement.SelectNodes("CustomerNum");
				flag = false;
				foreach (XmlNode xmlNode in xmlNodeList)
				{
					text5 = string.Empty;
					text6 = string.Empty;
					//XmlNode xmlNode;
					text = xmlNode.Attributes["BeginIP"].Value.Trim();
					text2 = xmlNode.Attributes["EndIP"].Value.Trim();
					if (text.Length == text2.Length && text.Length == 15)
					{
						text5 = text.Replace(".", string.Empty);
						text6 = text2.Replace(".", string.Empty);
					}
					else
					{
						string[] array3 = text.Split(new char[]
						{
							'.'
						});
						string[] array4 = text2.Split(new char[]
						{
							'.'
						});
						string[] array2 = array3;
						for (int i = 0; i < array2.Length; i++)
						{
							string text9 = array2[i];
							text5 += text9.PadLeft(3, '0');
						}
						array2 = array4;
						for (int i = 0; i < array2.Length; i++)
						{
							string text9 = array2[i];
							text6 += text9.PadLeft(3, '0');
						}
					}
					if (long.Parse(text4) >= long.Parse(text5) && long.Parse(text4) <= long.Parse(text6))
					{
						flag = true;
						str = xmlNode.Attributes["DefaultHeaderCode"].Value.Trim();
						text3 = xmlNode.Attributes["CurCustomerNum"].Value.Trim();
						if (text3.Contains(text8))
						{
							string s = text3.Substring(text3.Length - 4);
							int num = 0;
							int.TryParse(s, out num);
							num++;
							text7 = str + text8 + num.ToString().PadLeft(4, '0');
						}
						else
						{
							text7 = str + text8 + "0001";
						}
						xmlNode.Attributes["CurCustomerNum"].Value = text7;
						break;
					}
				}
				if (!flag)
				{
					XmlNode xmlNode = documentElement.SelectSingleNode("LostCustomerNum");
					str = xmlNode.Attributes["DefaultHeaderCode"].Value.Trim();
					text3 = xmlNode.Attributes["CurCustomerNum"].Value.Trim();
					if (text3.Contains(text8))
					{
						string s = text3.Substring(text3.Length - 4);
						int num = 0;
						int.TryParse(s, out num);
						num++;
						text7 = str + text8 + num.ToString().PadLeft(4, '0');
					}
					else
					{
						text7 = str + text8 + "0001";
					}
					xmlNode.Attributes["CurCustomerNum"].Value = text7;
				}
				xmlDocument.Save(Customer_Waste._path);
			}
			if (HeaderCode != string.Empty)
			{
				text7 = HeaderCode + text7.Substring(2);
			}
			return text7;
		}

		public static string CreateMaxNumX(ref string Code128c, int OperType)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			string text7 = string.Empty;
			string text8 = string.Empty;
			string text9 = string.Empty;
			string text10 = string.Empty;
			bool flag = false;
			text10 = Public.GetClientIP();
			string result;
			try
			{
				Customer_Waste.FileRWLocker.AcquireWriterLock(new System.TimeSpan(0, 0, 2));
				text9 = DateTime.Now.ToString("MMddyyyy");
				if (!Public.CheckIPV4(text10))
				{
					text10 = string.Empty;
					Log4J.Instance.Info(string.Concat(new string[]
					{
						"客户端IP:",
						text10,
						",UserHostName:",
						HttpContext.Current.Request.UserHostName.ToString(),
						",UserHostAddress:",
						HttpContext.Current.Request.UserHostAddress.ToString(),
						"不满足IPV4标准,系统已主动转换为缺损值!"
					}));
				}
				if (string.IsNullOrEmpty(text10))
				{
					text5 = string.Empty;
				}
				else if (text10.Length != 15)
				{
					string[] array = text10.Split(new char[]
					{
						'.'
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text11 = array2[i];
						text5 += text11.PadLeft(3, '0');
					}
				}
				else
				{
					text5 = text10.Replace(".", string.Empty);
				}
				xmlDocument.Load(Customer_Waste._path);
				if (!xmlDocument.HasChildNodes)
				{
					Customer_Waste.FileRWLocker.ReleaseWriterLock();
					Log4J.Instance.Info(text10 + ",读取体检号配置文件为空(" + Customer_Waste._path + ")");
					result = Customer_Waste.CreateMaxNumX(ref Code128c, OperType);
				}
				else
				{
					XmlElement documentElement = xmlDocument.DocumentElement;
					XmlNodeList xmlNodeList = documentElement.SelectNodes("CustomerNum");
					flag = false;
					try
					{
						XmlNode xmlNode = documentElement.SelectSingleNode("DefaultCurCustomerNum");
						XmlNode xmlNode2 = documentElement.SelectSingleNode("DefaultCurSubScribNum");
						XmlNode xmlNode3 = documentElement.SelectSingleNode("DefaultCurTeamSubScribNum");
						XmlNode xmlNode4 = documentElement.SelectSingleNode("DefaultMaxCustomerNum");
						XmlNode xmlNode5 = documentElement.SelectSingleNode("DefaultMaxSubScribNum");
						XmlNode xmlNode6 = documentElement.SelectSingleNode("DefaultMaxTeamSubScribNum");
						XmlNode xmlNode7 = documentElement.SelectSingleNode("DefaultCurInternatSubScribNum");
						XmlNode xmlNode8 = documentElement.SelectSingleNode("DefaultMaxInternatSubScribNum");
						XmlNode xmlNode9 = documentElement.SelectSingleNode("IsCheckSubScrib");
						int.TryParse(xmlNode9.InnerText.Trim(), out Customer_Waste.IsCheckSubScrib);
						int.TryParse(xmlNode.InnerText.Trim(), out Customer_Waste.DefaultCurCustomerNum);
						int.TryParse(xmlNode2.InnerText.Trim(), out Customer_Waste.DefaultCurSubScribNum);
						int.TryParse(xmlNode3.InnerText.Trim(), out Customer_Waste.DefaultCurTeamSubScribNum);
						int.TryParse(xmlNode4.InnerText.Trim(), out Customer_Waste.DefaultMaxCustomerNum);
						int.TryParse(xmlNode5.InnerText.Trim(), out Customer_Waste.DefaultMaxSubScribNum);
						int.TryParse(xmlNode6.InnerText.Trim(), out Customer_Waste.DefaultMaxTeamSubScribNum);
						int.TryParse(xmlNode7.InnerText.Trim(), out Customer_Waste.DefaultCurInternatSubScribNum);
						int.TryParse(xmlNode8.InnerText.Trim(), out Customer_Waste.DefaultMaxInternatSubScribNum);
						if (Customer_Waste.DefaultCurCustomerNum == -1)
						{
							Customer_Waste.DefaultCurCustomerNum = 3;
						}
						if (Customer_Waste.DefaultCurSubScribNum == -1)
						{
							Customer_Waste.DefaultCurSubScribNum = 6;
						}
						if (Customer_Waste.DefaultCurTeamSubScribNum == -1)
						{
							Customer_Waste.DefaultCurTeamSubScribNum = 9;
						}
						if (Customer_Waste.DefaultCurInternatSubScribNum == -1)
						{
							Customer_Waste.DefaultCurInternatSubScribNum = 5;
						}
					}
					catch (System.Exception ex)
					{
						Log4J.Instance.Info("体检号通用生成方法出现错误：" + ex.Message);
						Customer_Waste.DefaultCurCustomerNum = 3;
						Customer_Waste.DefaultCurSubScribNum = 6;
						Customer_Waste.DefaultCurTeamSubScribNum = 9;
						Customer_Waste.DefaultMaxCustomerNum = 9999;
						Customer_Waste.DefaultMaxSubScribNum = 9999;
						Customer_Waste.DefaultMaxTeamSubScribNum = 9999;
						Customer_Waste.DefaultCurInternatSubScribNum = 5;
						Customer_Waste.DefaultMaxInternatSubScribNum = 9999;
					}
					if (OperType == 1)
					{
						Customer_Waste.CurKey = "CurCustomerNum";
						Customer_Waste.DefaultMaxNum = Customer_Waste.DefaultMaxCustomerNum;
						Customer_Waste.DefaultCode = Customer_Waste.DefaultCurCustomerNum;
					}
					else if (OperType == 0)
					{
						Customer_Waste.CurKey = "CurSubScribNum";
						Customer_Waste.DefaultMaxNum = Customer_Waste.DefaultMaxSubScribNum;
						Customer_Waste.DefaultCode = Customer_Waste.DefaultCurSubScribNum;
					}
					else if (OperType == 2)
					{
						Customer_Waste.CurKey = "CurTeamSubScribNum";
						Customer_Waste.DefaultMaxNum = Customer_Waste.DefaultMaxTeamSubScribNum;
						Customer_Waste.DefaultCode = Customer_Waste.DefaultCurTeamSubScribNum;
					}
					else if (OperType == 3)
					{
						Customer_Waste.CurKey = "CurInternatSubScribNum";
						Customer_Waste.DefaultMaxNum = Customer_Waste.DefaultMaxInternatSubScribNum;
						Customer_Waste.DefaultCode = Customer_Waste.DefaultCurInternatSubScribNum;
					}
					if (string.IsNullOrEmpty(text5))
					{
						flag = false;
					}
					else
					{
						foreach (XmlNode xmlNode10 in xmlNodeList)
						{
							text6 = string.Empty;
							text7 = string.Empty;
							text = xmlNode10.Attributes["BeginIP"].Value.Trim();
							text2 = xmlNode10.Attributes["EndIP"].Value.Trim();
							if (text.Length == text2.Length && text.Length == 15)
							{
								text6 = text.Replace(".", string.Empty);
								text7 = text2.Replace(".", string.Empty);
							}
							else
							{
								string[] array3 = text.Split(new char[]
								{
									'.'
								});
								string[] array4 = text2.Split(new char[]
								{
									'.'
								});
								string[] array2 = array3;
								for (int i = 0; i < array2.Length; i++)
								{
									string text11 = array2[i];
									text6 += text11.PadLeft(3, '0');
								}
								array2 = array4;
								for (int i = 0; i < array2.Length; i++)
								{
									string text11 = array2[i];
									text7 += text11.PadLeft(3, '0');
								}
							}
							if (long.Parse(text5) >= long.Parse(text6) && long.Parse(text5) <= long.Parse(text7))
							{
								flag = true;
								text3 = xmlNode10.Attributes["DefaultHeaderCode"].Value.Trim();
								text4 = xmlNode10.Attributes[Customer_Waste.CurKey].Value.Trim();
								if (text4.Contains(text9))
								{
									string s = text4.Substring(text4.Length - 4);
									int num = 0;
									int.TryParse(s, out num);
									num++;
									if (OperType == 0)
									{
										if (Customer_Waste.IsCheckSubScrib == 1)
										{
										}
									}
									else if (num > Customer_Waste.DefaultMaxNum)
									{
										Customer_Waste.FileRWLocker.ReleaseWriterLock();
										result = string.Empty;
										return result;
									}
									text8 = string.Concat(new object[]
									{
										text3,
										Customer_Waste.DefaultCode,
										text9,
										num.ToString().PadLeft(4, '0')
									});
								}
								else
								{
									text8 = string.Concat(new object[]
									{
										text3,
										Customer_Waste.DefaultCode,
										text9,
										"0001"
									});
								}
								xmlNode10.Attributes[Customer_Waste.CurKey].Value = text8;
								break;
							}
						}
					}
					if (!flag)
					{
						XmlNode xmlNode10 = documentElement.SelectSingleNode("LostCustomerNum");
						text3 = xmlNode10.Attributes["DefaultHeaderCode"].Value.Trim();
						text4 = xmlNode10.Attributes[Customer_Waste.CurKey].Value.Trim();
						if (text4.Contains(text9))
						{
							string s = text4.Substring(text4.Length - 4);
							int num = 0;
							int.TryParse(s, out num);
							num++;
							if (OperType == 0)
							{
								if (Customer_Waste.IsCheckSubScrib == 1)
								{
								}
							}
							else if (num > Customer_Waste.DefaultMaxNum)
							{
								Customer_Waste.FileRWLocker.ReleaseWriterLock();
								result = string.Empty;
								return result;
							}
							text8 = string.Concat(new object[]
							{
								text3,
								Customer_Waste.DefaultCode,
								text9,
								num.ToString().PadLeft(4, '0')
							});
						}
						else
						{
							text8 = string.Concat(new object[]
							{
								text3,
								Customer_Waste.DefaultCode,
								text9,
								"0001"
							});
						}
						xmlNode10.Attributes[Customer_Waste.CurKey].Value = text8;
					}
					xmlDocument.Save(Customer_Waste._path);
					Customer_Waste.FileRWLocker.ReleaseWriterLock();
					Code128c = Code128.GetEncodedData(text8);
					result = text8;
				}
			}
			catch (System.Exception ex)
			{
				Customer_Waste.FileRWLocker.ReleaseWriterLock();
				Log4J.Instance.Info(string.Concat(new string[]
				{
					text10,
					",读取体检号错误(",
					Customer_Waste._path,
					")：",
					ex.Message
				}));
				result = Customer_Waste.CreateMaxNumX(ref Code128c, OperType);
			}
			return result;
		}

		public static string CreateMaxNumX(ref string Code128c, int OperType, DateTime SubScribDate, DataTable RegistSumData)
		{
			XmlDocument xmlDocument = new XmlDocument();
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			string text5 = string.Empty;
			string text6 = string.Empty;
			string text7 = string.Empty;
			string text8 = string.Empty;
			string text9 = string.Empty;
			string text10 = string.Empty;
			bool flag = false;
			text10 = Public.GetClientIP();
			string result;
			try
			{
				Customer_Waste.FileRWLocker.AcquireWriterLock(new System.TimeSpan(0, 0, 2));
				text9 = DateTime.Now.ToString("MMddyyyy");
				if (!Public.CheckIPV4(text10))
				{
					text10 = string.Empty;
					Log4J.Instance.Info(string.Concat(new string[]
					{
						"客户端IP:",
						text10,
						",UserHostName:",
						HttpContext.Current.Request.UserHostName.ToString(),
						",UserHostAddress:",
						HttpContext.Current.Request.UserHostAddress.ToString(),
						"不满足IPV4标准,系统已主动转换为缺损值!"
					}));
				}
				if (string.IsNullOrEmpty(text10))
				{
					text5 = string.Empty;
				}
				else if (text10.Length != 15)
				{
					string[] array = text10.Split(new char[]
					{
						'.'
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text11 = array2[i];
						text5 += text11.PadLeft(3, '0');
					}
				}
				else
				{
					text5 = text10.Replace(".", string.Empty);
				}
				xmlDocument.Load(Customer_Waste._path);
				if (!xmlDocument.HasChildNodes)
				{
					Customer_Waste.FileRWLocker.ReleaseWriterLock();
					Log4J.Instance.Info(text10 + ",读取体检号配置文件为空(" + Customer_Waste._path + ")");
					result = Customer_Waste.CreateMaxNumX(ref Code128c, OperType);
				}
				else
				{
					XmlElement documentElement = xmlDocument.DocumentElement;
					XmlNodeList xmlNodeList = documentElement.SelectNodes("CustomerNum");
					flag = false;
					try
					{
						XmlNode xmlNode = documentElement.SelectSingleNode("DefaultCurCustomerNum");
						XmlNode xmlNode2 = documentElement.SelectSingleNode("DefaultCurSubScribNum");
						XmlNode xmlNode3 = documentElement.SelectSingleNode("DefaultCurTeamSubScribNum");
						XmlNode xmlNode4 = documentElement.SelectSingleNode("DefaultMaxCustomerNum");
						XmlNode xmlNode5 = documentElement.SelectSingleNode("DefaultMaxSubScribNum");
						XmlNode xmlNode6 = documentElement.SelectSingleNode("DefaultMaxTeamSubScribNum");
						XmlNode xmlNode7 = documentElement.SelectSingleNode("IsCheckSubScrib");
						int.TryParse(xmlNode7.InnerText.Trim(), out Customer_Waste.IsCheckSubScrib);
						int.TryParse(xmlNode.InnerText.Trim(), out Customer_Waste.DefaultCurCustomerNum);
						int.TryParse(xmlNode2.InnerText.Trim(), out Customer_Waste.DefaultCurSubScribNum);
						int.TryParse(xmlNode3.InnerText.Trim(), out Customer_Waste.DefaultCurTeamSubScribNum);
						int.TryParse(xmlNode4.InnerText.Trim(), out Customer_Waste.DefaultMaxCustomerNum);
						int.TryParse(xmlNode5.InnerText.Trim(), out Customer_Waste.DefaultMaxSubScribNum);
						int.TryParse(xmlNode6.InnerText.Trim(), out Customer_Waste.DefaultMaxTeamSubScribNum);
						if (Customer_Waste.DefaultCurCustomerNum == -1)
						{
							Customer_Waste.DefaultCurCustomerNum = 3;
						}
						if (Customer_Waste.DefaultCurSubScribNum == -1)
						{
							Customer_Waste.DefaultCurSubScribNum = 6;
						}
						if (Customer_Waste.DefaultCurTeamSubScribNum == -1)
						{
							Customer_Waste.DefaultCurTeamSubScribNum = 9;
						}
					}
					catch (System.Exception ex)
					{
						Log4J.Instance.Info("体检号通用生成方法出现错误：" + ex.Message);
						Customer_Waste.DefaultCurCustomerNum = 3;
						Customer_Waste.DefaultCurSubScribNum = 6;
						Customer_Waste.DefaultCurTeamSubScribNum = 9;
						Customer_Waste.DefaultMaxCustomerNum = 9999;
						Customer_Waste.DefaultMaxSubScribNum = 9999;
						Customer_Waste.DefaultMaxTeamSubScribNum = 9999;
					}
					if (OperType == 0)
					{
						Customer_Waste.CurKey = "CurSubScribNum";
						Customer_Waste.DefaultMaxNum = Customer_Waste.DefaultMaxSubScribNum;
						Customer_Waste.DefaultCode = Customer_Waste.DefaultCurSubScribNum;
					}
					else if (OperType == 1)
					{
						Customer_Waste.CurKey = "CurCustomerNum";
						Customer_Waste.DefaultMaxNum = Customer_Waste.DefaultMaxCustomerNum;
						Customer_Waste.DefaultCode = Customer_Waste.DefaultCurCustomerNum;
					}
					else if (OperType == 2)
					{
						Customer_Waste.CurKey = "CurTeamSubScribNum";
						Customer_Waste.DefaultMaxNum = Customer_Waste.DefaultMaxTeamSubScribNum;
						Customer_Waste.DefaultCode = Customer_Waste.DefaultCurTeamSubScribNum;
					}
					if (string.IsNullOrEmpty(text5))
					{
						flag = false;
					}
					else
					{
						foreach (XmlNode xmlNode8 in xmlNodeList)
						{
							text6 = string.Empty;
							text7 = string.Empty;
							text = xmlNode8.Attributes["BeginIP"].Value.Trim();
							text2 = xmlNode8.Attributes["EndIP"].Value.Trim();
							if (text.Length == text2.Length && text.Length == 15)
							{
								text6 = text.Replace(".", string.Empty);
								text7 = text2.Replace(".", string.Empty);
							}
							else
							{
								string[] array3 = text.Split(new char[]
								{
									'.'
								});
								string[] array4 = text2.Split(new char[]
								{
									'.'
								});
								string[] array2 = array3;
								for (int i = 0; i < array2.Length; i++)
								{
									string text11 = array2[i];
									text6 += text11.PadLeft(3, '0');
								}
								array2 = array4;
								for (int i = 0; i < array2.Length; i++)
								{
									string text11 = array2[i];
									text7 += text11.PadLeft(3, '0');
								}
							}
							if (long.Parse(text5) >= long.Parse(text6) && long.Parse(text5) <= long.Parse(text7))
							{
								flag = true;
								text3 = xmlNode8.Attributes["DefaultHeaderCode"].Value.Trim();
								text4 = xmlNode8.Attributes[Customer_Waste.CurKey].Value.Trim();
								if (text4.Contains(text9))
								{
									string s = text4.Substring(text4.Length - 4);
									int num = 0;
									int.TryParse(s, out num);
									num++;
									if (Customer_Waste.IsCheckSubScrib == 1)
									{
										if (OperType == 1)
										{
											DataRow[] array5 = RegistSumData.Select(string.Concat(new object[]
											{
												"SubScribDate='",
												SubScribDate.ToString("yyyy-MM-dd"),
												"' AND Is_Subscribed='",
												OperType,
												"'"
											}));
											if (array5.Length > 0)
											{
												num = int.Parse(array5[0]["Num"].ToString()) + 1;
												if (num > Customer_Waste.DefaultMaxNum)
												{
													Customer_Waste.FileRWLocker.ReleaseWriterLock();
													result = string.Empty;
													return result;
												}
											}
										}
										else if (num >= Customer_Waste.DefaultMaxNum)
										{
											Customer_Waste.FileRWLocker.ReleaseWriterLock();
											result = string.Empty;
											return result;
										}
									}
									else if (OperType != 1)
									{
										if (num >= Customer_Waste.DefaultMaxNum)
										{
											Customer_Waste.FileRWLocker.ReleaseWriterLock();
											result = string.Empty;
											return result;
										}
									}
									text8 = string.Concat(new object[]
									{
										text3,
										Customer_Waste.DefaultCode,
										text9,
										num.ToString().PadLeft(4, '0')
									});
								}
								else
								{
									text8 = string.Concat(new object[]
									{
										text3,
										Customer_Waste.DefaultCode,
										text9,
										"0001"
									});
								}
								xmlNode8.Attributes[Customer_Waste.CurKey].Value = text8;
								break;
							}
						}
					}
					if (!flag)
					{
						XmlNode xmlNode8 = documentElement.SelectSingleNode("LostCustomerNum");
						text3 = xmlNode8.Attributes["DefaultHeaderCode"].Value.Trim();
						text4 = xmlNode8.Attributes[Customer_Waste.CurKey].Value.Trim();
						if (text4.Contains(text9))
						{
							string s = text4.Substring(text4.Length - 4);
							int num = 0;
							int.TryParse(s, out num);
							num++;
							if (Customer_Waste.IsCheckSubScrib == 1)
							{
								if (OperType == 1)
								{
									DataRow[] array5 = RegistSumData.Select(string.Concat(new object[]
									{
										"SubScribDate='",
										SubScribDate.ToString("yyyy-MM-dd"),
										"' AND Is_Subscribed='",
										OperType,
										"'"
									}));
									if (array5.Length > 0)
									{
										num = int.Parse(array5[0]["Num"].ToString()) + 1;
										if (num > Customer_Waste.DefaultMaxNum)
										{
											Customer_Waste.FileRWLocker.ReleaseWriterLock();
											result = string.Empty;
											return result;
										}
									}
								}
								else if (num >= Customer_Waste.DefaultMaxNum)
								{
									Customer_Waste.FileRWLocker.ReleaseWriterLock();
									result = string.Empty;
									return result;
								}
							}
							else if (OperType != 1)
							{
								if (num >= Customer_Waste.DefaultMaxNum)
								{
									Customer_Waste.FileRWLocker.ReleaseWriterLock();
									result = string.Empty;
									return result;
								}
							}
							text8 = string.Concat(new object[]
							{
								text3,
								Customer_Waste.DefaultCode,
								text9,
								num.ToString().PadLeft(4, '0')
							});
						}
						else
						{
							text8 = string.Concat(new object[]
							{
								text3,
								Customer_Waste.DefaultCode,
								text9,
								"0001"
							});
						}
						xmlNode8.Attributes[Customer_Waste.CurKey].Value = text8;
					}
					xmlDocument.Save(Customer_Waste._path);
					Customer_Waste.FileRWLocker.ReleaseWriterLock();
					Code128c = Code128.GetEncodedData(text8);
					result = text8;
				}
			}
			catch (System.Exception ex)
			{
				Customer_Waste.FileRWLocker.ReleaseWriterLock();
				Log4J.Instance.Info(string.Concat(new string[]
				{
					text10,
					",读取体检号错误(",
					Customer_Waste._path,
					")：",
					ex.Message
				}));
				result = Customer_Waste.CreateMaxNumX(ref Code128c, OperType);
			}
			return result;
		}

		public static string CreateMaxApplyID()
		{
			XmlDocument xmlDocument = new XmlDocument();
			string text = string.Empty;
			string result;
			try
			{
				Customer_Waste.FileRWLocker.AcquireWriterLock(new System.TimeSpan(0, 0, 2));
				xmlDocument.Load(Customer_Waste._path);
				if (!xmlDocument.HasChildNodes)
				{
					Customer_Waste.FileRWLocker.ReleaseWriterLock();
					Log4J.Instance.Info(Public.GetClientIP() + ",读取到申请号配置文件为空(" + Customer_Waste._path + ")");
					result = Customer_Waste.CreateMaxApplyID();
				}
				else
				{
					XmlElement documentElement = xmlDocument.DocumentElement;
					XmlNode xmlNode = documentElement.SelectSingleNode("ApplyID");
					string text2 = string.Empty;
					string text3 = DateTime.Now.ToString("yyMMdd");
					if (xmlNode != null)
					{
						text2 = xmlNode.Attributes["CurApplyIDNum"].Value.Trim();
					}
					if (!text2.Contains(text3) || string.IsNullOrEmpty(text2))
					{
						text = "0" + text3 + "000001505";
					}
					else
					{
						string s = text2.Substring(7, 6);
						int num = 1;
						int.TryParse(s, out num);
						num++;
						text = "0" + text3 + num.ToString().PadLeft(6, '0') + "505";
					}
					xmlNode.Attributes["CurApplyIDNum"].Value = text;
					xmlDocument.Save(Customer_Waste._path);
					Customer_Waste.FileRWLocker.ReleaseWriterLock();
					result = text;
				}
			}
			catch (System.Exception ex)
			{
				Customer_Waste.FileRWLocker.ReleaseWriterLock();
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",读取申请号错误(",
					Customer_Waste._path,
					")：",
					ex.Message
				}));
				result = Customer_Waste.CreateMaxApplyID();
			}
			return result;
		}
	}
}
