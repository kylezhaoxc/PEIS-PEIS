using PEIS.Base;
using NVelocity;
using System;
using System.Web.UI.HtmlControls;

namespace PEIS.Web.System.Customer
{
	public class ShowDialog : BasePage
	{
		protected HtmlForm Form1;

		public string frameSrc;

		public string title;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["Type"] != null)
			{
				string text = base.Request["Type"].ToString();
				if (text != null)
				{
					if (!(text == "Excel"))
					{
						if (text == "Word")
						{
							this.title = "从Word文件中导入客户名单";
						}
					}
					else
					{
						this.title = "从Excel中导入客户名单";
						if (base.Request["Path"] != null)
						{
							text = base.Request["Path"].ToString();
							if (text != null)
							{
								if (!(text == "KHMD"))
								{
									if (!(text == "Word"))
									{
									}
								}
								else
								{
									this.title = "从Excel中导入客户名单";
								}
							}
						}
					}
				}
			}
			if (base.Request["Page"] != null)
			{
				string newValue = base.Request["Page"];
				string oldValue = "ShowDialog.aspx";
				string text2 = base.Request.Url.ToString();
				this.frameSrc = text2.Replace(oldValue, newValue).ToLower();
			}
		}

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("frameSrc", this.frameSrc);
			vltContext.Put("title", this.title);
		}
	}
}
