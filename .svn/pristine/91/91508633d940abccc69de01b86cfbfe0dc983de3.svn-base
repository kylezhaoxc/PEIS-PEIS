using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictCountry
	{
		private readonly IDictCountry dal = DataAccess.CreateDictCountry();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int CountryID)
		{
			return this.dal.Exists(CountryID);
		}

		public int Add(PEIS.Model.DictCountry model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictCountry model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int CountryID)
		{
			return this.dal.Delete(CountryID);
		}

		public bool DeleteList(string CountryIDlist)
		{
			return this.dal.DeleteList(CountryIDlist);
		}

		public PEIS.Model.DictCountry GetModel(int CountryID)
		{
			return this.dal.GetModel(CountryID);
		}

		public PEIS.Model.DictCountry GetModelByCache(int CountryID)
		{
			string cacheKey = "DictCountryModel-" + CountryID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(CountryID);
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
			return (PEIS.Model.DictCountry)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictCountry> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictCountry> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictCountry> list = new List<PEIS.Model.DictCountry>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictCountry dctCountry = new PEIS.Model.DictCountry();
					if (dt.Rows[i]["CountryID"].ToString() != "")
					{
						dctCountry.CountryID = int.Parse(dt.Rows[i]["CountryID"].ToString());
					}
					dctCountry.CountryName = dt.Rows[i]["CountryName"].ToString();
					dctCountry.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(dctCountry);
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
