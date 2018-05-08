using PEIS.Base;
using PEIS.BLL;
using NVelocity;
using System;
using System.Data;

namespace PEIS.Web.System.Statistics
{
	public class SectionOncustFeeWorkLoad : BasePage
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
			vltContext.Put("pageTitle", "结算报表");
			DataTable dataTable = CommonSystemInfo.Instance.GetQuickSectionList(string.Empty);
			DataView defaultView = dataTable.DefaultView;
			defaultView.Sort = "SectionName ASC";
			dataTable = defaultView.ToTable();
			vltContext.Put("SectionDT", dataTable);
			dataTable.Dispose();
		}
	}
}
