using System;
using System.Text;

namespace PEIS.Common
{
	internal static class Code128
	{
		public static int GetAsc(string charater)
		{
			int result;
			if (charater.Length == 1)
			{
				System.Text.ASCIIEncoding aSCIIEncoding = new System.Text.ASCIIEncoding();
				int num = (int)aSCIIEncoding.GetBytes(charater)[0];
				result = num;
			}
			else
			{
				result = int.Parse(charater);
			}
			return result;
		}

		public static string GetUniCode(int charater)
		{
			return Convert.ToChar(charater).ToString();
		}

		public static string GetEncodedDataX(string InputData)
		{
			string empty = string.Empty;
			int num = 104;
			for (int i = 0; i < InputData.Length; i++)
			{
				if (InputData[i] > ' ')
				{
					num += (int)(InputData[i] - ' ') * (i + 1);
				}
				else
				{
					num += (int)(InputData[i] + '@') * (i + 1);
				}
			}
			num %= 103;
			if (num < 95)
			{
				num += 32;
			}
			else
			{
				num += 100;
			}
			object obj = empty;
			return string.Concat(new object[]
			{
				obj,
				Convert.ToChar(204),
				InputData.Trim(),
				Convert.ToChar(num),
				Convert.ToChar(206)
			});
		}

		public static string GetEncodedData(string rawData)
		{
			return Code128.GetEncodedDataX(rawData);
		}

		internal static byte GetSIndexFromA(char c)
		{
			byte b = (byte)c;
			if (b < 32)
			{
				b += 64;
			}
			else
			{
				if (b >= 96)
				{
					throw new System.NotImplementedException();
				}
				b -= 32;
			}
			return b;
		}
	}
}
