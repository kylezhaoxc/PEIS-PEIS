using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustBackLog
	{
		private readonly IOnCustBackLog dal = DataAccess.CreateOnCustBackLog();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_BackLog)
		{
			return this.dal.Exists(ID_BackLog);
		}

		public int AddNew(PEIS.Model.OnCustBackLog model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustBackLog model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_BackLog)
		{
			return this.dal.Delete(ID_BackLog);
		}

		public bool DeleteList(string ID_BackLoglist)
		{
			return this.dal.DeleteList(ID_BackLoglist);
		}

		public PEIS.Model.OnCustBackLog GetModel(int ID_BackLog)
		{
			return this.dal.GetModel(ID_BackLog);
		}

		public PEIS.Model.OnCustBackLog GetModelByCache(int ID_BackLog)
		{
			string cacheKey = "OnCustBackLogModel-" + ID_BackLog;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_BackLog);
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
			return (PEIS.Model.OnCustBackLog)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustBackLog> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustBackLog> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustBackLog> list = new List<PEIS.Model.OnCustBackLog>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustBackLog onCustBackLog = new PEIS.Model.OnCustBackLog();
					if (dt.Rows[i]["ID_BackLog"].ToString() != "")
					{
						onCustBackLog.ID_BackLog = (long)int.Parse(dt.Rows[i]["ID_BackLog"].ToString());
					}
					if (dt.Rows[i]["ID_Customer"].ToString() != "")
					{
						onCustBackLog.ID_Customer = long.Parse(dt.Rows[i]["ID_Customer"].ToString());
					}
					if (dt.Rows[i]["ID_BackLogType"].ToString() != "")
					{
						onCustBackLog.ID_BackLogType = int.Parse(dt.Rows[i]["ID_BackLogType"].ToString());
					}
					if (dt.Rows[i]["CreateDate"].ToString() != "")
					{
						onCustBackLog.CreateDate = DateTime.Parse(dt.Rows[i]["CreateDate"].ToString());
					}
					if (dt.Rows[i]["OperateDate"].ToString() != "")
					{
						onCustBackLog.OperateDate = DateTime.Parse(dt.Rows[i]["OperateDate"].ToString());
					}
					if (dt.Rows[i]["Is_Finished"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Finished"].ToString() == "1" || dt.Rows[i]["Is_Finished"].ToString().ToLower() == "true")
						{
							onCustBackLog.Is_Finished = new bool?(true);
						}
						else
						{
							onCustBackLog.Is_Finished = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_Operator"].ToString() != "")
					{
						onCustBackLog.ID_Operator = int.Parse(dt.Rows[i]["ID_Operator"].ToString());
					}
					if (dt.Rows[i]["ID_Operator"].ToString() != "")
					{
						onCustBackLog.ID_Operator = int.Parse(dt.Rows[i]["ID_Operator"].ToString());
					}
					onCustBackLog.Operator = dt.Rows[i]["Operator"].ToString();
					list.Add(onCustBackLog);
				}
			}
			return list;
		}

		public DataSet GetAllList()
		{
			return this.GetList("");
		}

		public int AddOrUpdateByBackLogType(PEIS.Model.OnCustBackLog model)
		{
			return this.dal.AddOrUpdateByBackLogType(model);
		}
	}
}
