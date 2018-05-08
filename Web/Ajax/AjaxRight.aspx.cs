using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using PEIS.Model;
using System;
using System.Data;
using System.Reflection;

namespace PEIS.Web.Ajax
{
	public class AjaxRight : BasePage
	{
		public string ErrorMessage = string.Empty;

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

		public void GetAllRightItemList()
		{
			SqlConditionInfo[] conditions = null;
			string querySqlCode = "QueryAllRightItemList_Param";
			try
			{
				DataSet ds = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			catch (Exception ex)
			{
				this.OutPutMessage(ex.Message);
			}
		}

		public void SaveRightNodeItem()
		{
			int num = 0;
			int @int = base.GetInt("type", 0);
			int num2 = base.GetInt("txtRightID", 0);
			int num3 = base.GetInt("txtCurrentLevel", 0);
			string @string = base.GetString("txtRightName");
			string string2 = base.GetString("txtRightCode");
			int value = base.GetInt("txtParentRightID", 0);
			int int2 = base.GetInt("txtDispOrder", 0);
			int int3 = base.GetInt("IsLockedRight", 0);
			switch (@int)
			{
			case 1:
				num3 = 1;
				value = 0;
				num2 = 0;
				break;
			case 2:
				num3++;
				value = num2;
				num2 = 0;
				break;
			case 3:
				break;
			default:
				num = -2;
				break;
			}
			if (num >= 0)
			{
				PEIS.Model.SYSRight sysRight = new PEIS.Model.SYSRight();
                sysRight.RightID = num2;
                sysRight.RightLevel = new int?(num3);
                sysRight.RightName = Input.URLDecode(@string);
                sysRight.RightCode = Input.URLDecode(string2);
                sysRight.DispOrder = int2;
                sysRight.Is_Locked = int3;
                sysRight.OperatorID = new int?(this.LoginUserModel.UserID);
                sysRight.CreateDate = DateTime.Now;
                sysRight.ParentID = new int?(value);
				SqlConditionInfo[] conditions = new SqlConditionInfo[]
				{
					new SqlConditionInfo("@RightCode", sysRight.RightCode, TypeCode.String)
				};
				string querySqlCode = "QueryRightCodeInfo_Param";
				try
				{
					DataSet dataSet = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
					int count = dataSet.Tables[0].Rows.Count;
					switch (@int)
					{
					case 1:
					case 2:
						if (count > 0)
						{
							num = -1;
						}
						else
						{
							num = PEIS.BLL.SYSRight.Instance.Add(sysRight);
							if (num > 0)
							{
								Log4J.Instance.Info(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",新增权限 权限名称：",
                                    sysRight.RightName,
									",ID:",
									num,
									",权限值:",
                                    sysRight.RightCode
								}));
							}
							else
							{
								Log4J.Instance.Error(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",新增权限失败 权限名称：",
                                    sysRight.RightName,
									",ID:",
									num,
									",权限值:",
                                    sysRight.RightCode
								}));
							}
						}
						break;
					case 3:
						if (count > 0 && int.Parse(dataSet.Tables[0].Rows[0][0].ToString()) != sysRight.RightID)
						{
							num = -1;
						}
						else
						{
							num = (PEIS.BLL.SYSRight.Instance.Update(sysRight) ? 1 : 0);
							if (num > 0)
							{
								Log4J.Instance.Info(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",修改权限 权限名称：",
                                    sysRight.RightName,
									",ID:",
                                    sysRight.RightID,
									",权限值:",
                                    sysRight.RightCode
								}));
							}
							else
							{
								Log4J.Instance.Error(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",修改权限失败 权限名称：",
                                    sysRight.RightName,
									",ID:",
                                    sysRight.RightID,
									",权限值:",
                                    sysRight.RightCode
								}));
							}
						}
						break;
					default:
						num = -2;
						break;
					}
				}
				catch (Exception ex)
				{
					num = 0;
				}
			}
			this.OutPutMessage(num.ToString());
		}

		public void DeleteRightNodeItem()
		{
			int @int = base.GetInt("txtRightID", 0);
			int num = 0;
			int num2 = 0;
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@RightID", @int, TypeCode.Int32)
			};
			string querySqlCode = "QueryRightUsedCount_Param";
			try
			{
				DataSet dataSet = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
				num = int.Parse(dataSet.Tables[0].Rows[0][0].ToString());
				num += int.Parse(dataSet.Tables[1].Rows[0][0].ToString());
				num2 = int.Parse(dataSet.Tables[2].Rows[0][0].ToString());
			}
			catch (Exception ex)
			{
			}
			int num3;
			if (num > 0)
			{
				num3 = -1;
			}
			else if (num2 > 0)
			{
				num3 = -2;
			}
			else
			{
				num3 = (PEIS.BLL.SYSRight.Instance.Delete(@int) ? 1 : 0);
				if (num3 > 0)
				{
					Log4J.Instance.Info(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",删除权限 ID:",
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
						",删除权限失败 ID:",
						@int
					}));
				}
			}
			this.OutPutMessage(num3.ToString());
		}

		public void GetAllRoleItemList()
		{
			SqlConditionInfo[] conditions = null;
			string querySqlCode = "QueryAllRoleItemList_Param";
			try
			{
				DataSet ds = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			catch (Exception ex)
			{
				this.OutPutMessage(ex.Message);
			}
		}

		public void SaveRoleNodeItem()
		{
			int num = 0;
			int @int = base.GetInt("type", 0);
			int int2 = base.GetInt("txtRoleID", 0);
			string @string = base.GetString("txtRoleName");
			int int3 = base.GetInt("txtDispOrder", 0);
			int int4 = base.GetInt("IsLockedRole", 0);
			string string2 = base.GetString("currRightIDsStr");
			switch (@int)
			{
			case 1:
			case 2:
				break;
			default:
				num = -2;
				break;
			}
			if (num >= 0)
			{
				PEIS.Model.SysRole sysRole = new PEIS.Model.SysRole();
				if (@int == 2)
				{
					if (int2 > 0)
					{
                        sysRole = PEIS.BLL.SysRole.Instance.GetModel(int2);
					}
                    sysRole.RoleID = int2;
				}
                sysRole.RoleName = Input.URLDecode(@string);
                sysRole.DispOrder = int3;
                sysRole.Is_Locked = int4;
                sysRole.OperatorID = new int?(this.LoginUserModel.UserID);
                sysRole.CreateDate = new DateTime?(DateTime.Now);
				SqlConditionInfo[] conditions = new SqlConditionInfo[]
				{
					new SqlConditionInfo("@RoleName", sysRole.RoleName, TypeCode.String)
				};
				string querySqlCode = "QueryRoleNameInfo_Param";
				try
				{
					DataSet dataSet = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
					int count = dataSet.Tables[0].Rows.Count;
					switch (@int)
					{
					case 1:
						if (count > 0)
						{
							num = -1;
						}
						else
						{
                                sysRole.Is_DefaultRole = new int?(0);
							num = PEIS.BLL.SysRole.Instance.Add(sysRole);
							if (num > 0)
							{
								Log4J.Instance.Info(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",新增角色 角色名称:",
                                    sysRole.RoleName,
									",ID_Role:",
									num
								}));
							}
							else
							{
								Log4J.Instance.Error(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",新增角色失败 角色名称:",
                                    sysRole.RoleName,
									",ID_Role:",
									num
								}));
							}
						}
						break;
					case 2:
						if (count > 0 && int.Parse(dataSet.Tables[0].Rows[0][0].ToString()) != sysRole.RoleID)
						{
							num = -1;
						}
						else
						{
							num = (PEIS.BLL.SysRole.Instance.Update(sysRole) ? 1 : 0);
							if (num > 0)
							{
								Log4J.Instance.Info(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",修改角色 角色名称:",
                                    sysRole.RoleName,
									",ID_Role:",
                                    sysRole.RoleID
								}));
							}
							else
							{
								Log4J.Instance.Error(string.Concat(new object[]
								{
									Public.GetClientIP(),
									",",
									this.LoginUserModel.UserName,
									",修改角色失败 角色名称:",
                                    sysRole.RoleName,
									",ID_Role:",
                                    sysRole.RoleID
								}));
							}
						}
						break;
					default:
						num = -2;
						break;
					}
					if (num > 0)
					{
						int iD_Role;
						if (@int == 2)
						{
							iD_Role = int2;
						}
						else
						{
							iD_Role = num;
						}
						num = 1 + CommonRight.Instance.OperateRoleRight(iD_Role, string2, this.LoginUserModel.UserID, sysRole.CreateDate.Value);
					}
				}
				catch (Exception ex)
				{
					Log4J.Instance.Error(string.Concat(new string[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",角色 错误信息:",
						ex.Message
					}));
					num = 0;
				}
			}
			this.OutPutMessage(num.ToString());
		}

		public void SaveRoleRightRel()
		{
			int num = 0;
			int @int = base.GetInt("txtRoleID", 0);
			string @string = base.GetString("currRightIDsStr");
			try
			{
				PEIS.Model.SysRole model = PEIS.BLL.SysRole.Instance.GetModel(@int);
				num = CommonRight.Instance.OperateRoleRight(@int, @string, this.LoginUserModel.UserID, model.CreateDate.Value);
			}
			catch (Exception ex)
			{
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",角色 错误信息:",
					ex.Message
				}));
				num = 0;
			}
			this.OutPutMessage(num.ToString());
		}

		public void DeleteRoleNodeItem()
		{
			int @int = base.GetInt("txtRoleID", 0);
			int num = 0;
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Role", @int, TypeCode.Int32)
			};
			string querySqlCode = "QueryRoleUsedCount_Param";
			try
			{
				DataSet dataSet = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
				num = int.Parse(dataSet.Tables[0].Rows[0][0].ToString());
			}
			catch (Exception ex)
			{
				Log4J.Instance.Error(string.Concat(new string[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",删除角色 错误信息:",
					ex.Message
				}));
			}
			int num2;
			if (num > 0)
			{
				num2 = -1;
			}
			else
			{
				PEIS.Model.SysRole model = PEIS.BLL.SysRole.Instance.GetModel(@int);
				if (model.Is_DefaultRole == 1)
				{
					num2 = -2;
				}
				else
				{
					num2 = CommonRight.Instance.DeleteRole(@int);
					if (num2 > 0)
					{
						Log4J.Instance.Info(string.Concat(new object[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",删除角色 ID_Role:",
							model.RoleID
						}));
					}
					else
					{
						Log4J.Instance.Error(string.Concat(new object[]
						{
							Public.GetClientIP(),
							",",
							this.LoginUserModel.UserName,
							",删除角色失败 ID_Role:",
							model.RoleID
                        }));
					}
				}
			}
			this.OutPutMessage(num2.ToString());
		}

		public void GetRoleAssignedRightItemList()
		{
			int RoleID = base.GetInt("RoleID", 0);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@RoleID", RoleID, TypeCode.Int32)
			};
			string querySqlCode = "QueryRoleAssignedRightItemList_Param";
			try
			{
				DataSet ds = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			catch (Exception e)
			{
				this.OutPutMessage(e.Message);
			}
		}

		public void GetRoleRightListByUserID()
		{
			int UserID = base.GetInt("UserID", 0);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@UserID", UserID, TypeCode.Int32)
			};
			string querySqlCode = "QueryRoleRightListByUserID_Param";
			try
			{
				DataSet dataSet = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
				dataSet = this.MergeUserRoleRightState(dataSet);
				DataSet dataSet2 = new DataSet();
				dataSet2.Merge(dataSet.Tables[0]);
				dataSet2.Merge(dataSet.Tables[1]);
				dataSet2.Merge(dataSet.Tables[2]);
				string msg = JsonHelperFont.Instance.DataSetToJSON(dataSet2);
				this.OutPutMessage(msg);
			}
			catch (Exception ex)
			{
				this.OutPutMessage(ex.Message);
			}
		}

		private DataSet MergeUserRoleRightState(DataSet ds)
		{
			DataRow[] array;
			DataRow[] array2;
			foreach (DataRow dataRow in ds.Tables[4].Rows)
			{
				
				array = ds.Tables[0].Select(" role = " + dataRow["ID_Role"].ToString());
				array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow2 = array2[i];
					if (dataRow2["role"].ToString() == dataRow["ID_Role"].ToString())
					{
						dataRow2["state"] = 1;
					}
				}
			}
			array2 = ds.Tables[5].Select();
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				array = ds.Tables[1].Select(" ID_Right = " + dataRow["ID_Right"].ToString());
				DataRow[] array3 = array;
				for (int j = 0; j < array3.Length; j++)
				{
					DataRow dataRow2 = array3[j];
					if (dataRow2["ID_Right"].ToString() == dataRow["ID_Right"].ToString())
					{
						dataRow2["state"] = 1;
					}
				}
			}
			foreach (DataRow dataRow2 in ds.Tables[1].Rows)
			{
				
				array = ds.Tables[1].Select(" is_leaf = 1 and pid = " + dataRow2["ID_Right"].ToString());
				array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow3 = array2[i];
					dataRow2["is_leaf"] = "0";
				}
			}
			int num = 100;
			array = ds.Tables[1].Select(" ID_Right = " + num.ToString());
			array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow3 = array2[i];
				dataRow3["is_leaf"] = "0";
			}
			ds.Tables[1].AcceptChanges();
			ds.Tables[1].Columns.Add("right_id", typeof(string));
			foreach (DataRow dataRow2 in ds.Tables[1].Rows)
			{
				
				dataRow2["right_id"] = dataRow2["ID_Right"].ToString();
			}
			foreach (DataRow dataRow2 in ds.Tables[6].Rows)
			{
				
				array = ds.Tables[3].Select(" ID_Section = " + dataRow2["ID_Section"].ToString());
				array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow3 = array2[i];
					dataRow3["state"] = "1";
				}
			}
			ds.Tables[3].AcceptChanges();
			foreach (DataRow dataRow2 in ds.Tables[3].Rows)
			{
				DataRow dataRow4 = ds.Tables[1].NewRow();
				
				dataRow4["right_id"] = dataRow2["ID_Section"].ToString() + "k";
				dataRow4["pid"] = num.ToString();
				dataRow4["name"] = dataRow2["SectionName"].ToString();
				dataRow4["CurrentLevel"] = "99";
				dataRow4["state"] = dataRow2["state"].ToString();
				dataRow4["order_id"] = dataRow2["DispOrder"].ToString();
				dataRow4["is_leaf"] = "1";
				dataRow4["ks"] = "1";
				ds.Tables[1].Rows.Add(dataRow4);
			}
			ds.Tables[2].Columns.Add("is_leaf", typeof(string));
			foreach (DataRow dataRow2 in ds.Tables[2].Rows)
			{
			
				array = ds.Tables[1].Select(" ID_Right = " + dataRow2["right_id"].ToString());
				array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow3 = array2[i];
					dataRow2["is_leaf"] = dataRow3["is_leaf"].ToString();
				}
			}
			array = ds.Tables[2].Select(" right_id = " + num.ToString());
			array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow3 = array2[i];
				dataRow3["is_leaf"] = "0";
			}
			ds.AcceptChanges();
			return ds;
		}

		public void GetUserAssignedRoleRightItemList()
		{
			int @int = base.GetInt("UserID", 0);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_User", @int, TypeCode.Int32)
			};
			string querySqlCode = "QueryUserAssignedRoleRightItemList_Param";
			try
			{
				DataSet ds = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			catch (Exception e)
			{
				this.OutPutMessage("");
			}
		}

		public void SaveUserRoleRightSectionItem()
		{
			int @int = base.GetInt("UserID", 0);
			string @string = base.GetString("currRoleIDsStr");
			string string2 = base.GetString("currRightIDsStr");
			string text = base.GetString("currSectionIDsStr");
			text = text.ToLower().Replace("k", "");
			int iD_User = this.LoginUserModel.UserID;
			DateTime now = DateTime.Now;
			try
			{
				int num = CommonRight.Instance.UpdateUserRoleRightSection(@int, @string, string2, text, iD_User, now);
				if (num > 0)
				{
					Log4J.Instance.Info(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",保存用户与角色，权限的对应关系 ID_User:",
						@int,
						",RoleIDsStr:",
						@string,
						",RightIDsStr：",
						string2,
						",SectionIDsStr:",
						text
					}));
				}
				else
				{
					Log4J.Instance.Error(string.Concat(new object[]
					{
						Public.GetClientIP(),
						",",
						this.LoginUserModel.UserName,
						",保存用户与角色失败，权限的对应关系 ID_User:",
						@int,
						",RoleIDsStr:",
						@string,
						",RightIDsStr：",
						string2,
						",SectionIDsStr:",
						text
					}));
				}
				this.OutPutMessage(num.ToString());
			}
			catch (Exception ex)
			{
				this.OutPutMessage(ex.Message);
			}
			DataTable quickSectionList = CommonSystemInfo.Instance.GetQuickSectionList("");
			if (quickSectionList != null)
			{
				foreach (DataRow dataRow in quickSectionList.Rows)
				{
					base.ClearCache_User(int.Parse(dataRow["ID_Section"].ToString()));
				}
			}
			base.ClearCache_AllStatsUserInfo();
		}

		public void GetUserMenuRightSectionList()
		{
			int num = 1;
			try
			{
				num = int.Parse(BaseConfig.MenuModuleType);
			}
			catch (Exception ex)
			{
				num = 1;
			}
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@UserID", this.LoginUserModel.UserID, TypeCode.Int32),
				new SqlConditionInfo("@MenuLevel", num, TypeCode.Int16)
			};
			string querySqlCode = "QueryUserMenuRightSectionList_Param";
			try
			{
				DataSet ds = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			catch (Exception ex)
			{
				this.OutPutMessage(ex.Message);
			}
		}

		public void SearchRoleList()
		{
			int @int = base.GetInt("pageIndex", 0);
			int int2 = base.GetInt("pageSize", 10);
			int totalCount = 0;
			int num = 0;
			string text = base.GetString("SearchRoleKeyword").Trim();
			SqlConditionInfo[] array = null;
			string pageCode = "QueryPagesRoleList_Param";
			if (!string.IsNullOrEmpty(text))
			{
				array = new SqlConditionInfo[1];
				pageCode = "QueryPagesRoleListParamByName";
				array[0] = new SqlConditionInfo("@RoleName", text, TypeCode.String);
				array[0].Blur = 3;
				array[0].Place = 2;
			}
			DataTable page = CommonConfig.Instance.GetPage(pageCode, @int, int2, out totalCount, out num, array);
			string msg = JsonHelperFont.Instance.DataTableToJSON(page, totalCount);
			this.OutPutMessage(msg);
		}

		public void GetFeeDetailListByRole()
		{
			int @int = base.GetInt("RoleID", 0);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Role", @int, TypeCode.Int32)
			};
			string querySqlCode = "QueryFeeDetailListByRole_Param";
			try
			{
				DataSet ds = CommonConfig.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			catch (Exception e)
			{
				this.OutPutMessage("");
			}
		}

		public void SaveRoleInfo()
		{
			int RoleID = base.GetInt("RoleID", 0);
			string text = base.GetString("RoleName");
			text = Input.URLDecode(text);
			int int2 = base.GetInt("DispOrder", 500);
			int arg_47_0 = (base.GetInt("Is_DefaultRole", 0) == 1) ? 1 : 0;
			string text2 = base.GetString("Remark");
			text2 = Input.URLDecode(text2);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@RoleName", text, TypeCode.String)
			};
			string querySqlCode = "QueryRoleNameIsExis_Param";
			try
			{
				DataSet dataSet = CommonConfig.Instance.ExcuteQuerySql(querySqlCode, conditions);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					if (RoleID <= 0 || dataSet.Tables[0].Rows.Count != 1 || int.Parse(dataSet.Tables[0].Rows[0][0].ToString()) != RoleID)
					{
						this.OutPutMessage("-1".ToString());
						return;
					}
				}
			}
			catch (Exception ex)
			{
				return;
			}
			PEIS.Model.SysRole sysRole;
			if (RoleID <= 0)
			{
                sysRole = new PEIS.Model.SysRole();
			}
			else
			{
                sysRole = PEIS.BLL.SysRole.Instance.GetModel(RoleID);
			}
            sysRole.RoleID = RoleID;
			if (RoleID <= 0)
			{
                sysRole.CreateDate = new DateTime?(DateTime.Now);
			}
            sysRole.RoleName = text;
            sysRole.DispOrder = int2;
            sysRole.Remark = text2;
            sysRole.OperatorID = new int?(this.LoginUserModel.UserID);
			int num = CommonConfig.Instance.SaveRole(sysRole);
			int num2 = (RoleID > 0) ? RoleID : num;
			string text3 = (RoleID > 0) ? "修改" : "新增";
			if (num > 0)
			{
				Log4J.Instance.Info(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text3,
					"角色 名称：",
					text,
					",编号：",
					num2
				}));
			}
			else
			{
				Log4J.Instance.Error(string.Concat(new object[]
				{
					Public.GetClientIP(),
					",",
					this.LoginUserModel.UserName,
					",",
					text3,
					"角色失败 名称：",
					text,
					",编号：",
					num2
				}));
			}
			this.OutPutMessage(num.ToString());
		}

		public void GetSingleNatRoleInfo()
		{
			int @int = base.GetInt("ID_Role", 0);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Role", @int, TypeCode.Int32)
			};
			string querySqlCode = "QuerySingleNatRole_Param";
			try
			{
				DataSet ds = CommonConfig.Instance.ExcuteQuerySql(querySqlCode, conditions);
				string msg = JsonHelperFont.Instance.DataSetToJSON(ds);
				this.OutPutMessage(msg);
			}
			catch (Exception e)
			{
				this.OutPutMessage("");
			}
		}

		public void GetRightListByRoleID()
		{
			int @int = base.GetInt("RoleID", 0);
			SqlConditionInfo[] conditions = new SqlConditionInfo[]
			{
				new SqlConditionInfo("@ID_Role", @int, TypeCode.Int32)
			};
			string querySqlCode = "QueryRightListByRoleID_Param";
			try
			{
				DataSet dataSet = CommonRight.Instance.ExcuteQuerySql(querySqlCode, conditions);
				dataSet = this.MergeRoleRightState(dataSet);
				DataSet dataSet2 = new DataSet();
				dataSet2.Merge(dataSet.Tables[0]);
				string msg = JsonHelperFont.Instance.DataSetToJSON(dataSet2);
				this.OutPutMessage(msg);
			}
			catch (Exception ex)
			{
				this.OutPutMessage(ex.Message);
			}
		}

		private DataSet MergeRoleRightState(DataSet ds)
		{
			DataRow[] array = ds.Tables[1].Select();
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dataRow = array[i];
				DataRow[] array2 = ds.Tables[0].Select(" right_id = " + dataRow["ID_Right"].ToString());
				DataRow[] array3 = array2;
				for (int j = 0; j < array3.Length; j++)
				{
					DataRow dataRow2 = array3[j];
					if (dataRow2["right_id"].ToString() == dataRow["ID_Right"].ToString())
					{
						dataRow2["state"] = 1;
					}
				}
			}
			foreach (DataRow dataRow2 in ds.Tables[0].Rows)
			{
				
				DataRow[] array2 = ds.Tables[0].Select(" is_leaf = 1 and pid = " + dataRow2["right_id"].ToString());
				array = array2;
				for (int i = 0; i < array.Length; i++)
				{
					DataRow dataRow3 = array[i];
					dataRow2["is_leaf"] = "0";
				}
			}
			ds.Tables[1].AcceptChanges();
			ds.AcceptChanges();
			return ds;
		}
	}
}
