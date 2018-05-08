using System;
using System.Collections.Generic;

namespace PEIS.Common
{
	public class LicenceServer
	{
		public static LicenceModel Licence;

		private static int SpearCount = 4;

		public static int[] intCode = new int[127];

		public static int[] intNumber;

		public static char[] Charcode;

		private List<IPConfigModel> IPConfigModelList = null;

		public static string GetDiskID()
		{
			return Computer.GetDiskID();
		}

		public static string GetCpuID()
		{
			return Computer.GetCpuID();
		}

		public static string GetGuid()
		{
			return LicenceServer.GetCpuID() + LicenceServer.GetDiskID();
		}

		public static string GetMachineCode(bool IsMD5)
		{
			string guid = LicenceServer.GetGuid();
			string text = guid.Substring(0, 24);
			char[] array = text.ToCharArray();
			int length = text.Length;
			string text2 = string.Empty;
			for (int i = 0; i <= length; i += LicenceServer.SpearCount)
			{
				if (i + LicenceServer.SpearCount <= length)
				{
					text2 = text2 + text.Substring(i, LicenceServer.SpearCount) + "-";
				}
			}
			string result;
			if (IsMD5)
			{
				result = MD5X.MD5(text2.TrimEnd(new char[]
				{
					'-'
				})).ToUpper();
			}
			else
			{
				result = text2.TrimEnd(new char[]
				{
					'-'
				}).ToUpper();
			}
			return result;
		}

		public static void SetIntCode()
		{
			for (int i = 1; i < LicenceServer.intCode.Length; i++)
			{
				LicenceServer.intCode[i] = i % 9;
			}
		}

		public static string GetRunLicenceCode(LicenceModel lm)
		{
			LicenceServer.SetIntCode();
			string text = string.Concat(new object[]
			{
				lm.CustomerCode,
				lm.RegisteDate,
				lm.UseDays.ToString(),
				lm.ConnectCount,
				lm.MachineCode
			});
			int length = text.Length;
			LicenceServer.intNumber = new int[length];
			LicenceServer.Charcode = new char[length];
			for (int i = 1; i < LicenceServer.Charcode.Length; i++)
			{
				LicenceServer.Charcode[i] = Convert.ToChar(text.Substring(i - 1, 1));
			}
			for (int j = 1; j < LicenceServer.intNumber.Length; j++)
			{
				LicenceServer.intNumber[j] = LicenceServer.intCode[Convert.ToInt32(LicenceServer.Charcode[j])] + Convert.ToInt32(LicenceServer.Charcode[j]);
			}
			string text2 = "";
			for (int j = 1; j < LicenceServer.intNumber.Length; j++)
			{
				if (LicenceServer.intNumber[j] >= 48 && LicenceServer.intNumber[j] <= 57)
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j]).ToString();
				}
				else if (LicenceServer.intNumber[j] >= 65 && LicenceServer.intNumber[j] <= 90)
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j]).ToString();
				}
				else if (LicenceServer.intNumber[j] >= 97 && LicenceServer.intNumber[j] <= 122)
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j]).ToString();
				}
				else if (LicenceServer.intNumber[j] > 122)
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j] - 10).ToString();
				}
				else
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j] - 9).ToString();
				}
			}
			return text2;
		}

		public static string GetRunLicenceCode(LicenceModel lm, ref string FilePath)
		{
			FilePath = string.Empty;
			LicenceServer.SetIntCode();
			string text = string.Concat(new object[]
			{
				lm.CustomerCode,
				lm.RegisteDate,
				lm.UseDays.ToString(),
				lm.ConnectCount,
				lm.MachineCode
			});
			int length = text.Length;
			LicenceServer.intNumber = new int[length];
			LicenceServer.Charcode = new char[length];
			for (int i = 1; i < LicenceServer.Charcode.Length; i++)
			{
				LicenceServer.Charcode[i] = Convert.ToChar(text.Substring(i - 1, 1));
			}
			for (int j = 1; j < LicenceServer.intNumber.Length; j++)
			{
				LicenceServer.intNumber[j] = LicenceServer.intCode[Convert.ToInt32(LicenceServer.Charcode[j])] + Convert.ToInt32(LicenceServer.Charcode[j]);
			}
			string text2 = "";
			for (int j = 1; j < LicenceServer.intNumber.Length; j++)
			{
				if (LicenceServer.intNumber[j] >= 48 && LicenceServer.intNumber[j] <= 57)
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j]).ToString();
				}
				else if (LicenceServer.intNumber[j] >= 65 && LicenceServer.intNumber[j] <= 90)
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j]).ToString();
				}
				else if (LicenceServer.intNumber[j] >= 97 && LicenceServer.intNumber[j] <= 122)
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j]).ToString();
				}
				else if (LicenceServer.intNumber[j] > 122)
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j] - 10).ToString();
				}
				else
				{
					text2 += Convert.ToChar(LicenceServer.intNumber[j] - 9).ToString();
				}
			}
			lm.LinceCode = text2;
			FilePath = LicenceConfig.SaveLicence(lm);
			LicenceServer.Licence = lm;
			return lm.LinceCode;
		}

		public void WriteLicence()
		{
		}

		public static bool IsCanUseIP(string IP, string FilePath)
		{
			string empty = string.Empty;
			List<IPConfigModel> iPServer = IPConfig.GetIPServer(FilePath, ref empty);
			bool result;
			if (iPServer.Count == 0)
			{
				result = true;
			}
			else
			{
				string text = string.Empty;
				string text2 = string.Empty;
				string text3 = string.Empty;
				foreach (IPConfigModel current in iPServer)
				{
					text = string.Empty;
					text2 = string.Empty;
					text3 = string.Empty;
					string[] array = IP.Split(new char[]
					{
						'.'
					});
					string[] array2 = current.BeginIP.Split(new char[]
					{
						'.'
					});
					string[] array3 = current.EndIP.Split(new char[]
					{
						'.'
					});
					string[] array4 = array;
					for (int i = 0; i < array4.Length; i++)
					{
						string text4 = array4[i];
						text += text4.PadRight(3, '0');
					}
					array4 = array2;
					for (int i = 0; i < array4.Length; i++)
					{
						string text4 = array4[i];
						text2 += text4.PadRight(3, '0');
					}
					array4 = array3;
					for (int i = 0; i < array4.Length; i++)
					{
						string text4 = array4[i];
						text3 += text4.PadRight(3, '0');
					}
					if ((long.Parse(text) >= long.Parse(text2) && long.Parse(text) <= long.Parse(text3)) || (long.Parse(text) <= long.Parse(text2) && long.Parse(text) >= long.Parse(text3)))
					{
						result = true;
						return result;
					}
				}
				result = false;
			}
			return result;
		}
	}
}
