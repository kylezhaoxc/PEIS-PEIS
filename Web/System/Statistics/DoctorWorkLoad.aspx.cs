using PEIS.Base;
using PEIS.BLL;
using NVelocity;
using System;

namespace PEIS.Web.System.Statistics
{
	public class DoctorWorkLoad : BasePage
	{
		private string flag = "1";

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
			int @int = base.GetInt("flag", -1);
			string str = string.Empty;
			if (@int == 1)
			{
				str = "分检";
			}
			else if (@int == 2)
			{
				str = "总检";
			}
			else if (@int == 3)
			{
				str = "总审";
			}
			else if (@int == 4)
			{
				str = "录入";
			}
			vltContext.Put("flag", base.GetString("flag").ToLower());
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("modelName", base.GetString("modelName").ToLower());
			vltContext.Put("UserID", this.UserID);
			vltContext.Put("UserName", this.UserName);
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy-MM-dd"));
			vltContext.Put("pageTitle", str + "统计");
			vltContext.Put("DctDoctorDT", CommonOnArcCust.Instance.GetUser("1,3"));
		}
	}
}
