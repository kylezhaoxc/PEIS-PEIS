using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustExamSection
	{
		private static readonly OnCustExamSection _instance = new OnCustExamSection();

		private readonly IOnCustExamSection dal = DataAccess.CreateOnCustExamSection();

		public static OnCustExamSection Instance
		{
			get
			{
				return OnCustExamSection._instance;
			}
		}

		public int GetMaxId()
		{
			return this.dal.GetMaxId();
		}

		public bool Exists(int ID_CustExamSection)
		{
			return this.dal.Exists(ID_CustExamSection);
		}

		public int Add(PEIS.Model.OnCustExamSection model)
		{
			return this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustExamSection model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(int ID_CustExamSection)
		{
			return this.dal.Delete(ID_CustExamSection);
		}

		public bool DeleteList(string ID_CustExamSectionlist)
		{
			return this.dal.DeleteList(ID_CustExamSectionlist);
		}

		public PEIS.Model.OnCustExamSection GetModel(int ID_CustExamSection)
		{
			return this.dal.GetModel(ID_CustExamSection);
		}

		public PEIS.Model.OnCustExamSection GetModelByCache(int ID_CustExamSection)
		{
			string cacheKey = "OnCustExamSectionModel-" + ID_CustExamSection;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_CustExamSection);
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
			return (PEIS.Model.OnCustExamSection)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustExamSection> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustExamSection> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustExamSection> list = new List<PEIS.Model.OnCustExamSection>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustExamSection onCustExamSection = new PEIS.Model.OnCustExamSection();
					if (dt.Rows[i]["ID_CustExamSection"].ToString() != "")
					{
						onCustExamSection.ID_CustExamSection = int.Parse(dt.Rows[i]["ID_CustExamSection"].ToString());
					}
					if (dt.Rows[i]["ID_Customer"].ToString() != "")
					{
						onCustExamSection.ID_Customer = new long?(long.Parse(dt.Rows[i]["ID_Customer"].ToString()));
					}
					if (dt.Rows[i]["ID_Section"].ToString() != "")
					{
						onCustExamSection.ID_Section = new int?(int.Parse(dt.Rows[i]["ID_Section"].ToString()));
					}
					onCustExamSection.CustomerName = dt.Rows[i]["CustomerName"].ToString();
					onCustExamSection.SectionName = dt.Rows[i]["SectionName"].ToString();
					if (dt.Rows[i]["DiseaseLevel"].ToString() != "")
					{
						onCustExamSection.DiseaseLevel = new int?(int.Parse(dt.Rows[i]["DiseaseLevel"].ToString()));
					}
					if (dt.Rows[i]["SectionSummaryDate"].ToString() != "")
					{
						onCustExamSection.SectionSummaryDate = new DateTime?(DateTime.Parse(dt.Rows[i]["SectionSummaryDate"].ToString()));
					}
					onCustExamSection.SectionSummary = dt.Rows[i]["SectionSummary"].ToString();
					onCustExamSection.PositiveSummary = dt.Rows[i]["PositiveSummary"].ToString();
					if (dt.Rows[i]["ID_SummaryDoctor"].ToString() != "")
					{
						onCustExamSection.ID_SummaryDoctor = new int?(int.Parse(dt.Rows[i]["ID_SummaryDoctor"].ToString()));
					}
					onCustExamSection.SummaryDoctorName = dt.Rows[i]["SummaryDoctorName"].ToString();
					if (dt.Rows[i]["ID_Typist"].ToString() != "")
					{
						onCustExamSection.ID_Typist = new int?(int.Parse(dt.Rows[i]["ID_Typist"].ToString()));
					}
					onCustExamSection.TypistName = dt.Rows[i]["TypistName"].ToString();
					if (dt.Rows[i]["TypistDate"].ToString() != "")
					{
						onCustExamSection.TypistDate = new DateTime?(DateTime.Parse(dt.Rows[i]["TypistDate"].ToString()));
					}
					if (dt.Rows[i]["Is_Check"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Check"].ToString() == "1" || dt.Rows[i]["Is_Check"].ToString().ToLower() == "true")
						{
							onCustExamSection.Is_Check = new bool?(true);
						}
						else
						{
							onCustExamSection.Is_Check = new bool?(false);
						}
					}
					onCustExamSection.CheckerName = dt.Rows[i]["CheckerName"].ToString();
					if (dt.Rows[i]["CheckDate"].ToString() != "")
					{
						onCustExamSection.CheckDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CheckDate"].ToString()));
					}
					if (dt.Rows[i]["ID_Checker"].ToString() != "")
					{
						onCustExamSection.ID_Checker = new int?(int.Parse(dt.Rows[i]["ID_Checker"].ToString()));
					}
					if (dt.Rows[i]["IS_giveup"].ToString() != "")
					{
						if (dt.Rows[i]["IS_giveup"].ToString() == "1" || dt.Rows[i]["IS_giveup"].ToString().ToLower() == "true")
						{
							onCustExamSection.IS_giveup = new bool?(true);
						}
						else
						{
							onCustExamSection.IS_giveup = new bool?(false);
						}
					}
					if (dt.Rows[i]["IS_Refund"].ToString() != "")
					{
						if (dt.Rows[i]["IS_Refund"].ToString() == "1" || dt.Rows[i]["IS_Refund"].ToString().ToLower() == "true")
						{
							onCustExamSection.IS_Refund = new bool?(true);
						}
						else
						{
							onCustExamSection.IS_Refund = new bool?(false);
						}
					}
					onCustExamSection.ImageUrl = dt.Rows[i]["ImageUrl"].ToString();
					list.Add(onCustExamSection);
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
