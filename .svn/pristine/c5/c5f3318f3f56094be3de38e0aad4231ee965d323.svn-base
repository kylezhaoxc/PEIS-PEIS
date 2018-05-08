using PEIS.Base;
using PEIS.Common;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Fee
{
	public class FeeExamRelOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "收费项目明细设置");
			int @int = base.GetInt("FeeID", 0);
			string value = Input.URLDecode(base.GetString("feename"));
			int int2 = base.GetInt("sectionid", 0);
			vltContext.Put("ID_Fee", @int);
			vltContext.Put("FeeName", value);
			vltContext.Put("ID_Section", int2);
		}
	}
}
