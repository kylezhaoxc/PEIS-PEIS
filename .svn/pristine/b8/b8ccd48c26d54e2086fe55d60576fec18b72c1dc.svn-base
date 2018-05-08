using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System
{
	public class welcome : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			if (this.LoginUserModel == null)
			{
				base.Redirect("/Login.aspx?flag=logout");
			}
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "欢迎页面");
			vltContext.Put("UserName", this.LoginUserModel.UserName);
			vltContext.Put("Sex", this.LoginUserModel.Sex);
			string value;
			if (this.LoginUserModel.LastLoginTime.HasValue)
			{
				value = Convert.ToDateTime(this.LoginUserModel.LastLoginTime.ToString()).ToString("yyyy年MM月dd日 HH:mm");
			}
			else
			{
				value = "首次登陆";
			}
			vltContext.Put("LastLoginTime", value);
			vltContext.Put("ClientIP", Public.GetClientIP());
			if (this.LoginUserModel.SectionID.HasValue && !string.IsNullOrEmpty(this.LoginUserModel.SectionID.ToString()))
			{
				PEIS.Model.SYSSection modelByCache = PEIS.BLL.SYSSection.Instance.GetModelByCache(int.Parse(this.LoginUserModel.SectionID.ToString()));
				if (modelByCache != null)
				{
					vltContext.Put("UserSectionName", modelByCache.SectionName);
				}
			}
		}
	}
}
