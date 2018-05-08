using PEIS.Base;
using NVelocity;
using System;

namespace FYH.Web.System.Exam
{
	public class VideoCapture : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "视频采集卡");
		}
	}
}
