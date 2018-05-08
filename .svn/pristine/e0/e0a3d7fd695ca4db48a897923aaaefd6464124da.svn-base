using System;
using System.Threading;

namespace PEIS.Common
{
	public class Rand
	{
		private static char[] Pattern = new char[]
		{
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'J',
			'K',
			'M',
			'N',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'j',
			'k',
			'm',
			'n',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z'
		};

		private static string[] PatternColor = new string[]
		{
			"800000",
			"B22222",
			"DC143C",
			"FF0000",
			"C71585",
			"D87093",
			"FF00FF",
			"4B0082",
			"9932CC",
			"6A5ACD",
			"BA55D3",
			"D8BFD8",
			"E6E6FA",
			"8B4513",
			"D2691E",
			"FFA07A",
			"F4A460",
			"FFD700",
			"BDB76B",
			"2F4F4F",
			"006400",
			"32CD32",
			"66CDAA",
			"00FA9A",
			"0000CD",
			"00BFFF",
			"000000",
			"483D8B",
			"4169E1",
			"808000"
		};

		public static string Number(int Length)
		{
			return Rand.Number(Length, false);
		}

		public static string Number(int Length, bool Sleep)
		{
			if (Sleep)
			{
				System.Threading.Thread.Sleep(3);
			}
			string text = "";
			System.Random random = new System.Random();
			for (int i = 0; i < Length; i++)
			{
				text += random.Next(10).ToString();
			}
			return text;
		}

		public static string Str(int Length)
		{
			return Rand.Str(Length, false);
		}

		public static string Str(int Length, bool Sleep)
		{
			if (Sleep)
			{
				System.Threading.Thread.Sleep(3);
			}
			string text = "";
			int maxValue = Rand.Pattern.Length;
			System.Random random = new System.Random(~(int)DateTime.Now.Ticks);
			for (int i = 0; i < Length; i++)
			{
				int num = random.Next(0, maxValue);
				text += Rand.Pattern[num];
			}
			return text;
		}

		public static string Str_char(int Length)
		{
			return Rand.Str_char(Length, false);
		}

		public static string Str_char(int Length, bool Sleep)
		{
			if (Sleep)
			{
				System.Threading.Thread.Sleep(3);
			}
			string text = "";
			int maxValue = Rand.Pattern.Length;
			System.Random random = new System.Random(~(int)DateTime.Now.Ticks);
			for (int i = 0; i < Length; i++)
			{
				int num = random.Next(10, maxValue);
				text += Rand.Pattern[num];
			}
			return text;
		}

		public static string Str_Color(bool Sleep)
		{
			if (Sleep)
			{
				System.Threading.Thread.Sleep(3);
			}
			int maxValue = Rand.PatternColor.Length;
			System.Random random = new System.Random(~(int)DateTime.Now.Ticks);
			int num = random.Next(0, maxValue);
			return Rand.PatternColor[num];
		}

		public static byte[] Bytes(int Length)
		{
			return Rand.Bytes(Length, false);
		}

		public static byte[] Bytes(int Length, bool Sleep)
		{
			if (Sleep)
			{
				System.Threading.Thread.Sleep(3);
			}
			byte[] array = new byte[Length];
			System.Random random = new System.Random();
			random.NextBytes(array);
			return array;
		}
	}
}
