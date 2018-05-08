using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Web;
using System.Xml;

namespace PEIS.Common
{
	public class Customer
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

		internal static bool SaveFile(string filePath, string fileText)
		{
			bool result = false;
			lock (result.ToString())
			{
				try
				{
					StreamWriter streamWriter = new StreamWriter(filePath, false);
					streamWriter.Write(fileText);
					streamWriter.Close();
					streamWriter.Dispose();
					result = true;
				}
				catch (System.Exception ex)
				{
					Log4J.Instance.Info(string.Concat(new string[]
					{
						"客户端IP:",
						Public.GetClientIP(),
						",UserHostName:",
						HttpContext.Current.Request.UserHostName.ToString(),
						",UserHostAddress:",
						HttpContext.Current.Request.UserHostAddress.ToString(),
						" 加密基本配置文件失败，失败原因为:",
						ex.Message
					}));
					result = false;
				}
			}
			return result;
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
                result = ex.Message;
            }
			return result;
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
				Customer.FileRWLocker.AcquireWriterLock(new System.TimeSpan(0, 0, 2));
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
				try
				{
					if (SecretConfig.IsDataEncryption.ToLower() == "True".ToLower() || SecretConfig.IsDataEncryption.ToLower() == "1")
					{
						string xml = SecretCommon.AES.Decrypt(Customer.FileStreamReadFile(Customer._path));
						xmlDocument.LoadXml(xml);
					}
					else
					{
						xmlDocument.Load(Customer._path);
					}
				}
				catch (System.Exception ex)
				{
					xmlDocument.Load(Customer._path);
				}
				if (!xmlDocument.HasChildNodes)
				{
					Customer.FileRWLocker.ReleaseWriterLock();
					Log4J.Instance.Info(text10 + ",读取体检号配置文件为空(" + Customer._path + ")");
					result = Customer.CreateMaxNumX(ref Code128c, OperType);
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
						int.TryParse(xmlNode9.InnerText.Trim(), out Customer.IsCheckSubScrib);
						int.TryParse(xmlNode.InnerText.Trim(), out Customer.DefaultCurCustomerNum);
						int.TryParse(xmlNode2.InnerText.Trim(), out Customer.DefaultCurSubScribNum);
						int.TryParse(xmlNode3.InnerText.Trim(), out Customer.DefaultCurTeamSubScribNum);
						int.TryParse(xmlNode4.InnerText.Trim(), out Customer.DefaultMaxCustomerNum);
						int.TryParse(xmlNode5.InnerText.Trim(), out Customer.DefaultMaxSubScribNum);
						int.TryParse(xmlNode6.InnerText.Trim(), out Customer.DefaultMaxTeamSubScribNum);
						int.TryParse(xmlNode7.InnerText.Trim(), out Customer.DefaultCurInternatSubScribNum);
						int.TryParse(xmlNode8.InnerText.Trim(), out Customer.DefaultMaxInternatSubScribNum);
						if (Customer.DefaultCurCustomerNum == -1)
						{
							Customer.DefaultCurCustomerNum = 3;
						}
						if (Customer.DefaultCurSubScribNum == -1)
						{
							Customer.DefaultCurSubScribNum = 6;
						}
						if (Customer.DefaultCurTeamSubScribNum == -1)
						{
							Customer.DefaultCurTeamSubScribNum = 9;
						}
						if (Customer.DefaultCurInternatSubScribNum == -1)
						{
							Customer.DefaultCurInternatSubScribNum = 5;
						}
					}
					catch (System.Exception ex)
					{
						Log4J.Instance.Info("体检号通用生成方法出现错误：" + ex.Message);
						Customer.DefaultCurCustomerNum = 3;
						Customer.DefaultCurSubScribNum = 6;
						Customer.DefaultCurTeamSubScribNum = 9;
						Customer.DefaultMaxCustomerNum = 9999;
						Customer.DefaultMaxSubScribNum = 9999;
						Customer.DefaultMaxTeamSubScribNum = 9999;
						Customer.DefaultCurInternatSubScribNum = 5;
						Customer.DefaultMaxInternatSubScribNum = 9999;
					}
					if (OperType == 1)
					{
						Customer.CurKey = "CurCustomerNum";
						Customer.DefaultMaxNum = Customer.DefaultMaxCustomerNum;
						Customer.DefaultCode = Customer.DefaultCurCustomerNum;
					}
					else if (OperType == 0)
					{
						Customer.CurKey = "CurSubScribNum";
						Customer.DefaultMaxNum = Customer.DefaultMaxSubScribNum;
						Customer.DefaultCode = Customer.DefaultCurSubScribNum;
					}
					else if (OperType == 2)
					{
						Customer.CurKey = "CurTeamSubScribNum";
						Customer.DefaultMaxNum = Customer.DefaultMaxTeamSubScribNum;
						Customer.DefaultCode = Customer.DefaultCurTeamSubScribNum;
					}
					else if (OperType == 3)
					{
						Customer.CurKey = "CurInternatSubScribNum";
						Customer.DefaultMaxNum = Customer.DefaultMaxInternatSubScribNum;
						Customer.DefaultCode = Customer.DefaultCurInternatSubScribNum;
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
								text4 = xmlNode10.Attributes[Customer.CurKey].Value.Trim();
								if (text4.Contains(text9))
								{
									string s = text4.Substring(text4.Length - 4);
									int num = 0;
									int.TryParse(s, out num);
									num++;
									if (OperType == 0)
									{
										if (Customer.IsCheckSubScrib == 1)
										{
										}
									}
									else if (num > Customer.DefaultMaxNum)
									{
										Customer.FileRWLocker.ReleaseWriterLock();
										result = string.Empty;
										return result;
									}
									text8 = string.Concat(new object[]
									{
										text3,
										Customer.DefaultCode,
										text9,
										num.ToString().PadLeft(4, '0')
									});
								}
								else
								{
									text8 = string.Concat(new object[]
									{
										text3,
										Customer.DefaultCode,
										text9,
										"0001"
									});
								}
								xmlNode10.Attributes[Customer.CurKey].Value = text8;
								break;
							}
						}
					}
					if (!flag)
					{
						XmlNode xmlNode10 = documentElement.SelectSingleNode("LostCustomerNum");
						text3 = xmlNode10.Attributes["DefaultHeaderCode"].Value.Trim();
						text4 = xmlNode10.Attributes[Customer.CurKey].Value.Trim();
						if (text4.Contains(text9))
						{
							string s = text4.Substring(text4.Length - 4);
							int num = 0;
							int.TryParse(s, out num);
							num++;
							if (OperType == 0)
							{
								if (Customer.IsCheckSubScrib == 1)
								{
								}
							}
							else if (num > Customer.DefaultMaxNum)
							{
								Customer.FileRWLocker.ReleaseWriterLock();
								result = string.Empty;
								return result;
							}
							text8 = string.Concat(new object[]
							{
								text3,
								Customer.DefaultCode,
								text9,
								num.ToString().PadLeft(4, '0')
							});
						}
						else
						{
							text8 = string.Concat(new object[]
							{
								text3,
								Customer.DefaultCode,
								text9,
								"0001"
							});
						}
						xmlNode10.Attributes[Customer.CurKey].Value = text8;
					}
					if (SecretConfig.IsDataEncryption.ToLower() == "True".ToLower() || SecretConfig.IsDataEncryption.ToLower() == "1")
					{
						Customer.SaveFile(Customer._path, Secret.AES.Encrypt(xmlDocument.InnerXml));
					}
					else
					{
						xmlDocument.Save(Customer._path);
					}
					Customer.FileRWLocker.ReleaseWriterLock();
					Code128c = Code128.GetEncodedData(text8);
					result = text8;
				}
			}
			catch (System.Exception ex)
			{
				Customer.FileRWLocker.ReleaseWriterLock();
				Log4J.Instance.Info(string.Concat(new string[]
				{
					text10,
					",读取体检号错误(",
					Customer._path,
					")：",
					ex.Message
				}));
				result = Customer.CreateMaxNumX(ref Code128c, OperType);
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
				Customer.FileRWLocker.AcquireWriterLock(new System.TimeSpan(0, 0, 2));
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
				try
				{
					if (SecretConfig.IsDataEncryption.ToLower() == "True".ToLower() || SecretConfig.IsDataEncryption.ToLower() == "1")
					{
						string xml = SecretCommon.AES.Decrypt(Customer.FileStreamReadFile(Customer._path));
						xmlDocument.LoadXml(xml);
					}
					else
					{
						xmlDocument.Load(Customer._path);
					}
				}
				catch (System.Exception ex)
				{
					xmlDocument.Load(Customer._path);
				}
				if (!xmlDocument.HasChildNodes)
				{
					Customer.FileRWLocker.ReleaseWriterLock();
					Log4J.Instance.Info(text10 + ",读取体检号配置文件为空(" + Customer._path + ")");
					result = Customer.CreateMaxNumX(ref Code128c, OperType);
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
						int.TryParse(xmlNode7.InnerText.Trim(), out Customer.IsCheckSubScrib);
						int.TryParse(xmlNode.InnerText.Trim(), out Customer.DefaultCurCustomerNum);
						int.TryParse(xmlNode2.InnerText.Trim(), out Customer.DefaultCurSubScribNum);
						int.TryParse(xmlNode3.InnerText.Trim(), out Customer.DefaultCurTeamSubScribNum);
						int.TryParse(xmlNode4.InnerText.Trim(), out Customer.DefaultMaxCustomerNum);
						int.TryParse(xmlNode5.InnerText.Trim(), out Customer.DefaultMaxSubScribNum);
						int.TryParse(xmlNode6.InnerText.Trim(), out Customer.DefaultMaxTeamSubScribNum);
						if (Customer.DefaultCurCustomerNum == -1)
						{
							Customer.DefaultCurCustomerNum = 3;
						}
						if (Customer.DefaultCurSubScribNum == -1)
						{
							Customer.DefaultCurSubScribNum = 6;
						}
						if (Customer.DefaultCurTeamSubScribNum == -1)
						{
							Customer.DefaultCurTeamSubScribNum = 9;
						}
					}
					catch (System.Exception ex)
					{
						Log4J.Instance.Info("体检号通用生成方法出现错误：" + ex.Message);
						Customer.DefaultCurCustomerNum = 3;
						Customer.DefaultCurSubScribNum = 6;
						Customer.DefaultCurTeamSubScribNum = 9;
						Customer.DefaultMaxCustomerNum = 9999;
						Customer.DefaultMaxSubScribNum = 9999;
						Customer.DefaultMaxTeamSubScribNum = 9999;
					}
					if (OperType == 0)
					{
						Customer.CurKey = "CurSubScribNum";
						Customer.DefaultMaxNum = Customer.DefaultMaxSubScribNum;
						Customer.DefaultCode = Customer.DefaultCurSubScribNum;
					}
					else if (OperType == 1)
					{
						Customer.CurKey = "CurCustomerNum";
						Customer.DefaultMaxNum = Customer.DefaultMaxCustomerNum;
						Customer.DefaultCode = Customer.DefaultCurCustomerNum;
					}
					else if (OperType == 2)
					{
						Customer.CurKey = "CurTeamSubScribNum";
						Customer.DefaultMaxNum = Customer.DefaultMaxTeamSubScribNum;
						Customer.DefaultCode = Customer.DefaultCurTeamSubScribNum;
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
								text4 = xmlNode8.Attributes[Customer.CurKey].Value.Trim();
								if (text4.Contains(text9))
								{
									string s = text4.Substring(text4.Length - 4);
									int num = 0;
									int.TryParse(s, out num);
									num++;
									if (Customer.IsCheckSubScrib == 1)
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
												if (num > Customer.DefaultMaxNum)
												{
													Customer.FileRWLocker.ReleaseWriterLock();
													result = string.Empty;
													return result;
												}
											}
										}
										else if (num >= Customer.DefaultMaxNum)
										{
											Customer.FileRWLocker.ReleaseWriterLock();
											result = string.Empty;
											return result;
										}
									}
									else if (OperType != 1)
									{
										if (num >= Customer.DefaultMaxNum)
										{
											Customer.FileRWLocker.ReleaseWriterLock();
											result = string.Empty;
											return result;
										}
									}
									text8 = string.Concat(new object[]
									{
										text3,
										Customer.DefaultCode,
										text9,
										num.ToString().PadLeft(4, '0')
									});
								}
								else
								{
									text8 = string.Concat(new object[]
									{
										text3,
										Customer.DefaultCode,
										text9,
										"0001"
									});
								}
								xmlNode8.Attributes[Customer.CurKey].Value = text8;
								break;
							}
						}
					}
					if (!flag)
					{
						XmlNode xmlNode8 = documentElement.SelectSingleNode("LostCustomerNum");
						text3 = xmlNode8.Attributes["DefaultHeaderCode"].Value.Trim();
						text4 = xmlNode8.Attributes[Customer.CurKey].Value.Trim();
						if (text4.Contains(text9))
						{
							string s = text4.Substring(text4.Length - 4);
							int num = 0;
							int.TryParse(s, out num);
							num++;
							if (Customer.IsCheckSubScrib == 1)
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
										if (num > Customer.DefaultMaxNum)
										{
											Customer.FileRWLocker.ReleaseWriterLock();
											result = string.Empty;
											return result;
										}
									}
								}
								else if (num >= Customer.DefaultMaxNum)
								{
									Customer.FileRWLocker.ReleaseWriterLock();
									result = string.Empty;
									return result;
								}
							}
							else if (OperType != 1)
							{
								if (num >= Customer.DefaultMaxNum)
								{
									Customer.FileRWLocker.ReleaseWriterLock();
									result = string.Empty;
									return result;
								}
							}
							text8 = string.Concat(new object[]
							{
								text3,
								Customer.DefaultCode,
								text9,
								num.ToString().PadLeft(4, '0')
							});
						}
						else
						{
							text8 = string.Concat(new object[]
							{
								text3,
								Customer.DefaultCode,
								text9,
								"0001"
							});
						}
						xmlNode8.Attributes[Customer.CurKey].Value = text8;
					}
					if (SecretConfig.IsDataEncryption.ToLower() == "True".ToLower() || SecretConfig.IsDataEncryption.ToLower() == "1")
					{
						Customer.SaveFile(Customer._path, Secret.AES.Encrypt(xmlDocument.InnerXml));
					}
					else
					{
						xmlDocument.Save(Customer._path);
					}
					Customer.FileRWLocker.ReleaseWriterLock();
					Code128c = Code128.GetEncodedData(text8);
					result = text8;
				}
			}
			catch (System.Exception ex)
			{
				Customer.FileRWLocker.ReleaseWriterLock();
				Log4J.Instance.Info(string.Concat(new string[]
				{
					text10,
					",读取体检号错误(",
					Customer._path,
					")：",
					ex.Message
				}));
				result = Customer.CreateMaxNumX(ref Code128c, OperType);
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
				Customer.FileRWLocker.AcquireWriterLock(new System.TimeSpan(0, 0, 2));
				try
				{
					if (SecretConfig.IsDataEncryption.ToLower() == "True".ToLower() || SecretConfig.IsDataEncryption.ToLower() == "1")
					{
						string xml = SecretCommon.AES.Decrypt(Customer.FileStreamReadFile(Customer._path));
						xmlDocument.LoadXml(xml);
					}
					else
					{
						xmlDocument.Load(Customer._path);
					}
				}
				catch (System.Exception ex)
				{
					xmlDocument.Load(Customer._path);
				}
				if (!xmlDocument.HasChildNodes)
				{
					Customer.FileRWLocker.ReleaseWriterLock();
					Log4J.Instance.Info(Public.GetClientIP() + ",读取到申请号配置文件为空(" + Customer._path + ")");
					result = Customer.CreateMaxApplyID();
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
					if (SecretConfig.IsDataEncryption.ToLower() == "True".ToLower() || SecretConfig.IsDataEncryption.ToLower() == "1")
					{
						Customer.SaveFile(Customer._path, Secret.AES.Encrypt(xmlDocument.InnerXml));
					}
					else
					{
						xmlDocument.Save(Customer._path);
					}
					Customer.FileRWLocker.ReleaseWriterLock();
					result = text;
				}
			}
			catch (System.Exception ex)
			{
				Customer.FileRWLocker.ReleaseWriterLock();
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",读取申请号错误(",
					Customer._path,
					")：",
					ex.Message
				}));
				result = Customer.CreateMaxApplyID();
			}
			return result;
		}
	}
}
