using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace PEIS.Common
{
	public class LicenceConfig
	{
		private static string DesPwd = DES.DesPwd;

		private static int _UseDays = 0;

		public static string ConfigName = LicenceConfig.GetAssemblyLocation("LicenceSetting.xml");

		public static string LicenceConfigName = LicenceConfig.GetAssemblyLocation("Licence.Licence");

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

		public static string GetAssemblyLocationFileFolder()
		{
			return System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\"));
		}

		public static string CreateAssemblyLocationFile(string FileName)
		{
			string str = System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, System.Reflection.Assembly.GetExecutingAssembly().Location.LastIndexOf("\\"));
			FileName = str + "\\" + FileName;
			if (!File.Exists(FileName))
			{
				FileStream fileStream = File.Create(FileName);
				fileStream.Close();
				fileStream.Dispose();
			}
			return FileName;
		}

		public static void SetLicence(string NodeName, string Value)
		{
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.Load(LicenceConfig.ConfigName);
				xmlDocument.GetElementsByTagName(NodeName)[0].InnerXml = Value;
				xmlDocument.Save(LicenceConfig.ConfigName);
			}
			catch
			{
			}
		}

		public static LicenceModel ReadLicence(string FilePath, string RegesiterMachineCode, ref string ErrorMessage)
		{
			LicenceModel licenceModel = new LicenceModel();
			ErrorMessage = string.Empty;
			LicenceModel result;
			if (!File.Exists(FilePath))
			{
				ErrorMessage = "未找到许可文件！";
				result = licenceModel;
			}
			else
			{
				FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.None);
				StreamReader streamReader = new StreamReader(fileStream);
				string text = streamReader.ReadLine();
				string text2 = streamReader.ReadLine();
				string machineCode = streamReader.ReadLine();
				string text3 = streamReader.ReadLine();
				string text4 = streamReader.ReadLine();
				string text5 = streamReader.ReadLine();
				string text6 = streamReader.ReadLine();
				streamReader.Close();
				streamReader.Dispose();
				fileStream.Close();
				fileStream.Dispose();
				text = DES.Instance(LicenceConfig.DesPwd).DecryptString(text);
				text2 = DES.Instance(LicenceConfig.DesPwd).DecryptString(text2);
				text3 = DES.Instance(LicenceConfig.DesPwd).DecryptString(text3);
				text4 = DES.Instance(LicenceConfig.DesPwd).DecryptString(text4);
				text5 = DES.Instance(LicenceConfig.DesPwd).DecryptString(text5);
				licenceModel.CustomerName = text;
				licenceModel.CustomerCode = text2;
				licenceModel.MachineCode = machineCode;
				licenceModel.RegisteDate = text3;
				licenceModel.UseDays = ((text4 == string.Empty) ? "0" : text4);
				licenceModel.ConnectCount = ((text5 == string.Empty) ? 0 : int.Parse(text5));
				licenceModel.LinceCode = text6;
				if (!string.IsNullOrEmpty(RegesiterMachineCode))
				{
					if (RegesiterMachineCode != licenceModel.MachineCode)
					{
						ErrorMessage = "机器码识别失败，许可不可用，如需继续使用，请和我们取得联系！";
					}
				}
				else if (LicenceServer.GetMachineCode(true) != licenceModel.MachineCode)
				{
					ErrorMessage = "机器码识别失败，许可不可用，如需继续使用，请和我们取得联系！";
				}
				if (LicenceServer.GetRunLicenceCode(licenceModel) == text6)
				{
					DateTime t = DateTime.Parse(text3);
					DateTime t2 = t.AddDays((double)int.Parse(licenceModel.UseDays.ToString()));
					if (t2 < DateTime.Now)
					{
						ErrorMessage = "许可已过期";
						LicenceConfig.PauseLicence(FilePath, licenceModel);
					}
					if (t > DateTime.Now)
					{
						ErrorMessage = "注册时间被篡改，许可不可用，如需继续使用，请和我们取得联系！";
					}
				}
				else
				{
					ErrorMessage = "许可文件被篡改，许可不可用，如需继续使用，请和我们取得联系！";
				}
				result = licenceModel;
			}
			return result;
		}

		public static void PauseLicence(string FilePath, LicenceModel lm)
		{
			try
			{
				int num = new System.Random().Next(0, lm.MachineCode.Length - 5);
				lm.UseDays = 0;
				FileStream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Write, FileShare.None);
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.CustomerName));
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.CustomerCode));
				streamWriter.WriteLine(lm.MachineCode);
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.RegisteDate));
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.UseDays.ToString()));
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.ConnectCount.ToString()));
				streamWriter.WriteLine(lm.LinceCode);
				streamWriter.Close();
				streamWriter.Dispose();
				fileStream.Close();
				fileStream.Dispose();
			}
			catch
			{
			}
		}

		public static LicenceModel ReadLicence(ref string ErrorMessage)
		{
			LicenceModel licenceModel = new LicenceModel();
			ErrorMessage = string.Empty;
			LicenceModel result;
			if (!File.Exists(LicenceConfig.LicenceConfigName))
			{
				ErrorMessage = "未找到许可文件！";
				result = licenceModel;
			}
			else
			{
				FileStream fileStream = new FileStream(LicenceConfig.LicenceConfigName, FileMode.Open, FileAccess.Read, FileShare.None);
				StreamReader streamReader = new StreamReader(fileStream);
				string text = streamReader.ReadLine();
				string text2 = streamReader.ReadLine();
				string machineCode = streamReader.ReadLine();
				string text3 = streamReader.ReadLine();
				string text4 = streamReader.ReadLine();
				string text5 = streamReader.ReadLine();
				string text6 = streamReader.ReadLine();
				streamReader.Close();
				streamReader.Dispose();
				fileStream.Close();
				fileStream.Dispose();
				text = DES.Instance(LicenceConfig.DesPwd).DecryptString(text);
				text2 = DES.Instance(LicenceConfig.DesPwd).DecryptString(text2);
				text3 = DES.Instance(LicenceConfig.DesPwd).DecryptString(text3);
				text4 = DES.Instance(LicenceConfig.DesPwd).DecryptString(text4);
				text5 = DES.Instance(LicenceConfig.DesPwd).DecryptString(text5);
				licenceModel.CustomerName = text;
				licenceModel.CustomerCode = text2;
				licenceModel.MachineCode = machineCode;
				licenceModel.RegisteDate = text3;
				licenceModel.UseDays = ((text4 == string.Empty) ? "0" : text4);
				licenceModel.ConnectCount = ((text5 == string.Empty) ? 0 : int.Parse(text5));
				licenceModel.LinceCode = text6;
				if (LicenceServer.GetMachineCode(true) != licenceModel.MachineCode)
				{
					ErrorMessage = "机器码识别失败，许可不可用，如需继续使用，请和我们取得联系！";
				}
				if (LicenceServer.GetRunLicenceCode(licenceModel) == text6)
				{
					DateTime t = DateTime.Parse(text3);
					DateTime t2 = t.AddDays((double)int.Parse(licenceModel.UseDays.ToString()));
					if (t2 < DateTime.Now)
					{
						ErrorMessage = "许可已过期，如需继续使用，请和我们取得联系！";
					}
					if (t > DateTime.Now)
					{
						ErrorMessage = "注册时间被篡改，许可不可用，如需继续使用，请和我们取得联系！";
					}
				}
				else
				{
					ErrorMessage = "许可文件被篡改，许可不可用，如需继续使用，请和我们取得联系！";
				}
				result = licenceModel;
			}
			return result;
		}

		public static string SaveLicence(LicenceModel lm)
		{
			string text = "Licence.Licence";
			text = LicenceConfig.CreateAssemblyLocationFile(text);
			try
			{
				FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Write, FileShare.None);
				StreamWriter streamWriter = new StreamWriter(fileStream);
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.CustomerName));
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.CustomerCode));
				streamWriter.WriteLine(lm.MachineCode);
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.RegisteDate));
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.UseDays.ToString()));
				streamWriter.WriteLine(DES.Instance(LicenceConfig.DesPwd).EncryptString(lm.ConnectCount.ToString()));
				streamWriter.WriteLine(lm.LinceCode);
				streamWriter.Close();
				streamWriter.Dispose();
				fileStream.Close();
				fileStream.Dispose();
			}
			catch
			{
			}
			return text;
		}
	}
}
