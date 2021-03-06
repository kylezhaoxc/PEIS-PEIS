using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using NVelocity;
using System;
using System.Data;

namespace PEIS.Web.System.Customer
{
	public class RegistOper : BasePage
	{
		private string IsTeam = "0";

		private string type = string.Empty;

		private string modelName = string.Empty;

		private int Is_Subscribed = -1;

		private string UserID = string.Empty;

		private new string UserName = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.UserID = this.LoginUserModel.UserID.ToString();
			this.UserName = this.LoginUserModel.UserName;
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("CurWeek", DateTime.Now.DayOfWeek.ToString());
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("SecurityLevelDT", Public.GetSecurityLevelDataFromEnum());
			DataSet allDctData = CommonOnArcCust.Instance.GetAllDctData();
			for (int i = 0; i < allDctData.Tables.Count; i++)
			{
				vltContext.Put(CommonOnArcCust.Instance.tbName[i], allDctData.Tables[i]);
			}
			allDctData.Dispose();
			vltContext.Put("DisCountRate", this.LoginUserModel.DisCountRate.Value);
			vltContext.Put("UserID", this.UserID);
			vltContext.Put("UserName", this.UserName);
			vltContext.Put("RegisteDate", DateTime.Now.ToString("yyyy-MM-dd"));
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy-MM-dd"));
			this.IsTeam = base.GetString("IsTeam").ToLower().Trim();
			this.type = base.GetString("type").ToLower().Trim();
			this.modelName = base.GetString("modelName").Trim().ToLower();
            //通过url带的参数判断是那个页面进来的生成title
            if (this.modelName == "regist")
			{
				this.Is_Subscribed = 1;
			}
			if (this.modelName == "sign")
			{
				this.Is_Subscribed = 0;
			}
			if (this.type == "add")
			{
				vltContext.Put("Gender", 0);
				if ((this.modelName == "regist" || this.modelName == "sign") && this.IsTeam != "1")
				{
					if (this.modelName == "regist")
					{
						vltContext.Put("pageTitle", "个人预约");
					}
					else
					{
						vltContext.Put("pageTitle", "个人登记");
					}
				}
				else if (this.modelName == "sign")
				{
					vltContext.Put("pageTitle", "团体登记");
				}
			}
			else if (this.type == "edit")
			{
				if (this.modelName == "regist" || this.modelName == "sign" || this.modelName == "signandregiste")
				{
					string PEID = base.GetString("ID_Customer").Trim();
					DataSet onArcustAndPhysicalInfo = CommonOnArcCust.Instance.GetOnArcustAndPhysicalInfo(PEID);
					this.OutPutTable(ref vltContext, onArcustAndPhysicalInfo.Tables[0]);
					this.OutPutTable(ref vltContext, onArcustAndPhysicalInfo.Tables[1]);
					if (onArcustAndPhysicalInfo.Tables[0].Rows.Count > 0)
					{
						if (onArcustAndPhysicalInfo.Tables[0].Rows[0]["ID_Team"].ToString() != "")
						{
							vltContext.Put("pageTitle", "团体登记");
						}
						else if (onArcustAndPhysicalInfo.Tables[0].Rows[0]["Is_Subscribed"].ToString() == "1")
						{
							vltContext.Put("pageTitle", "个人预约");
						}
						else
						{
							vltContext.Put("pageTitle", "个人登记");
						}
					}
				}
			}
		}

		public void OutPutTable(ref VelocityContext vltContext, DataTable dt)
		{
			int count = dt.Rows.Count;
			int count2 = dt.Columns.Count;
			if (count == 1)
			{
				for (int i = 0; i < count2; i++)
				{
					if (dt.Columns[i].ToString().Trim().ToLower() == "Photo".Trim().ToLower())
					{
						if (!Convert.IsDBNull(dt.Rows[0][i]))
						{
							vltContext.Put("Base64Photo", Convert.ToBase64String((byte[])dt.Rows[0][i]));
						}
					}
					else
					{
						vltContext.Put(dt.Columns[i].ToString(), dt.Rows[0][i].ToString());
					}
					if (dt.Columns[i].ToString().Trim().ToLower() == "RegisteDate".Trim().ToLower())
					{
						if (Convert.IsDBNull(dt.Rows[0][i]) || string.IsNullOrEmpty(dt.Rows[0][i].ToString()))
						{
							dt.Rows[0][i] = DateTime.Now.ToString("yyyy-MM-dd");
							vltContext.Put("RegisteDate", dt.Rows[0][i].ToString());
						}
					}
				}
			}
		}
	}
}
