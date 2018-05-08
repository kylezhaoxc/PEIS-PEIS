using PEIS.Common;
using PEIS.Model;
using System;

namespace PEIS.SQLServerDAL
{
	internal class Util
	{
		public static string GetStrByType(SqlConditionInfo info)
		{
			TypeCode paramType = info.ParamType;
			string result;
			if (paramType != TypeCode.Char)
			{
				switch (paramType)
				{
				case TypeCode.DateTime:
					result = "'" + info.ParamValue + "'";
					return result;
				case TypeCode.String:
					goto IL_3B;
				}
				result = info.ParamValue.ToString();
				return result;
			}
			IL_3B:
			result = "'" + Input.Filter((string)info.ParamValue) + "'";
			return result;
		}

		public static string GetStr(SqlConditionInfo info)
		{
			string result;
			switch (info.Blur)
			{
			case 1:
				result = "'%" + info.ParamValue + "'";
				break;
			case 2:
				result = "'" + info.ParamValue + "%'";
				break;
			case 3:
				result = "'%" + info.ParamValue + "%'";
				break;
			default:
				result = Util.GetStrByType(info);
				break;
			}
			return result;
		}

		public static string GetConvertParam2Where(SqlConditionInfo con)
		{
			string text = string.Empty;
			if (con.Blur == 1 || con.Blur == 2 || con.Blur == 3)
			{
				text = string.Concat(new string[]
				{
					text,
					" and ",
					con.ParamName.Replace("@", ""),
					" like ",
					con.ParamName
				});
			}
			else if (!string.IsNullOrEmpty(con.ParamOper))
			{
				if (con.ParamType == TypeCode.DateTime)
				{
					text = string.Concat(new string[]
					{
						text,
						" and datediff( day,'",
						con.ParamValue.ToString().Replace("上午", "").Replace("下午", ""),
						"',",
						con.ParamName.Replace("@", ""),
						" ) ",
						con.ParamOper,
						" 0 "
					});
				}
				else if (con.ParamType == TypeCode.Decimal || con.ParamType == TypeCode.Double || con.ParamType == TypeCode.Int16 || con.ParamType == TypeCode.Int32 || con.ParamType == TypeCode.Int64 || con.ParamType == TypeCode.UInt16 || con.ParamType == TypeCode.UInt32 || con.ParamType == TypeCode.UInt64)
				{
					text = string.Concat(new string[]
					{
						text,
						" and ",
						con.ParamName.Replace("@", ""),
						con.ParamOper,
						con.ParamName
					});
				}
				else
				{
					text = string.Concat(new string[]
					{
						text,
						" and ",
						con.ParamName.Replace("@", ""),
						" = ",
						con.ParamName
					});
				}
			}
			else
			{
				text = string.Concat(new string[]
				{
					text,
					" and ",
					con.ParamName.Replace("@", ""),
					" = ",
					con.ParamName
				});
			}
			return text;
		}
	}
}
