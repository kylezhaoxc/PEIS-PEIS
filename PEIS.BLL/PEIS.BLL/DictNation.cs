using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictNation
	{
        private readonly IDictNation dal = DataAccess.CreateDictNation();

        public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int NationID)
		{
			return this.dal.Exists(NationID);
		}

		public int Add(PEIS.Model.DictNation model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictNation model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int NationID)
		{
			return this.dal.Delete(NationID);
		}

		public bool DeleteList(string NationIDlist)
		{
			return this.dal.DeleteList(NationIDlist);
		}

		public PEIS.Model.DictNation GetModel(int NationID)
		{
			return this.dal.GetModel(NationID);
		}

		public PEIS.Model.DictNation GetModelByCache(int NationID)
		{
			string cacheKey = "DictNationModel-" + NationID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(NationID);
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
			return (PEIS.Model.DictNation)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictNation> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictNation> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictNation> list = new List<PEIS.Model.DictNation>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictNation dctNation = new PEIS.Model.DictNation();
					if (dt.Rows[i]["NationID"].ToString() != "")
					{
						dctNation.NationID = int.Parse(dt.Rows[i]["NationID"].ToString());
					}
					dctNation.NationName = dt.Rows[i]["NationName"].ToString();
					dctNation.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(dctNation);
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
