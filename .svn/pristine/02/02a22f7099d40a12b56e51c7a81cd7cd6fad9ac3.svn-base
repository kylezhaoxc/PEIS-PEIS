using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;
using System.Collections.Generic;

namespace PEIS.Web.System.Customer
{
	public class RegistList : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.TemplateName = "blue2";
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			vltContext.Put("pageTitle", "客户登记");
			string @string = base.GetString("IsTeam");
			string string2 = base.GetString("modelName");
			if (@string != "1")
			{
				if (string2 == "Sign")
				{
					vltContext.Put("pageTitle", "个人登记");
				}
				else
				{
					vltContext.Put("pageTitle", "个人预约");
				}
			}
			else if (string2 == "Sign")
			{
				vltContext.Put("pageTitle", "团体登记");
			}
			vltContext.Put("SYSOpUserModelList", this.GetUserModelList());
		}

		private List<PEIS.Model.SYSOpUser> GetUserModelList()
		{
			string strWhere = "";
			return PEIS.BLL.SYSOpUser.Instance.GetModelList(strWhere);
		}
	}
}
