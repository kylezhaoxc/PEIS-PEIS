using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class CommonConclusion
	{
		private static ICommonConclusion dal = DataAccess.CreateCommonConclusion();

		private static readonly CommonConclusion _instance = new CommonConclusion();

		public static CommonConclusion Instance
		{
			get
			{
				return CommonConclusion._instance;
			}
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return CommonConclusion.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		public DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return CommonConclusion.dal.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public DataSet ExcuteQuerySqlX(string AppSettingKey, string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return CommonConclusion.dal.ExcuteQuerySqlX(AppSettingKey, QuerySqlCode, conditions);
		}

		public DataTable GetConclusionByKeyWord(string keyword)
		{
			string cacheKey = "AllConclusionList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryCust_ConclusionList_Param", null).Tables[0];
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
			DataRow[] array = dataTable2.Select(string.Concat(new string[]
			{
				" (InputCode like '",
				keyword,
				"%' or ConclusionName like '",
				keyword,
				"%') and Is_Banned = 0 "
			}));
			if (array != null && array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (hashSet.Add(int.Parse(array[i]["ID_Conclusion"].ToString())))
					{
						array[i]["Explanation"] = "";
						array[i]["Suggestion"] = "";
						array[i]["DietGuide"] = "";
						array[i]["SportsGuide"] = "";
						array[i]["HealthKnowledge"] = "";
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
				if (keyword.Length >= num3)
				{
					array = dataTable2.Select(string.Concat(new string[]
					{
						" (InputCode like '%",
						keyword,
						"%' or ConclusionName like '%",
						keyword,
						"%') and Is_Banned = 0 "
					}));
					if (array != null && array.Length > 0)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (hashSet.Add(int.Parse(array[i]["ID_Conclusion"].ToString())))
							{
								array[i]["Explanation"] = "";
								array[i]["Suggestion"] = "";
								array[i]["DietGuide"] = "";
								array[i]["SportsGuide"] = "";
								array[i]["HealthKnowledge"] = "";
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
			if (num2 < num)
			{
				array = null;
				if (keyword.Length >= num3)
				{
					array = dataTable2.Select(" TeamConclusionName like '%" + keyword + "%'  and Is_Banned = 0 ");
				}
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_Conclusion"].ToString())))
						{
							array[i]["Explanation"] = "";
							array[i]["Suggestion"] = "";
							array[i]["DietGuide"] = "";
							array[i]["SportsGuide"] = "";
							array[i]["HealthKnowledge"] = "";
							array[i]["InputCode"] = array[i]["InputCode"].ToString().ToUpper();
							dataTable3.ImportRow(array[i]);
							if (keyword.Length < num3)
							{
								if (num2++ > num)
								{
									break;
								}
							}
						}
					}
				}
			}
			return dataTable3;
		}

		public string GetConclusionName(int ID_Conclusion)
		{
			string cacheKey = "AllConclusionList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryCust_ConclusionList_Param", null).Tables[0];
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
			if (ID_Conclusion > 0)
			{
				DataRow[] array = dataTable2.Select(" [ID_Conclusion] = " + ID_Conclusion, " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					int num = 0;
					if (num < array.Length)
					{
						result = array[num]["ConclusionName"].ToString();
						return result;
					}
				}
			}
			result = "";
			return result;
		}

		public int SaveConclusion(PEIS.Model.BusConclusion ConclusionModel)
		{
			return CommonConclusion.dal.SaveConclusion(ConclusionModel);
		}

		public DataTable GetConclusionAllContentByIDs(string IDList, string ExistIDList)
		{
			string cacheKey = "AllConclusionList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryCust_ConclusionList_Param", null).Tables[0];
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
			HashSet<int> hashSet = new HashSet<int>();
			if (!string.IsNullOrEmpty(ExistIDList.Trim()))
			{
				string[] array = ExistIDList.Split(new char[]
				{
					','
				});
				if (array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!string.IsNullOrEmpty(array[i].ToString()))
						{
							hashSet.Add(int.Parse(array[i].ToString()));
						}
					}
				}
			}
			DataTable dataTable2 = dataTable.Clone();
			if (!string.IsNullOrEmpty(IDList.Trim()))
			{
				string[] array2 = IDList.Split(new char[]
				{
					','
				});
				if (array2.Length > 0)
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (!string.IsNullOrEmpty(array2[i].ToString()))
						{
							if (hashSet.Add(int.Parse(array2[i].ToString())))
							{
								DataRow[] array3 = dataTable.Select(" ID_Conclusion = " + array2[i].ToString() + "  and Is_Banned = 0 ");
								if (array3 != null && array3.Length > 0)
								{
									for (int j = 0; j < array3.Length; j++)
									{
										dataTable2.ImportRow(array3[j]);
									}
								}
							}
						}
					}
				}
			}
			return dataTable2;
		}

		public int SaveFinalConclusionData(PEIS.Model.OnCustPhysicalExamInfo OCPEIModel, List<PEIS.Model.OnCustConclusion> OnCustConclusionList)
		{
			return CommonConclusion.dal.SaveFinalConclusionData(OCPEIModel, OnCustConclusionList);
		}

		public int UpdateFinalConclusionSectionLock(PEIS.Model.OnCustPhysicalExamInfo OCPEIModel)
		{
			return CommonConclusion.dal.UpdateFinalConclusionSectionLock(OCPEIModel);
		}

		public int SaveCustomerFinalCheck(PEIS.Model.OnFianlCheck OFCModel)
		{
			return CommonConclusion.dal.SaveCustomerFinalCheck(OFCModel);
		}

		public int LockCustomerFinalCheck(PEIS.Model.OnCustPhysicalExamInfo OCPEIModel)
		{
			return CommonConclusion.dal.LockCustomerFinalCheck(OCPEIModel);
		}

		public PEIS.Model.OnFianlCheck GetCustomerLastOnFianlCheck(long ID_Customer)
		{
			return CommonConclusion.dal.GetCustomerLastOnFianlCheck(ID_Customer);
		}

		public int SaveCustomerFinaUnCheck(PEIS.Model.OnFianlCheck OFCModel)
		{
			return CommonConclusion.dal.SaveCustomerFinaUnCheck(OFCModel);
		}

		public int UpdateCustomerNotFinalFinished(PEIS.Model.OnCustPhysicalExamInfo OCPEIModel)
		{
			return CommonConclusion.dal.UpdateCustomerNotFinalFinished(OCPEIModel);
		}

		public string GetConclusionTypeName(int ID_ConclusionType)
		{
			string cacheKey = "AllConclusionTypeList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickConclusionTypeList_Param", null).Tables[0];
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
			if (ID_ConclusionType > 0)
			{
				DataRow[] array = dataTable2.Select(" [ID_ConclusionType] = " + ID_ConclusionType, " ConclusionTypeName ASC ");
				if (array != null && array.Length > 0)
				{
					int num = 0;
					if (num < array.Length)
					{
						result = array[num]["ConclusionTypeName"].ToString();
						return result;
					}
				}
			}
			result = "";
			return result;
		}
	}
}
