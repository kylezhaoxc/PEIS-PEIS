using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Statistics
{
	public class DiseaseLevelWorkLoad : BasePage
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
			int @int = base.GetInt("flag", -1);
			vltContext.Put("flag", base.GetString("flag").ToLower());
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("modelName", base.GetString("modelName").ToLower());
			vltContext.Put("UserID", this.UserID);
			vltContext.Put("UserName", this.UserName);
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy-MM-dd"));
			if (@int == 1)
			{
				vltContext.Put("pageTitle", "病症级别统计");
			}
			else if (@int == 0)
			{
				vltContext.Put("pageTitle", "病症级别查询");
			}
		}
	}
}
