using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustExamItem
	{
		private readonly IOnCustExamItem dal = DataAccess.CreateOnCustExamItem();

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_CustExamItem)
		{
			return this.dal.Exists(ID_CustExamItem);
		}

		public int Add(PEIS.Model.OnCustExamItem model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustExamItem model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_CustExamItem)
		{
			return this.dal.Delete(ID_CustExamItem);
		}

		public bool DeleteList(string ID_CustExamItemlist)
		{
			return this.dal.DeleteList(ID_CustExamItemlist);
		}

		public PEIS.Model.OnCustExamItem GetModel(int ID_CustExamItem)
		{
			return this.dal.GetModel(ID_CustExamItem);
		}

		public PEIS.Model.OnCustExamItem GetModelByCache(int ID_CustExamItem)
		{
			string cacheKey = "OnCustExamItemModel-" + ID_CustExamItem;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_CustExamItem);
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
			return (PEIS.Model.OnCustExamItem)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustExamItem> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustExamItem> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustExamItem> list = new List<PEIS.Model.OnCustExamItem>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustExamItem onCustExamItem = new PEIS.Model.OnCustExamItem();
					if (dt.Rows[i]["ID_CustExamItem"].ToString() != "")
					{
						onCustExamItem.ID_CustExamItem = int.Parse(dt.Rows[i]["ID_CustExamItem"].ToString());
					}
					if (dt.Rows[i]["ID_CustFee"].ToString() != "")
					{
						onCustExamItem.ID_CustFee = new int?(int.Parse(dt.Rows[i]["ID_CustFee"].ToString()));
					}
					if (dt.Rows[i]["ID_Fee"].ToString() != "")
					{
						onCustExamItem.ID_Fee = new int?(int.Parse(dt.Rows[i]["ID_Fee"].ToString()));
					}
					if (dt.Rows[i]["ID_ExamItem"].ToString() != "")
					{
						onCustExamItem.ID_ExamItem = int.Parse(dt.Rows[i]["ID_ExamItem"].ToString());
					}
					onCustExamItem.ExamItemName = dt.Rows[i]["ExamItemName"].ToString();
					if (dt.Rows[i]["DiseaseLevel"].ToString() != "")
					{
						onCustExamItem.DiseaseLevel = new int?(int.Parse(dt.Rows[i]["DiseaseLevel"].ToString()));
					}
					onCustExamItem.ResultLabValues = dt.Rows[i]["ResultLabValues"].ToString();
					if (dt.Rows[i]["ResultNumber"].ToString() != "")
					{
						onCustExamItem.ResultNumber = new decimal?(decimal.Parse(dt.Rows[i]["ResultNumber"].ToString()));
					}
					if (dt.Rows[i]["ResultLabLowLimit"].ToString() != "")
					{
						onCustExamItem.ResultLabLowLimit = new decimal?(decimal.Parse(dt.Rows[i]["ResultLabLowLimit"].ToString()));
					}
					if (dt.Rows[i]["ResultLabHighLimit"].ToString() != "")
					{
						onCustExamItem.ResultLabHighLimit = new decimal?(decimal.Parse(dt.Rows[i]["ResultLabHighLimit"].ToString()));
					}
					onCustExamItem.ResultLabUnit = dt.Rows[i]["ResultLabUnit"].ToString();
					onCustExamItem.ResultLabMark = dt.Rows[i]["ResultLabMark"].ToString();
					onCustExamItem.ResultSummary = dt.Rows[i]["ResultSummary"].ToString();
					if (dt.Rows[i]["ID_SummaryDoctor"].ToString() != "")
					{
						onCustExamItem.ID_SummaryDoctor = new int?(int.Parse(dt.Rows[i]["ID_SummaryDoctor"].ToString()));
					}
					onCustExamItem.SummaryDoctorName = dt.Rows[i]["SummaryDoctorName"].ToString();
					onCustExamItem.TypistName = dt.Rows[i]["TypistName"].ToString();
					if (dt.Rows[i]["ExamItemSummaryDate"].ToString() != "")
					{
						onCustExamItem.ExamItemSummaryDate = DateTime.Parse(dt.Rows[i]["ExamItemSummaryDate"].ToString());
					}
					if (dt.Rows[i]["ID_Typist"].ToString() != "")
					{
						onCustExamItem.ID_Typist = new int?(int.Parse(dt.Rows[i]["ID_Typist"].ToString()));
					}
					onCustExamItem.ResultLabRange = dt.Rows[i]["ResultLabRange"].ToString();
					if (dt.Rows[i]["ID_Customer"].ToString() != "")
					{
						onCustExamItem.ID_Customer = new long?(long.Parse(dt.Rows[i]["ID_Customer"].ToString()));
					}
					onCustExamItem.AbbrExamName = dt.Rows[i]["AbbrExamName"].ToString();
					onCustExamItem.DetectionMethod = dt.Rows[i]["DetectionMethod"].ToString();
					onCustExamItem.SCO = dt.Rows[i]["SCO"].ToString();
					onCustExamItem.ID_CustApply = dt.Rows[i]["ID_CustApply"].ToString();
					list.Add(onCustExamItem);
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
