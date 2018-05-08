using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictCultrul
    {
        private readonly IDictCultrul dal = DataAccess.CreateDictCultrul();

        public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int CultrulID)
		{
			return this.dal.Exists(CultrulID);
		}

		public int Add(PEIS.Model.DictCultrul model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictCultrul model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int CultrulID)
		{
			return this.dal.Delete(CultrulID);
		}

		public bool DeleteList(string CultrulIDlist)
		{
			return this.dal.DeleteList(CultrulIDlist);
		}

		public PEIS.Model.DictCultrul GetModel(int CultrulID)
		{
			return this.dal.GetModel(CultrulID);
		}

		public PEIS.Model.DictCultrul GetModelByCache(int CultrulID)
		{
			string cacheKey = "DictCultrulModel-" + CultrulID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(CultrulID);
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
			return (PEIS.Model.DictCultrul)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictCultrul> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictCultrul> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictCultrul> list = new List<PEIS.Model.DictCultrul>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictCultrul dctCultrul = new PEIS.Model.DictCultrul();
					if (dt.Rows[i]["CultrulID"].ToString() != "")
					{
						dctCultrul.CultrulID = int.Parse(dt.Rows[i]["CultrulID"].ToString());
					}
					dctCultrul.CultrulName = dt.Rows[i]["CultrulName"].ToString();
					dctCultrul.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(dctCultrul);
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
