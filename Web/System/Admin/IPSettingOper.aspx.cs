using PEIS.Base;
using PEIS.Common;
using NVelocity;
using System;
using System.Data;
using System.Web;

namespace PEIS.Web.System.Admin
{
	public class IPSettingOper : BasePage
	{
		private string type = string.Empty;

		private string modelName = string.Empty;

		private string ID_User = string.Empty;

		private new string UserName = string.Empty;

		private string GUID = string.Empty;

		private string FilePath = HttpContext.Current.Request.PhysicalApplicationPath;

		private string ErrorMessage = string.Empty;

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
			this.type = base.GetString("type").ToLower();
			vltContext.Put("type", this.type);
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy-MM-dd"));
			this.type = base.GetString("type").ToLower().Trim();
			this.modelName = base.GetString("modelName").Trim().ToLower();
			this.GUID = base.GetString("GUID").Trim();
			if (this.type == "add")
			{
				vltContext.Put("pageTitle", "新增IP段");
			}
			else if (this.type == "edit")
			{
				vltContext.Put("pageTitle", "修改IP段");
				this.OutPutTable(ref vltContext, IPConfig.GetIPList(this.FilePath + "\\IPSetting.xml", this.GUID, ref this.ErrorMessage));
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
