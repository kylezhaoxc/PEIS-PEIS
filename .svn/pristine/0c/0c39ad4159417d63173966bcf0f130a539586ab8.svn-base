using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Conclusion
{
	public class ConclusionTypeOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("FeeReportID", 0);
			vltContext.Put("pageTitle", "新增收费项目报告");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改收费项目报告");
				this.GetEditConclusionTypeInfo(@int, ref vltContext);
			}
		}

		protected void GetEditConclusionTypeInfo(int ID_FeeReport, ref VelocityContext vltContext)
		{
			if (ID_FeeReport > 0)
			{
				PEIS.Model.BusFeeReport model = PEIS.BLL.BusFeeReport.Instance.GetModel(ID_FeeReport);
				if (null != model)
				{
					vltContext.Put("ID_FeeReport", model.ID_FeeReport);
					vltContext.Put("ID_Fee", model.ID_Fee);
					vltContext.Put("FeeName", model.FeeName);
					vltContext.Put("Is_Banned", model.Is_Banned);
					vltContext.Put("Operator", model.Operator);
					vltContext.Put("ID_Operator", model.ID_Operator);
					vltContext.Put("Note", model.Note);
					vltContext.Put("OperateDate", model.OperateDate);
				}
			}
		}
	}
}
