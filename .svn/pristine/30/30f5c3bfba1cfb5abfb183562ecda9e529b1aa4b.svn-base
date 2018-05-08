using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;

namespace PEIS.BLL
{
	public class Jet4DB
	{
		private OleDbConnection getDBConn(string ExcelFilePath)
		{
			OleDbConnection result = null;
			try
			{
				string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties='Excel 8.0;HDR=YES;IMEX=1';";
				result = new OleDbConnection(connectionString);
			}
			catch (System.Exception ex)
			{
				throw ex;
			}
			return result;
		}

		public System.Collections.ArrayList ExcelSheetName(string ExcelFilePath)
		{
			System.Collections.ArrayList arrayList = new System.Collections.ArrayList();
			OleDbConnection dBConn = this.getDBConn(ExcelFilePath);
			if (dBConn != null)
			{
				try
				{
					dBConn.Open();
					DataTable oleDbSchemaTable = dBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
					{
						null,
						null,
						null,
						"TABLE"
					});
					dBConn.Close();
					foreach (DataRow dataRow in oleDbSchemaTable.Rows)
					{
						arrayList.Add(dataRow[2]);
					}
					dBConn.Dispose();
				}
				catch
				{
				}
			}
			return arrayList;
		}

		public DataSet dataset(string ExcelFilePath, string workSheetName, ref string sRet)
		{
			sRet = "";
			DataSet dataSet = new DataSet();
			OleDbConnection oleDbConnection = null;
			try
			{
				oleDbConnection = this.getDBConn(ExcelFilePath);
				if (oleDbConnection != null)
				{
					oleDbConnection.Open();
					if (workSheetName.IndexOf("$") == -1)
					{
						workSheetName += "$";
					}
					string selectCommandText = string.Format("SELECT * FROM [{0}]", workSheetName);
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, oleDbConnection.ConnectionString);
					oleDbDataAdapter.Fill(dataSet);
				}
			}
			catch (System.Exception ex)
			{
				sRet = "导入Excel文件过程中发生错误：" + ex.Message;
			}
			finally
			{
				if (oleDbConnection != null)
				{
					oleDbConnection.Close();
				}
			}
			return dataSet;
		}
	}
}
