using PEIS.Base;
using PEIS.Common;
using System;

namespace PEIS.Web
{
	public class Logout : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			ClientListManagement.Instance().ClearMe(this.Session.SessionID);
			this.Session["UserID"] = "0";
			BasePage.ClearCookie("");
			base.Redirect("/Login.aspx?flag=logout");
		}
	}
}
