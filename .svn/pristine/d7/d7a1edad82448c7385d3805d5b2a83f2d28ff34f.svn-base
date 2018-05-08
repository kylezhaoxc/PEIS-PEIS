using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Section
{
	public class SectionOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("SectionID", 0);
			vltContext.Put("pageTitle", "新增科室");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改科室");
				this.GetEditSectionInfo(@int, ref vltContext);
			}
		}

		protected void GetEditSectionInfo(int SectionID, ref VelocityContext vltContext)
		{
			PEIS.Model.SYSSection model = PEIS.BLL.SYSSection.Instance.GetModel(SectionID);
			vltContext.Put("SectionID", model.SectionID);
			vltContext.Put("SectionName", model.SectionName);
			vltContext.Put("FunctionType", model.FunctionType);
            vltContext.Put("IsAutoApprove", model.IsAutoApprove);
            vltContext.Put("IsOwnInterface", model.IsOwnInterface);
            vltContext.Put("InterfaceType", model.InterfaceType);
            vltContext.Put("PacsInterfaceFlag", model.PacsInterfaceFlag);
            vltContext.Put("ImageType", model.ImageType);
            vltContext.Put("IsNonPrintSectSummary", model.IsNonPrintSectSummary);
            vltContext.Put("IsNotSamePage", model.IsNotSamePage);
            vltContext.Put("PicPrintSetup", model.PicPrintSetup);
            vltContext.Put("SummaryName", model.SummaryName);
            vltContext.Put("DispOrder", model.DispOrder);
            vltContext.Put("DefaultSummary", model.DefaultSummary);
            vltContext.Put("SepBetweenExamItems", model.SepBetweenExamItems);
            vltContext.Put("SepBetweenSymptoms", model.SepBetweenSymptoms);
            vltContext.Put("TerminalSymbol", model.TerminalSymbol);
            vltContext.Put("SepExamAndValue", model.SepExamAndValue);
            vltContext.Put("NoBetweenExamItems", model.NoBetweenExamItems);
            vltContext.Put("NoBetweenSympotms", model.NoBetweenSympotms);
            vltContext.Put("IsNoEntryFinalSummary", model.IsNoEntryFinalSummary);
            vltContext.Put("IsNonPrintInReport", model.IsNonPrintInReport);
            vltContext.Put("IsDel", model.IsDel);
            vltContext.Put("DisplayMenu", model.DisplayMenu);
            vltContext.Put("Note", model.Note);
        }
	}
}
