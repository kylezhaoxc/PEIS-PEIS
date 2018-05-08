using PEIS.Base;
using PEIS.Common;
using PEIS.BLL;
using System;
using System.Collections;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PEIS.Web.System.Customer
{
	public class SelectExcelInfo : BasePage
	{
		protected Label lblType;

		protected LinkButton lbImport;

		protected Label lblResult;

		protected HtmlAnchor btnCancel;

		protected HtmlInputHidden hdCover;

		protected HtmlInputHidden HdWorkSheetName;

		protected HtmlInputHidden HdExcelFilePath;

		protected HtmlInputHidden HdError;

		protected HtmlSelect ddlSelectExcelInfo;

		protected HtmlInputHidden HdOpt;

		private Jet4DB _Jet4DB = new Jet4DB();

		private void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (base.Request["FilePath"] != null)
			{
				string text = base.Request["FilePath"].ToString();
				try
				{
					ArrayList arrayList = this._Jet4DB.ExcelSheetName(text);
					this.ddlSelectExcelInfo.Items.Clear();
					for (int i = 0; i < arrayList.Count; i++)
					{
						this.ddlSelectExcelInfo.Items.Add(new ListItem(arrayList[i].ToString().Replace("'", string.Empty).Replace("$", string.Empty), arrayList[i].ToString().Replace("'", string.Empty).Replace("$", string.Empty)));
					}
				}
				catch (Exception ex)
				{
					Log4J.Instance.Info("Error:" + ex.Message + "filePath:" + text);
				}
			}
		}

		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}

		private void InitializeComponent()
		{
			base.Load += new EventHandler(this.Page_Load);
		}
	}
}
