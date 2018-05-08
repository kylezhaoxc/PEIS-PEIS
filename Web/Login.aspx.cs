using PEIS.Base;
using PEIS.Common;
using NVelocity;
using System;

namespace Web
{
	public class Login : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            this.TemplateName = "blue2";           

            this.Session["UserID"] = "0";
			BasePage.ClearCookie("");
			this.CurrTemplateFileName = UiConfig.LoginSkin;
			if (!string.IsNullOrEmpty(this.CurrTemplateFileName))
			{
				this.CurrTemplateFileName += "/login.htm";
			}
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "登录页面");
			string strurl = base.GetString("urls");
			vltContext.Put("urls", strurl);
			BasePage.ClearCookie("");
		}
	}
}
