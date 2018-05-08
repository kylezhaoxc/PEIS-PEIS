using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class BusFeeDetail
	{
		private readonly IBusFeeDetail dal = DataAccess.CreateBusFeeDetail();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_DtlFee)
		{
			return this.dal.Exists(ID_DtlFee);
		}

		public int Add(PEIS.Model.BusFeeDetail model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.BusFeeDetail model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_DtlFee)
		{
			return this.dal.Delete(ID_DtlFee);
		}

		public bool DeleteList(string ID_DtlFeelist)
		{
			return this.dal.DeleteList(ID_DtlFeelist);
		}

		public PEIS.Model.BusFeeDetail GetModel(int ID_DtlFee)
		{
			return this.dal.GetModel(ID_DtlFee);
		}

		public PEIS.Model.BusFeeDetail GetModelByCache(int ID_DtlFee)
		{
			string cacheKey = "BusFeeDetailModel-" + ID_DtlFee;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_DtlFee);
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
			return (PEIS.Model.BusFeeDetail)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.BusFeeDetail> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.BusFeeDetail> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.BusFeeDetail> list = new List<PEIS.Model.BusFeeDetail>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.BusFeeDetail busFeeDetail = new PEIS.Model.BusFeeDetail();
					if (dt.Rows[i]["ID_DtlFee"].ToString() != "")
					{
						busFeeDetail.ID_DtlFee = int.Parse(dt.Rows[i]["ID_DtlFee"].ToString());
					}
					if (dt.Rows[i]["ID_Fee"].ToString() != "")
					{
						busFeeDetail.ID_Fee = new int?(int.Parse(dt.Rows[i]["ID_Fee"].ToString()));
					}
					if (dt.Rows[i]["ID_ExamItem"].ToString() != "")
					{
						busFeeDetail.ID_ExamItem = new int?(int.Parse(dt.Rows[i]["ID_ExamItem"].ToString()));
					}
					list.Add(busFeeDetail);
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
