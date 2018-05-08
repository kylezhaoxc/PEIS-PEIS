using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Text;

namespace PEIS.Common
{
	public class JsonHelperFont
	{
		private static readonly JsonHelperFont _instance = new JsonHelperFont();

		public static JsonHelperFont Instance
		{
			get
			{
				return JsonHelperFont._instance;
			}
		}

		public string DataTableToJSON(DataTable dt, string dtName)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder);
			using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
			{
				JsonSerializer jsonSerializer = new JsonSerializer();
				jsonWriter.WriteStartObject();
				jsonWriter.WritePropertyName(dtName);
				jsonWriter.WriteStartArray();
				foreach (DataRow dataRow in dt.Rows)
				{
					jsonWriter.WriteStartObject();
					foreach (DataColumn dataColumn in dt.Columns)
					{
						jsonWriter.WritePropertyName(dataColumn.ColumnName);
						jsonSerializer.Serialize(jsonWriter, dataRow[dataColumn].ToString());
					}
					jsonWriter.WriteEndObject();
				}
				jsonWriter.WriteEndArray();
				jsonWriter.WriteEndObject();
				stringWriter.Close();
				jsonWriter.Close();
			}
			return stringBuilder.ToString();
		}

		public string DataTableToJSON(DataTable dt, int totalCount)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder);
			using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
			{
				JsonSerializer jsonSerializer = new JsonSerializer();
				jsonWriter.WriteStartObject();
				jsonWriter.WritePropertyName("totalCount");
				jsonSerializer.Serialize(jsonWriter, totalCount.ToString());
				jsonWriter.WritePropertyName("dataList");
				jsonWriter.WriteStartArray();
				foreach (DataRow dataRow in dt.Rows)
				{
					jsonWriter.WriteStartObject();
					foreach (DataColumn dataColumn in dt.Columns)
					{
						jsonWriter.WritePropertyName(dataColumn.ColumnName);
						jsonSerializer.Serialize(jsonWriter, dataRow[dataColumn].ToString());
					}
					jsonWriter.WriteEndObject();
				}
				jsonWriter.WriteEndArray();
				jsonWriter.WriteEndObject();
				stringWriter.Close();
				jsonWriter.Close();
			}
			return stringBuilder.ToString();
		}

		public string DataSetToJSON(DataSet ds)
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			StringWriter stringWriter = new StringWriter(stringBuilder);
			string result;
			if (ds == null || ds.Tables.Count <= 0)
			{
				result = "";
			}
			else
			{
				using (JsonWriter jsonWriter = new JsonTextWriter(stringWriter))
				{
					JsonSerializer jsonSerializer = new JsonSerializer();
					jsonWriter.WriteStartObject();
					jsonWriter.WritePropertyName("totalCount");
					jsonSerializer.Serialize(jsonWriter, ds.Tables.Count.ToString());
					for (int i = 0; i < ds.Tables.Count; i++)
					{
						jsonWriter.WritePropertyName("dataList" + i);
						jsonWriter.WriteStartArray();
						foreach (DataRow dataRow in ds.Tables[i].Rows)
						{
							jsonWriter.WriteStartObject();
							foreach (DataColumn dataColumn in ds.Tables[i].Columns)
							{
								jsonWriter.WritePropertyName(dataColumn.ColumnName);
								jsonSerializer.Serialize(jsonWriter, dataRow[dataColumn].ToString());
							}
							jsonWriter.WriteEndObject();
						}
						jsonWriter.WriteEndArray();
					}
					jsonWriter.WriteEndObject();
					stringWriter.Close();
					jsonWriter.Close();
				}
				result = stringBuilder.ToString();
			}
			return result;
		}
	}
}
