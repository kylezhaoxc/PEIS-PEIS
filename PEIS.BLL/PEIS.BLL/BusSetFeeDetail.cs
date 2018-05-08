using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusSetFeeDetail
	{
		private readonly IBusSetFeeDetail dal = DataAccess.CreateBusSetFeeDetail();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_DtlSetFee)
		{
			return this.dal.Exists(ID_DtlSetFee);
		}

		public int Add(PEIS.Model.BusSetFeeDetail model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusSetFeeDetail model)
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

		public PEIS.Model.BusSetFeeDetail GetModel(int ID_DtlSetFee)
		{
			return this.dal.GetModel(ID_DtlSetFee);
		}

		public PEIS.Model.BusSetFeeDetail GetModelByCache(int ID_DtlSetFee)
		{
			string cacheKey = "BusSetFeeDetailModel-" + ID_DtlSetFee;
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
			return (PEIS.Model.BusSetFeeDetail)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusSetFeeDetail> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusSetFeeDetail> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusSetFeeDetail> list = new List<PEIS.Model.BusSetFeeDetail>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusSetFeeDetail busSetFeeDetail = new PEIS.Model.BusSetFeeDetail();
					if (dt.Rows[i]["ID_DtlSetFee"].ToString() != "")
					{
						busSetFeeDetail.ID_DtlSetFee = int.Parse(dt.Rows[i]["ID_DtlSetFee"].ToString());
					}
					if (dt.Rows[i]["PEPackageID"].ToString() != "")
					{
						busSetFeeDetail.PEPackageID = new int?(int.Parse(dt.Rows[i]["PEPackageID"].ToString()));
					}
					if (dt.Rows[i]["ID_FeeItem"].ToString() != "")
					{
						busSetFeeDetail.ID_FeeItem = new int?(int.Parse(dt.Rows[i]["ID_FeeItem"].ToString()));
					}
					list.Add(busSetFeeDetail);
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
