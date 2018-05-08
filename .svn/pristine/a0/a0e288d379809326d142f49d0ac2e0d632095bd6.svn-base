using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Fee
{
	public class FeeOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("FeeID", 0);
			vltContext.Put("pageTitle", "新增收费项目");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改收费项目");
				this.GetEditFeeItemInfo(ref vltContext);
			}
		}

		protected void GetEditFeeItemInfo(ref VelocityContext vltContext)
		{
			int @int = base.GetInt("FeeID", 0);
			if (@int > 0)
			{
				PEIS.Model.BusFee model = PEIS.BLL.BusFee.Instance.GetModel(@int);
				vltContext.Put("ID_Fee", model.ID_Fee);
				vltContext.Put("FeeName", model.FeeName);
				vltContext.Put("ReportFeeName", model.ReportFeeName);
				vltContext.Put("InterfaceName", model.InterfaceName);
				vltContext.Put("FeeCode", model.FeeCode);
				vltContext.Put("Price", float.Parse(model.Price.ToString()).ToString("0.00"));
				vltContext.Put("ID_Section", model.ID_Section);
				vltContext.Put("SectionName", model.SectionName);
				vltContext.Put("ID_Specimen", model.ID_Specimen);
				vltContext.Put("SpecimenName", model.SpecimenName);
				vltContext.Put("DispOrder", model.DispOrder);
				vltContext.Put("WorkGroupCode", model.WorkGroupCode);
				vltContext.Put("WorkStationCode", model.WorkStationCode);
				vltContext.Put("WorkBenchCode", model.WorkBenchCode);
				vltContext.Put("Note", model.Note);
				vltContext.Put("BreakfastOrder", model.BreakfastOrder);
				vltContext.Put("Forsex", model.ForGender);
				vltContext.Put("Is_Banned", model.Is_Banned);
				vltContext.Put("BanDescribe", model.BanDescribe);
				vltContext.Put("Is_FeeNonPrintInReport", model.Is_FeeNonPrintInReport);
				vltContext.Put("IS_FeeReportMerger", model.IS_FeeReportMerger);
				vltContext.Put("ID_FeeReportMerger", model.ID_FeeReportMerger);
				vltContext.Put("OperationalDate", model.OperationalDate);
				if (model.IS_FeeReportMerger == true)
				{
					vltContext.Put("FeeReportMergerName", model.ReportFeeName);
				}
			}
		}
	}
}
