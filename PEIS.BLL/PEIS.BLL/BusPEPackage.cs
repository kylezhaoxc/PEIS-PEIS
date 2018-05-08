using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusPEPackage
    {
		private readonly IBusPEPackage dal = DataAccess.CreateBusPEPackage();

		private static readonly BusPEPackage _instance = new BusPEPackage();

		public static BusPEPackage Instance
		{
			get
			{
				return BusPEPackage._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int PEPackageID)
		{
			return this.dal.Exists(PEPackageID);
		}

		public int Add(PEIS.Model.BusPEPackage model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusPEPackage model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int PEPackageID)
		{
			return this.dal.Delete(PEPackageID);
		}

		public bool DeleteList(string PEPackageIDlist)
		{
			return this.dal.DeleteList(PEPackageIDlist);
		}

		public PEIS.Model.BusPEPackage GetModel(int PEPackageID)
		{
			return this.dal.GetModel(PEPackageID);
		}

		public PEIS.Model.BusPEPackage GetModelByCache(int PEPackageID)
		{
			string cacheKey = "PEPackageModel-" + PEPackageID;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(PEPackageID);
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
			return (PEIS.Model.BusPEPackage)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusPEPackage> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusPEPackage> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusPEPackage> list = new List<PEIS.Model.BusPEPackage>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusPEPackage pePackage = new PEIS.Model.BusPEPackage();
					if (dt.Rows[i]["PEPackageID"].ToString() != "")
					{
						pePackage.PEPackageID = int.Parse(dt.Rows[i]["PEPackageID"].ToString());
					}
					pePackage.PEPackageName = dt.Rows[i]["PEPackageName"].ToString();
					if (dt.Rows[i]["Forsex"].ToString() != "")
					{
						pePackage.Forsex = new int?(int.Parse(dt.Rows[i]["Forsex"].ToString()));
					}
					if (dt.Rows[i]["CreatorID"].ToString() != "")
					{
						pePackage.CreatorID = new int?(int.Parse(dt.Rows[i]["CreatorID"].ToString()));
					}
					if (dt.Rows[i]["CreateDate"].ToString() != "")
					{
						pePackage.CreateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreateDate"].ToString()));
					}
					if (dt.Rows[i]["isBanned"].ToString() != "")
					{
						if (dt.Rows[i]["isBanned"].ToString() == "1" || dt.Rows[i]["isBanned"].ToString().ToLower() == "true")
						{
							pePackage.isBanned = true;
						}
						else
						{
							pePackage.isBanned = false;
						}
					}
					if (dt.Rows[i]["IDBanOpr"].ToString() != "")
					{
						pePackage.IDBanOpr = new int?(int.Parse(dt.Rows[i]["IDBanOpr"].ToString()));
					}
					if (dt.Rows[i]["BanDate"].ToString() != "")
					{
						pePackage.BanDate = new DateTime?(DateTime.Parse(dt.Rows[i]["BanDate"].ToString()));
					}
					pePackage.BanDescribe = dt.Rows[i]["BanDescribe"].ToString();
					pePackage.InputCode = dt.Rows[i]["InputCode"].ToString();
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
						pePackage.DispOrder = new int?(int.Parse(dt.Rows[i]["DispOrder"].ToString()));
					}
					pePackage.Note = dt.Rows[i]["Note"].ToString();
					list.Add(pePackage);
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
