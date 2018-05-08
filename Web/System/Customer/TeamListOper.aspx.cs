using PEIS.Base;
using PEIS.BLL;
using NVelocity;
using System;
using System.Data;

namespace PEIS.Web.System.Customer
{
	public class TeamListOper : BasePage
	{
		private string type = string.Empty;

		private string ID_User = string.Empty;

		private new string UserName = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ID_User = this.LoginUserModel.UserID.ToString();
			this.UserName = this.LoginUserModel.UserName;
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", base.GetUrlEncode("title"));
			vltContext.Put("modelName", base.GetString("modelName").ToLower());
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy年M月dd日"));
			this.type = base.GetString("type").ToLower();
			if (this.type == "add")
			{
				vltContext.Put("CreateDate", DateTime.Now.ToString("yyyy-MM-dd"));
				vltContext.Put("Creator", this.UserName);
				vltContext.Put("ID_Creator", this.ID_User);
			}
			else if (this.type == "edit")
			{
				string iD_Team = base.GetString("ID_Team").Trim();
				string teamName = base.GetString("TeamName").Trim();
				DataTable teamInfo = CommonTeam.Instance.GetTeamInfo(iD_Team, teamName);
				this.OutPutTable(ref vltContext, teamInfo);
			}
		}

		private void OutPutTable(ref VelocityContext vltContext, DataTable dt)
		{
			int count = dt.Rows.Count;
			int count2 = dt.Columns.Count;
			if (count == 1)
			{
				for (int i = 0; i < count2; i++)
				{
					vltContext.Put(dt.Columns[i].ToString(), dt.Rows[0][i].ToString());
				}
			}
		}
	}
}
