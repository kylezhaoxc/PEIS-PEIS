using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class CommonConfig
	{
		private static ICommonConfig dal = DataAccess.CreateCommonConfig();

		private static readonly CommonConfig _instance = new CommonConfig();

		public static CommonConfig Instance
		{
			get
			{
				return CommonConfig._instance;
			}
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return CommonConfig.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		public DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return CommonConfig.dal.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public DataTable GetQuickFeeReportMergerList(string inputcode)
		{
			string cacheKey = "AllFeeReportMergerList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickFeeReportMergerList_Param", null).Tables[0];
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
				DataRow[] array = dataTable2.Select(" InputCode like '" + inputcode + "%' ", " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_FeeReportMerger"].ToString())))
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
						array = dataTable2.Select(" InputCode like '%" + inputcode + "%' ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_FeeReportMerger"].ToString())))
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

		public DataTable GetQuickSpecimenList(string inputcode)
		{
			string cacheKey = "AllSpecimenList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickSpecimenList_Param", null).Tables[0];
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
				DataRow[] array = dataTable2.Select(" InputCode like '" + inputcode + "%' ", " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_Specimen"].ToString())))
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
						array = dataTable2.Select(" InputCode like '%" + inputcode + "%' ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_Specimen"].ToString())))
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

		public DataTable GetQuickConclusionTypeList(string inputcode)
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
			HashSet<int> hashSet = new HashSet<int>();
			int num = 100;
			int num2 = 0;
			int num3 = 1;
			DataTable dataTable3 = dataTable2.Clone();
			if (string.IsNullOrEmpty(inputcode))
			{
				DataRow[] array = dataTable2.Select(" Is_Banned = 0 ", " ConclusionTypeName ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_ConclusionType"].ToString())))
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
			else
			{
				DataRow[] array = dataTable2.Select(" InputCode like '" + inputcode + "%' and Is_Banned = 0 ", " ConclusionTypeName ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_ConclusionType"].ToString())))
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
						array = dataTable2.Select(" InputCode like '%" + inputcode + "%' and Is_Banned = 0 ", " ConclusionTypeName ASC ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_ConclusionType"].ToString())))
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

		public DataTable GetQuickFinalConclusionTypeList(string inputcode)
		{
			string cacheKey = "AllFinalConclusionTypeList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickFinalConclusionTypeList_Param", null).Tables[0];
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
				DataRow[] array = dataTable2.Select(" Is_Banned = 0 ", " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_FinalConclusionType"].ToString())))
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
			else
			{
				DataRow[] array = dataTable2.Select(" InputCode like '" + inputcode + "%' and Is_Banned = 0 ", " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_FinalConclusionType"].ToString())))
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
						array = dataTable2.Select(" InputCode like '%" + inputcode + "%' and Is_Banned = 0 ", " DispOrder ASC ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_FinalConclusionType"].ToString())))
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

		public string GetFinalConclusionTypeName(int ID_FinalConclusionType)
		{
			string cacheKey = "AllFinalConclusionTypeList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickFinalConclusionTypeList_Param", null).Tables[0];
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
			if (ID_FinalConclusionType > 0)
			{
				DataRow[] array = dataTable2.Select(" [ID_FinalConclusionType] = " + ID_FinalConclusionType, " FinalConclusionTypeName ASC ");
				if (array != null && array.Length > 0)
				{
					int num = 0;
					if (num < array.Length)
					{
						result = array[num]["FinalConclusionTypeName"].ToString();
						return result;
					}
				}
			}
			result = "";
			return result;
		}

		public DataTable GetQuickICD10List(string inputcode)
		{
			string cacheKey = "AllQuickICD10ListCopy";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickICD10List_Param", null).Tables[0];
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
				DataRow[] array = dataTable2.Select(" Is_Banned = 0 ", " ICDCNName ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_ICD"].ToString())))
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
			else
			{
				DataRow[] array = dataTable2.Select(" InputCode like '" + inputcode + "%' and Is_Banned = 0 ", " ICDCNName ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_ICD"].ToString())))
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
						array = dataTable2.Select(" InputCode like '%" + inputcode + "%' and Is_Banned = 0 ", " ICDCNName ASC ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_ICD"].ToString())))
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

		public string GetICDCNName(int ID_ICD)
		{
			string cacheKey = "AllQuickICD10ListCopy";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickICD10List_Param", null).Tables[0];
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
			if (ID_ICD > 0)
			{
				DataRow[] array = dataTable2.Select(" [ID_ICD] = " + ID_ICD, " ICDCNName ASC ");
				if (array != null && array.Length > 0)
				{
					int num = 0;
					if (num < array.Length)
					{
						result = array[num]["ICDCNName"].ToString();
						return result;
					}
				}
			}
			result = "";
			return result;
		}

		public int SaveSection(PEIS.Model.SYSSection SectionModel)
		{
			return CommonConfig.dal.SaveSection(SectionModel);
		}

		public int SaveSpecimen(PEIS.Model.BusSpecimen SpecimenModel)
		{
			return CommonConfig.dal.SaveSpecimen(SpecimenModel);
		}

		public int SaveRole(PEIS.Model.SysRole RoleModel)
		{
			return CommonConfig.dal.SaveRole(RoleModel);
		}

		public int SaveExamPlace(PEIS.Model.DictExamPlace ExamPlaceModel)
		{
			return CommonConfig.dal.SaveExamPlace(ExamPlaceModel);
		}

		public int SaveDictExamType(PEIS.Model.DictExamType DictExamTypeModel)
		{
			return CommonConfig.dal.SaveDictExamType(DictExamTypeModel);
		}

		public int SaveFeeItem(PEIS.Model.BusFee busFeeModel)
		{
			return CommonConfig.dal.SaveFeeItem(busFeeModel);
		}

		public int SaveFeeExamRel(int ID_Fee, string newExamItemIDStrs)
		{
			return CommonConfig.dal.SaveFeeExamRel(ID_Fee, newExamItemIDStrs);
		}

		public int SaveExamItemGroupExamRel(int ID_ExamItemGroup, string newExamItemIDStrs)
		{
			return CommonConfig.dal.SaveExamItemGroupExamRel(ID_ExamItemGroup, newExamItemIDStrs);
		}

		public int SaveExamItem(PEIS.Model.BusExamItem ExamItemModel)
		{
			return CommonConfig.dal.SaveExamItem(ExamItemModel);
		}

		public DataTable GetQuickExamItemList(string inputcode, int ID_Section)
		{
			string cacheKey = "AllExamItemList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickExamItemList_Param", null).Tables[0];
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
				int num4 = 0;
				foreach (DataRow row in dataTable2.Rows)
				{
					num4++;
					dataTable3.ImportRow(row);
					if (num4 >= num)
					{
						break;
					}
				}
			}
			else
			{
				DataRow[] array = dataTable2.Select(string.Concat(new string[]
				{
					"  InputCode like '",
					inputcode,
					"%' OR  ExamItemName like '",
					inputcode,
					"%'  "
				}), " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_ExamItem"].ToString())))
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
						array = dataTable2.Select(string.Concat(new string[]
						{
							"  InputCode like '%",
							inputcode,
							"%'  OR  ExamItemName like '%",
							inputcode,
							"%'  "
						}));
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_ExamItem"].ToString())))
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

		public DataTable GetQuickFeeList(string inputcode, int ID_Section)
		{
			string cacheKey = "AllFeeList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickFeeList_Param", null).Tables[0];
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
			int num = 1000;
			int num2 = 0;
			int num3 = 1;
			DataTable dataTable3 = dataTable2.Clone();
			if (string.IsNullOrEmpty(inputcode))
			{
				int num4 = 0;
				foreach (DataRow row in dataTable2.Rows)
				{
					num4++;
					dataTable3.ImportRow(row);
					if (num4 >= num)
					{
						break;
					}
				}
			}
			else
			{
				DataRow[] array = dataTable2.Select("  InputCode like '" + inputcode + "%' ", " InputCode ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_Fee"].ToString())))
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
						array = dataTable2.Select("  InputCode like '%" + inputcode + "%' ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_Fee"].ToString())))
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

		public int SaveSymptom(PEIS.Model.BusSymptom BusSymptomModel)
		{
			return CommonConfig.dal.SaveSymptom(BusSymptomModel);
		}

		public int SaveUser(PEIS.Model.SYSOpUser UserModel)
		{
			return CommonConfig.dal.SaveUser(UserModel);
		}

		public int ClearUserLoginCount(int ID_User)
		{
			return CommonConfig.dal.ClearUserLoginCount(ID_User);
		}

		public int ResetUserPassword(int ID_User, string DefaultPassword)
		{
			return CommonConfig.dal.ResetUserPassword(ID_User, DefaultPassword);
		}

		public DataTable GetQuickNationList(string inputcode)
		{
			string cacheKey = "AllQuickNationListCopy";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickNationList_Param", null).Tables[0];
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
				DataRow[] array = dataTable2.Select(" InputCode like '" + inputcode + "%' ", " NationID ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["NationID"].ToString())))
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
						array = dataTable2.Select(" InputCode like '%" + inputcode + "%' ", " NationID ASC ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["NationID"].ToString())))
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

		public DataTable GetQuickExamTypeList(string inputcode)
		{
			string cacheKey = "AllExamTypeList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickExamTypeList_Param", null).Tables[0];
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
				DataRow[] array = dataTable2.Select(" InputCode like '" + inputcode + "%' ", " ExamTypeID ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ExamTypeID"].ToString())))
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
						array = dataTable2.Select(" InputCode like '%" + inputcode + "%' ", " ExamTypeID ASC ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ExamTypeID"].ToString())))
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

		public DataTable GetQuickSetList(string inputcode, int nSex)
		{
			string cacheKey = "AllSetList";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickSetList_Param", null).Tables[0];
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
				DataRow[] array = dataTable2.Select(string.Concat(new object[]
				{
					" InputCode like '",
					inputcode,
					"%' and ( Forsex = ",
					nSex,
					" or Forsex = 2  )  "
				}), " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["PEPackageID"].ToString())))
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
			else
			{
				DataRow[] array = dataTable2.Select(string.Concat(new object[]
				{
					" InputCode like '",
					inputcode,
					"%'  and ( Forsex = ",
					nSex,
					" or Forsex = 2  )  "
				}), " DispOrder ASC ");
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["PEPackageID"].ToString())))
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
						array = dataTable2.Select(string.Concat(new object[]
						{
							" InputCode like '%",
							inputcode,
							"%' and ( Forsex = ",
							nSex,
							" or Forsex = 2  )  "
						}), " DispOrder ASC ");
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["PEPackageID"].ToString())))
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

		public int SaveBusPEPackage(PEIS.Model.BusPEPackage buspePackage)
		{
			return CommonConfig.dal.SaveBusPEPackage(buspePackage);
		}

		public int SaveSetFeeRel(int PEPackageID, string newFeeIDStrs)
		{
			return CommonConfig.dal.SaveSetFeeRel(PEPackageID, newFeeIDStrs);
		}

		public int MergeCustExamInfo(string MergerID_01, string MergerID_02, string[] ConnectionStringArray)
		{
			return CommonConfig.dal.MergeCustExamInfo(MergerID_01, MergerID_02, ConnectionStringArray);
		}

		public int SaveConclusionType(PEIS.Model.BusConclusionType ConclusionTypeModel)
		{
			return CommonConfig.dal.SaveConclusionType(ConclusionTypeModel);
		}

		public int SaveFinalConclusionType(PEIS.Model.DctFinalConclusionType FinalConclusionTypeModel)
		{
			return CommonConfig.dal.SaveFinalConclusionType(FinalConclusionTypeModel);
		}

		public int SaveICD(PEIS.Model.DctICDTen ICDTenModel)
		{
			return CommonConfig.dal.SaveICD(ICDTenModel);
		}

		public int SaveFeeReport(PEIS.Model.BusFeeReport FeeReportModel)
		{
			return CommonConfig.dal.SaveFeeReport(FeeReportModel);
		}

		public int SaveExamItemGroup(PEIS.Model.BusExamItemGroup ExamItemGroupModel)
		{
			return CommonConfig.dal.SaveExamItemGroup(ExamItemGroupModel);
		}
	}
}
