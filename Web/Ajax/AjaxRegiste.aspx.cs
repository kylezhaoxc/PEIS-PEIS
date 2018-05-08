using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using PEIS.Model;
using PEIS.Web.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Collections.Generic;

namespace PEIS.Web.Ajax
{
	public class AjaxRegiste : BasePage
	{
		private string SendInterfaceType = SendInterfaceConfig.SendToInterfaceType;

		private DateTime BeginDate;

		private int _defaultCount = 200;

		private string UserID = string.Empty;

		private new string UserName = string.Empty;

		private int Is_Subscribed = -1;

		private int Discount = 10;

		public string ErrorMessage = string.Empty;

		private int _maxRowCount = 5;

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public void TestOutMessage()
		{
			this.OutPutMessage("This is the Test Info ... ");
		}

		public void GetBusSet()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("Forsex");
			text = ((text == string.Empty) ? "1" : text);
			string sql = string.Format("select PEPackageID,PEPackageName,Forsex,InputCode from BusPEPackage WHERE Forsex='{0}'", text);
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonExcuteSql.Instance.ExcuteSql(sql).Tables[0], "dataList");
			this.OutPutMessage(msg);
			Log4J.Instance.Info(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",获取套餐 性别：",
				text,
				",耗时:",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void DelData()
		{
			this.BeginDate = DateTime.Now;
			string itemExamCard = base.GetString("ItemExamCard").TrimEnd(new char[]
			{
				','
			});
			string text = base.GetString("ItemArcCustomer").TrimEnd(new char[]
			{
				','
			});
			int num = CommonOnArcCust.Instance.DelData(itemExamCard, text);
			if (num <= 0)
			{
				string msg = "{\"success\":\"0\",\"Message\":\"删除失败，请重试！\"}";
				this.OutPutMessage(msg);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",删除客户体检信息失败 存档号为：",
					text,
					" ",
					Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
				}));
			}
			else
			{
				string msg = "{\"success\":\"1\",\"Message\":\"删除成功！\"}";
				this.OutPutMessage(msg);
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",成功删除客户体检信息 存档号为：",
					text,
					" ",
					Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
				}));
			}
		}

		public void DeleteCustFee()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("ID_Customer").TrimEnd(new char[]
			{
				','
			});
			string text2 = base.GetString("CustFeeID").TrimEnd(new char[]
			{
				','
			});
			int @int = base.GetInt("Forsex", 2);
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				try
				{
					int num = CommonOnArcCust.Instance.DeleteCustFee(text, text2);
					if (num <= 0)
					{
						string msg = "{\"success\":\"0\",\"Message\":\"删除失败，请重试！\"}";
						this.OutPutMessage(msg);
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",删除收费项目失败 体检号为：",
							text,
							",收费项目:",
							text2,
							" ",
							Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
						}));
					}
					else
					{
						string msg = "{\"success\":\"1\",\"Message\":\"删除成功！\"}";
						this.OutPutMessage(msg);
						CommonOnArcCust.Instance.SumSectionExamInfo(text);
						Log4J.Instance.Info(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",成功删除预约登记 体检号为：",
							text,
							",收费项目:",
							text2,
							" ",
							Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
						}));
					}
				}
				catch (Exception ex)
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",删除收费项目失败 体检号为：",
						text,
						",收费项目:",
						text2,
						" ",
						Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now),
						",错误原因为:",
						ex.Message
					}));
				}
			}
		}

		public void SaveData()
		{
			this.InsertCustomerInfo_NewBusiness();
		}

		public void JustSaveData()
		{
			this.JustSaveCustomerInfo_NewBusiness();
		}

		public void SaveData_New()
		{
			this.InsertCustomerInfo_NewBusiness();
		}

		private int UpdateCustomerInfo(string IDCard, string CustomerName, string Gender, string GenderName, string BirthDay, int Nation, string NationName, string Base64Photo)
		{
			this.BeginDate = DateTime.Now;
			byte[] array = Convert.FromBase64String(Base64Photo);
			MemoryStream memoryStream = new MemoryStream(array);
			PEIS.Model.OnArcCust onArcCust = new PEIS.Model.OnArcCust();
			onArcCust.IDCard = IDCard;
			onArcCust.CustomerName = CustomerName;
			onArcCust.ID_Gender = new int?(int.Parse(Gender));
			onArcCust.GenderName = GenderName;
			onArcCust.BirthDay = new DateTime?(DateTime.Parse(BirthDay));
			onArcCust.NationID = new int?(Nation);
			onArcCust.NationName = NationName;
			onArcCust.Photo = array;
			int num = CommonUser.Instance.AddCustomerArcInfo(onArcCust);
			if (num > 0)
			{
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存客户存档信息 姓名：",
					CustomerName,
					",证件号:",
					IDCard,
					" ",
					Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
				}));
			}
			else
			{
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存客户存档信息失败 姓名：",
					CustomerName,
					",证件号:",
					IDCard,
					" ",
					Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
				}));
			}
			memoryStream.Close();
			memoryStream.Dispose();
			return num;
		}

		private int UpdateCustomerInfo_NewBusiness(string ID_Customer, string IDCard, string CustomerName, string Gender, string GenderName, string BirthDay, int Nation, string NationName, string Base64Photo)
		{
			int num = 0;
			if (!string.IsNullOrEmpty(Base64Photo))
			{
				this.BeginDate = DateTime.Now;
				byte[] array = Convert.FromBase64String(Base64Photo);
				MemoryStream memoryStream = new MemoryStream(array);
				PEIS.Model.OnArcCust onArcCust = new PEIS.Model.OnArcCust();
				onArcCust.IDCard = IDCard;
				onArcCust.CustomerName = CustomerName;
				onArcCust.ID_Gender = new int?(int.Parse(Gender));
				onArcCust.GenderName = GenderName;
				onArcCust.BirthDay = new DateTime?(DateTime.Parse(BirthDay));
				onArcCust.NationID = new int?(Nation);
				onArcCust.NationName = NationName;
				onArcCust.Photo = array;
				string text = array.ToString();
				num = CommonUser.Instance.UpdateCustomerPicInfo(ID_Customer, onArcCust);
				if (num > 0)
				{
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",保存客户存档信息 姓名：",
						CustomerName,
						",证件号:",
						IDCard,
						" ",
						Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
					}));
				}
				else
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",保存客户存档信息失败 姓名：",
						CustomerName,
						",证件号:",
						IDCard,
						" ",
						Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
					}));
				}
				memoryStream.Close();
				memoryStream.Dispose();
			}
			return num;
		}
        public Image Qrcode(string code, string sizeQs, Image logoImg)
        {
            int num;
            if (!int.TryParse(sizeQs, out num))
            {
                num = 160;
            }
            if (logoImg == null)
            {
                return QrCodeHelper.GetQrcode(code, num);
            }
            return QrCodeHelper.GetQrcode(code, num, logoImg, 30, Brushes.White, Brushes.Black);
        }




        private void InsertCustomerInfo()
		{
			this.BeginDate = DateTime.Now;
			string text = string.Empty;
			string text2 = base.GetString("type").ToLower();
			string text3 = base.GetString("modelName").Trim().ToLower();
			string s = base.GetString("ID_ArcCustomer").Trim();
			string @string = base.GetString("CardNum");
			string text4 = base.GetString("CustomerName");
			text4 = Input.URLDecode(text4);
			string string2 = base.GetString("ExamCard");
			string text5 = base.GetString("ID_Customer");
			string text6 = string.Empty;
			string text7 = string.Empty;
			string text8 = string.Empty;
			bool isNew = false;
			if (text3 == "regist")
			{
				text6 = "预约";
				this.Is_Subscribed = 1;
				text7 = "预约失败，请重试！";
				text8 = "预约成功！";
			}
			if (text3 == "sign")
			{
				text6 = "登记";
				this.Is_Subscribed = 0;
				text7 = "登记失败，请重试！";
				text8 = "登记成功！";
			}
			string value = string.Empty;
			if (string.IsNullOrEmpty(text5))
			{
				isNew = true;
				text5 = Customer.CreateMaxNumX(ref value, this.Is_Subscribed);
				if (string.IsNullOrEmpty(text5))
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增",
						text6,
						"体检号不足! 客户姓名：",
						text4,
						",证件号:",
						@string
					}));
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("success");
					dataTable.Columns.Add("Message");
					DataRow dataRow = dataTable.NewRow();
					dataRow["success"] = 0;
					dataRow["Message"] = "对不起,用于" + text6 + "的体检号不足,请您与管理员联系！";
					dataTable.Rows.Add(dataRow);
					string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
					this.OutPutMessage(msg);
					dataTable.Dispose();
					return;
				}
			}
			string string3 = base.GetString("Gender");
			string text9 = base.GetString("GenderName");
			text9 = Input.URLDecode(text9);
			string text10 = base.GetString("BirthDay");
			text10 = Input.URLDecode(text10);
			string string4 = base.GetString("Married");
			string text11 = base.GetString("MarriageName");
			text11 = Input.URLDecode(text11);
			string string5 = base.GetString("MobileNo");
			string text12 = base.GetString("RegisteDate");
			text12 = Input.URLDecode(text12);
			string text13 = base.GetString("Email");
			text13 = Input.URLDecode(text13);
			int @int = base.GetInt("GuideNurse", -1);
			string text14 = base.GetString("GuideNurseName");
			text14 = Input.URLDecode(text14);
			string string6 = base.GetString("FeeWay");
			string text15 = base.GetString("FeeWayName");
			text15 = Input.URLDecode(text15);
			string string7 = base.GetString("ReportWay");
			string text16 = base.GetString("ReportWayName");
			text16 = Input.URLDecode(text16);
			string s2 = base.GetInt("ExamType", -1).ToString();
			string text17 = base.GetString("ExamTypeName");
			text17 = Input.URLDecode(text17);
			string string8 = base.GetString("OperateLevel");
			string s3 = base.GetInt("BusSet", -1).ToString();
			string text18 = base.GetString("BusPEPackageName");
			text18 = Input.URLDecode(text18);
			string text19 = base.GetString("Note");
			text19 = Input.URLDecode(text19);
			string text20 = base.GetString("BusFeeItems");
			text20 = Input.URLDecode(text20);
			string string9 = base.GetString("ExamPlace");
			string text21 = base.GetString("ExamPlaceName");
			text21 = Input.URLDecode(text21);
			int int2 = base.GetInt("Nation", -1);
			string text22 = base.GetString("NationName");
			text22 = Input.URLDecode(text22);
			string string10 = base.GetString("Cultrul");
			string text23 = base.GetString("CultrulName");
			text23 = Input.URLDecode(text23);
			string text24 = base.GetString("SubScribDate");
			text24 = Input.URLDecode(text24);
			string s4 = DateTime.Now.ToString("yyyy-MM-dd");
			string string11 = base.GetString("Base64Photo");
			string[] array = text20.TrimEnd(new char[]
			{
				'|'
			}).Split(new char[]
			{
				'|'
			});
			int num = array.Length;
			if (!string.IsNullOrEmpty(string11))
			{
				this.UpdateCustomerInfo(@string, text4, string3, text9, text10, int2, text22, string11);
			}
			CustomerInfo customerInfo = new CustomerInfo();
			customerInfo.IsNew = isNew;
			PEIS.Model.OnArcCust onArcCust = new PEIS.Model.OnArcCust();
			PEIS.Model.OnCustPhysicalExamInfo onCustPhysicalExamInfo = new PEIS.Model.OnCustPhysicalExamInfo();
			customerInfo.PhotoX = string11;
			customerInfo.Type = text2;
			customerInfo.ModelName = text3;
			if (text2 == "edit")
			{
				onArcCust.ID_ArcCustomer = int.Parse(s);
			}
			onArcCust.IDCard = @string;
			onArcCust.CustomerName = text4;
			onArcCust.ExamCard = string2;
			onArcCust.ID_Gender = new int?(int.Parse(string3));
			onArcCust.GenderName = text9;
			onArcCust.BirthDay = new DateTime?(DateTime.Parse(text10));
			onArcCust.ID_Marriage = new int?(int.Parse(string4));
			onArcCust.MarriageName = text11;
			onArcCust.MobileNo = string5;
			onArcCust.Email = text13;
			onArcCust.NationID = new int?(int2);
			onArcCust.NationName = text22;
			onArcCust.CultrulID = new int?(int.Parse(string10));
			onArcCust.CultrulName = text23;
			customerInfo.OnArcCust = onArcCust;
			onCustPhysicalExamInfo.Is_Subscribed = new int?(this.Is_Subscribed);
			onCustPhysicalExamInfo.ID_Customer = long.Parse(text5);
			onCustPhysicalExamInfo.ID_Operator = new int?(int.Parse(this.UserID));
			onCustPhysicalExamInfo.Operator = this.UserName;
			onCustPhysicalExamInfo.ID_GuideNurse = new int?(@int);
			onCustPhysicalExamInfo.GuideNurse = text14;
			onCustPhysicalExamInfo.ID_FeeWay = new int?(int.Parse(string6));
			onCustPhysicalExamInfo.FeeWayName = text15;
			onCustPhysicalExamInfo.ID_ReportWay = new int?(int.Parse(string7));
			onCustPhysicalExamInfo.ReportWayName = text16;
			onCustPhysicalExamInfo.ID_ExamType = new int?(int.Parse(s2));
			onCustPhysicalExamInfo.ExamTypeName = text17;
			onCustPhysicalExamInfo.SecurityLevel = new int?(int.Parse(string8));
			onCustPhysicalExamInfo.PEPackageID = new int?(int.Parse(s3));
			onCustPhysicalExamInfo.PEPackageName = text18;
			onCustPhysicalExamInfo.Note = text19;
			onCustPhysicalExamInfo.ExamPlaceID = new int?(int.Parse(string9));
			onCustPhysicalExamInfo.ExamPlaceName = text21;
			onCustPhysicalExamInfo.SubScribDate = new DateTime?(DateTime.Parse(text24));
			onCustPhysicalExamInfo.OperateDate = new DateTime?(DateTime.Parse(s4));
			customerInfo.OnCustPhysicalExamInfo = onCustPhysicalExamInfo;
			//List<PEIS.Model.OnCustFee> list = new List<PEIS.Model.OnCustFee>();
            List<PEIS.Model.OnCustFee> ListonCustFees = new List<PEIS.Model.OnCustFee>();
            int iD_CustFee = -1;
			string text25 = string.Empty;
			string a = string.Empty;
			for (int i = 0; i < num; i++)
			{
				string[] array2 = array[i].TrimEnd(new char[]
				{
					'_'
				}).Split(new char[]
				{
					'_'
				});
				if (array2[0] != string.Empty)
				{
					PEIS.Model.OnCustFee onCustFee = new PEIS.Model.OnCustFee();
					onCustFee.Type = array2[array2.Length - 1];
					onCustFee.ID_Customer = new long?(long.Parse(text5));
					onCustFee.ID_Fee = new int?(int.Parse(array2[0]));
					onCustFee.FeeItemName = array2[1];
					onCustFee.ID_Register = new int?(int.Parse(this.UserID));
					onCustFee.RegisterName = this.UserName;
					onCustFee.RegistDate = new DateTime?(DateTime.Parse(text12));
					onCustFee.OriginalPrice = decimal.Parse(array2[2]);
					onCustFee.Discount = decimal.Parse(array2[3]);
					onCustFee.FactPrice = decimal.Parse(array2[4]);
					onCustFee.ID_FeeType = new int?(int.Parse(array2[5]));
					onCustFee.FeetypeName = array2[6];
					onCustFee.ID_Section = array2[8];
					onCustFee.SectionName = array2[9];
					onCustFee.ID_Discounter = new int?(int.Parse(array2[10]));
					onCustFee.DiscounterName = array2[11];
					int.TryParse(array2[13], out iD_CustFee);
					onCustFee.ID_CustFee = iD_CustFee;
					if (array2[array2.Length - 1] == "0")
					{
						text25 = Customer.CreateMaxApplyID();
						while (a == text25)
						{
							text25 = Customer.CreateMaxApplyID();
						}
						onCustFee.ApplyID = text25;
						a = text25;
					}
                    ListonCustFees.Add(onCustFee);
				}
			}
            customerInfo.OnCustFeeList = ListonCustFees;
            int num2 = CommonOnArcCust.Instance.AddCustomerInfo(customerInfo);
			if (num2 <= 0)
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = text7;
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				this.OutPutMessage(msg);
				text = this.GetdateDiff("使用原方法", this.BeginDate, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text7.Replace("，请重试", ""),
					text,
					" 客户姓名：",
					text4,
					",身份证：",
					@string,
					",体检号：",
					text5
				}));
			}
			else
			{
				CommonOnArcCust.Instance.SumSectionExamInfo(text5);
				DataSet customerRelation = CommonOnArcCust.Instance.GetCustomerRelation(text5, true);
				DataTable dataTable2 = customerRelation.Tables[0];
				string value2 = string.Empty;
				string value3 = text5;
				string value4 = string.Empty;
				string value5 = string.Empty;
				string value6 = string.Empty;
				if (dataTable2.Rows.Count > 0)
				{
					value3 = dataTable2.Rows[0]["ID_Customer"].ToString();
					value = dataTable2.Rows[0]["Code128c"].ToString();
					value4 = dataTable2.Rows[0]["ID_ArcCustomer"].ToString();
					value5 = dataTable2.Rows[0]["Is_GuideSheetPrinted"].ToString();
					value6 = dataTable2.Rows[0]["Is_Subscribed"].ToString();
					value2 = dataTable2.Rows[0]["Is_FeeSettled"].ToString();
				}
				DataTable dataTable3 = new DataTable();
				dataTable3.Columns.Add("success");
				dataTable3.Columns.Add("Message");
				dataTable3.Columns.Add("ID_Customer");
				dataTable3.Columns.Add("Code128c");
				dataTable3.Columns.Add("ID_ArcCustomer");
				dataTable3.Columns.Add("Is_GuideSheetPrinted");
				dataTable3.Columns.Add("Is_Subscribed");
				dataTable3.Columns.Add("Is_FeeSettled");
				DataRow dataRow = dataTable3.NewRow();
				dataRow["success"] = 1;
				dataRow["Message"] = text8;
				dataRow["ID_Customer"] = value3;
				dataRow["Code128c"] = value;
				dataRow["ID_ArcCustomer"] = value4;
				dataRow["Is_GuideSheetPrinted"] = value5;
				dataRow["Is_Subscribed"] = value6;
				dataRow["Is_FeeSettled"] = value2;
				dataTable3.Rows.Add(dataRow);
				string msg2 = JsonHelperFont.Instance.DataTableToJSON(dataTable3, "dataList");
				this.OutPutMessage(msg2);
				dataTable3.Dispose();
				dataTable2.Dispose();
				customerRelation.Dispose();
				base.ClearCache_CustRelationCustPEInfo(text5);
				text = this.GetdateDiff("使用原方法", this.BeginDate, DateTime.Now);
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text8,
					text,
					" 客户姓名：",
					text4,
					",身份证：",
					@string,
					",体检号：",
					text5
				}));
			}
		}

		private void InsertCustomerInfo_New()
		{
			this.BeginDate = DateTime.Now;
			string text = string.Empty;
			string text2 = base.GetString("ID_Customer");
			string @string = base.GetString("CardNum");
			string text3 = string.Empty;
			string text4 = base.GetString("CustomerName");
			text4 = Input.URLDecode(text4).Trim();
			string string2 = base.GetString("ExamCard");
			string text5 = base.GetString("SubScribDate");
			text5 = Input.URLDecode(text5);
			DateTime dateTime = DateTime.Parse(text5);
			string a = base.GetString("modelName").Trim().ToLower();
			string text6 = (a == "sign") ? "0" : "1";
			string text7 = string.Empty;
			string text8 = string.Empty;
			string value = string.Empty;
			bool flag = false;
			if (a == "regist")
			{
				text7 = "预约";
				this.Is_Subscribed = 1;
				text3 = "注册失败，请重试！";
				text8 = "注册成功！";
			}
			if (a == "sign")
			{
				text7 = "登记";
				this.Is_Subscribed = 0;
				text3 = "登记失败，请重试！";
				text8 = "登记成功！";
			}
			if (string.IsNullOrEmpty(text2))
			{
				flag = true;
				text2 = Customer.CreateMaxNumX(ref value, this.Is_Subscribed, dateTime, base.GetAllSumRegistCache());
				if (string.IsNullOrEmpty(text2))
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("success");
					dataTable.Columns.Add("Message");
					DataRow dataRow = dataTable.NewRow();
					dataRow["success"] = 0;
					dataRow["Message"] = string.Concat(new string[]
					{
						"对不起,用于",
						text7,
						"体检日期",
						text5,
						"的体检号不足,请您与管理员联系！"
					});
					dataTable.Rows.Add(dataRow);
					string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
					this.OutPutMessage(msg);
					dataTable.Dispose();
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增",
						text7,
						"体检号不足! 客户姓名：",
						text4,
						",证件号:",
						@string
					}));
					return;
				}
			}
			string text9 = base.GetString("type").Trim().ToLower();
			string text10 = base.GetString("ID_ArcCustomer").Trim();
			string text11 = base.GetString("Gender").Trim();
			string text12 = base.GetString("GenderName");
			text12 = Input.URLDecode(text12).Trim();
			string text13 = base.GetString("BirthDay");
			text13 = Input.URLDecode(text13).Trim();
			string text14 = base.GetString("Married").Trim();
			string text15 = base.GetString("MarriageName");
			text15 = Input.URLDecode(text15).Trim();
			string text16 = base.GetString("MobileNo");
			text16 = Secret.AES.EncryptPrefix(text16).Trim();
			string text17 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			text17 = Input.URLDecode(text17).Trim();
			string text18 = base.GetString("Email");
			text18 = Input.URLDecode(text18).Trim();
			string text19 = base.GetInt("GuideNurse", -1).ToString();
			string text20 = base.GetString("GuideNurseName").Trim();
			text20 = Input.URLDecode(text20);
			string text21 = base.GetString("FeeWay").Trim();
			string text22 = base.GetString("FeeWayName");
			text22 = Input.URLDecode(text22).Trim();
			string text23 = base.GetString("ReportWay").Trim();
			string text24 = base.GetString("ReportWayName");
			text24 = Input.URLDecode(text24).Trim();
			string text25 = base.GetInt("ExamType", -1).ToString().Trim();
			string text26 = base.GetString("ExamTypeName").Trim();
			text26 = Input.URLDecode(text26).Trim();
			string text27 = base.GetString("OperateLevel").Trim();
			string text28 = base.GetInt("BusSet", -1).ToString().Trim();
			string text29 = base.GetString("BusPEPackageName");
			text29 = Input.URLDecode(text29).Trim();
			string text30 = base.GetString("Note");
			text30 = Input.URLDecode(text30).Trim();
			string text31 = base.GetString("BusFeeItems");
			text31 = Input.URLDecode(text31).Trim();
			string text32 = base.GetString("ExamPlace").Trim();
			string text33 = base.GetString("ExamPlaceName");
			text33 = Input.URLDecode(text33).Trim();
			int num = base.GetInt("Nation", -1);
			if (num < 1)
			{
				num = -1;
			}
			string text34 = num.ToString().Trim();
			string text35 = base.GetString("NationName");
			text35 = Input.URLDecode(text35).Trim();
			string text36 = base.GetInt("Cultrul", -1).ToString().Trim();
			string text37 = base.GetString("CultrulName");
			text37 = Input.URLDecode(text37).Trim();
			if (text36 == "-1")
			{
				text37 = string.Empty;
			}
			string text38 = DateTime.Now.ToString();
			string string3 = base.GetString("Base64Photo");
			string text39 = base.GetString("Address").Trim();
			string text40 = base.GetInt("Vocation", -1).ToString().Trim();
			string text41 = base.GetString("VocationName").Trim();
			if (text40 == "-1")
			{
				text41 = string.Empty;
			}
			string[] array = text31.TrimEnd(new char[]
			{
				'|'
			}).Split(new char[]
			{
				'|'
			});
			int num2 = array.Length;
			string newValue = string.Empty;
			string newValue2 = string.Empty;
			string text42 = string.Empty;
			List<string> list = new List<string>();
			string text43 = string.Format("if not exists(select CultrulName from OnArcCust where IDCard='{0}' AND CustomerName='{1}')\r\n            BEGIN\r\n                @TempInsertSql\r\n            END\r\n            ELSE \r\n            BEGIN\r\n                @TempUpdateSql\r\n            END;", @string, text4);
			string text44 = string.Format("IF NOT EXISTS(select ID_Customer FROM OnCustPhysicalExamInfo where ID_Customer='{0}')\r\nBEGIN\r\n     @TempInsertSql\r\nEND\r\nELSE \r\nBEGIN\r\n    @TempUpdateSql\r\nEND;", text2);
			string text45 = string.Format("IF NOT EXISTS(select ID_Customer FROM OnCustRelationCustPEInfo WHERE ID_Customer='{0}')\r\nBEGIN\r\n     @TempInsertSql\r\nEND\r\nELSE \r\nBEGIN\r\n    @TempUpdateSql\r\nEND;", text2);
			string sql = string.Format("SELECT ID_Customer,Is_Subscribed,ID_SubScriber,SubScriber,SubScriberOperDate,SubScribDate,ID_Operator,Operator,OperateDate,ISNULL(Is_FeeSettled,0) Is_FeeSettled,ISNULL(Is_GuideSheetPrinted,0) Is_GuideSheetPrinted,ISNULL(Is_Team,0) Is_Team FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}';\r\n                        SELECT ID_Customer,ID_Section FROM OnCustExamSection where ID_Customer='{0}';SELECT ID_CustFee,ID_Customer,ID_Fee,ISNULL(Is_FeeCharged,0)Is_FeeCharged,ISNULL(Is_FeeRefund,0) Is_FeeRefund FROM OnCustFee WHERE ID_Customer='{0}';", text2);
			DataSet dataSet = CommonExcuteSql.Instance.ExcuteSql(sql);
			DataTable dataTable2 = dataSet.Tables[0];
			DataTable dataTable3 = dataSet.Tables[1];
			DataTable dataTable4 = dataSet.Tables[2];
			string value2 = string.Empty;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			bool flag2 = false;
			if (dataTable2.Rows.Count > 0)
			{
				value2 = dataTable2.Rows[0]["ID_Customer"].ToString();
				num4 = (bool.Parse(dataTable2.Rows[0]["Is_GuideSheetPrinted"].ToString()) ? 1 : 0);
				num3 = (bool.Parse(dataTable2.Rows[0]["Is_FeeSettled"].ToString()) ? 1 : 0);
				num5 = (bool.Parse(dataTable2.Rows[0]["Is_Team"].ToString()) ? 1 : 0);
				int arg_7DB_0 = string.IsNullOrEmpty(dataTable2.Rows[0]["Operator"].ToString()) ? 0 : 1;
			}
			string UserID = this.UserID;
			string userName = this.UserName;
			string text46 = DateTime.Now.ToString();
			int num6 = int.Parse(this.GetFeeWay().Select("FeeWayName='统一收费'")[0]["ID_FeeWay"].ToString());
			dataTable2.Dispose();
			if (a == "regist" || a == "sign" || a == "signandregiste")
			{
				if (text34 != "-1" && text36 != "-1")
				{
					newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,ID_Nation,NationName,ID_Cultrul,CultrulName,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',1);", new object[]
					{
						text4,
						@string,
						string2,
						text13,
						text11,
						text12,
						text14,
						text15,
						text16,
						text18,
						0,
						text17,
						text17,
						text34,
						text35,
						text36,
						text37
					});
					if (flag)
					{
						newValue2 = string.Format("update OnArcCust set ExamCard='{13}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',ID_Cultrul='{9}',CultrulName='{10}',BirthDay='{11}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{12}' AND CustomerName='{0}';", new object[]
						{
							text4,
							text11,
							text12,
							text14,
							text15,
							text16,
							text18,
							text34,
							text35,
							text36,
							text37,
							text13,
							@string,
							string2
						});
					}
					else
					{
						newValue2 = string.Format("update OnArcCust set ExamCard='{13}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',ID_Cultrul='{9}',CultrulName='{10}',BirthDay='{11}' where IDCard='{12}' AND CustomerName='{0}';", new object[]
						{
							text4,
							text11,
							text12,
							text14,
							text15,
							text16,
							text18,
							text34,
							text35,
							text36,
							text37,
							text13,
							@string,
							string2
						});
					}
					text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
				}
				if (text34 != "-1" && text36 == "-1")
				{
					newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,ID_Nation,NationName,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',1);", new object[]
					{
						text4,
						@string,
						string2,
						text13,
						text11,
						text12,
						text14,
						text15,
						text16,
						text18,
						0,
						text17,
						text17,
						text34,
						text35
					});
					if (flag)
					{
						newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',BirthDay='{9}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{10}' AND CustomerName='{0}';", new object[]
						{
							text4,
							text11,
							text12,
							text14,
							text15,
							text16,
							text18,
							text34,
							text35,
							text13,
							@string,
							string2
						});
					}
					else
					{
						newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',BirthDay='{9}' where IDCard='{10}' AND CustomerName='{0}';", new object[]
						{
							text4,
							text11,
							text12,
							text14,
							text15,
							text16,
							text18,
							text34,
							text35,
							text13,
							@string,
							string2
						});
					}
					text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
				}
				if (text34 == "-1" && text36 == "-1")
				{
					newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',1);", new object[]
					{
						text4,
						@string,
						string2,
						text13,
						text11,
						text12,
						text14,
						text15,
						text16,
						text18,
						0,
						text17,
						text17
					});
					if (flag)
					{
						newValue2 = string.Format("update OnArcCust set  ExamCard='{9}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',BirthDay='{7}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{8}' AND CustomerName='{0}';", new object[]
						{
							text4,
							text11,
							text12,
							text14,
							text15,
							text16,
							text18,
							text13,
							@string,
							string2
						});
					}
					else
					{
						newValue2 = string.Format("update OnArcCust set  ExamCard='{9}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',BirthDay='{7}' where IDCard='{8}' AND CustomerName='{0}';", new object[]
						{
							text4,
							text11,
							text12,
							text14,
							text15,
							text16,
							text18,
							text13,
							@string,
							string2
						});
					}
					text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
				}
				if (text34 == "-1" && text36 != "-1")
				{
					newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,ID_Cultrul,CultrulName,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',1);", new object[]
					{
						text4,
						@string,
						string2,
						text13,
						text11,
						text12,
						text14,
						text15,
						text16,
						text18,
						0,
						text17,
						text17,
						text36,
						text37
					});
					if (flag)
					{
						newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Cultrul='{7}',CultrulName='{8}',BirthDay='{9}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{10}' AND CustomerName='{0}';", new object[]
						{
							text4,
							text11,
							text12,
							text14,
							text15,
							text16,
							text18,
							text36,
							text37,
							text13,
							@string,
							string2
						});
					}
					else
					{
						newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Cultrul='{7}',CultrulName='{8}',BirthDay='{9}' where IDCard='{10}' AND CustomerName='{0}';", new object[]
						{
							text4,
							text11,
							text12,
							text14,
							text15,
							text16,
							text18,
							text36,
							text37,
							text13,
							@string,
							string2
						});
					}
					text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
				}
				if (text28 != "-1" && text19 != "-1")
				{
					newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team\r\n,ID_Set,SetName,ID_GuideNurse,GuideNurse,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName\r\n,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}'\r\n,'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}';", new object[]
					{
						text2,
						text25,
						0,
						text28,
						text29,
						text19,
						text20,
						text23,
						text24,
						text21,
						text22,
						text27,
						0,
						0,
						0,
						this.Is_Subscribed,
						text30,
						text4,
						text33,
						text32,
						text26,
						this.UserID,
						this.UserName,
						text5,
						text38,
						text11,
						text12,
						text14,
						text15,
						text34,
						text35,
						text36,
						text37,
						text40,
						text41,
						@string,
						string2,
						text13,
						text39,
						text16,
						text18
					});
					newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_Set='{4}',SetName='{5}',ID_GuideNurse='{6}',GuideNurse='{7}',ID_ReportWay='{8}'\r\n,ReportWayName='{9}',ID_FeeWay='{10}',FeeWayName='{11}',SecurityLevel='{12}',Note='{13}',ID_ExamPlace='{14}',SubScribDate='{15}'\r\n,ID_Gender='{17}',GenderName='{18}',ID_Marriage='{19}',MarriageName='{20}',ID_Nation='{21}',NationName='{22}',ID_Cultrul='{23}',CultrulName='{24}',ID_Vocation='{25}',VocationName='{26}',IDCard='{27}',ExamCard='{28}',BirthDay='{29}',Address='{30}',MobileNo='{31}',Email='{32}'\r\nwhere ID_Customer='{16}';", new object[]
					{
						text4,
						text25,
						text26,
						text33,
						text28,
						text29,
						text19,
						text20,
						text23,
						text24,
						text21,
						text22,
						text27,
						text30,
						text32,
						text5,
						text2,
						text11,
						text12,
						text14,
						text15,
						text34,
						text35,
						text36,
						text37,
						text40,
						text41,
						@string,
						string2,
						text13,
						text39,
						text16,
						text18
					});
					text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
				}
				if (text28 != "-1" && text19 == "-1")
				{
					newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team\r\n,ID_Set,SetName,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName\r\n,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}'\r\n,'{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}'\r\n;", new object[]
					{
						text2,
						text25,
						0,
						text28,
						text29,
						text23,
						text24,
						text21,
						text22,
						text27,
						0,
						0,
						0,
						this.Is_Subscribed,
						text30,
						text4,
						text33,
						text32,
						text26,
						this.UserID,
						this.UserName,
						text5,
						text38,
						text11,
						text12,
						text14,
						text15,
						text34,
						text35,
						text36,
						text37,
						text40,
						text41,
						@string,
						string2,
						text13,
						text39,
						text16,
						text18
					});
					newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_Set='{4}',SetName='{5}',ID_ReportWay='{6}'\r\n,ReportWayName='{7}',ID_FeeWay='{8}',FeeWayName='{9}',SecurityLevel='{10}',Note='{11}',ID_ExamPlace='{12}',SubScribDate='{13}'\r\n,ID_Gender='{15}',GenderName='{16}',ID_Marriage='{17}',MarriageName='{18}',ID_Nation='{19}',NationName='{20}',ID_Cultrul='{21}',CultrulName='{22}',ID_Vocation='{23}',VocationName='{24}',IDCard='{25}',ExamCard='{26}',BirthDay='{27}',Address='{28}',MobileNo='{29}',Email='{30}'\r\nwhere ID_Customer='{14}';", new object[]
					{
						text4,
						text25,
						text26,
						text33,
						text28,
						text29,
						text23,
						text24,
						text21,
						text22,
						text27,
						text30,
						text32,
						text5,
						text2,
						text11,
						text12,
						text14,
						text15,
						text34,
						text35,
						text36,
						text37,
						text40,
						text41,
						@string,
						string2,
						text13,
						text39,
						text16,
						text18
					});
					text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
				}
				if (text28 == "-1" && text19 != "-1")
				{
					newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName\r\n,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}'\r\n,'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}';", new object[]
					{
						text2,
						text25,
						0,
						text23,
						text24,
						text21,
						text22,
						text27,
						0,
						0,
						0,
						this.Is_Subscribed,
						text30,
						text4,
						text33,
						text32,
						text26,
						this.UserID,
						this.UserName,
						text5,
						text38,
						text11,
						text12,
						text14,
						text15,
						text34,
						text35,
						text36,
						text37,
						text40,
						text41,
						@string,
						string2,
						text13,
						text39,
						text16,
						text18
					});
					newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_GuideNurse='{4}',GuideNurse='{5}',ID_ReportWay='{6}'\r\n,ReportWayName='{7}',ID_FeeWay='{8}',FeeWayName='{9}',SecurityLevel='{10}',Note='{11}',ID_ExamPlace='{12}',SubScribDate='{13}'\r\n,ID_Gender='{15}',GenderName='{16}',ID_Marriage='{17}',MarriageName='{18}',ID_Nation='{19}',NationName='{20}',ID_Cultrul='{21}',CultrulName='{22}',ID_Vocation='{23}',VocationName='{24}',IDCard='{25}',ExamCard='{26}',BirthDay='{27}',Address='{28}',MobileNo='{29}',Email='{30}'\r\nwhere ID_Customer='{14}';", new object[]
					{
						text4,
						text25,
						text26,
						text33,
						text19,
						text20,
						text23,
						text24,
						text21,
						text22,
						text27,
						text30,
						text32,
						text5,
						text2,
						text11,
						text12,
						text14,
						text15,
						text34,
						text35,
						text36,
						text37,
						text40,
						text41,
						@string,
						string2,
						text13,
						text39,
						text16,
						text18
					});
					text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
				}
				if (text28 == "-1" && text19 == "-1")
				{
					newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}'\r\n,'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}';", new object[]
					{
						text2,
						text25,
						0,
						text23,
						text24,
						text21,
						text22,
						text27,
						0,
						0,
						0,
						this.Is_Subscribed,
						text30,
						text4,
						text33,
						text32,
						text26,
						this.UserID,
						this.UserName,
						text5,
						text38,
						text11,
						text12,
						text14,
						text15,
						text34,
						text35,
						text36,
						text37,
						text40,
						text41,
						@string,
						string2,
						text13,
						text39,
						text16,
						text18
					});
					newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_ReportWay='{4}'\r\n,ReportWayName='{5}',ID_FeeWay='{6}',FeeWayName='{7}',SecurityLevel='{8}',Note='{9}',ID_ExamPlace='{10}',SubScribDate='{11}'\r\n,ID_Gender='{13}',GenderName='{14}',ID_Marriage='{15}',MarriageName='{16}',ID_Nation='{17}',NationName='{18}',ID_Cultrul='{19}',CultrulName='{20}',ID_Vocation='{21}',VocationName='{22}',IDCard='{23}',ExamCard='{24}',BirthDay='{25}',Address='{26}',MobileNo='{27}',Email='{28}'\r\nwhere ID_Customer='{12}';", new object[]
					{
						text4,
						text25,
						text26,
						text33,
						text23,
						text24,
						text21,
						text22,
						text27,
						text30,
						text32,
						text5,
						text2,
						text11,
						text12,
						text14,
						text15,
						text34,
						text35,
						text36,
						text37,
						text40,
						text41,
						@string,
						string2,
						text13,
						text39,
						text16,
						text18
					});
					text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
				}
				if (string.IsNullOrEmpty(value2))
				{
					text42 += string.Format("INSERT INTO OnCustRelationCustPEInfo(ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,Is_CompletePhysical,ExamState)\r\nVALUES((SELECT TOP 1 ID_ArcCustomer FROM OnArcCust WHERE IDCard='{0}' AND CustomerName='{3}' ORDER BY ID_ArcCustomer DESC),'{0}','{1}','{2}',0,0);", new object[]
					{
						@string,
						string2,
						text2,
						text4
					});
				}
				else
				{
					text42 += string.Format("UPDATE OnCustRelationCustPEInfo SET IDCardNo='{0}',ExamCardNo='{1}' WHERE ID_Customer='{2}';", @string, string2, text2);
				}
				int num7 = -1;
				string text47 = string.Empty;
				string text48 = string.Empty;
				string text49 = string.Empty;
				string text50 = string.Empty;
				string text51 = string.Empty;
				string a2 = string.Empty;
				string text52 = string.Empty;
				string text53 = string.Empty;
				string text54 = string.Empty;
				string text55 = string.Empty;
				string a3 = string.Empty;
				for (int i = 0; i < num2; i++)
				{
					string[] array2 = array[i].TrimEnd(new char[]
					{
						'_'
					}).Split(new char[]
					{
						'_'
					});
					if (array2[0] != string.Empty)
					{
						text47 = array2[0].Trim();
						text48 = array2[1].Trim();
						text49 = this.UserID.ToString();
						text50 = this.UserName;
						decimal num8 = decimal.Parse(array2[2].Trim());
						decimal num9 = decimal.Parse(array2[3].Trim());
						decimal num10 = decimal.Parse(array2[4].Trim());
						text51 = array2[5].Trim();
						a2 = array2[6].Trim();
						text52 = array2[8].Trim();
						text53 = array2[9].Trim();
						int num11 = int.Parse(array2[10].Trim());
						text54 = array2[11].Trim();
						int.TryParse(array2[13], out num7);
						if (array2[array2.Length - 1] == "0")
						{
							text55 = Customer.CreateMaxApplyID();
							while (a3 == text55)
							{
								text55 = Customer.CreateMaxApplyID();
							}
							a3 = text55;
						}
						int num12 = 0;
						int num13 = 0;
						if (num5 == 1)
						{
							if (text51 != num6.ToString())
							{
								num13 = 1;
							}
						}
						else if (num4 == 1)
						{
							num13 = 1;
						}
						if (num3 == 0)
						{
							if (num13 == 1)
							{
								num3 = 1;
							}
						}
						if (a2 == "统一收费" || a2 == "记账")
						{
							num12 = 1;
						}
						if (num12 == 1)
						{
							if (num7 > 0)
							{
								text42 += string.Format("UPDATE OnCustFee SET OriginalPrice='{0}',Discount='{1}',FactPrice='{2}',ID_FeeType='{3}',ID_Discounter='{4}',DiscounterName='{5}'\r\n                                                  ,Is_FeeCharged='{6}',ID_FeeCharger='{7}',FeeCharger='{8}',FeeChargeDate='{9}' WHERE ID_CustFee='{10}' AND ISNULL(Is_FeeCharged,0)=0;", new object[]
								{
									num8,
									num9,
									num10,
									text51,
									num11,
									text54,
									num12,
                                    UserID,
									userName,
									text46,
									num7
								});
							}
							else
							{
								flag2 = true;
								DataRow[] array3 = dataTable4.Select(string.Format("ID_Customer='{0}' AND ID_Fee='{1}' AND Is_FeeCharged=0", text2, text47));
								if (array3.Length < 1)
								{
									text42 += string.Format("INSERT INTO OnCustFee(ID_Customer,ID_Fee,FeeItemName,ID_Register,RegisterName,RegistDate\r\n,OriginalPrice,Discount,FactPrice,ID_FeeType,ID_Discounter,DiscounterName,Is_FeeCharged,CustFeeChargeState,Is_Printed,ID_FeeCharger,FeeCharger,FeeChargeDate,ApplyID)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}');", new object[]
									{
										text2,
										text47,
										text48,
										text49,
										text50,
										text17,
										num8,
										num9,
										num10,
										text51,
										num11,
										text54,
										num12,
										num13,
										0,
                                        UserID,
										userName,
										text46,
										text55
									});
								}
							}
						}
						else if (num7 > 0)
						{
							text42 += string.Format("UPDATE OnCustFee SET OriginalPrice='{0}',Discount='{1}',FactPrice='{2}',ID_FeeType='{3}',ID_Discounter='{4}',DiscounterName='{5}'\r\n                                                  ,Is_FeeCharged='{6}',ID_FeeCharger='{7}',FeeCharger='{8}',FeeChargeDate='{9}' WHERE ID_CustFee='{10}' AND ISNULL(Is_FeeCharged,0)=0;", new object[]
							{
								num8,
								num9,
								num10,
								text51,
								num11,
								text54,
								num12,
                                UserID,
								userName,
								text46,
								num7
							});
						}
						else
						{
							DataRow[] array3 = dataTable4.Select(string.Format("ID_Customer='{0}' AND ID_Fee='{1}' AND Is_FeeCharged=0", text2, text47));
							if (array3.Length < 1)
							{
								text42 += string.Format("INSERT INTO OnCustFee(ID_Customer,ID_Fee,FeeItemName,ID_Register,RegisterName,RegistDate\r\n,OriginalPrice,Discount,FactPrice,ID_FeeType,ID_Discounter,DiscounterName,CustFeeChargeState,Is_FeeCharged,Is_Printed,ApplyID)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',0,0,'{13}');", new object[]
								{
									text2,
									text47,
									text48,
									text49,
									text50,
									text17,
									num8.ToString(),
									num9,
									num10.ToString(),
									text51,
									num11,
									text54,
									num13,
									text55
								});
							}
						}
						if (dataTable3.Select(string.Concat(new string[]
						{
							"ID_Customer='",
							text2,
							"' AND ID_Section='",
							text52,
							"'"
						})).Length == 0)
						{
							text42 += string.Format("INSERT INTO OnCustExamSection(ID_Customer,ID_Section,SectionName,CustomerName) values('{0}','{1}','{2}','{3}');", new object[]
							{
								text2,
								text52,
								text53,
								text4
							});
							DataRow dataRow2 = dataTable3.NewRow();
							dataRow2["ID_Customer"] = text2;
							dataRow2["ID_Section"] = text52;
							dataTable3.Rows.Add(dataRow2);
						}
						if (i != 0 && i % this._defaultCount == 0)
						{
							list.Add(text42);
							text42 = string.Empty;
						}
					}
				}
				if (text42 != string.Empty)
				{
					list.Add(text42);
				}
				string item = string.Format("IF NOT EXISTS(select TOP 1 ID_Customer FROM OnCustFee WHERE ID_Customer='{0}' AND Is_FeeCharged!=1)\r\nBEGIN\r\n     UPDATE OnCustPhysicalExamInfo SET Is_FeeSettled=1 WHERE ID_Customer='{0}';\r\nEND\r\nELSE \r\nBEGIN\r\n     UPDATE OnCustPhysicalExamInfo SET Is_FeeSettled=0 WHERE ID_Customer='{0}';\r\nEND;", text2);
				list.Add(item);
			}
			int num14 = CommonExcuteSql.Instance.ExecuteSqlTran(list);
			if (num14 <= 0)
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = text3;
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				this.OutPutMessage(msg);
				text = this.GetdateDiff("使用新方法", this.BeginDate, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text3.Replace("，请重试", ""),
					text,
					"  客户姓名：",
					text4,
					",身份证：",
					@string,
					",体检号：",
					text2
				}));
			}
			else
			{
				this.UpdateCustomerInfo_NewBusiness(text2, @string, text4, text11, text12, text13, int.Parse(text34), text35, string3);
				if (text6 == "1")
				{
					base.AddRegistCache(dateTime, 1, num5);
				}
				else if (text6 == "0")
				{
					base.AddRegistCache(dateTime, 0, num5);
				}
				try
				{
					int num15 = CommonOnArcCust.Instance.SumSectionExamInfo(text2);
				}
				catch (Exception ex)
				{
					text = this.GetdateDiff("SumSectionExamInfo_New", this.BeginDate, DateTime.Now);
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						text,
						" 客户姓名：",
						text4,
						",身份证：",
						@string,
						",体检号：",
						text2,
						",汇总科室收费状态失败,失败原因为:",
						ex.Message
					}));
				}
				DataTable dataTable5 = CommonOnArcCust.Instance.GetCustomerRelation(text2, false).Tables[0];
				string value3 = string.Empty;
				string value4 = text2;
				string value5 = string.Empty;
				string value6 = string.Empty;
				text6 = string.Empty;
				if (dataTable5.Rows.Count > 0)
				{
					value4 = dataTable5.Rows[0]["ID_Customer"].ToString();
					value = dataTable5.Rows[0]["Code128c"].ToString();
					value5 = dataTable5.Rows[0]["ID_ArcCustomer"].ToString();
					value6 = dataTable5.Rows[0]["Is_GuideSheetPrinted"].ToString();
					text6 = dataTable5.Rows[0]["Is_Subscribed"].ToString();
					value3 = dataTable5.Rows[0]["Is_FeeSettled"].ToString();
				}
				DataTable dataTable6 = new DataTable();
				dataTable6.Columns.Add("success");
				dataTable6.Columns.Add("Message");
				dataTable6.Columns.Add("ID_Customer");
				dataTable6.Columns.Add("Code128c");
				dataTable6.Columns.Add("ID_ArcCustomer");
				dataTable6.Columns.Add("Is_GuideSheetPrinted");
				dataTable6.Columns.Add("Is_Subscribed");
				dataTable6.Columns.Add("Is_FeeSettled");
				DataRow dataRow = dataTable6.NewRow();
				dataRow["success"] = 1;
				dataRow["Message"] = text8;
				dataRow["ID_Customer"] = value4;
				dataRow["Code128c"] = value;
				dataRow["ID_ArcCustomer"] = value5;
				dataRow["Is_GuideSheetPrinted"] = value6;
				dataRow["Is_Subscribed"] = text6;
				dataRow["Is_FeeSettled"] = value3;
				dataTable6.Rows.Add(dataRow);
				string msg2 = JsonHelperFont.Instance.DataTableToJSON(dataTable6, "dataList");
				this.OutPutMessage(msg2);
				dataTable6.Dispose();
				dataTable5.Dispose();
				base.ClearCache_CustRelationCustPEInfo(text2);
				text = this.GetdateDiff("使用新方法", this.BeginDate, DateTime.Now);
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text8,
					text,
					" 客户姓名：",
					text4,
					",身份证：",
					@string,
					",体检号：",
					text2
				}));
				if (flag)
				{
					EnumBusBackLogType enumBusBackLogType = EnumBusBackLogType.其它;
					if (text7 == "预约")
					{
						enumBusBackLogType = EnumBusBackLogType.预约;
					}
					else if (text7 == "登记")
					{
						PEIS.Model.OnCustBackLog onCustBackLog = new PEIS.Model.OnCustBackLog();
						enumBusBackLogType = EnumBusBackLogType.登记;
					}
					if (enumBusBackLogType != EnumBusBackLogType.其它)
					{
						base.AddOrUpdateByBackLogType(long.Parse(text2.ToString()), enumBusBackLogType, true, null);
					}
				}
				else
				{
					base.AddOrUpdateByBackLogType(long.Parse(text2.ToString()), EnumBusBackLogType.登记, true, null);
				}
				int num16 = bool.Parse(value3) ? 1 : 0;
				if (num16 == 1)
				{
					if (num16 != num3)
					{
						base.AddOrUpdateByBackLogType(long.Parse(text2.ToString()), EnumBusBackLogType.缴费, true, null);
					}
					else if (flag2)
					{
						base.AddOrUpdateByBackLogType(long.Parse(text2.ToString()), EnumBusBackLogType.缴费, true, null);
					}
				}
			}
			if (dataTable2 != null)
			{
				dataTable2.Dispose();
				dataTable2 = null;
			}
			if (dataTable3 != null)
			{
				dataTable3.Dispose();
				dataTable3 = null;
			}
			if (dataSet != null)
			{
				dataSet.Dispose();
				dataSet = null;
			}
		}

		private bool IsIDCard(string IDCard)
		{
			return IDCard.Length == 15 || IDCard.Length == 18;
		}

		private void JustSaveCustomerInfo_NewBusiness()
		{
			this.BeginDate = DateTime.Now;
			string text = string.Empty;
			string text2 = base.GetString("ID_Customer");
			string @string = base.GetString("CardNum");
			string text3 = string.Empty;
			string text4 = base.GetString("CustomerName");
			text4 = Input.URLDecode(text4).Trim();
			string string2 = base.GetString("ExamCard");
			string text5 = base.GetString("SubScribDate");
			text5 = Input.URLDecode(text5);
			DateTime dateTime = DateTime.Parse(text5);
			string a = base.GetString("modelName").Trim().ToLower();
			string text6 = (a == "sign") ? "0" : "1";
			string text7 = string.Empty;
			string text8 = string.Empty;
			string value = string.Empty;
			bool flag = false;
			if (a == "regist")
			{
				text7 = "预约";
				this.Is_Subscribed = 1;
				text3 = "注册失败，请重试！";
				text8 = "保存成功！";
			}
			if (a == "sign")
			{
				text7 = "登记";
				this.Is_Subscribed = 0;
				text3 = "登记失败，请重试！";
				text8 = "保存成功！";
			}
			string text9 = base.GetString("type").Trim().ToLower();
			string text10 = base.GetString("ID_ArcCustomer").Trim();
			string text11 = base.GetString("Gender").Trim();
			string text12 = base.GetString("GenderName");
			text12 = Input.URLDecode(text12).Trim();
			string text13 = base.GetString("BirthDay");
			text13 = Input.URLDecode(text13).Trim();
			string text14 = base.GetString("Married").Trim();
			string text15 = base.GetString("MarriageName");
			text15 = Input.URLDecode(text15).Trim();
			string text16 = base.GetString("MobileNo");
			text16 = Secret.AES.EncryptPrefix(text16).Trim();
			string text17 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			text17 = Input.URLDecode(text17).Trim();
			string text18 = base.GetString("Email");
			text18 = Input.URLDecode(text18).Trim();
			string text19 = base.GetInt("GuideNurse", -1).ToString();
			string text20 = base.GetString("GuideNurseName").Trim();
			text20 = Input.URLDecode(text20);
			string text21 = base.GetString("FeeWay").Trim();
			string text22 = base.GetString("FeeWayName");
			text22 = Input.URLDecode(text22).Trim();
			string text23 = base.GetString("ReportWay").Trim();
			string text24 = base.GetString("ReportWayName");
			text24 = Input.URLDecode(text24).Trim();
			string text25 = base.GetInt("ExamType", -1).ToString().Trim();
			string text26 = base.GetString("ExamTypeName").Trim();
			text26 = Input.URLDecode(text26).Trim();
			string text27 = base.GetString("OperateLevel").Trim();
			string text28 = base.GetInt("BusSet", -1).ToString().Trim();
			string text29 = base.GetString("BusPEPackageName");
			text29 = Input.URLDecode(text29).Trim();
			string text30 = base.GetString("Note");
			text30 = Input.URLDecode(text30).Trim();
			string text31 = base.GetString("BusFeeItems");
			text31 = Input.URLDecode(text31).Trim();
			string text32 = base.GetString("ExamPlace").Trim();
			string text33 = base.GetString("ExamPlaceName");
			text33 = Input.URLDecode(text33).Trim();
			string text34 = base.GetInt("Nation", -1).ToString().Trim();
			string text35 = base.GetString("NationName");
			text35 = Input.URLDecode(text35).Trim();
			string text36 = base.GetInt("Cultrul", -1).ToString().Trim();
			string text37 = base.GetString("CultrulName");
			text37 = Input.URLDecode(text37).Trim();
			if (text36 == "-1")
			{
				text37 = string.Empty;
			}
			string text38 = DateTime.Now.ToString();
			string string3 = base.GetString("Base64Photo");
			string text39 = base.GetString("Address").Trim();
			string text40 = base.GetInt("Vocation", -1).ToString().Trim();
			string text41 = base.GetString("VocationName").Trim();
			if (text40 == "-1")
			{
				text41 = string.Empty;
			}
			string[] array = text31.TrimEnd(new char[]
			{
				'|'
			}).Split(new char[]
			{
				'|'
			});
			int num = array.Length;
			if (string.IsNullOrEmpty(text4))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户姓名不允许为空！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户姓名为空! 客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else if (string.IsNullOrEmpty(text12))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户性别不允许为空！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户性别不允许为空! 客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else if (string.IsNullOrEmpty(@string))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户证件号不允许为空！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户证件号为空!客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else if (!this.IsIDCard(@string))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户证件号格式不正确！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户证件号格式不正确! 客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else if (string.IsNullOrEmpty(text13))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户出生日期不允许为空！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户出生日期不允许为空! 客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else
			{
				DateTime now = DateTime.Now;
				if (!DateTime.TryParse(text13, out now))
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("success");
					dataTable.Columns.Add("Message");
					DataRow dataRow = dataTable.NewRow();
					dataRow["success"] = 0;
					dataRow["Message"] = "客户出生日期格式不正确！";
					dataTable.Rows.Add(dataRow);
					string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
					this.OutPutMessage(msg);
					dataTable.Dispose();
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增",
						text7,
						"客户出生日期格式不正确! 客户姓名：",
						text4,
						"客户性别:",
						text12,
						"出生日期:",
						text13,
						",婚姻状况:",
						text15,
						",证件号:",
						@string,
						",预定日期：",
						text5
					}));
				}
				else if (string.IsNullOrEmpty(text15))
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("success");
					dataTable.Columns.Add("Message");
					DataRow dataRow = dataTable.NewRow();
					dataRow["success"] = 0;
					dataRow["Message"] = "客户婚姻状况不允许为空！";
					dataTable.Rows.Add(dataRow);
					string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
					this.OutPutMessage(msg);
					dataTable.Dispose();
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增",
						text7,
						"客户婚姻状况不允许为空! 客户姓名：",
						text4,
						"客户性别:",
						text12,
						"出生日期:",
						text13,
						",婚姻状况:",
						text15,
						",证件号:",
						@string,
						",预定日期：",
						text5
					}));
				}
				else if (string.IsNullOrEmpty(text5))
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("success");
					dataTable.Columns.Add("Message");
					DataRow dataRow = dataTable.NewRow();
					dataRow["success"] = 0;
					dataRow["Message"] = "客户预定日期不允许为空！";
					dataTable.Rows.Add(dataRow);
					string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
					this.OutPutMessage(msg);
					dataTable.Dispose();
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增",
						text7,
						"客户预定日期不允许为空! 客户姓名：",
						text4,
						"客户性别:",
						text12,
						"出生日期:",
						text13,
						",婚姻状况:",
						text15,
						",证件号:",
						@string,
						",预定日期：",
						text5
					}));
				}
				else
				{
					DateTime now2 = DateTime.Now;
					if (!DateTime.TryParse(text5, out now2))
					{
						DataTable dataTable = new DataTable();
						dataTable.Columns.Add("success");
						dataTable.Columns.Add("Message");
						DataRow dataRow = dataTable.NewRow();
						dataRow["success"] = 0;
						dataRow["Message"] = "客户预定日期格式不正确！";
						dataTable.Rows.Add(dataRow);
						string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
						this.OutPutMessage(msg);
						dataTable.Dispose();
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",新增",
							text7,
							"客户预定日期格式不正确! 客户姓名：",
							text4,
							"客户性别:",
							text12,
							"出生日期:",
							text13,
							",婚姻状况:",
							text15,
							",证件号:",
							@string,
							",预定日期：",
							text5
						}));
					}
					else
					{
						if (string.IsNullOrEmpty(text2))
						{
							flag = true;
							text2 = Customer.CreateMaxNumX(ref value, this.Is_Subscribed, dateTime, base.GetAllSumRegistCache());
							if (string.IsNullOrEmpty(text2))
							{
								DataTable dataTable = new DataTable();
								dataTable.Columns.Add("success");
								dataTable.Columns.Add("Message");
								DataRow dataRow = dataTable.NewRow();
								dataRow["success"] = 0;
								dataRow["Message"] = string.Concat(new string[]
								{
									"对不起,用于",
									text7,
									"体检日期",
									text5,
									"的体检号不足,请您与管理员联系！"
								});
								dataTable.Rows.Add(dataRow);
								string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
								this.OutPutMessage(msg);
								dataTable.Dispose();
								Log4J.Instance.Error(string.Concat(new string[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",新增",
									text7,
									"体检号不足! 客户姓名：",
									text4,
									"客户性别:",
									text12,
									",证件号:",
									@string
								}));
								return;
							}
						}
						string newValue = string.Empty;
						string newValue2 = string.Empty;
						string text42 = string.Empty;
						List<string> list = new List<string>();
						string text43 = string.Format("if not exists(select CultrulName from OnArcCust where IDCard='{0}' AND CustomerName='{1}')\r\n            BEGIN\r\n                @TempInsertSql\r\n            END\r\n            ELSE \r\n            BEGIN\r\n                @TempUpdateSql\r\n            END;", @string, text4);
						string text44 = string.Format("IF NOT EXISTS(select ID_Customer FROM OnCustPhysicalExamInfo where ID_Customer='{0}')\r\nBEGIN\r\n     @TempInsertSql\r\nEND\r\nELSE \r\nBEGIN\r\n    @TempUpdateSql\r\nEND;", text2);
						string text45 = string.Format("IF NOT EXISTS(select ID_Customer FROM OnCustRelationCustPEInfo WHERE ID_Customer='{0}')\r\nBEGIN\r\n     @TempInsertSql\r\nEND\r\nELSE \r\nBEGIN\r\n    @TempUpdateSql\r\nEND;", text2);
						string sql = string.Format("SELECT ID_Customer,Is_Subscribed,ID_SubScriber,SubScriber,SubScriberOperDate,SubScribDate,ID_Operator,Operator,OperateDate,ISNULL(Is_FeeSettled,0) Is_FeeSettled,ISNULL(Is_GuideSheetPrinted,0) Is_GuideSheetPrinted,ISNULL(Is_Team,0) Is_Team FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}';\r\n                        SELECT ID_Customer,ID_Section FROM OnCustExamSection where ID_Customer='{0}';SELECT ID_CustFee,ID_Customer,ID_Fee,ISNULL(Is_FeeCharged,0)Is_FeeCharged,ISNULL(Is_FeeRefund,0) Is_FeeRefund FROM OnCustFee WHERE ID_Customer='{0}';", text2);
						DataSet dataSet = CommonExcuteSql.Instance.ExcuteSql(sql);
						DataTable dataTable2 = dataSet.Tables[0];
						DataTable dataTable3 = dataSet.Tables[1];
						DataTable dataTable4 = dataSet.Tables[2];
						string value2 = string.Empty;
						int num2 = 0;
						int num3 = 0;
						int num4 = 0;
						if (dataTable2.Rows.Count > 0)
						{
							value2 = dataTable2.Rows[0]["ID_Customer"].ToString();
							num3 = (bool.Parse(dataTable2.Rows[0]["Is_GuideSheetPrinted"].ToString()) ? 1 : 0);
							num2 = (bool.Parse(dataTable2.Rows[0]["Is_FeeSettled"].ToString()) ? 1 : 0);
							num4 = (bool.Parse(dataTable2.Rows[0]["Is_Team"].ToString()) ? 1 : 0);
							int arg_13D7_0 = string.IsNullOrEmpty(dataTable2.Rows[0]["Operator"].ToString()) ? 0 : 1;
						}
						string iD_User = this.UserID;
						string userName = this.UserName;
						string text46 = DateTime.Now.ToString();
						int num5 = int.Parse(this.GetFeeWay().Select("FeeWayName='统一收费'")[0]["FeeWayID"].ToString());
						dataTable2.Dispose();
						if (a == "regist" || a == "sign" || a == "signandregiste")
						{
							if (text34 != "-1" && text36 != "-1")
							{
								newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,ID_Nation,NationName,ID_Cultrul,CultrulName,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',1);", new object[]
								{
									text4,
									@string,
									string2,
									text13,
									text11,
									text12,
									text14,
									text15,
									text16,
									text18,
									0,
									text17,
									text17,
									text34,
									text35,
									text36,
									text37
								});
								if (flag)
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{13}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',ID_Cultrul='{9}',CultrulName='{10}',BirthDay='{11}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{12}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text34,
										text35,
										text36,
										text37,
										text13,
										@string,
										string2
									});
								}
								else
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{13}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',ID_Cultrul='{9}',CultrulName='{10}',BirthDay='{11}' where IDCard='{12}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text34,
										text35,
										text36,
										text37,
										text13,
										@string,
										string2
									});
								}
								text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text34 != "-1" && text36 == "-1")
							{
								newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,ID_Nation,NationName,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',1);", new object[]
								{
									text4,
									@string,
									string2,
									text13,
									text11,
									text12,
									text14,
									text15,
									text16,
									text18,
									0,
									text17,
									text17,
									text34,
									text35
								});
								if (flag)
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',BirthDay='{9}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{10}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text34,
										text35,
										text13,
										@string,
										string2
									});
								}
								else
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',BirthDay='{9}' where IDCard='{10}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text34,
										text35,
										text13,
										@string,
										string2
									});
								}
								text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text34 == "-1" && text36 == "-1")
							{
								newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',1);", new object[]
								{
									text4,
									@string,
									string2,
									text13,
									text11,
									text12,
									text14,
									text15,
									text16,
									text18,
									0,
									text17,
									text17
								});
								if (flag)
								{
									newValue2 = string.Format("update OnArcCust set  ExamCard='{9}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',BirthDay='{7}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{8}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text13,
										@string,
										string2
									});
								}
								else
								{
									newValue2 = string.Format("update OnArcCust set  ExamCard='{9}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',BirthDay='{7}' where IDCard='{8}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text13,
										@string,
										string2
									});
								}
								text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text34 == "-1" && text36 != "-1")
							{
								newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,ID_Cultrul,CultrulName,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',1);", new object[]
								{
									text4,
									@string,
									string2,
									text13,
									text11,
									text12,
									text14,
									text15,
									text16,
									text18,
									0,
									text17,
									text17,
									text36,
									text37
								});
								if (flag)
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Cultrul='{7}',CultrulName='{8}',BirthDay='{9}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{10}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text36,
										text37,
										text13,
										@string,
										string2
									});
								}
								else
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Cultrul='{7}',CultrulName='{8}',BirthDay='{9}' where IDCard='{10}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text36,
										text37,
										text13,
										@string,
										string2
									});
								}
								text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text28 != "-1" && text19 != "-1")
							{
								newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team\r\n,ID_Set,SetName,ID_GuideNurse,GuideNurse,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName\r\n,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}'\r\n,'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}';", new object[]
								{
									text2,
									text25,
									0,
									text28,
									text29,
									text19,
									text20,
									text23,
									text24,
									text21,
									text22,
									text27,
									0,
									0,
									0,
									this.Is_Subscribed,
									text30,
									text4,
									text33,
									text32,
									text26,
									this.UserID,
									this.UserName,
									text5,
									text38,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_Set='{4}',SetName='{5}',ID_GuideNurse='{6}',GuideNurse='{7}',ID_ReportWay='{8}'\r\n,ReportWayName='{9}',ID_FeeWay='{10}',FeeWayName='{11}',SecurityLevel='{12}',Note='{13}',ID_ExamPlace='{14}',SubScribDate='{15}'\r\n,ID_Gender='{17}',GenderName='{18}',ID_Marriage='{19}',MarriageName='{20}',ID_Nation='{21}',NationName='{22}',ID_Cultrul='{23}',CultrulName='{24}',ID_Vocation='{25}',VocationName='{26}',IDCard='{27}',ExamCard='{28}',BirthDay='{29}',Address='{30}',MobileNo='{31}',Email='{32}'\r\nwhere ID_Customer='{16}';", new object[]
								{
									text4,
									text25,
									text26,
									text33,
									text28,
									text29,
									text19,
									text20,
									text23,
									text24,
									text21,
									text22,
									text27,
									text30,
									text32,
									text5,
									text2,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text28 != "-1" && text19 == "-1")
							{
								newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team\r\n,ID_Set,SetName,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName\r\n,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}'\r\n,'{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}'\r\n;", new object[]
								{
									text2,
									text25,
									0,
									text28,
									text29,
									text23,
									text24,
									text21,
									text22,
									text27,
									0,
									0,
									0,
									this.Is_Subscribed,
									text30,
									text4,
									text33,
									text32,
									text26,
									this.UserID,
									this.UserName,
									text5,
									text38,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_Set='{4}',SetName='{5}',ID_ReportWay='{6}'\r\n,ReportWayName='{7}',ID_FeeWay='{8}',FeeWayName='{9}',SecurityLevel='{10}',Note='{11}',ID_ExamPlace='{12}',SubScribDate='{13}'\r\n,ID_Gender='{15}',GenderName='{16}',ID_Marriage='{17}',MarriageName='{18}',ID_Nation='{19}',NationName='{20}',ID_Cultrul='{21}',CultrulName='{22}',ID_Vocation='{23}',VocationName='{24}',IDCard='{25}',ExamCard='{26}',BirthDay='{27}',Address='{28}',MobileNo='{29}',Email='{30}'\r\nwhere ID_Customer='{14}';", new object[]
								{
									text4,
									text25,
									text26,
									text33,
									text28,
									text29,
									text23,
									text24,
									text21,
									text22,
									text27,
									text30,
									text32,
									text5,
									text2,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text28 == "-1" && text19 != "-1")
							{
								newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName\r\n,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}'\r\n,'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}';", new object[]
								{
									text2,
									text25,
									0,
									text23,
									text24,
									text21,
									text22,
									text27,
									0,
									0,
									0,
									this.Is_Subscribed,
									text30,
									text4,
									text33,
									text32,
									text26,
									this.UserID,
									this.UserName,
									text5,
									text38,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_GuideNurse='{4}',GuideNurse='{5}',ID_ReportWay='{6}'\r\n,ReportWayName='{7}',ID_FeeWay='{8}',FeeWayName='{9}',SecurityLevel='{10}',Note='{11}',ID_ExamPlace='{12}',SubScribDate='{13}'\r\n,ID_Gender='{15}',GenderName='{16}',ID_Marriage='{17}',MarriageName='{18}',ID_Nation='{19}',NationName='{20}',ID_Cultrul='{21}',CultrulName='{22}',ID_Vocation='{23}',VocationName='{24}',IDCard='{25}',ExamCard='{26}',BirthDay='{27}',Address='{28}',MobileNo='{29}',Email='{30}'\r\nwhere ID_Customer='{14}';", new object[]
								{
									text4,
									text25,
									text26,
									text33,
									text19,
									text20,
									text23,
									text24,
									text21,
									text22,
									text27,
									text30,
									text32,
									text5,
									text2,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text28 == "-1" && text19 == "-1")
							{
								newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}'\r\n,'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}';", new object[]
								{
									text2,
									text25,
									0,
									text23,
									text24,
									text21,
									text22,
									text27,
									0,
									0,
									0,
									this.Is_Subscribed,
									text30,
									text4,
									text33,
									text32,
									text26,
									this.UserID,
									this.UserName,
									text5,
									text38,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_ReportWay='{4}'\r\n,ReportWayName='{5}',ID_FeeWay='{6}',FeeWayName='{7}',SecurityLevel='{8}',Note='{9}',ID_ExamPlace='{10}',SubScribDate='{11}'\r\n,ID_Gender='{13}',GenderName='{14}',ID_Marriage='{15}',MarriageName='{16}',ID_Nation='{17}',NationName='{18}',ID_Cultrul='{19}',CultrulName='{20}',ID_Vocation='{21}',VocationName='{22}',IDCard='{23}',ExamCard='{24}',BirthDay='{25}',Address='{26}',MobileNo='{27}',Email='{28}'\r\nwhere ID_Customer='{12}';", new object[]
								{
									text4,
									text25,
									text26,
									text33,
									text23,
									text24,
									text21,
									text22,
									text27,
									text30,
									text32,
									text5,
									text2,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (string.IsNullOrEmpty(value2))
							{
								text42 += string.Format("INSERT INTO OnCustRelationCustPEInfo(ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,Is_CompletePhysical,ExamState)\r\nVALUES((SELECT TOP 1 ID_ArcCustomer FROM OnArcCust WHERE IDCard='{0}' AND CustomerName='{3}' ORDER BY ID_ArcCustomer DESC),'{0}','{1}','{2}',0,0);", new object[]
								{
									@string,
									string2,
									text2,
									text4
								});
							}
							else
							{
								text42 += string.Format("UPDATE OnCustRelationCustPEInfo SET IDCardNo='{0}',ExamCardNo='{1}' WHERE ID_Customer='{2}';", @string, string2, text2);
							}
							int num6 = -1;
							string text47 = string.Empty;
							string text48 = string.Empty;
							string text49 = string.Empty;
							string text50 = string.Empty;
							string text51 = string.Empty;
							string text52 = string.Empty;
							string text53 = string.Empty;
							string text54 = string.Empty;
							string text55 = string.Empty;
							string text56 = string.Empty;
							string a2 = string.Empty;
							for (int i = 0; i < num; i++)
							{
								string[] array2 = array[i].TrimEnd(new char[]
								{
									'_'
								}).Split(new char[]
								{
									'_'
								});
								if (array2[0] != string.Empty)
								{
									text47 = array2[0].Trim();
									text48 = array2[1].Trim();
									text49 = this.UserID.ToString();
									text50 = this.UserName;
									decimal num7 = decimal.Parse(array2[2].Trim());
									decimal num8 = decimal.Parse(array2[3].Trim());
									decimal num9 = decimal.Parse(array2[4].Trim());
									text51 = array2[5].Trim();
									text52 = array2[6].Trim();
									text53 = array2[8].Trim();
									text54 = array2[9].Trim();
									int num10 = int.Parse(array2[10].Trim());
									text55 = array2[11].Trim();
									int.TryParse(array2[13], out num6);
									if (array2[array2.Length - 1] == "0")
									{
										text56 = Customer.CreateMaxApplyID();
										while (a2 == text56)
										{
											text56 = Customer.CreateMaxApplyID();
										}
										a2 = text56;
									}
									int num11 = 0;
									int num12 = 0;
									if (num4 == 1)
									{
										if (text51 != num5.ToString())
										{
											num12 = 1;
										}
									}
									else if (num3 == 1)
									{
										num12 = 1;
									}
									if (num2 == 0)
									{
										if (num12 == 1)
										{
											num2 = 1;
										}
									}
									if (num11 == 1)
									{
										if (num6 > 0)
										{
											text42 += string.Format("UPDATE OnCustFee SET OriginalPrice='{0}',Discount='{1}',FactPrice='{2}',ID_FeeType='{3}',ID_Discounter='{4}',DiscounterName='{5}' ,Is_FeeCharged='{6}',ID_FeeCharger='{7}',FeeCharger='{8}',FeeChargeDate='{9}' WHERE ID_CustFee='{10}' AND ISNULL(Is_FeeCharged,0)=0;", new object[]
											{
												num7,
												num8,
												num9,
												text51,
												num10,
												text55,
												num11,
												iD_User,
												userName,
												text46,
												num6
											});
										}
										else
										{
											DataRow[] array3 = dataTable4.Select(string.Format("ID_Customer='{0}' AND ID_Fee='{1}' AND Is_FeeCharged=0", text2, text47));
											if (array3.Length < 1)
											{
												text42 += string.Format("INSERT INTO OnCustFee(ID_Customer,ID_Fee,FeeItemName,ID_Register,RegisterName,RegistDate\r\n,OriginalPrice,Discount,FactPrice,ID_FeeType,ID_Discounter,DiscounterName,Is_FeeCharged,CustFeeChargeState,Is_Printed,ID_FeeCharger,FeeCharger,FeeChargeDate,ApplyID)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}');", new object[]
												{
													text2,
													text47,
													text48,
													text49,
													text50,
													text17,
													num7,
													num8,
													num9,
													text51,
													num10,
													text55,
													num11,
													num12,
													0,
													iD_User,
													userName,
													text46,
													text56
												});
											}
										}
									}
									else if (num6 > 0)
									{
										text42 += string.Format("UPDATE OnCustFee SET OriginalPrice='{0}',Discount='{1}',FactPrice='{2}',ID_FeeType='{3}',ID_Discounter='{4}',DiscounterName='{5}' WHERE ID_CustFee='{6}' AND ISNULL(Is_FeeCharged,0)=0;", new object[]
										{
											num7,
											num8,
											num9,
											text51,
											num10,
											text55,
											num6
										});
									}
									else
									{
										DataRow[] array3 = dataTable4.Select(string.Format("ID_Customer='{0}' AND ID_Fee='{1}' AND Is_FeeCharged=0", text2, text47));
										if (array3.Length < 1)
										{
											text42 += string.Format("INSERT INTO OnCustFee(ID_Customer,ID_Fee,FeeItemName,ID_Register,RegisterName,RegistDate,OriginalPrice,Discount,FactPrice,ID_FeeType,ID_Discounter,DiscounterName,CustFeeChargeState,Is_FeeCharged,Is_Printed,ApplyID)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',0,0,'{13}');", new object[]
											{
												text2,
												text47,
												text48,
												text49,
												text50,
												text17,
												num7.ToString(),
												num8,
												num9.ToString(),
												text51,
												num10,
												text55,
												num12,
												text56
											});
										}
									}
									if (dataTable3.Select(string.Concat(new string[]
									{
										"ID_Customer='",
										text2,
										"' AND ID_Section='",
										text53,
										"'"
									})).Length == 0)
									{
										text42 += string.Format("INSERT INTO OnCustExamSection(ID_Customer,ID_Section,SectionName,CustomerName) values('{0}','{1}','{2}','{3}');", new object[]
										{
											text2,
											text53,
											text54,
											text4
										});
										DataRow dataRow2 = dataTable3.NewRow();
										dataRow2["ID_Customer"] = text2;
										dataRow2["ID_Section"] = text53;
										dataTable3.Rows.Add(dataRow2);
									}
									if (i != 0 && i % this._defaultCount == 0)
									{
										list.Add(text42);
										text42 = string.Empty;
									}
								}
							}
							if (text42 != string.Empty)
							{
								list.Add(text42);
							}
							string item = string.Format("IF NOT EXISTS(select TOP 1 ID_Customer FROM OnCustFee WHERE ID_Customer='{0}' AND Is_FeeCharged!=1)\r\nBEGIN\r\n     UPDATE OnCustPhysicalExamInfo SET Is_FeeSettled=1 WHERE ID_Customer='{0}';\r\nEND\r\nELSE \r\nBEGIN\r\n     UPDATE OnCustPhysicalExamInfo SET Is_FeeSettled=0 WHERE ID_Customer='{0}';\r\nEND;", text2);
							list.Add(item);
						}
						int num13 = CommonExcuteSql.Instance.ExecuteSqlTran(list);
						if (num13 <= 0)
						{
							DataTable dataTable = new DataTable();
							dataTable.Columns.Add("success");
							dataTable.Columns.Add("Message");
							DataRow dataRow = dataTable.NewRow();
							dataRow["success"] = 0;
							dataRow["Message"] = text3;
							dataTable.Rows.Add(dataRow);
							string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
							this.OutPutMessage(msg);
							dataTable.Dispose();
							this.OutPutMessage(msg);
							text = this.GetdateDiff("JustSaveCustomerInfo_NewBusiness", this.BeginDate, DateTime.Now);
							Log4J.Instance.Error(string.Concat(new string[]
							{
								Public.GetClientIP(),
								",",
								this.LoginUserModel.UserName,
								",",
								text3.Replace("，请重试", ""),
								text,
								"  客户姓名：",
								text4,
								",身份证：",
								@string,
								",体检号：",
								text2
							}));
						}
						else
						{
							this.UpdateCustomerInfo_NewBusiness(text2, @string, text4, text11, text12, text13, int.Parse(text34), text35, string3);
							if (text6 == "1")
							{
								base.AddRegistCache(dateTime, 1, num4);
							}
							else if (text6 == "0")
							{
								base.AddRegistCache(dateTime, 0, num4);
							}
							try
							{
								int num14 = CommonOnArcCust.Instance.SumSectionExamInfo(text2);
							}
							catch (Exception ex)
							{
								text = this.GetdateDiff("SumSectionExamInfo", this.BeginDate, DateTime.Now);
								Log4J.Instance.Info(string.Concat(new string[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									text,
									" 客户姓名：",
									text4,
									",身份证：",
									@string,
									",体检号：",
									text2,
									",汇总科室收费状态失败,失败原因为:",
									ex.Message
								}));
							}
							DataTable dataTable5 = CommonOnArcCust.Instance.GetCustomerRelation(text2, false).Tables[0];
							string value3 = string.Empty;
							string value4 = text2;
							string value5 = string.Empty;
							string value6 = string.Empty;
							text6 = string.Empty;
							if (dataTable5.Rows.Count > 0)
							{
								value4 = dataTable5.Rows[0]["ID_Customer"].ToString();
								value = dataTable5.Rows[0]["Code128c"].ToString();
								value5 = dataTable5.Rows[0]["ID_ArcCustomer"].ToString();
								value6 = dataTable5.Rows[0]["Is_GuideSheetPrinted"].ToString();
								text6 = dataTable5.Rows[0]["Is_Subscribed"].ToString();
								value3 = dataTable5.Rows[0]["Is_FeeSettled"].ToString();
							}
							DataTable dataTable6 = new DataTable();
							dataTable6.Columns.Add("success");
							dataTable6.Columns.Add("Message");
							dataTable6.Columns.Add("ID_Customer");
							dataTable6.Columns.Add("Code128c");
							dataTable6.Columns.Add("ID_ArcCustomer");
							dataTable6.Columns.Add("Is_GuideSheetPrinted");
							dataTable6.Columns.Add("Is_Subscribed");
							dataTable6.Columns.Add("Is_FeeSettled");
							DataRow dataRow = dataTable6.NewRow();
							dataRow["success"] = 1;
							dataRow["Message"] = text8;
							dataRow["ID_Customer"] = value4;
							dataRow["Code128c"] = value;
							dataRow["ID_ArcCustomer"] = value5;
							dataRow["Is_GuideSheetPrinted"] = value6;
							dataRow["Is_Subscribed"] = text6;
							dataRow["Is_FeeSettled"] = value3;
							dataTable6.Rows.Add(dataRow);
							string msg2 = JsonHelperFont.Instance.DataTableToJSON(dataTable6, "dataList");
							this.OutPutMessage(msg2);
							dataTable6.Dispose();
							dataTable5.Dispose();
							base.ClearCache_CustRelationCustPEInfo(text2);
							text = this.GetdateDiff("JustSaveCustomerInfo_NewBusiness", this.BeginDate, DateTime.Now);
							Log4J.Instance.Info(string.Concat(new string[]
							{
								Public.GetClientIP(),
								",",
								this.LoginUserModel.UserName,
								",",
								text8,
								text,
								" 客户姓名：",
								text4,
								",身份证：",
								@string,
								",体检号：",
								text2
							}));
						}
						if (dataTable2 != null)
						{
							dataTable2.Dispose();
							dataTable2 = null;
						}
						if (dataTable3 != null)
						{
							dataTable3.Dispose();
							dataTable3 = null;
						}
						if (dataSet != null)
						{
							dataSet.Dispose();
							dataSet = null;
						}
					}
				}
			}
		}

		private void InsertCustomerInfo_NewBusiness()
		{
			this.BeginDate = DateTime.Now;
			string text = string.Empty;
			string text2 = base.GetString("ID_Customer");
			string @string = base.GetString("CardNum");
			string text3 = string.Empty;
			string text4 = base.GetString("CustomerName");
			text4 = Input.URLDecode(text4).Trim();
			string string2 = base.GetString("ExamCard");
			string text5 = base.GetString("SubScribDate");
			text5 = Input.URLDecode(text5);
			DateTime dateTime = DateTime.Parse(text5);
			string a = base.GetString("modelName").Trim().ToLower();
			string text6 = (a == "sign") ? "0" : "1";
			string text7 = string.Empty;
			string text8 = string.Empty;
			string value = string.Empty;
			bool flag = false;
			if (a == "regist")
			{
				text7 = "预约";
				this.Is_Subscribed = 1;
				text3 = "注册失败，请重试！";
				text8 = "注册成功！";
			}
			if (a == "sign")
			{
				text7 = "登记";
				this.Is_Subscribed = 0;
				text3 = "登记失败，请重试！";
				text8 = "登记成功！";
			}
			string text9 = base.GetString("type").Trim().ToLower();
			string text10 = base.GetString("ID_ArcCustomer").Trim();
			string text11 = base.GetString("Gender").Trim();
			string text12 = base.GetString("GenderName");
			text12 = Input.URLDecode(text12).Trim();
			string text13 = base.GetString("BirthDay");
			text13 = Input.URLDecode(text13).Trim();
			string text14 = base.GetString("Married").Trim();
			string text15 = base.GetString("MarriageName");
			text15 = Input.URLDecode(text15).Trim();
			string text16 = base.GetString("MobileNo");
			text16 = Secret.AES.EncryptPrefix(text16).Trim();
			string text17 = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
			text17 = Input.URLDecode(text17).Trim();
			string text18 = base.GetString("Email");
			text18 = Input.URLDecode(text18).Trim();
			string text19 = base.GetInt("GuideNurse", -1).ToString();
			string text20 = base.GetString("GuideNurseName").Trim();
			text20 = Input.URLDecode(text20);
			string text21 = base.GetString("FeeWay").Trim();
			string text22 = base.GetString("FeeWayName");
			text22 = Input.URLDecode(text22).Trim();
			string text23 = base.GetString("ReportWay").Trim();
			string text24 = base.GetString("ReportWayName");
			text24 = Input.URLDecode(text24).Trim();
			string text25 = base.GetInt("ExamType", -1).ToString().Trim();
			string text26 = base.GetString("ExamTypeName").Trim();
			text26 = Input.URLDecode(text26).Trim();
			string text27 = base.GetString("OperateLevel").Trim();
			string text28 = base.GetInt("BusSet", -1).ToString().Trim();
			string text29 = base.GetString("BusPEPackageName");
			text29 = Input.URLDecode(text29).Trim();
			string text30 = base.GetString("Note");
			text30 = Input.URLDecode(text30).Trim();
			string text31 = base.GetString("BusFeeItems");
			text31 = Input.URLDecode(text31).Trim();
			string text32 = base.GetString("ExamPlace").Trim();
			string text33 = base.GetString("ExamPlaceName");
			text33 = Input.URLDecode(text33).Trim();
			int num = base.GetInt("Nation", -1);
			if (num < 1)
			{
				num = -1;
			}
			string text34 = num.ToString().Trim();
			string text35 = base.GetString("NationName");
			text35 = Input.URLDecode(text35).Trim();
			string text36 = base.GetInt("Cultrul", -1).ToString().Trim();
			string text37 = base.GetString("CultrulName");
			text37 = Input.URLDecode(text37).Trim();
			if (text36 == "-1")
			{
				text37 = string.Empty;
			}
			string text38 = DateTime.Now.ToString();
			string string3 = base.GetString("Base64Photo");
			string text39 = base.GetString("Address").Trim();
			string text40 = base.GetInt("Vocation", -1).ToString().Trim();
			string text41 = base.GetString("VocationName").Trim();
			if (text40 == "-1")
			{
				text41 = string.Empty;
			}
			string[] array = text31.TrimEnd(new char[]
			{
				'|'
			}).Split(new char[]
			{
				'|'
			});
			int num2 = array.Length;
			if (string.IsNullOrEmpty(text4))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户姓名不允许为空！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户姓名为空! 客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else if (string.IsNullOrEmpty(text12))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户性别不允许为空！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户性别不允许为空! 客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else if (string.IsNullOrEmpty(@string))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户证件号不允许为空！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户证件号为空!客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else if (!this.IsIDCard(@string))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户证件号格式不正确！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户证件号格式不正确! 客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else if (string.IsNullOrEmpty(text13))
			{
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add("success");
				dataTable.Columns.Add("Message");
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "客户出生日期不允许为空！";
				dataTable.Rows.Add(dataRow);
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				dataTable.Dispose();
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",新增",
					text7,
					"客户出生日期不允许为空! 客户姓名：",
					text4,
					"客户性别:",
					text12,
					"出生日期:",
					text13,
					",婚姻状况:",
					text15,
					",证件号:",
					@string,
					",预定日期：",
					text5
				}));
			}
			else
			{
				DateTime now = DateTime.Now;
				if (!DateTime.TryParse(text13, out now))
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("success");
					dataTable.Columns.Add("Message");
					DataRow dataRow = dataTable.NewRow();
					dataRow["success"] = 0;
					dataRow["Message"] = "客户出生日期格式不正确！";
					dataTable.Rows.Add(dataRow);
					string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
					this.OutPutMessage(msg);
					dataTable.Dispose();
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增",
						text7,
						"客户出生日期格式不正确! 客户姓名：",
						text4,
						"客户性别:",
						text12,
						"出生日期:",
						text13,
						",婚姻状况:",
						text15,
						",证件号:",
						@string,
						",预定日期：",
						text5
					}));
				}
				else if (string.IsNullOrEmpty(text15))
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("success");
					dataTable.Columns.Add("Message");
					DataRow dataRow = dataTable.NewRow();
					dataRow["success"] = 0;
					dataRow["Message"] = "客户婚姻状况不允许为空！";
					dataTable.Rows.Add(dataRow);
					string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
					this.OutPutMessage(msg);
					dataTable.Dispose();
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增",
						text7,
						"客户婚姻状况不允许为空! 客户姓名：",
						text4,
						"客户性别:",
						text12,
						"出生日期:",
						text13,
						",婚姻状况:",
						text15,
						",证件号:",
						@string,
						",预定日期：",
						text5
					}));
				}
				else if (string.IsNullOrEmpty(text5))
				{
					DataTable dataTable = new DataTable();
					dataTable.Columns.Add("success");
					dataTable.Columns.Add("Message");
					DataRow dataRow = dataTable.NewRow();
					dataRow["success"] = 0;
					dataRow["Message"] = "客户预定日期不允许为空！";
					dataTable.Rows.Add(dataRow);
					string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
					this.OutPutMessage(msg);
					dataTable.Dispose();
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增",
						text7,
						"客户预定日期不允许为空! 客户姓名：",
						text4,
						"客户性别:",
						text12,
						"出生日期:",
						text13,
						",婚姻状况:",
						text15,
						",证件号:",
						@string,
						",预定日期：",
						text5
					}));
				}
				else
				{
					DateTime now2 = DateTime.Now;
					if (!DateTime.TryParse(text5, out now2))
					{
						DataTable dataTable = new DataTable();
						dataTable.Columns.Add("success");
						dataTable.Columns.Add("Message");
						DataRow dataRow = dataTable.NewRow();
						dataRow["success"] = 0;
						dataRow["Message"] = "客户预定日期格式不正确！";
						dataTable.Rows.Add(dataRow);
						string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
						this.OutPutMessage(msg);
						dataTable.Dispose();
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",新增",
							text7,
							"客户预定日期格式不正确! 客户姓名：",
							text4,
							"客户性别:",
							text12,
							"出生日期:",
							text13,
							",婚姻状况:",
							text15,
							",证件号:",
							@string,
							",预定日期：",
							text5
						}));
					}
					else
					{
						if (string.IsNullOrEmpty(text2))
						{
							flag = true;
							text2 = Customer.CreateMaxNumX(ref value, this.Is_Subscribed, dateTime, base.GetAllSumRegistCache());
							if (string.IsNullOrEmpty(text2))
							{
								DataTable dataTable = new DataTable();
								dataTable.Columns.Add("success");
								dataTable.Columns.Add("Message");
								DataRow dataRow = dataTable.NewRow();
								dataRow["success"] = 0;
								dataRow["Message"] = string.Concat(new string[]
								{
									"对不起,用于",
									text7,
									"体检日期",
									text5,
									"的体检号不足,请您与管理员联系！"
								});
								dataTable.Rows.Add(dataRow);
								string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
								this.OutPutMessage(msg);
								dataTable.Dispose();
								Log4J.Instance.Error(string.Concat(new string[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",新增",
									text7,
									"体检号不足! 客户姓名：",
									text4,
									",证件号:",
									@string
								}));
								return;
							}
						}
						string newValue = string.Empty;
						string newValue2 = string.Empty;
						string text42 = string.Empty;
						List<string> list = new List<string>();
						string text43 = string.Format("if not exists(select CultrulName from OnArcCust where IDCard='{0}' AND CustomerName='{1}')\r\n            BEGIN\r\n                @TempInsertSql\r\n            END\r\n            ELSE \r\n            BEGIN\r\n                @TempUpdateSql\r\n            END;", @string, text4);
						string text44 = string.Format("IF NOT EXISTS(select ID_Customer FROM OnCustPhysicalExamInfo where ID_Customer='{0}')\r\nBEGIN\r\n     @TempInsertSql\r\nEND\r\nELSE \r\nBEGIN\r\n    @TempUpdateSql\r\nEND;", text2);
						string text45 = string.Format("IF NOT EXISTS(select ID_Customer FROM OnCustRelationCustPEInfo WHERE ID_Customer='{0}')\r\nBEGIN\r\n     @TempInsertSql\r\nEND\r\nELSE \r\nBEGIN\r\n    @TempUpdateSql\r\nEND;", text2);
						string sql = string.Format("SELECT ID_Customer,Is_Subscribed,ID_SubScriber,SubScriber,SubScriberOperDate,SubScribDate,ID_Operator,Operator,OperateDate,ISNULL(Is_FeeSettled,0) Is_FeeSettled,ISNULL(Is_GuideSheetPrinted,0) Is_GuideSheetPrinted,ISNULL(Is_Team,0) Is_Team FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}';\r\n                        SELECT ID_Customer,ID_Section FROM OnCustExamSection where ID_Customer='{0}';SELECT ID_CustFee,ID_Customer,ID_Fee,ISNULL(Is_FeeCharged,0)Is_FeeCharged,ISNULL(Is_FeeRefund,0) Is_FeeRefund FROM OnCustFee WHERE ID_Customer='{0}';", text2);
						DataSet dataSet = CommonExcuteSql.Instance.ExcuteSql(sql);
						DataTable dataTable2 = dataSet.Tables[0];
						DataTable dataTable3 = dataSet.Tables[1];
						DataTable dataTable4 = dataSet.Tables[2];
						string value2 = string.Empty;
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						bool flag2 = false;
						if (dataTable2.Rows.Count > 0)
						{
							value2 = dataTable2.Rows[0]["ID_Customer"].ToString();
							num4 = (bool.Parse(dataTable2.Rows[0]["Is_GuideSheetPrinted"].ToString()) ? 1 : 0);
							num3 = (bool.Parse(dataTable2.Rows[0]["Is_FeeSettled"].ToString()) ? 1 : 0);
							num5 = (bool.Parse(dataTable2.Rows[0]["Is_Team"].ToString()) ? 1 : 0);
							int arg_13DD_0 = string.IsNullOrEmpty(dataTable2.Rows[0]["Operator"].ToString()) ? 0 : 1;
						}
						string UserID = this.UserID;
						string userName = this.UserName;
						string text46 = DateTime.Now.ToString();
						int num6 = int.Parse(this.GetFeeWay().Select("FeeWayName='统一收费'")[0]["ID_FeeWay"].ToString());
						dataTable2.Dispose();
						if (a == "regist" || a == "sign" || a == "signandregiste")
						{
							if (text34 != "-1" && text36 != "-1")
							{
								newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,ID_Nation,NationName,ID_Cultrul,CultrulName,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}',1);", new object[]
								{
									text4,
									@string,
									string2,
									text13,
									text11,
									text12,
									text14,
									text15,
									text16,
									text18,
									0,
									text17,
									text17,
									text34,
									text35,
									text36,
									text37
								});
								if (flag)
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{13}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',ID_Cultrul='{9}',CultrulName='{10}',BirthDay='{11}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{12}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text34,
										text35,
										text36,
										text37,
										text13,
										@string,
										string2
									});
								}
								else
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{13}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',ID_Cultrul='{9}',CultrulName='{10}',BirthDay='{11}' where IDCard='{12}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text34,
										text35,
										text36,
										text37,
										text13,
										@string,
										string2
									});
								}
								text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text34 != "-1" && text36 == "-1")
							{
								newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,ID_Nation,NationName,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',1);", new object[]
								{
									text4,
									@string,
									string2,
									text13,
									text11,
									text12,
									text14,
									text15,
									text16,
									text18,
									0,
									text17,
									text17,
									text34,
									text35
								});
								if (flag)
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',BirthDay='{9}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{10}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text34,
										text35,
										text13,
										@string,
										string2
									});
								}
								else
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Nation='{7}',NationName='{8}',BirthDay='{9}' where IDCard='{10}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text34,
										text35,
										text13,
										@string,
										string2
									});
								}
								text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text34 == "-1" && text36 == "-1")
							{
								newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',1);", new object[]
								{
									text4,
									@string,
									string2,
									text13,
									text11,
									text12,
									text14,
									text15,
									text16,
									text18,
									0,
									text17,
									text17
								});
								if (flag)
								{
									newValue2 = string.Format("update OnArcCust set  ExamCard='{9}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',BirthDay='{7}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{8}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text13,
										@string,
										string2
									});
								}
								else
								{
									newValue2 = string.Format("update OnArcCust set  ExamCard='{9}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',BirthDay='{7}' where IDCard='{8}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text13,
										@string,
										string2
									});
								}
								text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text34 == "-1" && text36 != "-1")
							{
								newValue = string.Format("INSERT INTO OnArcCust(CustomerName,IDCard,ExamCard,BirthDay,ID_Gender,GenderName,ID_Marriage,MarriageName,MobileNo,Email,FinishedNum,FirstDatePE,LatestDatePE,ID_Cultrul,CultrulName,UnfinishedNum)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',1);", new object[]
								{
									text4,
									@string,
									string2,
									text13,
									text11,
									text12,
									text14,
									text15,
									text16,
									text18,
									0,
									text17,
									text17,
									text36,
									text37
								});
								if (flag)
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Cultrul='{7}',CultrulName='{8}',BirthDay='{9}',UnfinishedNum=ISNULL(UnfinishedNum,0)+1 where IDCard='{10}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text36,
										text37,
										text13,
										@string,
										string2
									});
								}
								else
								{
									newValue2 = string.Format("update OnArcCust set ExamCard='{11}', CustomerName='{0}',ID_Gender='{1}',GenderName='{2}',ID_Marriage='{3}',MarriageName='{4}',MobileNo='{5}',Email='{6}',ID_Cultrul='{7}',CultrulName='{8}',BirthDay='{9}' where IDCard='{10}' AND CustomerName='{0}';", new object[]
									{
										text4,
										text11,
										text12,
										text14,
										text15,
										text16,
										text18,
										text36,
										text37,
										text13,
										@string,
										string2
									});
								}
								text42 = text43.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text28 != "-1" && text19 != "-1")
							{
								newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team\r\n,ID_Set,SetName,ID_GuideNurse,GuideNurse,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName\r\n,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}'\r\n,'{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}';", new object[]
								{
									text2,
									text25,
									0,
									text28,
									text29,
									text19,
									text20,
									text23,
									text24,
									text21,
									text22,
									text27,
									0,
									0,
									0,
									this.Is_Subscribed,
									text30,
									text4,
									text33,
									text32,
									text26,
									this.UserID,
									this.UserName,
									text5,
									text38,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_Set='{4}',SetName='{5}',ID_GuideNurse='{6}',GuideNurse='{7}',ID_ReportWay='{8}'\r\n,ReportWayName='{9}',ID_FeeWay='{10}',FeeWayName='{11}',SecurityLevel='{12}',Note='{13}',ID_ExamPlace='{14}',SubScribDate='{15}'\r\n,ID_Gender='{17}',GenderName='{18}',ID_Marriage='{19}',MarriageName='{20}',ID_Nation='{21}',NationName='{22}',ID_Cultrul='{23}',CultrulName='{24}',ID_Vocation='{25}',VocationName='{26}',IDCard='{27}',ExamCard='{28}',BirthDay='{29}',Address='{30}',MobileNo='{31}',Email='{32}'\r\nwhere ID_Customer='{16}';", new object[]
								{
									text4,
									text25,
									text26,
									text33,
									text28,
									text29,
									text19,
									text20,
									text23,
									text24,
									text21,
									text22,
									text27,
									text30,
									text32,
									text5,
									text2,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text28 != "-1" && text19 == "-1")
							{
								newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team\r\n,ID_Set,SetName,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName\r\n,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}'\r\n,'{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}'\r\n;", new object[]
								{
									text2,
									text25,
									0,
									text28,
									text29,
									text23,
									text24,
									text21,
									text22,
									text27,
									0,
									0,
									0,
									this.Is_Subscribed,
									text30,
									text4,
									text33,
									text32,
									text26,
									this.UserID,
									this.UserName,
									text5,
									text38,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_Set='{4}',SetName='{5}',ID_ReportWay='{6}'\r\n,ReportWayName='{7}',ID_FeeWay='{8}',FeeWayName='{9}',SecurityLevel='{10}',Note='{11}',ID_ExamPlace='{12}',SubScribDate='{13}'\r\n,ID_Gender='{15}',GenderName='{16}',ID_Marriage='{17}',MarriageName='{18}',ID_Nation='{19}',NationName='{20}',ID_Cultrul='{21}',CultrulName='{22}',ID_Vocation='{23}',VocationName='{24}',IDCard='{25}',ExamCard='{26}',BirthDay='{27}',Address='{28}',MobileNo='{29}',Email='{30}'\r\nwhere ID_Customer='{14}';", new object[]
								{
									text4,
									text25,
									text26,
									text33,
									text28,
									text29,
									text23,
									text24,
									text21,
									text22,
									text27,
									text30,
									text32,
									text5,
									text2,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text28 == "-1" && text19 != "-1")
							{
								newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName\r\n,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}'\r\n,'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}';", new object[]
								{
									text2,
									text25,
									0,
									text23,
									text24,
									text21,
									text22,
									text27,
									0,
									0,
									0,
									this.Is_Subscribed,
									text30,
									text4,
									text33,
									text32,
									text26,
									this.UserID,
									this.UserName,
									text5,
									text38,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_GuideNurse='{4}',GuideNurse='{5}',ID_ReportWay='{6}'\r\n,ReportWayName='{7}',ID_FeeWay='{8}',FeeWayName='{9}',SecurityLevel='{10}',Note='{11}',ID_ExamPlace='{12}',SubScribDate='{13}'\r\n,ID_Gender='{15}',GenderName='{16}',ID_Marriage='{17}',MarriageName='{18}',ID_Nation='{19}',NationName='{20}',ID_Cultrul='{21}',CultrulName='{22}',ID_Vocation='{23}',VocationName='{24}',IDCard='{25}',ExamCard='{26}',BirthDay='{27}',Address='{28}',MobileNo='{29}',Email='{30}'\r\nwhere ID_Customer='{14}';", new object[]
								{
									text4,
									text25,
									text26,
									text33,
									text19,
									text20,
									text23,
									text24,
									text21,
									text22,
									text27,
									text30,
									text32,
									text5,
									text2,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (text28 == "-1" && text19 == "-1")
							{
								newValue = string.Format("INSERT INTO OnCustPhysicalExamInfo(ID_Customer,ID_ExamType,Is_Team,ID_ReportWay,ReportWayName,ID_FeeWay,FeeWayName,SecurityLevel,Is_GuideSheetPrinted,Is_SectionLock,Is_FinalFinished,Is_Subscribed,Note,CustomerName,ExamPlaceName,ID_ExamPlace,ExamTypeName,ID_SubScriber,SubScriber,SubScribDate,SubScriberOperDate\r\n,ID_Gender,GenderName,ID_Marriage,MarriageName,ID_Nation,NationName,ID_Cultrul,CultrulName,ID_Vocation,VocationName,IDCard,ExamCard,BirthDay,Address,MobileNo,Email)\r\nselect '{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}'\r\n,'{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}';", new object[]
								{
									text2,
									text25,
									0,
									text23,
									text24,
									text21,
									text22,
									text27,
									0,
									0,
									0,
									this.Is_Subscribed,
									text30,
									text4,
									text33,
									text32,
									text26,
									this.UserID,
									this.UserName,
									text5,
									text38,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								newValue2 = string.Format("update OnCustPhysicalExamInfo set CustomerName='{0}',ID_ExamType='{1}',ExamTypeName='{2}',ExamPlaceName='{3}',ID_ReportWay='{4}'\r\n,ReportWayName='{5}',ID_FeeWay='{6}',FeeWayName='{7}',SecurityLevel='{8}',Note='{9}',ID_ExamPlace='{10}',SubScribDate='{11}'\r\n,ID_Gender='{13}',GenderName='{14}',ID_Marriage='{15}',MarriageName='{16}',ID_Nation='{17}',NationName='{18}',ID_Cultrul='{19}',CultrulName='{20}',ID_Vocation='{21}',VocationName='{22}',IDCard='{23}',ExamCard='{24}',BirthDay='{25}',Address='{26}',MobileNo='{27}',Email='{28}'\r\nwhere ID_Customer='{12}';", new object[]
								{
									text4,
									text25,
									text26,
									text33,
									text23,
									text24,
									text21,
									text22,
									text27,
									text30,
									text32,
									text5,
									text2,
									text11,
									text12,
									text14,
									text15,
									text34,
									text35,
									text36,
									text37,
									text40,
									text41,
									@string,
									string2,
									text13,
									text39,
									text16,
									text18
								});
								text42 += text44.Clone().ToString().Replace("@TempInsertSql", newValue).Replace("@TempUpdateSql", newValue2);
							}
							if (string.IsNullOrEmpty(value2))
							{
								text42 += string.Format("INSERT INTO OnCustRelationCustPEInfo(ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,Is_CompletePhysical,ExamState)\r\nVALUES((SELECT TOP 1 ID_ArcCustomer FROM OnArcCust WHERE IDCard='{0}' AND CustomerName='{3}' ORDER BY ID_ArcCustomer DESC),'{0}','{1}','{2}',0,0);", new object[]
								{
									@string,
									string2,
									text2,
									text4
								});
							}
							else
							{
								text42 += string.Format("UPDATE OnCustRelationCustPEInfo SET IDCardNo='{0}',ExamCardNo='{1}' WHERE ID_Customer='{2}';", @string, string2, text2);
							}
							int num7 = -1;
							string text47 = string.Empty;
							string text48 = string.Empty;
							string text49 = string.Empty;
							string text50 = string.Empty;
							string text51 = string.Empty;
							string a2 = string.Empty;
							string text52 = string.Empty;
							string text53 = string.Empty;
							string text54 = string.Empty;
							string text55 = string.Empty;
							string a3 = string.Empty;
							for (int i = 0; i < num2; i++)
							{
								string[] array2 = array[i].TrimEnd(new char[]
								{
									'_'
								}).Split(new char[]
								{
									'_'
								});
								if (array2[0] != string.Empty)
								{
									text47 = array2[0].Trim();
									text48 = array2[1].Trim();
									text49 = this.UserID.ToString();
									text50 = this.UserName;
									decimal num8 = decimal.Parse(array2[2].Trim());
									decimal num9 = decimal.Parse(array2[3].Trim());
									decimal num10 = decimal.Parse(array2[4].Trim());
									text51 = array2[5].Trim();
									a2 = array2[6].Trim();
									text52 = array2[8].Trim();
									text53 = array2[9].Trim();
									int num11 = int.Parse(array2[10].Trim());
									text54 = array2[11].Trim();
									int.TryParse(array2[13], out num7);
									if (array2[array2.Length - 1] == "0")
									{
										text55 = Customer.CreateMaxApplyID();
										while (a3 == text55)
										{
											text55 = Customer.CreateMaxApplyID();
										}
										a3 = text55;
									}
									int num12 = 0;
									int num13 = 0;
									if (num5 == 1)
									{
										if (text51 != num6.ToString())
										{
											num13 = 1;
										}
									}
									else if (num4 == 1)
									{
										num13 = 1;
									}
									if (num3 == 0)
									{
										if (num13 == 1)
										{
											num3 = 1;
										}
									}
									if (a2 == "统一收费" || a2 == "记账")
									{
										num12 = 1;
									}
									if (num12 == 1)
									{
										if (num7 > 0)
										{
											text42 += string.Format("UPDATE OnCustFee SET OriginalPrice='{0}',Discount='{1}',FactPrice='{2}',ID_FeeType='{3}',ID_Discounter='{4}',DiscounterName='{5}'\r\n                                                  ,Is_FeeCharged='{6}',ID_FeeCharger='{7}',FeeCharger='{8}',FeeChargeDate='{9}' WHERE ID_CustFee='{10}' AND ISNULL(Is_FeeCharged,0)=0;", new object[]
											{
												num8,
												num9,
												num10,
												text51,
												num11,
												text54,
												num12,
                                                UserID,
												userName,
												text46,
												num7
											});
										}
										else
										{
											flag2 = true;
											DataRow[] array3 = dataTable4.Select(string.Format("ID_Customer='{0}' AND ID_Fee='{1}' AND Is_FeeCharged=0", text2, text47));
											if (array3.Length < 1)
											{
												text42 += string.Format("INSERT INTO OnCustFee(ID_Customer,ID_Fee,FeeItemName,ID_Register,RegisterName,RegistDate\r\n,OriginalPrice,Discount,FactPrice,ID_FeeType,ID_Discounter,DiscounterName,Is_FeeCharged,CustFeeChargeState,Is_Printed,ID_FeeCharger,FeeCharger,FeeChargeDate,ApplyID)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}');", new object[]
												{
													text2,
													text47,
													text48,
													text49,
													text50,
													text17,
													num8,
													num9,
													num10,
													text51,
													num11,
													text54,
													num12,
													num13,
													0,
                                                    UserID,
													userName,
													text46,
													text55
												});
											}
										}
									}
									else if (num7 > 0)
									{
										text42 += string.Format("UPDATE OnCustFee SET OriginalPrice='{0}',Discount='{1}',FactPrice='{2}',ID_FeeType='{3}',ID_Discounter='{4}',DiscounterName='{5}' WHERE ID_CustFee='{6}' AND ISNULL(Is_FeeCharged,0)=0;", new object[]
										{
											num8,
											num9,
											num10,
											text51,
											num11,
											text54,
											num7
										});
									}
									else
									{
										DataRow[] array3 = dataTable4.Select(string.Format("ID_Customer='{0}' AND ID_Fee='{1}' AND Is_FeeCharged=0", text2, text47));
										if (array3.Length < 1)
										{
											text42 += string.Format("INSERT INTO OnCustFee(ID_Customer,ID_Fee,FeeItemName,ID_Register,RegisterName,RegistDate\r\n,OriginalPrice,Discount,FactPrice,ID_FeeType,ID_Discounter,DiscounterName,CustFeeChargeState,Is_FeeCharged,Is_Printed,ApplyID)\r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}',0,0,'{13}');", new object[]
											{
												text2,
												text47,
												text48,
												text49,
												text50,
												text17,
												num8.ToString(),
												num9,
												num10.ToString(),
												text51,
												num11,
												text54,
												num13,
												text55
											});
										}
									}
									if (dataTable3.Select(string.Concat(new string[]
									{
										"ID_Customer='",
										text2,
										"' AND ID_Section='",
										text52,
										"'"
									})).Length == 0)
									{
										text42 += string.Format("INSERT INTO OnCustExamSection(ID_Customer,ID_Section,SectionName,CustomerName) values('{0}','{1}','{2}','{3}');", new object[]
										{
											text2,
											text52,
											text53,
											text4
										});
										DataRow dataRow2 = dataTable3.NewRow();
										dataRow2["ID_Customer"] = text2;
										dataRow2["ID_Section"] = text52;
										dataTable3.Rows.Add(dataRow2);
									}
									if (i != 0 && i % this._defaultCount == 0)
									{
										list.Add(text42);
										text42 = string.Empty;
									}
								}
							}
							if (text42 != string.Empty)
							{
								list.Add(text42);
							}
							string item = string.Format("IF NOT EXISTS(select TOP 1 ID_Customer FROM OnCustFee WHERE ID_Customer='{0}' AND Is_FeeCharged!=1)\r\nBEGIN\r\n     UPDATE OnCustPhysicalExamInfo SET Is_FeeSettled=1 WHERE ID_Customer='{0}';\r\nEND\r\nELSE \r\nBEGIN\r\n     UPDATE OnCustPhysicalExamInfo SET Is_FeeSettled=0 WHERE ID_Customer='{0}';\r\nEND;", text2);
							list.Add(item);
						}
						int num14 = CommonExcuteSql.Instance.ExecuteSqlTran(list);
						if (num14 <= 0)
						{
							DataTable dataTable = new DataTable();
							dataTable.Columns.Add("success");
							dataTable.Columns.Add("Message");
							DataRow dataRow = dataTable.NewRow();
							dataRow["success"] = 0;
							dataRow["Message"] = text3;
							dataTable.Rows.Add(dataRow);
							string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
							this.OutPutMessage(msg);
							dataTable.Dispose();
							this.OutPutMessage(msg);
							text = this.GetdateDiff("使用新方法", this.BeginDate, DateTime.Now);
							Log4J.Instance.Error(string.Concat(new string[]
							{
								Public.GetClientIP(),
								",",
								this.LoginUserModel.UserName,
								",",
								text3.Replace("，请重试", ""),
								text,
								"  客户姓名：",
								text4,
								",身份证：",
								@string,
								",体检号：",
								text2
							}));
						}
						else
						{
							this.UpdateCustomerInfo_NewBusiness(text2, @string, text4, text11, text12, text13, int.Parse(text34), text35, string3);
							if (text6 == "1")
							{
								base.AddRegistCache(dateTime, 1, num5);
							}
							else if (text6 == "0")
							{
								base.AddRegistCache(dateTime, 0, num5);
							}
							try
							{
								int num15 = CommonOnArcCust.Instance.SumSectionExamInfo(text2);
							}
							catch (Exception ex)
							{
								text = this.GetdateDiff("SumSectionExamInfo_New", this.BeginDate, DateTime.Now);
								Log4J.Instance.Info(string.Concat(new string[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									text,
									" 客户姓名：",
									text4,
									",身份证：",
									@string,
									",体检号：",
									text2,
									",汇总科室收费状态失败,失败原因为:",
									ex.Message
								}));
							}
							DataTable dataTable5 = CommonOnArcCust.Instance.GetCustomerRelation(text2, false).Tables[0];
							string value3 = string.Empty;
							string value4 = text2;
							string value5 = string.Empty;
							string value6 = string.Empty;
							text6 = string.Empty;
							if (dataTable5.Rows.Count > 0)
							{
								value4 = dataTable5.Rows[0]["ID_Customer"].ToString();
								value = dataTable5.Rows[0]["Code128c"].ToString();
								value5 = dataTable5.Rows[0]["ID_ArcCustomer"].ToString();
								value6 = dataTable5.Rows[0]["Is_GuideSheetPrinted"].ToString();
								text6 = dataTable5.Rows[0]["Is_Subscribed"].ToString();
								value3 = dataTable5.Rows[0]["Is_FeeSettled"].ToString();
							}
							DataTable dataTable6 = new DataTable();
							dataTable6.Columns.Add("success");
							dataTable6.Columns.Add("Message");
							dataTable6.Columns.Add("ID_Customer");
							dataTable6.Columns.Add("Code128c");
							dataTable6.Columns.Add("ID_ArcCustomer");
							dataTable6.Columns.Add("Is_GuideSheetPrinted");
							dataTable6.Columns.Add("Is_Subscribed");
							dataTable6.Columns.Add("Is_FeeSettled");
							DataRow dataRow = dataTable6.NewRow();
							dataRow["success"] = 1;
							dataRow["Message"] = text8;
							dataRow["ID_Customer"] = value4;
							dataRow["Code128c"] = value;
							dataRow["ID_ArcCustomer"] = value5;
							dataRow["Is_GuideSheetPrinted"] = value6;
							dataRow["Is_Subscribed"] = text6;
							dataRow["Is_FeeSettled"] = value3;
							dataTable6.Rows.Add(dataRow);
							string msg2 = JsonHelperFont.Instance.DataTableToJSON(dataTable6, "dataList");
							this.OutPutMessage(msg2);
							dataTable6.Dispose();
							dataTable5.Dispose();
							base.ClearCache_CustRelationCustPEInfo(text2);
							text = this.GetdateDiff("使用新方法", this.BeginDate, DateTime.Now);
							Log4J.Instance.Info(string.Concat(new string[]
							{
								Public.GetClientIP(),
								",",
								this.LoginUserModel.UserName,
								",",
								text8,
								text,
								" 客户姓名：",
								text4,
								",身份证：",
								@string,
								",体检号：",
								text2
							}));
							DateTime dateTime2;
							try
							{
								dateTime2 = DateTime.Parse(text5);
							}
							catch (Exception ex)
							{
								dateTime2 = DateTime.Now;
								Log4J.Instance.Info(string.Concat(new string[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",",
									text8,
									text,
									" 客户体检时间[",
									text5,
									"]转换成日期格式失败：",
									text4,
									",身份证：",
									@string,
									",体检号：",
									text2,
									" 失败原因:",
									ex.Message
								}));
							}
							if (flag)
							{
								EnumBusBackLogType enumBusBackLogType = EnumBusBackLogType.其它;
								if (text7 == "预约")
								{
									enumBusBackLogType = EnumBusBackLogType.预约;
								}
								else if (text7 == "登记")
								{
									PEIS.Model.OnCustBackLog onCustBackLog = new PEIS.Model.OnCustBackLog();
									enumBusBackLogType = EnumBusBackLogType.登记;
								}
								if (enumBusBackLogType != EnumBusBackLogType.其它)
								{
									base.AddOrUpdateByBackLogTypeOfRegiste(long.Parse(text2.ToString()), enumBusBackLogType, true, dateTime2);
								}
							}
							else
							{
								ArrayList arrayList = new ArrayList();
								arrayList.Add(1);
								arrayList.Add(2);
								ExternByUpdateRegisteType externByUpdateRegisteType = new ExternByUpdateRegisteType();
								externByUpdateRegisteType.Is_updateRegisteDate = new bool?(true);
								externByUpdateRegisteType.RegisteDate = dateTime2;
								externByUpdateRegisteType.CustBackLogList = arrayList;
								base.AddOrUpdateByBackLogType(long.Parse(text2.ToString()), EnumBusBackLogType.登记, true, externByUpdateRegisteType);
							}
							int num16 = bool.Parse(value3) ? 1 : 0;
							if (num16 == 1)
							{
								if (num16 != num3)
								{
									base.AddOrUpdateByBackLogType(long.Parse(text2.ToString()), EnumBusBackLogType.缴费, true, null);
								}
								else if (flag2)
								{
									base.AddOrUpdateByBackLogType(long.Parse(text2.ToString()), EnumBusBackLogType.缴费, true, null);
								}
							}
						}
						if (dataTable2 != null)
						{
							dataTable2.Dispose();
							dataTable2 = null;
						}
						if (dataTable3 != null)
						{
							dataTable3.Dispose();
							dataTable3 = null;
						}
						if (dataSet != null)
						{
							dataSet.Dispose();
							dataSet = null;
						}
					}
				}
			}
		}

		private string GetdateDiff(string Title, DateTime BeginDate, DateTime EndDate)
		{
			return Public.GetDateDiff(Title, BeginDate, EndDate);
		}

		private DataTable GetFeeWay()
		{
			return CommonOnArcCust.Instance.GetFeeWay();
		}

		public void SendWaitToInterfaceByClient()
		{
			int @int = base.GetInt("Forsex", -1);
			string @string = base.GetString("ID_Customer");
			if (string.IsNullOrEmpty(@string) || @int == -1)
			{
				Log4J.Instance.Error(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",调用接口待发送列表 体检号为：",
					@string,
					",性别为:",
					@int,
					",无法发送",
					Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
				}));
			}
			else
			{
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
					DataTable onCustExamSectionDT = CommonOnArcCust.Instance.GetOnCustWaitSendToInterfaceInfo(@string, text).Tables[0];
					this.SendWaitToInterface(@int, @string, onCustExamSectionDT);
				}
				catch (Exception ex)
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",读取待发送接口列表出错 体检号为：",
						@string,
						" 错误原因：",
						ex.Message,
						" 耗时：",
						Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
					}));
				}
				base.ClearCache_CustRelationCustPEInfo(@string);
			}
		}

		public void SendWaitToInterface(int Forsex, string ID_Customer, DataTable OnCustExamSectionDT)
		{
			DateTime now = DateTime.Now;
			if (OnCustExamSectionDT != null)
			{
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
							Forsex
						});
					}
				}
				List<string> list = new List<string>(3);
				string item = string.Format("DELETE FROM OnCustWaitSendToInterface WHERE ID_Customer='{0}';", ID_Customer);
				list.Add(item);
				if (stringBuilder.Length > 0)
				{
					list.Add(stringBuilder.ToString());
					OnCustExamSectionDT.Dispose();
				}
				try
				{
					int num = CommandWebserviceInterface.Instance.ExecuteSqlTran(list);
					if (num > 0)
					{
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",成功写入接口待发送列表 体检号为：",
							ID_Customer,
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
							",写入接口待发送列表失败 0行受影响 体检号为：",
							ID_Customer,
							" ",
							Public.GetDateDiff(string.Empty, now, DateTime.Now)
						}));
					}
				}
				catch (Exception ex)
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",写入接口待发送列表失败 体检号为：",
						ID_Customer,
						" ",
						Public.GetDateDiff(string.Empty, now, DateTime.Now),
						" 错误原因为：",
						ex.Message
					}));
				}
			}
		}

		private void RemoveImg(string IDCard)
		{
			string text = base.Server.MapPath("~/Image");
			if (!Directory.Exists(text))
			{
				Directory.CreateDirectory(text);
			}
			string str = IDCard + ".bmp";
			string path = text + "\\" + str;
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}

		public void GetBusFeeNotINCustFeeID()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("Gender").Trim();
			string text2 = base.GetString("CustFeeID").TrimEnd(new char[]
			{
				','
			});
			DataView defaultView = CommonOnArcCust.Instance.GetBusFeeNotINCustFeeID(text, text2, this.UserName, this.Discount.ToString()).DefaultView;
			string text3 = string.Empty;
			if (text == "-1")
			{
				text3 += "ForGender in(0,1,2)";
			}
			else
			{
				text3 = text3 + "ForGender in('" + text + "','2')";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text3 = text3 + " AND ID_Fee NOT IN(" + text2 + ")";
			}
			defaultView.RowFilter = text3;
			DataTable dataTable = defaultView.ToTable("dt");
			DataTable dataTable2 = dataTable.Clone();
			if (dataTable.Rows.Count > this._maxRowCount)
			{
				for (int i = 0; i < this._maxRowCount; i++)
				{
					dataTable2.ImportRow(dataTable.Rows[i]);
				}
			}
			else
			{
				dataTable2 = dataTable.Copy();
			}
			dataTable.Dispose();
			string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable2, "dataList");
			this.OutPutMessage(msg);
			dataTable2.Dispose();
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",通过性别和所属套餐获取套餐外项目(GetBusFeeNotINCustFeeID) 性别:",
				text,
				" 收费项目编码:",
				text2,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void SearchBusFeeByCustFeeID()
		{
			string @string = base.GetString("Gender");
			string text = base.GetString("CustFeeID").TrimEnd(new char[]
			{
				','
			});
			string selectedFee = base.GetString("SelectedFee").TrimEnd(new char[]
			{
				','
			});
			string inputCode = base.GetString("InputCode").Trim();
			DataView defaultView = CommonOnArcCust.Instance.SearchBusFeeByCustFeeID(@string, this.UserName, this.Discount.ToString(), text, selectedFee, inputCode).DefaultView;
			string text2 = string.Empty;
			if (@string == "-1")
			{
				text2 += "ForGender in(0,1,2)";
			}
			else
			{
				text2 = text2 + "ForGender in('" + @string + "','2')";
			}
			if (!string.IsNullOrEmpty(text))
			{
				text2 = text2 + " AND ID_Fee NOT IN(" + text + ")";
			}
			defaultView.RowFilter = text2;
			DataTable dataTable = defaultView.ToTable("dt");
			string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
			this.OutPutMessage(msg);
			defaultView.Dispose();
			dataTable.Dispose();
		}

		public void GetBusFeeBySetID()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("PEPackageID").Trim();
			string text2 = base.GetString("PEID").Trim();
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonOnArcCust.Instance.GetBusFeeBySetID(text2, text, this.UserID, this.UserName, this.Discount.ToString()), "dataList");
			this.OutPutMessage(msg);
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",通过套餐ID获取收费明细(GetBusFeeBySetID) 套餐编号:",
				text,
				" 体检号:",
				text2,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void GetBusFeeByCustomerID()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("ID_Customer").Trim();
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonOnArcCust.Instance.GetBusFeeByCustomerID(text, this.UserName), "dataList");
			this.OutPutMessage(msg);
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",通过体检号获取客户收费项目信息(GetBusFeeByCustomerID) 体检号:",
				text,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void GetRegisteCustomerList_Old()
		{
			int @int = base.GetInt("Is_InternatSubscribe", -1);
			int int2 = base.GetInt("IsTeam", -1);
			string a = base.GetString("modelName").Trim().ToLower();
			int int3 = base.GetInt("pageIndex", 1);
			int int4 = base.GetInt("pageSize", 10);
			string text = base.Server.HtmlDecode(base.GetString("BeginExamDate"));
			string text2 = base.Server.HtmlDecode(base.GetString("EndExamDate"));
			string paramvalue = base.Server.HtmlDecode(base.GetString("IDCard"));
			int totalCount = 0;
			int num = 0;
			if (!string.IsNullOrEmpty(text))
			{
				text += " 00:00:00";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 += " 23:59:59";
			}
			if (a == "regist")
			{
				this.Is_Subscribed = 1;
			}
			if (a == "sign")
			{
				this.Is_Subscribed = 0;
			}
			int int5 = base.GetInt("OnlyMySelf", -1);
			string text3 = Input.URLDecode(base.GetString("CustomerName"));
			int int6 = base.GetInt("Is_FeeSettled", -1);
			string pageCode = "QueryPhysicalExamInfoPagesParamX";
			SqlConditionInfo[] array = new SqlConditionInfo[9];
			if (@int == 1)
			{
				array[8] = new SqlConditionInfo("@Is_InternatSubscribe", @int, TypeCode.Int32);
				array[8].Blur = 4;
			}
			if (int5 >= 0)
			{
				if (int2 != -1)
				{
					if (this.Is_Subscribed == 0)
					{
						array[0] = new SqlConditionInfo("@ID_Operator", this.UserID, TypeCode.Int32);
						array[0].Blur = 4;
					}
					else if (this.Is_Subscribed == 1)
					{
						array[0] = new SqlConditionInfo("@ID_SubScriber", this.UserID, TypeCode.Int32);
						array[0].Blur = 4;
					}
					else
					{
						array[0] = new SqlConditionInfo("@ID_Creator", this.UserID, TypeCode.Int32);
						array[0].Blur = 4;
					}
				}
				else
				{
					array[0] = new SqlConditionInfo("@ID_Creator", this.UserID, TypeCode.Int32);
					array[0].Blur = 4;
				}
			}
			array[1] = new SqlConditionInfo("@IDCardNo", paramvalue, TypeCode.String);
			array[1].Blur = 3;
			if (this.Is_Subscribed == 1)
			{
				if (int2 != -1)
				{
					array[2] = new SqlConditionInfo("@Is_Subscribed", this.Is_Subscribed, TypeCode.Int32);
					array[2].Blur = 4;
				}
			}
			if (int2 != -1)
			{
				array[3] = new SqlConditionInfo("@Is_Team", int2, TypeCode.Int32);
				array[3].Blur = 4;
			}
			if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
			{
				if (int2 != -1)
				{
					if (this.Is_Subscribed == 0)
					{
						array[4] = new SqlConditionInfo("@OperateDate", text, TypeCode.DateTime);
						array[4].ParamOper = ">=";
					}
					else if (this.Is_Subscribed == 1)
					{
						array[4] = new SqlConditionInfo("@SubScriberOperDate", text, TypeCode.DateTime);
						array[4].ParamOper = ">=";
					}
					else
					{
						array[4] = new SqlConditionInfo("@CreateDate", text, TypeCode.DateTime);
						array[4].ParamOper = ">=";
					}
				}
				else
				{
					array[4] = new SqlConditionInfo("@CreateDate", text, TypeCode.DateTime);
					array[4].ParamOper = ">=";
				}
			}
			if (!string.IsNullOrEmpty(text2) && Input.IsDate(text2))
			{
				if (int2 != -1)
				{
					if (this.Is_Subscribed == 0)
					{
						array[5] = new SqlConditionInfo("@OperateDate", text2, TypeCode.DateTime);
						array[5].ParamOper = "<=";
					}
					else if (this.Is_Subscribed == 1)
					{
						array[5] = new SqlConditionInfo("@SubScriberOperDate", text2, TypeCode.DateTime);
						array[5].ParamOper = "<=";
					}
					else
					{
						array[5] = new SqlConditionInfo("@CreateDate", text2, TypeCode.DateTime);
						array[5].ParamOper = "<=";
					}
				}
				else
				{
					array[5] = new SqlConditionInfo("@CreateDate", text2, TypeCode.DateTime);
					array[5].ParamOper = "<=";
				}
			}
			if (!string.IsNullOrEmpty(text3))
			{
				array[6] = new SqlConditionInfo("@CustomerName", text3, TypeCode.String);
				array[6].ParamOper = "=";
			}
			if (int6 != -1)
			{
				array[7] = new SqlConditionInfo("@Is_FeeSettled", int6, TypeCode.Int32);
				array[7].ParamOper = "=";
			}
			DataTable page = CommonRegiste.Instance.GetPage(pageCode, int3, int4, out totalCount, out num, array);
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		public void GetRegisteCustomerList_BadSearch()
		{
			int @int = base.GetInt("IsTeam", -1);
			string a = base.GetString("modelName").Trim().ToLower();
			int int2 = base.GetInt("pageIndex", 1);
			int int3 = base.GetInt("pageSize", 10);
			string text = base.Server.HtmlDecode(base.GetString("BeginExamDate"));
			string text2 = base.Server.HtmlDecode(base.GetString("EndExamDate"));
			string text3 = base.Server.HtmlDecode(base.GetString("IDCard"));
			int totalCount = 0;
			int num = 0;
			if (!string.IsNullOrEmpty(text))
			{
				text += " 00:00:00";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 += " 23:59:59";
			}
			if (a == "regist")
			{
				this.Is_Subscribed = 1;
			}
			if (a == "sign")
			{
				this.Is_Subscribed = 0;
			}
			int int4 = base.GetInt("OnlyMySelf", -1);
			string text4 = Input.URLDecode(base.GetString("CustomerName"));
			int int5 = base.GetInt("Is_FeeSettled", -1);
			string pageCode = "QueryPhysicalExamInfoPagesParamX";
			SqlConditionInfo[] array = new SqlConditionInfo[8];
			string a2 = base.GetString("dateTypeName").Trim().ToLower();
			if (int4 >= 0)
			{
				if (@int != -1)
				{
					if (this.Is_Subscribed == 0)
					{
						if (a2 == "SubScriberOperDate".ToLower())
						{
							array[0] = new SqlConditionInfo("@ID_SubScriber", this.UserID, TypeCode.Int32);
							array[0].Blur = 4;
						}
						else if (a2 == "OperateDate".ToLower())
						{
							array[0] = new SqlConditionInfo("@ID_Operator", this.UserID, TypeCode.Int32);
							array[0].Blur = 4;
						}
					}
					else if (this.Is_Subscribed == 1)
					{
						if (a2 == "SubScriberOperDate".ToLower())
						{
							array[0] = new SqlConditionInfo("@ID_SubScriber", this.UserID, TypeCode.Int32);
							array[0].Blur = 4;
						}
						else if (a2 == "OperateDate".ToLower())
						{
							array[0] = new SqlConditionInfo("@ID_Operator", this.UserID, TypeCode.Int32);
							array[0].Blur = 4;
						}
					}
					else if (a2 == "SubScriberOperDate".ToLower())
					{
						array[0] = new SqlConditionInfo("@ID_SubScriber", this.UserID, TypeCode.Int32);
						array[0].Blur = 4;
					}
					else if (a2 == "OperateDate".ToLower())
					{
						array[0] = new SqlConditionInfo("@ID_Operator", this.UserID, TypeCode.Int32);
						array[0].Blur = 4;
					}
				}
				else if (a2 == "SubScriberOperDate".ToLower())
				{
					array[0] = new SqlConditionInfo("@ID_SubScriber", this.UserID, TypeCode.Int32);
					array[0].Blur = 4;
				}
				else if (a2 == "OperateDate".ToLower())
				{
					array[0] = new SqlConditionInfo("@ID_Operator", this.UserID, TypeCode.Int32);
					array[0].Blur = 4;
				}
			}
			array[1] = new SqlConditionInfo("@IDCard", text3, TypeCode.String);
			array[1].Blur = 3;
			if (this.Is_Subscribed == 1)
			{
				if (@int != -1)
				{
					array[2] = new SqlConditionInfo("@Is_Subscribed", this.Is_Subscribed, TypeCode.Int32);
					array[2].Blur = 4;
				}
			}
			if (@int != -1)
			{
				array[3] = new SqlConditionInfo("@Is_Team", @int, TypeCode.Int32);
				array[3].Blur = 4;
			}
			if (string.IsNullOrEmpty(text3))
			{
				if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
				{
					if (@int != -1)
					{
						if (this.Is_Subscribed == 0)
						{
							if (a2 == "SubScriberOperDate".ToLower())
							{
								array[4] = new SqlConditionInfo("@SubScriberOperDate", text, TypeCode.DateTime);
								array[4].ParamOper = ">=";
							}
							else if (a2 == "OperateDate".ToLower())
							{
								array[4] = new SqlConditionInfo("@OperateDate", text, TypeCode.DateTime);
								array[4].ParamOper = ">=";
							}
							else
							{
								array[4] = new SqlConditionInfo("@OperateDate", text, TypeCode.DateTime);
								array[4].ParamOper = ">=";
							}
						}
						else if (this.Is_Subscribed == 1)
						{
							if (a2 == "SubScriberOperDate".ToLower())
							{
								array[4] = new SqlConditionInfo("@SubScriberOperDate", text, TypeCode.DateTime);
								array[4].ParamOper = ">=";
							}
							else if (a2 == "OperateDate".ToLower())
							{
								array[4] = new SqlConditionInfo("@OperateDate", text, TypeCode.DateTime);
								array[4].ParamOper = ">=";
							}
							else
							{
								array[4] = new SqlConditionInfo("@SubScriberOperDate", text, TypeCode.DateTime);
								array[4].ParamOper = ">=";
							}
						}
						else if (a2 == "SubScriberOperDate".ToLower())
						{
							array[4] = new SqlConditionInfo("@SubScriberOperDate", text, TypeCode.DateTime);
							array[4].ParamOper = ">=";
						}
						else if (a2 == "OperateDate".ToLower())
						{
							array[4] = new SqlConditionInfo("@OperateDate", text, TypeCode.DateTime);
							array[4].ParamOper = ">=";
						}
					}
					else if (a2 == "SubScriberOperDate".ToLower())
					{
						array[4] = new SqlConditionInfo("@SubScriberOperDate", text, TypeCode.DateTime);
						array[4].ParamOper = ">=";
					}
					else if (a2 == "OperateDate".ToLower())
					{
						array[4] = new SqlConditionInfo("@OperateDate", text, TypeCode.DateTime);
						array[4].ParamOper = ">=";
					}
				}
				if (!string.IsNullOrEmpty(text2) && Input.IsDate(text2))
				{
					if (@int != -1)
					{
						if (this.Is_Subscribed == 0)
						{
							array[5] = new SqlConditionInfo("@OperateDate", text2, TypeCode.DateTime);
							array[5].ParamOper = "<=";
						}
						else if (this.Is_Subscribed == 1)
						{
							array[5] = new SqlConditionInfo("@SubScriberOperDate", text2, TypeCode.DateTime);
							array[5].ParamOper = "<=";
						}
						else if (a2 == "SubScriberOperDate".ToLower())
						{
							array[5] = new SqlConditionInfo("@SubScriberOperDate", text2, TypeCode.DateTime);
							array[5].ParamOper = "<=";
						}
						else if (a2 == "OperateDate".ToLower())
						{
							array[5] = new SqlConditionInfo("@OperateDate", text2, TypeCode.DateTime);
							array[5].ParamOper = "<=";
						}
					}
					else if (a2 == "SubScriberOperDate".ToLower())
					{
						array[5] = new SqlConditionInfo("@SubScriberOperDate", text2, TypeCode.DateTime);
						array[5].ParamOper = "<=";
					}
					else if (a2 == "OperateDate".ToLower())
					{
						array[5] = new SqlConditionInfo("@OperateDate", text2, TypeCode.DateTime);
						array[5].ParamOper = "<=";
					}
				}
			}
			if (!string.IsNullOrEmpty(text4))
			{
				array[6] = new SqlConditionInfo("@CustomerName", text4, TypeCode.String);
				array[6].ParamOper = "=";
			}
			if (int5 != -1)
			{
				array[7] = new SqlConditionInfo("@Is_FeeSettled", int5, TypeCode.Int32);
				array[7].ParamOper = "=";
			}
			DataTable page = CommonRegiste.Instance.GetPage(pageCode, int2, int3, out totalCount, out num, array);
			if (page.Rows.Count > 0)
			{
				int count = page.Columns.Count;
				if (page.Columns.Contains("Photo"))
				{
					page.Columns.Add("Base64Photo");
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

		public void GetRegisteCustomerList()
		{
			int @int = base.GetInt("Is_InternatSubscribe", -1);
			int int2 = base.GetInt("IsTeam", -1);
			string a = base.GetString("modelName").Trim().ToLower();
			int int3 = base.GetInt("pageIndex", 1);
			int int4 = base.GetInt("pageSize", 10);
			string text = base.Server.HtmlDecode(base.GetString("BeginExamDate"));
			string text2 = base.Server.HtmlDecode(base.GetString("EndExamDate"));
			string text3 = base.Server.HtmlDecode(base.GetString("IDCard"));
			int totalCount = 0;
			int num = 0;
			if (!string.IsNullOrEmpty(text))
			{
				text += " 00:00:00";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 += " 23:59:59";
			}
			if (a == "regist")
			{
				this.Is_Subscribed = 1;
			}
			if (a == "sign")
			{
				this.Is_Subscribed = 0;
			}
			int int5 = base.GetInt("OnlyMySelf", -1);
			string text4 = Input.URLDecode(base.GetString("CustomerName"));
			int int6 = base.GetInt("Is_FeeSettled", -1);
			string pageCode = "QueryPhysicalExamInfoPagesParamX";
			SqlConditionInfo[] array = new SqlConditionInfo[9];
			if (@int == 1)
			{
				array[8] = new SqlConditionInfo("@Is_InternatSubscribe", @int, TypeCode.Int32);
				array[8].Blur = 4;
			}
			string a2 = base.GetString("dateTypeName").Trim().ToLower();
			if (int5 >= 0)
			{
				if (a2 == "SubScriberOperDate".ToLower())
				{
					array[0] = new SqlConditionInfo("@ID_SubScriber", this.UserID, TypeCode.Int32);
					array[0].Blur = 4;
				}
				else if (a2 == "OperateDate".ToLower())
				{
					array[0] = new SqlConditionInfo("@ID_Operator", this.UserID, TypeCode.Int32);
					array[0].Blur = 4;
				}
			}
			array[1] = new SqlConditionInfo("@IDCard", text3, TypeCode.String);
			array[1].Blur = 3;
			if (this.Is_Subscribed == 1)
			{
				if (int2 != -1)
				{
					array[2] = new SqlConditionInfo("@Is_Subscribed", this.Is_Subscribed, TypeCode.Int32);
					array[2].Blur = 4;
				}
			}
			if (int2 != -1)
			{
				array[3] = new SqlConditionInfo("@Is_Team", int2, TypeCode.Int32);
				array[3].Blur = 4;
			}
			if (string.IsNullOrEmpty(text3))
			{
				if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
				{
					if (a2 == "SubScriberOperDate".ToLower())
					{
						array[4] = new SqlConditionInfo("@SubScriberOperDate", text, TypeCode.DateTime);
						array[4].ParamOper = ">=";
					}
					else if (a2 == "OperateDate".ToLower())
					{
						array[4] = new SqlConditionInfo("@OperateDate", text, TypeCode.DateTime);
						array[4].ParamOper = ">=";
					}
				}
				if (!string.IsNullOrEmpty(text2) && Input.IsDate(text2))
				{
					if (a2 == "SubScriberOperDate".ToLower())
					{
						array[5] = new SqlConditionInfo("@SubScriberOperDate", text2, TypeCode.DateTime);
						array[5].ParamOper = "<=";
					}
					else if (a2 == "OperateDate".ToLower())
					{
						array[5] = new SqlConditionInfo("@OperateDate", text2, TypeCode.DateTime);
						array[5].ParamOper = "<=";
					}
				}
			}
			if (!string.IsNullOrEmpty(text4))
			{
				array[6] = new SqlConditionInfo("@CustomerName", text4, TypeCode.String);
				array[6].ParamOper = "=";
			}
			if (int6 != -1)
			{
				array[7] = new SqlConditionInfo("@Is_FeeSettled", int6, TypeCode.Int32);
				array[7].ParamOper = "=";
			}
			DataTable page = CommonRegiste.Instance.GetPage(pageCode, int3, int4, out totalCount, out num, array);
			if (page.Rows.Count > 0)
			{
				int count = page.Columns.Count;
				if (page.Columns.Contains("Photo"))
				{
					page.Columns.Add("Base64Photo");
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

		public void GetInternatRegisteCustomerList()
		{
			int @int = base.GetInt("Is_InternatSubscribe", -1);
			int int2 = base.GetInt("IsTeam", -1);
			string a = base.GetString("modelName").Trim().ToLower();
			int int3 = base.GetInt("pageIndex", 1);
			int int4 = base.GetInt("pageSize", 10);
			string text = base.Server.HtmlDecode(base.GetString("BeginExamDate"));
			string text2 = base.Server.HtmlDecode(base.GetString("EndExamDate"));
			string text3 = base.Server.HtmlDecode(base.GetString("IDCard"));
			int totalCount = 0;
			int num = 0;
			if (!string.IsNullOrEmpty(text))
			{
				text += " 00:00:00";
			}
			if (!string.IsNullOrEmpty(text2))
			{
				text2 += " 23:59:59";
			}
			if (a == "regist")
			{
				this.Is_Subscribed = 1;
			}
			if (a == "sign")
			{
				this.Is_Subscribed = 0;
			}
			int int5 = base.GetInt("OnlyMySelf", -1);
			string text4 = Input.URLDecode(base.GetString("CustomerName"));
			int int6 = base.GetInt("Is_FeeSettled", -1);
			string pageCode = "QueryPhysicalExamInfoPagesParamX";
			SqlConditionInfo[] array = new SqlConditionInfo[9];
			if (@int == 1)
			{
				array[8] = new SqlConditionInfo("@Is_InternatSubscribe", @int, TypeCode.Int32);
				array[8].Blur = 4;
			}
			string a2 = base.GetString("dateTypeName").Trim().ToLower();
			if (int5 >= 0)
			{
				if (a2 == "SubScriberOperDate".ToLower())
				{
					array[0] = new SqlConditionInfo("@ID_SubScriber", this.UserID, TypeCode.Int32);
					array[0].Blur = 4;
				}
				else if (a2 == "OperateDate".ToLower())
				{
					array[0] = new SqlConditionInfo("@ID_Operator", this.UserID, TypeCode.Int32);
					array[0].Blur = 4;
				}
			}
			if (text3.ToUpper().StartsWith("D"))
			{
				array[1] = new SqlConditionInfo("@CustomerOrderNo", text3, TypeCode.String);
				array[1].Blur = 3;
			}
			else
			{
				array[1] = new SqlConditionInfo("@IDCard", text3, TypeCode.String);
				array[1].Blur = 3;
			}
			if (this.Is_Subscribed == 1)
			{
				if (int2 != -1)
				{
					array[2] = new SqlConditionInfo("@Is_Subscribed", this.Is_Subscribed, TypeCode.Int32);
					array[2].Blur = 4;
				}
			}
			if (int2 != -1)
			{
				array[3] = new SqlConditionInfo("@Is_Team", int2, TypeCode.Int32);
				array[3].Blur = 4;
			}
			if (string.IsNullOrEmpty(text3))
			{
				if (!string.IsNullOrEmpty(text) && Input.IsDate(text))
				{
					if (a2 == "SubScriberOperDate".ToLower())
					{
						array[4] = new SqlConditionInfo("@SubScriberOperDate", text, TypeCode.DateTime);
						array[4].ParamOper = ">=";
					}
					else if (a2 == "OperateDate".ToLower())
					{
						array[4] = new SqlConditionInfo("@OperateDate", text, TypeCode.DateTime);
						array[4].ParamOper = ">=";
					}
				}
				if (!string.IsNullOrEmpty(text2) && Input.IsDate(text2))
				{
					if (a2 == "SubScriberOperDate".ToLower())
					{
						array[5] = new SqlConditionInfo("@SubScriberOperDate", text2, TypeCode.DateTime);
						array[5].ParamOper = "<=";
					}
					else if (a2 == "OperateDate".ToLower())
					{
						array[5] = new SqlConditionInfo("@OperateDate", text2, TypeCode.DateTime);
						array[5].ParamOper = "<=";
					}
				}
			}
			if (!string.IsNullOrEmpty(text4))
			{
				array[6] = new SqlConditionInfo("@CustomerName", text4, TypeCode.String);
				array[6].ParamOper = "=";
			}
			if (int6 != -1)
			{
				array[7] = new SqlConditionInfo("@Is_FeeSettled", int6, TypeCode.Int32);
				array[7].ParamOper = "=";
			}
			DataTable page = CommonRegiste.Instance.GetPage(pageCode, int3, int4, out totalCount, out num, array);
			if (page.Rows.Count > 0)
			{
				int count = page.Columns.Count;
				if (page.Columns.Contains("Photo"))
				{
					page.Columns.Add("Base64Photo");
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

		public void GetCustomerByIDCard()
		{
			this.BeginDate = DateTime.Now;
			string modelName = base.GetString("modelName").Trim();
			string @string = base.GetString("Key");
			string string2 = base.GetString("KeyValue");
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonOnArcCust.Instance.GetCustomerByIDCard(modelName, @string, string2), "dataList");
			this.OutPutMessage(msg);
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",通过存档号或体检号或一卡通获取客户体检信息和收费项目信息(GetCustomerByIDCard) 检索Key:",
				@string,
				" 检索值:",
				string2,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void GetCustomerExamPhysicalInfo()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("ID_ArcCustomer").Trim();
			string text2 = base.GetString("ID_Customer").Trim();
			int @int = base.GetInt("IsTeam", -1);
			if (text != string.Empty)
			{
				string msg = string.Empty;
				if (@int == -1)
				{
					msg = JsonHelperFont.Instance.DataSetToJSON(CommonOnArcCust.Instance.GetCustomerExamPhysicalInfo(text));
				}
				else
				{
					msg = JsonHelperFont.Instance.DataSetToJSON(CommonOnArcCust.Instance.GetCustomerExamPhysicalInfo(text, @int));
				}
				this.OutPutMessage(msg);
			}
			else if (text2 != string.Empty)
			{
				string msg = JsonHelperFont.Instance.DataSetToJSON(CommonOnArcCust.Instance.GetCustomerExamPhysicalInfoByID_Customer(text2));
				this.OutPutMessage(msg);
			}
			Log4J.Instance.Error(string.Concat(new object[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",通过存档号和团体标识或体检号获取客户体检信息和收费项目信息(GetCustomerExamPhysicalInfo) 存档编号:",
				text,
				" 体检号:",
				text2,
				" 是否团体:",
				@int,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void GetCustomerByIDCardX()
		{
			this.BeginDate = DateTime.Now;
			int @int = base.GetInt("IsGenerateCustomerCard", 0);
			string modelName = base.GetString("modelName").Trim();
			string @string = base.GetString("Key");
			string string2 = base.GetString("KeyValue");
			string text = Input.URLDecode(base.GetString("CustomerName"));
			DataTable customerByIDCardX = CommonOnArcCust.Instance.GetCustomerByIDCardX(modelName, @string, string2, text, @int);
			if (customerByIDCardX.Rows.Count > 0)
			{
				int count = customerByIDCardX.Columns.Count;
				if (customerByIDCardX.Columns.Contains("Photo"))
				{
					customerByIDCardX.Columns.Add("Base64Photo");
					customerByIDCardX.Columns.Add("FilePath");
					foreach (DataRow dataRow in customerByIDCardX.Rows)
					{
						if (!Convert.IsDBNull(dataRow["Photo"]))
						{
							dataRow["Base64Photo"] = Convert.ToBase64String((byte[])dataRow["Photo"]);
						}
					}
				}
			}
			string msg = JsonHelperFont.Instance.DataTableToJSON(customerByIDCardX, "dataList");
			this.OutPutMessage(msg);
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",通过存档号或一卡通或体检号获取客户基本信息(GetCustomerByIDCardX) 检索Key:",
				@string,
				" 检索值:",
				string2,
				" 客户名称:",
				text,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		private void GetCustomerByIDCardX(string FilePath)
		{
			this.BeginDate = DateTime.Now;
			int @int = base.GetInt("IsGenerateCustomerCard", 0);
			string modelName = base.GetString("modelName").Trim();
			string @string = base.GetString("Key");
			string string2 = base.GetString("KeyValue");
			string string3 = base.GetString("CustomerName");
			DataTable customerByIDCardX = CommonOnArcCust.Instance.GetCustomerByIDCardX(modelName, @string, string2, string3, @int);
			string msg = string.Empty;
			if (customerByIDCardX.Rows.Count > 0)
			{
				int count = customerByIDCardX.Columns.Count;
				if (customerByIDCardX.Columns.Contains("Photo"))
				{
					customerByIDCardX.Columns.Add("Base64Photo");
					customerByIDCardX.Columns.Add("FilePath");
					foreach (DataRow dataRow in customerByIDCardX.Rows)
					{
						if (!Convert.IsDBNull(dataRow["Photo"]))
						{
							dataRow["Base64Photo"] = Convert.ToBase64String((byte[])dataRow["Photo"]);
							dataRow["FilePath"] = FilePath;
						}
					}
				}
				msg = JsonHelperFont.Instance.DataTableToJSON(customerByIDCardX, "dataList");
			}
			else
			{
				msg = string.Concat(new string[]
				{
					"{\"success\":\"1\",\"Message\":\"未获取到用户信息\",\"FilePath\":\"",
					FilePath,
					"\",\"IDCard\":\"",
					string2,
					"\",\"CustomerName\":\"",
					string3,
					"\"}"
				});
			}
			this.OutPutMessage(msg);
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",通过存档号或一卡通或体检号获取客户基本信息(GetCustomerByIDCardX) 检索Key:",
				@string,
				" 检索值:",
				string2,
				" 客户名称:",
				string3,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void GetCustomerByIDCustomer()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("modelName").Trim();
			string @string = base.GetString("Key");
			string string2 = base.GetString("KeyValue");
			string string3 = base.GetString("ID_Customer");
			DataSet dataSet = null;
			DataTable dataTable = null;
			string sql = string.Format("SELECT ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,[dbo].StrToCode128(ID_Customer) ID_CustomerCode128,Is_CompletePhysical,ExamState FROM OnCustRelationCustPEInfo WHERE ID_Customer='{0}';", string3);
			DataTable dataTable2 = CommonExcuteSql.Instance.ExcuteSql(sql).Tables[0];
			int count = dataTable2.Rows.Count;
			string text2 = string.Empty;
			int num = -1;
			if (count > 0)
			{
				int.TryParse(dataTable2.Rows[0]["ExamState"].ToString(), out num);
			}
			if (num == 0)
			{
				text2 = "ConnectionString";
			}
			else
			{
				if (num == -1)
				{
				}
				if (num == 1)
				{
					text2 = "ToOffLineConnectionString";
				}
				else
				{
					text2 = "FYH_HisFile" + (num - 1).ToString().PadLeft(3, '0');
				}
			}
			if (!string.IsNullOrEmpty(text2) && count > 0)
			{
				dataSet = CommonOnArcCust.Instance.GetCustomerByIDCustomer(text2, string3);
				dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					int count2 = dataTable.Columns.Count;
					if (dataTable.Columns.Contains("Photo"))
					{
						dataTable.Columns.Add("Base64Photo");
						dataTable.Columns.Add("FilePath");
						if (!Convert.IsDBNull(dataTable.Rows[0]["Photo"]))
						{
							dataTable.Rows[0]["Base64Photo"] = Convert.ToBase64String((byte[])dataTable.Rows[0]["Photo"]);
						}
					}
				}
				string msg = JsonHelperFont.Instance.DataSetToJSON(dataSet);
				this.OutPutMessage(msg);
			}
			if (dataTable2 != null)
			{
				dataTable2.Dispose();
			}
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			if (dataSet != null)
			{
				dataSet.Dispose();
			}
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",通过体检号获取客户体检状态(GetCustomerByIDCustomer) 检索Key:",
				@string,
				" 检索值:",
				string2,
				" 体检号:",
				string3,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void GetCustomerByIDCustomerOfOnline()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("modelName").Trim();
			string @string = base.GetString("Key");
			string a = base.GetString("IsLoadCustomerInfo").Trim();
			string string2 = base.GetString("KeyValue");
			string string3 = base.GetString("ID_Customer");
			DataSet dataSet = null;
			DataTable dataTable = null;
			string sql = string.Format("SELECT ID_ArcCustomer,IDCardNo,ExamCardNo,ID_Customer,[dbo].StrToCode128(ID_Customer) ID_CustomerCode128,Is_CompletePhysical,ExamState FROM OnCustRelationCustPEInfo WHERE ID_Customer='{0}';", string3);
			DataTable dataTable2 = CommonExcuteSql.Instance.ExcuteSql(sql).Tables[0];
			int count = dataTable2.Rows.Count;
			string text2 = string.Empty;
			int num = -1;
			if (count > 0)
			{
				int.TryParse(dataTable2.Rows[0]["ExamState"].ToString(), out num);
			}
			if (num == 0)
			{
				text2 = "ConnectionString";
			}
			else
			{
				if (num == -1)
				{
				}
				if (num == 1)
				{
					text2 = "ToOffLineConnectionString";
				}
				else
				{
					text2 = "FYH_HisFile" + (num - 1).ToString().PadLeft(3, '0');
				}
				if (a != "1")
				{
					string msg = string.Concat(new object[]
					{
						"{\"success\":\"0\",\"Message\":\"数据已归档\",\"ID_Customer\":\"",
						string3,
						"\",\"IDCard\":\"",
						string2,
						"\",\"ExamState\":\"",
						num,
						"\",\"AppSettingKey\":\"",
						text2,
						"\"}"
					});
					this.OutPutMessage(msg);
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",通过体检号获取客户体检状态(GetCustomerByIDCustomer) 检索Key:",
						@string,
						" 检索值:",
						string2,
						" 体检号:",
						string3,
						" ",
						Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now),
						",该数据已归档不能进行改签，位置[",
						text2,
						"]"
					}));
					return;
				}
			}
			if (!string.IsNullOrEmpty(text2) && count > 0)
			{
				dataSet = CommonOnArcCust.Instance.GetCustomerByIDCustomer(text2, string3);
				dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					if (!dataTable.Columns.Contains("ExamState"))
					{
						dataTable.Columns.Add("ExamState");
					}
					dataTable.Rows[0]["ExamState"] = num;
					int count2 = dataTable.Columns.Count;
					if (dataTable.Columns.Contains("Photo"))
					{
						dataTable.Columns.Add("Base64Photo");
						dataTable.Columns.Add("FilePath");
						if (!Convert.IsDBNull(dataTable.Rows[0]["Photo"]))
						{
							dataTable.Rows[0]["Base64Photo"] = Convert.ToBase64String((byte[])dataTable.Rows[0]["Photo"]);
						}
					}
				}
				string msg = JsonHelperFont.Instance.DataSetToJSON(dataSet);
				this.OutPutMessage(msg);
			}
			if (dataTable2 != null)
			{
				dataTable2.Dispose();
			}
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			if (dataSet != null)
			{
				dataSet.Dispose();
			}
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",通过体检号获取客户体检状态(GetCustomerByIDCustomer) 检索Key:",
				@string,
				" 检索值:",
				string2,
				" 体检号:",
				string3,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void SaveCustomerInfo()
		{
			this.BeginDate = DateTime.Now;
			int @int = base.GetInt("IsUpdateUserInfo", -1);
			int int2 = base.GetInt("IsLoadCustomer", -1);
			if (@int > 0)
			{
				string @string = base.GetString("IDCard");
				string string2 = base.GetString("CustomerName");
				string string3 = base.GetString("Gender");
				string string4 = base.GetString("GenderName");
				string string5 = base.GetString("BirthDay");
				int int3 = base.GetInt("Nation", -1);
				string string6 = base.GetString("NationName");
				string string7 = base.GetString("Base64Photo");
				byte[] array = Convert.FromBase64String(string7);
				MemoryStream memoryStream = new MemoryStream(array);
				string empty = string.Empty;
				PEIS.Model.OnArcCust onArcCust = new PEIS.Model.OnArcCust();
				onArcCust.IDCard = @string;
				onArcCust.CustomerName = string2;
				onArcCust.ID_Gender = new int?(int.Parse(string3));
				onArcCust.GenderName = string4;
				onArcCust.BirthDay = new DateTime?(DateTime.Parse(string5));
				onArcCust.NationID = new int?(int3);
				onArcCust.NationName = string6;
				onArcCust.Photo = array;
				int num = CommonUser.Instance.AddCustomerArcInfo(onArcCust);
				if (num > 0)
				{
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",保存客户存档信息 姓名：",
						string2,
						",身份证号:",
						@string
					}));
				}
				else
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",保存客户存档信息失败 姓名：",
						string2,
						",身份证号:",
						@string
					}));
				}
				memoryStream.Close();
				memoryStream.Dispose();
			}
			if (int2 > 0)
			{
				this.GetCustomerByIDCardX();
			}
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",保存客户基本信息包含头像信息(SaveCustomerInfo) ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void Charge()
		{
			string text = base.GetString("AllFeeID").TrimEnd(new char[]
			{
				','
			});
			string text2 = base.GetString("ID_Customer").TrimEnd(new char[]
			{
				','
			});
			string text3 = base.GetString("Invoice").TrimEnd(new char[]
			{
				','
			});
			int num = CommonOnArcCust.Instance.Charge(this.UserID, this.UserName, text2, text, text3);
			CommonOnArcCust.Instance.SumSectionExamInfo(text2);
			base.ClearCache_CustRelationCustPEInfo(text2);
			if (num <= 0)
			{
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",客户缴费失败 体检号：",
					text2,
					",收费项目:",
					text,
					",发票号:",
					text3
				}));
				string msg = "{\"success\":\"0\",\"Message\":\"缴费失败，请重试！\"}";
				this.OutPutMessage(msg);
			}
			else
			{
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",客户缴费 体检号：",
					text2,
					",收费项目:",
					text,
					",发票号:",
					text3
				}));
				string msg = "{\"success\":\"1\",\"Message\":\"缴费成功！\"}";
				this.OutPutMessage(msg);
				string sql = string.Format("SELECT ID_Customer,Is_Subscribed,ID_SubScriber,SubScriber,SubScriberOperDate,SubScribDate,ID_Operator,Operator,OperateDate,ISNULL(Is_FeeSettled,0) Is_FeeSettled,ISNULL(Is_GuideSheetPrinted,0) Is_GuideSheetPrinted,ISNULL(Is_Team,0) Is_Team FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}';", text2);
				DataSet dataSet = CommonExcuteSql.Instance.ExcuteSql(sql);
				DataTable dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					base.AddOrUpdateByBackLogType(long.Parse(text2.ToString()), EnumBusBackLogType.缴费, true, null);
				}
			}
		}

		public void UnCharge()
		{
			int @int = base.GetInt("Forsex", 2);
			string text = base.GetString("AllFeeID").TrimEnd(new char[]
			{
				','
			});
			string text2 = base.GetString("ID_Customer").TrimEnd(new char[]
			{
				','
			});
			int num = CommonOnArcCust.Instance.UnCharge(this.UserID, this.UserName, text2, text);
			CommonOnArcCust.Instance.SumSectionExamInfo(text2);
			base.ClearCache_CustRelationCustPEInfo(text2);
			if (num <= 0)
			{
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",客户退费失败 体检号：",
					text2,
					",AllFeeID:",
					text
				}));
				string msg = "{\"success\":\"0\",\"Message\":\"退费失败，请重试！\"}";
				this.OutPutMessage(msg);
			}
			else
			{
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",客户退费 体检号：",
					text2,
					",AllFeeID:",
					text
				}));
				string msg = "{\"success\":\"1\",\"Message\":\"退费成功！\"}";
				this.OutPutMessage(msg);
				string sql = string.Format("SELECT ID_Customer,Is_Subscribed,ID_SubScriber,SubScriber,SubScriberOperDate,SubScribDate,ID_Operator,Operator,OperateDate,ISNULL(Is_FeeSettled,0) Is_FeeSettled,ISNULL(Is_GuideSheetPrinted,0) Is_GuideSheetPrinted,ISNULL(Is_Team,0) Is_Team FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}';", text2);
				DataSet dataSet = CommonExcuteSql.Instance.ExcuteSql(sql);
				DataTable dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					DateTime now = DateTime.Now;
					string value = dataTable.Rows[0]["Is_FeeSettled"].ToString();
					PEIS.Model.OnCustBackLog onCustBackLog = new PEIS.Model.OnCustBackLog();
					onCustBackLog.ID_Customer = long.Parse(text2.ToString());
					onCustBackLog.ID_BackLogType = 4;
					onCustBackLog.CreateDate = now;
					onCustBackLog.OperateDate = now;
					onCustBackLog.Is_Finished = new bool?(bool.Parse(value));
					onCustBackLog.ID_Operator = int.Parse(this.LoginUserModel.UserID.ToString());
					onCustBackLog.Operator = this.LoginUserModel.UserName;
					if (onCustBackLog != null)
					{
						try
						{
							int num2 = new PEIS.BLL.OnCustBackLog().AddOrUpdateByBackLogType(onCustBackLog);
							Log4J.Instance.Info(string.Concat(new string[]
							{
								Public.GetClientIP(),
								",体检号：",
								text2,
								",收费人：",
								this.LoginUserModel.UserName,
								",成功写入积案日志记录"
							}));
						}
						catch (Exception ex)
						{
							Log4J.Instance.Info(string.Concat(new string[]
							{
								Public.GetClientIP(),
								",体检号：",
								text2,
								",收费人：",
								this.LoginUserModel.UserName,
								",写入积案日志记录失败,失败原因:",
								ex.Message
							}));
						}
					}
				}
			}
		}

		public void UnChargeInvoice()
		{
			int @int = base.GetInt("Forsex", 2);
			string text = base.GetString("AllFeeID").TrimEnd(new char[]
			{
				','
			});
			string text2 = base.GetString("ID_Customer").TrimEnd(new char[]
			{
				','
			});
			string text3 = base.GetString("Invoice").TrimEnd(new char[]
			{
				','
			});
			int int2 = base.GetInt("UnChargeType", -1);
			int num = CommonOnArcCust.Instance.UnCharge(int2, text3, this.UserID, this.UserName, text2, text);
			CommonOnArcCust.Instance.SumSectionExamInfo(text2);
			base.ClearCache_CustRelationCustPEInfo(text2);
			if (num <= 0)
			{
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",客户退费失败 体检号：",
					text2,
					",AllFeeID:",
					text,
					",Invoice:",
					text3
				}));
				string msg = "{\"success\":\"0\",\"Message\":\"退费失败，请重试！\"}";
				this.OutPutMessage(msg);
			}
			else
			{
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",客户退费 体检号：",
					text2,
					",AllFeeID:",
					text,
					",Invoice:",
					text3
				}));
				string msg = "{\"success\":\"1\",\"Message\":\"退费成功！\"}";
				this.OutPutMessage(msg);
			}
		}

		public void LoseInvoiceCharge()
		{
			string text = base.GetString("ID_Customer").TrimEnd(new char[]
			{
				','
			});
			string text2 = base.GetString("Invoice").TrimEnd(new char[]
			{
				','
			});
			int num = CommonOnArcCust.Instance.LoseInvoiceCharge(text, text2);
			CommonOnArcCust.Instance.SumSectionExamInfo(text);
			base.ClearCache_CustRelationCustPEInfo(text);
			if (num <= 0)
			{
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",客户退费失败 体检号：",
					text,
					",Invoice:",
					text2
				}));
				string msg = "{\"success\":\"0\",\"Message\":\"收费发票补录失败，请重试！\"}";
				this.OutPutMessage(msg);
			}
			else
			{
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",客户收费发票补录 体检号：",
					text,
					",Invoice:",
					text2
				}));
				string msg = "{\"success\":\"1\",\"Message\":\"收费发票补录成功！\"}";
				this.OutPutMessage(msg);
			}
		}

		public void GetCustomerUnionBusFee()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("type").ToLower();
			string text2 = base.GetString("ID_Team").Trim();
			string text3 = base.GetString("ID_TeamTask").Trim();
			string text4 = base.GetString("ID_TeamTaskGroupS").Trim();
			string text5 = base.GetString("ID_Customers").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonOnArcCust.Instance.GetCustomerUnionBusFee(this.UserName, text2, text3, text4, text5), "dataList");
			this.OutPutMessage(msg);
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",获取团体客户共用收费项目(GetCustomerUnionBusFee) 体检号:",
				text5,
				" 团体编号:",
				text2,
				" 任务编号:",
				text3,
				" 分组编号:",
				text4,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void SearchCustomerUnionBusFee()
		{
			string text = base.GetString("type").ToLower();
			string iD_Team = base.GetString("ID_Team").Trim();
			string iD_TeamTask = base.GetString("ID_TeamTask").Trim();
			string iD_TeamTaskGroup = base.GetString("ID_TeamTaskGroupS").Trim();
			string iD_CustomerS = base.GetString("ID_Customers").TrimEnd(new char[]
			{
				','
			});
			string inputCode = base.GetString("InputCode").TrimEnd(new char[]
			{
				','
			});
			string selectedFee = base.GetString("SelectedFee").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonOnArcCust.Instance.SearchCustomerUnionBusFee(this.UserName, iD_Team, iD_TeamTask, iD_TeamTaskGroup, iD_CustomerS, inputCode, selectedFee), "dataList");
			this.OutPutMessage(msg);
		}

		public void GetCustomerNumber()
		{
			long @int = base.GetInt64("ID_Customer", 0L);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Customer", @int, TypeCode.Int64)
			};
			int num = 0;
			string text = "0";
			string text2 = "0";
			string querySqlCode = "CustomerInfoByID_Param";
			try
			{
				DataSet dataSet = CommonCustExam.Instance.ExcuteQuerySql(querySqlCode, conditions);
				DataTable dataTable = dataSet.Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					num = dataTable.Rows.Count;
					text2 = dataTable.Rows[0]["Is_Subscribed"].ToString();
					text = dataTable.Rows[0]["SecurityLevel"].ToString();
				}
				else
				{
					num = 0;
				}
			}
			catch (Exception var_8_CD)
			{
			}
			string msg = string.Concat(new object[]
			{
				"{\"nret\":\"",
				num,
				"\",\"SecurityLevel\":\"",
				text,
				"\",\"Is_Subscribed\":\"",
				text2,
				"\"}"
			});
			this.OutPutMessage(msg);
		}

		public void GetCustomerPrint()
		{
			string text = base.GetString("ID_Customer").Trim();
			if (text != string.Empty)
			{
				string msg = JsonHelperFont.Instance.DataTableToJSON(CommonOnArcCust.Instance.GetCustomerPrint(text).Tables[0], "dataList");
				this.OutPutMessage(msg);
			}
		}

		public void UpdateCustomerSubScribDate()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("CustomerName").Trim();
			string text2 = base.GetString("ID_Customer").Trim();
			string text3 = base.GetString("SubScribDate").Trim();
			string item = string.Format("UPDATE OnCustPhysicalExamInfo SET SubScribDate='{1}' WHERE ID_Customer='{0}';", text2, text3);
			List<string> list = new List<string>(0);
			list.Add(item);
			int num = CommonExcuteSql.Instance.ExecuteSqlTran(list);
			string msg = string.Empty;
			if (num > 0)
			{
				msg = "{\"success\":\"1\",\"Message\":\"修改成功\"}";
			}
			else
			{
				msg = "{\"success\":\"0\",\"Message\":\"修改失败\"}";
			}
			this.OutPutMessage(msg);
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",修改客户体检日期(UpdateCustomerSubScribDate) 体检号:",
				text2,
				" 客户名称:",
				text,
				" 体检日期:",
				text3,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void UpdateCustomerSubscribDateOfTeam()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("ID_Customer").Trim();
			string text2 = base.GetString("SubScribDate").Trim();
			int num = CommonOnArcCust.Instance.UpdateCustomerSubscribDateOfTeam(text, text2);
			string msg = string.Empty;
			if (num > 0)
			{
				msg = "{\"success\":\"1\",\"Message\":\"修改成功\"}";
			}
			else
			{
				msg = "{\"success\":\"0\",\"Message\":\"修改失败\"}";
			}
			this.OutPutMessage(msg);
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",修改团体体检日期(UpdateCustomerSubscribDateOfTeam) 体检号:",
				text,
				" 体检日期:",
				text2,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void SearchCloneCustomer()
		{
			string @string = base.GetString("ID_Customer");
			string string2 = base.GetString("CustomerName");
			string string3 = base.GetString("GenderName");
			string string4 = base.GetString("BirthDay");
			string string5 = base.GetString("IDCard");
			string text = string.Empty;
			if (string.IsNullOrEmpty(@string))
			{
				text = string.Format("SELECT * INTO #TempOnArcCust_{0} FROM OnArcCust WHERE 1=1 @WHERE; \r\nSELECT CRPE.ID_ArcCustomer,CRPE.ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) BirthDay,IDCard,BirthDay,Photo FROM OnCustRelationCustPEInfo CRPE\r\nINNER JOIN #TempOnArcCust_{0}\r\nON CRPE.ID_ArcCustomer=#TempOnArcCust_{0}.ID_ArcCustomer;\r\nDROP TABLE #TempOnArcCust_{0};", Public.GetGuid("-", string.Empty));
				string text2 = string.Empty;
				if (!string.IsNullOrEmpty(string2))
				{
					text2 += string.Format(" AND CustomerName LIKE '%{0}%'", string2);
				}
				if (!string.IsNullOrEmpty(string3))
				{
					text2 += string.Format(" AND GenderName='{0}'", string3);
				}
				if (!string.IsNullOrEmpty(string4))
				{
					text2 += string.Format(" AND BirthDay='{0}'", string4);
				}
				if (!string.IsNullOrEmpty(string5))
				{
					text2 += string.Format(" AND IDCard LIKE '%{0}%'", string5);
				}
				text = text.Replace("@WHERE", text2);
			}
			else
			{
				text = string.Format("SELECT ID_ArcCustomer,'{0}' ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) BirthDay,IDCard,BirthDay,Photo FROM OnArcCust \r\nWHERE EXISTS(SELECT ID_ArcCustomer FROM OnCustRelationCustPEInfo WHERE ID_Customer='{0}' \r\nAND OnArcCust.ID_ArcCustomer=OnCustRelationCustPEInfo.ID_ArcCustomer);", @string);
			}
			DataTable dataTable = CommonExcuteSql.Instance.ExcuteSql(text).Tables[0];
			if (dataTable.Rows.Count > 0)
			{
				int count = dataTable.Columns.Count;
				if (dataTable.Columns.Contains("Photo"))
				{
					dataTable.Columns.Add("Base64Photo");
					dataTable.Columns.Add("FilePath");
					foreach (DataRow dataRow in dataTable.Rows)
					{
						if (!Convert.IsDBNull(dataRow["Photo"]))
						{
							dataRow["Base64Photo"] = Convert.ToBase64String((byte[])dataRow["Photo"]);
						}
					}
				}
			}
			string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
			this.OutPutMessage(msg);
		}

		public void GetCustomerQueue()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("ID_Customer");
			if (!string.IsNullOrEmpty(text))
			{
				text = text.TrimEnd(new char[]
				{
					','
				});
				string sql = string.Format("SELECT * INTO #TempOnCustRelationCustPEInfo_{1} FROM OnCustRelationCustPEInfo WHERE ID_Customer IN({0}); \r\nSELECT CRPE.ID_ArcCustomer,CRPE.ID_Customer,CustomerName,GenderName,CONVERT(varchar(10),BirthDay,120) BirthDay,IDCard,BirthDay,Photo FROM \r\n#TempOnCustRelationCustPEInfo_{1} CRPE\r\nINNER JOIN (SELECT * FROM OnArcCust WHERE EXISTS(SELECT ID_ArcCustomer FROM #TempOnCustRelationCustPEInfo_{1} WHERE OnArcCust.ID_ArcCustomer=#TempOnCustRelationCustPEInfo_{1}.ID_ArcCustomer)) TempOnArcCust\r\nON CRPE.ID_ArcCustomer=TempOnArcCust.ID_ArcCustomer ORDER BY CRPE.ID_CustRelation DESC;\r\nDROP TABLE #TempOnCustRelationCustPEInfo_{1};", text, Public.GetGuid("-", string.Empty));
				DataTable dataTable = CommonExcuteSql.Instance.ExcuteSql(sql).Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					int count = dataTable.Columns.Count;
					if (dataTable.Columns.Contains("Photo"))
					{
						dataTable.Columns.Add("Base64Photo");
						dataTable.Columns.Add("FilePath");
						foreach (DataRow dataRow in dataTable.Rows)
						{
							if (!Convert.IsDBNull(dataRow["Photo"]))
							{
								dataRow["Base64Photo"] = Convert.ToBase64String((byte[])dataRow["Photo"]);
							}
						}
					}
				}
				string msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
			}
			Log4J.Instance.Error(string.Concat(new string[]
			{
				Public.GetClientIP(),
				",",
				this.LoginUserModel.UserName,
				",获取克隆数据源(GetCustomerQueue) 体检号:",
				text,
				" ",
				Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
			}));
		}

		public void UpdateCustFeePrintFlag()
		{
			this.BeginDate = DateTime.Now;
			string text = base.GetString("ID_Customer").Trim();
			string text2 = base.GetString("AllIDFee").Trim();
			text2 = text2.TrimEnd(new char[]
			{
				','
			});
			if (!string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				string item = string.Format("UPDATE OnCustFee SET Is_Printed=2 WHERE ID_Customer='{0}' AND ID_Fee IN({1});", text, text2);
				List<string> list = new List<string>();
				list.Add(item);
				int num = CommonExcuteSql.Instance.ExecuteSqlTran(list);
				string msg = string.Empty;
				if (num > 0)
				{
					msg = "{\"success\":\"1\",\"Message\":\"修改收费项目打印标记成功\"}";
				}
				else
				{
					msg = "{\"success\":\"0\",\"Message\":\"修改收费项目打印标记失败\"}";
				}
				this.OutPutMessage(msg);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",修改收费项目打印标记(UpdateCustFeePrintFlag) 体检号:",
					text,
					" 收费项目编码:",
					text2.Replace("'", string.Empty),
					" ",
					Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
				}));
			}
		}

		public void UnlockReportState()
		{
			int @int = base.GetInt("ReportReceipted", -1);
			string msg = string.Empty;
			string item = string.Empty;
			string text = base.GetString("ID_Customer").Trim();
			if (string.IsNullOrEmpty(text))
			{
				msg = "{\"success\":\"-1\",\"Message\":\"未获取到体检号\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",修改报告领取标志(UnlockReportState)失败 失败原因：未获取到体检号 ",
					Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
				}));
			}
			else if (@int == -1)
			{
				msg = "{\"success\":\"-1\",\"Message\":\"未获取到修改标志\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",修改报告领取标志(UnlockReportState)失败 失败原因：未获取到修改标志 体检号:",
					text,
					" ",
					Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
				}));
			}
			else
			{
				item = string.Format("UPDATE OnCustReportManage SET Is_ReportReceipted=0 WHERE ID_Customer='{0}';UPDATE OnCustPhysicalExamInfo SET Is_ReportReceipted='{1}' WHERE ID_Customer='{0}';\r\nUPDATE OnArcCust SET UnfinishedNum=ISNULL(UnfinishedNum,0)+1,FinishedNum=ISNULL(FinishedNum,0)-1 WHERE ID_ArcCustomer=(SELECT ID_ArcCustomer FROM OnCustRelationCustPEInfo WHERE ID_Customer='{0}');", text, @int);
				List<string> list = new List<string>();
				list.Add(item);
				try
				{
					int num = CommonExcuteSql.Instance.ExecuteSqlTran(list);
					if (num > 0)
					{
						msg = "{\"success\":\"1\",\"Message\":\"成功修改报告领取标志\"}";
						Log4J.Instance.Error(string.Concat(new object[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",修改报告领取标志(UnlockReportState)成功 体检号:",
							text,
							" 修改标志:",
							@int,
							" ",
							Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
						}));
					}
					else
					{
						msg = "{\"success\":\"0\",\"Message\":\"修改报告领取标志失败 失败原因:受影响的行数为0\"}";
						Log4J.Instance.Error(string.Concat(new object[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",修改报告领取标志(UnlockReportState)失败 失败原因:受影响的行数为0 体检号:",
							text,
							" 修改标志:",
							@int,
							" ",
							Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
						}));
					}
				}
				catch (Exception ex)
				{
					msg = "{\"success\":\"0\",\"Message\":\"修改报告领取标志失败 失败原因:" + ex.Message + "\"}";
					Log4J.Instance.Error(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",修改报告领取标志(UnlockReportState)失败 失败原因:",
						ex.Message,
						" 体检号:",
						text,
						" 修改标志:",
						@int,
						" ",
						Public.GetDateDiff(string.Empty, this.BeginDate, DateTime.Now)
					}));
				}
			}
			this.OutPutMessage(msg);
		}

		public void GetCustomerInfo()
		{
			string arg = base.GetString("ID_Customer").Trim();
			string sql = string.Format("SELECT ISNULL(Is_Team,0) Is_Team,ID_Team FROM OnCustPhysicalExamInfo WHERE ID_Customer='{0}';", arg);
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonExcuteSql.Instance.ExcuteSql(sql).Tables[0], "dataList");
			this.OutPutMessage(msg);
		}

		public void GetCustPhysicalExamInfoByIDCard()
		{
			string msg = string.Empty;
			int @int = base.GetInt("Is_Team", -1);
			string text = base.GetString("IDCard").Trim();
			string text2 = base.GetString("CustomerName").Trim();
			if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2))
			{
				msg = "{\"success\":\"0\",\"Message\":\"未获取到证件号码和客户名称！\"}";
				this.OutPutMessage(msg);
			}
			else
			{
				string text3 = string.Empty;
				string text4 = string.Empty;
				text3 = string.Format("SELECT OCPE.*,TTG.RoleName FROM(SELECT ID_Customer,Photo,IDCard,CustomerName,ID_Gender,GenderName,TeamName,Department,DepartSubA,DepartSubB,DepartSubC FROM OnCustPhysicalExamInfo \r\nWHERE ISNULL(Is_GuideSheetPrinted,0)=0 @whereStr)OCPE\r\nLEFT JOIN TeamTaskGroupCust TTGC ON OCPE.ID_Customer=TTGC.ID_Customer\r\nLEFT JOIN TeamTaskGroup TTG ON TTGC.ID_TeamTaskGroup=TTG.ID_TeamTaskGroup", new object[0]);
				if (@int > -1)
				{
					object obj = text4;
					text4 = string.Concat(new object[]
					{
						obj,
						" AND Is_Team='",
						@int,
						"'"
					});
				}
				if (!string.IsNullOrEmpty(text))
				{
					text4 = text4 + " AND IDCard='" + text + "'";
				}
				if (!string.IsNullOrEmpty(text2))
				{
					text4 = text4 + " AND CustomerName='" + text2 + "'";
				}
				text3 = text3.Replace("@whereStr", text4);
				DataTable dataTable = CommonExcuteSql.Instance.ExcuteSql(text3).Tables[0];
				if (dataTable.Rows.Count > 0)
				{
					int count = dataTable.Columns.Count;
					if (dataTable.Columns.Contains("Photo"))
					{
						dataTable.Columns.Add("Base64Photo");
						foreach (DataRow dataRow in dataTable.Rows)
						{
							if (!Convert.IsDBNull(dataRow["Photo"]))
							{
								dataRow["Base64Photo"] = Convert.ToBase64String((byte[])dataRow["Photo"]);
							}
						}
					}
				}
				msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				this.OutPutMessage(msg);
				if (dataTable != null)
				{
					dataTable.Dispose();
				}
			}
		}

		public void GetServerInfo()
		{
			string msg = "{\"ServerTime\":\"" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\",\"Message\":\"成功获取服务信息\"}";
			this.OutPutMessage(msg);
		}

		public string GetRandNum(int MinValue, int MaxValue)
		{
			Random random = new Random();
			int num = 1;
			string text = DateTime.Now.ToString("HHmmss");
			num = random.Next(MinValue, MaxValue);
			try
			{
				if (this.Session.SessionID != string.Empty)
				{
					text = this.Session.SessionID + text;
				}
			}
			catch
			{
			}
			return text + num.ToString();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			this.UserID = this.LoginUserModel.UserID.ToString();
			this.UserName = this.LoginUserModel.UserName;
			this.ErrorMessage = "error";
			string @string = base.GetString("action");
			MethodInfo method = base.GetType().GetMethod(@string);
			try
			{
				method.Invoke(this, null);
			}
			catch (Exception ex)
			{
				this.ErrorMessage = ex.Message;
				this.OutPutMessage(this.ErrorMessage);
			}
		}
	}
}
