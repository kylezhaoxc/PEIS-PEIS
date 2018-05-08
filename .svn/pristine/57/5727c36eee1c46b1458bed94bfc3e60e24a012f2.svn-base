using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Conclusion
{
	public class FinalConclusionTypeOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("FinalConclusionTypeID", 0);
			vltContext.Put("pageTitle", "新增结论类型");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改结论类型");
				this.GetEditFinalConclusionTypeInfo(@int, ref vltContext);
			}
		}

		protected void GetEditFinalConclusionTypeInfo(int ID_FinalConclusionType, ref VelocityContext vltContext)
		{
			if (ID_FinalConclusionType > 0)
			{
				PEIS.Model.DctFinalConclusionType model = PEIS.BLL.DctFinalConclusionType.Instance.GetModel(ID_FinalConclusionType);
				if (null != model)
				{
					vltContext.Put("ID_FinalConclusionType", model.ID_FinalConclusionType);
					vltContext.Put("FinalConclusionTypeName", model.FinalConclusionTypeName);
					vltContext.Put("Is_Banned", model.Is_Banned);
					vltContext.Put("ID_BanOpr", model.ID_BanOpr);
					vltContext.Put("BanOperator", model.BanOperator);
					vltContext.Put("BanDescribe", model.BanDescribe);
					vltContext.Put("BanDate", model.BanDate);
					vltContext.Put("DispOrder", model.DispOrder);
					vltContext.Put("Note", model.Note);
					vltContext.Put("FinalConclusionSignCode", model.FinalConclusionSignCode);
				}
			}
		}
	}
}
