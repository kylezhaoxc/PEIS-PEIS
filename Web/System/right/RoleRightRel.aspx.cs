using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.right
{
	public class RoleRightRel : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "权限管理-角色配置");
			vltContext.Put("RoleID", base.GetInt("RoleID", 0));
			vltContext.Put("RoleName", base.GetString("RoleName"));
		}
	}
}
