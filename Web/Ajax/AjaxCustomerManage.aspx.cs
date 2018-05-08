using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using PEIS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Reflection;
using System.Web;

namespace PEIS.Web.Ajax
{
	public class AjaxCustomerManage : BasePage
	{
		private string FilePath = HttpContext.Current.Request.PhysicalApplicationPath + "\\CustomerManageLog";

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

		public void GetCustomerManageList()
		{
			int @int = base.GetInt("ID_ArcCustomer", -1);
			int int2 = base.GetInt("pageIndex", 1);
			int int3 = base.GetInt("pageSize", 10);
			string text = Input.URLDecode(base.GetString("CustomerName").Trim().ToLower());
			string text2 = base.GetString("Gender").Trim().ToLower();
			string text3 = base.GetString("IDCard").Trim().ToLower();
			string text4 = base.GetString("Birthday").Trim().ToLower();
			int totalCount = 0;
			int num = 0;
			string pageCode = "QueryCustomerManageListInfoPagesParam";
			SqlConditionInfo[] array = new SqlConditionInfo[5];
			if (@int > -1)
			{
				array[4] = new SqlConditionInfo("@ID_ArcCustomer", @int, TypeCode.Int16);
				array[4].Place = 3;
			}
			else
			{
				if (!string.IsNullOrEmpty(text))
				{
					array[0] = new SqlConditionInfo("@CustomerName", text, TypeCode.String);
					array[0].Blur = 3;
					array[0].Place = 3;
				}
				if (!string.IsNullOrEmpty(text2))
				{
					if (text2 != "-1")
					{
						array[1] = new SqlConditionInfo("@ID_Gender", text2, TypeCode.String);
						array[1].Place = 3;
					}
				}
				if (!string.IsNullOrEmpty(text3))
				{
					array[2] = new SqlConditionInfo("@IDCard", text3, TypeCode.String);
					array[2].Blur = 3;
					array[2].Place = 3;
				}
				if (!string.IsNullOrEmpty(text4))
				{
					array[3] = new SqlConditionInfo("@BirthDay", text4, TypeCode.String);
					array[3].Place = 3;
				}
			}
			DataTable page = CommonRegiste.Instance.GetPage(pageCode, int2, int3, out totalCount, out num, array);
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
					}
				}
			}
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		private bool IsIDCard(string IDCard)
		{
			return IDCard.Length == 15 || IDCard.Length == 18;
		}

		public void SaveCustomerManage()
		{
			int @int = base.GetInt("ID_ArcCustomer", -1);
			if (@int > -1)
			{
				string text = base.GetString("CustomerName").Trim();
				int int2 = base.GetInt("Gender", -1);
				string genderName = base.GetString("GenderName").Trim();
				string text2 = base.GetString("IDCard").Trim();
				string s = base.GetString("Birthday").Trim();
				string mobileNo = base.GetString("MobileNo").Trim();
				string s2 = base.GetString("Base64Photo").Trim();
				int int3 = base.GetInt("NationID", -1);
				string nationName = base.GetString("NationName").Trim();
				int int4 = base.GetInt("ID_Marriage", -1);
				string marriageName = base.GetString("MarriageName").Trim();
				if (!this.IsIDCard(text2))
				{
					Log4J.Instance.Error(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",保存客户存档信息失败 存档ID：",
						@int,
						",身份证号:",
						text2
					}));
					this.jsonInfo = "{\"success\":\"0\",\"Message\":\"证件号格式不正确！\"}";
					this.OutPutMessage(this.jsonInfo);
				}
				else
				{
					string sql = string.Format("SELECT ID_ArcCustomer,IDCard,CustomerName,BirthDay,ID_Gender FROM OnArcCust WHERE IDCard='{0}' AND CustomerName='{1}';", text2, text);
					DataTable dataTable = CommonExcuteSql.Instance.ExcuteSql(sql).Tables[0];
					DataRow[] array = dataTable.Select("ID_ArcCustomer='" + @int + "'");
					if ((array.Length > 0 && dataTable.Rows.Count == 1) || dataTable.Rows.Count == 0)
					{
						PEIS.Model.OnArcCust onArcCust = new PEIS.Model.OnArcCust();
						onArcCust.ID_ArcCustomer = @int;
						onArcCust.CustomerName = text;
						onArcCust.ID_Gender = new int?(int2);
						onArcCust.GenderName = genderName;
						onArcCust.IDCard = text2;
						onArcCust.BirthDay = new DateTime?(DateTime.Parse(s));
						onArcCust.MobileNo = mobileNo;
						onArcCust.NationID = new int?(int3);
						onArcCust.NationName = nationName;
						onArcCust.ID_Marriage = new int?(int4);
						onArcCust.MarriageName = marriageName;
						byte[] array2 = Convert.FromBase64String(s2);
						MemoryStream memoryStream = new MemoryStream(array2);
						onArcCust.Photo = array2;
						int num = CommonUser.Instance.AddCustomerManageArcInfo(onArcCust);
						string msg = string.Empty;
						if (num > 0)
						{
							DataTable dataTable2 = null;
							if (this.Session["CustomerManage"] != null)
							{
								dataTable2 = (this.Session["CustomerManage"] as DataTable);
							}
							if (dataTable2 != null)
							{
								if (!Directory.Exists(this.FilePath))
								{
									Directory.CreateDirectory(this.FilePath);
								}
								string fileName = string.Concat(new object[]
								{
									this.FilePath,
									"\\",
									@int,
									"_",
									DateTime.Now.ToString("yyyyMMddhhmmss"),
									".l"
								});
								dataTable2.WriteXml(fileName, XmlWriteMode.IgnoreSchema, true);
								dataTable2.Dispose();
							}
							Log4J.Instance.Info(string.Concat(new object[]
							{
								Public.GetClientIP(),
								",",
								this.LoginUserModel.UserName,
								",保存客户存档信息 存档ID：",
								@int,
								",身份证号:",
								text2
							}));
							string text3 = string.Format("/*****************************修改在线库未领取体检报告的客户名称 Begin***********************************/\r\n--通过客户存档ID获取其对应的所有客户体检号\r\nSELECT ID_Customer INTO #OnArct@RandNum FROM OnCustRelationCustPEInfo WHERE ID_ArcCustomer IN({0})\r\nAND NOT EXISTS(SELECT ID_Customer FROM(SELECT ID_Customer FROM OnCustReportManage WHERE Is_ReportReceipted=1)OnCustReportManage\r\nWHERE OnCustRelationCustPEInfo.ID_Customer=OnCustReportManage.ID_Customer);\r\n\r\n--修改关系表中的证件号\r\nUPDATE OnCustRelationCustPEInfo SET IDCardNo='{2}' WHERE ID_ArcCustomer IN({0});\r\n\r\n--修改体检信息表中客户名称\r\nUPDATE OnCustPhysicalExamInfo SET CustomerName='{1}' WHERE EXISTS(SELECT ID_Customer FROM #OnArct@RandNum WHERE OnCustPhysicalExamInfo.ID_Customer=#OnArct@RandNum.ID_Customer);\r\n\r\n--修改体检者检查科室结论表中客户名称\r\nUPDATE OnCustExamSection SET CustomerName='{1}' WHERE EXISTS(SELECT ID_Customer FROM #OnArct@RandNum WHERE OnCustExamSection.ID_Customer=#OnArct@RandNum.ID_Customer);\r\n\r\n--修改总审表中客户名称 \r\nUPDATE OnFianlCheck SET CustomerName='{1}' WHERE EXISTS(SELECT ID_Customer FROM #OnArct@RandNum WHERE OnFianlCheck.ID_Customer=#OnArct@RandNum.ID_Customer);\r\n\r\n--删除中间表\r\nDROP TABLE #OnArct@RandNum;\r\n/*****************************修改在线库未领取体检报告的客户名称 End***********************************/", @int, text, text2);
							List<string> list = new List<string>(1);
							text3 = text3.Replace("@RandNum", Public.GetGuid("-", string.Empty));
							list.Add(text3);
							int num2 = CommonExcuteSql.Instance.ExecuteSqlTran(list);
							if (num2 > 0)
							{
								Log4J.Instance.Info(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",成功修改客户存档ID：",
									@int,
									",在线库客户名称为:",
									text
								}));
							}
							else
							{
								Log4J.Instance.Info(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",修改客户存档ID失败：",
									@int,
									",在线库客户名称为:",
									text
								}));
							}
							msg = "{\"success\":\"1\",\"Message\":\"修改成功！\"}";
							this.OutPutMessage(msg);
						}
						else
						{
							Log4J.Instance.Error(string.Concat(new object[]
							{
								Public.GetClientIP(),
								",",
								this.LoginUserModel.UserName,
								",保存客户存档信息失败 存档ID：",
								@int,
								",身份证号:",
								text2
							}));
							msg = "{\"success\":\"0\",\"Message\":\"修改失败！\"}";
							this.OutPutMessage(msg);
						}
						memoryStream.Close();
						memoryStream.Dispose();
					}
					else
					{
						this.jsonInfo = string.Concat(new string[]
						{
							"{\"success\":\"0\",\"Message\":\"已存在客户名称[",
							text,
							"],证件号[",
							text2,
							"]的信息，不允许修改！\"}"
						});
						this.OutPutMessage(this.jsonInfo);
					}
				}
			}
		}
	}
}
