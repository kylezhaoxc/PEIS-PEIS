using PEIS.Base;
using NVelocity;
using System;
using System.Data;
using System.Web;
using System.Xml;

namespace PEIS.Web.System.Statistics
{
	public class BackLogWorkLoad : BasePage
	{
		private string _backLogPath = HttpContext.Current.Server.MapPath("~/config/base/BackLogSetting.xml");

		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("flag", base.GetString("flag").ToLower());
			vltContext.Put("type", base.GetString("type").ToLower());
			vltContext.Put("modelName", base.GetString("modelName").ToLower());
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy-MM-dd"));
			vltContext.Put("pageTitle", "积案处理");
			vltContext.Put("BackWorkDT", this.GetBackWorkFlow());
		}

		private DataTable GetBackWorkFlow()
		{
			DataTable dataTable = new DataTable();
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(this._backLogPath);
			XmlElement documentElement = xmlDocument.DocumentElement;
			XmlNode xmlNode = documentElement.SelectSingleNode("BackLogWorkLoad");
			DataSet dataSet = new DataSet();
			if (documentElement != null)
			{
				string value = documentElement.Attributes["COLUMNS"].Value;
				string[] array = value.Split(new char[]
				{
					','
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text = array2[i];
					if (!string.IsNullOrEmpty(text))
					{
						dataTable.Columns.Add(text);
					}
				}
				XmlNodeList xmlNodeList = documentElement.SelectNodes("WorkFlowItem");
				DataRow dataRow = null;
				foreach (XmlNode xmlNode2 in xmlNodeList)
				{
					dataRow = dataTable.NewRow();
					foreach (DataColumn dataColumn in dataTable.Columns)
					{
						dataRow[dataColumn] = xmlNode2.Attributes[dataColumn.ColumnName].Value;
					}
					dataTable.Rows.Add(dataRow);
				}
			}
			dataTable.ReadXml(this._backLogPath);
			return dataTable;
		}
	}
}
