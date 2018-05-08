using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustRelationCustPEInfo
	{
		private static readonly OnCustRelationCustPEInfo _instance = new OnCustRelationCustPEInfo();

		private readonly IOnCustRelationCustPEInfo dal = DataAccess.CreateOnCustRelationCustPEInfo();

		public static OnCustRelationCustPEInfo Instance
		{
			get
			{
				return OnCustRelationCustPEInfo._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_CustRelation)
		{
			return this.dal.Exists(ID_CustRelation);
		}

		public int Add(PEIS.Model.OnCustRelationCustPEInfo model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustRelationCustPEInfo model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_CustRelation)
		{
			return this.dal.Delete(ID_CustRelation);
		}

		public bool DeleteList(string ID_CustRelationlist)
		{
			return this.dal.DeleteList(ID_CustRelationlist);
		}

		public PEIS.Model.OnCustRelationCustPEInfo GetModel(int ID_CustRelation)
		{
			return this.dal.GetModel(ID_CustRelation);
		}

		public PEIS.Model.OnCustRelationCustPEInfo GetModelByCache(int ID_CustRelation)
		{
			string cacheKey = "OnCustRelationCustPEInfoModel-" + ID_CustRelation;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_CustRelation);
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
			return (PEIS.Model.OnCustRelationCustPEInfo)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustRelationCustPEInfo> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustRelationCustPEInfo> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustRelationCustPEInfo> list = new List<PEIS.Model.OnCustRelationCustPEInfo>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustRelationCustPEInfo onCustRelationCustPEInfo = new PEIS.Model.OnCustRelationCustPEInfo();
					if (dt.Rows[i]["ID_CustRelation"].ToString() != "")
					{
						onCustRelationCustPEInfo.ID_CustRelation = int.Parse(dt.Rows[i]["ID_CustRelation"].ToString());
					}
					if (dt.Rows[i]["ID_ArcCustomer"].ToString() != "")
					{
						onCustRelationCustPEInfo.ID_ArcCustomer = new int?(int.Parse(dt.Rows[i]["ID_ArcCustomer"].ToString()));
					}
					onCustRelationCustPEInfo.IDCardNo = dt.Rows[i]["IDCardNo"].ToString();
					onCustRelationCustPEInfo.ExamCardNo = dt.Rows[i]["ExamCardNo"].ToString();
					if (dt.Rows[i]["ID_Customer"].ToString() != "")
					{
						onCustRelationCustPEInfo.ID_Customer = new long?(long.Parse(dt.Rows[i]["ID_Customer"].ToString()));
					}
					if (dt.Rows[i]["Is_CompletePhysical"].ToString() != "")
					{
						if (dt.Rows[i]["Is_CompletePhysical"].ToString() == "1" || dt.Rows[i]["Is_CompletePhysical"].ToString().ToLower() == "true")
						{
							onCustRelationCustPEInfo.Is_CompletePhysical = new bool?(true);
						}
						else
						{
							onCustRelationCustPEInfo.Is_CompletePhysical = new bool?(false);
						}
					}
					if (dt.Rows[i]["ExamState"].ToString() != "")
					{
						onCustRelationCustPEInfo.ExamState = new int?(int.Parse(dt.Rows[i]["ExamState"].ToString()));
					}
					list.Add(onCustRelationCustPEInfo);
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
