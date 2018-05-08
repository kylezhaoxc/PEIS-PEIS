using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Customer
{
	public class TeamCustomerManageOper : BasePage
	{
		private string type = string.Empty;

		private string modelName = string.Empty;

		private string ID_User = string.Empty;

		private new string UserName = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ID_User = this.LoginUserModel.UserID.ToString();
			this.UserName = this.LoginUserModel.UserName;
			this.ProcessRequest();
		}

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("pageTitle", "新增客户名单");
		}
	}
}
