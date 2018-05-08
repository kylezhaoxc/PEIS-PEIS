using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusConclusionType
	{
		private static readonly BusConclusionType _instance = new BusConclusionType();

		private readonly IBusConclusionType dal = DataAccess.CreateBusConclusionType();

		public static BusConclusionType Instance
		{
			get
			{
				return BusConclusionType._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_ConclusionType)
		{
			return this.dal.Exists(ID_ConclusionType);
		}

		public int Add(PEIS.Model.BusConclusionType model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusConclusionType model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_ConclusionType)
		{
			return this.dal.Delete(ID_ConclusionType);
		}

		public bool DeleteList(string ID_ConclusionTypelist)
		{
			return this.dal.DeleteList(ID_ConclusionTypelist);
		}

		public PEIS.Model.BusConclusionType GetModel(int ID_ConclusionType)
		{
			return this.dal.GetModel(ID_ConclusionType);
		}

		public PEIS.Model.BusConclusionType GetModelByCache(int ID_ConclusionType)
		{
			string cacheKey = "BusConclusionTypeModel-" + ID_ConclusionType;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_ConclusionType);
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
			return (PEIS.Model.BusConclusionType)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusConclusionType> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusConclusionType> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusConclusionType> list = new List<PEIS.Model.BusConclusionType>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusConclusionType busConclusionType = new PEIS.Model.BusConclusionType();
					if (dt.Rows[i]["ID_ConclusionType"].ToString() != "")
					{
						busConclusionType.ID_ConclusionType = int.Parse(dt.Rows[i]["ID_ConclusionType"].ToString());
					}
					busConclusionType.ConclusionTypeName = dt.Rows[i]["ConclusionTypeName"].ToString();
					busConclusionType.InputCode = dt.Rows[i]["InputCode"].ToString();
					if (dt.Rows[i]["Is_Banned"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Banned"].ToString() == "1" || dt.Rows[i]["Is_Banned"].ToString().ToLower() == "true")
						{
							busConclusionType.Is_Banned = new bool?(true);
						}
						else
						{
							busConclusionType.Is_Banned = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_BanOpr"].ToString() != "")
					{
						busConclusionType.ID_BanOpr = new int?(int.Parse(dt.Rows[i]["ID_BanOpr"].ToString()));
					}
					busConclusionType.BanOperator = dt.Rows[i]["BanOperator"].ToString();
					if (dt.Rows[i]["BanDate"].ToString() != "")
					{
						busConclusionType.BanDate = new DateTime?(DateTime.Parse(dt.Rows[i]["BanDate"].ToString()));
					}
					busConclusionType.BanDescribe = dt.Rows[i]["BanDescribe"].ToString();
					list.Add(busConclusionType);
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
