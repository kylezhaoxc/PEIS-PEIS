using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictFeeWay
    {
		private readonly IDictFeeWay dal = DataAccess.CreateDictFeeWay();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int FeeWayID)
		{
			return this.dal.Exists(FeeWayID);
		}

		public int Add(PEIS.Model.DictFeeWay model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictFeeWay model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int FeeWayID)
		{
			return this.dal.Delete(FeeWayID);
		}

		public bool DeleteList(string FeeWayIDlist)
		{
			return this.dal.DeleteList(FeeWayIDlist);
		}

		public PEIS.Model.DictFeeWay GetModel(int FeeWayID)
		{
			return this.dal.GetModel(FeeWayID);
		}

		public PEIS.Model.DictFeeWay GetModelByCache(int FeeWayID)
		{
			string cacheKey = "DictFeeWayModel-" + FeeWayID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(FeeWayID);
					if (obj != null)
					{
						int configInt = ConfigHelper.GetConfigInt("ModelCache");
						Maticsoft.Common.DataCache.SetCache(cacheKey, obj, DateTime.Now.AddMinutes((double)configInt), TimeSpan.Zero);
					}
				}
				catch
				{
				}
			}
			return (PEIS.Model.DictFeeWay)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictFeeWay> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictFeeWay> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictFeeWay> list = new List<PEIS.Model.DictFeeWay>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictFeeWay dictFeeWay = new PEIS.Model.DictFeeWay();
					if (dt.Rows[i]["FeeWayID"].ToString() != "")
					{
                        dictFeeWay.FeeWayID = int.Parse(dt.Rows[i]["FeeWayID"].ToString());
					}
                    dictFeeWay.FeeWayName = dt.Rows[i]["FeeWayName"].ToString();
					if (dt.Rows[i]["Default"].ToString() != "")
					{
						if (dt.Rows[i]["Default"].ToString() == "1" || dt.Rows[i]["Default"].ToString().ToLower() == "true")
						{
                            dictFeeWay.Default = true;
						}
						else
						{
                            dictFeeWay.Default = false;
						}
					}
                    dictFeeWay.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(dictFeeWay);
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
