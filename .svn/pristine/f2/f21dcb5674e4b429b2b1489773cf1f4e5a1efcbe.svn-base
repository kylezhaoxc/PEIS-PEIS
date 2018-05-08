using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.GiveUpManage
{
	public class GiveUpManageList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "已回收指引单列表");
		}
	}
}
