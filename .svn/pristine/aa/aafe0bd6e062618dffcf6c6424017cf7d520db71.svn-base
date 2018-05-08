using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using NVelocity;
using System;
using System.Data;

namespace PEIS.Web.System.Admin
{
	public class CustomerSecurityLevel : BasePage
	{
		private string type = string.Empty;

		private string modelName = string.Empty;

		private string ID_User = string.Empty;

		private new string UserName = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
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
			this.modelName = base.GetString("modelName").ToLower();
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy-MM-dd"));
			this.type = base.GetString("type").ToLower().Trim();
			if (this.modelName == "encode")
			{
				vltContext.Put("pageTitle", "加密客户");
			}
			else if (this.modelName == "encode")
			{
				vltContext.Put("pageTitle", "解密客户");
			}
			vltContext.Put("modelName", this.modelName);
			DataSet allDctData = CommonOnArcCust.Instance.GetAllDctData();
			for (int i = 0; i < allDctData.Tables.Count; i++)
			{
				vltContext.Put(CommonOnArcCust.Instance.tbName[i], allDctData.Tables[i]);
			}
			allDctData.Dispose();
			vltContext.Put("TeamDT", CommonTeam.Instance.GetTeamInfoByKeyWords(string.Empty));
			DataTable securityLevelDataFromEnum = Public.GetSecurityLevelDataFromEnum();
			DataTable dataTable = securityLevelDataFromEnum.Clone();
			int value = this.LoginUserModel.OperateLevel.Value;
			DataRow[] array = securityLevelDataFromEnum.Select("key<=" + value);
			if (array.Length > 0)
			{
				DataRow[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					DataRow row = array2[j];
					dataTable.ImportRow(row);
				}
			}
			vltContext.Put("SecurityLevelDT", dataTable);
			dataTable.Dispose();
			securityLevelDataFromEnum.Dispose();
		}
	}
}
