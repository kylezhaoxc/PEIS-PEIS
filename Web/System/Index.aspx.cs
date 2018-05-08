using PEIS.Base;
using PEIS.Common;
using NVelocity;
using System;

namespace PEIS.Web.User
{
	public class Index : BasePage
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
			vltContext.Put("pageTitle", "用户基本信息");
			vltContext.Put("UserName", this.LoginUserModel.UserName);
			string value = "首次登录";
			if (this.LoginUserModel.LastLoginTime.HasValue)
			{
				value = Convert.ToDateTime(this.LoginUserModel.LastLoginTime.ToString()).ToString("yyyy年MM月dd日 HH:mm");
			}
			vltContext.Put("LastLoginTime", value);
			vltContext.Put("SessionID", this.Session.SessionID);
			if (base.Request["IsLogin"] != null)
			{
				vltContext.Put("IsLogin", base.Request["IsLogin"].ToString());
			}
			else
			{
				vltContext.Put("IsLogin", "0");
			}
			string diseaseLevelWarning = BaseConfig.DiseaseLevelWarning;
			vltContext.Put("DiseaseLevelWarning", diseaseLevelWarning);
			vltContext.Put("ClientIP", Public.GetClientIP());
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy-MM-dd"));
		}
	}
}
