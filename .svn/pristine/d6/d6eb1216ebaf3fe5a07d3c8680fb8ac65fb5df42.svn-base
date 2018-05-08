using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusBackLogType
	{
		private readonly IBusBackLogType dal = DataAccess.CreateBusBackLogType();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_DtlSetFee)
		{
			return this.dal.Exists(ID_DtlSetFee);
		}

		public int Add(PEIS.Model.BusBackLogType model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusBackLogType model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_DtlSetFee)
		{
			return this.dal.Delete(ID_DtlSetFee);
		}

		public bool DeleteList(string ID_DtlSetFeelist)
		{
			return this.dal.DeleteList(ID_DtlSetFeelist);
		}

		public PEIS.Model.BusBackLogType GetModel(int ID_DtlSetFee)
		{
			return this.dal.GetModel(ID_DtlSetFee);
		}

		public PEIS.Model.BusBackLogType GetModelByCache(int ID_DtlSetFee)
		{
			string cacheKey = "BusBackLogTypeModel-" + ID_DtlSetFee;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_DtlSetFee);
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
			return (PEIS.Model.BusBackLogType)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusBackLogType> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusBackLogType> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusBackLogType> list = new List<PEIS.Model.BusBackLogType>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusBackLogType busBackLogType = new PEIS.Model.BusBackLogType();
					if (dt.Rows[i]["ID_BackLogType"].ToString() != "")
					{
						busBackLogType.ID_BackLogType = int.Parse(dt.Rows[i]["ID_BackLogType"].ToString());
					}
					busBackLogType.BackLogTypeName = dt.Rows[i]["BackLogTypeName"].ToString();
					if (dt.Rows[i]["Is_Banned"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Banned"].ToString() == "1" || dt.Rows[i]["Is_Banned"].ToString().ToLower() == "true")
						{
							busBackLogType.Is_Banned = new bool?(true);
						}
						else
						{
							busBackLogType.Is_Banned = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_Operator"].ToString() != "")
					{
						busBackLogType.ID_Operator = int.Parse(dt.Rows[i]["ID_Operator"].ToString());
					}
					if (dt.Rows[i]["DispOrder"].ToString() != "")
					{
						busBackLogType.DispOrder = int.Parse(dt.Rows[i]["DispOrder"].ToString());
					}
					busBackLogType.BanDescribe = dt.Rows[i]["BanDescribe"].ToString();
					busBackLogType.InputCode = dt.Rows[i]["InputCode"].ToString();
					busBackLogType.Operator = dt.Rows[i]["Operator"].ToString();
					if (dt.Rows[i]["OperateDate"].ToString() != "")
					{
						busBackLogType.OperateDate = DateTime.Parse(dt.Rows[i]["OperateDate"].ToString());
					}
					if (dt.Rows[i]["OperateType"].ToString() != "")
					{
						busBackLogType.OperateType = int.Parse(dt.Rows[i]["OperateDate"].ToString());
					}
					list.Add(busBackLogType);
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
