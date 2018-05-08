using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using PEIS.Model;
using System;
using System.Data;
using System.Reflection;

namespace PEIS.Web.Ajax
{
	public class AjaxCustomerSecurityLevel : BasePage
	{
		public string ErrorMessage = string.Empty;

		private string jsonInfo = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ErrorMessage = "error";
			string @string = base.GetString("action");
			MethodInfo method = base.GetType().GetMethod(@string);
			try
			{
				method.Invoke(this, null);
			}
			catch
			{
				this.OutPutMessage(this.ErrorMessage);
			}
		}

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public void GetCustomerSecurityLevelList()
		{
			int @int = base.GetInt("OperateLevel", -1);
			int int2 = base.GetInt("TeamID", -1);
			int int3 = base.GetInt("TeamTaskID", -1);
			int int4 = base.GetInt("SecurityLevel", -1);
			string text = base.GetString("ID_Customer").Trim().ToLower();
			string text2 = Input.URLDecode(base.GetString("CustomerName").Trim().ToLower());
			string text3 = base.GetString("modelName").Trim().ToLower();
			int int5 = base.GetInt("pageIndex", 1);
			int int6 = base.GetInt("pageSize", 10);
			string text4 = base.Server.HtmlDecode(base.GetString("BeginExamDate"));
			string text5 = base.Server.HtmlDecode(base.GetString("EndExamDate"));
			int totalCount = 0;
			int num = 0;
			if (!string.IsNullOrEmpty(text4))
			{
				text4 += " 00:00:00";
			}
			if (!string.IsNullOrEmpty(text5))
			{
				text5 += " 23:59:59";
			}
			string pageCode = "QueryCustomerSecurityLevelListInfoPagesParam";
			SqlConditionInfo[] array = new SqlConditionInfo[6];
			if (!string.IsNullOrEmpty(text))
			{
				array[2] = new SqlConditionInfo("@ID_Customer", text, TypeCode.Int64);
			}
			else
			{
				if (int2 != -1)
				{
					array[0] = new SqlConditionInfo("@ID_Team", int2, TypeCode.Int32);
				}
				if (int3 != -1)
				{
					array[1] = new SqlConditionInfo("@ID_TeamTask", int3, TypeCode.Int32);
				}
				if (!string.IsNullOrEmpty(text2))
				{
					array[3] = new SqlConditionInfo("@CustomerName", text2, TypeCode.String);
					array[3].Blur = 3;
					array[3].Place = 3;
				}
				if (int4 != -1)
				{
					array[4] = new SqlConditionInfo("@SecurityLevel", int4, TypeCode.Int64);
				}
				else
				{
					array[4] = new SqlConditionInfo("@SecurityLevel", @int, TypeCode.Int64);
					array[4].ParamOper = "<=";
				}
			}
			DataTable page = CommonRegiste.Instance.GetPage(pageCode, int5, int6, out totalCount, out num, array);
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		public void EncodeCustomerSecurityLevel()
		{
			string iD_Customer = base.GetString("ID_Customer").Trim().ToLower().TrimEnd(new char[]
			{
				','
			});
			int @int = base.GetInt("SecurityLevel", -1);
			if (@int == -1)
			{
				this.jsonInfo = "{\"success\":\"0\",\"Message\":\"密级不允许为空！\"}";
			}
			else
			{
				int num = CommonOnArcCust.Instance.UpdateSecurityLevel(iD_Customer, @int);
				this.jsonInfo = "{\"success\":\"1\",\"Message\":\"共成功加密" + num + "个客户\"}";
			}
			this.OutPutMessage(this.jsonInfo);
		}

		public void DecodeCustomerSecurityLevel()
		{
			string iD_Customer = base.GetString("ID_Customer").Trim().ToLower().TrimEnd(new char[]
			{
				','
			});
			int @int = base.GetInt("SecurityLevel", -1);
			if (@int == -1)
			{
				this.jsonInfo = "{\"success\":\"0\",\"Message\":\"密级不允许为空！\"}";
			}
			else
			{
				int num = CommonOnArcCust.Instance.UpdateSecurityLevel(iD_Customer, @int);
				this.jsonInfo = "{\"success\":\"1\",\"Message\":\"共成功解密" + num + "个客户\"}";
			}
			this.OutPutMessage(this.jsonInfo);
		}
	}
}
