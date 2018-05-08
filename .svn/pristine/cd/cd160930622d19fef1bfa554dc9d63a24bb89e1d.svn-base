using System;
using System.Data;
using System.Data.OleDb;

namespace PEIS.BLL
{
	public class ExcellCommand
	{
		public static DataSet ExcelToDataSet(string filePathName, string workSheetName, ref string ErrorMessage)
		{
			DataSet result;
			if (filePathName == null || filePathName.Trim() == "" || workSheetName == null || workSheetName.Trim() == "")
			{
				result = null;
			}
			else
			{
				DataSet dataSet = new DataSet();
				string text = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePathName + ";Extended Properties=Excel 8.0;";
				OleDbConnection oleDbConnection = new OleDbConnection(text);
				try
				{
					oleDbConnection.Open();
					string selectCommandText = string.Format("SELECT * FROM [{0}$]", workSheetName);
					OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(selectCommandText, text);
					oleDbDataAdapter.Fill(dataSet);
					oleDbConnection.Close();
					oleDbConnection.Dispose();
				}
				catch (System.Exception ex)
				{
					if (oleDbConnection.State != ConnectionState.Closed)
					{
						oleDbConnection.Close();
						oleDbConnection.Dispose();
					}
					ErrorMessage = ex.Message;
				}
				result = dataSet;
			}
			return result;
		}
	}
}
