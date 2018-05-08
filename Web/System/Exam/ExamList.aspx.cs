using PEIS.Base;
using PEIS.BLL;
using NVelocity;
using System;

namespace FYH.Web.System.Exam
{
	public class ExamList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "科室列表");
			int @int = base.GetInt("txtSectionID", 0);
			vltContext.Put("txtSectionID", @int.ToString());
			string sectionName = CommonSystemInfo.Instance.GetSectionName(@int);
			vltContext.Put("SectionName", sectionName);
		}
	}
}
