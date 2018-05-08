using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;
using System.Collections.Generic;
using System.Data;

namespace FYH.Web.System.Exam
{
	public class ExamOper : BasePage
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
			vltContext.Put("CustomerFileSavePath", BaseConfig.CustomerFileSavePath);
			vltContext.Put("CustomerFileResultFlagSavePath", BaseConfig.CustomerFileResultFlagSavePath);
			this.GetCustomerExamInfo(ref vltContext);
			this.GetTagPrintRelSectionID(ref vltContext);
			this.GetImageFileUploadUrl(ref vltContext);
			this.IsSectionShowVideoCapture(ref vltContext);
			this.IsSectionShowDeviceResultImagesList(ref vltContext);
			this.IsSectionShowComRead(ref vltContext);
			this.IsShowWriteCustomerFileSectionBtn(ref vltContext);
		}

		protected void IsSectionShowVideoCapture(ref VelocityContext vltContext)
		{
			int num = 0;
			string text = BaseConfig.VideoCaptureSection;
			int @int = base.GetInt("txtSectionID", 0);
			text = text.Replace('｜', '|');
			string[] array = text.Split(new char[]
			{
				'|'
			});
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i].ToString()))
					{
						if (@int > 0 && array[i].ToString() == @int.ToString())
						{
							num = 1;
						}
					}
				}
			}
			vltContext.Put("IsSectionShowVideoCapture", num.ToString());
		}

		protected void IsSectionShowDeviceResultImagesList(ref VelocityContext vltContext)
		{
			int num = 0;
			string text = BaseConfig.VideoImagesListSection;
			int @int = base.GetInt("txtSectionID", 0);
			text = text.Replace('｜', '|');
			string[] array = text.Split(new char[]
			{
				'|'
			});
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i].ToString()))
					{
						if (@int > 0 && array[i].ToString() == @int.ToString())
						{
							num = 1;
						}
					}
				}
			}
			vltContext.Put("IsShowDeviceResultImagesList", num.ToString());
		}

		protected void IsSectionShowComRead(ref VelocityContext vltContext)
		{
			int num = 0;
			string text = BaseConfig.ComReadSection;
			int @int = base.GetInt("txtSectionID", 0);
			text = text.Replace('｜', '|');
			string[] array = text.Split(new char[]
			{
				'|'
			});
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i].ToString()))
					{
						if (@int > 0 && array[i].ToString() == @int.ToString())
						{
							num = 1;
						}
					}
				}
			}
			vltContext.Put("IsSectionShowComRead", num.ToString());
		}

		protected void IsShowWriteCustomerFileSectionBtn(ref VelocityContext vltContext)
		{
			int num = 0;
			string text = BaseConfig.WriteCustomerFileSection;
			int @int = base.GetInt("txtSectionID", 0);
			text = text.Replace('｜', '|');
			string[] array = text.Split(new char[]
			{
				'|'
			});
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (!string.IsNullOrEmpty(array[i].ToString()))
					{
						if (@int > 0 && array[i].ToString() == @int.ToString())
						{
							num = 1;
						}
					}
				}
			}
			vltContext.Put("IsShowWriteCustomerFileSectionBtn", num.ToString());
		}

		protected int GetTagPrintRelSectionID(ref VelocityContext vltContext)
		{
			int @int = base.GetInt("txtSectionID", 0);
			int num = @int;
			string text = "False";
			string text2 = "";
			int result;
			try
			{
				string text3 = BaseConfig.SectionTagPrintRel;
				if (string.IsNullOrEmpty(text3))
				{
					result = @int;
					return result;
				}
				text3 = text3.Replace('；', ';').Replace('｜', '|');
				string[] array = text3.Split(new char[]
				{
					';'
				});
				if (array.Length > 0)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!string.IsNullOrEmpty(array[i].ToString()))
						{
							string[] array2 = array[i].Split(new char[]
							{
								'|'
							});
							if (array2.Length > 0)
							{
								if (int.Parse(array2[0].ToString()) == @int)
								{
									num = int.Parse(array2[1].ToString());
									if (@int != num)
									{
										text = ((array2[2].ToUpper() == "TRUE") ? "True" : "False");
									}
									text2 = array2[3].ToString();
								}
							}
						}
					}
				}
				vltContext.Put("TagPrintRelSectionID", num.ToString());
				vltContext.Put("IsPrintCurrSectionTag", text.ToString());
				vltContext.Put("TagPrintTemplete", text2.ToString());
			}
			catch (Exception var_8_194)
			{
			}
			result = @int;
			return result;
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
			this.GetNatSectionModel(ref vltContext, int2, @int);
			this.GetCustomerInfo(@int, ref vltContext);
			this.GetNatUserList(ref vltContext);
		}

		protected void GetCustomerInfo(long ID_Customer, ref VelocityContext vltContext)
		{
			DataSet custRelationCustPEInfo = CommonCustExam.Instance.GetCustRelationCustPEInfo(ID_Customer, "", "");
			if (custRelationCustPEInfo != null && 0 < custRelationCustPEInfo.Tables[0].Rows.Count)
			{
				List<PEIS.Model.OnCustRelationCustPEInfo> list = PEIS.BLL.OnCustRelationCustPEInfo.Instance.DataTableToList(custRelationCustPEInfo.Tables[0]);
				if (list == null)
				{
					base.Response.Write("<script> jQuery(document).ready(function () { ReLoginConfirm();}); </script>");
					base.Response.End();
				}
				else
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

		protected void GetNatSectionModel(ref VelocityContext vltContext, int ID_Section, long CustomerID)
		{
			PEIS.Model.SYSSection modelByCache = PEIS.BLL.SYSSection.Instance.GetModelByCache(ID_Section);
			if (modelByCache != null)
			{
				if (modelByCache.IsOwnInterface  == true && modelByCache.InterfaceType == "LAB")
				{
					string url = string.Concat(new string[]
					{
						"/System/Exam/LabExamOperSingle.aspx?txtSectionID=",
						ID_Section.ToString(),
						"&txtCustomerID=",
						CustomerID.ToString(),
						"&date=",
						base.Server.UrlEncode(DateTime.Now.ToString())
					});
					base.Redirect(url);
				}
				vltContext.Put("NatSectionModel", modelByCache);
			}
		}

		protected void GetImageFileUploadUrl(ref VelocityContext vltContext)
		{
			vltContext.Put("CurrImageFileUploadUrl", base.GetImageFileUploadUrl());
		}
	}
}
