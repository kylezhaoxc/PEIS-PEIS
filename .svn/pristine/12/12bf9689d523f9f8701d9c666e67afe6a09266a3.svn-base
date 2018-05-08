using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Customer
{
	public class TeamList : BasePage
	{
		private string ID_User = string.Empty;

		private new string UserName = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ID_User = this.LoginUserModel.UserID.ToString();
			this.UserName = this.LoginUserModel.UserName;
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "团体列表");
			vltContext.Put("UserID", this.ID_User);
			vltContext.Put("UserName", this.UserName);
			vltContext.Put("CreateDate", DateTime.Now.ToString("yyyy-MM-dd"));
		}
	}
}
