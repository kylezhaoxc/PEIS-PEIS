using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictVocation
	{
		private readonly IDictVocation dal = DataAccess.CreateDictVocation();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int VocationID)
		{
			return this.dal.Exists(VocationID);
		}

		public int Add(PEIS.Model.DictVocation model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictVocation model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int VocationID)
		{
			return this.dal.Delete(VocationID);
		}

		public bool DeleteList(string VocationIDlist)
		{
			return this.dal.DeleteList(VocationIDlist);
		}

		public PEIS.Model.DictVocation GetModel(int VocationID)
		{
			return this.dal.GetModel(VocationID);
		}

		public PEIS.Model.DictVocation GetModelByCache(int VocationID)
		{
			string cacheKey = "DictVocationModel-" + VocationID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(VocationID);
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
			return (PEIS.Model.DictVocation)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictVocation> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictVocation> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictVocation> list = new List<PEIS.Model.DictVocation>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictVocation dctVocation = new PEIS.Model.DictVocation();
					if (dt.Rows[i]["VocationID"].ToString() != "")
					{
						dctVocation.VocationID = int.Parse(dt.Rows[i]["VocationID"].ToString());
					}
					dctVocation.VocationName = dt.Rows[i]["VocationName"].ToString();
					dctVocation.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(dctVocation);
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
