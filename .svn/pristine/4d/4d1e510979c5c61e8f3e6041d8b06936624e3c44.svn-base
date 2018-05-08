using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.User
{
	public class UserOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("UserID", 0);
			vltContext.Put("pageTitle", "新增用户");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改用户");
				this.GetEditUserInfo(@int, ref vltContext);
			}
		}

		protected void GetEditUserInfo(int UserID, ref VelocityContext vltContext)
		{
			PEIS.Model.SYSOpUser model = PEIS.BLL.SYSOpUser.Instance.GetModel(UserID);
			if (model != null)
			{
				vltContext.Put("UserID", model.UserID);
				vltContext.Put("UserName", model.UserName);
				vltContext.Put("SectionID", model.SectionID);
				vltContext.Put("DisCountRate", model.DisCountRate);
				vltContext.Put("OperateLevel", model.OperateLevel);
				vltContext.Put("VocationType", model.VocationType);
				vltContext.Put("Sex", model.Sex);
				vltContext.Put("Signature", model.Signature);
				vltContext.Put("Is_Del", model.Is_Del);
				vltContext.Put("Note", model.Note);
				string sectionName = CommonSystemInfo.Instance.GetSectionName(int.Parse(model.SectionID.ToString()));
				vltContext.Put("SectionName", sectionName);
			}
		}
	}
}
