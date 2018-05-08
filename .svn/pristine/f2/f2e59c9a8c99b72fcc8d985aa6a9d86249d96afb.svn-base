using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DictMarriage
	{
		private static readonly DictMarriage _instance = new DictMarriage();

		private readonly IDictMarriage dal = DataAccess.CreateDictMarriage();

		public static DictMarriage Instance
		{
			get
			{
				return DictMarriage._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int MarriageID)
		{
			return this.dal.Exists(MarriageID);
		}

		public int Add(PEIS.Model.DictMarriage model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DictMarriage model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int MarriageID)
		{
			return this.dal.Delete(MarriageID);
		}

		public bool DeleteList(string MarriageIDlist)
		{
			return this.dal.DeleteList(MarriageIDlist);
		}

		public PEIS.Model.DictMarriage GetModel(int MarriageID)
		{
			return this.dal.GetModel(MarriageID);
		}

		public PEIS.Model.DictMarriage GetModelByCache(int MarriageID)
		{
			string cacheKey = "MarriageModel-" + MarriageID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(MarriageID);
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
			return (PEIS.Model.DictMarriage)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DictMarriage> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DictMarriage> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DictMarriage> list = new List<PEIS.Model.DictMarriage>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DictMarriage marriage = new PEIS.Model.DictMarriage();
					if (dt.Rows[i]["MarriageID"].ToString() != "")
					{
                        marriage.MarriageID = int.Parse(dt.Rows[i]["MarriageID"].ToString());
					}
                    marriage.MarriageName = dt.Rows[i]["MarriageName"].ToString();
					if (dt.Rows[i]["MarriageID"].ToString() != "")
					{
                        marriage.MarriageID = int.Parse(dt.Rows[i]["MarriageID"].ToString());
					}
                    marriage.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(marriage);
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
