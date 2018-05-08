using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.right
{
	public class RightList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "权限管理-权限列表");
		}
	}
}
