using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnArcCust
	{
		private readonly IOnArcCust dal = DataAccess.CreateOnArcCust();

		private static readonly OnArcCust _instance = new OnArcCust();

		public static OnArcCust Instance
		{
			get
			{
				return OnArcCust._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_ArcCustomer)
		{
			return this.dal.Exists(ID_ArcCustomer);
		}

		public int Add(PEIS.Model.OnArcCust model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnArcCust model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_ArcCustomer)
		{
			return this.dal.Delete(ID_ArcCustomer);
		}

		public bool DeleteList(string ID_ArcCustomerlist)
		{
			return this.dal.DeleteList(ID_ArcCustomerlist);
		}

		public PEIS.Model.OnArcCust GetModel(int ID_ArcCustomer)
		{
			return this.dal.GetModel(ID_ArcCustomer);
		}

		public PEIS.Model.OnArcCust GetModelByCache(int ID_ArcCustomer)
		{
			string cacheKey = "OnArcCustModel-" + ID_ArcCustomer;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_ArcCustomer);
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
			return (PEIS.Model.OnArcCust)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnArcCust> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnArcCust> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnArcCust> list = new List<PEIS.Model.OnArcCust>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnArcCust onArcCust = new PEIS.Model.OnArcCust();
					if (dt.Rows[i]["ID_ArcCustomer"].ToString() != "")
					{
						onArcCust.ID_ArcCustomer = int.Parse(dt.Rows[i]["ID_ArcCustomer"].ToString());
					}
					if (dt.Rows[i]["ID_Gender"].ToString() != "")
					{
						onArcCust.ID_Gender = new int?(int.Parse(dt.Rows[i]["ID_Gender"].ToString()));
					}
					if (dt.Rows[i]["ID_Marriage"].ToString() != "")
					{
						onArcCust.ID_Marriage = new int?(int.Parse(dt.Rows[i]["ID_Marriage"].ToString()));
					}
					if (dt.Rows[i]["NationID"].ToString() != "")
					{
						onArcCust.NationID = new int?(int.Parse(dt.Rows[i]["NationID"].ToString()));
					}
					if (dt.Rows[i]["CultrulID"].ToString() != "")
					{
						onArcCust.CultrulID = new int?(int.Parse(dt.Rows[i]["CultrulID"].ToString()));
					}
					if (dt.Rows[i]["VocationID"].ToString() != "")
					{
						onArcCust.VocationID = new int?(int.Parse(dt.Rows[i]["VocationID"].ToString()));
					}
					onArcCust.CustomerName = dt.Rows[i]["CustomerName"].ToString();
					onArcCust.IDCard = dt.Rows[i]["IDCard"].ToString();
					onArcCust.ExamCard = dt.Rows[i]["ExamCard"].ToString();
					if (dt.Rows[i]["Photo"].ToString() != "")
					{
						onArcCust.Photo = (byte[])dt.Rows[i]["Photo"];
					}
					if (dt.Rows[i]["BirthDay"].ToString() != "")
					{
						onArcCust.BirthDay = new DateTime?(DateTime.Parse(dt.Rows[i]["BirthDay"].ToString()));
					}
					onArcCust.GenderName = dt.Rows[i]["GenderName"].ToString();
					onArcCust.MarriageName = dt.Rows[i]["MarriageName"].ToString();
					onArcCust.NationName = dt.Rows[i]["NationName"].ToString();
					onArcCust.Address = dt.Rows[i]["Address"].ToString();
					onArcCust.MobileNo = dt.Rows[i]["MobileNo"].ToString();
					onArcCust.Email = dt.Rows[i]["Email"].ToString();
					onArcCust.CultrulName = dt.Rows[i]["CultrulName"].ToString();
					onArcCust.VocationName = dt.Rows[i]["VocationName"].ToString();
					if (dt.Rows[i]["FinishedNum"].ToString() != "")
					{
						onArcCust.FinishedNum = int.Parse(dt.Rows[i]["FinishedNum"].ToString());
					}
					if (dt.Rows[i]["UnfinishedNum"].ToString() != "")
					{
						onArcCust.UnfinishedNum = new int?(int.Parse(dt.Rows[i]["UnfinishedNum"].ToString()));
					}
					if (dt.Rows[i]["FirstDatePE"].ToString() != "")
					{
						onArcCust.FirstDatePE = new DateTime?(DateTime.Parse(dt.Rows[i]["FirstDatePE"].ToString()));
					}
					if (dt.Rows[i]["LatestDatePE"].ToString() != "")
					{
						onArcCust.LatestDatePE = new DateTime?(DateTime.Parse(dt.Rows[i]["LatestDatePE"].ToString()));
					}
					list.Add(onArcCust);
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
