using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusFeeReport
	{
		private static readonly BusFeeReport _instance = new BusFeeReport();

		private readonly IBusFeeReport dal = DataAccess.CreateBusFeeReport();

		public static BusFeeReport Instance
		{
			get
			{
				return BusFeeReport._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_FeeReport)
		{
			return this.dal.Exists(ID_FeeReport);
		}

		public int Add(PEIS.Model.BusFeeReport model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusFeeReport model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_FeeReport)
		{
			return this.dal.Delete(ID_FeeReport);
		}

		public bool DeleteList(string ID_FeeReportlist)
		{
			return this.dal.DeleteList(ID_FeeReportlist);
		}

		public PEIS.Model.BusFeeReport GetModel(int ID_FeeReport)
		{
			return this.dal.GetModel(ID_FeeReport);
		}

		public PEIS.Model.BusFeeReport GetModelByCache(int ID_FeeReport)
		{
			string cacheKey = "BusFeeReportModel-" + ID_FeeReport;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_FeeReport);
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
			return (PEIS.Model.BusFeeReport)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusFeeReport> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusFeeReport> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusFeeReport> list = new List<PEIS.Model.BusFeeReport>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusFeeReport busFeeReport = new PEIS.Model.BusFeeReport();
					if (dt.Rows[i]["ID_FeeReport"].ToString() != "")
					{
						busFeeReport.ID_FeeReport = int.Parse(dt.Rows[i]["ID_FeeReport"].ToString());
					}
					if (dt.Rows[i]["ID_Fee"].ToString() != "")
					{
						busFeeReport.ID_Fee = new int?(int.Parse(dt.Rows[i]["ID_Fee"].ToString()));
					}
					busFeeReport.FeeName = dt.Rows[i]["FeeName"].ToString();
					busFeeReport.ReportKey = dt.Rows[i]["ReportKey"].ToString();
					busFeeReport.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
					busFeeReport.Note = dt.Rows[i]["Note"].ToString();
					if (dt.Rows[i]["Is_Banned"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Banned"].ToString() == "1" || dt.Rows[i]["Is_Banned"].ToString().ToLower() == "true")
						{
							busFeeReport.Is_Banned = new bool?(true);
						}
						else
						{
							busFeeReport.Is_Banned = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_Operator"].ToString() != "")
					{
						busFeeReport.ID_Operator = new int?(int.Parse(dt.Rows[i]["ID_Operator"].ToString()));
					}
					busFeeReport.Operator = dt.Rows[i]["Operator"].ToString();
					if (dt.Rows[i]["OperateDate"].ToString() != "")
					{
						busFeeReport.OperateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["OperateDate"].ToString()));
					}
					if (dt.Rows[i]["OperateType"].ToString() != "")
					{
						busFeeReport.OperateType = new int?(int.Parse(dt.Rows[i]["OperateType"].ToString()));
					}
					busFeeReport.BanDescribe = dt.Rows[i]["BanDescribe"].ToString();
					list.Add(busFeeReport);
				}
			}
			return list;
		}

		public DataSet GetAllList()
		{
			return this.GetList("");
		}
	}
}
