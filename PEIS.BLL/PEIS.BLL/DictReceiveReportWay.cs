using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictReceiveReportWay
    {
		private static readonly DictReceiveReportWay _instance = new DictReceiveReportWay();

		private readonly IDictReceiveReportWay dal = DataAccess.CreateDictReceiveReportWay();

		public static DictReceiveReportWay Instance
		{
			get
			{
				return DictReceiveReportWay._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_ReportWay)
		{
			return this.dal.Exists(ID_ReportWay);
		}

		public int Add(PEIS.Model.DictReceiveReportWay model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictReceiveReportWay model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_ReportWay)
		{
			return this.dal.Delete(ID_ReportWay);
		}

		public bool DeleteList(string ID_ReportWaylist)
		{
			return this.dal.DeleteList(ID_ReportWaylist);
		}

		public PEIS.Model.DictReceiveReportWay GetModel(int ID_ReportWay)
		{
			return this.dal.GetModel(ID_ReportWay);
		}

		public PEIS.Model.DictReceiveReportWay GetModelByCache(int ReportWayID)
		{
			string cacheKey = "ReceiveReportWay-" + ReportWayID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ReportWayID);
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
			return (PEIS.Model.DictReceiveReportWay)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictReceiveReportWay> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictReceiveReportWay> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictReceiveReportWay> list = new List<PEIS.Model.DictReceiveReportWay>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictReceiveReportWay dictReceiveReportWay = new PEIS.Model.DictReceiveReportWay();
					if (dt.Rows[i]["ReportWayID"].ToString() != "")
					{
                        dictReceiveReportWay.ReportWayID = int.Parse(dt.Rows[i]["ReportWayID"].ToString());
					}
                    dictReceiveReportWay.ReportWayName = dt.Rows[i]["ReportWayName"].ToString();
					if (dt.Rows[i]["Default"].ToString() != "")
					{
						if (dt.Rows[i]["Default"].ToString() == "1" || dt.Rows[i]["Default"].ToString().ToLower() == "true")
						{
                            dictReceiveReportWay.Default = true;
						}
						else
						{
                            dictReceiveReportWay.Default = false;
						}
					}
                    dictReceiveReportWay.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(dictReceiveReportWay);
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
