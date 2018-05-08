using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Conclusion
{
	public class ConclusionTypeList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "结论词分类列表");
		}
	}
}
