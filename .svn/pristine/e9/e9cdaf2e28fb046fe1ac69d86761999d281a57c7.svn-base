using PEIS.Base;
using PEIS.BLL;
using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace PEIS.Web.System.Customer
{
	public class ImportExcel : BasePage
	{
		protected Label lblType;

		protected LinkButton lbImport;

		protected Label lblResult;

		protected HtmlInputFile upload;

		protected HtmlAnchor btnCancel;

		protected HtmlInputHidden hdCover;

		protected HtmlInputHidden HdWorkSheetName;

		protected HtmlInputHidden HdExcelFilePath;

		protected HtmlInputHidden HdError;

		protected HtmlInputHidden HdOpt;

		private void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.lblType.Text = "客户信息";
			}
			else if (this.HdOpt.Value == "1")
			{
				try
				{
					string empty = string.Empty;
					string text = this.UploadFile();
					if (text == null)
					{
						return;
					}
					this.HdExcelFilePath.Value = text;
					this.HdWorkSheetName.Value = empty;
					if (empty == "-1")
					{
						this.Alert("请您选择需要导入的表名！", this.Page);
						return;
					}
					this.HdWorkSheetName.Value = empty;
					string text2 = text.Replace("|", "\\\\");
					string text3 = string.Empty;
					string value = this.HdWorkSheetName.Value;
					string text4 = "<Script Language=javascript>dialogArguments.ExcelFilePath='" + text.Replace("|", "\\\\") + "';";
					text4 = text4 + " dialogArguments.WorkSheetName='" + this.HdWorkSheetName.Value + "';";
					if (this.HdError.Value.Length == 0)
					{
						if (this.HdExcelFilePath.Value.Length > 0)
						{
							text3 = "ok";
							text4 += " dialogArguments.ReturnValue = 'ok';";
						}
						else
						{
							text3 = "no";
							text4 += " dialogArguments.ReturnValue='no';";
						}
					}
					else
					{
						text4 = text4 + " dialogArguments.ReturnValue='" + this.HdError.Value + "';";
					}
					text4 += " window.close(); </Script>";
					string str = string.Concat(new string[]
					{
						"{\"WorkSheetName\":\"",
						value,
						"\",\"ExcelFilePath\":\"",
						text.Replace("|", "\\\\"),
						"\",\"ReturnValue\":\"",
						text3,
						"\"}"
					});
					text4 = "<Script Language=javascript>parent.parent.art.dialog.data('aExcelFileInfo', " + str + "); parent.parent.art.dialog.get('OperWindowFrame').close();</Script>";
					this.RegisterClientScriptBlock(string.Empty, text4);
				}
				catch (Exception ex)
				{
					this.HdError.Value = ex.Message;
					this.HdOpt.Value = "";
					this.Alert(ex.Message, this.Page);
					return;
				}
				this.HdOpt.Value = "";
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

		public void Alert(string str_Message, Page page)
		{
			this.Page.RegisterClientScriptBlock("", "<script language=javascript> ShowSystemWarningDialog('" + str_Message + "'); </script>");
		}

		private DataSet ExcelToDataSet(string filePathName, string workSheetName, ref string ErrorMessage)
		{
			return ExcellCommand.ExcelToDataSet(filePathName, workSheetName, ref ErrorMessage);
		}

		private string UploadFile()
		{
			string fileName = Path.GetFileName(this.upload.PostedFile.FileName);
			string result;
			if (this.upload.PostedFile == null || this.upload.PostedFile.FileName.Length == 0 || this.upload.PostedFile.ContentLength == 0)
			{
				this.lblResult.Text = "请先选择文件！";
				result = null;
			}
			else if (Path.GetExtension(fileName) != ".xls")
			{
				this.lblResult.Text = "上传文件不是Excel格式！";
				result = null;
			}
			else
			{
				string str = (base.Request["TeamTaskGroupID"] == null) ? (DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls") : (base.Request["TeamTaskGroupID"].ToString() + ".xls");
				string path = "TempFile/" + str;
				string text = base.Server.MapPath(path);
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(base.Server.MapPath("TempFile"));
				}
				if (Directory.Exists(base.Server.MapPath("TempFile")))
				{
					try
					{
						if (File.Exists(text))
						{
							File.Delete(text);
						}
						this.upload.PostedFile.SaveAs(text);
					}
					catch (Exception ex)
					{
						this.Alert(ex.Message.Replace("\\", "\\\\"), this.Page);
						result = null;
						return result;
					}
				}
				result = text.Replace("\\", "|");
			}
			return result;
		}
	}
}
