using System;
using System.Collections;
using System.Data;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace PEIS.Common
{
	public class JSONConverter
	{
		private static void WriteDataRow(System.Text.StringBuilder sb, DataRow row)
		{
			sb.Append("{");
			foreach (DataColumn dataColumn in row.Table.Columns)
			{
				sb.AppendFormat("\"{0}\":", dataColumn.ColumnName);
				JSONConverter.WriteValue(sb, row[dataColumn]);
				sb.Append(",");
			}
			if (row.Table.Columns.Count > 0)
			{
				sb.Length--;
			}
			sb.Append("}");
		}

		private static void WriteDataTable(System.Text.StringBuilder sb, DataTable table)
		{
			if (string.IsNullOrEmpty(sb.ToString()))
			{
				sb.Append("{data:[");
			}
			foreach (DataRow row in table.Rows)
			{
				JSONConverter.WriteDataRow(sb, row);
				sb.Append(",");
			}
			if (table.Rows.Count > 0)
			{
				sb.Length--;
			}
			sb.Append("]}");
		}

		private static void WriteEnumerable(System.Text.StringBuilder sb, System.Collections.IEnumerable e)
		{
			bool flag = false;
			sb.Append("[");
			foreach (object current in e)
			{
				JSONConverter.WriteValue(sb, current);
				sb.Append(",");
				flag = true;
			}
			if (flag)
			{
				sb.Length--;
			}
			sb.Append("]");
		}

		private static void WriteHashtable(System.Text.StringBuilder sb, System.Collections.Hashtable e)
		{
			bool flag = false;
			sb.Append("{");
			foreach (string text in e.Keys)
			{
				sb.AppendFormat("\"{0}\":", text.ToLower());
				JSONConverter.WriteValue(sb, e[text]);
				sb.Append(",");
				flag = true;
			}
			if (flag)
			{
				sb.Length--;
			}
			sb.Append("}");
		}

		private static void WriteObject(System.Text.StringBuilder sb, object o)
		{
			System.Reflection.MemberInfo[] members = o.GetType().GetMembers(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			sb.Append("{");
			bool flag = false;
			System.Reflection.MemberInfo[] array = members;
			for (int i = 0; i < array.Length; i++)
			{
				System.Reflection.MemberInfo memberInfo = array[i];
				bool flag2 = false;
				object val = null;
				if ((memberInfo.MemberType & System.Reflection.MemberTypes.Field) == System.Reflection.MemberTypes.Field)
				{
					System.Reflection.FieldInfo fieldInfo = (System.Reflection.FieldInfo)memberInfo;
					val = fieldInfo.GetValue(o);
					flag2 = true;
				}
				else if ((memberInfo.MemberType & System.Reflection.MemberTypes.Property) == System.Reflection.MemberTypes.Property)
				{
					System.Reflection.PropertyInfo propertyInfo = (System.Reflection.PropertyInfo)memberInfo;
					if (propertyInfo.CanRead && propertyInfo.GetIndexParameters().Length == 0)
					{
						val = propertyInfo.GetValue(o, null);
						flag2 = true;
					}
				}
				if (flag2)
				{
					sb.Append("\"");
					sb.Append(memberInfo.Name);
					sb.Append("\":");
					JSONConverter.WriteValue(sb, val);
					sb.Append(",");
					flag = true;
				}
			}
			if (flag)
			{
				sb.Length--;
			}
			sb.Append("}");
		}

		private static void WriteString(System.Text.StringBuilder sb, string s)
		{
			sb.Append("\"");
			sb.Append(s.Replace("\"", "“"));
			sb.Append("\"" + s + "\"");
			int i = 0;
			while (i < s.Length)
			{
				char c = s[i];
				char c2 = c;
				switch (c2)
				{
				case '\b':
					sb.Append("\\b");
					break;
				case '\t':
					sb.Append("\\t");
					break;
				case '\n':
					sb.Append("\\n");
					break;
				case '\v':
					goto IL_E8;
				case '\f':
					sb.Append("\\f");
					break;
				case '\r':
					sb.Append("\\r");
					break;
				default:
					if (c2 != '"')
					{
						if (c2 != '\\')
						{
							goto IL_E8;
						}
						sb.Append("\\\\");
					}
					else
					{
						sb.Append("\\\"");
					}
					break;
				}
				IL_122:
				i++;
				continue;
				IL_E8:
				int num = (int)c;
				if (num < 32 || num > 127)
				{
					sb.AppendFormat("\\u{0:X04}", num);
				}
				else
				{
					sb.Append(c);
				}
				goto IL_122;
			}
			sb.Append("\"");
		}

		public static void WriteValue(System.Text.StringBuilder sb, object val)
		{
			if (val == null || val == System.DBNull.Value)
			{
				sb.Append("\"null\"");
			}
			else if (val is string || val is System.Guid)
			{
				JSONConverter.WriteString(sb, val.ToString());
			}
			else if (val is bool)
			{
				JSONConverter.WriteString(sb, val.ToString());
			}
			else if (val is double || val is float || val is long || val is int || val is short || val is byte || val is decimal)
			{
				JSONConverter.WriteString(sb, val.ToString());
			}
			else if (val.GetType().IsEnum)
			{
				sb.Append("\"" + (int)val + "\"");
			}
			else if (val is DateTime)
			{
				JSONConverter.WriteString(sb, ((DateTime)val).ToString("yyyy-MM-dd HH:mm:ss", new System.Globalization.CultureInfo("en-US", false).DateTimeFormat));
			}
			else if (val is DataTable)
			{
				JSONConverter.WriteDataTable(sb, val as DataTable);
			}
			else if (val is DataRow)
			{
				JSONConverter.WriteDataRow(sb, val as DataRow);
			}
			else if (val is System.Collections.Hashtable)
			{
				JSONConverter.WriteHashtable(sb, val as System.Collections.Hashtable);
			}
			else if (val is System.Collections.IEnumerable)
			{
				JSONConverter.WriteEnumerable(sb, val as System.Collections.IEnumerable);
			}
			else
			{
				JSONConverter.WriteObject(sb, val);
			}
		}

		public static string Convert2Json(object o)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			JSONConverter.WriteValue(stringBuilder, o);
			return stringBuilder.ToString();
		}

		public static string DataTable2Json(int totalCount, bool flag, string errmsg, string singleinfo, DataTable dt)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			if (flag && totalCount > 0 && dt != null && dt.Rows.Count > 0)
			{
				stringBuilder.Append(string.Concat(new object[]
				{
					"{\"totalCount\":\"",
					totalCount,
					"\",\"success\":\"",
					flag.ToString().ToLower(),
					"\",\"error\":\"",
					errmsg,
					"\",\"singleInfo\":\"",
					singleinfo,
					"\",\"dataList\":["
				}));
				JSONConverter.WriteValue(stringBuilder, dt);
			}
			else if (string.IsNullOrEmpty(errmsg))
			{
				stringBuilder.Append("{\"success\":\"" + flag.ToString().ToLower() + "\"}");
			}
			else
			{
				stringBuilder.Append(string.Concat(new string[]
				{
					"{\"success\":\"",
					flag.ToString().ToLower(),
					",\"error\":\"",
					errmsg,
					"\"}"
				}));
			}
			return stringBuilder.ToString();
		}

		public static string DataTable2Json(int totalCount, DataTable dt)
		{
			return JSONConverter.DataTable2Json(totalCount, true, "", "", dt);
		}

		public static string DataTable2Json(DataTable dt)
		{
			string result;
			if (dt != null && dt.Rows.Count > 0)
			{
				result = JSONConverter.DataTable2Json(dt.Rows.Count, true, "", "", dt);
			}
			else
			{
				result = "{success:false}";
			}
			return result;
		}

		private static void WriteFormDataTable(System.Text.StringBuilder sb, DataTable table)
		{
			if (string.IsNullOrEmpty(sb.ToString()))
			{
				sb.Append("{\"success\":\"true\",\"dataList\":");
			}
			foreach (DataRow row in table.Rows)
			{
				JSONConverter.WriteDataRow(sb, row);
				sb.Append(",");
			}
			if (table.Rows.Count > 0)
			{
				sb.Length--;
			}
			sb.Append("}");
		}

		public static string DataTable2JsonForm(DataTable dt)
		{
			string result;
			if (dt != null && dt.Rows.Count > 0)
			{
				System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
				JSONConverter.WriteFormDataTable(stringBuilder, dt);
				result = stringBuilder.ToString();
			}
			else
			{
				result = "{success:false}";
			}
			return result;
		}
	}
}
