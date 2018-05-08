using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Customer
{
	public class PrintCustomerGuide : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public void OutPutMessage(string msg)
		{
			base.Response.Write(msg);
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("CurDate", DateTime.Now.ToString("yyyy年M月dd日"));
			vltContext.Put("pageTitle", "补打指引单");
		}
	}
}
