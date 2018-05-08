using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DctFinalConclusionType
	{
		private static readonly DctFinalConclusionType _instance = new DctFinalConclusionType();

		private readonly IDctFinalConclusionType dal = DataAccess.CreateDctFinalConclusionType();

		public static DctFinalConclusionType Instance
		{
			get
			{
				return DctFinalConclusionType._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_FinalConclusionType)
		{
			return this.dal.Exists(ID_FinalConclusionType);
		}

		public int Add(PEIS.Model.DctFinalConclusionType model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DctFinalConclusionType model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_FinalConclusionType)
		{
			return this.dal.Delete(ID_FinalConclusionType);
		}

		public bool DeleteList(string ID_FinalConclusionTypelist)
		{
			return this.dal.DeleteList(ID_FinalConclusionTypelist);
		}

		public PEIS.Model.DctFinalConclusionType GetModel(int ID_FinalConclusionType)
		{
			return this.dal.GetModel(ID_FinalConclusionType);
		}

		public PEIS.Model.DctFinalConclusionType GetModelByCache(int ID_FinalConclusionType)
		{
			string cacheKey = "DctFinalConclusionTypeModel-" + ID_FinalConclusionType;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_FinalConclusionType);
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
			return (PEIS.Model.DctFinalConclusionType)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DctFinalConclusionType> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DctFinalConclusionType> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DctFinalConclusionType> list = new List<PEIS.Model.DctFinalConclusionType>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DctFinalConclusionType dctFinalConclusionType = new PEIS.Model.DctFinalConclusionType();
					if (dt.Rows[i]["ID_FinalConclusionType"].ToString() != "")
					{
						dctFinalConclusionType.ID_FinalConclusionType = int.Parse(dt.Rows[i]["ID_FinalConclusionType"].ToString());
					}
					dctFinalConclusionType.FinalConclusionTypeName = dt.Rows[i]["FinalConclusionTypeName"].ToString();
					dctFinalConclusionType.InputCode = dt.Rows[i]["InputCode"].ToString();
					dctFinalConclusionType.Note = dt.Rows[i]["Note"].ToString();
					if (dt.Rows[i]["ID_Creator"].ToString() != "")
					{
						dctFinalConclusionType.ID_Creator = new int?(int.Parse(dt.Rows[i]["ID_Creator"].ToString()));
					}
					if (dt.Rows[i]["CreateDate"].ToString() != "")
					{
						dctFinalConclusionType.CreateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreateDate"].ToString()));
					}
					if (dt.Rows[i]["Is_Banned"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Banned"].ToString() == "1" || dt.Rows[i]["Is_Banned"].ToString().ToLower() == "true")
						{
							dctFinalConclusionType.Is_Banned = new bool?(true);
						}
						else
						{
							dctFinalConclusionType.Is_Banned = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_BanOpr"].ToString() != "")
					{
						dctFinalConclusionType.ID_BanOpr = new int?(int.Parse(dt.Rows[i]["ID_BanOpr"].ToString()));
					}
					dctFinalConclusionType.BanDescribe = dt.Rows[i]["BanDescribe"].ToString();
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
						dctFinalConclusionType.DispOrder = int.Parse(dt.Rows[i]["DispOrder"].ToString());
					}
					if (dt.Rows[i]["BanDate"].ToString() != "")
					{
						dctFinalConclusionType.BanDate = new DateTime?(DateTime.Parse(dt.Rows[i]["BanDate"].ToString()));
					}
					dctFinalConclusionType.BanOperator = dt.Rows[i]["BanOperator"].ToString();
					dctFinalConclusionType.FinalConclusionSignCode = dt.Rows[i]["FinalConclusionSignCode"].ToString();
					list.Add(dctFinalConclusionType);
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
