using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.User
{
	public class UserRightSet : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "用户权限设置");
			vltContext.Put("RightUserID", base.GetInt("UserID", 0));
			vltContext.Put("RightUserName", base.GetString("UserName"));
		}
	}
}
