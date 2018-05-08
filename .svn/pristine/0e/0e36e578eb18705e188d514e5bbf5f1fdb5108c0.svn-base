using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.ExamType
{
	public class ExamTypeList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "体检类型列表");
		}
	}
}
