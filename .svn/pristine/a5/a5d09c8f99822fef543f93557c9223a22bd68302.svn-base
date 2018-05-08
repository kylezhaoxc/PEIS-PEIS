using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;
using System.Collections.Generic;
using System.Data;

namespace FYH.Web.System.Exam
{
	public class LabExamOperSingle : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "用户基本信息");
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
			this.GetNatSectionModel(ref vltContext, int2);
			this.GetCustomerInfo(@int, ref vltContext);
			this.GetNatUserList(ref vltContext);
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
						vltContext.Put("txtGenderID", (modelByCache.ID_Gender == 1) ? "1" : "0");
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
						vltContext.Put("Is_Paused", onCustPhysicalExamInfo.Is_Paused);
						vltContext.Put("Is_ReportReceipted", onCustPhysicalExamInfo.Is_ReportReceipted);
					}
				}
			}
		}

		protected void GetNatUserList(ref VelocityContext vltContext)
		{
		}

		protected void GetNatSectionModel(ref VelocityContext vltContext, int SectionID)
		{
			vltContext.Put("SYSSectionModel", PEIS.BLL.SYSSection.Instance.GetModelByCache(SectionID));
		}
	}
}
