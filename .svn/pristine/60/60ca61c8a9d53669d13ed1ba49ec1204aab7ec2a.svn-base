using PEIS.Base;
using PEIS.BLL;
using NVelocity;
using System;

namespace PEIS.Web.System.Statistics
{
	public class CompeleteWorkLoadOfTeam : BasePage
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
			vltContext.Put("flag", base.GetString("flag").ToLower());
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("modelName", base.GetString("modelName").ToLower());
			vltContext.Put("UserID", this.UserID);
			vltContext.Put("UserName", this.UserName);
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy-MM-dd"));
			vltContext.Put("pageTitle", "完成情况统计");
			vltContext.Put("TeamDT", CommonTeam.Instance.GetTeamInfoByKeyWords(string.Empty));
		}
	}
}
