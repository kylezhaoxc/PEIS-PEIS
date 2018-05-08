using PEIS.Base;
using PEIS.Common;
using NVelocity;
using System;

namespace PEIS.Web.System.GiveUpManage
{
	public class GiveUpManageOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "弃检操作");
			long @int = base.GetInt64("txtCustomerID", 0L);
			if (@int > 0L)
			{
				vltContext.Put("txtCustomerID", @int.ToString());
			}
			int num = 10;
			try
			{
				string reportReceipteDays = BaseConfig.ReportReceipteDays;
				if (!string.IsNullOrEmpty(reportReceipteDays))
				{
					num = int.Parse(reportReceipteDays);
				}
			}
			catch (Exception var_3_7A)
			{
				num = 10;
			}
			vltContext.Put("DefaultReceliveDate", DateTime.Now.AddDays((double)num).ToString("yyyy-MM-dd"));
		}
	}
}
