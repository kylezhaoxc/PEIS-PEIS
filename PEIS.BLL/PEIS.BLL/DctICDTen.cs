using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class DctICDTen
	{
		private static readonly DctICDTen _instance = new DctICDTen();

		private readonly IDctICDTen dal = DataAccess.CreateDctICDTen();

		public static DctICDTen Instance
		{
			get
			{
				return DctICDTen._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_ICD)
		{
			return this.dal.Exists(ID_ICD);
		}

		public int Add(PEIS.Model.DctICDTen model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.DctICDTen model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_ICD)
		{
			return this.dal.Delete(ID_ICD);
		}

		public bool DeleteList(string ID_ICDlist)
		{
			return this.dal.DeleteList(ID_ICDlist);
		}

		public PEIS.Model.DctICDTen GetModel(int ID_ICD)
		{
			return this.dal.GetModel(ID_ICD);
		}

		public PEIS.Model.DctICDTen GetModelByCache(int ID_ICD)
		{
			string cacheKey = "DctICDTenModel-" + ID_ICD;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_ICD);
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
			return (PEIS.Model.DctICDTen)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.DctICDTen> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.DctICDTen> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.DctICDTen> list = new List<PEIS.Model.DctICDTen>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.DctICDTen dctICDTen = new PEIS.Model.DctICDTen();
					if (dt.Rows[i]["ID_ICD"].ToString() != "")
					{
						dctICDTen.ID_ICD = int.Parse(dt.Rows[i]["ID_ICD"].ToString());
					}
					dctICDTen.ICDCNName = dt.Rows[i]["ICDCNName"].ToString();
					dctICDTen.ICDENName = dt.Rows[i]["ICDENName"].ToString();
					dctICDTen.Code = dt.Rows[i]["Code"].ToString();
					dctICDTen.Codea = dt.Rows[i]["Codea"].ToString();
					if (dt.Rows[i]["ID_Creator"].ToString() != "")
					{
						dctICDTen.ID_Creator = new int?(int.Parse(dt.Rows[i]["ID_Creator"].ToString()));
					}
					dctICDTen.Creator = dt.Rows[i]["Creator"].ToString();
					if (dt.Rows[i]["CreateDate"].ToString() != "")
					{
						dctICDTen.CreateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CreateDate"].ToString()));
					}
					if (dt.Rows[i]["Is_Banned"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Banned"].ToString() == "1" || dt.Rows[i]["Is_Banned"].ToString().ToLower() == "true")
						{
							dctICDTen.Is_Banned = new bool?(true);
						}
						else
						{
							dctICDTen.Is_Banned = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_BanOpr"].ToString() != "")
					{
						dctICDTen.ID_BanOpr = new int?(int.Parse(dt.Rows[i]["ID_BanOpr"].ToString()));
					}
					dctICDTen.BanOperator = dt.Rows[i]["BanOperator"].ToString();
					if (dt.Rows[i]["BanDate"].ToString() != "")
					{
						dctICDTen.BanDate = new DateTime?(DateTime.Parse(dt.Rows[i]["BanDate"].ToString()));
					}
					dctICDTen.BanDescribe = dt.Rows[i]["BanDescribe"].ToString();
					if (dt.Rows[i]["LevelA"].ToString() != "")
					{
						dctICDTen.LevelA = new int?(int.Parse(dt.Rows[i]["LevelA"].ToString()));
					}
					if (dt.Rows[i]["LevelB"].ToString() != "")
					{
						dctICDTen.LevelB = new int?(int.Parse(dt.Rows[i]["LevelB"].ToString()));
					}
					if (dt.Rows[i]["LevelC"].ToString() != "")
					{
						dctICDTen.LevelC = new int?(int.Parse(dt.Rows[i]["LevelC"].ToString()));
					}
					if (dt.Rows[i]["LevelD"].ToString() != "")
					{
						dctICDTen.LevelD = new int?(int.Parse(dt.Rows[i]["LevelD"].ToString()));
					}
					if (dt.Rows[i]["LevelE"].ToString() != "")
					{
						dctICDTen.LevelE = new int?(int.Parse(dt.Rows[i]["LevelE"].ToString()));
					}
					if (dt.Rows[i]["LevelTree"].ToString() != "")
					{
						dctICDTen.LevelTree = new int?(int.Parse(dt.Rows[i]["LevelTree"].ToString()));
					}
					dctICDTen.Class = dt.Rows[i]["Class"].ToString();
					dctICDTen.Tag = dt.Rows[i]["Tag"].ToString();
					dctICDTen.ICDtoSection = dt.Rows[i]["ICDtoSection"].ToString();
					dctICDTen.Note = dt.Rows[i]["Note"].ToString();
					dctICDTen.InputCode = dt.Rows[i]["InputCode"].ToString();
					list.Add(dctICDTen);
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
