using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.right
{
	public class RoleOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "权限管理-角色管理");
			int @int = base.GetInt("RoleID", 0);
			vltContext.Put("ID_Role", @int.ToString());
			vltContext.Put("pageTitle", "新增体检地址");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改体检地址");
				this.GetEditRoleInfo(@int, ref vltContext);
			}
		}

		protected void GetEditRoleInfo(int RoleID, ref VelocityContext vltContext)
		{
			if (RoleID > 0)
			{
				PEIS.Model.SysRole model = PEIS.BLL.SysRole.Instance.GetModel(RoleID);
				if (null != model)
				{
					vltContext.Put("RoleID", model.RoleID);
					vltContext.Put("RoleName", model.RoleName);
					vltContext.Put("DispOrder", model.DispOrder);
					vltContext.Put("Is_Default", (model.Is_DefaultRole == 1) ? "True" : "False");
					vltContext.Put("Note", model.Remark);
				}
			}
		}
	}
}
