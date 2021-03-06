using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class CommonUser
	{
		private readonly int _DefaultNumLength = 4;

		private readonly ICommonUser dal = DataAccess.CreateCommonUser();

		private static readonly CommonUser _instance = new CommonUser();

		public static CommonUser Instance
		{
			get
			{
				return CommonUser._instance;
			}
		}

		public DataTable GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return this.dal.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		public DataSet ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return this.dal.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		public DataTable GetUserInfoByLoginName(string userLoginName)
		{
			return this.dal.GetUserInfoByLoginName(userLoginName);
		}

		public int AddCustomerArcInfo(PEIS.Model.OnArcCust model)
		{
			return this.dal.AddCustomerArcInfo(model);
		}

		public int UpdateCustomerPicInfo(string ID_Customer, PEIS.Model.OnArcCust model)
		{
			return this.dal.UpdateCustomerPicInfo(ID_Customer, model);
		}

		public int AddCustomerManageArcInfo(PEIS.Model.OnArcCust model)
		{
			return this.dal.AddCustomerManageArcInfo(model);
		}

		public int UpdateCustomerPicInfo(PEIS.Model.OnArcCust model)
		{
			return this.dal.UpdateCustomerPicInfo(model);
		}

		public DataTable GetQuickUserList(string inputcode, int VocationType)
		{
			string cacheKey = "AllQuickUserListCopy";
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.ExcuteQuerySql("QueryQuickUserList_Param", null).Tables[0];
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
				DataRow[] array;
				if (VocationType > 0)
				{
					array = dataTable2.Select(" VocationType = " + VocationType.ToString() + " ", " UserName ASC ");
				}
				else
				{
					array = dataTable2.Select(" VocationType >= 0 ", " UserName ASC ");
				}
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["UserID"].ToString())))
						{
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
				DataRow[] array;
				if (VocationType > 0)
				{
					array = dataTable2.Select(string.Concat(new string[]
					{
                        " VocationType = ",
                        VocationType.ToString(),
						" and (UserName like '",
						inputcode,
						"%' OR LoginName like '",
						inputcode,
						"%') "
					}), " UserName ASC ");
				}
				else
				{
					array = dataTable2.Select(string.Concat(new string[]
					{
                        " VocationType >= 0 and (UserName like '",
						inputcode,
						"%' OR LoginName like '",
						inputcode,
						"%') "
					}), " UserName ASC ");
				}
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_User"].ToString())))
						{
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
						if (VocationType > 0)
						{
							array = dataTable2.Select(string.Concat(new string[]
							{
                                " VocationType = ",
                                VocationType.ToString(),
								" and (UserName like '%",
								inputcode,
								"%' OR LoginName like '%",
								inputcode,
								"%') "
							}), " UserName ASC ");
						}
						else
						{
							array = dataTable2.Select(string.Concat(new string[]
							{
                                " VocationType >= 0 and (UserName like '%",
								inputcode,
								"%' OR LoginName like '%",
								inputcode,
								"%') "
							}), " UserName ASC ");
						}
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_User"].ToString())))
								{
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

		public DataTable GetQuickSectionUserList(string inputcode, int VocationType, int SectionID)
		{
			DataTable dataTable = null;
			string cacheKey = "SectionDoctorList-" + SectionID.ToString();
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			try
			{
				if (obj != null)
				{
					dataTable = (DataTable)obj;
				}
			}
			catch (System.Exception var_4_35)
			{
				obj = null;
			}
			if (obj == null)
			{
				try
				{
					string querySqlCode = "QuerySectionExamUserList_Param";
					obj = this.ExcuteQuerySql(querySqlCode, new SqlConditionInfo[]
					{
						new SqlConditionInfo("@SectionID", SectionID, System.TypeCode.Int32)
					}).Tables[0];
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
			if (dataTable == null)
			{
				dataTable = (DataTable)obj;
			}
			DataTable dataTable2 = dataTable.Copy();
			HashSet<int> hashSet = new HashSet<int>();
			int num = 100;
			int num2 = 0;
			int num3 = 1;
			DataTable dataTable3 = dataTable2.Clone();
			if (string.IsNullOrEmpty(inputcode))
			{
				DataRow[] array;
				if (VocationType > 0)
				{
					array = dataTable2.Select(" VocationType = " + VocationType.ToString() + " ", " UserName ASC ");
				}
				else
				{
					array = dataTable2.Select(" VocationType >= 0 ", " UserName ASC ");
				}
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_User"].ToString())))
						{
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
				DataRow[] array;
				if (VocationType > 0)
				{
					array = dataTable2.Select(string.Concat(new string[]
					{
                        " VocationType = ",
                        VocationType.ToString(),
						" and (UserName like '",
						inputcode,
						"%' OR LoginName like '",
						inputcode,
						"%') "
					}), " UserName ASC ");
				}
				else
				{
					array = dataTable2.Select(string.Concat(new string[]
					{
                        " VocationType >= 0 and (UserName like '",
						inputcode,
						"%' OR LoginName like '",
						inputcode,
						"%') "
					}), " UserName ASC ");
				}
				if (array != null && array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (hashSet.Add(int.Parse(array[i]["ID_User"].ToString())))
						{
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
						if (VocationType > 0)
						{
							array = dataTable2.Select(string.Concat(new string[]
							{
                                " VocationType = ",
                                VocationType.ToString(),
								" and (UserName like '%",
								inputcode,
								"%' OR LoginName like '%",
								inputcode,
								"%') "
							}), " UserName ASC ");
						}
						else
						{
							array = dataTable2.Select(string.Concat(new string[]
							{
                                " VocationType >= 0 and (UserName like '%",
								inputcode,
								"%' OR LoginName like '%",
								inputcode,
								"%') "
							}), " UserName ASC ");
						}
						if (array != null && array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (hashSet.Add(int.Parse(array[i]["ID_User"].ToString())))
								{
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
	}
}
