using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class CommonSystemInfo
	{
		private static ICommonSystemInfo dal = DataAccess.CreateCommonSystemInfo();

		private static readonly CommonSystemInfo _instance = new CommonSystemInfo();

		public static CommonSystemInfo Instance
		{
			get
			{
				return CommonSystemInfo._instance;
			}
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return CommonSystemInfo.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		public DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return CommonSystemInfo.dal.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public DataTable GetQuickSectionList(string inputcode)
		{
			string cacheKey = "AllSectionList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickSectionList_Param", null).Tables[0];
					if (obj != null)
					{
						int configInt = ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(cacheKey, obj, DateTime.Now.AddMinutes((double)configInt), System.TimeSpan.Zero);
					}
				}
				catch
				{
				}
			}
			DataTable dataTable = (DataTable)obj;
			DataTable dataTable2 = dataTable.Copy();
			HashSet<int> hashSet = new HashSet<int>();
			int num = 100;
			int num2 = 0;
			int num3 = 1;
			DataTable dataTable3 = dataTable2.Clone();
			if (string.IsNullOrEmpty(inputcode))
			{
				dataTable3 = dataTable2.Copy();
			}
			else
			{
				DataRow[] array = dataTable2.Select(" [InputCode] like '" + inputcode + "%' ", " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_Section"].ToString())))
						{
							array[i]["InputCode"] = array[i]["InputCode"].ToString().ToUpper();
							dataTable3.ImportRow(array[i]);
							if (num2++ > num)
							{
								break;
							}
						}
					}
				}
				if (num2 < num)
				{
					if (inputcode.Length >= num3)
					{
						array = dataTable2.Select(" [InputCode] like '%" + inputcode + "%' ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_Section"].ToString())))
								{
									array[i]["InputCode"] = array[i]["InputCode"].ToString().ToUpper();
									dataTable3.ImportRow(array[i]);
									if (num2++ > num)
									{
										break;
									}
								}
							}
						}
					}
				}
			}
			return dataTable3;
		}

		public string GetSectionName(int ID_Section)
		{
			string cacheKey = "AllSectionList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickSectionList_Param", null).Tables[0];
					if (obj != null)
					{
						int configInt = ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(cacheKey, obj, DateTime.Now.AddMinutes((double)configInt), System.TimeSpan.Zero);
					}
				}
				catch
				{
				}
			}
			DataTable dataTable = (DataTable)obj;
			DataTable dataTable2 = dataTable.Copy();
			DataTable dataTable3 = dataTable2.Clone();
			string result;
			if (ID_Section > 0)
			{
				DataRow[] array = dataTable2.Select(" [ID_Section] = " + ID_Section, " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					int num = 0;
					if (num < array.Length)
					{
						result = array[num]["SectionName"].ToString();
						return result;
					}
				}
			}
			result = "";
			return result;
		}
	}
}
