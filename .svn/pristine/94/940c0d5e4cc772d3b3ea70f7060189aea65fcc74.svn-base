using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using PEIS.Model;
using System;
using System.Data;
using System.Reflection;

namespace PEIS.Web.Ajax
{
	public class AjaxCustDiseaseLevel : BasePage
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

		public void QueryCustDiseaseLevelList()
		{
			string @string = base.GetString("CustomerID");
			int @int = base.GetInt("flag", -1);
			int int2 = base.GetInt("IsInformed", -1);
			int int3 = base.GetInt("MinLevel", -1);
			int int4 = base.GetInt("MaxLevel", -1);
			string text = base.Server.HtmlDecode(base.GetString("BeginExamDate"));
			string text2 = base.Server.HtmlDecode(base.GetString("EndExamDate"));
			string text3 = text.Replace("-", string.Empty);
			string text4 = text2.Replace("-", string.Empty);
			if (!string.IsNullOrEmpty(text))
			{
				text += " 00:00:00";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 += " 23:59:59";
			}
			int int5 = base.GetInt("pageIndex", 0);
			int int6 = base.GetInt("pageSize", 10);
			int totalCount = 0;
			int num = 0;
			string pageCode = "QueryCustDiseaseLevelPagesParam";
			SqlConditionInfo[] array = new SqlConditionInfo[8];
			if (!string.IsNullOrEmpty(@string))
			{
				array[0] = new SqlConditionInfo("@ID_Customer", @string, TypeCode.Int32);
				array[0].Place = 3;
			}
			else
			{
				if (int2 >= 0)
				{
					array[1] = new SqlConditionInfo("@Is_DiseaseLevelInformed", int2, TypeCode.Int32);
					array[1].Place = 3;
				}
				if (int3 >= 0)
				{
					array[2] = new SqlConditionInfo("@DiseaseLevel", int3, TypeCode.Int32);
					array[2].ParamOper = ">=";
					array[2].Place = 3;
				}
				if (int4 >= 0)
				{
					array[3] = new SqlConditionInfo("@DiseaseLevel", int4, TypeCode.Int32);
					array[3].ParamOper = "<=";
					array[3].Place = 3;
				}
				if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
				{
					array[4] = new SqlConditionInfo("@OperateDate", text, TypeCode.DateTime);
					array[4].ParamOper = ">=";
					array[4].Place = 3;
				}
				if (!string.IsNullOrEmpty(text2) && Input.IsDate(text2))
				{
					array[5] = new SqlConditionInfo("@OperateDate", text2, TypeCode.DateTime);
					array[5].ParamOper = "<=";
					array[5].Place = 3;
				}
			}
			DataTable page = CommonCustExam.Instance.GetPage(pageCode, int5, int6, out totalCount, out num, array);
			foreach (DataRow dataRow in page.Rows)
			{
				if (dataRow["OperateDate"] != null && !string.IsNullOrEmpty(dataRow["OperateDate"].ToString()))
				{
					dataRow["OperateDate"] = Convert.ToDateTime(dataRow["OperateDate"].ToString()).ToString("yyyy-MM-dd HH:mm");
				}
				if (dataRow["DiseaseLevelInformedDate"] != null && !string.IsNullOrEmpty(dataRow["DiseaseLevelInformedDate"].ToString()))
				{
					dataRow["DiseaseLevelInformedDate"] = Convert.ToDateTime(dataRow["DiseaseLevelInformedDate"].ToString()).ToString("yyyy-MM-dd HH:mm");
				}
				if (dataRow["CheckedDate"] != null && !string.IsNullOrEmpty(dataRow["CheckedDate"].ToString()))
				{
					dataRow["CheckedDate"] = Convert.ToDateTime(dataRow["CheckedDate"].ToString()).ToString("yyyy-MM-dd HH:mm");
				}
			}
			page.AcceptChanges();
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		public void GetCustomerDiseaseLevelExamResultList()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			string querySqlCode = "QueryCust_DiseaseLevelExamResult_Param";
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

		public void GetCustomerDiseaseLevelInform()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			string querySqlCode = "QueryCust_DiseaseLevelInform_Param";
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

		public void SaveCustomerDiseaselLevelInformInfo()
		{
			long @int = base.GetInt64("CustomerID", 0L);
			string text = base.GetString("InformContent");
			text = Input.URLDecode(text);
			PEIS.Model.OnCustPhysicalExamInfo onCustPhysicalExamInfo = new PEIS.Model.OnCustPhysicalExamInfo();
			onCustPhysicalExamInfo.Is_DiseaseLevelInformed = new bool?(true);
			onCustPhysicalExamInfo.DiseaseLevelInformer = this.LoginUserModel.UserName;
			onCustPhysicalExamInfo.ID_DiseaseLevelInformer = new int?(this.LoginUserModel.UserID);
			onCustPhysicalExamInfo.DiseaseLevelInformedDate = new DateTime?(DateTime.Now);
			onCustPhysicalExamInfo.DiseaseLevelInformNote = text;
			onCustPhysicalExamInfo.ID_Customer = @int;
			int num = CommonCustExam.Instance.UpdateDiseaselLevelInform(onCustPhysicalExamInfo);
			if (num > 0)
			{
				Log4J.Instance.Info(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",病症级别通知成功 ,体检号：",
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
					",病症级别通知失败 ,体检号：",
					@int
				}));
			}
			this.OutPutMessage(num.ToString());
		}
	}
}
