using PEIS.Base;
using PEIS.BLL;
using NVelocity;
using System;

namespace PEIS.Web.System.Customer
{
	public class TeamTaskList : BasePage
	{
		private string type = string.Empty;

		private string ID_User = string.Empty;

		private new string UserName = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ID_User = this.LoginUserModel.UserID.ToString();
			this.UserName = this.LoginUserModel.UserName;
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("UserID", this.ID_User);
			vltContext.Put("UserName", this.UserName);
			vltContext.Put("CreateDate", DateTime.Now.ToString("yyyy-MM-dd"));
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy年M月dd日"));
			vltContext.Put("pageTitle", "团体任务");
			vltContext.Put("RegisteDate", DateTime.Now.ToString("yyyy-MM-dd"));
			vltContext.Put("Register", this.UserName);
			vltContext.Put("TeamDT", CommonTeam.Instance.GetTeamInfoByKeyWords(string.Empty));
			this.type = base.GetString("type").ToLower();
			vltContext.Put("CreateDate", DateTime.Now.ToString("yyyy-MM-dd"));
			vltContext.Put("Creator", this.UserName);
			vltContext.Put("ID_Creator", this.ID_User);
			if (this.type == "edit")
			{
			}
		}
	}
}
