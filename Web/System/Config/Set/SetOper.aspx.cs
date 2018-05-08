using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Set
{
	public class SetOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("SetID", 0);
			vltContext.Put("pageTitle", "新增套餐");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改套餐");
				this.GetEditSetInfo(ref vltContext);
			}
		}

		protected void GetEditSetInfo(ref VelocityContext vltContext)
		{
			int @int = base.GetInt("PEPackageID", 0);
			if (@int > 0)
			{
				PEIS.Model.BusPEPackage model = PEIS.BLL.BusPEPackage.Instance.GetModel(@int);
				vltContext.Put("PEPackageID", model.PEPackageID);
				vltContext.Put("PEPackageName", model.PEPackageName);
				vltContext.Put("IDBanOpr", model.IDBanOpr);
				vltContext.Put("CreatorID", model.CreatorID);
				vltContext.Put("InputCode", model.InputCode);
				vltContext.Put("isBanned", model.isBanned);
				vltContext.Put("Note", model.Note);
				vltContext.Put("BanDescribe", model.BanDescribe);
				vltContext.Put("DispOrder", model.DispOrder);
				vltContext.Put("BanDate", model.BanDate);
				vltContext.Put("CreateDate", model.CreateDate);
				vltContext.Put("Forsex", model.Forsex);
			}
		}
	}
}
