using PEIS.IDAL;
using PEIS.Model;
using PEIS.DBUtility;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PEIS.SQLServerDAL
{
	public class BusSpecimen : IBusSpecimen
	{
		public int GetMaxId()
		{
			return DbHelperSQL.GetMaxID("ID_Specimen", "BusSpecimen");
		}

		public bool Exists(int ID_Specimen)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select count(1) from BusSpecimen");
			stringBuilder.Append(" where ID_Specimen=@ID_Specimen ");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Specimen", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Specimen;
			return DbHelperSQL.Exists(stringBuilder.ToString(), array);
		}

		public int Add(PEIS.Model.BusSpecimen model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("insert into BusSpecimen(");
			stringBuilder.Append("SpecimenName,InputCode,DispOrder,LisSpecimenName)");
			stringBuilder.Append(" values (");
			stringBuilder.Append("@SpecimenName,@InputCode,@DispOrder,@LisSpecimenName)");
			stringBuilder.Append(";select @@IDENTITY");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@SpecimenName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@LisSpecimenName", SqlDbType.VarChar, 50)
			};
			array[0].Value = model.SpecimenName;
			array[1].Value = model.InputCode;
			array[2].Value = model.DispOrder;
			array[3].Value = model.LisSpecimenName;
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

		public bool Update(PEIS.Model.BusSpecimen model)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("update BusSpecimen set ");
			stringBuilder.Append("SpecimenName=@SpecimenName,");
			stringBuilder.Append("InputCode=@InputCode,");
			stringBuilder.Append("DispOrder=@DispOrder,");
			stringBuilder.Append("LisSpecimenName=@LisSpecimenName");
			stringBuilder.Append(" where ID_Specimen=@ID_Specimen");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@SpecimenName", SqlDbType.VarChar, 50),
				new SqlParameter("@InputCode", SqlDbType.VarChar, 10),
				new SqlParameter("@DispOrder", SqlDbType.Int, 4),
				new SqlParameter("@LisSpecimenName", SqlDbType.VarChar, 50),
				new SqlParameter("@ID_Specimen", SqlDbType.Int, 4)
			};
			array[0].Value = model.SpecimenName;
			array[1].Value = model.InputCode;
			array[2].Value = model.DispOrder;
			array[3].Value = model.LisSpecimenName;
			array[4].Value = model.ID_Specimen;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool Delete(int ID_Specimen)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusSpecimen ");
			stringBuilder.Append(" where ID_Specimen=@ID_Specimen");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Specimen", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Specimen;
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString(), array);
			return num > 0;
		}

		public bool DeleteList(string ID_Specimenlist)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("delete from BusSpecimen ");
			stringBuilder.Append(" where ID_Specimen in (" + ID_Specimenlist + ")  ");
			int num = DbHelperSQL.ExecuteSql(stringBuilder.ToString());
			return num > 0;
		}

		public PEIS.Model.BusSpecimen GetModel(int ID_Specimen)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select  top 1 ID_Specimen,SpecimenName,InputCode,DispOrder,LisSpecimenName from BusSpecimen ");
			stringBuilder.Append(" where ID_Specimen=@ID_Specimen");
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@ID_Specimen", SqlDbType.Int, 4)
			};
			array[0].Value = ID_Specimen;
			PEIS.Model.BusSpecimen busSpecimen = new PEIS.Model.BusSpecimen();
			DataSet dataSet = DbHelperSQL.Query(stringBuilder.ToString(), array);
			PEIS.Model.BusSpecimen result;
			if (dataSet.Tables[0].Rows.Count > 0)
			{
				if (dataSet.Tables[0].Rows[0]["ID_Specimen"].ToString() != "")
				{
					busSpecimen.ID_Specimen = int.Parse(dataSet.Tables[0].Rows[0]["ID_Specimen"].ToString());
				}
				busSpecimen.SpecimenName = dataSet.Tables[0].Rows[0]["SpecimenName"].ToString();
				busSpecimen.InputCode = dataSet.Tables[0].Rows[0]["InputCode"].ToString();
				if (dataSet.Tables[0].Rows[0]["DispOrder"].ToString() != "")
				{
					busSpecimen.DispOrder = int.Parse(dataSet.Tables[0].Rows[0]["DispOrder"].ToString());
				}
				busSpecimen.LisSpecimenName = dataSet.Tables[0].Rows[0]["LisSpecimenName"].ToString();
				result = busSpecimen;
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
			stringBuilder.Append("select ID_Specimen,SpecimenName,InputCode,DispOrder,LisSpecimenName ");
			stringBuilder.Append(" FROM BusSpecimen ");
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
			stringBuilder.Append(" ID_Specimen,SpecimenName,InputCode,DispOrder,LisSpecimenName ");
			stringBuilder.Append(" FROM BusSpecimen ");
			if (strWhere.Trim() != "")
			{
				stringBuilder.Append(" where " + strWhere);
			}
			stringBuilder.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(stringBuilder.ToString());
		}
	}
}
