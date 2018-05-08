using PEIS.DALFactory;
using PEIS.IDAL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.BLL
{
	public class OnCustPhysicalExamInfo
	{
		private static readonly OnCustPhysicalExamInfo _instance = new OnCustPhysicalExamInfo();

		private readonly IOnCustPhysicalExamInfo dal = DataAccess.CreateOnCustPhysicalExamInfo();

		public static OnCustPhysicalExamInfo Instance
		{
			get
			{
				return OnCustPhysicalExamInfo._instance;
			}
		}

		public bool Exists(long ID_Customer)
		{
			return this.dal.Exists(ID_Customer);
		}

		public void Add(PEIS.Model.OnCustPhysicalExamInfo model)
		{
			this.dal.Add(model);
		}

		public bool Update(PEIS.Model.OnCustPhysicalExamInfo model)
		{
			return this.dal.Update(model);
		}

		public bool Delete(long ID_Customer)
		{
			return this.dal.Delete(ID_Customer);
		}

		public bool DeleteList(string ID_Customerlist)
		{
			return this.dal.DeleteList(ID_Customerlist);
		}

		public PEIS.Model.OnCustPhysicalExamInfo GetModel(long ID_Customer)
		{
			return this.dal.GetModel(ID_Customer);
		}

		public PEIS.Model.OnCustPhysicalExamInfo GetModelByCache(long ID_Customer)
		{
			string cacheKey = "OnCustPhysicalExamInfoModel-" + ID_Customer;
			object obj = Maticsoft.Common.DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					obj = this.dal.GetModel(ID_Customer);
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
			return (PEIS.Model.OnCustPhysicalExamInfo)obj;
		}

		public DataSet GetList(string strWhere)
		{
			return this.dal.GetList(strWhere);
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			return this.dal.GetList(Top, strWhere, filedOrder);
		}

		public List<PEIS.Model.OnCustPhysicalExamInfo> GetModelList(string strWhere)
		{
			DataSet list = this.dal.GetList(strWhere);
			return this.DataTableToList(list.Tables[0]);
		}

		public List<PEIS.Model.OnCustPhysicalExamInfo> DataTableToList(DataTable dt)
		{
			List<PEIS.Model.OnCustPhysicalExamInfo> list = new List<PEIS.Model.OnCustPhysicalExamInfo>();
			int count = dt.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					PEIS.Model.OnCustPhysicalExamInfo onCustPhysicalExamInfo = new PEIS.Model.OnCustPhysicalExamInfo();
					if (dt.Rows[i]["ID_Customer"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_Customer = long.Parse(dt.Rows[i]["ID_Customer"].ToString());
					}
					onCustPhysicalExamInfo.CustomerName = dt.Rows[i]["CustomerName"].ToString();
					if (dt.Rows[i]["Is_Team"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Team"].ToString() == "1" || dt.Rows[i]["Is_Team"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_Team = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_Team = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_Team"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_Team = new int?(int.Parse(dt.Rows[i]["ID_Team"].ToString()));
					}
					if (dt.Rows[i]["Is_Paused"].ToString() != "")
					{
						onCustPhysicalExamInfo.Is_Paused = new int?(int.Parse(dt.Rows[i]["Is_Paused"].ToString()));
					}
					if (dt.Rows[i]["Is_SectionLock"].ToString() != "")
					{
						if (dt.Rows[i]["Is_SectionLock"].ToString() == "1" || dt.Rows[i]["Is_SectionLock"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_SectionLock = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_SectionLock = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_ExamType"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_ExamType = new int?(int.Parse(dt.Rows[i]["ID_ExamType"].ToString()));
					}
					onCustPhysicalExamInfo.ExamTypeName = dt.Rows[i]["ExamTypeName"].ToString();
					if (dt.Rows[i]["PEPackageID"].ToString() != "")
					{
						onCustPhysicalExamInfo.PEPackageID = new int?(int.Parse(dt.Rows[i]["PEPackageID"].ToString()));
					}
					onCustPhysicalExamInfo.PEPackageName = dt.Rows[i]["PEPackageName"].ToString();
					if (dt.Rows[i]["ExamPlaceID"].ToString() != "")
					{
						onCustPhysicalExamInfo.ExamPlaceID = new int?(int.Parse(dt.Rows[i]["ExamPlaceID"].ToString()));
					}
					onCustPhysicalExamInfo.ExamPlaceName = dt.Rows[i]["ExamPlaceName"].ToString();
					if (dt.Rows[i]["ID_GuideNurse"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_GuideNurse = new int?(int.Parse(dt.Rows[i]["ID_GuideNurse"].ToString()));
					}
					onCustPhysicalExamInfo.GuideNurse = dt.Rows[i]["GuideNurse"].ToString();
					if (dt.Rows[i]["ID_ReportWay"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_ReportWay = new int?(int.Parse(dt.Rows[i]["ID_ReportWay"].ToString()));
					}
					onCustPhysicalExamInfo.ReportWayName = dt.Rows[i]["ReportWayName"].ToString();
					if (dt.Rows[i]["ID_FeeWay"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_FeeWay = new int?(int.Parse(dt.Rows[i]["ID_FeeWay"].ToString()));
					}
					onCustPhysicalExamInfo.FeeWayName = dt.Rows[i]["FeeWayName"].ToString();
					if (dt.Rows[i]["SecurityLevel"].ToString() != "")
					{
						onCustPhysicalExamInfo.SecurityLevel = new int?(int.Parse(dt.Rows[i]["SecurityLevel"].ToString()));
					}
					if (dt.Rows[i]["DiseaseLevel"].ToString() != "")
					{
						onCustPhysicalExamInfo.DiseaseLevel = new int?(int.Parse(dt.Rows[i]["DiseaseLevel"].ToString()));
					}
					if (dt.Rows[i]["Is_FeeSettled"].ToString() != "")
					{
						if (dt.Rows[i]["Is_FeeSettled"].ToString() == "1" || dt.Rows[i]["Is_FeeSettled"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_FeeSettled = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_FeeSettled = new bool?(false);
						}
					}
					if (dt.Rows[i]["Is_GuideSheetPrinted"].ToString() != "")
					{
						if (dt.Rows[i]["Is_GuideSheetPrinted"].ToString() == "1" || dt.Rows[i]["Is_GuideSheetPrinted"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_GuideSheetPrinted = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_GuideSheetPrinted = new bool?(false);
						}
					}
					if (dt.Rows[i]["Is_GuideSheetReturned"].ToString() != "")
					{
						if (dt.Rows[i]["Is_GuideSheetReturned"].ToString() == "1" || dt.Rows[i]["Is_GuideSheetReturned"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_GuideSheetReturned = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_GuideSheetReturned = new bool?(false);
						}
					}
					if (dt.Rows[i]["GuideSheetReturnedDate"].ToString() != "")
					{
						onCustPhysicalExamInfo.GuideSheetReturnedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["GuideSheetReturnedDate"].ToString()));
					}
					onCustPhysicalExamInfo.GuideSheetReturnedby = dt.Rows[i]["GuideSheetReturnedby"].ToString();
					if (dt.Rows[i]["Is_UseHideCode"].ToString() != "")
					{
						if (dt.Rows[i]["Is_UseHideCode"].ToString() == "1" || dt.Rows[i]["Is_UseHideCode"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_UseHideCode = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_UseHideCode = new bool?(false);
						}
					}
					if (dt.Rows[i]["Is_FinalFinished"].ToString() != "")
					{
						if (dt.Rows[i]["Is_FinalFinished"].ToString() == "1" || dt.Rows[i]["Is_FinalFinished"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_FinalFinished = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_FinalFinished = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_FinalDoctor"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_FinalDoctor = new int?(int.Parse(dt.Rows[i]["ID_FinalDoctor"].ToString()));
					}
					onCustPhysicalExamInfo.FinalDoctor = dt.Rows[i]["FinalDoctor"].ToString();
					if (dt.Rows[i]["FinalDate"].ToString() != "")
					{
						onCustPhysicalExamInfo.FinalDate = new DateTime?(DateTime.Parse(dt.Rows[i]["FinalDate"].ToString()));
					}
					onCustPhysicalExamInfo.FinalOverView = dt.Rows[i]["FinalOverView"].ToString();
					onCustPhysicalExamInfo.FinalConclusion = dt.Rows[i]["FinalConclusion"].ToString();
					onCustPhysicalExamInfo.ResultCompare = dt.Rows[i]["ResultCompare"].ToString();
					onCustPhysicalExamInfo.MainDiagnose = dt.Rows[i]["MainDiagnose"].ToString();
					onCustPhysicalExamInfo.FinalDietGuide = dt.Rows[i]["FinalDietGuide"].ToString();
					onCustPhysicalExamInfo.FinalSportGuide = dt.Rows[i]["FinalSportGuide"].ToString();
					onCustPhysicalExamInfo.FinalHealthKnowlage = dt.Rows[i]["FinalHealthKnowlage"].ToString();
					if (dt.Rows[i]["Is_Checked"].ToString() != "")
					{
						if (dt.Rows[i]["Is_Checked"].ToString() == "1" || dt.Rows[i]["Is_Checked"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_Checked = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_Checked = new bool?(false);
						}
					}
					if (dt.Rows[i]["ID_Checker"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_Checker = new int?(int.Parse(dt.Rows[i]["ID_Checker"].ToString()));
					}
					onCustPhysicalExamInfo.Checker = dt.Rows[i]["Checker"].ToString();
					if (dt.Rows[i]["CheckedDate"].ToString() != "")
					{
						onCustPhysicalExamInfo.CheckedDate = new DateTime?(DateTime.Parse(dt.Rows[i]["CheckedDate"].ToString()));
					}
					if (dt.Rows[i]["Is_ReportReceipted"].ToString() != "")
					{
						if (dt.Rows[i]["Is_ReportReceipted"].ToString() == "1" || dt.Rows[i]["Is_ReportReceipted"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_ReportReceipted = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_ReportReceipted = new bool?(false);
						}
					}
					if (dt.Rows[i]["SubScribDate"].ToString() != "")
					{
						onCustPhysicalExamInfo.SubScribDate = new DateTime?(DateTime.Parse(dt.Rows[i]["SubScribDate"].ToString()));
					}
					if (dt.Rows[i]["OperateDate"].ToString() != "")
					{
						onCustPhysicalExamInfo.OperateDate = new DateTime?(DateTime.Parse(dt.Rows[i]["OperateDate"].ToString()));
					}
					if (dt.Rows[i]["ID_Operator"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_Operator = new int?(int.Parse(dt.Rows[i]["ID_Operator"].ToString()));
					}
					onCustPhysicalExamInfo.Operator = dt.Rows[i]["Operator"].ToString();
					onCustPhysicalExamInfo.Note = dt.Rows[i]["Note"].ToString();
					if (dt.Rows[i]["Is_Subscribed"].ToString() != "")
					{
						onCustPhysicalExamInfo.Is_Subscribed = new int?(int.Parse(dt.Rows[i]["Is_Subscribed"].ToString()));
					}
					onCustPhysicalExamInfo.Invoice = dt.Rows[i]["Invoice"].ToString();
					if (dt.Rows[i]["ID_UserGuideSheetReturnedBy"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_UserGuideSheetReturnedBy = new int?(int.Parse(dt.Rows[i]["ID_UserGuideSheetReturnedBy"].ToString()));
					}
					if (dt.Rows[i]["ID_SubScriber"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_SubScriber = new int?(int.Parse(dt.Rows[i]["ID_SubScriber"].ToString()));
					}
					onCustPhysicalExamInfo.SubScriber = dt.Rows[i]["SubScriber"].ToString();
					if (dt.Rows[i]["SubScriberOperDate"].ToString() != "")
					{
						onCustPhysicalExamInfo.SubScriberOperDate = new DateTime?(DateTime.Parse(dt.Rows[i]["SubScriberOperDate"].ToString()));
					}
					if (dt.Rows[i]["Is_ExamStarted"].ToString() != "")
					{
						if (dt.Rows[i]["Is_ExamStarted"].ToString() == "1" || dt.Rows[i]["Is_ExamStarted"].ToString().ToLower() == "true")
						{
							onCustPhysicalExamInfo.Is_ExamStarted = new bool?(true);
						}
						else
						{
							onCustPhysicalExamInfo.Is_ExamStarted = new bool?(false);
						}
					}
					onCustPhysicalExamInfo.TeamName = dt.Rows[i]["TeamName"].ToString();
					onCustPhysicalExamInfo.IndicatorDiagnose = dt.Rows[i]["IndicatorDiagnose"].ToString();
					onCustPhysicalExamInfo.OtherDiagnose = dt.Rows[i]["OtherDiagnose"].ToString();
					if (dt.Rows[i]["ID_Gender"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_Gender = new int?(int.Parse(dt.Rows[i]["ID_Gender"].ToString()));
					}
					onCustPhysicalExamInfo.GenderName = dt.Rows[i]["GenderName"].ToString();
					if (dt.Rows[i]["ID_Marriage"].ToString() != "")
					{
						onCustPhysicalExamInfo.ID_Marriage = new int?(int.Parse(dt.Rows[i]["ID_Marriage"].ToString()));
					}
					onCustPhysicalExamInfo.MarriageName = dt.Rows[i]["MarriageName"].ToString();
					if (dt.Rows[i]["NationID"].ToString() != "")
					{
						onCustPhysicalExamInfo.NationID = new int?(int.Parse(dt.Rows[i]["NationID"].ToString()));
					}
					onCustPhysicalExamInfo.NationName = dt.Rows[i]["NationName"].ToString();
					if (dt.Rows[i]["CultrulID"].ToString() != "")
					{
						onCustPhysicalExamInfo.CultrulID = new int?(int.Parse(dt.Rows[i]["CultrulID"].ToString()));
					}
					onCustPhysicalExamInfo.CultrulName = dt.Rows[i]["CultrulName"].ToString();
					if (dt.Rows[i]["VocationID"].ToString() != "")
					{
						onCustPhysicalExamInfo.VocationID = new int?(int.Parse(dt.Rows[i]["VocationID"].ToString()));
					}
					onCustPhysicalExamInfo.VocationName = dt.Rows[i]["VocationName"].ToString();
					onCustPhysicalExamInfo.IDCard = dt.Rows[i]["IDCard"].ToString();
					onCustPhysicalExamInfo.ExamCard = dt.Rows[i]["ExamCard"].ToString();
					if (dt.Rows[i]["Photo"].ToString() != "")
					{
						onCustPhysicalExamInfo.Photo = (byte[])dt.Rows[i]["Photo"];
					}
					if (dt.Rows[i]["BirthDay"].ToString() != "")
					{
						onCustPhysicalExamInfo.BirthDay = new DateTime?(DateTime.Parse(dt.Rows[i]["BirthDay"].ToString()));
					}
					onCustPhysicalExamInfo.Address = dt.Rows[i]["Address"].ToString();
					onCustPhysicalExamInfo.MobileNo = dt.Rows[i]["MobileNo"].ToString();
					onCustPhysicalExamInfo.Email = dt.Rows[i]["Email"].ToString();
					onCustPhysicalExamInfo.SecondaryDiagnose = dt.Rows[i]["SecondaryDiagnose"].ToString();
					onCustPhysicalExamInfo.NormalDiagnose = dt.Rows[i]["NormalDiagnose"].ToString();
					list.Add(onCustPhysicalExamInfo);
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
