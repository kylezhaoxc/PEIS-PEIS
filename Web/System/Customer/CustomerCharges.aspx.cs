using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using NVelocity;
using System;

namespace PEIS.Web.System.Customer
{
	public class CustomerCharges : BasePage
	{
		private string IsTeam = "0";

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
			vltContext.Put("pageTitle", "客户收、退费");
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("SecurityLevelDT", Public.GetSecurityLevelDataFromEnum());
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy年M月dd日"));
			vltContext.Put("pageTitle", "团体备单");
			vltContext.Put("RegisteDate", DateTime.Now.ToString("yyyy-MM-dd"));
			vltContext.Put("Register", this.UserName);
			vltContext.Put("DisCountRate", CommonExcuteSql.Instance.ExcuteSql(string.Format("select isnull(DisCountRate,10) DisCountRate from SYSOpUser where UserID='{0}';", this.UserID)).Tables[0].Rows[0][0].ToString());
			vltContext.Put("UserID", this.UserID);
			vltContext.Put("UserName", this.UserName);
		}
	}
}
