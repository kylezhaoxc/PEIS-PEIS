using PEIS.IDAL;
using PEIS.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PEIS.SQLServerDAL
{
	public class CommonSubScribe : ICommonSubScribe
	{
		DataTable ICommonSubScribe.GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			DataTable result;
			try
			{
				string[] sqlSentence = this.GetSqlSentence(pageCode);
				string indexField = sqlSentence[0];
				string text = sqlSentence[1];
				string text2 = sqlSentence[2];
				string text3 = sqlSentence[3];
				if (conditions != null)
				{
					for (int i = 0; i < conditions.Length; i++)
					{
						SqlConditionInfo sqlConditionInfo = conditions[i];
						if (sqlConditionInfo != null)
						{
							string text4 = sqlConditionInfo.ParamValue.ToString();
							if (!string.IsNullOrEmpty(text4) && !(text4 == "-1"))
							{
								string text5 = sqlConditionInfo.ParamName;
								if (text5.IndexOf("@") == -1)
								{
									text5 = "@" + text5;
								}
								text2 += Util.GetConvertParam2Where(sqlConditionInfo);
								text2 = text2.Replace(text5, Util.GetStr(sqlConditionInfo));
								text3 = text3.Replace(sqlConditionInfo.ParamName, sqlConditionInfo.ParamValue.ToString());
								text = text.Replace(text5, Util.GetStr(sqlConditionInfo));
							}
						}
					}
				}
				result = Pagination.ProcPage(text, text2, indexField, text3, pageIndex, pageSize, out recordCount, out pageCount);
			}
			catch (SqlException ex)
			{
				throw ex;
			}
			catch (Exception ex2)
			{
				throw new Exception("用于分页的SQL语句无效," + ex2.Message);
			}
			return result;
		}

		protected string[] GetSqlSentence(string PageName)
		{
			FieldInfo field = base.GetType().GetField(PageName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new Exception("没有找到SQL");
			}
			return (string[])field.GetValue(this);
		}
	}
}
