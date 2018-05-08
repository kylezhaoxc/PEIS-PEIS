using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using NVelocity;
using System;

namespace PEIS.Web.System.Customer
{
	public class TeamBatch_BusFeeOper : BasePage
	{
		private string IsTeam = "0";

		private string type = string.Empty;

		private string modelName = string.Empty;

		private string ID_User = string.Empty;

		private new string UserName = string.Empty;

		private string Discount = string.Empty;

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
			string a = base.GetString("type").ToLower();
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("SecurityLevelDT", Public.GetSecurityLevelDataFromEnum());
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy年M月dd日"));
			vltContext.Put("pageTitle", "团体备单");
			vltContext.Put("RegisteDate", DateTime.Now.ToString("yyyy-MM-dd"));
			vltContext.Put("Register", this.UserName);
			vltContext.Put("DisCountRate", CommonExcuteSql.Instance.ExcuteSql(string.Format("select isnull(DisCountRate,10) DisCountRate from NatUser where ID_User='{0}';", this.ID_User)).Tables[0].Rows[0][0].ToString());
			string gender = base.GetString("Gender").Trim();
			string custFeeID = base.GetString("CustFeeID").TrimEnd(new char[]
			{
				','
			});
			if (this.Discount == string.Empty)
			{
				this.Discount = CommonExcuteSql.Instance.ExcuteSql(string.Format("select isnull(DisCountRate,10) DisCountRate from NatUser where ID_User='{0}';", this.ID_User)).Tables[0].Rows[0][0].ToString();
			}
			if (a == "delbatch")
			{
				string iD_Team = base.GetString("ID_Team").Trim();
				string iD_TeamTask = base.GetString("ID_TeamTask").Trim();
				string iD_TeamTaskGroup = base.GetString("ID_TeamTaskGroupS").Trim();
				string iD_CustomerS = base.GetString("ID_Customers").TrimEnd(new char[]
				{
					','
				});
				vltContext.Put("BusFeeDT", CommonOnArcCust.Instance.GetCustomerUnionBusFee(this.UserName, iD_Team, iD_TeamTask, iD_TeamTaskGroup, iD_CustomerS));
			}
			else if (a == "addbatch")
			{
				vltContext.Put("BusFeeDT", CommonOnArcCust.Instance.GetBusFeeNotINCustFeeID(gender, custFeeID, this.UserName, this.Discount));
			}
		}
	}
}
