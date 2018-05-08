using PEIS.Base;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.COM
{
	public class config : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
		}
	}
}
