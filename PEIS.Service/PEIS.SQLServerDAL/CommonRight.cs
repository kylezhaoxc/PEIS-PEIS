using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class CommonRight : CommonBase, ICommonRight
	{
		protected string[] QueryAllRightItemList_Param = new string[]
		{
            "SELECT [RightID],[RightName],[RightCode],[DispOrder],[Is_Locked],[ParentID],[Remark],[RightLevel],[OperatorID],[CreateDate]   FROM [SYSRight]    ORDER BY [RightLevel] ASC , [RightID] ASC ;  SELECT count(RightID) FirstNodeCount    FROM SYSRight WHERE [ParentID] = 0 ; "
        };

		protected string[] QueryRightUsedCount_Param = new string[]
		{
            "SELECT COUNT(0)   FROM SYSUserRight  Where RightID = @RightID; SELECT COUNT(0)   FROM SysRoleRight  Where RightID = @RightID;  SELECT COUNT(0)   FROM SYSRight     WHERE ParentID = @RightID;"
        };

		protected string[] QueryRightCodeInfo_Param = new string[]
		{
            "SELECT RightID  FROM SYSRight   where RightCode = @RightCode; "
        };

		protected string[] QueryAllRoleItemList_Param = new string[]
		{
            "SELECT [RoleID],[RoleName],[DispOrder],[Is_Locked],[Remark],[CreateDate],[OperatorID],[Is_DefaultRole]  FROM [SYSRole]  ORDER BY DispOrder ASC ; "
        };

		protected string[] QueryRoleNameInfo_Param = new string[]
		{
            "SELECT [RoleID]      FROM [SYSRole]       where RoleName = @RoleName; "
        };

		protected string[] QueryRoleUsedCount_Param = new string[]
		{
            "SELECT COUNT(0) FROM SYSUserRole   Where RoleID = @RoleID; "
        };

		protected string[] QueryRoleAssignedRightItemList_Param = new string[]
		{
            "SELECT RoleRightID ,RoleID,RightID  FROM SysRoleRight  Where RoleID = @RoleID; "
        };

		protected string[] QueryRoleRightListByUserID_Param = new string[]
		{
            "SELECT [RoleID] as role,[RoleName] as name,[DispOrder] as order_id ,0 state  FROM [SysRole]  ORDER BY DispOrder ASC ;", 
          //  -- 查询所有权限 is_leaf : 是否叶子节点  ks：是否科室 \r\n       
            "SELECT [RightID] ,[RightName] as name,[ParentID] as pid ,[RightLevel] ,[DispOrder] as order_id,0 state ,1 is_leaf ,0 ks FROM [SYSRight] WHERE ISNULL(Is_Locked,0) = 0     ORDER BY [RightLevel] ASC , [DispOrder] ASC , RightID ASC ; ",
           // -- 查询所有的角色和权限的对应关系\r\n           
            "SELECT [RoleID] role ,[RightID] right_id  FROM [SysRoleRight];",
           // -- 查询所有的科室\r\n       
           " SELECT [SectionID] ,[SectionName],[FunctionType] ,0 state  FROM [SYSSection]  WHERE  ISNULL([FunctionType],0) = 0  ;",    
        //    -- 查询当前用户的拥有的角色\r\n            
           " SELECT UserRoleID,RoleID,UserID  FROM SYSUserRole  Where UserID = @UserID;",
        //-- 查询当前用户的拥有的权限\r\n            
         "SELECT UserRightID,RightID,UserID  FROM SYSUserRight     Where UserID = @UserID;",
            //-- 查询当前用户的拥有的科室\r\n            
           " SELECT SectionID,SectionName,DispOrder FROM SYSSection WHERE  SectionID IN  (SELECT SectionID  FROM SYSUserSection  WHERE UserID = @UserID)     ORDER BY DispOrder ASC; "
        };

		protected string[] QueryRightListByRoleID_Param = new string[]
		{
			// "\r\n\r\n        -- 查询所有权限 is_leaf : 是否叶子节点\r\n        
            "SELECT [RightID] as right_id,[RightName] as name ,[ParentID] as pid ,[RightLevel] ,[DispOrder] as order_id ,0 state,1 is_leaf FROM [SYSRight]  Where ISNULL(Is_Locked,0) = 0 ORDER BY [RightLevel] ASC , [DispOrder] ASC , RightID ASC ; ",
        //-- 查询当前角色的拥有的权限\r\n          
        "SELECT RoleRightID,RightID ,RoleID  FROM SYSRoleRight  Where RoleID = @RoleID;   "
        };

		protected string[] QueryUserAssignedRoleRightItemList_Param = new string[]
		{
            "SELECT UserRoleID ,RoleID,UserID   FROM SYSUserRole  Where UserID = @UserID;",
        "SELECT ID_UserRight,ID_Right,ID_User  FROM NatUserRight   Where UserID = @UserID;",
        "SELECT SectionID,SectionName,DispOrder FROM SYSSection WHERE SectionID IN  (SELECT SectionID FROM SYSUserSection   WHERE UserID = @UserID)     ORDER BY DispOrder ASC;   "
        };

		protected string[] QueryUserMenuRightSectionList_Param = new string[]
		{
			//"\r\n            -- 1、 以下语句 查询用户具有的所有菜单 \r\n\r\n  \r\n\t\t  
            
           " SELECT DISTINCT [MenuName],[ParentID],[Url] ,[Is_CombineWithSection],[DispOrder] FROM [SYSMenu]  WHERE MenuLevel = @MenuLevel and  RightCode IN ( SELECT RightCode FROM SYSRight WHERE ISNULL(Is_Locked,0) = 0 AND RightID in (SELECT RightID FROM SYSUserRight WHERE UserID = @UserID ))   ORDER BY [DispOrder] ASC;  SELECT [RightID],[RightName],[RightCode],[Is_Locked] ,[ParentID] ,[RightLevel] FROM SYSRight WHERE ISNULL(Is_Locked,0) = 0 AND RightID in (SELECT RightID FROM SYSUserRight WHERE UserID = @UserID ) ;  SELECT [SectionID],[SectionName],[FunctionType] ,[DisplayMenu]   FROM [SYSSection] WHERE FunctionType = 0  AND [SectionID] IN ( SELECT [SectionID] FROM [SYSUserSection] WHERE [UserID] = @UserID  ); SELECT [MenuID],[MenuName],[ParentID] ,[Url],[Is_CombineWithSection],[DispOrder] FROM [SYSMenu] WHERE  [ParentID] = 0 AND MenuLevel = @MenuLevel AND MenuID IN ( SELECT DISTINCT  [ParentID] FROM [SYSMenu] WHERE  RightCode IN ( SELECT RightCode FROM SYSRight where ISNULL(Is_Locked,0) = 0 AND RightID in (select RightID from SYSUserRight where UserID = @UserID ))  ) ORDER BY [DispOrder] ASC;"
        };

		DataTable ICommonRight.GetPage(string pageCode, int pageIndex, int pageSize, out int recordCount, out int pageCount, params SqlConditionInfo[] conditions)
		{
			return base.GetPage(pageCode, pageIndex, pageSize, out recordCount, out pageCount, conditions);
		}

		DataSet ICommonRight.ExcuteQuerySql(string QuerySqlCode, params SqlConditionInfo[] conditions)
		{
			return base.ExcuteQuerySql(QuerySqlCode, conditions);
		}

		int ICommonRight.UpdateUserRoleRightSection(int ID_User, string currRoleIDsStr, string currRightIDsStr, string currSectionIDsStr, int ID_Operator, DateTime OperDate)
		{
			string connectionString = PEIS.DBUtility.PubConstant.ConnectionString;
			int result;
			using (SqlConnection sqlConnection = new SqlConnection(connectionString))
			{
				sqlConnection.Open();
				using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
				{
					SqlCommand sqlCommand = new SqlCommand();
					try
					{
						sqlCommand.Connection = sqlConnection;
						sqlCommand.Transaction = sqlTransaction;
						int num = this.OperateUserRole(sqlCommand, ID_User, currRoleIDsStr, ID_Operator, OperDate);
						num += this.OperateUserRight(sqlCommand, ID_User, currRightIDsStr, ID_Operator, OperDate);
						num += this.OperateUserSection(sqlCommand, ID_User, currSectionIDsStr, ID_Operator, OperDate);
						if (num > 0)
						{
							sqlTransaction.Commit();
						}
						result = num;
					}
					catch
					{
						sqlTransaction.Rollback();
						result = 0;
					}
				}
			}
			return result;
		}

		private int OperateUserRole(SqlCommand cmd, int UserID, string currRoleIDsStr, int ID_Operator, DateTime OperDate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (string.IsNullOrEmpty(currRoleIDsStr))
			{
				stringBuilder.Append(" DELETE from SYSUserRole ");
				stringBuilder.Append(" WHERE UserID = " + UserID.ToString() + "; ");
			}
			else
			{
				stringBuilder.Append(" UPDATE SYSUserRole SET ");
				stringBuilder.Append(" CreateDate='" + OperDate.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				stringBuilder.Append(" ID_Operator=" + ID_Operator + " ");
				stringBuilder.Append(" WHERE UserID=" + UserID.ToString() + "; ");
				stringBuilder.Append(" DELETE from SYSUserRole ");
				stringBuilder.Append(" WHERE UserID = " + UserID.ToString());
				stringBuilder.Append(" AND ID_Role IN (  SELECT [ID_Role]   FROM [SYSUserRole] WHERE UserID = " + UserID.ToString() + " )");
				stringBuilder.Append(string.Concat(new string[]
				{
                    " AND ID_Role NOT IN (  SELECT [ID_Role]   FROM [SYSUserRole] WHERE UserID = ",
                    UserID.ToString(),
					"  AND [ID_Role] IN (",
					currRoleIDsStr,
					") ); "
				}));
				stringBuilder.Append(" INSERT INTO SYSUserRole  ");
				stringBuilder.Append(string.Concat(new string[]
				{
					" SELECT [ID_Role],'",
					OperDate.ToString("yyyy-MM-dd HH:mm:ss"),
					"' CreateDate,",
					ID_Operator.ToString(),
					" ID_Operator,",
                    UserID.ToString(),
                    " UserID "
                }));
				stringBuilder.Append(" FROM NatRole  ");
				stringBuilder.Append(" WHERE ID_Role NOT IN ( SELECT [ID_Role] FROM [SYSUserRole] WHERE UserID = " + UserID.ToString() + " ) ");
				stringBuilder.Append(" AND ID_Role IN (" + currRoleIDsStr + "); ");
			}
			cmd.CommandText = stringBuilder.ToString();
			return cmd.ExecuteNonQuery();
		}

		private int OperateUserRight(SqlCommand cmd, int UserID, string currRightIDsStr, int ID_Operator, DateTime OperDate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (string.IsNullOrEmpty(currRightIDsStr))
			{
				stringBuilder.Append(" DELETE from SYSUserRole ");
				stringBuilder.Append(" WHERE UserID = " + UserID.ToString() + "; ");
			}
			else
			{
				stringBuilder.Append(" UPDATE SYSUserRole SET ");
				stringBuilder.Append(" CreateDate='" + OperDate.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				stringBuilder.Append(" ID_Operator=" + ID_Operator + " ");
				stringBuilder.Append(" WHERE UserID=" + UserID.ToString() + "; ");
				stringBuilder.Append(" DELETE FROM NatUserRight ");
				stringBuilder.Append(" WHERE ID_User = " + UserID.ToString());
				stringBuilder.Append(" AND ID_Right IN (  SELECT [ID_Right]   FROM [NatUserRight] WHERE UserID = " + UserID.ToString() + " )");
				stringBuilder.Append(string.Concat(new string[]
				{
                    " AND ID_Right NOT IN (  SELECT [ID_Right]   FROM [NatUserRight] WHERE UserID = ",
                    UserID.ToString(),
					"  AND [ID_Right] IN (",
					currRightIDsStr,
					") ); "
				}));
				stringBuilder.Append(" INSERT INTO NatUserRight  ");
				stringBuilder.Append(string.Concat(new string[]
				{
					" SELECT [ID_Right],'",
					OperDate.ToString("yyyy-MM-dd HH:mm:ss"),
					"' CreateDate,",
					ID_Operator.ToString(),
					" ID_Operator,",
                    UserID.ToString(),
					" ID_User "
				}));
				stringBuilder.Append(" FROM NatRight  ");
				stringBuilder.Append(" WHERE ID_Right NOT IN ( SELECT [ID_Right] FROM [NatUserRight] WHERE UserID = " + UserID.ToString() + " ) ");
				stringBuilder.Append(" AND ID_Right IN (" + currRightIDsStr + "); ");
			}
			cmd.CommandText = stringBuilder.ToString();
			return cmd.ExecuteNonQuery();
		}

		int ICommonRight.OperateRoleRight(int ID_Role, string currRightIDsStr, int ID_Operator, DateTime OperDate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int result;
			if (string.IsNullOrEmpty(currRightIDsStr))
			{
				result = 0;
			}
			else
			{
				stringBuilder.Append(" UPDATE NatRoleRight SET ");
				stringBuilder.Append(" CreateDate='" + OperDate.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				stringBuilder.Append(" ID_Operator=" + ID_Operator + " ");
				stringBuilder.Append(" WHERE ID_Role=" + ID_Role.ToString() + "; ");
				stringBuilder.Append(" DELETE FROM NatRoleRight ");
				stringBuilder.Append(" WHERE ID_Role = " + ID_Role.ToString());
				stringBuilder.Append(" AND ID_Right IN (  SELECT [ID_Right]   FROM [NatRoleRight] WHERE ID_Role = " + ID_Role.ToString() + " )");
				stringBuilder.Append(string.Concat(new string[]
				{
					" AND ID_Right NOT IN (  SELECT [ID_Right]   FROM [NatRoleRight] WHERE ID_Role = ",
					ID_Role.ToString(),
					"  AND [ID_Right] IN (",
					currRightIDsStr,
					") ); "
				}));
				stringBuilder.Append(" INSERT INTO NatRoleRight  ");
				stringBuilder.Append(string.Concat(new string[]
				{
					" SELECT ",
					ID_Role.ToString(),
					" ID_Role, [ID_Right],'",
					OperDate.ToString("yyyy-MM-dd HH:mm:ss"),
					"' CreateDate,",
					ID_Operator.ToString(),
					" ID_Operator "
				}));
				stringBuilder.Append(" FROM NatRight  ");
				stringBuilder.Append(" WHERE ID_Right NOT IN ( SELECT [ID_Right] FROM [NatRoleRight] WHERE ID_Role = " + ID_Role.ToString() + " ) ");
				stringBuilder.Append(" AND ID_Right IN (" + currRightIDsStr + "); ");
				int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
				result = num;
			}
			return result;
		}

		private int OperateUserSection(SqlCommand cmd, int ID_User, string currSectionIDsStr, int ID_Operator, DateTime OperDate)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (string.IsNullOrEmpty(currSectionIDsStr))
			{
				stringBuilder.Append(" DELETE from NatUserSection ");
				stringBuilder.Append(" WHERE ID_User = " + ID_User.ToString() + "; ");
			}
			else
			{
				stringBuilder.Append(" UPDATE NatUserSection SET ");
				stringBuilder.Append(" CreateDate='" + OperDate.ToString("yyyy-MM-dd HH:mm:ss") + "',");
				stringBuilder.Append(" ID_Operator=" + ID_Operator + " ");
				stringBuilder.Append(" WHERE ID_User=" + ID_User.ToString() + "; ");
				stringBuilder.Append(" DELETE FROM NatUserSection ");
				stringBuilder.Append(" WHERE ID_User = " + ID_User.ToString());
				stringBuilder.Append(" AND ID_Section IN (  SELECT [ID_Section]   FROM [NatUserSection] WHERE ID_User = " + ID_User.ToString() + " )");
				stringBuilder.Append(string.Concat(new string[]
				{
					" AND ID_Section NOT IN (  SELECT [ID_Section]   FROM [NatUserSection] WHERE ID_User = ",
					ID_User.ToString(),
					"  AND [ID_Section] IN (",
					currSectionIDsStr,
					") ); "
				}));
				stringBuilder.Append(" INSERT INTO NatUserSection  ");
				stringBuilder.Append(string.Concat(new string[]
				{
					" SELECT [ID_Section],'",
					OperDate.ToString("yyyy-MM-dd HH:mm:ss"),
					"' CreateDate,",
					ID_Operator.ToString(),
					" ID_Operator,",
					ID_User.ToString(),
					" ID_User "
				}));
				stringBuilder.Append(" FROM SYSSection  ");
				stringBuilder.Append(" WHERE ID_Section NOT IN ( SELECT [ID_Section] FROM [NatUserSection] WHERE ID_User = " + ID_User.ToString() + " ) ");
				stringBuilder.Append(" AND ID_Section IN (" + currSectionIDsStr + "); ");
			}
			cmd.CommandText = stringBuilder.ToString();
			return cmd.ExecuteNonQuery();
		}

		int ICommonRight.DeleteRole(int ID_Role)
		{
			StringBuilder stringBuilder = new StringBuilder();
			int result;
			if (ID_Role <= 0)
			{
				result = 0;
			}
			else
			{
				stringBuilder.Append("delete from NatRoleRight ");
				stringBuilder.Append(" where ID_Role=" + ID_Role.ToString() + "; ");
				stringBuilder.Append("delete from NatRole ");
				stringBuilder.Append(" where ID_Role=" + ID_Role.ToString() + "; ");
				string connectionString = PEIS.DBUtility.PubConstant.ConnectionString;
				using (SqlConnection sqlConnection = new SqlConnection(connectionString))
				{
					sqlConnection.Open();
					using (SqlTransaction sqlTransaction = sqlConnection.BeginTransaction())
					{
						SqlCommand sqlCommand = new SqlCommand();
						try
						{
							sqlCommand.Connection = sqlConnection;
							sqlCommand.Transaction = sqlTransaction;
							sqlCommand.CommandText = stringBuilder.ToString();
							int num = sqlCommand.ExecuteNonQuery();
							if (num > 0)
							{
								sqlTransaction.Commit();
							}
							result = num;
						}
						catch
						{
							sqlTransaction.Rollback();
							result = 0;
						}
					}
				}
			}
			return result;
		}

		protected new string[] GetSqlSentence(string PageName)
		{
			FieldInfo field = base.GetType().GetField(PageName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.NonPublic);
			if (field == null)
			{
				throw new Exception("没有找到SQL");
			}
			return (string[])field.GetValue(this);
		}
	}
}
