using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictGender
	{
		private readonly IDictGender dal = DataAccess.CreateDictGender();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int GenderID)
		{
			return this.dal.Exists(GenderID);
		}

		public int Add(PEIS.Model.DictGender model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictGender model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int GenderID)
		{
			return this.dal.Delete(GenderID);
		}

		public bool DeleteList(string GenderIDlist)
		{
			return this.dal.DeleteList(GenderIDlist);
		}

		public PEIS.Model.DictGender GetModel(int GenderID)
		{
			return this.dal.GetModel(GenderID);
		}

		public PEIS.Model.DictGender GetModelByCache(int GenderID)
		{
			string cacheKey = "DictGenderModel-" + GenderID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(GenderID);
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
			return (PEIS.Model.DictGender)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictGender> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictGender> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictGender> list = new List<PEIS.Model.DictGender>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictGender dctGender = new PEIS.Model.DictGender();
					if (dt.Rows[i]["GenderID"].ToString() != "")
					{
						dctGender.GenderID = int.Parse(dt.Rows[i]["GenderID"].ToString());
					}
					dctGender.GenderName = dt.Rows[i]["GenderName"].ToString();
					dctGender.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(dctGender);
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
