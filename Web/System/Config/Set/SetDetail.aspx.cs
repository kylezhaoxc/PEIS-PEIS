using PEIS.Base;
using PEIS.Common;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Set
{
	public class SetDetail : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "套餐明细设置");
			int @int = base.GetInt("SetID", 0);
			string value = Input.URLDecode(base.GetString("setname"));
			int int2 = base.GetInt("sectionid", 0);
			vltContext.Put("PEPackageID", @int);
			vltContext.Put("PEPackageName", value);
			vltContext.Put("ID_Section", int2);
		}
	}
}
