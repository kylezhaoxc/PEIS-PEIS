using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnFianlCheck
	{
		private readonly IOnFianlCheck dal = DataAccess.CreateOnFianlCheck();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_FinalCheck)
		{
			return this.dal.Exists(ID_FinalCheck);
		}

		public int Add(PEIS.Model.OnFianlCheck model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnFianlCheck model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_FinalCheck)
		{
			return this.dal.Delete(ID_FinalCheck);
		}

		public bool DeleteList(string ID_FinalChecklist)
		{
			return this.dal.DeleteList(ID_FinalChecklist);
		}

		public PEIS.Model.OnFianlCheck GetModel(int ID_FinalCheck)
		{
			return this.dal.GetModel(ID_FinalCheck);
		}

		public PEIS.Model.OnFianlCheck GetModelByCache(int ID_FinalCheck)
		{
			string cacheKey = "OnFianlCheckModel-" + ID_FinalCheck;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_FinalCheck);
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
			return (PEIS.Model.OnFianlCheck)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnFianlCheck> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnFianlCheck> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnFianlCheck> list = new List<PEIS.Model.OnFianlCheck>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnFianlCheck onFianlCheck = new PEIS.Model.OnFianlCheck();
					if (dt.Rows[i]["ID_FinalCheck"].ToString() != "")
					{
						onFianlCheck.ID_FinalCheck = int.Parse(dt.Rows[i]["ID_FinalCheck"].ToString());
					}
					if (dt.Rows[i]["ID_Customer"].ToString() != "")
					{
						onFianlCheck.ID_Customer = new long?(long.Parse(dt.Rows[i]["ID_Customer"].ToString()));
					}
					onFianlCheck.CustomerName = dt.Rows[i]["CustomerName"].ToString();
					if (dt.Rows[i]["ID_FinalDoctor"].ToString() != "")
					{
						onFianlCheck.ID_FinalDoctor = new int?(int.Parse(dt.Rows[i]["ID_FinalDoctor"].ToString()));
					}
					onFianlCheck.FinalDoctor = dt.Rows[i]["FinalDoctor"].ToString();
					if (dt.Rows[i]["SubmitDate"].ToString() != "")
					{
						onFianlCheck.SubmitDate = new DateTime?(DateTime.Parse(dt.Rows[i]["SubmitDate"].ToString()));
					}
					if (dt.Rows[i]["ID_FinalCheckDoctor"].ToString() != "")
					{
						onFianlCheck.ID_FinalCheckDoctor = new int?(int.Parse(dt.Rows[i]["ID_FinalCheckDoctor"].ToString()));
					}
					onFianlCheck.FinalCheckDoctor = dt.Rows[i]["FinalCheckDoctor"].ToString();
					if (dt.Rows[i]["FinaleCheckDate"].ToString() != "")
					{
						onFianlCheck.FinaleCheckDate = new DateTime?(DateTime.Parse(dt.Rows[i]["FinaleCheckDate"].ToString()));
					}
					if (dt.Rows[i]["Is_Pass"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Pass"].ToString() == "1" || dt.Rows[i]["Is_Pass"].ToString().ToLower() == "true")
						{
							onFianlCheck.Is_Pass = new bool?(true);
						}
						else
						{
							onFianlCheck.Is_Pass = new bool?(false);
						}
					}
					onFianlCheck.RefuseReason = dt.Rows[i]["RefuseReason"].ToString();
					list.Add(onFianlCheck);
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
