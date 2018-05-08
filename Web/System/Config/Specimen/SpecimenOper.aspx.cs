using PEIS.Base;
using PEIS.BLL;
using PEIS.Model;
using NVelocity;
using System;

namespace PEIS.Web.System.Config.Specimen
{
	public class SpecimenOper : BasePage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			this.ProcessRequest();
		}

		public override void ReplaceContent(ref VelocityContext vltContext)
		{
			vltContext.Put("webName", this.SiteName);
			int @int = base.GetInt("SpecimenID", 0);
			vltContext.Put("pageTitle", "新增样本");
			if (@int > 0)
			{
				vltContext.Put("pageTitle", "修改样本");
				this.GetEditSpecimenInfo(@int, ref vltContext);
			}
		}

		protected void GetEditSpecimenInfo(int ID_Specimen, ref VelocityContext vltContext)
		{
			if (ID_Specimen > 0)
			{
				PEIS.Model.BusSpecimen model = PEIS.BLL.BusSpecimen.Instance.GetModel(ID_Specimen);
				if (null != model)
				{
					vltContext.Put("ID_Specimen", model.ID_Specimen);
					vltContext.Put("SpecimenName", model.SpecimenName);
					vltContext.Put("DispOrder", model.DispOrder);
					vltContext.Put("InputCode", model.InputCode);
					vltContext.Put("LisSpecimenName", model.LisSpecimenName);
				}
			}
		}
	}
}
