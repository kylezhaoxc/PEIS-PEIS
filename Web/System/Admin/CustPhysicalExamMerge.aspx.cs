using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Admin
{
	public class CustPhysicalExamMerge : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "客户信息关联");
		}
	}
}
