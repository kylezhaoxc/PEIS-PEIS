using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace FYH.Web.System.Exam
{
	public class ExamView : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "分科检查预览");
			long @int = base.GetInt64("txtCustomerID", 0L);
			int int2 = base.GetInt("txtSectionID", 0);
			vltContext.Put("txtSectionID", int2.ToString());
			if (@int > 0L)
			{
				vltContext.Put("txtCustomerID", @int.ToString());
			}
			this.GetNatSectionModel(ref vltContext, int2, @int);
		}

		protected void GetNatSectionModel(ref VelocityContext vltContext, int SectionID, long CustomerID)
		{
			PEIS.Model.SYSSection modelByCache = PEIS.BLL.SYSSection.Instance.GetModelByCache(SectionID);
			if (modelByCache != null)
			{
				vltContext.Put("SYSSectionModel", modelByCache);
			}
		}
	}
}
