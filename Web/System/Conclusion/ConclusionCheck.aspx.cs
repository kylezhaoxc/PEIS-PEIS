using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;
using System.Collections.Generic;
using System.Data;

namespace PEIS.Web.System.Conclusion
{
	public class ConclusionCheck : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "总检");
			long @int = base.GetInt64("txtCustomerID", 0L);
			if (@int > 0L)
			{
				vltContext.Put("txtCustomerID", @int.ToString());
			}
			this.GetCustomerExamInfo(ref vltContext);
		}

		protected void GetCustomerExamInfo(ref VelocityContext vltContext)
		{
			long @int = base.GetInt64("txtCustomerID", 0L);
			int int2 = base.GetInt("txtSectionID", 0);
			vltContext.Put("txtSectionID", int2.ToString());
			if (@int > 0L)
			{
				vltContext.Put("txtCustomerID", @int.ToString());
			}
			this.GetCustomerInfo(@int, ref vltContext);
		}

		protected void GetCustomerInfo(long ID_Customer, ref VelocityContext vltContext)
		{
			DataSet custRelationCustPEInfo = CommonCustExam.Instance.GetCustRelationCustPEInfo(ID_Customer, "", "");
			if (custRelationCustPEInfo != null && 0 < custRelationCustPEInfo.Tables[0].Rows.Count)
			{
				List<PEIS.Model.OnCustRelationCustPEInfo> list = PEIS.BLL.OnCustRelationCustPEInfo.Instance.DataTableToList(custRelationCustPEInfo.Tables[0]);
				if (list != null)
				{
					vltContext.Put("ExamState", list[0].ExamState);
					PEIS.Model.OnCustPhysicalExamInfo onCustPhysicalExamInfo = null;
					PEIS.Model.OnArcCust modelByCache = PEIS.BLL.OnArcCust.Instance.GetModelByCache(int.Parse(list[0].ID_ArcCustomer.ToString()));
					if (list[0].ExamState == 0)
					{
						onCustPhysicalExamInfo = PEIS.BLL.OnCustPhysicalExamInfo.Instance.GetModel(list[0].ID_Customer.Value);
					}
					if (modelByCache != null)
					{
						vltContext.Put("ExamCardNo", modelByCache.ExamCard);
						vltContext.Put("IDCardNo", modelByCache.IDCard);
						vltContext.Put("CustomerName", modelByCache.CustomerName);
						vltContext.Put("MarriageName", modelByCache.MarriageName);
						vltContext.Put("GenderName", modelByCache.GenderName);
						vltContext.Put("MobileNo", modelByCache.MobileNo);
						int finishedNum = modelByCache.FinishedNum;
						int num = (!modelByCache.UnfinishedNum.HasValue) ? 0 : modelByCache.UnfinishedNum.Value;
						int num2 = finishedNum + num;
						vltContext.Put("totalExamNumber", num2);
					}
					if (onCustPhysicalExamInfo != null)
					{
						vltContext.Put("Is_FeeSettled", onCustPhysicalExamInfo.Is_FeeSettled);
						vltContext.Put("CustomerSecurityLevel", onCustPhysicalExamInfo.SecurityLevel);
						vltContext.Put("DiseaseLevel", onCustPhysicalExamInfo.DiseaseLevel);
						vltContext.Put("Is_GuideSheetPrinted", onCustPhysicalExamInfo.Is_GuideSheetPrinted);
						vltContext.Put("Is_SectionLock", onCustPhysicalExamInfo.Is_SectionLock);
						vltContext.Put("Is_Checked", onCustPhysicalExamInfo.Is_Checked);
						vltContext.Put("ID_FinalDoctor", onCustPhysicalExamInfo.ID_FinalDoctor);
						vltContext.Put("FinalDoctor", onCustPhysicalExamInfo.FinalDoctor);
						vltContext.Put("FinalDate", onCustPhysicalExamInfo.FinalDate.HasValue ? DateTime.Parse(onCustPhysicalExamInfo.FinalDate.ToString()).ToString("yyyy-MM-dd") : "");
						vltContext.Put("FinalDateDetail", onCustPhysicalExamInfo.FinalDate);
						vltContext.Put("Is_FinalFinished", onCustPhysicalExamInfo.Is_FinalFinished);
						vltContext.Put("Is_GuideSheetReturned", onCustPhysicalExamInfo.Is_GuideSheetReturned);
						vltContext.Put("Is_GuideSheetPrinted", onCustPhysicalExamInfo.Is_GuideSheetPrinted);
						vltContext.Put("Is_Paused", onCustPhysicalExamInfo.Is_Paused);
						vltContext.Put("Is_ReportReceipted", onCustPhysicalExamInfo.Is_ReportReceipted);
						vltContext.Put("Is_Checked", onCustPhysicalExamInfo.Is_Checked);
						vltContext.Put("ID_Checker", onCustPhysicalExamInfo.ID_Checker);
						vltContext.Put("Checker", onCustPhysicalExamInfo.Checker);
						vltContext.Put("CheckedDate", onCustPhysicalExamInfo.CheckedDate.HasValue ? DateTime.Parse(onCustPhysicalExamInfo.CheckedDate.ToString()).ToString("yyyy-MM-dd") : "");
						vltContext.Put("FinalOverView", onCustPhysicalExamInfo.FinalOverView.Replace("\n", "<br/>"));
						vltContext.Put("FinalConclusion", onCustPhysicalExamInfo.FinalConclusion.Replace("\n", "<br/>"));
						vltContext.Put("ResultCompare", onCustPhysicalExamInfo.ResultCompare.Replace("\n", "<br/>"));
						vltContext.Put("MainDiagnose", onCustPhysicalExamInfo.MainDiagnose.Replace("\n", "<br/>"));
						vltContext.Put("FinalDietGuide", onCustPhysicalExamInfo.FinalDietGuide.Replace("\n", "<br/>"));
						vltContext.Put("FinalSportGuide", onCustPhysicalExamInfo.FinalSportGuide.Replace("\n", "<br/>"));
						vltContext.Put("FinalHealthKnowlage", onCustPhysicalExamInfo.FinalHealthKnowlage.Replace("\n", "<br/>"));
						if (onCustPhysicalExamInfo.Is_Checked == false)
						{
							vltContext.Put("RefuseReason", this.GetCustomerRefuseReason(ID_Customer));
						}
					}
				}
			}
		}

		public string GetCustomerRefuseReason(long ID_Customer)
		{
			PEIS.Model.OnFianlCheck customerLastOnFianlCheck = CommonConclusion.Instance.GetCustomerLastOnFianlCheck(ID_Customer);
			string result;
			if (customerLastOnFianlCheck != null)
			{
				if (customerLastOnFianlCheck.Is_Pass == false)
				{
					result = customerLastOnFianlCheck.RefuseReason;
					return result;
				}
			}
			result = "";
			return result;
		}
	}
}
