using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using PEIS.Model;
using Maticsoft.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace PEIS.Web.Ajax
{
	public class AjaxCustExam : BasePage
	{
		public string ErrorMessage = string.Empty;

		private DateTime? OCESM_SectionSummaryDate = null;

		private string OCESM_SectionSummary = "";

		private string OCESM_PositiveSummary = "";

		private int? OCESM_ID_SummaryDoctor = null;

		private string OCESM_SummaryDoctorName = "";

		private int? OCESM_ID_Typist = null;

		private string OCESM_TypistName;

		private DateTime? OCESM_TypistDate = null;

		private int OCESM_DiseaseLevel = 0;

		private bool OCESM_Is_Check = false;

		private int OCESM_ID_Section = 0;

		private long OCESM_ID_Customer = 0L;

		private int OCESM_Forsex = 0;

		private string OCESM_ImageFileUrl = "";

		private string OCESM_ExamProjectName = "";

		private string SendInterfaceType = SendInterfaceConfig.SendToInterfaceType;

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public void TestOutMessage()
		{
			this.OutPutMessage("This is the Test Info ... ");
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ErrorMessage = "error";
			string str = base.GetString("action");
			MethodInfo method = base.GetType().GetMethod(str);
			try
			{
				method.Invoke(this, null);
			}
			catch
			{
				this.OutPutMessage(this.ErrorMessage);
			}
		}

		public void GetCustomerExamListArcPhysicalInfo()
		{
			string @string = base.GetString("CustomerIDsStr");
			string querySqlCode = "QueryCust_OAC_OCPEI_Param";
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer_Str", @string, TypeCode.Object)
			};
			DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
			string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
			this.OutPutMessage(msg);
		}

		public void GetCustomerExamList()
		{
			int @int = base.GetInt("pageIndex", 0);
			int int2 = base.GetInt("pageSize", 10);
			int int3 = base.GetInt("examItemID", 0);
			int int4 = base.GetInt("sectionID", 0);
			string text = base.Server.HtmlDecode(base.GetString("BeginExamDate"));
			string text2 = base.Server.HtmlDecode(base.GetString("EndExamDate"));
			int totalCount = 0;
			int num = 0;
			string pageCode = "QueryPagesCustExamListParam";
			if (!string.IsNullOrEmpty(text))
			{
				text += " 00:00:00";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 += " 23:59:59";
			}
			int int5 = base.GetInt("OnlyMySelf", -1);
			int int6 = base.GetInt("IsNotExam", -1);
			SqlConditionInfo[] array;
			if (int6 >= 0)
			{
				pageCode = "QueryPagesCustExamListNotExamed_Param";
				array = new SqlConditionInfo[4];
				array[0] = new SqlConditionInfo("@ID_Section", int4, TypeCode.Int32);
				array[0].Place = 2;
				if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
				{
					array[1] = new SqlConditionInfo("@BeginDate", text, TypeCode.DateTime);
					array[1].ParamOper = ">=";
					array[1].Place = 2;
				}
				if (!string.IsNullOrEmpty(text2) && Input.IsDate(text2))
				{
					array[2] = new SqlConditionInfo("@EndDate", text2, TypeCode.DateTime);
					array[2].ParamOper = "<=";
					array[2].Place = 2;
				}
			}
			else
			{
				array = new SqlConditionInfo[4];
				array[0] = new SqlConditionInfo("@ID_Section", int4, TypeCode.Int32);
				array[0].Place = 2;
				if (this.LoginUserModel.VocationType == 1)
				{
					if (int5 >= 0)
					{
						array[1] = new SqlConditionInfo("@ID_SummaryDoctor", this.LoginUserModel.UserID, TypeCode.Int32);
					}
					if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
					{
						array[2] = new SqlConditionInfo("@SectionSummaryDate", text, TypeCode.DateTime);
						array[2].ParamOper = ">=";
					}
					if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
					{
						array[3] = new SqlConditionInfo("@SectionSummaryDate", text2, TypeCode.DateTime);
						array[3].ParamOper = "<=";
					}
				}
				else
				{
					if (int5 >= 0)
					{
						array[1] = new SqlConditionInfo("@ID_Typist", this.LoginUserModel.UserID, TypeCode.Int32);
					}
					if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
					{
						array[2] = new SqlConditionInfo("@TypistDate", text, TypeCode.DateTime);
						array[2].ParamOper = ">=";
					}
					if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
					{
						array[3] = new SqlConditionInfo("@TypistDate", text2, TypeCode.DateTime);
						array[3].ParamOper = "<=";
					}
				}
			}
			DataTable page = CommonCustExam.Instance.GetPage(pageCode, @int, int2, out totalCount, out num, array);
			foreach (DataRow dataRow in page.Rows)
			{
				if (dataRow["SectionSummaryFormatDate"] != null && !string.IsNullOrEmpty(dataRow["SectionSummaryFormatDate"].ToString()))
				{
					dataRow["SectionSummaryFormatDate"] = Convert.ToDateTime(dataRow["SectionSummaryFormatDate"].ToString()).ToString("yyyy-MM-dd HH:mm");
				}
				if (dataRow["CheckFormatDate"] != null && !string.IsNullOrEmpty(dataRow["CheckFormatDate"].ToString()))
				{
					dataRow["CheckFormatDate"] = Convert.ToDateTime(dataRow["CheckFormatDate"].ToString()).ToString("yyyy-MM-dd HH:mm");
				}
			}
			page.AcceptChanges();
			string text3 = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			text3 = text3.Replace("\\r\\n", "");
			this.OutPutMessage(text3);
		}

		public void GetCustomerExamDetailDataList()
		{
			DateTime now = DateTime.Now;
			int @int = base.GetInt("SectionID", 0);
			long int2 = base.GetInt64("CustomerID", 0L);
			string @string = base.GetString("InterfaceType");
			int int3 = base.GetInt("ID_Gender", 0);
			string querySqlCode = "QueryCust_FeeList_ExamList_Param";
			if (@string.ToUpper() == "LAB")
			{
				querySqlCode = "QueryCust_FeeList_ExamList_Lab_Param";
			}
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Section", @int, TypeCode.Int32),
				new SqlConditionInfo("@ID_Customer", int2, TypeCode.Int64),
				new SqlConditionInfo("@Forsex", int3, TypeCode.Int64)
			};
			if (@int > 0 && int2 > 0L)
			{
				DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				DateTime now2 = DateTime.Now;
				string dateDiff = Public.GetDateDiff("获取客户体检项目列表", now, now2);
				Log4J.Instance.Debug(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",科室：",
					@int,
					",体检号：",
					int2,
					",",
					this.LoginUserModel.UserName,
					",",
					dateDiff
				}));
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void GetCustomerExamSectionList()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			string querySqlCode = "QueryCustomerExamSectionList_Param";
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", @int, TypeCode.Int64)
			};
			if (@int > 0L)
			{
				DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void GetCustomerIDList()
		{
			string @string = base.GetString("IDCardNo");
			string string2 = base.GetString("CustomerName");
			string querySqlCode = "QueryCustomerIDList_Param";
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@IDCard", @string, TypeCode.String),
				new SqlConditionInfo("@CustomerName", string2, TypeCode.String)
			};
			if (!string.IsNullOrEmpty(@string) && !string.IsNullOrEmpty(string2))
			{
				DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void GetCustomerExamItemListAndSummary()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			int int2 = base.GetInt("SectionID", 0);
			string querySqlCode = "QueryCustomerExamItemListAndSummary_Param";
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", @int, TypeCode.Int64),
				new SqlConditionInfo("@ID_Section", int2, TypeCode.Int32)
			};
			if (@int > 0L)
			{
				DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void DeleteCustomerExamItem()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			int int2 = base.GetInt("SectionID", 0);
			string interFaceType = base.GetString("InterfaceType").ToUpper();
			if (@int > 0L)
			{
				int num = CommonCustExam.Instance.DeleteCustomerExamItem(@int, int2, interFaceType);
				if (num > 0)
				{
					int num2 = this.UpdateInterfaceRepeatRecvState(@int.ToString(), int2.ToString());
					if (num2 < 0)
					{
						num = num2;
					}
				}
				this.OutPutMessage(num.ToString());
			}
			else
			{
				this.OutPutMessage("-1");
			}
		}

		public void DeleteCustomerExamItem_LAB()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			int int2 = base.GetInt("SectionID", 0);
			string @string = base.GetString("ApplyID");
			int int3 = base.GetInt("ID_MergerFee", 0);
			string interFaceType = base.GetString("InterfaceType").ToUpper();
			if (@int > 0L)
			{
				int num = CommonCustExam.Instance.DeleteCustomerExamItem_LAB(@int, int2, @string, int3, interFaceType);
				if (num > 0)
				{
					int num2 = this.UpdateInterfaceRepeatRecvState_LAB(@int.ToString(), int2.ToString(), @string.ToString());
					if (num2 < 0)
					{
						num = num2;
					}
				}
				this.OutPutMessage(num.ToString());
			}
			else
			{
				this.OutPutMessage("-1");
			}
		}

		private int SaveSummarySectionExamResult_Lab(PEIS.Model.OnCustExamSection OnCustExamSectionModel)
		{
			int num = 0;
			long value = OnCustExamSectionModel.ID_Customer.Value;
			int value2 = OnCustExamSectionModel.ID_Section.Value;
			int @int = base.GetInt("ID_Gender", 0);
			string querySqlCode = "QueryCust_GetSummaryExamList_Lab_Param";
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Section", value2, TypeCode.Int32),
				new SqlConditionInfo("@ID_Customer", value, TypeCode.Int64),
				new SqlConditionInfo("@Forsex", @int, TypeCode.Int64)
			};
			DataSet dataSet = null;
			if (value > 0L)
			{
				dataSet = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
			}
			int result;
			if (dataSet == null)
			{
				result = 0;
			}
			else
			{
				string text = "";
				int count = dataSet.Tables[0].Rows.Count;
				string text2 = "";
				foreach (DataRow dataRow in dataSet.Tables[3].Rows)
				{
					string a = dataRow["Is_Check"].ToString();
					if (a == "1")
					{
						result = 1;
						return result;
					}
					string a2 = dataRow["IS_giveup"].ToString();
					if (a2 == "1")
					{
						result = 1;
						return result;
					}
					string a3 = dataRow["IS_Refund"].ToString();
					if (a3 == "1")
					{
						result = 1;
						return result;
					}
				}
				string str = "";
				string text3 = "";
				string text4 = "";
				string text5 = "";
				foreach (DataRow dataRow2 in dataSet.Tables[2].Rows)
				{
					str = dataRow2["SepBetweenExamItems"].ToString();
					string text6 = dataRow2["SepBetweenSymptoms"].ToString();
					text3 = dataRow2["TerminalSymbol"].ToString();
					text4 = dataRow2["SepExamAndValue"].ToString();
					text5 = dataRow2["NoBetweenExamItems"].ToString();
					string text7 = dataRow2["NoBetweenSympotms"].ToString();
					text2 = dataRow2["DefaultSummary"].ToString();
				}
				if (count > 0)
				{
					foreach (DataRow dataRow3 in dataSet.Tables[0].Rows)
					{
						string text8 = "";
						int num2 = int.Parse(dataRow3["ID_CustFee"].ToString());
						string text9 = dataRow3["ReportFeeName"].ToString();
						int num3 = 0;
						DataRow[] array = dataSet.Tables[1].Select(" ID_CustFee =  " + num2);
						bool flag = array.Length > 1;
						DataRow[] array2 = array;
						for (int i = 0; i < array2.Length; i++)
						{
							DataRow dataRow4 = array2[i];
							num3++;
							string text10 = text5.Replace("1", num3.ToString());
							int num4 = int.Parse(dataRow3["ID_CustFee"].ToString());
							if (num4 == num2)
							{
								if (!string.IsNullOrEmpty(text8))
								{
									text8 += str;
								}
								if (string.IsNullOrEmpty(text8))
								{
									text8 += "  ";
								}
								if (flag)
								{
									text8 = string.Concat(new string[]
									{
										text8,
										text10,
										dataRow4["ExamItemName"].ToString(),
										text4,
										dataRow4["ResultSummary"].ToString().Replace(" ＋", "").Replace(" ±", "")
									});
								}
								else
								{
									text8 = text8 + dataRow4["ExamItemName"].ToString() + text4 + dataRow4["ResultSummary"].ToString().Replace(" ＋", "").Replace(" ±", "");
								}
							}
						}
						if (!string.IsNullOrEmpty(text8))
						{
							if (!string.IsNullOrEmpty(text))
							{
								text += "\r\n";
							}
							string text11 = text;
							text = string.Concat(new string[]
							{
								text11,
								text9,
								"\r\n",
								text8,
								text3
							});
						}
					}
					this.OCESM_SectionSummaryDate = new DateTime?(DateTime.Now);
					this.OCESM_ID_SummaryDoctor = null;
					this.OCESM_ID_Typist = null;
					this.OCESM_TypistDate = new DateTime?(DateTime.Now);
					this.OCESM_ID_Section = value2;
					this.OCESM_ID_Customer = value;
					this.OCESM_Forsex = @int;
					this.OCESM_PositiveSummary = text;
					this.OCESM_ExamProjectName = "";
					if (!string.IsNullOrEmpty(text))
					{
						this.OCESM_DiseaseLevel = 1;
					}
					else
					{
						this.OCESM_DiseaseLevel = 0;
						if (string.IsNullOrEmpty(text))
						{
							text = text2;
						}
					}
					this.OCESM_SectionSummary = text;
					this.OCESM_Is_Check = false;
					if (!string.IsNullOrEmpty(text))
					{
						try
						{
							OnCustExamSectionModel.ID_Section = new int?(this.OCESM_ID_Section);
							OnCustExamSectionModel.ID_Customer = new long?(this.OCESM_ID_Customer);
							OnCustExamSectionModel.SectionSummary = Secret.AES.EncryptPrefix(this.OCESM_SectionSummary);
							OnCustExamSectionModel.PositiveSummary = Secret.AES.EncryptPrefix(this.OCESM_PositiveSummary);
							OnCustExamSectionModel.SectionSummaryDate = this.OCESM_SectionSummaryDate;
							OnCustExamSectionModel.DiseaseLevel = new int?(this.OCESM_DiseaseLevel);
							num = CommonCustExam.Instance.SaveCustomerSectionSummary(null, null, OnCustExamSectionModel, null, 0, null);
						}
						catch (Exception ex)
						{
							string message = ex.Message;
						}
					}
				}
				result = num;
			}
			return result;
		}

		private int UpdateInterfaceRepeatRecvState(string CustomerID, string SectionID)
		{
			int result;
			try
			{
				List<string> list = new List<string>(1);
				string format = " \r\n                    update OnCustSendToInterfaceApplyList\r\n                    set [RecvState] = 0\r\n                      where ID_Customer = {0} and  ID_Section = {1}  ";
				string item = string.Format(format, CustomerID, SectionID);
				list.Add(item);
				int num = CommandWebserviceInterface.Instance.ExecuteSqlTran(list);
				if (num > 0)
				{
					Log4J.Instance.Info(string.Format("设置体检号【{0}】，科室【{1}】的重新接收标记成功", CustomerID, SectionID));
				}
				else
				{
					Log4J.Instance.Info(string.Format("设置体检号【{0}】，科室【{1}】的重新接收标记失败", CustomerID, SectionID));
				}
				result = num;
			}
			catch (Exception var_4_72)
			{
				result = -2;
			}
			return result;
		}

		private int UpdateInterfaceRepeatRecvState_LAB(string CustomerID, string SectionID, string ApplyID)
		{
			int result;
			try
			{
				List<string> list = new List<string>(1);
				string format = " \r\n                    update OnCustSendToInterfaceApplyList\r\n                    set [RecvState] = 0\r\n                      where ID_Customer = {0} and  ID_Section = {1} and ApplyID = '{2}' ";
				string item = string.Format(format, CustomerID, SectionID, ApplyID);
				list.Add(item);
				int num = CommandWebserviceInterface.Instance.ExecuteSqlTran(list);
				if (num > 0)
				{
					Log4J.Instance.Info(string.Format("设置体检号【{0}】，科室【{1}】的重新接收标记成功", CustomerID, SectionID));
				}
				else
				{
					Log4J.Instance.Info(string.Format("设置体检号【{0}】，科室【{1}】的重新接收标记失败", CustomerID, SectionID));
				}
				result = num;
			}
			catch (Exception var_4_73)
			{
				result = -2;
			}
			return result;
		}

		public void UpdateSectionSummaryCheckState()
		{
			DateTime now = DateTime.Now;
			int @int = base.GetInt("ID_CustExamSection", 0);
			int int2 = base.GetInt("SummaryCheckState", 0);
			try
			{
				PEIS.Model.OnCustExamSection model = PEIS.BLL.OnCustExamSection.Instance.GetModel(@int);
				if (model != null)
				{
					if (int2 == 1)
					{
						if (string.IsNullOrEmpty(model.SectionSummary))
						{
							this.OutPutMessage("-1");
							return;
						}
					}
					model.ID_Typist = new int?(this.LoginUserModel.UserID);
					model.TypistName = this.LoginUserModel.UserName;
					model.TypistDate = new DateTime?(DateTime.Now);
					model.Is_Check = new bool?(int2 == 1);
					model.ID_Checker = new int?(this.LoginUserModel.UserID);
					model.CheckerName = this.LoginUserModel.UserName;
					model.CheckDate = new DateTime?(DateTime.Now);
				}
				int num = CommonCustExam.Instance.UpdateSectionSummaryCheckState(model);
				DateTime now2 = DateTime.Now;
				string text = string.Empty;
				if (int2 == 1)
				{
					text = Public.GetDateDiff("提交分科", now, now2);
				}
				else
				{
					text = Public.GetDateDiff("取回分科", now, now2);
				}
				if (num > 0)
				{
					this.OutPutMessage(model.TypistDate.ToString());
					if (model != null && int2 == 1)
					{
						CommonCustExam.Instance.UpdateLisCustFeeExamItem(model.ID_Customer.Value, model.ID_Section.Value, this.LoginUserModel);
					}
					if (model != null)
					{
						Log4J.Instance.Info(string.Concat(new object[]
						{
							Public.GetClientIP(),
							",科室：",
							model.ID_Section,
							",体检号：",
							model.ID_Customer,
							",",
							this.LoginUserModel.UserName,
							",成功 ",
							text,
							", 科室小结编号：",
							@int,
							",审核状态:",
							int2
						}));
					}
				}
				else
				{
					this.OutPutMessage(0.ToString());
					if (model != null)
					{
						Log4J.Instance.Error(string.Concat(new object[]
						{
							Public.GetClientIP(),
							",科室：",
							model.ID_Section,
							",体检号：",
							model.ID_Customer,
							",",
							this.LoginUserModel.UserName,
							",失败 ",
							text,
							", 科室小结编号：",
							@int,
							",审核状态:",
							int2
						}));
					}
				}
			}
			catch (Exception var_7_353)
			{
				this.OutPutMessage(0.ToString());
			}
		}

		public void UpdateSectionSummaryCheckState_old()
		{
			DateTime now = DateTime.Now;
			int @int = base.GetInt("ID_CustExamSection", 0);
			int int2 = base.GetInt("SummaryCheckState", 0);
			List<int> list = new List<int>();
			list.Add(@int);
			try
			{
				PEIS.Model.OnCustExamSection model = PEIS.BLL.OnCustExamSection.Instance.GetModel(@int);
				if (int2 == 1)
				{
					if (model != null)
					{
						if (string.IsNullOrEmpty(model.SectionSummary))
						{
							this.OutPutMessage("-1");
							return;
						}
					}
				}
				int num = CommonCustExam.Instance.UpdateSectionSummaryCheckState(this.LoginUserModel, int2, list);
				DateTime now2 = DateTime.Now;
				string text = string.Empty;
				if (int2 == 1)
				{
					text = Public.GetDateDiff("提交分科", now, now2);
				}
				else
				{
					text = Public.GetDateDiff("取回分科", now, now2);
				}
				if (num > 0)
				{
					if (model != null && int2 == 1)
					{
						CommonCustExam.Instance.UpdateLisCustFeeExamItem(model.ID_Customer.Value, model.ID_Section.Value, this.LoginUserModel);
					}
					if (model != null)
					{
						Log4J.Instance.Info(string.Concat(new object[]
						{
							Public.GetClientIP(),
							",科室：",
							model.ID_Section,
							",体检号：",
							model.ID_Customer,
							",",
							this.LoginUserModel.UserName,
							",成功 ",
							text,
							", 科室小结编号：",
							@int,
							",审核状态:",
							int2
						}));
					}
				}
				else if (model != null)
				{
					Log4J.Instance.Error(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",科室：",
						model.ID_Section,
						",体检号：",
						model.ID_Customer,
						",",
						this.LoginUserModel.UserName,
						",失败 ",
						text,
						", 科室小结编号：",
						@int,
						",审核状态:",
						int2
					}));
				}
				this.OutPutMessage(num.ToString());
			}
			catch (Exception var_8_2B9)
			{
			}
		}

		public void SaveUploadImageFileResult()
		{
			DateTime now = DateTime.Now;
			int @int = base.GetInt("ID_CustExamSection", 0);
			int int2 = base.GetInt("SectionID", 0);
			string @string = base.GetString("SaveUploadImageFileNames");
			List<int> list = new List<int>();
			list.Add(@int);
			try
			{
				PEIS.Model.OnCustExamSection model = PEIS.BLL.OnCustExamSection.Instance.GetModel(@int);
				if (model == null)
				{
					this.OutPutMessage("-1");
				}
				else
				{
					model.ImageUrl = @string;
					int num = CommonCustExam.Instance.UpdateSectionImageUrl(@int, @string);
					DateTime now2 = DateTime.Now;
					string text = string.Empty;
					text = Public.GetDateDiff("保存科室图像", now, now2);
					if (num > 0)
					{
						Log4J.Instance.Info(string.Concat(new object[]
						{
							Public.GetClientIP(),
							",科室：",
							model.ID_Section,
							",体检号：",
							model.ID_Customer,
							",",
							this.LoginUserModel.UserName,
							",成功 ",
							text,
							", 科室小结编号：",
							@int,
							",图片文件:",
							@string
						}));
					}
					else if (model != null)
					{
						Log4J.Instance.Error(string.Concat(new object[]
						{
							Public.GetClientIP(),
							",科室：",
							model.ID_Section,
							",体检号：",
							model.ID_Customer,
							",",
							this.LoginUserModel.UserName,
							",失败 ",
							text,
							", 科室小结编号：",
							@int,
							",图片文件:",
							@string
						}));
					}
					this.OutPutMessage(num.ToString());
				}
			}
			catch (Exception var_9_224)
			{
			}
		}

		public void SaveCustomerSectionSummaryInfo()
		{
			DateTime now = DateTime.Now;
			int @int = base.GetInt("ID_CustExamSection", 0);
			long int2 = base.GetInt64("ID_Customer", 0L);
			int int3 = base.GetInt("SectionID", 0);
			string text = base.GetString("InterfaceType").ToUpper();
			string text2 = base.GetString("SectionSummary");
			text2 = Input.URLDecode(text2);
			string @string = base.GetString("SummaryDoctorName");
			string text3 = base.GetString("PositiveSummary");
			text3 = Input.URLDecode(text3);
			int int4 = base.GetInt("SectionMaxDiseaseLevel", 0);
			string string2 = base.GetString("CustFeeDataInfoListStr");
			string string3 = base.GetString("CustExamDataInfoListStr");
			string string4 = base.GetString("CustReportDataInfo");
			string text4 = base.GetString("SectionSummaryDate");
			text4 = Input.URLDecode(text4);
			DateTime value = DateTime.Now;
			int int5 = base.GetInt("IS_NotUseLastSaveData", 0);
			int iD_FeeReportMerger = 0;
			if (!string.IsNullOrEmpty(text4))
			{
				try
				{
					value = DateTime.Parse(text4);
				}
				catch (Exception var_16_F8)
				{
				}
			}
			DateTime typistDate = DateTime.Now;
			string text5 = base.GetString("TypistDate");
			text5 = Input.URLDecode(text5);
			if (!string.IsNullOrEmpty(text5))
			{
				try
				{
					typistDate = DateTime.Parse(text5);
				}
				catch (Exception var_16_F8)
				{
				}
			}
			if (!(text.ToUpper() == "LAB"))
			{
				if (this.VerifySectionExamTypistDateIsChanged(int3, int2, typistDate))
				{
					return;
				}
			}
			PEIS.Model.OnCustExamSection onCustExamSection = null;
			PEIS.Model.OnCustApply onCustApply = null;
			List<PEIS.Model.OnCustFee> list = null;
			List<PEIS.Model.OnCustExamItem> list2 = null;
			onCustExamSection = new PEIS.Model.OnCustExamSection();
			onCustExamSection.ID_CustExamSection = @int;
			onCustExamSection.ID_Customer = new long?(int2);
			onCustExamSection.ID_Section = new int?(int3);
			onCustExamSection.SectionSummary = Secret.AES.EncryptPrefix(text2);
			onCustExamSection.PositiveSummary = Secret.AES.EncryptPrefix(text3);
			onCustExamSection.SectionSummaryDate = new DateTime?(value);
			onCustExamSection.DiseaseLevel = new int?(int4);
			onCustExamSection.ID_SummaryDoctor = new int?(this.LoginUserModel.UserID);
			onCustExamSection.SummaryDoctorName = this.LoginUserModel.UserName;
			onCustExamSection.ID_Typist = new int?(this.LoginUserModel.UserID);
			onCustExamSection.TypistName = this.LoginUserModel.UserName;
			onCustExamSection.TypistDate = new DateTime?(DateTime.Now);
			if (text == "LAB")
			{
				onCustApply = new PEIS.Model.OnCustApply();
				iD_FeeReportMerger = 0;
				string[] array = string4.Split(new char[]
				{
					'、'
				});
				if (array.Length > 4)
				{
					onCustApply.ID_Apply = array[0].ToString();
					onCustApply.Is_TypistFinish = new bool?(array[1].ToString() == "1");
					onCustApply.ID_Section = new int?(int3);
					onCustApply.ID_Customer = new long?(int2);
					onCustApply.ID_Typist = new int?(this.LoginUserModel.UserID);
					onCustApply.TypistName = this.LoginUserModel.UserName;
					onCustApply.TypistDate = onCustExamSection.TypistDate;
					onCustApply.ID_DetectionDoctor = ((array[3].ToString() == "") ? null : new int?(int.Parse(array[3].ToString())));
					onCustApply.DetectionDoctorName = Input.URLDecode(array[4].ToString());
					if (array[2].ToString().Trim() != "")
					{
						iD_FeeReportMerger = int.Parse(array[2].ToString());
					}
					else
					{
						iD_FeeReportMerger = 0;
					}
					onCustApply.SendProjectIDs = Input.URLDecode(array[5].ToString());
					onCustApply.SendGroupParams = Input.URLDecode(array[6].ToString());
					onCustApply.ReportTime = onCustApply.TypistDate;
					onCustApply.CreateTime = onCustApply.TypistDate;
					if (!string.IsNullOrEmpty(onCustApply.SendGroupParams))
					{
						onCustApply.SpecimenName = onCustApply.SendGroupParams.Substring(onCustApply.SendGroupParams.LastIndexOf('|') + 1);
					}
					onCustApply.SpecimenNo = onCustApply.ID_Apply;
				}
			}
			string[] array2 = string2.Split(new char[]
			{
				'|'
			});
			if (array2.Length > 0)
			{
				list = new List<PEIS.Model.OnCustFee>();
				int i = 0;
				while (i < array2.Length)
				{
					if (array2[i].Length > 0)
					{
						string[] array3 = array2[i].Split(new char[]
						{
							'、'
						});
						if (array3.Length >= 3)
						{
							PEIS.Model.OnCustFee onCustFee = new PEIS.Model.OnCustFee();
							onCustFee.ID_Fee = new int?(int.Parse(array3[0].ToString()));
							onCustFee.ID_CustFee = int.Parse(array3[1].ToString());
							onCustFee.ID_ExamDoctor = ((array3[2].ToString() == "") ? null : new int?(int.Parse(array3[2].ToString())));
							onCustFee.ExamDoctorName = Input.URLDecode(array3[3].ToString());
							onCustFee.ExamDate = onCustExamSection.SectionSummaryDate;
							onCustFee.Is_Examined = new bool?(true);
							onCustExamSection.ID_SummaryDoctor = onCustFee.ID_ExamDoctor;
							onCustExamSection.SummaryDoctorName = onCustFee.ExamDoctorName;
							if (text.ToUpper() == "LAB")
							{
								if (string.IsNullOrEmpty(onCustApply.DetectionDoctorName))
								{
									onCustApply.ID_DetectionDoctor = onCustExamSection.ID_SummaryDoctor;
									onCustApply.DetectionDoctorName = onCustExamSection.SummaryDoctorName;
								}
								if (!string.IsNullOrEmpty(array3[4].ToString()))
								{
									onCustFee.ExamDate = new DateTime?(Convert.ToDateTime(Input.URLDecode(array3[4].ToString())));
								}
								else
								{
									onCustFee.ExamDate = new DateTime?(DateTime.Now);
								}
								onCustFee.Is_Examined = new bool?(!(Input.URLDecode(array3[5].ToString()) == "0"));
							}
							list.Add(onCustFee);
						}
					}
					IL_67C:
					i++;
					continue;
					goto IL_67C;
				}
			}
			string[] array4 = string3.Split(new char[]
			{
				'|'
			});
			if (array4.Length > 0)
			{
				list2 = new List<PEIS.Model.OnCustExamItem>();
				string empty = string.Empty;
				string text6 = string.Empty;
				int i = 0;
				while (i < array4.Length)
				{
					if (array4[i].Length > 0)
					{
						string[] array5 = array4[i].Split(new char[]
						{
							'、'
						});
						if (array5.Length >= 4)
						{
							PEIS.Model.OnCustExamItem onCustExamItem = new PEIS.Model.OnCustExamItem();
							onCustExamItem.ID_Fee = new int?(int.Parse(array5[0].ToString()));
							onCustExamItem.ID_CustFee = new int?(int.Parse(array5[1].ToString()));
							onCustExamItem.ID_ExamItem = int.Parse(array5[2].ToString());
							onCustExamItem.ExamItemName = Input.URLDecode(array5[3].ToString());
							onCustExamItem.ResultSummary = Input.URLDecode(array5[4].ToString());
							onCustExamItem.ID_Customer = new long?(int2);
							if (onCustApply != null && !string.IsNullOrEmpty(onCustApply.ID_Apply))
							{
								onCustExamItem.ID_CustApply = onCustApply.ID_Apply;
							}
							text6 = Input.URLDecode(array5[5].ToString());
							if (!string.IsNullOrEmpty(array5[6]))
							{
								onCustExamItem.ID_CustExamItem = int.Parse(array5[6].ToString());
							}
							else
							{
								onCustExamItem.ID_CustExamItem = 0;
							}
							onCustExamItem.DiseaseLevel = new int?(0);
							onCustExamItem.ExamItemSummaryDate = DateTime.Now;
							onCustExamItem.ID_SummaryDoctor = ((array5[7].ToString() == "") ? null : new int?(int.Parse(array5[7].ToString())));
							onCustExamItem.SummaryDoctorName = Input.URLDecode(array5[8].ToString());
							try
							{
								onCustExamItem.DiseaseLevel = new int?(int.Parse(array5[9].ToString()));
							}
							catch (Exception var_16_F8)
							{
								onCustExamItem.DiseaseLevel = new int?(0);
							}
							onCustExamItem.ID_Typist = new int?(this.LoginUserModel.UserID);
							onCustExamItem.TypistName = this.LoginUserModel.UserName;
							onCustExamItem.ResultLabValues = "";
							onCustExamItem.ResultNumber = null;
							onCustExamItem.ResultLabRange = "";
							onCustExamItem.ResultLabLowLimit = null;
							onCustExamItem.ResultLabHighLimit = null;
							onCustExamItem.ResultLabUnit = "";
							onCustExamItem.ResultLabMark = "";
							onCustExamItem.SCO = "";
							onCustExamItem.DetectionMethod = "";
							onCustExamItem.AbbrExamName = "";
							if (!string.IsNullOrEmpty(array5[10]) && array5[10].ToUpper() == "N" && !string.IsNullOrEmpty(array5[11]) && Input.IsDecimal(array5[11].ToString()))
							{
								onCustExamItem.ResultNumber = new decimal?(decimal.Parse(array5[11].ToString()));
							}
							else
							{
								onCustExamItem.ResultLabValues = Input.URLDecode(array5[11].ToString()).Trim();
							}
							if (text == "LAB")
							{
								onCustExamItem.ResultLabMark = Input.URLDecode(array5[4].ToString());
								if (!string.IsNullOrEmpty(onCustExamItem.ResultLabMark.Trim()))
								{
									onCustExamItem.DiseaseLevel = new int?(1);
								}
								else
								{
									onCustExamItem.DiseaseLevel = new int?(0);
								}
								onCustExamItem.ResultNumber = null;
								onCustExamItem.ResultLabValues = Input.URLDecode(array5[5].ToString()).Trim();
								if (!string.IsNullOrEmpty(array5[9].ToString()))
								{
									onCustExamItem.ResultLabLowLimit = new decimal?(decimal.Parse(array5[9].ToString()));
								}
								if (!string.IsNullOrEmpty(array5[10].ToString()))
								{
									onCustExamItem.ResultLabHighLimit = new decimal?(decimal.Parse(array5[10].ToString()));
								}
								onCustExamItem.ResultLabRange = Input.URLDecode(array5[11].ToString());
								onCustExamItem.ResultLabUnit = Input.URLDecode(array5[12].ToString());
								onCustExamItem.SCO = Input.URLDecode(array5[13].ToString());
								onCustExamItem.DetectionMethod = Input.URLDecode(array5[14].ToString());
								onCustExamItem.AbbrExamName = Input.URLDecode(array5[15].ToString());
								if (!string.IsNullOrEmpty(onCustExamItem.ResultLabValues))
								{
									onCustExamItem.ResultSummary = onCustExamItem.ResultLabValues + " " + onCustExamItem.ResultLabUnit + onCustExamItem.ResultLabMark;
								}
							}
							else if (!string.IsNullOrEmpty(text6))
							{
								string[] array6 = text6.Split(new char[]
								{
									'、'
								});
								if (array6.Length > 2)
								{
									onCustExamItem.ExamItemResultList = new List<PEIS.Model.OnCustExamItemResult>();
									for (int j = 0; j < array6.Length; j++)
									{
										if (!string.IsNullOrEmpty(array6[j]))
										{
											PEIS.Model.OnCustExamItemResult onCustExamItemResult = new PEIS.Model.OnCustExamItemResult();
											onCustExamItemResult.ID_CustExamItem = new int?(onCustExamItem.ID_ExamItem);
											onCustExamItemResult.ID_Symptom = new int?(int.Parse(Input.HtmlDecode(Input.HtmlDecode(array6[j])).ToString()));
											onCustExamItem.ExamItemResultList.Add(onCustExamItemResult);
										}
									}
								}
							}
							onCustExamItem.ResultLabMark = Secret.AES.EncryptPrefix(onCustExamItem.ResultLabMark);
							onCustExamItem.ResultLabValues = Secret.AES.EncryptPrefix(onCustExamItem.ResultLabValues);
							onCustExamItem.ResultSummary = Secret.AES.EncryptPrefix(onCustExamItem.ResultSummary);
							list2.Add(onCustExamItem);
						}
					}
					IL_CD3:
					i++;
					continue;
					goto IL_CD3;
				}
			}
			SqlConditionInfo[] array7 = null;
			if (@string != "")
			{
				array7 = new SqlConditionInfo[]
				{
					new SqlConditionInfo("@Is_FirstSaveSummary", 0, TypeCode.Int32),
					new SqlConditionInfo("@IS_NotUseLastSaveData", int5, TypeCode.Int32)
				};
			}
			if (text == "LAB")
			{
				array7 = new SqlConditionInfo[2];
				array7[0] = new SqlConditionInfo("@InterfaceType", "LAB", TypeCode.String);
			}
			DateTime now2 = DateTime.Now;
			string dateDiff = Public.GetDateDiff(" Start 保存科室小结", now, now2);
			Log4J.Instance.Info(string.Concat(new object[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",科室：",
				onCustExamSection.ID_Section,
				",体检号：",
				onCustExamSection.ID_Customer,
				",",
				dateDiff
			}));
			int num = CommonCustExam.Instance.SaveCustomerSectionSummary(list2, list, onCustExamSection, onCustApply, iD_FeeReportMerger, array7);
			now2 = DateTime.Now;
			dateDiff = Public.GetDateDiff("保存科室小结", now, now2);
			if (text == "LAB")
			{
				num = this.SaveSummarySectionExamResult_Lab(onCustExamSection);
				if (num <= 0)
				{
					Log4J.Instance.Error(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",科室：",
						onCustExamSection.ID_Section,
						",体检号：",
						onCustExamSection.ID_Customer,
						",失败 ",
						dateDiff,
						",自动汇总并保存科室小结失败。"
					}));
				}
			}
			if (num > 0)
			{
				num = 1;
				base.AddOrUpdateByBackLogType(int2, EnumBusBackLogType.分科体检, true, null);
				Log4J.Instance.Info(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",科室：",
					onCustExamSection.ID_Section,
					",体检号：",
					onCustExamSection.ID_Customer,
					",成功 ",
					dateDiff,
					",影响行数:",
					num.ToString(),
					",小结内容:",
					text2
				}));
			}
			else
			{
				Log4J.Instance.Error(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",科室：",
					onCustExamSection.ID_Section,
					",体检号：",
					onCustExamSection.ID_Customer,
					",失败 ",
					dateDiff,
					",小结内容:",
					text2
				}));
			}
			this.OutPutMessage(num.ToString());
		}

		private bool VerifySectionExamTypistDateIsChanged(int SectionID, long ID_Customer, DateTime TypistDate)
		{
			string querySqlCode = "QueryCust_ExamSection_SaveData_Param";
			bool result;
			try
			{
				SqlConditionInfo[] conditions = new SqlConditionInfo[]
				{
					new SqlConditionInfo("@ID_Section", SectionID, TypeCode.Int32),
					new SqlConditionInfo("@ID_Customer", ID_Customer, TypeCode.Int64)
				};
				DataSet dataSet = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					if (dataSet.Tables[0].Rows[0]["TypistDate"].ToString() != "" && Convert.ToDateTime(dataSet.Tables[0].Rows[0]["TypistDate"].ToString()) != TypistDate)
					{
						string msg = string.Concat(new object[]
						{
							"体检号",
							ID_Customer,
							"的该科室数据已由【",
							dataSet.Tables[0].Rows[0]["TypistName"].ToString(),
							"】于 [",
							dataSet.Tables[0].Rows[0]["TypistDate"].ToString(),
							"] 进行了操作，请重新进入本科室再操作！"
						});
						this.OutPutMessage(msg);
						result = true;
						return result;
					}
				}
			}
			catch (Exception var_4_198)
			{
			}
			result = false;
			return result;
		}

		public void GetCustomerExamLastSaveDataList()
		{
			DateTime now = DateTime.Now;
			int @int = base.GetInt("SectionID", 0);
			long int2 = base.GetInt64("CustomerID", 0L);
			string querySqlCode = "QueryCust_Exam_SaveData_Param";
			string @string = base.GetString("InterfaceType");
			if (@string.ToUpper() == "LAB")
			{
				querySqlCode = "QueryCust_Exam_SaveData_Lab_Param";
			}
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Section", @int, TypeCode.Int32),
				new SqlConditionInfo("@ID_Customer", int2, TypeCode.Int64)
			};
			if (@int > 0 && int2 > 0L)
			{
				DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				DateTime now2 = DateTime.Now;
				string dateDiff = Public.GetDateDiff("获取客户体检项目对应结果值", now, now2);
				Log4J.Instance.Debug(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",科室：",
					@int,
					",体检号：",
					int2,
					",",
					this.LoginUserModel.UserName,
					",",
					dateDiff
				}));
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void BandingCustomerSectionExamInfo()
		{
			DateTime now = DateTime.Now;
			int @int = base.GetInt("ID_CustExamSection", 0);
			int int2 = base.GetInt("SectionID", 0);
			long int3 = base.GetInt64("ID_Customer", 0L);
			PEIS.Model.OnCustExamSection onCustExamSection = new PEIS.Model.OnCustExamSection();
			onCustExamSection.SectionSummaryDate = new DateTime?(DateTime.Now);
			onCustExamSection.ID_SummaryDoctor = new int?(this.LoginUserModel.UserID);
			onCustExamSection.SummaryDoctorName = "";
			onCustExamSection.ID_Typist = new int?(this.LoginUserModel.UserID);
			onCustExamSection.TypistName = this.LoginUserModel.UserName;
			onCustExamSection.TypistDate = new DateTime?(DateTime.Now);
			onCustExamSection.ID_CustExamSection = @int;
			onCustExamSection.ID_Customer = new long?(int3);
			int num = CommonCustExam.Instance.BandingCustomerSectionExamInfo(onCustExamSection);
			if (num > 0)
			{
				DateTime now2 = DateTime.Now;
				string dateDiff = Public.GetDateDiff("记录首次进入分检", now, now2);
				this.OutPutMessage(onCustExamSection.TypistDate.ToString());
				Log4J.Instance.Info(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",科室：",
					int2,
					",体检号：",
					onCustExamSection.ID_Customer,
					",科室小结编号：",
					onCustExamSection.ID_CustExamSection,
					",",
					dateDiff
				}));
			}
			else
			{
				if (num != 0)
				{
					Log4J.Instance.Error(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",记录首次进入分检-失败 科室：",
						int2,
						",体检号：",
						onCustExamSection.ID_Customer,
						",科室小结编号：",
						onCustExamSection.ID_CustExamSection
					}));
				}
				this.OutPutMessage("");
			}
		}

		public void QueryCustomerExamItemDiseaseLevelTipsList()
		{
			DateTime now = DateTime.Now;
			long @int = base.GetInt64("CustomerID", 0L);
			int int2 = base.GetInt("MinDiseaseLevel", 0);
			int int3 = base.GetInt("SectionID", 0);
			string querySqlCode = "QueryCustomerExamItemDiseaseLevelTips_Param";
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", @int, TypeCode.Int64),
				new SqlConditionInfo("@DiseaseLevel", int2, TypeCode.Int32)
			};
			if (@int > 0L)
			{
				DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				DateTime now2 = DateTime.Now;
				string dateDiff = Public.GetDateDiff("获取客户病症级别列表", now, now2);
				Log4J.Instance.Debug(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",科室：",
					int3,
					",体检号：",
					@int,
					",最小病症级别：",
					int2,
					",",
					this.LoginUserModel.UserName,
					",",
					dateDiff
				}));
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void GetGuideSheetReturnedList()
		{
			int @int = base.GetInt("pageIndex", 0);
			int int2 = base.GetInt("pageSize", 10);
			string text = base.Server.HtmlDecode(base.GetString("BeginExamDate"));
			string text2 = base.Server.HtmlDecode(base.GetString("EndExamDate"));
			int totalCount = 0;
			int num = 0;
			string pageCode = "QueryGuideSheetReturnedPagesParam";
			if (!string.IsNullOrEmpty(text))
			{
				text += " 00:00:00";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 += " 23:59:59";
			}
			int int3 = base.GetInt("OnlyMySelf", -1);
			int int4 = base.GetInt("IsNotExam", -1);
			int int5 = base.GetInt("selIsTeam", -1);
			SqlConditionInfo[] array = new SqlConditionInfo[5];
			if (int3 >= 0)
			{
				array[0] = new SqlConditionInfo("@ID_UserGuideSheetReturnedBy", this.LoginUserModel.UserID, TypeCode.Int32);
			}
			if (int4 == -1)
			{
				if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
				{
					array[1] = new SqlConditionInfo("@GuideSheetReturnedDate", text, TypeCode.DateTime);
					array[1].ParamOper = ">=";
				}
				if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
				{
					array[2] = new SqlConditionInfo("@GuideSheetReturnedDate", text2, TypeCode.DateTime);
					array[2].ParamOper = "<=";
				}
				array[3] = new SqlConditionInfo("@Is_GuideSheetReturned", 1, TypeCode.Int32);
				array[3].Place = 2;
			}
			else
			{
				if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
				{
					array[1] = new SqlConditionInfo("@SubScribDate", text, TypeCode.DateTime);
					array[1].ParamOper = ">=";
				}
				if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
				{
					array[2] = new SqlConditionInfo("@SubScribDate", text2, TypeCode.DateTime);
					array[2].ParamOper = "<=";
				}
				array[3] = new SqlConditionInfo("@Is_GuideSheetReturned", 0, TypeCode.Int32);
				array[3].Place = 2;
			}
			if (int5 >= 0)
			{
				array[4] = new SqlConditionInfo("@Is_Team", int5, TypeCode.Int32);
				array[4].Place = 3;
			}
			DataTable page = CommonCustExam.Instance.GetPage(pageCode, @int, int2, out totalCount, out num, array);
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		public void GetGuideSheetReturnExamSectionItem()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", @int, TypeCode.Int64)
			};
			string querySqlCode = "QueryGuideSheetReturn_ExamSectionList_Param";
			if (@int > 0L)
			{
				DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void SetGuideSheetReturnState()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", @int, TypeCode.Int64)
			};
			string querySqlCode = "QueryGuideSheetReturn_ExamSectionList_Param";
			int num = 1;
			if (@int > 0L)
			{
				DataSet dataSet = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
				{
					this.OutPutMessage("-1");
				}
				else
				{
					if (dataSet.Tables[0].Rows.Count > 0)
					{
						if (dataSet.Tables[0].Rows[0]["Is_FinalFinished"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_FinalFinished"].ToString().ToLower() == "true")
						{
							this.OutPutMessage("-4");
							return;
						}
						if (dataSet.Tables[0].Rows[0]["Is_Checked"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_Checked"].ToString().ToLower() == "true")
						{
							this.OutPutMessage("-3");
							return;
						}
						if (dataSet.Tables[0].Rows[0]["Is_GuideSheetReturned"].ToString() == "1" || dataSet.Tables[0].Rows[0]["Is_GuideSheetReturned"].ToString().ToLower() == "true")
						{
							this.OutPutMessage("-2");
							return;
						}
						if (dataSet.Tables[0].Rows[0]["Is_GuideSheetPrinted"].ToString() != "1" && dataSet.Tables[0].Rows[0]["Is_GuideSheetPrinted"].ToString().ToLower() != "true")
						{
							this.OutPutMessage("-5");
							return;
						}
					}
					if (dataSet.Tables[1].Rows.Count > 0)
					{
						for (int i = 0; i < dataSet.Tables[1].Rows.Count; i++)
						{
							if (string.IsNullOrEmpty(dataSet.Tables[1].Rows[i]["InterfaceType"].ToString()))
							{
								if (dataSet.Tables[1].Rows[i]["SummaryDoctorName"] == null || string.IsNullOrEmpty(dataSet.Tables[1].Rows[i]["SummaryDoctorName"].ToString()))
								{
									if (dataSet.Tables[1].Rows[i]["IS_giveup"] == null || dataSet.Tables[1].Rows[i]["IS_giveup"].ToString() == "0" || dataSet.Tables[1].Rows[i]["IS_giveup"].ToString().ToLower() == "false" || string.IsNullOrEmpty(dataSet.Tables[1].Rows[i]["IS_giveup"].ToString()))
									{
										num = 0;
										break;
									}
								}
							}
						}
					}
					if (num == 1)
					{
						PEIS.Model.OnCustPhysicalExamInfo onCustPhysicalExamInfo = new PEIS.Model.OnCustPhysicalExamInfo();
						onCustPhysicalExamInfo.ID_Customer = @int;
						onCustPhysicalExamInfo.Is_GuideSheetReturned = new bool?(true);
						onCustPhysicalExamInfo.ID_UserGuideSheetReturnedBy = new int?(this.LoginUserModel.UserID);
						onCustPhysicalExamInfo.GuideSheetReturnedby = this.LoginUserModel.UserName;
						onCustPhysicalExamInfo.GuideSheetReturnedDate = new DateTime?(DateTime.Now);
						int num2 = CommonCustExam.Instance.UpdateGuideSheetReturnState(onCustPhysicalExamInfo);
						if (num2 > 0)
						{
							base.AddOrUpdateByBackLogType(@int, EnumBusBackLogType.指引单回收, true, null);
							Log4J.Instance.Info(string.Concat(new object[]
							{
								Public.GetClientIP(),
								",",
								this.LoginUserModel.UserName,
								",指引单回收成功 ,体检号：",
								@int
							}));
						}
						else
						{
							Log4J.Instance.Error(string.Concat(new object[]
							{
								Public.GetClientIP(),
								",",
								this.LoginUserModel.UserName,
								",指引单回收失败 ,体检号：",
								@int
							}));
						}
						this.OutPutMessage(num2.ToString());
					}
					else
					{
						this.OutPutMessage("-6");
					}
				}
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void SetExamSectionGiveUp()
		{
			int @int = base.GetInt("Is_GiveUp", 0);
			int int2 = base.GetInt("ID_CustExamSection", 0);
			string @string = base.GetString("CustomerID");
			string string2 = base.GetString("ID_Section");
			PEIS.Model.OnCustExamSection onCustExamSection = new PEIS.Model.OnCustExamSection();
			onCustExamSection.IS_giveup = new bool?(@int == 1);
			onCustExamSection.ID_CustExamSection = int2;
			int num = CommonCustExam.Instance.UpdateExamSectionGiveUpState(onCustExamSection);
			string text;
			if (@int == 1)
			{
				text = "";
			}
			else
			{
				text = "恢复";
			}
			if (num > 0)
			{
				Log4J.Instance.Info(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",科室：",
					string2,
					",体检号：",
					@string,
					",",
					text,
					"弃检成功 ,客户检查科室编号：",
					int2
				}));
			}
			else
			{
				Log4J.Instance.Error(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",科室：",
					string2,
					",体检号：",
					@string,
					",",
					text,
					"弃检失败 ,客户检查科室编号：",
					int2
				}));
			}
			this.OutPutMessage(num.ToString());
		}

		public void SetAllNotExamedSectionGiveUp()
		{
			int @int = base.GetInt("Is_GiveUp", 0);
			long int2 = base.GetInt64("CustomerID", 0L);
			PEIS.Model.OnCustExamSection onCustExamSection = new PEIS.Model.OnCustExamSection();
			onCustExamSection.IS_giveup = new bool?(@int == 1);
			onCustExamSection.ID_Customer = new long?(int2);
			int num = CommonCustExam.Instance.UpdateAllNotExamedSectionGiveUp(onCustExamSection);
			if (num > 0)
			{
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",体检号：",
					int2.ToString(),
					",批量弃检成功"
				}));
			}
			else
			{
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",体检号：",
					int2.ToString(),
					",批量弃检失败"
				}));
			}
			this.OutPutMessage(num.ToString());
		}

		public void RepeatCustomerLisPacsProject()
		{
            string str = base.GetString("ID_Customer").Trim();
            List<string> sQLStringList = new List<string>(1);
            string format = " \r\n                    update OnCustWaitSendToInterface\r\n                    set [SendState] = 0\r\n                      where ID_Customer = {0} ";
            string item = string.Format(format, str);
            sQLStringList.Add(item);
            int num = CommandWebserviceInterface.Instance.ExecuteSqlTran(sQLStringList);
            num = this.SendWaitToInterfaceByRepeat(str);
            if (num > 0)
            {
                Log4J.Instance.Info(string.Format("{0}提交重发【LIS/PACS】申请成功，重发体检号为：{1}", base.UserName, str));
            }
            else
            {
                Log4J.Instance.Info(string.Format("{0}提交重发【LIS/PACS】申请失败，重发体检号为：{1}", base.UserName, str));
            }
            this.OutPutMessage(num.ToString());
        }


        private int SendWaitToInterfaceByRepeat(string ID_Customer)
		{
			DateTime now = DateTime.Now;
			int result = 0;
			string[] array = this.SendInterfaceType.Trim().TrimEnd(new char[]
			{
				'|'
			}).Split(new char[]
			{
				'|'
			});
			string text = string.Empty;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text2 = array2[i];
				text = text + "'" + text2.Trim() + "',";
			}
			text = text.TrimEnd(new char[]
			{
				','
			});
			try
			{
				DataTable dataTable = CommonOnArcCust.Instance.GetOnCustWaitSendToInterfaceInfo_Ex(ID_Customer, text).Tables[0];
				result = this.SendWaitToInterface(dataTable);
				dataTable.Dispose();
			}
			catch (Exception ex)
			{
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",读取待发送接口列表出错 体检号为：",
					ID_Customer,
					" 错误原因：",
					ex.Message,
					" 耗时：",
					Public.GetDateDiff(string.Empty, now, DateTime.Now)
				}));
				result = 0;
			}
			return result;
		}

		public int SendWaitToInterface(DataTable OnCustExamSectionDT)
		{
			string empty = string.Empty;
			DateTime now = DateTime.Now;
			int result;
			if (OnCustExamSectionDT == null || OnCustExamSectionDT.Rows.Count == 0)
			{
				result = 0;
			}
			else
			{
				int num = (OnCustExamSectionDT.Rows[0]["ID_Gender"].ToString().Trim() == "1") ? 1 : 0;
				string text = OnCustExamSectionDT.Rows[0]["ID_Customer"].ToString().Trim();
				StringBuilder stringBuilder = new StringBuilder();
				string str = string.Empty;
				foreach (DataRow dataRow in OnCustExamSectionDT.Rows)
				{
					if (dataRow["IS_Refund"].ToString().Trim() == "1" || dataRow["IS_Refund"].ToString().Trim() == "True")
					{
						str = str + "'" + dataRow["ID_Section"].ToString() + "',";
					}
					else
					{
						stringBuilder.AppendFormat("INSERT INTO OnCustWaitSendToInterface (ID_Customer,ID_Section,InterfaceType,Forsex) values ('{0}','{1}','{2}','{3}');", new object[]
						{
							dataRow["ID_Customer"].ToString(),
							dataRow["ID_Section"].ToString(),
							dataRow["InterfaceType"].ToString(),
							num
						});
					}
				}
				List<string> list = new List<string>(3);
				string item = string.Format("DELETE FROM OnCustWaitSendToInterface WHERE ID_Customer='{0}';", text);
				list.Add(item);
				if (stringBuilder.Length > 0)
				{
					list.Add(stringBuilder.ToString());
					OnCustExamSectionDT.Dispose();
				}
				try
				{
					int num2 = CommandWebserviceInterface.Instance.ExecuteSqlTran(list);
					if (num2 > 0)
					{
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",成功写入接口待发送列表 体检号为：",
							text,
							" ",
							Public.GetDateDiff(string.Empty, now, DateTime.Now)
						}));
					}
					else
					{
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",无数据发送到接口,待发送列表为空 0行受影响 体检号为：",
							text,
							" ",
							Public.GetDateDiff(string.Empty, now, DateTime.Now)
						}));
					}
					result = num2;
				}
				catch (Exception ex)
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",写入接口待发送列表失败 体检号为：",
						text,
						" ",
						Public.GetDateDiff(string.Empty, now, DateTime.Now),
						" 错误原因为：",
						ex.Message
					}));
					result = 0;
				}
			}
			return result;
		}

		public void GetCustomerLisPacsFeeItem()
		{
			long @int = base.GetInt64("ID_Customer", 0L);
			string querySqlCode = "GetCustomerLisPacsFeeItem";
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", @int, TypeCode.Int64)
			};
			if (@int > 0L)
			{
				DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void GetHistoryCustomerIDSectionExamItem()
		{
			long @int = base.GetInt64("ID_Customer", 0L);
			int int2 = base.GetInt("ID_Section", 0);
			int int3 = base.GetInt("CustomerExamState", 0);
			string querySqlCode = "QueryCustomerExamItemListAndSummary_Param";
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", @int, TypeCode.Int64),
				new SqlConditionInfo("@ID_Section", int2, TypeCode.Int32),
				new SqlConditionInfo("@CustomerExamState", int3, TypeCode.Int32)
			};
			if (@int > 0L)
			{
				DataSet ds = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		public void GetCustomerCompareExamItem()
		{
			int @int = base.GetInt("ID_Section", 0);
			long int2 = base.GetInt64("ID_Customer01", 0L);
			long int3 = base.GetInt64("ID_Customer02", 0L);
			int int4 = base.GetInt("CustomerExamState01", 0);
			int int5 = base.GetInt("CustomerExamState02", 0);
			string querySqlCode = "QueryCustomerFeeItemExamItemListAndSummary_Param";
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", int2, TypeCode.Int64),
				new SqlConditionInfo("@ID_Section", @int, TypeCode.Int32),
				new SqlConditionInfo("@CustomerExamState", int4, TypeCode.Int32)
			};
			SqlConditionInfo[] conditions2 = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", int3, TypeCode.Int64),
				new SqlConditionInfo("@ID_Section", @int, TypeCode.Int32),
				new SqlConditionInfo("@CustomerExamState", int5, TypeCode.Int32)
			};
			if (int2 > 0L && int3 > 0L)
			{
				DataSet customerExamItemDS = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				DataSet customerExamItemDS2 = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions2);
				DataSet dataSet = this.GetSectionFeeItemList(@int.ToString()).Copy();
				dataSet = this.SelectCompareFeeItem(dataSet, customerExamItemDS, customerExamItemDS2);
				dataSet = this.SelectCompareExamItem(dataSet, customerExamItemDS, customerExamItemDS2);
				dataSet = this.SelectCompareSectionSummary(dataSet, customerExamItemDS, customerExamItemDS2);
				string msg = JsonHelperFont.Instance.DataSetToJSON(dataSet);
				this.OutPutMessage(msg);
			}
			else
			{
				this.OutPutMessage("");
			}
		}

		private DataSet GetSectionFeeItemList(string ID_Section)
		{
			string querySqlCode = "QuerySectionFeeItemList_Param";
			string cacheKey = "SectionFeeItemList-" + ID_Section;
			object obj = DataCache.GetCache(cacheKey);
			if (obj == null)
			{
				try
				{
					SqlConditionInfo[] conditions = new SqlConditionInfo[]
					{
						new SqlConditionInfo("@ID_Section", ID_Section, TypeCode.Int32)
					};
					DataSet dataSet = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
					obj = dataSet;
					if (obj != null)
					{
						int configInt = ConfigHelper.GetConfigInt("ModelCache");
						DataCache.SetCache(cacheKey, obj, DateTime.Now.AddMinutes((double)configInt), TimeSpan.Zero);
					}
				}
				catch
				{
				}
			}
			return (DataSet)obj;
		}

		private DataSet SelectCompareFeeItem(DataSet CompareExamItemDS, DataSet CustomerExamItemDS01, DataSet CustomerExamItemDS02)
		{
			foreach (DataRow dataRow in CustomerExamItemDS01.Tables[1].Rows)
			{
				DataRow[] array = CompareExamItemDS.Tables[0].Select(" IsShowCompareInfo = 0 and ID_Fee = " + dataRow["ID_Fee"].ToString());
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow2 = array2[i];
					dataRow2["IsShowCompareInfo"] = 1;
				}
			}
			foreach (DataRow dataRow in CustomerExamItemDS02.Tables[1].Rows)
			{
				DataRow[] array = CompareExamItemDS.Tables[0].Select(" IsShowCompareInfo = 0 and ID_Fee = " + dataRow["ID_Fee"].ToString());
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow2 = array2[i];
					dataRow2["IsShowCompareInfo"] = 1;
				}
			}
			foreach (DataRow dataRow in CompareExamItemDS.Tables[0].Rows)
			{
				DataRow[] array = CompareExamItemDS.Tables[0].Select(" IsShowCompareInfo = 0 ");
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow2 = array2[i];
					dataRow2.Delete();
				}
			}
			CompareExamItemDS.AcceptChanges();
			return CompareExamItemDS;
		}

		private DataSet SelectCompareExamItem(DataSet CompareExamItemDS, DataSet CustomerExamItemDS01, DataSet CustomerExamItemDS02)
		{
			DataTable dataTable = new DataTable("CompareExamItem");
			dataTable.Columns.Add("ID_Section", typeof(string));
			dataTable.Columns.Add("ID_Fee", typeof(string));
			dataTable.Columns.Add("ID_ExamItem", typeof(string));
			dataTable.Columns.Add("ExamItemName", typeof(string));
			dataTable.Columns.Add("ResultLabValues01", typeof(string));
			dataTable.Columns.Add("ResultLabUnit01", typeof(string));
			dataTable.Columns.Add("ResultLabMark01", typeof(string));
			dataTable.Columns.Add("ResultLabRange01", typeof(string));
			dataTable.Columns.Add("ResultSummary01", typeof(string));
			dataTable.Columns.Add("Is_FeeRefund01", typeof(string));
			dataTable.Columns.Add("Is_Examined01", typeof(string));
			dataTable.Columns.Add("ResultLabValues02", typeof(string));
			dataTable.Columns.Add("ResultLabUnit02", typeof(string));
			dataTable.Columns.Add("ResultLabMark02", typeof(string));
			dataTable.Columns.Add("ResultLabRange02", typeof(string));
			dataTable.Columns.Add("ResultSummary02", typeof(string));
			dataTable.Columns.Add("Is_FeeRefund02", typeof(string));
			dataTable.Columns.Add("Is_Examined02", typeof(string));
			dataTable.Columns.Add("IsSameExamSummary", typeof(string));
			foreach (DataRow dataRow in CustomerExamItemDS01.Tables[0].Rows)
			{
				DataRow[] array = dataTable.Select(" ID_ExamItem = " + dataRow["ID_ExamItem"].ToString());
				if (array != null && array.Length > 0)
				{
					DataRow[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						DataRow dataRow2 = array2[i];
						dataRow2["ResultLabValues01"] = dataRow["ResultLabValues"];
						dataRow2["ResultLabUnit01"] = dataRow["ResultLabUnit"];
						dataRow2["ResultLabMark01"] = dataRow["ResultLabMark"];
						dataRow2["ResultLabRange01"] = dataRow["ResultLabRange"];
						dataRow2["ResultSummary01"] = dataRow["ResultSummary"];
						dataRow2["Is_FeeRefund01"] = dataRow["Is_FeeRefund"];
						dataRow2["Is_Examined01"] = dataRow["Is_Examined"];
						if (dataRow2["ResultSummary01"].ToString() == dataRow2["ResultSummary02"].ToString())
						{
							dataRow2["IsSameExamSummary"] = "1";
						}
					}
				}
				else
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["ID_Section"] = dataRow["ID_Section"];
					dataRow2["ID_Fee"] = dataRow["ID_Fee"];
					dataRow2["ID_ExamItem"] = dataRow["ID_ExamItem"];
					dataRow2["ExamItemName"] = dataRow["ExamItemName"];
					dataRow2["ResultLabValues01"] = dataRow["ResultLabValues"];
					dataRow2["ResultLabUnit01"] = dataRow["ResultLabUnit"];
					dataRow2["ResultLabMark01"] = dataRow["ResultLabMark"];
					dataRow2["ResultLabRange01"] = dataRow["ResultLabRange"];
					dataRow2["ResultSummary01"] = dataRow["ResultSummary"];
					dataRow2["Is_FeeRefund01"] = dataRow["Is_FeeRefund"];
					dataRow2["Is_Examined01"] = dataRow["Is_Examined"];
					dataRow2["IsSameExamSummary"] = "0";
					dataTable.Rows.Add(dataRow2);
				}
			}
			foreach (DataRow dataRow in CustomerExamItemDS02.Tables[0].Rows)
			{
				DataRow[] array = dataTable.Select(" ID_ExamItem = " + dataRow["ID_ExamItem"].ToString());
				if (array != null && array.Length > 0)
				{
					DataRow[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						DataRow dataRow2 = array2[i];
						dataRow2["ResultLabValues02"] = dataRow["ResultLabValues"];
						dataRow2["ResultLabUnit02"] = dataRow["ResultLabUnit"];
						dataRow2["ResultLabMark02"] = dataRow["ResultLabMark"];
						dataRow2["ResultLabRange02"] = dataRow["ResultLabRange"];
						dataRow2["ResultSummary02"] = dataRow["ResultSummary"];
						dataRow2["Is_FeeRefund02"] = dataRow["Is_FeeRefund"];
						dataRow2["Is_Examined02"] = dataRow["Is_Examined"];
						if (dataRow2["ResultSummary01"].ToString() == dataRow2["ResultSummary02"].ToString())
						{
							dataRow2["IsSameExamSummary"] = "1";
						}
					}
				}
				else
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["ID_Section"] = dataRow["ID_Section"];
					dataRow2["ID_Fee"] = dataRow["ID_Fee"];
					dataRow2["ID_ExamItem"] = dataRow["ID_ExamItem"];
					dataRow2["ExamItemName"] = dataRow["ExamItemName"];
					dataRow2["ResultLabValues02"] = dataRow["ResultLabValues"];
					dataRow2["ResultLabUnit02"] = dataRow["ResultLabUnit"];
					dataRow2["ResultLabMark02"] = dataRow["ResultLabMark"];
					dataRow2["ResultLabRange02"] = dataRow["ResultLabRange"];
					dataRow2["ResultSummary02"] = dataRow["ResultSummary"];
					dataRow2["Is_FeeRefund02"] = dataRow["Is_FeeRefund"];
					dataRow2["Is_Examined02"] = dataRow["Is_Examined"];
					dataRow2["IsSameExamSummary"] = "0";
					dataTable.Rows.Add(dataRow2);
				}
			}
			dataTable.AcceptChanges();
			CompareExamItemDS.Merge(dataTable);
			return CompareExamItemDS;
		}

		private DataSet SelectCompareSectionSummary(DataSet CompareExamItemDS, DataSet CustomerExamItemDS01, DataSet CustomerExamItemDS02)
		{
			DataTable dataTable = new DataTable("CompareSectionSummary");
			dataTable.Columns.Add("ID_Section", typeof(string));
			dataTable.Columns.Add("SectionName", typeof(string));
			dataTable.Columns.Add("ID_Customer", typeof(string));
			dataTable.Columns.Add("DiseaseLevel01", typeof(string));
			dataTable.Columns.Add("SectionSummary01", typeof(string));
			dataTable.Columns.Add("PositiveSummary01", typeof(string));
			dataTable.Columns.Add("SectionSummaryDate01", typeof(string));
			dataTable.Columns.Add("SummaryDoctorName01", typeof(string));
			dataTable.Columns.Add("IS_giveup01", typeof(string));
			dataTable.Columns.Add("CheckDate01", typeof(string));
			dataTable.Columns.Add("CheckerName01", typeof(string));
			dataTable.Columns.Add("DiseaseLevel02", typeof(string));
			dataTable.Columns.Add("SectionSummary02", typeof(string));
			dataTable.Columns.Add("PositiveSummary02", typeof(string));
			dataTable.Columns.Add("SectionSummaryDate02", typeof(string));
			dataTable.Columns.Add("SummaryDoctorName02", typeof(string));
			dataTable.Columns.Add("IS_giveup02", typeof(string));
			dataTable.Columns.Add("CheckDate02", typeof(string));
			dataTable.Columns.Add("CheckerName02", typeof(string));
			foreach (DataRow dataRow in CustomerExamItemDS01.Tables[2].Rows)
			{
				DataRow[] array = dataTable.Select(" ID_Section = " + dataRow["ID_Section"].ToString());
				if (array != null && array.Length > 0)
				{
					DataRow[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						DataRow dataRow2 = array2[i];
						dataRow2["DiseaseLevel01"] = dataRow["DiseaseLevel"];
						dataRow2["SectionSummary01"] = dataRow["SectionSummary"];
						dataRow2["PositiveSummary01"] = dataRow["PositiveSummary"];
						dataRow2["SectionSummaryDate01"] = dataRow["SectionSummaryDate"];
						dataRow2["SummaryDoctorName01"] = dataRow["SummaryDoctorName"];
						dataRow2["IS_giveup01"] = dataRow["IS_giveup"];
						dataRow2["CheckDate01"] = dataRow["CheckDate"];
						dataRow2["CheckerName01"] = dataRow["CheckerName"];
					}
				}
				else
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["ID_Section"] = dataRow["ID_Section"];
					dataRow2["SectionName"] = dataRow["SectionName"];
					dataRow2["ID_Customer"] = dataRow["ID_Customer"];
					dataRow2["DiseaseLevel01"] = dataRow["DiseaseLevel"];
					dataRow2["SectionSummary01"] = dataRow["SectionSummary"];
					dataRow2["PositiveSummary01"] = dataRow["PositiveSummary"];
					dataRow2["SectionSummaryDate01"] = dataRow["SectionSummaryDate"];
					dataRow2["SummaryDoctorName01"] = dataRow["SummaryDoctorName"];
					dataRow2["IS_giveup01"] = dataRow["IS_giveup"];
					dataRow2["CheckDate01"] = dataRow["CheckDate"];
					dataRow2["CheckerName01"] = dataRow["CheckerName"];
					dataTable.Rows.Add(dataRow2);
				}
			}
			foreach (DataRow dataRow in CustomerExamItemDS02.Tables[2].Rows)
			{
				DataRow[] array = dataTable.Select(" ID_Section = " + dataRow["ID_Section"].ToString());
				if (array != null && array.Length > 0)
				{
					DataRow[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						DataRow dataRow2 = array2[i];
						dataRow2["DiseaseLevel02"] = dataRow["DiseaseLevel"];
						dataRow2["SectionSummary02"] = dataRow["SectionSummary"];
						dataRow2["PositiveSummary02"] = dataRow["PositiveSummary"];
						dataRow2["SectionSummaryDate02"] = dataRow["SectionSummaryDate"];
						dataRow2["SummaryDoctorName02"] = dataRow["SummaryDoctorName"];
						dataRow2["IS_giveup02"] = dataRow["IS_giveup"];
						dataRow2["CheckDate02"] = dataRow["CheckDate"];
						dataRow2["CheckerName02"] = dataRow["CheckerName"];
					}
				}
				else
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["ID_Section"] = dataRow["ID_Section"];
					dataRow2["SectionName"] = dataRow["SectionName"];
					dataRow2["ID_Customer"] = dataRow["ID_Customer"];
					dataRow2["DiseaseLevel02"] = dataRow["DiseaseLevel"];
					dataRow2["SectionSummary02"] = dataRow["SectionSummary"];
					dataRow2["PositiveSummary02"] = dataRow["PositiveSummary"];
					dataRow2["SectionSummaryDate02"] = dataRow["SectionSummaryDate"];
					dataRow2["SummaryDoctorName02"] = dataRow["SummaryDoctorName"];
					dataRow2["IS_giveup02"] = dataRow["IS_giveup"];
					dataRow2["CheckDate02"] = dataRow["CheckDate"];
					dataRow2["CheckerName02"] = dataRow["CheckerName"];
					dataTable.Rows.Add(dataRow2);
				}
			}
			dataTable.AcceptChanges();
			CompareExamItemDS.Merge(dataTable);
			return CompareExamItemDS;
		}
	}
}
