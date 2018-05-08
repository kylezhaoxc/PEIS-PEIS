using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class SYSOpUser : ISYSOpUser
    {
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("UserID", "SYSOpUser");
		}

		public bool Exists(int UserID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from SYSOpUser");
			stringBuilder.Append(" where UserID=@UserID ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@UserID", SqlDbType.Int, 4)
			};
			array[0].Value = UserID;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.SYSOpUser model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into SYSOpUser(");
			stringBuilder.Append("ID_Section,UserName,LoginName,PassWord,CreateTime,LastLoginTime,TotalNum,FailCount,OperateLevel,Note,DisDate,Signature,DisCountRate,sex,Is_Del,VocationType)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@SectionID,@UserName,@LoginName,@PassWord,@CreateTime,@LastLoginTime,@TotalNum,@FailCount,@OperateLevel,@Note,@DisDate,@Signature,@DisCountRate,@sex,@Is_Del,@VocationType)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@SectionID", SqlDbType.Int, 4),
				new SqlParameter("@UserName", SqlDbType.VarChar, 256),
				new SqlParameter("@LoginName", SqlDbType.VarChar, 30),
				new SqlParameter("@PassWord", SqlDbType.VarChar, 256),				
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
				new SqlParameter("@TotalNum", SqlDbType.Int, 4),
				new SqlParameter("@FailCount", SqlDbType.Int, 4),
				new SqlParameter("@OperateLevel", SqlDbType.Int, 4),
				new SqlParameter("@Note", SqlDbType.VarChar, 200),
				new SqlParameter("@DisDate", SqlDbType.DateTime),
				new SqlParameter("@Signature", SqlDbType.Image),
				new SqlParameter("@DisCountRate", SqlDbType.Int, 4),
				new SqlParameter("@sex", SqlDbType.Int, 4),
				new SqlParameter("@Is_Del", SqlDbType.Int, 4),
				new SqlParameter("@VocationType", SqlDbType.Int, 4)
			};
			array[0].Value = model.SectionID;
			array[1].Value = model.UserName;
			array[2].Value = model.LoginName;
			array[3].Value = model.PassWord;			
			array[4].Value = model.CreateTime;
			array[5].Value = model.LastLoginTime;
			array[6].Value = model.TotalNum;
			array[7].Value = model.FailCount;
			array[8].Value = model.OperateLevel;
			array[9].Value = model.Note;
			array[10].Value = model.DisDate;
			array[11].Value = model.Signature;
			array[12].Value = model.DisCountRate;
			array[13].Value = model.Sex;
			array[14].Value = model.Is_Del;
			array[15].Value = model.VocationType;
			object single = DbHelperSQL.GetSingle(stringBuilder.ToString(), array);
			int result;
			if (single == null)
			{
				result = 0;
			}
			else
			{
				result = Convert.ToInt32(single);
			}
			return result;
		}

		public bool Update(PEIS.Model.SYSOpUser model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update SYSOpUser set ");
			stringBuilder.Append("SectionID=@SectionID,");
			stringBuilder.Append("UserName=@UserName,");
			stringBuilder.Append("LoginName=@LoginName,");
			stringBuilder.Append("PassWord=@PassWord,");		
			stringBuilder.Append("CreateTime=@CreateTime,");
			stringBuilder.Append("LastLoginTime=@LastLoginTime,");
			stringBuilder.Append("TotalNum=@TotalNum,");
			stringBuilder.Append("FailCount=@FailCount,");
			stringBuilder.Append("OperateLevel=@OperateLevel,");
			stringBuilder.Append("Note=@Note,");
			stringBuilder.Append("DisDate=@DisDate,");
			stringBuilder.Append("Signature=@Signature,");
			stringBuilder.Append("DisCountRate=@DisCountRate,");
			stringBuilder.Append("sex=@sex,");
			stringBuilder.Append("Is_Del=@Is_Del,");
			stringBuilder.Append("VocationType=@VocationType");
			stringBuilder.Append(" where UserID=@UserID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@SectionID", SqlDbType.Int, 4),
				new SqlParameter("@UserName", SqlDbType.VarChar, 256),
				new SqlParameter("@LoginName", SqlDbType.VarChar, 30),
				new SqlParameter("@PassWord", SqlDbType.VarChar, 256),
				new SqlParameter("@CreateTime", SqlDbType.DateTime),
				new SqlParameter("@LastLoginTime", SqlDbType.DateTime),
				new SqlParameter("@TotalNum", SqlDbType.Int, 4),
				new SqlParameter("@FailCount", SqlDbType.Int, 4),
				new SqlParameter("@OperateLevel", SqlDbType.Int, 4),
				new SqlParameter("@Note", SqlDbType.VarChar, 200),
				new SqlParameter("@DisDate", SqlDbType.DateTime),
				new SqlParameter("@Signature", SqlDbType.Image),
				new SqlParameter("@DisCountRate", SqlDbType.Int, 4),
				new SqlParameter("@sex", SqlDbType.Int, 4),
				new SqlParameter("@Is_Del", SqlDbType.Int, 4),
				new SqlParameter("@VocationType", SqlDbType.Int, 4),
				new SqlParameter("@UserID", SqlDbType.Int, 4)
			};
			array[0].Value = model.SectionID;
			array[1].Value = model.UserName;
			array[2].Value = model.LoginName;
			array[3].Value = model.PassWord;			
			array[4].Value = model.CreateTime;
			array[5].Value = model.LastLoginTime;
			array[6].Value = model.TotalNum;
			array[7].Value = model.FailCount;
			array[8].Value = model.OperateLevel;
			array[9].Value = model.Note;
			array[10].Value = model.DisDate;
			array[11].Value = model.Signature;
			array[12].Value = model.DisCountRate;
			array[13].Value = model.Sex;
			array[14].Value = model.Is_Del;
			array[15].Value = model.VocationType;
			array[16].Value = model.UserID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int UserID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from SYSOpUser ");
			stringBuilder.Append(" where UserID=@UserID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_User", SqlDbType.Int, 4)
			};
			array[0].Value = UserID;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string UserIDlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from SYSOpUser ");
			stringBuilder.Append(" where UserID in (" + UserIDlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.SYSOpUser GetModel(int UserID)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 [UserID],[SectionID],[UserName],[LoginName],[PassWord],[LastLoginTime],[Note],[DisDate],[Signature],[DisCountRate],[Sex],[Is_Del],[VocationType],[OperateLevel],[TotalNum],[FailCount],[CreateTime] from SYSOpUser ");
			stringBuilder.Append(" where UserID=@UserID");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@UserID", SqlDbType.Int, 4)
			};
			array[0].Value = UserID;
			PEIS.Model.SYSOpUser sysopUser = new PEIS.Model.SYSOpUser();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.SYSOpUser result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["UserID"].ToString() != "")
				{
                    sysopUser.UserID = int.Parse(dataSet.Tables[0].Rows[0]["UserID"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["SectionID"].ToString() != "")
				{
                    sysopUser.SectionID = new int?(int.Parse(dataSet.Tables[0].Rows[0]["SectionID"].ToString()));
				}
                sysopUser.UserName = dataSet.Tables[0].Rows[0]["UserName"].ToString();
                sysopUser.LoginName = dataSet.Tables[0].Rows[0]["LoginName"].ToString();
                sysopUser.PassWord = dataSet.Tables[0].Rows[0]["PassWord"].ToString();              
				if (dataSet.Tables[0].Rows[0]["CreateTime"].ToString() != "")
				{
                    sysopUser.CreateTime = DateTime.Parse(dataSet.Tables[0].Rows[0]["CreateTime"].ToString());
				}
				if (dataSet.Tables[0].Rows[0]["LastLoginTime"].ToString() != "")
				{
                    sysopUser.LastLoginTime = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["LastLoginTime"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["TotalNum"].ToString() != "")
				{
                    sysopUser.TotalNum = new int?(int.Parse(dataSet.Tables[0].Rows[0]["TotalNum"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["FailCount"].ToString() != "")
				{
                    sysopUser.FailCount = new int?(int.Parse(dataSet.Tables[0].Rows[0]["FailCount"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["OperateLevel"].ToString() != "")
				{
                    sysopUser.OperateLevel = new int?(int.Parse(dataSet.Tables[0].Rows[0]["OperateLevel"].ToString()));
				}
                sysopUser.Note = dataSet.Tables[0].Rows[0]["Note"].ToString();
				if (dataSet.Tables[0].Rows[0]["DisDate"].ToString() != "")
				{
                    sysopUser.DisDate = new DateTime?(DateTime.Parse(dataSet.Tables[0].Rows[0]["DisDate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Signature"].ToString() != "")
				{
                    sysopUser.Signature = (byte[])dataSet.Tables[0].Rows[0]["Signature"];
				}
				if (dataSet.Tables[0].Rows[0]["DisCountRate"].ToString() != "")
				{
                    sysopUser.DisCountRate = new int?(int.Parse(dataSet.Tables[0].Rows[0]["DisCountRate"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Sex"].ToString() != "")
				{
                    sysopUser.Sex = new int?(int.Parse(dataSet.Tables[0].Rows[0]["Sex"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["Is_Del"].ToString() != "")
				{
                    sysopUser.Is_Del = new int?(int.Parse(dataSet.Tables[0].Rows[0]["Is_Del"].ToString()));
				}
				if (dataSet.Tables[0].Rows[0]["VocationType"].ToString() != "")
				{
                    sysopUser.VocationType = new int?(int.Parse(dataSet.Tables[0].Rows[0]["VocationType"].ToString()));
				}
				result = sysopUser;
			}
			else
			{
				result = null;
			}
			return result;
		}

		public DataSet GetList(string strWhere)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select [UserID],[SectionID],[UserName],[LoginName],[PassWord],[LastLoginTime],[Note],[DisDate],[Signature],[DisCountRate],[Sex],[Is_Del],[VocationType],[OperateLevel],[TotalNum],[FailCount],[CreateTime] ");
			stringBuilder.Append(" FROM SYSOpUser ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			return DbHelperSQL.Query(stringBuilder.ToString());
		}

		public DataSet GetList(int Top, string strWhere, string filedOrder)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select ");
			if (Top > 0)
			{
				stringBuilder.Append(" top " + Top.ToString());
			}
			stringBuilder.Append(" [UserID],[SectionID],[UserName],[LoginName],[PassWord],[LastLoginTime],[Note],[DisDate],[Signature],[DisCountRate],[Sex],[Is_Del],[VocationType],[OperateLevel],[TotalNum],[FailCount],[CreateTime]");
			stringBuilder.Append(" FROM SYSOpUser ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
