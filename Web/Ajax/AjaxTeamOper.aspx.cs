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
	public class AjaxTeamOper : BasePage
	{
		private string ID_User = string.Empty;

		private new string UserName = string.Empty;

		public string ErrorMessage = string.Empty;

		private string FailMessage = string.Empty;

		private string SuccessMessage = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.ID_User = this.LoginUserModel.UserID.ToString();
			this.UserName = this.LoginUserModel.UserName;
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

		public void SearchBusFee()
		{
			string a = base.GetString("curOper").Trim().ToLower();
			string inputCode = base.GetString("InputCode").Trim();
			string selectedFee = base.GetString("SelectedFee").TrimEnd(new char[]
			{
				','
			});
			if (a == "add")
			{
				this.OutPutMessage(JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.SearchBusFee(this.UserName, inputCode, selectedFee), "dataList"));
			}
			else if (a == "delete")
			{
				string iD_Team = base.GetString("ID_Team").Trim();
				string iD_TeamTask = base.GetString("ID_TeamTask").Trim();
				string text = base.GetString("ID_TeamTaskGroupS").Trim();
				string iD_CustomerS = base.GetString("ID_Customers").TrimEnd(new char[]
				{
					','
				});
				this.OutPutMessage(JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.SearchUnionCustomerBusFee(iD_Team, iD_TeamTask, iD_CustomerS, this.UserName, inputCode, selectedFee), "dataList"));
			}
		}

		public void SaveData()
		{
			string iD_Team = base.GetString("ID_Team").Trim();
			string text = base.GetString("TeamName").Trim();
			string iD_Creator = base.GetString("ID_Creator").Trim();
			string creator = base.GetString("Creator").Trim();
			string createDate = base.GetString("CreateDate").Trim();
			string text2 = base.GetString("InputCode").Trim();
			text2 = ((text2 == string.Empty) ? Input.GetStringSpellCode(text) : text2);
			text2 = ((text2.Length > 10) ? text2.Substring(0, 10) : text2);
			string note = base.GetString("Note").Trim();
			DataTable dataTable = CommonTeam.Instance.SaveTeamData(iD_Team, text, iD_Creator, creator, createDate, text2, note);
			int count = dataTable.Rows.Count;
			string a = string.Empty;
			string text3 = "******";
			if (count == 0)
			{
				this.FailMessage = "新增失败，请您重试！";
				this.SuccessMessage = "新增成功！";
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",修改团体信息失败 团体名称:",
					text,
					" ,团体编号：",
					text3
				}));
			}
			else
			{
				base.ClearCache_AllTeam();
				a = dataTable.Rows[0]["Result"].ToString();
				text3 = dataTable.Rows[0]["ID_Team"].ToString();
				if (a == "0")
				{
					this.FailMessage = "新增失败，请您重试！";
					this.SuccessMessage = "新增成功！";
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增团体信息成功 团体名称:",
						text,
						" ,团体编号：",
						text3
					}));
				}
				else if (a == "1")
				{
					this.FailMessage = "修改失败，请您重试！";
					this.SuccessMessage = "修改成功！";
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",修改团体信息成功 团体名称:",
						text,
						" ,团体编号：",
						text3
					}));
				}
			}
			string msg = string.Concat(new string[]
			{
				"{\"success\":\"1\",\"Message\":\"",
				this.SuccessMessage,
				"\",\"ID_Team\":\"",
				text3,
				"\",\"InputCode\":\"",
				text2,
				"\"}"
			});
			this.OutPutMessage(msg);
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
		}

		public void GetTeamList()
		{
			string text = base.GetString("ID_Team").Trim();
			string paramvalue = base.GetString("TeamName").Trim();
			int @int = base.GetInt("pageIndex", 1);
			int int2 = base.GetInt("pageSize", 10);
			int totalCount = 0;
			int num = 0;
			string pageCode = "TeamPagesParam";
			SqlConditionInfo[] array = new SqlConditionInfo[2];
			if (!string.IsNullOrEmpty(text))
			{
				array[0] = new SqlConditionInfo("@ID_Team", text, TypeCode.String);
			}
			else
			{
				array[0] = new SqlConditionInfo("@TeamName", paramvalue, TypeCode.String);
				array[0].Blur = 3;
			}
			DataTable page = CommonUser.Instance.GetPage(pageCode, @int, int2, out totalCount, out num, array);
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		public void ISCanDeleteTeamInfo()
		{
			string msg = string.Empty;
			string text = base.GetString("ID_Team").TrimEnd(new char[]
			{
				','
			});
			DataTable dataTable = null;
			try
			{
				dataTable = CommonTeam.Instance.ISCanDeleteTeamInfo(text);
				if (dataTable == null)
				{
					msg = string.Concat(new string[]
					{
						"{\"success\":\"-1\",\"Message\":\"",
						text,
						"不允许删除\",\"ID_Team\":\"",
						text,
						"\"}"
					});
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取团体是否可以删除失败 失败原因:未获取到任何数据 团体编号:",
						text
					}));
				}
				else
				{
					msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				}
			}
			catch (Exception ex)
			{
				msg = string.Concat(new string[]
				{
					"{\"success\":\"-1\",\"Message\":\"",
					ex.Message,
					"\",\"ID_Team\":\"",
					text,
					"\"}"
				});
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取团体是否可以删除失败 失败原因:",
					ex.Message,
					" 团体编号:",
					text
				}));
			}
			this.OutPutMessage(msg);
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			dataTable = null;
		}

		public void ISCanDeleteTeamTaskGroupFeeInfo()
		{
			string msg = string.Empty;
			string text = base.GetString("TeamTaskGroupID").TrimEnd(new char[]
			{
				','
			});
			DataTable dataTable = null;
			try
			{
				dataTable = CommonTeam.Instance.ISCanDeleteTeamTaskGroupFeeInfo(text);
				if (dataTable == null)
				{
					msg = string.Concat(new string[]
					{
						"{\"success\":\"-1\",\"Message\":\"",
						text,
						"不允许删除\",\"TeamTaskGroupID\":\"",
						text,
						"\"}"
					});
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取是否可用删除某个任务下的收费项目失败 失败原因:未获取到任何数据 任务编号:",
						text
					}));
				}
				else
				{
					msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				}
			}
			catch (Exception ex)
			{
				msg = string.Concat(new string[]
				{
					"{\"success\":\"-1\",\"Message\":\"",
					ex.Message,
					"\",\"TeamTaskGroupID\":\"",
					text,
					"\"}"
				});
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取是否可用删除某个任务下的收费项目失败 失败原因:",
					ex.Message,
					" 任务编号:",
					text
				}));
			}
			this.OutPutMessage(msg);
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			dataTable = null;
		}

		public void IsCanSaveCustomer()
		{
			string msg = string.Empty;
			string text = base.GetString("TeamTaskGroupID").TrimEnd(new char[]
			{
				','
			});
			DataTable dataTable = null;
			try
			{
				dataTable = CommonTeam.Instance.IsCanSaveCustomer(text);
				if (dataTable == null)
				{
					msg = string.Concat(new string[]
					{
						"{\"success\":\"-1\",\"Message\":\"",
						text,
						"尚未维护收费项目，不允许保存客户名单\",\"TeamTaskGroupID\":\"",
						text,
						"\"}"
					});
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取是否保可保存客户名单 失败原因:尚未维护收费项目，不允许保存客户名单 任务编号:",
						text
					}));
				}
				else
				{
					msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				}
			}
			catch (Exception ex)
			{
				msg = string.Concat(new string[]
				{
					"{\"success\":\"-1\",\"Message\":\"",
					ex.Message,
					"\",\"TeamTaskGroupID\":\"",
					text,
					"\"}"
				});
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取是否保可保存客户名单 失败原因:",
					ex.Message,
					" 任务编号:",
					text
				}));
			}
			this.OutPutMessage(msg);
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			dataTable = null;
		}

		public void ISCanDeleteTeamTaskInfo()
		{
			string msg = string.Empty;
			string text = base.GetString("ID_TeamTask").TrimEnd(new char[]
			{
				','
			});
			DataTable dataTable = null;
			try
			{
				dataTable = CommonTeam.Instance.ISCanDeleteTeamTaskInfo(text);
				if (dataTable == null)
				{
					msg = string.Concat(new string[]
					{
						"{\"success\":\"-1\",\"Message\":\"",
						text,
						"不允许删除\",\"ID_TeamTask\":\"",
						text,
						"\"}"
					});
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取是否可以删除团体任务失败 失败原因:未获取到任何数据，不允许删除 任务编号:",
						text
					}));
				}
				else
				{
					msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				}
			}
			catch (Exception ex)
			{
				msg = string.Concat(new string[]
				{
					"{\"success\":\"-1\",\"Message\":\"",
					ex.Message,
					"\",\"ID_TeamTask\":\"",
					text,
					"\"}"
				});
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取是否可以删除团体任务失败 失败原因:",
					ex.Message,
					" 任务编号:",
					text
				}));
			}
			this.OutPutMessage(msg);
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			dataTable = null;
		}

		public void ISCanDeleteTeamTaskGroupInfo()
		{
			string msg = string.Empty;
			string text = base.GetString("ID_TeamTaskGroup").TrimEnd(new char[]
			{
				','
			});
			DataTable dataTable = null;
			try
			{
				dataTable = CommonTeam.Instance.ISCanDeleteTeamTaskGroupInfo(text);
				if (dataTable == null)
				{
					msg = string.Concat(new string[]
					{
						"{\"success\":\"-1\",\"Message\":\"",
						text,
						"不允许删除\",\"ID_TeamTaskGroup\":\"",
						text,
						"\"}"
					});
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取是否可以删除团体任务分组失败 失败原因:未获取到任何数据，不允许删除 任务分组编号:",
						text
					}));
				}
				else
				{
					msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				}
			}
			catch (Exception ex)
			{
				msg = string.Concat(new string[]
				{
					"{\"success\":\"-1\",\"Message\":\"",
					ex.Message,
					"\",\"ID_TeamTaskGroup\":\"",
					text,
					"\"}"
				});
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取是否可以删除团体任务分组失败 失败原因:",
					ex.Message,
					" 任务分组编号:",
					text
				}));
			}
			this.OutPutMessage(msg);
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			dataTable = null;
		}

		public void ISCanDeleteTeamTaskGroupCustomerInfo()
		{
			string msg = string.Empty;
			string text = base.GetString("ID_Customer").TrimEnd(new char[]
			{
				','
			}).TrimStart(new char[]
			{
				','
			});
			DataTable dataTable = null;
			try
			{
				dataTable = CommonTeam.Instance.ISCanDeleteTeamTaskGroupCustomerInfo(text);
				if (dataTable == null)
				{
					msg = string.Concat(new string[]
					{
						"{\"success\":\"-1\",\"Message\":\"",
						text,
						"不允许删除\",\"ID_Customer\":\"",
						text,
						"\"}"
					});
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取是否可以客户名单失败 失败原因:未获取到任何数据，不允许删除 体检号:",
						text
					}));
				}
				else
				{
					msg = JsonHelperFont.Instance.DataTableToJSON(dataTable, "dataList");
				}
			}
			catch (Exception ex)
			{
				msg = string.Concat(new string[]
				{
					"{\"success\":\"-1\",\"Message\":\"",
					ex.Message,
					"\",\"ID_Customer\":\"",
					text,
					"\"}"
				});
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取是否可以客户名单失败 失败原因:",
					ex.Message,
					" 体检号:",
					text
				}));
			}
			this.OutPutMessage(msg);
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			dataTable = null;
		}

		public void GetCustomerByTeamAndTask()
		{
			string iD_Team = base.GetString("ID_Team").TrimEnd(new char[]
			{
				','
			});
			string iD_TeamTask = base.GetString("ID_TeamTask").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetCustomerByTeamAndTask(iD_Team, iD_TeamTask), "dataList");
			this.OutPutMessage(msg);
		}

		public void DelData()
		{
			string text = base.GetString("IDTeamS").TrimEnd(new char[]
			{
				','
			}).Replace("'", string.Empty);
			if (text != string.Empty)
			{
				try
				{
					int num = CommonTeam.Instance.DelTeamData(text);
					if (num <= 0)
					{
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",删除团体信息失败 0行受影响 团体编号:",
							text
						}));
						string msg = "{\"success\":\"0\",\"Message\":\"删除失败，请重试！\"}";
						this.OutPutMessage(msg);
					}
					else
					{
						base.ClearCache_AllTeam();
						Log4J.Instance.Info(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",删除团体信息成功 团体编号:",
							text
						}));
						string msg = "{\"success\":\"1\",\"Message\":\"删除成功！\"}";
						this.OutPutMessage(msg);
					}
				}
				catch (Exception ex)
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",删除团体信息失败 团体编号:",
						text,
						" 错误原因为：",
						ex.Message
					}));
				}
			}
		}

		public void DeleteTeamTask()
		{
			string text = base.GetString("CustTeamTaskID").TrimEnd(new char[]
			{
				','
			}).Replace("'", string.Empty);
			if (text != string.Empty)
			{
				int num = CommonTeam.Instance.DeleteTeamTask(text);
				if (num <= 0)
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",删除团体任务信息失败 任务编号:",
						text
					}));
					string msg = "{\"success\":\"0\",\"Message\":\"删除失败，请重试！\"}";
					this.OutPutMessage(msg);
				}
				else
				{
					base.ClearCache_AllTeamTask();
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",删除团体任务信息成功 任务编号:",
						text
					}));
					string msg = "{\"success\":\"1\",\"Message\":\"删除成功！\"}";
					this.OutPutMessage(msg);
				}
			}
		}

		public void DeleteTeamTaskGroup()
		{
			string text = base.GetString("CustTeamTaskGroupID").TrimEnd(new char[]
			{
				','
			}).Replace("'", string.Empty);
			if (text != string.Empty)
			{
				try
				{
					int num = CommonTeam.Instance.DeleteTeamTaskGroup(text);
					if (num <= 0)
					{
						string msg = "{\"success\":\"0\",\"Message\":\"删除失败，请重试！\"}";
						this.OutPutMessage(msg);
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",删除团体任务分组信息失败 0行受影响 任务编号:",
							text
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
							",删除团体任务分组信息成功 任务编号:",
							text
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
						",删除团体任务分组信息失败 任务编号:",
						text,
						" 错误原因为：",
						ex.Message
					}));
				}
			}
		}

		public void DeleteTeamTaskGroupFee()
		{
			string text = base.GetString("AllItem").TrimEnd(new char[]
			{
				'|'
			});
			if (text != string.Empty)
			{
				try
				{
					int num = CommonTeam.Instance.DeleteTeamTaskGroupFee(text);
					if (num <= 0)
					{
						string msg = "{\"success\":\"0\",\"Message\":\"删除失败，请重试！\"}";
						this.OutPutMessage(msg);
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",删除团体任务分组收费项目失败 0行受影响 任务分组收费项目编号:",
							text
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
							",删除团体任务分组收费项目成功 任务分组收费项目编号:",
							text
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
						",删除团体任务分组收费项目失败 任务分组收费项目编号:",
						text,
						" 错误原因为：",
						ex.Message
					}));
				}
			}
		}

		public void DoDeleteCustomerInfo()
		{
			string text = base.GetString("AllItem").TrimEnd(new char[]
			{
				','
			});
			if (text != string.Empty)
			{
				try
				{
					int num = CommonTeam.Instance.DeleteCustomerInfo(text);
					if (num <= 0)
					{
						string msg = "{\"success\":\"0\",\"Message\":\"删除失败，请重试！\"}";
						this.OutPutMessage(msg);
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",删除客户名单失败 0行受影响 体检号为:",
							text.Replace("'", string.Empty)
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
							",删除客户名单成功 体检号为:",
							text.Replace("'", string.Empty)
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
						",删除客户名单失败 体检号为:",
						text.Replace("'", string.Empty),
						" 错误原因为：",
						ex.Message
					}));
				}
			}
		}

		public void DoDeleteInternatCustomerInfo()
		{
			string text = base.GetString("AllItem").TrimEnd(new char[]
			{
				','
			});
			if (text != string.Empty)
			{
				try
				{
					int num = CommonTeam.Instance.DeleteInternatCustomerInfo(text);
					if (num <= 0)
					{
						string msg = "{\"success\":\"0\",\"Message\":\"删除失败，请重试！\"}";
						this.OutPutMessage(msg);
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",删除网络预约客户名单失败 0行受影响 体检号为:",
							text.Replace("'", string.Empty)
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
							",删除网络预约客户名单成功 体检号为:",
							text.Replace("'", string.Empty)
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
						",删除网络预约客户名单失败 体检号为:",
						text.Replace("'", string.Empty),
						" 错误原因为：",
						ex.Message
					}));
				}
			}
		}

		public void GetTeamTaskInfoByTaskID()
		{
			string teamTaskID = base.GetString("TeamTaskID").Trim();
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetTeamTaskInfoByTaskID(teamTaskID), "dataList");
			this.OutPutMessage(msg);
		}

		public void GetTeamTaskInfoByKeyWord()
		{
			string text = base.GetString("ID_Team").Trim();
			string columnValue = base.GetString("ColumnValue").Trim();
			string columnName = base.GetString("ColumnName").Trim();
			bool isLike = !(base.GetString("IsLike").Trim() == "0");
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetTeamTaskInfoByKeyWord(columnName, columnValue, isLike), "dataList");
			this.OutPutMessage(msg);
		}

		public void SaveTeamTaskData()
		{
			string text = base.GetString("modelName").Trim();
			string text2 = base.GetString("type").Trim();
			string text3 = base.GetString("ID_Team").Trim();
			string text4 = base.GetString("TeamTaskItems").Trim();
			string text5 = string.Empty;
			string text6 = text4.Substring(0, text4.IndexOf("_"));
			if (text6.Contains("******"))
			{
				text5 = "新增";
			}
			else
			{
				text5 = "修改";
			}
			string msg = string.Empty;
			try
			{
				int num = CommonTeam.Instance.SaveTeamTaskData(text3, text4, this.ID_User, this.UserName);
				if (num == -1)
				{
					this.FailMessage = "未获取到团体信息或者是没有填写团体任务信息，请您确认！";
					msg = "{\"success\":\"-1\",\"Message\":\"" + this.FailMessage + "\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",",
						text5,
						"团队任务 未获取到团体信息或者是没有填写团体任务信息 团体编号:",
						text3,
						" ,详细数据:",
						text4
					}));
				}
				else if (num == 0)
				{
					this.FailMessage = "保存失败，请您重试！";
					msg = "{\"success\":\"0\",\"Message\":\"" + this.FailMessage + "\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",",
						text5,
						"团队任务失败 0行受影响 团体编号:",
						text3,
						" ,详细数据:",
						text4
					}));
				}
				else
				{
					base.ClearCache_AllTeamTask();
					this.FailMessage = "保存成功！";
					msg = "{\"success\":\"1\",\"Message\":\"" + this.FailMessage + "\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",",
						text5,
						"团队任务成功 团体编号:",
						text3,
						" ,详细数据:",
						text4
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
					",",
					text5,
					"团队任务失败 团体编号:",
					text3,
					" 错误原因为：",
					ex.Message,
					" ,详细数据:",
					text4
				}));
			}
		}

		public void SaveTemaTaskGroup()
		{
			string text = base.GetString("modelName").Trim();
			string text2 = base.Server.HtmlDecode(base.GetString("type")).Trim();
			string text3 = base.Server.HtmlDecode(base.GetString("ID_Team")).Trim();
			string text4 = base.Server.HtmlDecode(base.GetString("ID_TeamTask")).Trim();
			string text5 = base.Server.HtmlDecode(base.GetString("TeamName")).Trim();
			string teamTaskName = base.Server.HtmlDecode(base.GetString("TeamTaskName")).Trim();
			string teamTaskItems = base.Server.HtmlDecode(base.GetString("TeamTaskGroupItems")).Trim();
			string msg = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				int num = CommonTeam.Instance.SaveTemaTaskGroup(text3, text5, text4, teamTaskName, teamTaskItems, this.ID_User, this.UserName, ref stringBuilder);
				if (num == -1)
				{
					this.FailMessage = "未获取到团体任务分组信息或者是没有填写团体任务分组信息，请您确认！";
					msg = "{\"success\":\"-1\",\"Message\":\"" + this.FailMessage + "\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增团体任务分组失败 未获取到团体任务分组信息或者是没有填写团体任务分组信息 团体编号:",
						text3,
						" ,团体名称:",
						text5,
						" ,任务编号:",
						text4,
						"\r\n详细数据:",
						stringBuilder.ToString()
					}));
				}
				else if (num == 0)
				{
					this.FailMessage = "团体任务分组保存失败，请您重试！";
					msg = "{\"success\":\"0\",\"Message\":\"" + this.FailMessage + "\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增团体任务分组失败 0行受影响 团体编号:",
						text3,
						" ,团体名称:",
						text5,
						" ,任务编号:",
						text4,
						"\r\n详细数据:",
						stringBuilder.ToString()
					}));
				}
				else
				{
					this.FailMessage = "团体任务分组保存成功！";
					msg = "{\"success\":\"1\",\"Message\":\"" + this.FailMessage + "\"}";
					this.OutPutMessage(msg);
					Log4J.Instance.Info(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",新增团体任务分组成功 团体编号:",
						text3,
						" ,团体名称:",
						text5,
						" ,任务编号:",
						text4,
						"\r\n详细数据:",
						stringBuilder.ToString()
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
					",新增团体任务分组失败 团体编号:",
					text3,
					" ,团体名称:",
					text5,
					" ,任务编号:",
					text4,
					" ,错误原因为：",
					ex.Message,
					"\r\n详细数据:",
					stringBuilder.ToString()
				}));
			}
		}

		public void SaveCustomerCustFeeOfBatch()
		{
			string a = base.GetString("BatchOper").Trim().ToLower();
			string text = string.Empty;
			string text2 = base.GetString("ID_Team").Trim();
			string text3 = base.GetString("ID_TeamTask").Trim();
			string text4 = base.GetString("ID_TeamTaskGroupS").Trim();
			string text5 = base.GetString("AllItem").Trim();
			int num = -1;
			string empty = string.Empty;
			string msg = string.Empty;
			string text6 = string.Empty;
			string text7 = string.Empty;
			if (a == "add")
			{
				text = "批量新增";
				text6 = base.GetString("ID_Customers").TrimEnd(new char[]
				{
					','
				});
				text7 = base.GetString("ID_FeeS").TrimEnd(new char[]
				{
					','
				});
				num = CommonTeam.Instance.AddCustomerCustFeeOfBatch(this.ID_User, this.UserName, text2, text3, text4, text5);
			}
			else if (a == "delete")
			{
				text = "批量删除";
				text6 = base.GetString("ID_Customers").TrimEnd(new char[]
				{
					','
				});
				text7 = base.GetString("ID_FeeS").TrimEnd(new char[]
				{
					','
				});
				num = CommonTeam.Instance.DeleteCustomerCustFeeOfBatch(text6, text7);
			}
			if (num == -1)
			{
				this.FailMessage = "未获取到客户信息，请您确认！";
				msg = "{\"success\":\"-1\",\"Message\":\"" + this.FailMessage + "\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text,
					"客户收费项目 失败 未获取到客户信息 团体编号:",
					text2,
					" ,任务编号:",
					text3,
					" ,分组编号:",
					text4,
					" ,详细数据:",
					text5,
					" ,体检号:",
					text6,
					" ,收费项目编号:",
					text7
				}));
			}
			else if (num == 0)
			{
				this.FailMessage = "保存失败，请您重试！";
				msg = "{\"success\":\"0\",\"Message\":\"" + this.FailMessage + "\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text,
					"客户收费项目 失败 团体编号:",
					text2,
					" ,任务编号:",
					text3,
					" ,分组编号:",
					text4,
					" ,详细数据:",
					text5,
					" ,体检号:",
					text6,
					" ,收费项目编号:",
					text7
				}));
			}
			else
			{
				this.FailMessage = ((a == "delete") ? "删除成功！" : "新增成功！");
				msg = "{\"success\":\"1\",\"Message\":\"" + this.FailMessage + "\"}";
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text,
					"客户收费项目 ",
					this.FailMessage,
					" 团体编号:",
					text2,
					" ,任务编号:",
					text3,
					" ,分组编号:",
					text4,
					" ,详细数据:",
					text5,
					" ,体检号:",
					text6,
					" ,收费项目编号:",
					text7
				}));
			}
			this.OutPutMessage(msg);
		}

		private string GetdateDiff(string Title, DateTime BeginDate, DateTime EndDate)
		{
			return Public.GetDateDiff(Title, BeginDate, EndDate);
		}

		public void SaveCustomerInfo()
		{
			DateTime now = DateTime.Now;
			string text = string.Empty;
			string empty = string.Empty;
			string text2 = base.Server.HtmlDecode(base.GetString("AllItem")).TrimEnd(new char[]
			{
				'|'
			});
			string text3 = base.Server.HtmlDecode(base.GetString("ID_Team"));
			string teamName = base.Server.HtmlDecode(base.GetString("TeamName"));
			string text4 = base.Server.HtmlDecode(base.GetString("ID_TeamTask"));
			string text5 = base.Server.HtmlDecode(base.GetString("ID_TeamTaskGroupS")).TrimEnd(new char[]
			{
				','
			});
			StringBuilder stringBuilder = new StringBuilder();
			string title = "使用SaveCustomerInfo_New";
			bool flag = false;
			int num = CommonTeam.Instance.SaveCustomerInfo_New(this.ID_User, this.UserName, text3, teamName, text4, text5, text2, ref stringBuilder, ref flag);
			if (stringBuilder.Length == 0)
			{
				stringBuilder.Append(text2);
			}
			DataSet dataSet = new DataSet();
			dataSet.Tables.Clear();
			DataTable dataTable = new DataTable();
			DataTable dataTable2 = null;
			dataSet.Tables.Add(dataTable);
			dataTable.Columns.Add("success");
			dataTable.Columns.Add("Message");
			if (flag)
			{
				this.FailMessage = "用于生成团体备单的预留体检号不足,请您与管理员联系！";
				if (num > 0)
				{
					this.FailMessage = "部分名单生成体检号成功，但用于生成团体备单的预留体检号不足,请您与管理员联系！";
					dataTable2 = CommonTeam.Instance.GetTeamTaskGroupCustomerInfo(text3, text4, text5).Copy();
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = this.FailMessage;
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存客户名单 未获取到客户信息 ",
					text,
					" 团体编号:",
					text3,
					" ,任务编号:",
					text4,
					" ,分组编号:",
					text5,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else if (num == -100)
			{
				this.FailMessage = "用于生成团体备单的预留体检号不足！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "对不起,用于生成团体备单的预留体检号不足,请您与管理员联系！";
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存客户名单 未获取到客户信息 ",
					text,
					" 团体编号:",
					text3,
					" ,任务编号:",
					text4,
					" ,分组编号:",
					text5,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else if (num == -200)
			{
				this.FailMessage = "用于生成团体备单的预留体检号不足！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = stringBuilder.ToString();
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存客户名单 未获取到客户信息 ",
					text,
					" 团体编号:",
					text3,
					" ,任务编号:",
					text4,
					" ,分组编号:",
					text5,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else if (num == -1)
			{
				this.FailMessage = "未获取到客户信息，请您确认！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = -1;
				dataRow["Message"] = this.FailMessage;
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff("使用SaveCustomerInfo_New方法", now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存客户名单 未获取到客户信息 ",
					text,
					" 团体编号:",
					text3,
					" ,任务编号:",
					text4,
					" ,分组编号:",
					text5,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else if (num == 0)
			{
				this.FailMessage = "体检号生成失败，请您重试！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = this.FailMessage;
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存客户名单 体检号生成失败 ",
					text,
					" 团体编号:",
					text3,
					" ,任务编号:",
					text4,
					" ,分组编号:",
					text5,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else
			{
				this.FailMessage = "体检号生成成功！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 1;
				dataRow["Message"] = this.FailMessage;
				dataTable.Rows.Add(dataRow);
				dataTable2 = CommonTeam.Instance.GetTeamTaskGroupCustomerInfo(text3, text4, text5).Copy();
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存客户名单 ",
					this.FailMessage,
					text,
					" 团体编号:",
					text3,
					" ,ID_TeamTask:",
					text4,
					" ,分组编号:",
					text5,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			dataSet.Tables.Clear();
			if (dataTable != null)
			{
				dataSet.Tables.Add(dataTable);
			}
			if (dataTable2 != null)
			{
				dataSet.Tables.Add(dataTable2);
			}
			this.OutPutMessage(JsonHelperFont.Instance.DataSetToJSON(dataSet));
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			if (dataSet != null)
			{
				dataSet.Dispose();
			}
		}

		public void SaveInternatCustomerInfo()
		{
			DateTime now = DateTime.Now;
			string text = string.Empty;
			string empty = string.Empty;
			string text2 = base.Server.HtmlDecode(base.GetString("AllItem")).TrimEnd(new char[]
			{
				'|'
			});
			StringBuilder stringBuilder = new StringBuilder();
			string title = "使用SaveInternatCustomerInfo";
			bool flag = false;
			string text3 = string.Empty;
			int num = CommonTeam.Instance.SaveInternatCustomerInfo(this.ID_User, this.UserName, text2, ref stringBuilder, ref flag, ref text3);
			text3 = text3.TrimEnd(new char[]
			{
				','
			});
			if (stringBuilder.Length == 0)
			{
				stringBuilder.Append(text2);
			}
			DataSet dataSet = new DataSet();
			dataSet.Tables.Clear();
			DataTable dataTable = new DataTable();
			DataTable dataTable2 = null;
			dataSet.Tables.Add(dataTable);
			dataTable.Columns.Add("success");
			dataTable.Columns.Add("Message");
			if (flag)
			{
				this.FailMessage = "用于生成网上预约备单的预留体检号不足,请您与管理员联系！";
				if (num > 0)
				{
					this.FailMessage = "部分名单生成体检号成功，但用于生成网上预约备单的预留体检号不足,请您与管理员联系！";
					dataTable2 = CommonTeam.Instance.GetInternatCustomerInfo(text3).Copy();
				}
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = this.FailMessage;
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存网上预约客户名单 未获取到客户信息 ",
					text,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else if (num == -100)
			{
				this.FailMessage = "用于生成团体备单的预留体检号不足！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = "对不起,用于生成网上预约备单的预留体检号不足,请您与管理员联系！";
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存网上预约客户名单 未获取到客户信息 ",
					text,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else if (num == -200)
			{
				this.FailMessage = "用于生成团体备单的预留体检号不足！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = stringBuilder.ToString();
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存网上预约客户名单 未获取到客户信息 ",
					text,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else if (num == -1)
			{
				this.FailMessage = "未获取到客户信息，请您确认！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = -1;
				dataRow["Message"] = this.FailMessage;
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff("使用SaveInternatCustomerInfo方法", now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存网上预约客户名单 未获取到客户信息 ",
					text,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else if (num == 0)
			{
				this.FailMessage = "体检号生成失败，请您重试！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 0;
				dataRow["Message"] = this.FailMessage;
				dataTable.Rows.Add(dataRow);
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存网上预约客户名单 体检号生成失败 ",
					text,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			else
			{
				this.FailMessage = "体检号生成成功！";
				DataRow dataRow = dataTable.NewRow();
				dataRow["success"] = 1;
				dataRow["Message"] = this.FailMessage;
				dataTable.Rows.Add(dataRow);
				dataTable2 = CommonTeam.Instance.GetInternatCustomerInfo(text3).Copy();
				text = this.GetdateDiff(title, now, DateTime.Now);
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存网上预约客户名单 ",
					this.FailMessage,
					text,
					" ,详细数据:",
					stringBuilder.ToString()
				}));
			}
			dataSet.Tables.Clear();
			if (dataTable != null)
			{
				dataSet.Tables.Add(dataTable);
			}
			if (dataTable2 != null)
			{
				dataSet.Tables.Add(dataTable2);
			}
			this.OutPutMessage(JsonHelperFont.Instance.DataSetToJSON(dataSet));
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			if (dataSet != null)
			{
				dataSet.Dispose();
			}
		}

		public void SaveTeamTaskGroupFee()
		{
			string msg = string.Empty;
			string allTeamTaskGroupFeeItem = base.Server.HtmlDecode(base.GetString("AllTeamTaskGroupFeeItem")).TrimEnd(new char[]
			{
				'|'
			});
			StringBuilder stringBuilder = new StringBuilder();
			int num = CommonTeam.Instance.SaveTeamTaskGroupFee(allTeamTaskGroupFeeItem, ref stringBuilder);
			if (num == -1)
			{
				this.FailMessage = "未获取到团体任务分组信息或者是没有填写团体任务分组信息，请您确认！";
				msg = "{\"success\":\"-1\",\"Message\":\"" + this.FailMessage + "\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存团体任务分组收费项目 失败 未获取到团体任务分组信息或者是没有填写团体任务分组信息\r\n详细数据:",
					stringBuilder.ToString()
				}));
			}
			else if (num == 0)
			{
				this.FailMessage = "收费项目保存失败，请您重试！";
				msg = "{\"success\":\"0\",\"Message\":\"" + this.FailMessage + "\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存团体任务分组收费项目 失败\r\n详细数据:",
					stringBuilder.ToString()
				}));
			}
			else
			{
				this.FailMessage = "收费项目保存成功！";
				msg = "{\"success\":\"1\",\"Message\":\"" + this.FailMessage + "\"}";
				Log4J.Instance.Info(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",保存团体任务分组收费项目 成功\r\n详细数据:",
					stringBuilder.ToString()
				}));
			}
			this.OutPutMessage(msg);
		}

		public void GetTeamInfoByKeyWords()
		{
			string inputCode = base.GetString("InputCode").TrimEnd(new char[]
			{
				','
			});
			string text = base.GetString("elementID").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetTeamInfoByKeyWords(inputCode), "dataList");
			this.OutPutMessage(msg);
		}

		public void GetTeamTaskGroupFeeDataByGroupID()
		{
			string iD_TeamTaskGroupS = base.Server.HtmlDecode(base.GetString("AllTeamTaskGroupID")).TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetTeamTaskGroupFeeDataByGroupID(iD_TeamTaskGroupS, this.UserName), "dataList");
			this.OutPutMessage(msg);
		}

		public void GetTeamTaskInfoByKeyWords()
		{
			string inputCode = base.GetString("InputCode").TrimEnd(new char[]
			{
				','
			});
			string iD_Team = base.GetString("ID_Team").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetTeamTaskInfoByKeyWords(inputCode, iD_Team), "dataList");
			this.OutPutMessage(msg);
		}

		public void GetTeamTaskGroupCustomerCustInfo()
		{
			string iD_Team = base.GetString("ID_Team").TrimEnd(new char[]
			{
				','
			});
			string iD_TeamTask = base.GetString("ID_TeamTask").TrimEnd(new char[]
			{
				','
			});
			string iD_TeamTaskGroupS = base.GetString("ID_TeamTaskGroupS").TrimEnd(new char[]
			{
				','
			});
			string iD_Customer = base.GetString("ID_Customer").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataSetToJSON(CommonTeam.Instance.GetTeamTaskGroupCustomerCustInfo(this.UserName, iD_Team, iD_TeamTask, iD_TeamTaskGroupS, iD_Customer));
			this.OutPutMessage(msg);
		}

		public void GetTeamTaskGroupCustomerInfo()
		{
			string iD_Team = base.GetString("ID_Team").TrimEnd(new char[]
			{
				','
			});
			string iD_TeamTask = base.GetString("ID_TeamTask").TrimEnd(new char[]
			{
				','
			});
			string iD_TeamTaskGroup = base.GetString("ID_TeamTaskGroup").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetTeamTaskGroupCustomerInfo(iD_Team, iD_TeamTask, iD_TeamTaskGroup), "dataList");
			this.OutPutMessage(msg);
		}

		public void GetTeamTaskGroupCustInfo()
		{
			string iD_Team = base.GetString("ID_Team").TrimEnd(new char[]
			{
				','
			});
			string iD_TeamTask = base.GetString("ID_TeamTask").TrimEnd(new char[]
			{
				','
			});
			string iD_TeamTaskGroupS = base.GetString("ID_TeamTaskGroupS").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataSetToJSON(CommonTeam.Instance.GetTeamTaskGroupCustInfo(iD_Team, iD_TeamTask, iD_TeamTaskGroupS));
			this.OutPutMessage(msg);
		}

		public void GetTeamTaskGroupCustInfoOfTeamBath()
		{
			string iD_Team = base.GetString("ID_Team").TrimEnd(new char[]
			{
				','
			});
			string iD_TeamTask = base.GetString("ID_TeamTask").TrimEnd(new char[]
			{
				','
			});
			string iD_TeamTaskGroupS = base.GetString("ID_TeamTaskGroupS").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataSetToJSON(CommonTeam.Instance.GetTeamTaskGroupCustInfoOfTeamBath(iD_Team, iD_TeamTask, iD_TeamTaskGroupS));
			this.OutPutMessage(msg);
		}

		public void GetTeamTaskGroupInfoByTeamAndTask()
		{
			string iD_Team = base.GetString("ID_Team").Trim();
			string iD_TeamTask = base.GetString("ID_TeamTask").Trim();
			bool isLike = !(base.GetString("IsLike").Trim() == "0");
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetTeamTaskGroupInfoByTeamAndTask(iD_Team, iD_TeamTask, isLike), "dataList");
			this.OutPutMessage(msg);
		}

		public void ImportCustomerInfoFromExcel()
		{
			string workSheetName = base.GetString("WorkSheetName").Trim();
			string text = base.GetString("ExcelFilePath").Trim();
			string svrAbsPath = text;
			string empty = string.Empty;
			string msg = JsonHelperFont.Instance.DataSetToJSON(CommonTeam.Instance.ImportCustomerInfoFromExcel(svrAbsPath, workSheetName, ref empty));
			this.OutPutMessage(msg);
		}

		public void ImportInternatCustomerInfoFromExcel()
		{
			string workSheetName = base.GetString("WorkSheetName").Trim();
			string text = base.GetString("ExcelFilePath").Trim();
			string svrAbsPath = text;
			string empty = string.Empty;
			DataSet dataSet = CommonTeam.Instance.ImportInternatCustomerInfoFromExcel(svrAbsPath, workSheetName, ref empty);
			DataTable dataTable = dataSet.Tables[0];
			if (!dataTable.Columns.Contains("ID_Customer"))
			{
				dataTable.Columns.Add("ID_Customer");
			}
			if (!dataTable.Columns.Contains("exist"))
			{
				dataTable.Columns.Add("exist");
			}
			if (!dataTable.Columns.Contains("State"))
			{
				dataTable.Columns.Add("State");
			}
			if (!dataTable.Columns.Contains("Operator"))
			{
				dataTable.Columns.Add("Operator");
			}
			DataTable dataTable2 = null;
			string text2 = string.Empty;
			foreach (DataRow dataRow in dataTable.Rows)
			{
				object obj = text2;
				text2 = string.Concat(new object[]
				{
					obj,
					"'",
					dataRow["UserOrderNO"],
					"',"
				});
				dataRow.BeginEdit();
				dataRow["ID_Customer"] = string.Empty;
				dataRow["State"] = 0;
				dataRow["exist"] = 0;
				dataRow["Operator"] = this.UserName;
				dataRow.EndEdit();
			}
			text2 = text2.TrimEnd(new char[]
			{
				','
			});
			if (text2.Length > 0)
			{
				string sql = string.Format("SELECT 1 exist,ID_InternetDataImportLog,CustomerOrderNO,OrderDetail,ID_Customer,Operator,OperateDate FROM OnInternetDataImportLog WHERE CustomerOrderNO IN({0})", text2);
				dataTable2 = CommonExcuteSql.Instance.ExcuteSql(sql).Tables[0];
			}
			if (dataTable2.Rows.Count > 0)
			{
				string arg = string.Empty;
				string arg2 = string.Empty;
				string filterExpression = string.Empty;
				foreach (DataRow dataRow in dataTable.Rows)
				{
					arg = dataRow["UserOrderNO"].ToString();
					arg2 = dataRow["ID_UserOrderDetail"].ToString();
					filterExpression = string.Format("CustomerOrderNO='{0}' AND OrderDetail='{1}'", arg, arg2);
					DataRow[] array = dataTable2.Select(filterExpression);
					if (array.Length > 0)
					{
						dataRow.BeginEdit();
						dataRow["ID_Customer"] = array[0]["ID_Customer"];
						dataRow["State"] = 1;
						dataRow["exist"] = array[0]["exist"];
						dataRow["Operator"] = array[0]["Operator"];
						dataRow.EndEdit();
					}
				}
			}
			string msg = JsonHelperFont.Instance.DataSetToJSON(dataSet);
			this.OutPutMessage(msg);
			if (dataTable != null)
			{
				dataTable.Dispose();
			}
			if (dataSet != null)
			{
				dataSet.Dispose();
			}
		}

		public void GetTeamTaskByTeam()
		{
			string iD_Team = base.GetString("ID_Team").Trim();
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetTeamTaskByTeam(iD_Team), "dataList");
			this.OutPutMessage(msg);
		}

		public void GetCustomerUnionBusFee()
		{
			string text = base.GetString("type").ToLower();
			string iD_Team = base.GetString("ID_Team").Trim();
			string iD_TeamTask = base.GetString("ID_TeamTask").Trim();
			string iD_TeamTaskGroup = base.GetString("ID_TeamTaskGroupS").Trim();
			string iD_CustomerS = base.GetString("ID_Customers").TrimEnd(new char[]
			{
				','
			});
			string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetCustomerUnionBusFee(this.UserName, iD_Team, iD_TeamTask, iD_TeamTaskGroup, iD_CustomerS), "dataList");
			this.OutPutMessage(msg);
		}

		public void CloneCustFee()
		{
			int @int = base.GetInt("FromTeamTaskGroupID", -1);
			int int2 = base.GetInt("ToTeamTaskGroupID", -1);
			if (@int != -1 && int2 != -1)
			{
				string msg = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.CloneCustFee(this.UserName, "10", @int.ToString(), int2.ToString()), "dataList");
				this.OutPutMessage(msg);
			}
		}

		public void GetCustomerPagesInfo()
		{
			string @string = base.GetString("CustomerName");
			string string2 = base.GetString("IDCard");
			string string3 = base.GetString("ID_Customer");
			int @int = base.GetInt("ID_Team", -1);
			int int2 = base.GetInt("ID_TeamTask", -1);
			string text = base.GetString("ID_TeamTaskGroup").TrimEnd(new char[]
			{
				','
			});
			int int3 = base.GetInt("pageIndex", 1);
			int int4 = base.GetInt("pageSize", 10);
			int totalCount = 0;
			int num = 0;
			string pageCode = "QueryCustomerPagesInfoParam";
			SqlConditionInfo[] array = new SqlConditionInfo[6];
			array[0] = new SqlConditionInfo("@ID_TeamTaskGroup", text, TypeCode.Object);
			if (string.IsNullOrEmpty(text) || text == "-1")
			{
				array[0].IsSelf = true;
			}
			array[0].Place = 2;
			array[1] = new SqlConditionInfo("@ID_TeamTask", int2, TypeCode.Int32);
			array[1].Place = 2;
			array[2] = new SqlConditionInfo("@ID_Team", @int, TypeCode.Int32);
			array[2].Place = 2;
			array[3] = new SqlConditionInfo("@ID_Customer", string3, TypeCode.String);
			array[3].Place = 2;
			if (string.IsNullOrEmpty(string3))
			{
				array[3].IsSelf = true;
			}
			array[4] = new SqlConditionInfo("@IDCard", string2, TypeCode.String);
			array[4].Place = 2;
			if (string.IsNullOrEmpty(string2))
			{
				array[4].IsSelf = true;
			}
			array[5] = new SqlConditionInfo("@CustomerName", "%" + @string + "%", TypeCode.String);
			array[5].Place = 2;
			if (string.IsNullOrEmpty(@string))
			{
				array[5].IsSelf = true;
			}
			DataTable page = CommonRegiste.Instance.GetPage(pageCode, int3, int4, out totalCount, out num, array);
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		public void PauseTeamTaskGroup()
		{
			string msg = string.Empty;
			string text = string.Empty;
			string @string = base.GetString("ID_TeamTaskGroup");
			int @int = base.GetInt("IsPaused", 0);
			text = ((@int == 0) ? "启用" : "禁用");
			if (!string.IsNullOrEmpty(@string))
			{
				int num = CommonTeam.Instance.PauseTeamTaskGroup(@string, @int);
				if (num > 0)
				{
					msg = "{\"success\":\"1\",\"Message\":\"" + text + "成功\"}";
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",",
						text,
						"分组ID:",
						@string,
						" 成功\r\n"
					}));
				}
				else
				{
					msg = "{\"success\":\"0\",\"Message\":\"" + text + "失败,请重试\"}";
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",",
						text,
						"分组ID:",
						@string,
						" 失败 0行受影响\r\n"
					}));
				}
			}
			else
			{
				msg = "{\"success\":\"0\",\"Message\":\"分组不能为空\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text,
					"分组ID:",
					@string,
					" 失败 分组ID为空\r\n"
				}));
			}
			this.OutPutMessage(msg);
		}

		public void PauseCustomer()
		{
			string msg = string.Empty;
			string text = string.Empty;
			string @string = base.GetString("ID_Customer");
			int @int = base.GetInt("IsPaused", 0);
			text = ((@int == 0) ? "启用" : "禁用");
			if (!string.IsNullOrEmpty(@string))
			{
				int num = CommonTeam.Instance.PauseCustomer(@string, @int);
				if (num > 0)
				{
					msg = "{\"success\":\"1\",\"Message\":\"" + text + "成功\"}";
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",",
						text,
						"体检号:",
						@string,
						" 成功\r\n"
					}));
				}
				else
				{
					msg = "{\"success\":\"0\",\"Message\":\"" + text + "失败,请重试\"}";
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",",
						text,
						"体检号:",
						@string,
						" 失败 0行受影响\r\n"
					}));
				}
			}
			else
			{
				msg = "{\"success\":\"0\",\"Message\":\"体检号不能为空\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text,
					"体检号:",
					@string,
					" 失败 体检号为空\r\n"
				}));
			}
			this.OutPutMessage(msg);
		}

		public void GetQuickTeamTaskList()
		{
			string @string = base.GetString("InputCode");
			int @int = base.GetInt("CurrSelectedTeamID", 0);
			DataTable quickTeamTaskList = CommonTeam.Instance.GetQuickTeamTaskList(@string, @int);
			string msg = JsonHelperFont.Instance.DataTableToJSON(quickTeamTaskList, quickTeamTaskList.Rows.Count);
			this.OutPutMessage(msg);
		}

		public void GetQuickTeamList()
		{
			string @string = base.GetString("InputCode");
			DataTable quickTeamList = CommonTeam.Instance.GetQuickTeamList(@string);
			string msg = JsonHelperFont.Instance.DataTableToJSON(quickTeamList, quickTeamList.Rows.Count);
			this.OutPutMessage(msg);
		}

		public void ISCanPauseTeamTaskGroup()
		{
			int @int = base.GetInt("ID_TeamTaskGroup", 0);
			string empty = string.Empty;
			string msg = string.Empty;
			if (@int > 0)
			{
				bool flag = CommonTeam.Instance.ISCanPauseTeamTaskGroup(@int);
				if (flag)
				{
					msg = "{\"success\":\"1\",\"Message\":\"任务分组[" + @int + "]可以禁用\"}";
					Log4J.Instance.Error(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取任务分组是否可以禁用(ISCanPauseTeamTaskGroup) 分组ID:",
						@int,
						" 可以禁用\r\n"
					}));
				}
				else
				{
					msg = "{\"success\":\"0\",\"Message\":\"任务分组[" + @int + "]下已有客户进行体检,不可以禁用\"}";
					Log4J.Instance.Error(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取任务分组是否可以禁用(ISCanPauseTeamTaskGroup) 分组ID:",
						@int,
						" 不可以禁用\r\n"
					}));
				}
			}
			else
			{
				msg = "{\"success\":\"0\",\"Message\":\"分组不能为空\"}";
				Log4J.Instance.Error(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取任务分组是否可以禁用(ISCanPauseTeamTaskGroup) 分组ID:",
					@int,
					" 不正确\r\n"
				}));
			}
			this.OutPutMessage(msg);
		}

		public void ISCanPauseCustomer()
		{
			string text = base.GetString("ID_Customer").Trim();
			string empty = string.Empty;
			string msg = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				bool flag = CommonTeam.Instance.ISCanPauseCustomer(text);
				if (flag)
				{
					msg = "{\"success\":\"1\",\"Message\":\"体检号[" + text + "]可以禁用\"}";
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取客户名单是否可以禁用客户(ISCanPauseCustomer) 体检号:",
						text,
						" 可以禁用\r\n"
					}));
				}
				else
				{
					msg = "{\"success\":\"0\",\"Message\":\"体检号[" + text + "]所对应的客户已进行体检,不可以禁用\"}";
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",获取客户名单是否可以禁用客户(ISCanPauseCustomer) 体检号:",
						text,
						" 不可以禁用\r\n"
					}));
				}
			}
			else
			{
				msg = "{\"success\":\"0\",\"Message\":\"客户体检号不能为空\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取客户名单是否可以禁用客户(ISCanPauseCustomer) 体检号:",
					text,
					" 不正确\r\n"
				}));
			}
			this.OutPutMessage(msg);
		}

		public void GetGuideSheetPrintedCustomer()
		{
			string text = base.GetString("ID_Customer").Trim().TrimEnd(new char[]
			{
				','
			});
			string empty = string.Empty;
			string text2 = string.Empty;
			if (!string.IsNullOrEmpty(text))
			{
				text2 = JsonHelperFont.Instance.DataTableToJSON(CommonTeam.Instance.GetGuideSheetPrintedCustomer(text).Tables[0], "dataList");
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取获取已经进行体检的客户(GetGuideSheetPrintedCustomer) 体检号:",
					text2,
					" 不正确\r\n"
				}));
			}
			else
			{
				text2 = "{\"success\":\"0\",\"Message\":\"客户体检号不能为空\"}";
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",获取获取已经进行体检的客户(GetGuideSheetPrintedCustomer) 体检号:",
					text,
					" 不正确\r\n"
				}));
			}
			this.OutPutMessage(text2);
		}

		public void SaveModifyCustomer()
		{
			string msg = string.Empty;
			string text = base.GetString("CustomerID").Trim();
			string text2 = base.GetString("ModifyIDCard").Trim();
			string text3 = base.GetString("ModifyCustomerName").Trim();
			string text4 = base.GetString("ModifyCustomerSex").Trim();
			string text5 = base.GetString("ModifyCustomerSexName").Trim();
			string text6 = base.GetString("ModifyCustomerMarriage").Trim();
			string text7 = base.GetString("ModifyCustomerMarriageName").Trim();
			string text8 = base.GetString("ModifyNation").Trim();
			string text9 = base.GetString("ModifyNationName").Trim();
			string text10 = base.GetString("ModifyBirthDay").Trim();
			string text11 = base.GetString("ModifyMobileNo").Trim();
			string text12 = base.GetString("ModifyRole").Trim();
			string text13 = base.GetString("ModifyDepartment").Trim();
			string text14 = base.GetString("ModifyDepartSubA").Trim();
			string text15 = base.GetString("ModifyDepartSubB").Trim();
			string text16 = base.GetString("ModifyDepartSubC").Trim();
			string text17 = base.GetString("ModifyNote").Trim();
			string text18 = string.Empty;
			if (string.IsNullOrEmpty(text))
			{
				text18 += "客户体检号不能为空@";
			}
			if (string.IsNullOrEmpty(text2))
			{
				text18 += "客户证件号不能为空@";
			}
			else if (text2.Length != 15 && text2.Length != 18)
			{
				text18 += "客户证件号格式不正确@";
			}
			if (string.IsNullOrEmpty(text3))
			{
				text18 += "客户姓名不能为空@";
			}
			if (string.IsNullOrEmpty(text10))
			{
				text18 += "客户出生日期不能为空@";
			}
			if (string.IsNullOrEmpty(text11))
			{
				text18 += "客户联系电话不能为空@";
			}
			if (text18.Length > 0)
			{
				text18 = text18.TrimEnd(new char[]
				{
					'@'
				}).Replace("@", "<br/>");
				msg = "{\"success\":\"0\",\"Message\":\"" + text18 + "\"}";
				this.OutPutMessage(msg);
			}
			else
			{
				List<string> list = new List<string>(1);
				list.Add(string.Format("\r\n--修改体检信息表\r\nUPDATE OnCustPhysicalExamInfo SET Department='{1}',DepartSubA='{2}',DepartSubB='{3}',DepartSubC='{4}',MobileNo='{5}' WHERE ID_Customer='{0}';\r\n--修改团体分组名单信息\r\nUPDATE TeamTaskGroupCust SET Department='{1}',DepartSubA='{2}',DepartSubB='{3}',DepartSubC='{4}' WHERE ID_Customer='{0}';", new object[]
				{
					text,
					text13,
					text14,
					text15,
					text16,
					text11
				}));
				try
				{
					int num = CommonExcuteSql.Instance.ExecuteSqlTran(list);
					if (num > 0)
					{
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",单个修改客户名单信息(SaveModifyCustomer)成功 体检号:",
							text,
							"|ModifyIDCard：",
							text2,
							"|ModifyCustomerName：",
							text3,
							"|ModifyBirthDay：",
							text10,
							"|ModifyMobileNo：",
							text11,
							"|ModifyDepartment：",
							text13,
							"|ModifyDepartSubA：",
							text14,
							"|ModifyDepartSubB：",
							text15,
							"|ModifyDepartSubC：",
							text16,
							"|ModifyNote：",
							text17,
							"\r\n"
						}));
						msg = "{\"success\":\"1\",\"Message\":\"成功修改客户信息\"}";
						this.OutPutMessage(msg);
					}
					else
					{
						Log4J.Instance.Error(string.Concat(new string[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",单个修改客户名单信息(SaveModifyCustomer)不成功，受影响的行数为0 体检号:",
							text,
							"|ModifyIDCard：",
							text2,
							"|ModifyCustomerName：",
							text3,
							"|ModifyBirthDay：",
							text10,
							"|ModifyMobileNo：",
							text11,
							"|ModifyDepartment：",
							text13,
							"|ModifyDepartSubA：",
							text14,
							"|ModifyDepartSubB：",
							text15,
							"|ModifyDepartSubC：",
							text16,
							"|ModifyNote：",
							text17,
							"\r\n"
						}));
						msg = "{\"success\":\"0\",\"Message\":\"受影响的行数为0\"}";
						this.OutPutMessage(msg);
					}
				}
				catch (Exception ex)
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",单个修改客户名单信息(SaveModifyCustomer)失败 体检号:",
						text,
						"|ModifyIDCard：",
						text2,
						"|ModifyCustomerName：",
						text3,
						"|ModifyBirthDay：",
						text10,
						"|ModifyMobileNo：",
						text11,
						"|ModifyDepartment：",
						text13,
						"|ModifyDepartSubA：",
						text14,
						"|ModifyDepartSubB：",
						text15,
						"|ModifyDepartSubC：",
						text16,
						"|ModifyNote：",
						text17,
						" 失败原因：",
						ex.Message,
						"\r\n"
					}));
					msg = "{\"success\":\"0\",\"Message\":\"执行保存失败，请重试！\"}";
					this.OutPutMessage(msg);
				}
			}
		}
	}
}
