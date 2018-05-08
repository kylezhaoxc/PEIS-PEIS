using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;
using System.Data;

namespace PEIS.Web.System.Admin
{
	public class CustomerManageOper : BasePage
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
			vltContext.Put("pageTitle", "修改[" + base.GetString("CustomerName").ToLower() + "]客户档案");
			int @int = base.GetInt("ID_ArcCustomer", -1);
			if (@int > -1)
			{
				string pageCode = "QueryCustomerManageListInfoPagesParam";
				SqlConditionInfo[] array = new SqlConditionInfo[]
				{
					new SqlConditionInfo("@ID_ArcCustomer", @int, TypeCode.Int16)
				};
				array[0].Place = 3;
				int num = 0;
				int pageIndex = 0;
				int pageSize = 10;
				int num2 = 1;
				DataTable page = CommonRegiste.Instance.GetPage(pageCode, pageIndex, pageSize, out num, out num2, array);
				if (page.Rows.Count > 0)
				{
					int count = page.Columns.Count;
					if (page.Columns.Contains("Photo"))
					{
						page.Columns.Add("Base64Photo");
						page.Columns.Add("FilePath");
						foreach (DataRow dataRow in page.Rows)
						{
							if (!Convert.IsDBNull(dataRow["Photo"]))
							{
								dataRow["Base64Photo"] = Convert.ToBase64String((byte[])dataRow["Photo"]);
							}
							vltContext.Put("ID_ArcCustomer", dataRow["ID_ArcCustomer"].ToString());
							vltContext.Put("CustomerName", dataRow["CustomerName"].ToString());
							vltContext.Put("Gender", dataRow["ID_Gender"].ToString());
							vltContext.Put("GenderName", dataRow["GenderName"].ToString());
							vltContext.Put("IDCard", dataRow["IDCard"].ToString());
							vltContext.Put("BirthDay", dataRow["BirthDay"].ToString());
							vltContext.Put("MobileNo", dataRow["MobileNo"].ToString());
							vltContext.Put("Base64Photo", dataRow["Base64Photo"].ToString());
							vltContext.Put("ID_Marriage", dataRow["ID_Marriage"].ToString());
							vltContext.Put("MarriageName", dataRow["MarriageName"].ToString());
							vltContext.Put("NationID", dataRow["NationID"].ToString());
							vltContext.Put("NationName", dataRow["NationName"].ToString());
						}
					}
					this.Session["CustomerManage"] = page;
				}
			}
		}
	}
}
