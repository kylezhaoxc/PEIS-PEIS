using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Admin
{
	public class ClientList : BasePage
	{
		private string type = string.Empty;

		private string modelName = string.Empty;

		private string UserID = string.Empty;

		private new string UserName = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.UserID = this.LoginUserModel.UserID.ToString();
			this.UserName = this.LoginUserModel.UserName;
			this.ProcessRequest();
		}

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy-MM-dd"));
			this.type = base.GetString("type").ToLower().Trim();
			this.modelName = base.GetString("modelName").Trim().ToLower();
			vltContext.Put("pageTitle", "客户列表");
		}
	}
}
