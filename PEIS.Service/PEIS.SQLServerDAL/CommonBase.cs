using PEIS.Common;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace PEIS.SQLServerDAL
{
	public abstract class CommonBase
	{
		protected static string tableStartLetter = "";

		public CommonBase()
		{
		}

		public virtual DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = string.Empty;
			string text4 = string.Empty;
			DataTable result;
			try
			{
				string[] sqlSentence = this.GetSqlSentence(pageCode);
				text = sqlSentence[0];
				text2 = sqlSentence[1];
				text3 = sqlSentence[2];
				text4 = sqlSentence[3];
				text3 += " as A where 1=1 ";
				if (conditions != null)
				{
					for (int i = 0; i < conditions.Length; i++)
					{
						SqlConditionInfo sqlConditionInfo = conditions[i];
						if (sqlConditionInfo != null)
						{
							string text5 = sqlConditionInfo.ParamValue.ToString();
							string text6 = sqlConditionInfo.ParamName;
							if (sqlConditionInfo.IsSelf)
							{
								text3 = text3.Replace(text6, sqlConditionInfo.ParamName.Replace("@", string.Empty));
							}
							if (!string.IsNullOrEmpty(text5) && !(text5 == "-1"))
							{
								if (text6.IndexOf("@") == -1)
								{
									text6 = "@" + text6;
								}
								if (sqlConditionInfo.Place != 2)
								{
									text3 += Util.GetConvertParam2Where(sqlConditionInfo);
								}
								text3 = text3.Replace(text6, Util.GetStr(sqlConditionInfo));
								text4 = text4.Replace(sqlConditionInfo.ParamName, sqlConditionInfo.ParamValue.ToString());
								text2 = text2.Replace(text6, Util.GetStr(sqlConditionInfo));
							}
						}
					}
				}
				result = Pagination.ProcPage(text2, text3, text, text4, pageIndex, pageSize, out recordCount, out pageCount);
			}
			catch (SqlException ex)
			{
				Log4J.Instance.Error(string.Concat(new object[]
				{
					"通用查询语句（分页) Exception：",
					ex.Message,
					" AllFields：",
					Secret.AES.Encrypt(text2),
					"， TablesAndWhere：",
					Secret.AES.Encrypt(text3),
					"， IndexField：",
					text,
					"， pageIndex：",
					pageIndex,
					"， pageSize：",
					pageSize
				}));
				throw ex;
			}
			catch (Exception ex2)
			{
				Log4J.Instance.Error(string.Concat(new object[]
				{
					"通用查询语句（分页) Exception：",
					ex2.Message,
					" AllFields：",
					Secret.AES.Encrypt(text2),
					"， TablesAndWhere：",
					Secret.AES.Encrypt(text3),
					"， IndexField：",
					text,
					"， pageIndex：",
					pageIndex,
					"， pageSize：",
					pageSize
				}));
				throw new Exception("用于分页的SQL语句无效," + ex2.Message);
			}
			return result;
		}

		public virtual DataTable CreateMaxNum(string NumberHeader)
		{
			if (NumberHeader == string.Empty || NumberHeader.Length < 2)
			{
				NumberHeader = "11";
			}
			else
			{
				NumberHeader = NumberHeader.Substring(0, 2);
			}
			NumberHeader += DateTime.Now.ToString("ddMMyyyy");
			SqlParameter[] commandParameters = new SqlParameter[]
			{
				new SqlParameter("@NumberHeader", NumberHeader)
			};
			SqlParameter sqlParameter = new SqlParameter();
			return DbHelperSQL.ExecuteTable(CommandType.StoredProcedure, "CreateMaxNum", commandParameters);
		}

		protected string[] GetSqlSentence(string PageName)
		{
			FieldInfo field = base.GetType().GetField(PageName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new Exception("没有找到SQL：" + PageName);
			}
			return (string[])field.GetValue(this);
		}

		public virtual DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			int num = 0;
			string text = string.Empty;
			DataSet result;
			try
			{
				string[] sqlSentence = this.GetSqlSentence(QuerySqlCode);
				if (sqlSentence.Length > 0)
				{
					text = sqlSentence[0].ToString();
				}
				if (!string.IsNullOrEmpty(text) && conditions != null)
				{
					for (int i = 0; i < conditions.Length; i++)
					{
						SqlConditionInfo sqlConditionInfo = conditions[i];
						if (sqlConditionInfo != null)
						{
							string text2 = sqlConditionInfo.ParamValue.ToString();
							if (!string.IsNullOrEmpty(text2))
							{
								string text3 = sqlConditionInfo.ParamName;
								if (text3.IndexOf("@") == -1)
								{
									text3 = "@" + text3;
								}
								if (text3 == "@CustomerExamState")
								{
									try
									{
										num = int.Parse(text2);
									}
									catch (Exception var_6_DB)
									{
										num = 0;
									}
								}
								text = text.Replace(text3, Util.GetStr(sqlConditionInfo));
							}
						}
					}
				}
				if (num > 0)
				{
					string connectionString;
					if (num == 1)
					{
						connectionString = PubConstant.GetConnectionString("ToOffLineConnectionString");
					}
					else
					{
						connectionString = PubConstant.GetConnectionString("FYH_HisFile" + (num - 1).ToString().PadLeft(3, '0'));
					}
					result = DbHelperSQL.Query(connectionString, text);
					return result;
				}
				result = DbHelperSQL.Query(text);
				return result;
			}
			catch (Exception ex)
			{
				Log4J.Instance.Error("通用查询语句（不分页) Exception：" + ex.Message + " Sql：" + Secret.AES.Encrypt(text));
			}
			result = null;
			return result;
		}

		public virtual DataSet ExcuteQuerySqlX(string AppSettingKey, string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			string connectionString = PEIS.DBUtility.PubConstant.GetConnectionString(AppSettingKey);
			DataSet result;
			if (string.IsNullOrEmpty(connectionString))
			{
				result = null;
			}
			else
			{
				string text = string.Empty;
				try
				{
					string[] sqlSentence = this.GetSqlSentence(QuerySqlCode);
					if (sqlSentence.Length > 0)
					{
						text = sqlSentence[0].ToString();
					}
					if (!string.IsNullOrEmpty(text) && conditions != null)
					{
						for (int i = 0; i < conditions.Length; i++)
						{
							SqlConditionInfo sqlConditionInfo = conditions[i];
							if (sqlConditionInfo != null)
							{
								string text2 = sqlConditionInfo.ParamValue.ToString();
								if (!string.IsNullOrEmpty(text2))
								{
									string text3 = sqlConditionInfo.ParamName;
									if (text3.IndexOf("@") == -1)
									{
										text3 = "@" + text3;
									}
									if (text3 == "@CustomerExamState")
									{
										try
										{
											int num = int.Parse(text2);
										}
										catch (Exception var_7_FE)
										{
										}
									}
									text = text.Replace(text3, Util.GetStr(sqlConditionInfo));
								}
							}
						}
					}
					result = DbHelperSQL.Query(connectionString, text);
					return result;
				}
				catch (Exception ex)
				{
					Log4J.Instance.Error("通用查询语句（不分页) Exception：" + ex.Message + " Sql：" + Secret.AES.Encrypt(text));
				}
				result = null;
			}
			return result;
		}

		public virtual int ExecuteSqlTran(List<string> SQLStringList)
		{
			return DbHelperSQL.ExecuteSqlTran(SQLStringList);
		}
	}
}
