using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.ConclusionEx
{
	public class ConclusionCheckList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "总审列表");
		}
	}
}
