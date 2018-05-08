using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Conclusion
{
	public class ConclusionUnCheck : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "解除总审");
			long @int = base.GetInt64("txtCustomerID", 0L);
			if (@int > 0L)
			{
				vltContext.Put("txtCustomerID", @int.ToString());
			}
		}
	}
}
