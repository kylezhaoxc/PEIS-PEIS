using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using PEIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace PEIS.Web.Ajax
{
	public class AjaxReportPreview : BasePage
	{
		private string ID_User = string.Empty;

		private new string UserName = string.Empty;

		private string jsonInfo = string.Empty;

		private string ErrorMessage = string.Empty;

		private CommonReport _CommonReport = new CommonReport();

		private int SecurityLevel = 100;

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public void PreviewReport()
		{
			string text = base.GetString("ID_Customer").Trim();
			if (text == string.Empty)
			{
				this.jsonInfo = "{\"success\":\"0\",\"Message\":\"体检号不允许为空！\"}";
				this.OutPutMessage(this.jsonInfo);
			}
			else
			{
				DataTable dataTable = this._CommonReport.GetCustomerManageReport(text).Tables[0];
				if (dataTable.Rows.Count == 0)
				{
					this.jsonInfo = "{\"success\":\"0\",\"Message\":\"未检索到该体检号对应的信息,请您核实！\"}";
				}
				else if (!bool.Parse(dataTable.Rows[0]["Is_Checked"].ToString()))
				{
					this.jsonInfo = "{\"success\":\"0\",\"Message\":\"该客户尚未完成总检，不允许查看！\"}";
				}
				else
				{
					this.jsonInfo = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				}
				this.OutPutMessage(this.jsonInfo);
			}
		}

		public void ViewReport()
		{
			string text = base.GetString("ID_Customer").Trim();
			string text2 = base.GetString("modelName").Trim().ToLower();
			int @int = base.GetInt("pageIndex", 1);
			int int2 = base.GetInt("pageSize", 10);
			int totalCount = 0;
			int num = 0;
			string pageCode = "CustomerReportPriViewInfoPagesParam";
			SqlConditionInfo[] array = null;
			if (text != string.Empty)
			{
				array = new SqlConditionInfo[]
				{
					new SqlConditionInfo("@ID_Customer", text, TypeCode.Int32)
				};
				array[0].Blur = 4;
			}
			DataTable page = CommonReport.Instance.GetPage(pageCode, @int, int2, out totalCount, out num, array);
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		public void PrintReport()
		{
			int @int = base.GetInt("containCheck", 0);
			string text = base.GetString("ID_Team").Trim().ToLower();
			string a = base.GetString("SearchType").Trim().ToLower();
			string text2 = base.GetString("ID_Customer").Trim();
			string text3 = base.GetString("modelName").Trim().ToLower();
			int int2 = base.GetInt("pageIndex", 1);
			int int3 = base.GetInt("pageSize", 10);
			int totalCount = 0;
			int num = 0;
			string paramvalue = DateTime.Now.AddDays(-15.0).ToString("yyyy-MM-dd 00:00:01");
			DataTable dt = null;
			if (a == "0")
			{
				SqlConditionInfo[] array = new SqlConditionInfo[3];
				string pageCode;
				if (@int == 1)
				{
					pageCode = "CustomerReportPrintInfoPagesParamNoCheck";
				}
				else
				{
					pageCode = "CustomerReportPrintInfoPagesParam";
				}
				if (!string.IsNullOrEmpty(text2))
				{
					array[0] = new SqlConditionInfo("@ID_Customer", text2, TypeCode.Int32);
					array[0].Blur = 4;
				}
				else
				{
					array[2] = new SqlConditionInfo("@CheckedDate", paramvalue, TypeCode.DateTime);
					array[2].ParamOper = ">=";
				}
				array[1] = new SqlConditionInfo("@SecurityLevel", this.SecurityLevel, TypeCode.Int32);
				array[1].Place = 2;
				dt = CommonReport.Instance.GetPage(pageCode, int2, int3, out totalCount, out num, array);
			}
			else if (a == "1")
			{
				string pageCode = "CustomerReportPrintSelfInfoPagesParam";
				SqlConditionInfo[] array = new SqlConditionInfo[3];
				if (!string.IsNullOrEmpty(text2))
				{
					array[0] = new SqlConditionInfo("@ID_Customer", text2, TypeCode.Int32);
					array[0].Blur = 4;
				}
				else
				{
					array[2] = new SqlConditionInfo("@CheckedDate", paramvalue, TypeCode.DateTime);
					array[2].ParamOper = ">=";
				}
				array[1] = new SqlConditionInfo("@SecurityLevel", this.SecurityLevel, TypeCode.Int32);
				array[1].Place = 2;
				dt = CommonReport.Instance.GetPage(pageCode, int2, int3, out totalCount, out num, array);
			}
			else if (a == "2")
			{
				string pageCode = "CustomerReportPrintTeamInfoPagesParam";
				SqlConditionInfo[] array = new SqlConditionInfo[3];
				if (!string.IsNullOrEmpty(text2))
				{
					array[0] = new SqlConditionInfo("@ID_Customer", text2, TypeCode.Int32);
					array[0].Blur = 4;
				}
				else
				{
					if (text != "-1")
					{
						array[0] = new SqlConditionInfo("@ID_Team", text, TypeCode.Int32);
						array[0].Blur = 4;
					}
					array[2] = new SqlConditionInfo("@CheckedDate", paramvalue, TypeCode.DateTime);
					array[2].ParamOper = ">=";
				}
				array[1] = new SqlConditionInfo("@SecurityLevel", this.SecurityLevel, TypeCode.Int32);
				array[1].Place = 2;
				dt = CommonReport.Instance.GetPage(pageCode, int2, int3, out totalCount, out num, array);
			}
			string msg = JsonHelperFont.Instance.DataTableToJSON(dt, totalCount);
			this.OutPutMessage(msg);
		}

		public void PrintCoverReport()
		{
			string text = base.GetString("ID_Customer").Trim();
			string querySqlCode = "CustomerReportPrintCoverInfoPagesParam";
			SqlConditionInfo[] array = new SqlConditionInfo[1];
			if (!string.IsNullOrEmpty(text))
			{
				array[0] = new SqlConditionInfo("@ID_Customer", text, TypeCode.Int32);
				array[0].Blur = 4;
			}
			DataTable dataTable = CommonReport.Instance.ExcuteQuerySql(querySqlCode, array).Tables[0];
			string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, dataTable.Rows.Count);
			this.OutPutMessage(msg);
		}

		public void GetInformerOrReceiptReport()
		{
			string text = base.GetString("ID_Team").Trim().ToLower();
			string a = base.GetString("SearchType").Trim().ToLower();
			string text2 = base.GetString("IDCard").Trim();
			string text3 = base.GetString("ID_Customer").Trim();
			string a2 = base.GetString("modelName").Trim().ToLower();
			int @int = base.GetInt("pageIndex", 1);
			int int2 = base.GetInt("pageSize", 10);
			int totalCount = 0;
			int num = 0;
			int num2 = 7;
			string pageCode = string.Empty;
			if (a2 == "informer")
			{
				int.TryParse(UiConfig.ReportOutDays, out num2);
				pageCode = "CustomerInformerPagesParam";
			}
			else if (a2 == "receipt")
			{
				pageCode = "CustomerReceiptPagesParam";
			}
			SqlConditionInfo[] array = new SqlConditionInfo[6];
			if (!string.IsNullOrEmpty(text3))
			{
				array[0] = new SqlConditionInfo("@ID_Customer", text3, TypeCode.Int32);
				array[0].Blur = 4;
			}
			array[1] = new SqlConditionInfo("@SecurityLevel", this.SecurityLevel, TypeCode.Int32);
			array[1].Place = 2;
			array[2] = new SqlConditionInfo("@ReportOutDays", num2, TypeCode.Int32);
			array[2].Place = 2;
			if (!string.IsNullOrEmpty(text2))
			{
				array[3] = new SqlConditionInfo("@IDCard", text2, TypeCode.String);
				array[3].Blur = 4;
			}
			if (!(a == "0"))
			{
				if (a == "1")
				{
					array[4] = new SqlConditionInfo("@Is_Team", 0, TypeCode.Int32);
					array[4].Blur = 4;
				}
				else if (a == "2")
				{
					array[4] = new SqlConditionInfo("@Is_Team", 1, TypeCode.Int32);
					array[4].Blur = 4;
					if (text != "-1")
					{
						array[5] = new SqlConditionInfo("@ID_Team", text, TypeCode.Int32);
						array[5].Blur = 4;
					}
				}
			}
			DataTable page = CommonReport.Instance.GetPage(pageCode, @int, int2, out totalCount, out num, array);
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		public void UpdateCustomerReportInformFlag()
		{
			string text = base.GetString("ID_Customers").Trim();
			string operType = base.GetString("modelName").Trim().ToLower();
			try
			{
				int num = CommonReport.Instance.UpdateCustomerReportInformFlag(this.ID_User, this.UserName, text, 0, operType);
				if (num > 0)
				{
					string msg = "{\"success\":\"1\",\"Message\":\"通知成功！\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",报告领取通知成功 体检号为：",
						text
					}));
					DateTime now = DateTime.Now;
					string[] array = text.Replace("'", string.Empty).TrimEnd(new char[]
					{
						','
					}).Split(new char[]
					{
						','
					});
					string[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						string text2 = array2[i];
						if (!string.IsNullOrEmpty(text2))
						{
							base.AddOrUpdateByBackLogType(long.Parse(text2.ToString()), EnumBusBackLogType.报告通知, true, null);
						}
					}
				}
				else
				{
					string msg = "{\"success\":\"0\",\"Message\":\"通知失败，请重试！\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",报告领取通知失败 体检号为：",
						text
					}));
				}
			}
			catch (Exception ex)
			{
				string msg = "{\"success\":\"0\",\"Message\":\"通知失败，请重试！\"}";
				this.OutPutMessage(msg);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",报告领取通知失败 体检号为：",
					text,
					" 错误原因为：",
					ex.Message
				}));
			}
		}

		public void UpdateCustomerReportReceiptFlag()
		{
			string text = base.GetString("AllData").TrimEnd(new char[]
			{
				'|'
			});
			string text2 = base.GetString("modelName").Trim().ToLower();
			List<PEIS.Model.OnCustReportManage> list = new List<PEIS.Model.OnCustReportManage>();
			string[] array = text.Split(new char[]
			{
				'|'
			});
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(string.Format("本次共计领取{0}个", array.Length));
			if (array.Length > 0)
			{
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text3 = array2[i];
					string[] array3 = text3.Split(new char[]
					{
						'-'
					});
					if (array3.Length > 0)
					{
						PEIS.Model.OnCustReportManage onCustReportManage = new PEIS.Model.OnCustReportManage();
						onCustReportManage.ID_Customer = array3[0];
						onCustReportManage.Is_SelfReceipted = new bool?(array3[1] == "1");
						onCustReportManage.ReportReceiptedDate = new DateTime?(DateTime.Now);
						onCustReportManage.ID_ReportOffer = new int?(int.Parse(this.ID_User));
						onCustReportManage.Is_ReportReceipted = new bool?(true);
						onCustReportManage.ReportOffer = this.UserName;
						onCustReportManage.ReportReceiptor = array3[2];
						onCustReportManage.ReportReceiptor = Secret.AES.EncryptPrefix(onCustReportManage.ReportReceiptor);
						list.Add(onCustReportManage);
						stringBuilder.AppendLine(string.Format("姓名[{0}],体检号[{1}],是否本人[{2}],领取人[{3}]", new object[]
						{
							array3[3],
							onCustReportManage.ID_Customer,
							onCustReportManage.Is_SelfReceipted,
							array3[2]
						}));
					}
				}
			}
			try
			{
				int num = CommonReport.Instance.UpdateCustomerReportReceiptFlag(list);
				if (num > 0)
				{
					string msg = "{\"success\":\"1\",\"Message\":\"领取成功！\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",报告领取成功 详细名单：",
						stringBuilder.ToString()
					}));
					DateTime now = DateTime.Now;
					foreach (PEIS.Model.OnCustReportManage current in list)
					{
						if (!string.IsNullOrEmpty(current.ID_Customer))
						{
							base.AddOrUpdateByBackLogType(long.Parse(current.ID_Customer.ToString()), EnumBusBackLogType.报告领取, true, null);
						}
					}
				}
				else
				{
					string msg = "{\"success\":\"0\",\"Message\":\"领取失败，请重试！\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",报告领取失败,0行受影响 详细名单：",
						stringBuilder.ToString()
					}));
				}
			}
			catch (Exception ex)
			{
				string msg = "{\"success\":\"0\",\"Message\":\"领取失败，请重试！\"}";
				this.OutPutMessage(msg);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",报告领取失败，详细名单：",
					stringBuilder.ToString(),
					"错误原因为",
					ex.Message
				}));
			}
		}

		public void GetCustomerFinalCheckInfo()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			if (@int > 0L)
			{
				string strWhere = string.Format("ID_Customer='{0}'", @int);
				JsonHelperFont.Instance.DataTableToJSON(PEIS.BLL.OnCustPhysicalExamInfo.Instance.GetList(strWhere).Tables[0], "dataList");
			}
		}

		public void ISCanReadReport()
		{
			string arg = base.GetString("ID_Customer").Trim().TrimEnd(new char[]
			{
				','
			});
			string sql = string.Format("SELECT ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,Is_CompletePhysical,ExamState FROM OnCustRelationCustPEInfo WHERE ID_Customer='{0}';", arg);
			DataTable dataTable = CommonExcuteSql.Instance.ExcuteSql(sql).Tables[0];
			int count = dataTable.Rows.Count;
			string appSettingKey = string.Empty;
			int num = -1;
			if (count > 0)
			{
				int.TryParse(dataTable.Rows[0]["ExamState"].ToString(), out num);
			}
			if (num == 0)
			{
				appSettingKey = "ConnectionString";
			}
			else
			{
				if (num == -1)
				{
				}
				if (num == 1)
				{
					appSettingKey = "ToOffLineConnectionString";
				}
				else
				{
					appSettingKey = "FYH_HisFile" + (num - 1).ToString().PadLeft(3, '0');
				}
			}
			string sql2 = string.Format("SELECT ISNULL(SecurityLevel,0) SecurityLevel FROM OnCustPhysicalExamInfo WHERE OnCustPhysicalExamInfo.ID_Customer='{0}';", arg);
			DataSet dataSet = CommonExcuteSql.Instance.ExcuteSql(appSettingKey, sql2);
			string msg = string.Empty;
			int num2 = 0;
			if (dataSet != null)
			{
				DataTable dataTable2 = dataSet.Tables[0];
				int count2 = dataTable2.Rows.Count;
				if (count2 > 0)
				{
					int.TryParse(dataTable2.Rows[0]["SecurityLevel"].ToString(), out num2);
				}
			}
			if (num2 == 0 || num2 > 100)
			{
				msg = "{\"success\":\"0\",\"Message\":\"该客户不存在或已加密，不可查看其体检报告\"}";
			}
			else
			{
				msg = "{\"success\":\"1\",\"Message\":\"该客户的尚未加密，可查看其体检报告\"}";
			}
			this.OutPutMessage(msg);
		}

		public void WritePrintReportLog()
		{
			string msg = string.Empty;
			try
			{
				string @string = base.GetString("allPrintID_Customer");
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",批量打印报告，待打印报告顺序为：",
					@string
				}));
				msg = "{\"success\":\"1\",\"Message\":\"成功写入报告打印日志\"}";
			}
			catch (Exception ex)
			{
				msg = "{\"success\":\"0\",\"Message\":\"写入报告打印日志失败:" + ex.Message + "\"}";
			}
			this.OutPutMessage(msg);
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ID_User = this.LoginUserModel.UserID.ToString();
			this.UserName = this.LoginUserModel.UserName;
			string @string = base.GetString("action");
			MethodInfo method = base.GetType().GetMethod(@string);
			try
			{
				method.Invoke(this, null);
			}
			catch (ExecutionEngineException ex)
			{
				this.OutPutMessage(ex.Message);
			}
		}
	}
}
