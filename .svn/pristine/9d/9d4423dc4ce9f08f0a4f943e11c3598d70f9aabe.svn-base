using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.LoginCount
{
	public class UserList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "用户登录错误次数列表");
		}
	}
}
